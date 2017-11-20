module Arguments
open Argu
open System

type private Arguments = 
    | InputFile of path:string
    | OutputFile of path:string
    | ToStdOut of bool
with
    interface IArgParserTemplate with
        member s.Usage = 
            match s with 
            | InputFile _  -> "Specify the GITPITCH.md file to process"
            | OutputFile _ -> "Specify the output file"
            | ToStdOut _   -> "If set, ignore the output file and just dump to the console" 

type ParsedArguments = {
        inputFile:  string
        outputFile: string
        toStdOut:   bool
    }

let parseArgs argv = 
    let errorHandler = ProcessExiter(colorizer = function ErrorCode.HelpText -> None | _ -> Some ConsoleColor.Red)

    let argParser = ArgumentParser.Create<Arguments>(programName = "GitPitchProcessor", errorHandler = errorHandler)
    let results = argParser.Parse argv
    let input = results.GetResult  (<@ InputFile @>, defaultValue = "GITPITCH.md")
    let output = results.GetResult (<@ OutputFile@>, defaultValue = @"assets/md/GITPITCH.md")
    let toStdOut = results.GetResult (<@ ToStdOut @>, defaultValue = false)

    {inputFile = input
     outputFile = output
     toStdOut = toStdOut
     }