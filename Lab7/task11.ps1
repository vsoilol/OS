$sourceFolder = ".\task11"

$fileExtensions = Get-ChildItem $sourceFolder | Where-Object { $_.PSIsContainer -eq $false } | Foreach-Object { $_.Extension } | Sort-Object -Unique

foreach ($ext in $fileExtensions) {
    $folderName = $ext.Replace(".", "")
    New-Item -Path $sourceFolder -Name $folderName -ItemType Directory -Force | Out-Null
}

Get-ChildItem $sourceFolder | Where-Object { $_.PSIsContainer -eq $false } | Foreach-Object {
    $extension = $_.Extension.Replace(".", "")
    $destinationFolder = Join-Path -Path $sourceFolder -ChildPath $extension
    Move-Item $_.FullName -Destination $destinationFolder
}
