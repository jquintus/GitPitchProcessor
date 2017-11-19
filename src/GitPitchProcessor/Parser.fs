module Parser

type FilePath = string
type CodeInclude = { 
    file: FilePath
    lang: string Option
    title: string Option
}
type CodeReference = { 
    title: string
    startLine: int
    endLine: int Option 
}

type Document = 
    | Content of string
    | Include of FilePath
    | CodeInclude of CodeInclude
    | CodeReference of CodeReference
    
let codeInclude str =
    // sample input: src/Code.fs&lang=FSharp&title=MyTitle

    CodeInclude {
        file = ""
        lang = None
        title = None
    }

let parse (line:string) = 
    let  (|Prefix|_|) (pattern:string) (str:string) =
        if str.ToLower().StartsWith (pattern.ToLower()) then
            Some (str.Substring pattern.Length)
        else
            None

    match line with
    | Prefix "---?include=" filePath -> Include filePath
    | Prefix "---?code=" codeIncludeStr -> codeInclude codeIncludeStr
    | _ ->   Content line