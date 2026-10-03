param(
    [string]$Configuration = "Release",
    [Parameter(Mandatory=$true)][string]$Project,
    [Parameter(Mandatory=$true)][string]$StampName
)

$ErrorActionPreference = "Stop"

$projectPath = Join-Path $PSScriptRoot $Project
$outputPath = Join-Path (Split-Path $projectPath -Parent) "bin\publish"

Write-Host "Publishing $projectPath -> $outputPath"
dotnet publish $projectPath -c $Configuration -o $outputPath --nologo

$stampDir = Join-Path $PSScriptRoot "bin"
New-Item -ItemType Directory -Force -Path $stampDir | Out-Null
Set-Content -Path (Join-Path $stampDir $StampName) -Value (Get-Date -Format "o")
