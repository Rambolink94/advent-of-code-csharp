param (
    [string]$TargetDir
)

$SourceDir = "../Input"
$TargetDir = Join-Path -Path $TargetDir -ChildPath "Input"

# Get all directories in the source directory
Get-ChildItem -Path $SourceDir -Directory | ForEach-Object {
    $FolderName = $_.Name
    if ($FolderName -match '^\d{4}$') {
        $InputDir = $_.FullName
        if (Test-Path -Path $InputDir) {
            $DestinationPath = Join-Path -Path $TargetDir -ChildPath "/$FolderName"
            if (-Not (Test-Path -Path $DestinationPath)) {
                New-Item -ItemType Directory -Path $DestinationPath | Out-Null
            }
            Write-Output "Copying: $InputDir => $DestinationPath"
            Copy-Item -Path $InputDir\* -Destination $DestinationPath -Recurse -Force
        }
    }
}