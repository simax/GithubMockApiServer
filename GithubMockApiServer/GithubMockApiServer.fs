open System
open Suave
open Suave.Web
open Suave.Operators
open Suave.Successful
open Suave.RequestErrors
open Suave.Filters
open System.IO


[<EntryPoint>]
let main [| port |] = 
    let json fileName =
        let content = File.ReadAllText fileName  
        content.Replace("\r", "").Replace("\n","")
        |> OK >=> Writers.setMimeType "application/json"      
    
    let user = pathScan "/users/%s" (fun _ -> "User.json" |> json)  
    let repos = pathScan "/users/%s/repos" (fun _ -> "Repos.json" |> json)
    // let aList = ["One";"Two"]
    // let home = GET >=> path "/" >=> request (fun r -> (OK (sprintf "using: %s @ %s %s" r.host (string DateTime.Now) (aList.Head)))) 
    // let home = GET >=> path "/" >=> request (fun r -> (OK (sprintf "using: %s @ %s" r.host (string DateTime.Now)))) 
    let home = GET >=> path "/" >=> request (fun r -> (OK "Hello AZURE !!!!!!!")) 
    let mockApi = choose [repos;user; home; BAD_REQUEST "Oooops unknown route"]

    let config =
            { defaultConfig with
                bindings = [ HttpBinding.mk HTTP Net.IPAddress.Loopback (uint16 port) ]
                listenTimeout = TimeSpan.FromMilliseconds 3000.
            }

    startWebServer config mockApi          
    0 
