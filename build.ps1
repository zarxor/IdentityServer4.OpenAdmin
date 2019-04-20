New-Item -ItemType Directory -Force -Path ./nuget
#dotnet tool install --tool-path tools SignClient

$cd = Get-Location
$projects = (
	'IdentityServer4.OpenAdmin.Core',
	'IdentityServer4.OpenAdmin.API',
	'IdentityServer4.OpenAdmin.UI',
	'IdentityServer4.OpenAdmin.AspNetIdentity',
	'IdentityServer4.OpenAdmin.EntityFramework',
	'IdentityServer4.OpenAdmin.InMemory'
)

ForEach ($project in $projects) 
{
	""
	"/---------------------------------------------------"
	"|   $project "
	"\---------------------------------------------------"
	""
	$path = ".\src\$project"
	set-location $path

	$ErrorActionPreference = "Stop";
	dotnet build -c Release -- $args
	#dotnet test -c Release --no-build -- $args
	dotnet pack -c Release -o './artifacts' --no-build

	#& ./build.ps1 $args
	Set-Location $cd

	if ($LASTEXITCODE -ne 0)
	{
		exit $LASTEXITCODE
	}

	Copy-Item -path $path\artifacts\*.nupkg -Destination .\nuget
}