let someInt = Some 23
let noneInt = None

let printOption = function
    | Some x -> printfn "%A" x
    | None   -> printfn "Nothing"

printOption someInt
printOption noneInt
Some "Hello world" |> printOption

noneInt.Value * 2 |> printfn "%i"

noneInt 
|> Option.map (fun x -> x * 2)
|> printfn "%A"