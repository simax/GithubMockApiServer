open System
open Suave
open Suave.Operators
open Suave.Successful
open Suave.RequestErrors
open Suave.Filters
open System.IO

[<EntryPoint>]
let main argv = 
    let json fileName =
        let content = File.ReadAllText fileName  
        content.Replace("\r", "").Replace("\n","")
        |> OK >=> Writers.setMimeType "application/json"      
    
    let user = pathScan "/users/%s" (fun _ -> "User.json" |> json)  
    let repos = pathScan "/users/%s/repos" (fun _ -> "Repos.json" |> json)
    // let aList = ["One";"Two"]
    // let home = GET >=> path "/" >=> request (fun r -> (OK (sprintf "using: %s @ %s %s" r.host (string DateTime.Now) (aList.Head)))) 
    let home = GET >=> path "/" >=> request (fun r -> (OK (sprintf "using: %s @ %s" r.host (string DateTime.Now)))) 
    let mockApi = choose [repos;user; home; BAD_REQUEST "Oooops unknown route"]

    startWebServer defaultConfig mockApi          
    0 
