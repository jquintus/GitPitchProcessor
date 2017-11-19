module Parser

type FilePath = string
type CodeInclude = { file: FilePath; lang: string; title: string }
type CodeReference = { title: string; startLine: int; endLine: int Option }

type Document = 
    | Content of string
    | Include of FilePath
    | CodeInclude of CodeInclude
    | CodeReference of CodeReference
    
let parse (line:string) = 
    Content line