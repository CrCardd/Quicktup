# RIGHT CLICKE WINDOWS 11
reg.exe add "HKCU\Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\InprocServer32" /f /ve
$process = Get-Process -Name "explorer"
Stop-Process -Id $process.Id
Start-Process -FilePath $process.Path