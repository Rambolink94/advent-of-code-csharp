param (
    [string]$SourceDir,
    [string]$TargetDir
)

# Ensure the target directory exists
if (-Not (Test-Path -Path $TargetDir)) {
    New-Item -ItemType Directory -Path $TargetDir | Out-Null
}

# Get all directories in the source directory
Get-ChildItem -Path $SourceDir -Directory | ForEach-Object {
    $FolderName = $_.Name
    if ($FolderName -match '^\d{4}$') {
        $InputDir = Join-Path -Path $_.FullName -ChildPath "Input"
        if (Test-Path -Path $InputDir) {
            $DestinationPath = Join-Path -Path $TargetDir -ChildPath "$FolderName/Input"
            if (-Not (Test-Path -Path $DestinationPath))
            {
                echo "Copying: $InputDir => $DestinationPath"
                Copy-Item -Path $InputDir -Destination $DestinationPath -Recurse -Force
            }
        }
    }
}