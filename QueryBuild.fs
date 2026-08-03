module exoplanetProject.QueryBuild
open System
open ArgHandler

type SortMode =
    | Distance
    | OrbitalPeriod
    | PlanetRadius
    | DiscoveryYear

type SearchCriteria =
    {
        MaximumDistanceParsecs: float option
        MinimumDiscoveryYear: int option
        MinimumRadiusEarths: float option
        MaximumRadiusEarths: float option
        MaximumOrbitalPeriodDays: float option
        DiscoveryMethod: string option

        SortBy: SortMode
    }   

