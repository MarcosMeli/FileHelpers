function CopyInputToFile($file) {

    $text = gc $file.FullName
    $copyMode = $false;

    $outFile = $file.Directory.FullName + "/" + "Input.txt"

    Remove-Item $outFile -ErrorAction Ignore

    foreach($line in $text) {
      if ($copyMode) {
        if ($line.Contains("/File")) {
                    $copyMode = $false
        } else {
            $cleanLine = $line.Trim().Replace("/*", "").Replace("*/", "")

            if ($cleanLine.Length -gt 0) {
                $cleanLine >> $outFile
            }
        }
      }

      $lower = $line.ToLower()
      if ($lower.Contains("filein:input.txt") -or $lower.Contains("file:input.txt")) {
        $copyMode = $true
      }
    }
}

$allFiles = gci -Recurse -File -Filter "*.cs"
foreach($file in $allFiles) {
    CopyInputToFile $file
}
