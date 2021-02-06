$config = "Release"

function Create-Folders() {
     Recreate-Directory "..\Output"
}

function Compile-Solutions() {
    "Compiling " + $config

    & 'dotnet.exe' build "..\FileHelpers.sln" -c $config "/p:Version=$env:GitVersion_SemVer"
    Ensure-CleanExit
}

function Pack-Packages () {
    "Creating NuGet packages"

    & 'dotnet.exe' pack "..\FileHelpers.sln" -c $config --output "../Output" "/p:Version=$env:GitVersion_SemVer"
    Ensure-CleanExit
}

function Test-Projects() {
    "Testing"

    & 'dotnet.exe' test "..\FileHelpers.sln" -c $config --logger:Appveyor
    Ensure-CleanExit
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

function Ensure-CleanExit()
{
    if($LASTEXITCODE -ge 1)
    {
        throw
    }
}

Create-Folders
Compile-Solutions
Test-Projects
Pack-Packages