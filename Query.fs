module exoplanetQuery

open System

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
type rowRange 
    {
        first: float
        last: float
    }

//This is the user query
//Distance SortBy and Limit are optional
type Query = 
    {
        Select: Column list
        SortBy: (Column * SortDirection) option
        Rows: rowRange option
        Limit: int option
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
    |> List.map columnName
    |> String.concat ", "
    |> sprintf "SELECT %s"

let private SortBy sortCriteria = 
    match sortCriteria with  