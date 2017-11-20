module Output
open System.IO

let toConsole lines =
    let cw line = 
        printfn "%s" line
    lines
    |> Seq.iter cw

let toFile (file:string) (lines:string seq) = 
    use outfile = new StreamWriter(file)
    let cw (line:string) = 
        outfile.WriteLine(line)

    lines
    |> Seq.iter cw