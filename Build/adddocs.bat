for /f %%f in ('dir /b ..\Help\html\*.htm') do ..\Libs\FileReplace.exe ..\Help\html\%%f "</div></div></div><div" -f adddocs.txt
