call "%VS90COMNTOOLS%vsvars32.bat"
@msbuild FileHelpers.shfbproj /t:Build /nologo
