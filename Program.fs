open System
open System.Net.Http
open System.Text.Json
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

Type SearchCriteria =
    {
        MaximumDistanceParsecs: float option
        MinimumDiscoveryYear: int option
        MinimumRadiusEarths: float option
        MaximumRadiusEarths: float option
        MaximumOrbitalPeriodDays: float option
        DiscoveryMethod: string option
        SortBy: SortMode
    }   

let query =
    "SELECT TOP 5 pl_name, hostname, pl_orbper, sy_dist " +
    "FROM pscomppars " +
    "WHERE pl_orbper IS NOT NULL AND sy_dist IS NOT NULL " +
    "ORDER BY sy_dist"

let endpoint =
    "https://exoplanetarchive.ipac.caltech.edu/TAP/sync"

let buildRequestUrl () =
    let encodedQuery = Uri.EscapeDataString(query)

    $"{endpoint}?query={encodedQuery}&format=json"

let fetchPlanets () =
    task {
        use client = new HttpClient()

        let requestUrl = buildRequestUrl ()

        let! response = client.GetAsync(requestUrl)
        let! json = response.Content.ReadAsStringAsync()

        if not response.IsSuccessStatusCode then
            failwithf
                "NASA request failed with status %O.\n%s"
                response.StatusCode
                json

        let planets =
            JsonSerializer.Deserialize<Planet array>(json)

        if isNull planets then
            return [||]
        else
            return planets
    }

let 


[<EntryPoint>]
let main _ =
    try
        let planets =
            fetchPlanets().GetAwaiter().GetResult()

        printfn "Nearest exoplanet systems returned by NASA:\n"

        for planet in planets do
            let distanceLightYears =
                planet.DistanceParsecs * 3.26156

            printfn "%s" planet.Name
            printfn "  Host star: %s" planet.HostStar
            printfn "  Orbital period: %.2f days" planet.OrbitalPeriodDays
            printfn "  Distance: %.2f parsecs" planet.DistanceParsecs
            printfn "  Distance: %.2f light-years\n" distanceLightYears

        0
    with
    | ex ->
        eprintfn "Error: %s" ex.Message
        1 

