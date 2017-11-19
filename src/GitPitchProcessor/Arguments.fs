module Arguments
open Argu
open System

type private Arguments = 
    | InputFile of path:string
    | OutputFile of path:string
with
    interface IArgParserTemplate with
        member s.Usage = 
            match s with 
            | InputFile _ -> "Specify the GITPITCH.md file to process"
            | OutputFile _ -> "Specify the output file"

type ParsedArguments = {
        inputFile: string
        outputFile: string
    }

let parseArgs argv = 
    let errorHandler = ProcessExiter(colorizer = function ErrorCode.HelpText -> None | _ -> Some ConsoleColor.Red)

    let argParser = ArgumentParser.Create<Arguments>(programName = "GitPitchProcessor", errorHandler = errorHandler)
    let results = argParser.Parse argv
    let input = results.GetResult  (<@ InputFile @>, defaultValue = "GITPITCH.md")
    let output = results.GetResult (<@ OutputFile@>, defaultValue = @"assets/md/GITPITCH.md")
    
    {inputFile = input; outputFile = output}