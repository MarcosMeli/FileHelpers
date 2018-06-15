$AssemblyVersion = "3.2.8"
$SemanticVersion = $AssemblyVersion + "-alpha"   #for stable version, set postfix to empty

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

Update-AssemblyInfoFile '..\FileHelpers\VersionInfo.cs' $AssemblyVersion $SemanticVersion
Update-NuGetVersion '.\NuGet\FileHelpers.ExcelStorage.nuspec' $SemanticVersion
Update-NuGetVersion '.\NuGet\FileHelpers.ExcelNPOIStorage.nuspec' $SemanticVersion
Update-NuGetVersion '.\NuGet\FileHelpers.nuspec' $SemanticVersion