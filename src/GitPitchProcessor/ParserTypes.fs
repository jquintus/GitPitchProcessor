﻿module ParserTypes

type FilePath = string
type CodeInclude = { 
    file: FilePath
    lang: string Option
    title: string Option
}
type CodeReference = { 
    title: string Option
    startLine: int
    endLine: int Option 
}

type PitchLine = 
    | Content of string
    | Include of FilePath
    | CodeInclude of CodeInclude
    | CodeReference of CodeReference

let codeReference startLine endLine title =
    CodeReference { startLine = startLine; endLine = endLine; title = title }
    
let ci file lang title = 
        CodeInclude { 
            file = file
            lang = lang
            title = title
        }