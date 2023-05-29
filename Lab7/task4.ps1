$processes = Get-Process | Sort-Object ProcessName | Select-Object Id, ProcessName, BasePriority

$currentDirectory = $PWD.Path
$xmlFilePath = $currentDirectory + "\processes.xml"

$xmlWriter = New-Object System.XMl.XmlTextWriter($xmlFilePath, $null)
$xmlWriter.Formatting = 'Indented'
$xmlWriter.WriteStartDocument()
$xmlWriter.WriteStartElement("Processes")

foreach ($process in $processes) {
    $xmlWriter.WriteStartElement("Process")
    $xmlWriter.WriteElementString("Id", $process.Id)
    $xmlWriter.WriteElementString("ProcessName", $process.ProcessName)
    $xmlWriter.WriteElementString("BasePriority", $process.BasePriority)
    $xmlWriter.WriteEndElement()
}

$xmlWriter.WriteEndElement()
$xmlWriter.WriteEndDocument()

$xmlWriter.Flush()
$xmlWriter.Close()

# Вывод процессов с BasePriority < 7
$processes | Where-Object { $_.BasePriority -lt 7 } | ForEach-Object {
    Write-Host "Id: $($_.Id) ProcessName: $($_.ProcessName) BasePriority: $($_.BasePriority)" -ForegroundColor Yellow
}