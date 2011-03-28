@cd ..
@call "%VS100COMNTOOLS%vsvars32.bat"
@msbuild FileHelpers.msbuild /t:pack-all /tv:4.0 /nologo
