call "%VS110COMNTOOLS%vsvars32.bat"
@msbuild FileHelpers.msbuild /nologo
@msbuild FileHelpers.shfbproj /t:Build /nologo
