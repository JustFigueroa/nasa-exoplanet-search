module ExoplanetArgumentHandler
open exoplanetQuery
open System

let query = 
    {
        Select = 
            [
                PlanetName
                HostStar
                OrbitalPeriodDays
                DistanceParsecs
            ]
        SortyBy = None
    }