./Build/.nuget/NuGet.exe install Build/.nuget/packages.config -OutputDirectory packages
./Build/.nuget/NuGet.exe restore ./FileHelpers.sln

cd Build/
./build.ps1
cd ..