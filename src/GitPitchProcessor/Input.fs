module Input

open System.IO
open ParserTypes

type T = T of string seq

// Creation from memory
let fromList inputList =
    T (List.toSeq inputList)

let fromFile (file:FilePath) = 
    let readLines = seq {
        use sr = new StreamReader (file)
        while not sr.EndOfStream do 
            yield sr.ReadLine()
    }
    T readLines

let fromLine line = fromList [ line ]

let fromLines2 line1 line2 = 
    fromList [ line1; line2 ]

let combine t1 t2 =
    let (T lines1) = t1
    let (T lines2) = t2
    lines2 |> Seq.append lines1
           |> Seq.toList
           |> fromList

let combine3 t1 t2 t3 =
    let t1' = combine t1 t2
    combine t1' t3

// Creation from file
let fromFileWithRootPath rootPath relativeFilePath =
    if Path.IsPathRooted relativeFilePath then
        fromFile relativeFilePath
    else
        let absolutePath = Path.Combine (rootPath, relativeFilePath)
        fromFile absolutePath

let getRootPath path =
    let info = FileInfo(path)
    info.DirectoryName

// Conversion
let asString t =
    let (T strings) = t
    strings |> joinStrings System.Environment.NewLine

let asContent t =
    let (T strings) = t
    strings |> Seq.map Content