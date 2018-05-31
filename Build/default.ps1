properties {
    $AssemblyVersion = "3.2.8"
    $SemanticVersion = $AssemblyVersion + "-alpha"   #for stable version, set postfix to empty
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

task common {
     Delete-Make-Directory ..\$config
     Delete-Make-Directory "..\Output"
}

task compile -depends common {
    "Compiling " + $config

    Compile-MSBuild-With-Deploy "..\FileHelpers.OnlyLibs.sln" "4.0" "Lib\net40"
    Compile-MSBuild-With-Deploy "..\FileHelpers.OnlyLibs.sln" "4.5" "Lib\net45"

    Compile-MSBuild "..\FileHelpers.sln" "4.5"

    Compile-DotNet "..\FileHelpers\FileHelpers.NetStandard.csproj" "Lib\netstandard2.0"

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
    ./.nuget/NuGet.exe pack ./Nuget/FileHelpers.ExcelNPOIStorage.nuspec -OutputDirectory ../Output
    ./.nuget/NuGet.exe pack ./Nuget/FileHelpers.ExcelStorage.nuspec -OutputDirectory ../Output
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

function Compile-DotNet($path, $deploy)
{
    & 'dotnet.exe' build $path -c $config
    MoveIt($deploy)
}

function Compile-MSBuild($path, $targetFramework)
{
    # found http://nichesoftware.co.nz/2017/08/05/finding-msbuild-psake-build.html
    $msbuild15 = resolve-path "C:\Program Files (x86)\Microsoft Visual Studio\*\*\MSBuild\*\Bin\MSBuild.exe"
    & $msbuild15.Path $path /p:TargetFrameworkVersion=v$targetFramework /t:rebuild /p:Configuration=$config /nologo /verbosity:minimal
}

function Compile-MSBuild-With-Deploy($path, $targetFramework, $deploy)
{
    Compile-MSBuild $path $targetFramework
    MoveIt $deploy
}

function MoveIt($deploy) {
    $deployDir = "..\" + $config + "\" + $deploy 
    Make-Directory $deployDir

    $fromDir = "..\" + $config + "\Bin"
    $fromFiles = $fromDir + "\*.*"

    copy $fromFiles $deployDir -Recurse

    Delete-Directory $fromDir
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