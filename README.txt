---------------------
NASA EXOPLANET SEARCH
---------------------
*This program is used to search and return planetary data based on NASA's Planetary Systems Composite Parameter Table
*The users search criteria is passed as program arguments based on ADQL the following parameters:
-------
Options
-------
-s or --select 
-r or --range 
-w or --where 
-o or --order 
-----------------
Example execution
-----------------
Program -range 0-5 -s pl_name -w sy_dist < 200000 -o sy_dist 
-----------------
*In this example, the user requests the first five planets whose systems are within 200,000 parsecs of Earth. The results display each planet's name and are sorted by distance from Earth
