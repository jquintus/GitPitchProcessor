// Learn more about F# at http://fsharp.org

open System
open Arguments

[<EntryPoint>]
let main argv =
    let args = parseArgs argv
    printfn "%A"args
    0