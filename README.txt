---------------------
NASA EXOPLANET SEARCH
---------------------
> This program is used to search and return planetary data based on NASA's Planetary Systems Composite Parameter Table
> The users search criteria is passed as program arguments based on ADQL the following parameters:
-------
Options
-------
--select
[--sort <Column to sort by> <Ascending or Descending>]
[--range <First Row> <Last Row>]
-----------------
Example execution
-----------------
./nasasearch --select PlanetName DistanceParsecs --sort DistanceParsecs Ascending --range 0 19
----------------
> In this example the program will return the name and distance of 20 planets  sorted in ascending order by distance: The nearest 20 exoplanets to earth
