Include("CurrentVersion.ps1")

properties {
    $base_dir = resolve-path .
    $package_dir = "$base_dir\..\packages"
    $config = "Release"
}

#tasks ----------------------------------------------------------------------------

task default -depends pack

task version {
    Update-AssemblyInfoFile '..\FileHelpers\VersionInfo.cs' $AssemblyVersion $SemanticVersion
    Update-NuGetVersion '..\Build\NuGet\FileHelpers.ExcelStorage.nuspec' $SemanticVersion
    Update-NuGetVersion '..\Build\NuGet\FileHelpers.ExcelNPOIStorage.nuspec' $SemanticVersion
    Update-NuGetVersion '..\Build\NuGet\FileHelpers.nuspec' $SemanticVersion
}

task common -depends version {
     Delete-Make-Directory ..\$config
     Delete-Make-Directory "..\Output"
}

task compile -depends common {
    "Compiling " + $config

    Compile-Sln-With-Deploy "..\FileHelpers.OnlyMainLib.sln" "3.5" "Lib\net35" ""
    Compile-Sln-With-Deploy "..\FileHelpers.OnlyLibs.sln" "4.0" "Lib\net40"
    Compile-Sln-With-Deploy "..\FileHelpers.OnlyLibs.sln" "4.5" "Lib\net45"

    Compile-Sln "..\FileHelpers.sln" "4.5"

    $delFiles = "..\" + $config + "\*.config"
    del $delFiles

    Delete-Directory ..\$config\bin
}

task compiledebug -depends common {
    $config = "Debug"
    "Compiling Debug"

    Compile-Sln "..\FileHelpers.sln" "4.5"
}

task docs -depends compile {
    "Documenting"

    exec { msbuild FileHelpers.shfbproj /p:Configuration=$config /nologo }

    Make-Directory ..\$config\Docs

    copy ..\Help\FileHelpers.chm ..\$config\Docs\FilHelpers.chm
}

task pack -depends compile {
    "Creating NuGet packages"

    ./.nuget/NuGet.exe pack ./Nuget/FileHelpers.nuspec -OutputDirectory ../Output
}

task test -depends compile{
    "Testing"

    ./../packages/NUnit.ConsoleRunner.3.6.1/tools/nunit3-console.exe ../Release/FileHelpers.Tests.dll
}

#functions ----------------------------------------------------

function Delete-Make-Directory($path)
{
    Delete-Directory $path
    Make-Directory $path
}

function Delete-Directory($path)
{
    rd $path -recurse -force -ErrorAction SilentlyContinue | out-null
}

function Make-Directory($path)
{
    md $path -ErrorAction SilentlyContinue | out-null
}

function Compile-Sln($path, $targetFramework)
{
    & 'C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe' $path /p:TargetFrameworkVersion=v$targetFramework /t:rebuild /p:Configuration=$config /nologo /verbosity:minimal
}

function Compile-Sln-With-Deploy($path, $targetFramework, $deploy)
{
    Compile-Sln $path $targetFramework
    $deployDir = "..\" + $config + "\" + $deploy 
    Make-Directory $deployDir

    $fromDir = "..\" + $config + "\Bin"
    $fromFiles = $fromDir + "\*.*"

    copy $fromFiles $deployDir

    Delete-Directory $fromDir
}

function Get-AssemblyInformationalVersion($path)
{
    $line = Get-Content $path | where {$_.Contains("AssemblyInformationalVersion")}
    $line.Split('"')[1]
}

function Update-AssemblyInformationalVersion
{
    if ($preReleaseVersion -ne $null)
    {
        $version = ([string]$input).Split('-')[0]
        $date = Get-Date
        $parsed = $preReleaseVersion.Replace("{date}", $date.ToString("yyMMdd"))
        return "$version-$parsed"
    }
    else
    {
        return $input
    }
}

function Create-Zip($sourcePath, $destinationFile)
{
    cd $package_dir\SharpZipLib.*\lib\20\

    Add-Type -Path ICSharpCode.SharpZipLib.dll

    $zip = New-Object ICSharpCode.SharpZipLib.Zip.FastZip
    $zip.CreateZip("$destinationFile", "$sourcePath", $true, $null)
}

function Update-NuGetVersion ([string] $filename, [string] $versionNumber)
{
    $versionPattern = '\<version\>[0-9]+(\.([0-9]+|\*)){1,3}\<\/version\>'
    $version = '<version>' + $versionNumber + '</version>';

    $dependenceVersionPattern = 'id="FileHelpers" version=\"[0-9]+(\.([0-9]+|\*)){1,3}\"'
    $dependenceVersion = 'id="FileHelpers" version="' + $versionNumber + '"';

    $filename + ' -> ' + $versionNumber

    (Get-Content $filename) | ForEach-Object {
         % {$_ -replace $versionPattern, $version } |
         % {$_ -replace $dependenceVersionPattern, $dependenceVersion }
    } | Set-Content $filename
}

function Update-AssemblyInfoFile ([string] $filename, [string] $assemblyVersionNumber, [string] $informationalVersionNumber)
{
    $assemblyVersionPattern = 'AssemblyVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)'
    $fileVersionPattern = 'AssemblyFileVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)'
    $informationalVersionPattern = 'AssemblyInformationalVersion\("[0-9]+(\.([0-9]+|\*)){1,3}(-[a-z]*)?"\)'
    $assemblyVersion = 'AssemblyVersion("' + $assemblyVersionNumber + '")';
    $fileVersion = 'AssemblyFileVersion("' + $assemblyVersionNumber + '")';
    $informationalVersion = 'AssemblyInformationalVersion("' + $informationalVersionNumber + '")';

    $filename + ' -> ' + $assemblyVersionNumber

    (Get-Content $filename) | ForEach-Object {
        % {$_ -replace $assemblyVersionPattern, $assemblyVersion } |
        % {$_ -replace $fileVersionPattern, $fileVersion } |
        % {$_ -replace $informationalVersionPattern, $informationalVersion }
    } | Set-Content $filename
}