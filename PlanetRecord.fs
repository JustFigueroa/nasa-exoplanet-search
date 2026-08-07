namespace ExoplanetPlanets
open System.Text.Json.Serialization
open System

type Planet =
    {
        [<JsonPropertyName("pl_name")>]
        Name: string

        [<JsonPropertyName("hostname")>]
        HostStar: string

        [<JsonPropertyName("pl_orbper")>]
        OrbitalPeriodDays: float option

        [<JsonPropertyName("sy_dist")>]
        DistanceParsecs: float option
    }
