module program
open Handler
open System
open NServiceBus
open ServiceShared

[<EntryPoint>]
let main argv = 
    printfn "Hello World - Waiting for orders" 
    let busConfiguration = 
        new BusConfiguration()
            
    busConfiguration.EndpointName("ConsoleService.FS")
    busConfiguration.UseSerialization<JsonSerializer>() |> ignore
    busConfiguration.EnableInstallers() |> ignore
    busConfiguration.UsePersistence<InMemoryPersistence>() |> ignore
            
    
    //using(Bus.Create(busConfiguration).Start())
    //     (fun sub -> sub.Subscribe<PlaceOrder>())

    use bus = Bus.Create(busConfiguration).Start()
    
    Console.WriteLine("Subscribed") |> ignore

         
    Console.ReadKey() |> ignore
    
    0 

