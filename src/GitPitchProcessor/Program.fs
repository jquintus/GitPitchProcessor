open Arguments

[<EntryPoint>]
let main argv =
    let args = parseArgs argv
    let input = Input.fromFile args.inputFile
    
    Parser.parseLines input
    |> Seq.map Parser.toString
    |> Output.toConsole

    0