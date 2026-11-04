$ErrorActionPreference = "Stop"

dotnet publish "$PSScriptRoot\GoodDogWordle.csproj" `
    --configuration Release `
    --runtime win-x64 `
    --self-contained true `
    -p:PublishSingleFile=true `
    -p:IncludeNativeLibrariesForSelfExtract=true `
    -p:EnableCompressionInSingleFile=true `
    --output "$PSScriptRoot\publish"

if ($LASTEXITCODE -ne 0) {
    throw "Publishing Good Dog Wordle failed with exit code $LASTEXITCODE."
}

Write-Host "Published executable: $PSScriptRoot\publish\GoodDogWordle.exe"
