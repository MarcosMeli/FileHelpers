call "%VS100COMNTOOLS%vsvars32.bat"
@msbuild FileHelpers.msbuild /t:release /tv:4.0 /nologo
