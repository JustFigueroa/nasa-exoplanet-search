module ExoplanetMain
open ExoplanetQuery
open ExoplanetPlanets
open ExoplanetArgumentHandler
open System

[<EntryPoint>]
    
let main args =
    try
            match parseArguments args with
            | Ok query ->
                try
                    let planets =
                        fetchPlanets query
                        |> fun request ->
                            request.GetAwaiter().GetResult()

                    printfn "Exoplanet systems returned by NASA:\n"

                    for planet in planets do
                        printfn "%s" planet.Name
                        if List.contains HostStar query.Select then
                            printfn "  Host star: %s" planet.HostStar
                        if List.contains OrbitalPeriodDays query.Select then
                            match planet.OrbitalPeriodDays with
                            | Some days -> printfn "  Orbital period: %.2f days" days
                            | None -> printfn " Orbital Period: Unknown"
                        if List.contains DistanceParsecs query.Select then
                            match planet.DistanceParsecs with
                                | Some distance -> 
                                    let distanceLightYears =
                                        distance * 3.26156
                                    printfn "  Distance: %.2f parsecs" distance
                                    printfn "            %.2f light-years" distanceLightYears
                                | None -> printfn "  Distance: Unknown"
                        printfn "\n"
                    0
                with
                | ex ->
                    eprintfn "Error: %s" ex.Message
                    1 
            | Error message ->
                eprintfn "Argument error: %s" message
                1
    with
    | ex -> 
        eprintfn "Error: %s" ex.Message
        1