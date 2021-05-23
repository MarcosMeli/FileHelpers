$AssemblyVersion = "3.5.0"
$SemanticVersion = $AssemblyVersion + ""   #for stable version, set postfix to empty

function Update-NuGetVersion ([string] $filename, [string] $versionNumber){
    $content = Get-Content $filename
    $xmlDoc = [xml]$content

    $xmlDoc.Project.PropertyGroup.Version = $versionNumber

    $path = Resolve-Path $filename
    $xmlDoc.Save($path.Path)
}

function Update-AppVeyorBuildFile([string]$filename, [string] $assemblyVersionNumber) {
    # open file
    $buildVersionPattern = 'version: [0-9]+\.[0-9]+\.[0-9]+\.\{build\}'

    (Get-Content $filename) | ForEach-Object {
        % {$_ -replace $buildVersionPattern, "version: $assemblyVersionNumber.{build}"}
    } | Set-Content $filename
}

Update-AppVeyorBuildFile '..\appveyor.yml' $AssemblyVersion
Update-NuGetVersion '..\FileHelpers.ExcelNPOIStorage\FileHelpers.ExcelNPOIStorage.csproj' $SemanticVersion
Update-NuGetVersion '..\FileHelpers\FileHelpers.csproj' $SemanticVersion