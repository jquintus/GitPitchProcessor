module Input

open System.IO
open ParserTypes

type T = T of string seq

let fromList inputList =
    T (List.toSeq inputList)

let fromFile (file:FilePath) = 
    let readLines = seq {
        use sr = new StreamReader (file)
        while not sr.EndOfStream do 
            yield sr.ReadLine()
    }
    T readLines

let fromFileWithRootPath rootPath relativeFilePath =
    if Path.IsPathRooted relativeFilePath then
        fromFile relativeFilePath
    else
        let absolutePath = Path.Combine (rootPath, relativeFilePath)
        fromFile absolutePath

let getRootPath path =
    let info = FileInfo(path)
    info.DirectoryName