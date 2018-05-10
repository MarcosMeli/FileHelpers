Import-Module '..\packages\psake.*\tools\psake\psake.psm1';
invoke-psake default.ps1 test, pack;

if ($LastExitCode -ne 0)
{
  write-host "ERROR: $LastExitCode" -fore RED;
  exit $LastExitCode
}