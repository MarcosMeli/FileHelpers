call "%VS90COMNTOOLS%vsvars32.bat"
@msbuild FileHelpers.msbuild /t:release /nologo
