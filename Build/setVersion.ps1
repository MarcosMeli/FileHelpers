$AssemblyVersion = "3.4.0"
$SemanticVersion = $AssemblyVersion + ""   #for stable version, set postfix to empty

function Update-NuGetVersion ([string] $filename, [string] $versionNumber){
    $content = Get-Content $filename
    $xmlDoc = [xml]$content

    # set own version
    $xmlDoc.package.metadata.version = $versionNumber

    # set reference to FileHelpers
    $fhRef = $xmlDoc.package.metadata.dependencies.dependency|where {$_.id -eq "FileHelpers"}
    if($fhRef){
        $fhRef.version = $versionNumber
    }

    $path = Resolve-Path $filename
    $xmlDoc.Save($path.Path)
}

function Update-StandardProject([string] $filename, [string] $versionNumber){
    $content = Get-Content $filename
    $xmlDoc = [xml]$content

    # set own version
    $xmlDoc.Project.PropertyGroup[0].Version = $versionNumber

    $path = Resolve-Path $filename
    $xmlDoc.Save($path.Path)
}

function Update-AssemblyInfoFile ([string] $filename, [string] $assemblyVersionNumber, [string] $informationalVersionNumber){
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

function Update-AppVeyorBuildFile([string]$filename, [string] $assemblyVersionNumber) {
    # open file
    $buildVersionPattern = 'version: [0-9]+\.[0-9]+\.[0-9]+\.\{build\}'

    (Get-Content $filename) | ForEach-Object {
        % {$_ -replace $buildVersionPattern, "version: $assemblyVersionNumber.{build}"}
    } | Set-Content $filename
}

Update-AppVeyorBuildFile '..\appveyor.yml' $AssemblyVersion
Update-AssemblyInfoFile '..\FileHelpers\VersionInfo.cs' $AssemblyVersion $SemanticVersion
Update-StandardProject '..\FileHelpers\FileHelpers.NetStandard.csproj' $SemanticVersion
Update-NuGetVersion '.\NuGet\FileHelpers.ExcelNPOIStorage.nuspec' $SemanticVersion
Update-NuGetVersion '.\NuGet\FileHelpers.nuspec' $SemanticVersion