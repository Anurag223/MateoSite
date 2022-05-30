@echo off
if "%1" == "f5" docker-compose -f docker-compose-local.yml -f docker-compose-local.override.yml up
if "%1" == "local" docker-compose -f docker-compose-local.yml -f docker-compose-local.override.yml up
if "%1" == "test" docker-compose -f docker-compose-external.yml -f docker-compose-external.override.yml up
if "%1" == "tests" docker-compose -f docker-compose-external.yml -f docker-compose-external.override.yml up
if "%1" == "fullup" docker-compose -f docker-compose.yml -f docker-compose.override.yml up
if "%1" == "" goto gethelp
goto end
:gethelp
echo.
echo To run dependency containers to support a debug session or run in VS.
echo    startdocker f5
echo    -- or --
echo    startdocker local
echo. 
echo To run dependency containers to support unit or integration testing:
echo    startdocker test
echo    -- or --
echo    startdocker tests
echo. 
echo To run all containers in the solution and all dependency containers:
echo    startdocker fullup
:end

