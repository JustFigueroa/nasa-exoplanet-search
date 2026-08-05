module exoplanetProject.QuerySubmit
open System
open System.Net.Http
open System.Text.Json
open System.Text.Json.Serialization


let defaultQuery =
    "SELECT TOP 5 pl_name, hostname, pl_orbper, sy_dist " +
    "FROM pscomppars " +
    "WHERE pl_orbper IS NOT NULL AND sy_dist IS NOT NULL " +
    "ORDER BY sy_dist"



let endpoint =
    "https://exoplanetarchive.ipac.caltech.edu/TAP/sync"

let buildRequestUrl () =
    let encodedQuery = Uri.EscapeDataString(defaultQuery)

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