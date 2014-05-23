@echo off
SET EnableNuGetPackageRestore=true
cd ..
.nuget\NuGet.exe install .nuget\packages.config -OutputDirectory packages
.nuget\NuGet.exe restore FileHelpers.sln
powershell.exe -NoProfile -ExecutionPolicy unrestricted -Command "& {Import-Module '.\packages\psake.*\tools\psake.psm1'; invoke-psake .\Build\default.ps1 pack; if ($LastExitCode -ne 0) {write-host "ERROR: $LastExitCode" -fore RED; exit $lastexitcode} }"
