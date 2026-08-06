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
            | Error message ->
                eprintfn "Argument error: %s" message
                1
    with
    | ex -> 
        eprintfn "Error: %s" ex.Message
        1