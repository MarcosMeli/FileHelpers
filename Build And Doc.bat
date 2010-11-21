@call "%ProgramFiles%\Microsoft Visual Studio 9.0\VC\vcvarsall.bat" x86
@msbuild FileHelpers.msbuild /t:doc /nologo
