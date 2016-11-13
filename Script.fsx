// Scripts


let warbler f a = f a a  

let x = warbler (fun z -> printfn "%A %A" z) 

let aaa (k: int) = printfn "%s %A" "hello"

aaa 3

x "ddd" 

