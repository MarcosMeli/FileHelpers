.nuget\NuGet.exe install .nuget\packages.config -OutputDirectory ..\packages
.nuget\NuGet.exe restore ..\FileHelpers.sln

Import-Module '..\packages\psake.*\tools\psake\psake.psm1';
invoke-psake default.ps1 test, pack;

if ($LastExitCode -ne 0)
{
  write-host "ERROR: $LastExitCode" -fore RED;
  exit $LastExitCode
}