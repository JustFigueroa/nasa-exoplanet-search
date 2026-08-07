--------------------- 
NASA EXOPLANET SEARCH
--------------------- 
Usage: 
	./nasasearch --select <Column...> [--sort <Column> <Ascending|Descending>] 
Options: 
	--select 
		Required. Specifies one or more columns to return. 
	--sort 
		Optional. Specifies the column to sort by and the sort direction. 
Available columns: 
	PlanetName
	HostStar 
	OrbitalPeriodDays 
	DistanceParsecs 
Sort directions: 
	Ascending 
	Descending 
Example: 
	./nasasearch --select PlanetName DistanceParsecs --sort DistanceParsecs Ascending
