Add-Type -AssemblyName System.Device #Required to access System.Device.Location namespace
$GeoWatcher = New-Object System.Device.Location.GeoCoordinateWatcher #Create the required object
$GeoWatcher.Start() #Begin resolving current locaton

while (($GeoWatcher.Status -ne 'Ready') -and ($GeoWatcher.Permission -ne 'Denied')) {
    Start-Sleep -Milliseconds 100 #Wait for discovery.
}  

if ($GeoWatcher.Permission -eq 'Denied'){
    Write-Error 'Access Denied for Location Information'
} else {
    $GeoWatcher.Position.Location | Select Latitude,Longitude #Select the relevent results.
}

$ComputerLocation = [PSCustomObject]@{
    ComputerName=$env:COMPUTERNAME
    UserName=$env:USERNAME
    Latitude=$GeoWatcher.Position.Location.Latitude
    Longitude=$GeoWatcher.Position.Location.Longitude
    SavedTime= (Get-Date -Format "yyyy-MM-ddTHH:mm:ss.fff")
}

$body = ConvertTo-Json $ComputerLocation


Invoke-RestMethod -Method Post -Uri "https://localhost:7275/api/ComputerLocation" -Body $body -ContentType "application/json"
