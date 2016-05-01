Include("CurrentVersion.ps1")

properties {
    $base_dir = resolve-path .
    $package_dir = "$base_dir\..\packages"
    $config = "Release"
}

#tasks ----------------------------------------------------------------------------

task default -depends pack

task version {
    Update-AssemblyInfoFile '..\FileHelpers\VersionInfo.cs' ($AssemblyVersion+'.0') $FullCurrentVersion $VisibleVersion
    Update-NuGetVersion '..\Build\NuGet\FileHelpers.ExcelStorage.nuspec' $VisibleVersion
    Update-NuGetVersion '..\Build\NuGet\FileHelpers.ExcelNPOIStorage.nuspec' $VisibleVersion
    Update-NuGetVersion '..\Build\NuGet\FileHelpers.nuspec' $VisibleVersion
}

function Update-AssemblyInfoFile ([string] $filename, [string] $assemblyVersionNumber, [string] $fileVersionNumber, [string] $informationalVersionNumber)
{
    $assemblyVersionPattern = 'AssemblyVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)'
    $fileVersionPattern = 'AssemblyFileVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)'
    $informationalVersionPattern = 'AssemblyInformationalVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)'
    $assemblyVersion = 'AssemblyVersion("' + $assemblyVersionNumber + '")';
    $fileVersion = 'AssemblyFileVersion("' + $fileVersionNumber + '")';
    $informationalVersion = 'AssemblyInformationalVersion("' + $informationalVersionNumber + '")';
    
        Write-Host $filename
        $filename + ' -> ' + $assemblyVersionNumber
    
        (Get-Content $filename) | ForEach-Object {
            % {$_ -replace $assemblyVersionPattern, $assemblyVersion } |
            % {$_ -replace $fileVersionPattern, $fileVersion } |
            % {$_ -replace $informationalVersionPattern, $informationalVersion }
        } | Set-Content $filename
}

function Update-NuGetVersion ([string] $filename, [string] $versionNumber)
{
    $versionPattern = '\<version\>[0-9]+(\.([0-9]+|\*)){1,3}\<\/version\>'
    $version = '<version>' + $versionNumber + '</version>';

    $dependenceVersionPattern = 'id="FileHelpers" version=\"[0-9]+(\.([0-9]+|\*)){1,3}\"'
    $dependenceVersion = 'id="FileHelpers" version="' + $versionNumber + '"';
    
    Write-Host $filename
    $filename + ' -> ' + $assemblyVersionNumber
    
        (Get-Content $filename) | ForEach-Object {
            % {$_ -replace $versionPattern, $version } |
            % {$_ -replace $dependenceVersionPattern, $dependenceVersion }
        } | Set-Content $filename
}



task common -depends version {
    "##teamcity[buildNumber '" + $VisibleVersion + "']"

     Delete-Make-Directory ..\$config
     Delete-Make-Directory "..\Output"
}


task compile -depends common {
    "Compiling " + $config
    
    Compile-Sln-With-Deploy "..\FileHelpers.OnlyLibs.sln" "2.0" "Lib\net20" ""
    Compile-Sln-With-Deploy "..\FileHelpers.OnlyLibs.sln" "3.5" "Lib\net35" ""
    Compile-Sln-With-Deploy "..\FileHelpers.OnlyLibs.sln" "4.5" "Lib\net45" "DOTNET_4"
    Compile-Sln-With-Deploy "..\FileHelpers.OnlyLibs.sln" "4.0" "Lib\net40" "DOTNET_4"

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

task pack -depends compile, docs {
    "Packing"

    copy "Home Page.url" ..\$config\

    $zipName = "Output\FileHelpers_" + $CurrentVersion + "_Build.zip"
    Create-Zip $config $zipName
}

task test -depends compile{
    "Testing"
    
    New-Item $build_dir\local\artifacts -Type directory -Force > $null
    
    cd $package_dir\xunit.runners*\tools\
    
    exec { & .\xunit.console.clr4 $base_dir\tests.xunit }
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
    exec { msbuild $path /p:TargetFrameworkVersion=v$targetFramework /t:rebuild /p:Configuration=$config  /nologo /verbosity:minimal }
}

function Compile-Sln-With-Deploy($path, $targetFramework, $deploy, $conditionals)
{
   Compile-Sln $path $targetFramework $conditionals
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
