##### This script depends on the ValidateWhiteSourceResult.ps1 ######

[CmdletBinding()]
Param(
    [Parameter(Mandatory=$True,Position=1)]
    [string]$WhitesourceProductToken,
   
    [Parameter(Mandatory=$True,Position=2)]
    [string]$WhitesourceApiUrl,

    [Parameter(Mandatory=$True, Position=3)]
    [string]$ReportDirectory,

    [Parameter(Mandatory=$True, Position=4)]
    [string]$ScriptDirectory

 )

Write-Host "Get all projects from product"
$body = @{
    requestType = 'getProductProjectVitals'
    productToken = $($WhitesourceProductToken)
}

$jsonBody = ConvertTo-Json $body

$headers = New-Object "System.Collections.Generic.Dictionary[[String],[String]]"
$headers.Add("cache-control", 'no-cache')
$headers.Add("content-type", 'application/json')
$headers.Add("charset", 'utf-8')

[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12

try {
    $response= Invoke-RestMethod -Uri $($WhitesourceApiUrl) -Headers $headers -Body $jsonBody -Method 'POST' -UseBasicParsing
} catch {
    Write-Host "StatusCode:" $_.Exception.Response.StatusCode.value__ 
    Write-Host "StatusDescription:" $_.Exception.Response.StatusDescription
    throw "failed to getProductProjectVitals"
}

$projects = $response.projectVitals

Write-Host "Analyze and save reports for each project from the product"
$scriptPath="$($ScriptDirectory)\ValidateWhiteSourceResult.ps1"
foreach ($project in $projects) {
    $name=$project.name
    $token=$project.token
    Write-Host $name
    Write-Host "Validating and pushing WhiteSource scan results for component ${name}"
    Try {
      $command = $scriptPath + " '${token}' '$($ReportDirectory)' '${name}' '$($WhitesourceApiUrl)'"
      Invoke-Expression $command
    }
    Catch {
       Write-Host $_.Exception
    }
}