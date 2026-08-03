namespace exoplanetProject
open System
open System.Text.Json.Serialization
type Planet =
    {
        [<JsonPropertyName("pl_name")>]
        Name: string

        [<JsonPropertyName("hostname")>]
        HostStar: string

        [<JsonPropertyName("pl_orbper")>]
        OrbitalPeriodDays: float

        [<JsonPropertyName("sy_dist")>]
        DistanceParsecs: float
    }

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