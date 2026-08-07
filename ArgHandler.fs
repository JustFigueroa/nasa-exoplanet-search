module ExoplanetArgumentHandler
open ExoplanetQuery
open System

let parseArguments (args: string array) : Result<Query, string> =

    let mutable columns = []
    let mutable sortWords = []
    let mutable error = ""
    let mutable mode = ""

    // sort every word into the right bucket
    for w in args do
        if w = "--select" then mode <- "select"
        elif w = "--sort" then mode <- "sort"
        elif mode = "select" then
            match w with
            | "PlanetName" -> columns <- columns @ [PlanetName]
            | "HostStar" -> columns <- columns @ [HostStar]
            | "OrbitalPeriodDays" -> columns <- columns @ [OrbitalPeriodDays]
            | "DistanceParsecs" -> columns <- columns @ [DistanceParsecs]
            | _ -> error <- sprintf "Unknown column: %s" w
        elif mode = "sort" then
            sortWords <- sortWords @ [w]

    // figure out the sort part
    let mutable sortBy = None
    if sortWords.Length = 2 then
        let col =
            match sortWords.[0] with
            | "PlanetName" -> Some PlanetName
            | "HostStar" -> Some HostStar
            | "OrbitalPeriodDays" -> Some OrbitalPeriodDays
            | "DistanceParsecs" -> Some DistanceParsecs
            | _ -> None
        let dir =
            match sortWords.[1] with
            | "Ascending" -> Some Ascending
            | "Descending" -> Some Descending
            | _ -> None
        match col, dir with
        | Some c, Some d -> sortBy <- Some (c, d)
        | _ -> error <- "Bad --sort values"
    elif sortWords.Length > 0 then
        error <- "--sort needs a column and a direction"

    // one decision at the end
    if error <> "" then Error error
    elif columns.IsEmpty then Error "Usage: --select <Column...> [--sort <Column> <Ascending|Descending>]"
    else Ok { Select = columns; SortBy = sortBy }