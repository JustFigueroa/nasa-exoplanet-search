module exoplanetProject.Main
open exoplanetProject.QuerySubmit
open System

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

