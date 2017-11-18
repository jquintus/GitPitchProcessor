module Tests

open Lib
open System
open Swensen.Unquote
open Xunit

[<Fact>]
let ``parse "hello" return "hello"`` () =
    test <@ parse "hello" = "hello" @>