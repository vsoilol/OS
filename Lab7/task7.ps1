
try {
    $eventLog = Get-EventLog -LogName System

    $outputFilePath = ".\eventlog.txt"

    if (Test-Path $outputFilePath) {
        Remove-Item $outputFilePath -Force
    }

    New-Item $outputFilePath -ItemType File

    Write-Host "A system event log exists" -ForegroundColor Green
    
    $events = $eventLog | Where-Object {$_.EntryType -eq "Warning" -or $_.EntryType -eq "Information"}
    
    foreach ($event in $events) {
        $textColor = switch ($event.EntryType) {
            "Information" { "Yellow" }
            "Warning" { "Black" }
        }
        
        Write-Host "$($event.EntryType): $($event.Message)" -ForegroundColor $textColor
        
        Add-Content -Path $outputFilePath -Value "$($event.EntryType): $($event.Message)"
    }
}
catch {
    Write-Host "The system event log does not exist" -ForegroundColor Red
}


# $outputFilePath = ".\eventlog.txt"

# if (Test-Path $outputFilePath) {
#     Remove-Item $outputFilePath -Force
# }

# New-Item $outputFilePath -ItemType File

# if ($eventLog) {
#     Write-Host "A system event log exists" -ForegroundColor Green
    
#     $events = $eventLog | Where-Object {$_.EntryType -eq "Warning" -or $_.EntryType -eq "Information"}
    
#     foreach ($event in $events) {
#         $textColor = switch ($event.EntryType) {
#             "Information" { "Yellow" }
#             "Warning" { "Black" }
#         }
        
#         Write-Host "$($event.EntryType): $($event.Message)" -ForegroundColor $textColor
        
#         Add-Content -Path $outputFilePath -Value "$($event.EntryType): $($event.Message)"
#     }
# } else {
#     Write-Host "The system event log does not exist" -ForegroundColor Red
# }
