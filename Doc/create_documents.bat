rem @echo off

echo Create a working html website for validation

mkdir ..\Release\Docs

copy Include\* ..\Release\Docs
copy index.html ..\Release\Docs
copy MSDN.css ="..\..\Release\Docs
copy tree.js ..\Release\Docs
copy tree.css ..\Release\Docs
copy treenodedot.gif ..\Release\Docs

..\Libs\FileReplace.exe ..\Release\Docs\*.html "</body>" -f "add_ads.txt" -v
..\Libs\FileReplace.exe ..\Release\Docs\*.html "img src='sf.gif" "img src='http://sflogo.sourceforge.net/sflogo.php?group_id=152382&amp;type=1" -v
..\Libs\FileReplace.exe  ..\Release\Docs\*.html "{$HEADER$}" -f "add_header.txt" -v
..\Libs\FileReplace.exe  ..\Release\Docs\*.html "{$FOOTER$}" -f "add_footer.txt" -v 