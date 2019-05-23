$config = "Release"

function Create-Folders() {
     Recreate-Directory ..\$config
     Recreate-Directory "..\Output"
}

function Compile-Solutions() {
    "Compiling " + $config

    Compile-MSBuild-With-Deploy "..\FileHelpers.OnlyLibs.sln" "4.0" "Lib\net40"
    Compile-MSBuild-With-Deploy "..\FileHelpers.OnlyLibs.sln" "4.5" "Lib\net45"

    Compile-MSBuild "..\FileHelpers.sln" "4.5"

    Compile-DotNet "..\FileHelpers\FileHelpers.NetStandard.csproj" "Lib\netstandard2.0"

    $delFiles = "..\" + $config + "\*.config"
    del $delFiles

    Delete-Directory ..\$config\bin
}

function Pack-Packages () {
    "Creating NuGet packages"

    ./.nuget/NuGet.exe pack ./Nuget/FileHelpers.nuspec -OutputDirectory ../Output
    Ensure-CleanExit
    ./.nuget/NuGet.exe pack ./Nuget/FileHelpers.ExcelNPOIStorage.nuspec -OutputDirectory ../Output
    Ensure-CleanExit
}

function Test-Projects() {
    "Testing"

    ./../packages/NUnit.ConsoleRunner.3.6.1/tools/nunit3-console.exe ../Release/FileHelpers.Tests.dll ../Release/FileHelpers.FSharp.Tests.dll
}

function Recreate-Directory($path)
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
    Ensure-CleanExit
    Move-To-Dir($deploy)
}

function Compile-MSBuild($path, $targetFramework)
{
    # found http://nichesoftware.co.nz/2017/08/05/finding-msbuild-psake-build.html
    $msbuild15 = resolve-path "C:\Program Files (x86)\Microsoft Visual Studio\*\*\MSBuild\*\Bin\MSBuild.exe"
    & $msbuild15.Path $path /p:TargetFrameworkVersion=v$targetFramework /t:rebuild /p:Configuration=$config /nologo /verbosity:minimal
    Ensure-CleanExit
}

function Ensure-CleanExit()
{
    if($LASTEXITCODE -ge 1)
    {
        throw
    }
}

function Compile-MSBuild-With-Deploy($path, $targetFramework, $deploy)
{
    Compile-MSBuild $path $targetFramework
    Move-To-Dir $deploy
}

function Move-To-Dir($deploy) {
    $deployDir = "..\" + $config + "\" + $deploy 
    Make-Directory $deployDir

    $fromDir = "..\" + $config + "\Bin"
    $fromFiles = $fromDir + "\*.*"

    copy $fromFiles $deployDir -Recurse

    Delete-Directory $fromDir
}

Create-Folders
Compile-Solutions
Test-Projects
Pack-Packages