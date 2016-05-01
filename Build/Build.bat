@echo off
RMDIR ..\packages\psake.4.3.2 /s /q
SET EnableNuGetPackageRestore=true
.nuget\NuGet.exe install .nuget\packages.config -OutputDirectory ..\packages
.nuget\NuGet.exe restore ..\FileHelpers.sln
cd ..
powershell.exe -NoProfile -ExecutionPolicy unrestricted -Command "& {Import-Module '.\packages\psake.*\tools\psake.psm1'; invoke-psake .\Build\default.ps1 compile; if ($LastExitCode -ne 0) {write-host "ERROR: $LastExitCode" -fore RED; exit $lastexitcode} }"
