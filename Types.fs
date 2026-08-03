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

type Host =
    {
        [<JsonPropertyName("hostname")>]
        Name: string

        [<JsonPropertyName("sy_name")>]
        SystemName: string

        [<JsonPropertyName("sy_pnum")>]
        NumberOfPlanets: int

        [<JsonPropertyName("sy_mnum")>]
        NumberOfMoons: int

    }
