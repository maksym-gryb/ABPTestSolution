param ($version='latest')

$currentFolder = $PSScriptRoot
$slnFolder = Join-Path $currentFolder "../../"

$dbMigratorFolder = Join-Path $slnFolder "src/Company.TestSolution.DbMigrator"

Write-Host "********* BUILDING DbMigrator *********" -ForegroundColor Green
Set-Location $dbMigratorFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t company/testsolution-db-migrator:$version .


$blazorFolder = Join-Path $slnFolder "src/Company.TestSolution.Blazor"
$hostFolder = Join-Path $slnFolder "src/Company.TestSolution.HttpApi.Host"

Write-Host "********* BUILDING Blazor Application *********" -ForegroundColor Green
Set-Location $blazorFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t company/testsolution-blazor:$version .

$identityServerAppFolder = Join-Path $slnFolder "src/Company.TestSolution.IdentityServer"
Write-Host "********* BUILDING IdentityServer Application *********" -ForegroundColor Green
Set-Location $identityServerAppFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t company/testsolution-identityserver:$version .

Write-Host "********* BUILDING Api.Host Application *********" -ForegroundColor Green
Set-Location $hostFolder
dotnet publish -c Release




### ALL COMPLETED
Write-Host "COMPLETED" -ForegroundColor Green
Set-Location $currentFolder