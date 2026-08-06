module ExoplanetQuery
open ExoplanetPlanets
open System
open System.Net.Http
open System.Text.Json
open System.Text.Json.Serialization

//Acceptable SELECT options
//Demonstrates F# discriminated unions
type Column =
    | PlanetName
    | HostStar
    | OrbitalPeriodDays
    | DistanceParsecs

//Sort Order
type SortDirection =
    | Ascending
    | Descending

//Groups the range values into one value
//Used to get specific range of rows
//Demonstates F# records
type rowRange =
    {
        first: int
        last: int
    }

//This is the user query
//Distance SortBy and Limit are optional
type Query = 
    {
        Select: Column list
        SortBy: (Column * SortDirection) option
     //   Rows: rowRange option
    }

//This will convert the select options into string compatable with NASA exoplanet columns
let private columnName column = 
    match column with
    | PlanetName -> "pl_name"
    | HostStar -> "hostname"
    | OrbitalPeriodDays -> "pl_orbper"
    | DistanceParsecs -> "sy_dist"

//This will create the SELECT clause
//Takes the list of desired columns and concatenates or * if none

let private buildSelect columns =
    match columns with 
    | [] ->
        "SELECT *"
    | selectedColumns ->
        selectedColumns
        |> List.map columnName
        |> String.concat ", "
        |> sprintf "SELECT %s"

//This will create the Order By section of ADQL request
let private buildSortBy sortCriteria = 
    match sortCriteria with  
    | None ->
        None
    | Some (column, direction) ->
        let directionName = 
            match direction with 
            | Ascending -> "ASC"
            | Descending -> "DESC"
            
        Some $"ORDER BY {columnName column} {directionName}"


//Building the actual request
let buildQuery query = 
    [
        Some (buildSelect query.Select)
        Some "FROM pscomppars"
        buildSortBy query.SortBy
    ]
    |> List.choose id
    |> String.concat "\n"

//NASA TAP endpoint
let endpoint =
    "https://exoplanetarchive.ipac.caltech.edu/TAP/sync"

//Builds the request url
let buildRequestUrl queryText =
    let encodedQuery = Uri.EscapeDataString(queryText)

    $"{endpoint}?query={encodedQuery}&format=json"

//Gets the data and returns planets
let fetchPlanets query =
    task {
        use client = new HttpClient()

        let queryText = buildQuery query
        let requestUrl = buildRequestUrl queryText

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

    