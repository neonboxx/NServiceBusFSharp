module program
open Handler
open System
open NServiceBus
open ServiceShared

[<EntryPoint>]
let main argv = 
    printfn "Hello World - Waiting for jokes" 
    let busConfiguration = 
        new BusConfiguration()
            
    busConfiguration.EndpointName("ConsoleService.FS")
    busConfiguration.UseSerialization<JsonSerializer>() |> ignore
    busConfiguration.EnableInstallers() 
    busConfiguration.UsePersistence<InMemoryPersistence>() |> ignore

    use bus = Bus.Create(busConfiguration).Start()         
    Console.ReadKey() |> ignore
    0 

