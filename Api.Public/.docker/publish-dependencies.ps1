param(
    [string]$Configuration = "Release"
)

$ErrorActionPreference = "Stop"

$dependencies = @(
    "..\..\Svc.Accounts\Svc.Accounts\Svc.Accounts.csproj",
    "..\..\Svc.Locations\Svc.Locations\Svc.Locations.csproj",
    "..\..\Svc.Emailing\Svc.Emailing\Svc.Emailing.csproj",
    "..\..\Svc.Places\Svc.Places\Svc.Places.csproj"
)

foreach ($project in $dependencies) {
    $projectPath = Join-Path $PSScriptRoot $project
    $outputPath = Join-Path (Split-Path $projectPath -Parent) "bin\publish"

    Write-Host "Publishing $projectPath -> $outputPath"
    dotnet publish $projectPath -c $Configuration -o $outputPath --nologo
}

$stampDir = Join-Path $PSScriptRoot "bin"
New-Item -ItemType Directory -Force -Path $stampDir | Out-Null
Set-Content -Path (Join-Path $stampDir "publish-dependencies.stamp") -Value (Get-Date -Format "o")
