namespace ExoplanetPlanets
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
