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
                        let distanceLightYears =
                            match planet.DistanceParsecs with
                            | Some distance -> distance * 3.26156
                            | None -> 0.0

                        
                        printfn "%s" planet.Name
                        printfn "  Host star: %s" planet.HostStar
                        match planet.OrbitalPeriodDays with
                        | Some days -> printfn "  Orbital period: %.2f days" days
                        | None -> printfn " Orbital Period: Unknown"
                        match planet.DistanceParsecs with
                        | Some distance -> 
                            printfn "  Distance: %.2f parsecs" distance
                            printfn "  Distance: %.2f light-years\n" distanceLightYears
                        | None -> printfn "  Distance: Unknown"
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