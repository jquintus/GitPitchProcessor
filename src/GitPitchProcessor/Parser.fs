module Parser

open System
open ParserTypes
open System.Text.RegularExpressions

let private (|Prefix|_|) (pattern:string) (str:string) =
    if str.ToLower().StartsWith (pattern.ToLower()) then
        Some (str.Substring pattern.Length)
    else
        None

let private toParamList (str:string) =
    let paramArray = str.Split('&', StringSplitOptions.RemoveEmptyEntries)
    Array.skip 1 paramArray

let private findParamValue (paramName:string) paramList =
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

let private codeInclude str =
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

let private (|PagePrefix|_|) (pattern:string) (str:string) =
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

let private (|CR|_|) (str:string) =
    let rx = @"" +
                @"^@\s*           " +  // Line starts with a @
                @"\[\s*           " +  // Opening [
                @"(?<start>\d+)   " +  // Starting line number
                @"\s*             " + 
                @"(               " +  // Opening optional number
                @"    -\s*        " +  // Optional dash
                @"    (?<end>\d+) " +  // Optional ending line number
                @")?              " +  // Closing optional number
                @"\s*]\s*         " +  // Closing ]
                @"(               " +  // Opening optional title
                @"    \(          " +  // Opening (
                @"    (?<title>.*)" +  // Optional title
                @"    \)          " +  // Closing )
                @")?              " +  // Closing optional title
                @"\s*$            "    // Trailing space

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

let parse (line:string) =
    match line with
    | PagePrefix "include=" filePath -> Include filePath
    | PagePrefix "code=" codeIncludeStr -> codeInclude codeIncludeStr
    | CR cr -> cr
    | _ ->   Content line

let parseLines inputStream = 
    let (Input.T lines) = inputStream
    lines
    |> Seq.map parse

let toString = function
    | Content ct -> ct
    | Include path -> sprintf ">>> Include=%s" path
    | CodeInclude c -> sprintf ">>> Code=%s" c.file
    | CodeReference cr -> sprintf ">>> CR %i" cr.startLine

let rec processLines fileReader pitchLines = 
    let processInclude path = 
        let inputStream = fileReader path
        let newLines = parseLines inputStream
        processLines fileReader newLines
        
    let processCodeInclude cincl = 
        Seq.singleton (Content cincl.file)
    
    let processLine line = 
        match line with 
        | Content _     // -> Fall through to the next case
        | CodeReference _  -> Seq.singleton line
        | Include path     -> processInclude path
        | CodeInclude c    -> processCodeInclude c

    pitchLines
    |> Seq.map processLine
    |> Seq.collect id
