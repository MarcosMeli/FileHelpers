@cd ..
@call "%ProgramFiles%\Microsoft Visual Studio 10.0\VC\vcvarsall.bat" x86
@msbuild FileHelpers.TeamCity.msbuild /t:doc /nologo
