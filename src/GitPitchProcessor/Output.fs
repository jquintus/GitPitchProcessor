module Output

let toConsole lines =
    let cw line = 
        printfn "%s" line
    lines
    |> Seq.iter cw