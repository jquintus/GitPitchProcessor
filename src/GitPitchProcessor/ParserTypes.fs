module ParserTypes

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

type Document = 
    | Content of string
    | Include of FilePath
    | CodeInclude of CodeInclude
    | CodeReference of CodeReference