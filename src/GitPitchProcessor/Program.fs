open Arguments

[<EntryPoint>]
let main argv =
    let run input output fileReader = 
        let processLinesFromFile pitchLines = 
            Parser.processLines fileReader pitchLines

        Parser.parseLines input
        |> processLinesFromFile
        |> Seq.map Parser.toString
        |> output

    let args = parseArgs argv

    let input = Input.fromFile args.inputFile
    let rootPath = Input.getRootPath args.inputFile
    let fileReader = Input.fromFileWithRootPath rootPath

    let output =
        if args.toStdOut then
            Output.toConsole
        else  
            Output.toFile args.outputFile

    run input output fileReader
    0