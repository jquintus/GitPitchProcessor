module Input

open System.IO

type T = T of string seq

let fromList inputList =
    T (List.toSeq inputList)

let fromFile (file:string) = 
    let readLines = seq {
        use sr = new StreamReader (file)
        while not sr.EndOfStream do 
            yield sr.ReadLine()
    }
    T readLines