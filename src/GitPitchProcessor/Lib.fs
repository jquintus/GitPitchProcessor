﻿[<AutoOpen>]
module Lib
open System

// convenient, functional TryParse wrappers returning option<'a>
let tryParseWith tryParseFunc = tryParseFunc >> function
    | true, v    -> Some v
    | false, _   -> None

let parseDate   = tryParseWith System.DateTime.TryParse
let parseInt    = tryParseWith System.Int32.TryParse
let parseSingle = tryParseWith System.Single.TryParse
let parseDouble = tryParseWith System.Double.TryParse
// etc.

// active patterns for try-parsing strings
let (|Date|_|)   = parseDate
let (|Int|_|)    = parseInt
let (|Single|_|) = parseSingle
let (|Double|_|) = parseDouble

let strToOpt (str:string) =
    if String.IsNullOrEmpty(str) then
        None
    else
        Some str

let getStringBefore (str:string) (c:char) =
    let idx = str.IndexOf c
    if idx > 0 then
        str.Substring (0, idx)
    else
        str

let splitString (c:char) (s:string) = s.Split(c)
let joinStrings (seperator : string) (s : string seq)  = String.Join(seperator, s |> Seq.toArray)

let trimSurrounding (c:char) (s:string) = 
    let len = s.Length
    if len < 2 then
        s
    else if s.[0] = c && s.[len - 1] = c then
        s.Substring(1, len - 2)
    else
        s