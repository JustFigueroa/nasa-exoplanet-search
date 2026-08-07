module ExoplanetArgumentHandler
open ExoplanetQuery
open System

// This function takes a string array (the passed arguments) and return a Result Ok or Error
// If Ok returns query if Error returns String

let parseArguments (args: string array) : Result<Query, string> =

    printfn "Arguments received: %d" args.Length

    args
    |> Array.iteri (fun index argument ->
        printfn "Argument %d: %s" index argument)

    Error "Argument parsing is not implemented yet."By }