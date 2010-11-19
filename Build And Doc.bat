call "%VS100COMNTOOLS%vsvars32.bat"
@msbuild FileHelpers.msbuild /t:doc /tv:4.0 /nologo
