[CmdletBinding()]
Param(

    [Parameter(Mandatory = $True, Position = 1)]
    [string]$UserKey,

    [Parameter(Mandatory = $True, Position = 2)]
    [string]$ProjectToken,

    [Parameter(Mandatory = $True, Position = 3)]
    [string]$ReportDirectory,

    [Parameter(Mandatory = $False, Position = 4)]
    [string]$component = "Main",

    [Parameter(Mandatory = $False, Position = 5)]
    [string]$whitesourceuri = "https://app-eu.whitesourcesoftware.com/api/v1.3"

)

Function AddComponentName( $c ) {
    if ( $c -eq "Main") {
        return "";
    }
    else {
        return "-" + $c;
    }
}


Function Generate-QPResultSummary($taskid, $outfile, $stepsuccess, $ReportDirectory, $ProjectToken, $mandatoryIssues, $qptype, $componentName) {

    ##### Write Json Result ######
    #$taskid = "54e01965-631d-4efa-b7d1-194c43c8f221"
    $outcome = "failed"
    # this expands the list in Ubuntu. Otherwise, it will fail if we search by Id
    if ( (Get-Item -Path Env:AGENT_OS).Value -eq "Linux" ) {
        $startdate = Get-Date -f "MM/dd/yyyy HH:mm:ss"
    }
    else {
        $cstzone = [System.TimeZoneInfo]::FindSystemTimeZoneById("Central Standard Time")
        $csttime = [System.TimeZoneInfo]::ConvertTimeFromUtc((Get-Date).ToUniversalTime(), $cstzone)
        $startdate = Get-Date $csttime.AddHours(-1) -f "MM/dd/yyyy HH:mm:ss"
    }
    $project = (Get-Item env:"QPAPP").Value
    $version = (Get-Item env:"QPAPPVERSION").Value
    $attachment = $outfile.ToLower().Replace($ReportDirectory.ToLower(), ".`\" ).Replace("`\", "`\`\" )
    $toolUsed = "White Source Software"
    $jsonFileOutput = "$($ReportDirectory)$($qptype)$($componentName)-$($id).json"
    $description = "White Source for Project $($ProjectToken)"
    $errorcontent = $mandatoryIssues
    $releaseId = ""
    $releaseName = ""
    $releaseUrl = ""
    $buildId = (Get-Item env:"BUILD_BUILDID").Value
    $buildNumber = (Get-Item env:"BUILD_BUILDNUMBER").Value

    if ( $stepsuccess -eq $FALSE ) {
        $outcome = "failed"
    }
    else {
        $outcome = "passed"
    }

    $arr = @()
    $att += [pscustomobject]@{
        "URI" = $attachment
    }

    $rootJson = [pscustomobject]@{
        id           = $taskid;
        state        = "completed";
        description  = $description;
        outcome      = $outcome;
        errorMessage = $errorcontent;
        startDate    = $startdate;
        attachments  = $att;
        toolUsed     = $toolUsed;
        qpTestType   = $qptype;
        project      = $project;
        version      = $version;
        buildId      = $buildId;
        buildNumber  = $buildNumber;
        releaseId    = $releaseId;
        releaseName  = $releaseName;
        component    = $component;
    }

    Write-Output "Summary from $($outfile) to $($jsonFileOutput)"

    $rootJson | ConvertTo-Json | Out-File $jsonFileOutput
    ###### end of Json Result #######

    if ( $stepsuccess -eq $FALSE ) {
        Write-Output "Error(s) found: " $mandatoryIssues
    }

}


$Uri = $whitesourceuri

$Headers = New-Object "System.Collections.Generic.Dictionary[[String],[String]]"
$Headers.Add("cache-control", 'no-cache')
$Headers.Add("Content-Type", 'application/json')
$Headers.Add("charset", 'utf-8')

$id = "54e01965-631d-4efa-b7d1-194c43c8f221"
$qpType = "SAQP-WhiteSourceLicense"
$jsonFileOutput = "$($ReportDirectory)$($qptype)" + (AddComponentName($component)) + "-$($id).json"
Remove-Item -Path $jsonFileOutput -Force -ErrorAction SilentlyContinue

$stepsuccess = $TRUE
$overallsuccess = $TRUE

#checking licenses
$body = @"
{
    "requestType" : "getProjectAlertsByType",
    "userKey":"$($UserKey)",
    "projectToken" : "$($ProjectToken)",
    "alertType" : "REJECTED_BY_POLICY_RESOURCE"
}
"@

$outfile = "$($ReportDirectory)/artifacts/SoftwareLicenses" + (AddComponentName($component)) + ".json"
$outdir = "$($ReportDirectory)/artifacts"

if ( !(Test-Path -Path "$($ReportDirectory)/artifacts")) {
    Write-Output "Creating $($ReportDirectory)/artifacts directory"
    New-Item -ItemType Directory -Force -Path "$($ReportDirectory)/artifacts"
}

Write-Output "Checking for licenses with Project Token $($ProjectToken)"

New-Item -ItemType directory -Path $outdir -Force -ErrorAction SilentlyContinue

Write-Output "Downloading Licensing Information"

[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
Invoke-WebRequest -Uri $Uri -Headers $Headers -usebasicparsing -Method POST -Body $body -OutFile $outfile

Write-Output "Reading Licensing Information"

if ( !(Test-Path -Path $outfile )) {
    $errorcontent = "Missing file $($outfile)"
    Write-Output $errorcontent
    $stepsuccess = $FALSE
    $overallsuccess = $FALSE
}
else {
    $responsecontent = [IO.File]::ReadAllText($outfile)
    $json = $responsecontent | ConvertFrom-Json

    if ( $responsecontent -match "errorCode" ) {
        $errorcontent += "$($responsecontent)`n`r"
        $stepsuccess = $FALSE
        $overallsuccess = $FALSE
    }
    else {
        # Get list of organization policies
        $jsonBody = @{
            requestType = "getOrganizationPolicies"
            orgToken    = "fd8128d952644fa98b610d373356a7e7b1b746355bb443fe89e2e7e41c69c10b" # Schlumberger organization API key from https://app-eu.whitesourcesoftware.com/Wss/WSS.html#!adminOrganization_integration
        } | ConvertTo-Json -Depth 1
        $response = Invoke-WebRequest -Uri $Uri -Headers $Headers -usebasicparsing -Method POST -Body $jsonBody

        if ( $response.StatusCode -ne 200) {
            $errorcontent = "getOrganizationPolicies failed. statusCode=$($response.StatusCode)"
            $stepsuccess = $FALSE
            $overallsuccess = $FALSE
        }
        else {
            # Filter policies where policy type is "LICENSE"
            $organizationPolicies = $response.Content | ConvertFrom-Json
            $licencePolicyNames = $organizationPolicies.Policies | Where-Object { $_.filter.type -eq "LICENSE" } | Select-Object -Property name | ForEach-Object { $_.name.Trim() }

            # Filter alerts where alert.description (containing licence name) is one of the disallowed licences
            $licenceAlerts = $json.alerts | Where-Object { $_.description.Trim() -in $licencePolicyNames }

            # Write licence alerts output file
            @{
                alerts = @($licenceAlerts)
            } | ConvertTo-Json -Depth 10 | Out-File $outfile

            Foreach ( $alert in $licenceAlerts ) {
                if ( $alert.level -eq "MAJOR" ) {
                    Foreach ( $lib in $alert.library ) {
                        Foreach ( $lic in $lib.licenses ) {
                            $errorcontent += "Lib $($lib.name) is under $($lic.name) license ($($lic.url))`n`r"
                            $stepsuccess = $FALSE
                            $overallsuccess = $FALSE
                        }
                    }
                }
            }

        }

    }
}
Generate-QPResultSummary $id $outfile $stepsuccess $ReportDirectory $ProjectToken $errorcontent $qpType (AddComponentName($component))


#checking alerts
$stepsuccess = $TRUE
$errorcontent = ""
$body = @"
{
    "requestType" : "getProjectAlerts",
    "userKey":"$($UserKey)",
    "projectToken" : "$($ProjectToken)"
}
"@

$outfile = "$($ReportDirectory)/artifacts/whitesourcealerts" + (AddComponentName($component)) + ".json"
Write-Output "Checking for alerts"

[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
Invoke-WebRequest -Uri $Uri -Headers $Headers -usebasicparsing -Method POST -Body $body -OutFile $outfile


if ( !(Test-Path -Path $outfile )) {
    $errorcontent = "Missing file $($outfile)"
    Write-Output $errorcontent
    $stepsuccess = $FALSE
    $overallsuccess = $FALSE
}
else {
    $responsecontent = [IO.File]::ReadAllText($outfile)
    $json = $responsecontent | ConvertFrom-Json

    if ( $responsecontent -match "errorCode" ) {
        $errorcontent += "$($responsecontent)`n`r"
        $stepsuccess = $FALSE
        $overallsuccess = $FALSE
    }
    else {
        Foreach ( $alert in $json.alerts ) {
            if ( $alert.type -eq "HIGH_SEVERITY_BUG" ) {
                if ( $alert.description -like "*Critical*" ) {
                    Write-Output $alert
                    $errorcontent += "High Critical Bug $($alert.library.filename) - $($alert.description)`n`r"
                    $stepsuccess = $FALSE
                    $overallsuccess = $FALSE
                }
            }
            else {
                if ( $alert.type -eq "SECURITY_VULNERABILITY" ) {
                    if ( $alert.description -like "*High*" ) {
                        Foreach ( $vul in $alert.vulnerability ) {
                            $errorcontent += "Vulnerability $($alert.library.filename) - $($vul.name) - $($alert.description)`n`r"
                            $stepsuccess = $FALSE
                            $overallsuccess = $FALSE
                        }

                    }

                }
            }
        }
    }
}


$id = "f126c271-5806-436a-8cd1-6fc84575549e"
Generate-QPResultSummary $id $outfile $stepsuccess $ReportDirectory $ProjectToken $errorcontent "SAQP-WhiteSourceVulnerability" (AddComponentName($component))


if ( $overallsuccess -eq $TRUE ) {
    exit 0
}
Write-Output "Overall Success of the Scan is:  $($overallsuccess)"
exit 0