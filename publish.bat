@echo off

set VERSION=%1
set OUT_DIR=%2
set PROJ_PATH=%cd%\src\TsaToolbox\TsaToolbox.csproj

dotnet publish %PROJ_PATH% --configuration Release --framework net48 --output %OUT_DIR%\tsa-toolbox\net48 -p:VersionPrefix=%VERSION%