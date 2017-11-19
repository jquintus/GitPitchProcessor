module Parser

open System
open ParserTypes
open System.Text.RegularExpressions

let parse (line:string) = 
    let  (|Prefix|_|) (pattern:string) (str:string) =
        if str.ToLower().StartsWith (pattern.ToLower()) then
            Some (str.Substring pattern.Length)
        else
            None

    let toParamList (str:string) = 
        let paramArray = str.Split('&', StringSplitOptions.RemoveEmptyEntries)
        Array.skip 1 paramArray

    let findParamValue (paramName:string) paramList =
        let toParamValue value =
            let name = paramName + "="
            match value with
            | Prefix name rest -> Some rest
            | _ -> None

        let values = paramList 
                     |> Array.map toParamValue
                     |> Array.where (fun x -> x.IsSome)
        if values.Length > 0 then 
            values.[0]
        else
            None

    let codeInclude str =
        // sample input: src/Code.fs&lang=FSharp&title=MyTitle
        let path = getStringBefore str '&'
        let paramList = toParamList str

        let lang = findParamValue "lang" paramList
        let title = findParamValue "title" paramList

        CodeInclude {
            file = path
            lang = lang
            title = title
        }

    let  (|PagePrefix|_|) (pattern:string) (str:string) =
        let lowerPattern = pattern.ToLower()
        let newSlidePattern  = "---?" + lowerPattern
        let newSectionPattern = "+++?" + lowerPattern

        let lowerStr = str.ToLower();

        let isMatch =
            (lowerStr.StartsWith newSlidePattern) 
                || (lowerStr.StartsWith newSectionPattern)

        if isMatch then
            Some (str.Substring newSlidePattern.Length)
        else 
            None

    let (|CR|_|) (str:string) =
        let rx = @"^@\s*\[\s*(?<start>\d+)\s*(-\s*(?<end>\d+))?\s*]\s*(\((?<title>.*)\))?\s*$"
        let result = Regex.Match(str, rx, RegexOptions.IgnorePatternWhitespace)
        if result.Success then
            let start =  result.Groups.["start"].Value |> parseInt
            let endLine = result.Groups.["end"].Value |> parseInt
            let title = result.Groups.["title"].Value |> strToOpt
            match start with 
            | None -> None
            | Some value -> Some (codeReference value endLine title)
        else 
            None

    match line with
    | PagePrefix "include=" filePath -> Include filePath
    | PagePrefix "code=" codeIncludeStr -> codeInclude codeIncludeStr
    | CR cr -> cr
    | _ ->   Content line