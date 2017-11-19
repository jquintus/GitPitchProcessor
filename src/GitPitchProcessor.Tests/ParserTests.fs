module Tests

open Parser
open ParserTypes
open Swensen.Unquote
open Xunit

// Helpers
let testParse input expected = test <@ parse input = expected  @>

let testParseLines lines expected = 
    let input = Input.fromList lines
    test <@ parseLines input  |> Seq.toList = expected  @>

module ``Content Tests``=
    [<Fact>]
    let ``parse <content>`` () = testParse "content" (Content "content")

    [<Fact>]
    let ``parse <>`` () = testParse @"" (Content @"")

    [<Fact>]
    let ``parse < ---?include=md/SomeFile.md>`` () =
        let input = @" ---?include=md/SomeFile.md"
        testParse input (Content input)
 
module ``Include Tests``=
    [<Fact>]
    let ``parse <---?include=md/SomeFile.md>`` () =
        let input = @"---?include=md/SomeFile.md"
        testParse input (Include @"md/SomeFile.md")

    [<Fact>]
    let ``parse <+++?include=md/SomeFile.md>`` () =
        let input = @"+++?include=md/SomeFile.md"
        testParse input (Include @"md/SomeFile.md")

module ``Code IncludeTests`` =
    [<Fact>]
    let ``parse <+++?code=src/Code.fs&lang=FSharp&title=My Title>`` () =
        let input    = @"+++?code=src/Code.fs&lang=FSharp&title=My Title"
        let expected = ci @"src/Code.fs" (Some "FSharp") (Some "My Title")
        testParse input expected

    [<Fact>]
    let ``parse <+++?code=src/Code.fs&lang=FSharp>`` () =
        let input    = @"+++?code=src/Code.fs&lang=FSharp"
        let expected = ci @"src/Code.fs" (Some "FSharp") None
        testParse input expected

    [<Fact>]
    let ``parse <+++?code=src/Code.fs&title=My Title>`` () =
        let input    = @"+++?code=src/Code.fs&title=My Title"
        let expected = ci @"src/Code.fs" None (Some "My Title")
        testParse input expected

    [<Fact>]
    let ``parse <+++?code=src/Code.fs>`` () =
        let input    = @"+++?code=src/Code.fs"
        let expected = ci @"src/Code.fs" None None
        testParse input expected

    [<Fact>]
    let ``parse <+++?code=src/Code.fs&>`` () =
        let input    = @"+++?code=src/Code.fs&"
        let expected = ci @"src/Code.fs" None None
        testParse input expected

    [<Fact>]
    let ``parse <+++?code=src/Code.fs&something=value>`` () =
        let input    = @"+++?code=src/Code.fs&something=value"
        let expected = ci @"src/Code.fs" None None
        testParse input expected

    [<Fact>]
    let ``parse <+++?code=src/Code.fs&title=My Title&lang=FSharp>`` () =
        let input    = @"+++?code=src/Code.fs&title=My Title&lang=FSharp"
        let expected = ci @"src/Code.fs" (Some "FSharp") (Some "My Title")
        testParse input expected

    // Now with --- leaders
    [<Fact>]
    let ``parse <---?code=src/Code.fs&lang=FSharp&title=My Title>`` () =
        let input    = @"---?code=src/Code.fs&lang=FSharp&title=My Title"
        let expected = ci @"src/Code.fs" (Some "FSharp") (Some "My Title")
        testParse input expected

    [<Fact>]
    let ``parse <---?code=src/Code.fs&lang=FSharp>`` () =
        let input    = @"---?code=src/Code.fs&lang=FSharp"
        let expected = ci @"src/Code.fs" (Some "FSharp") None
        testParse input expected

    [<Fact>]
    let ``parse <---?code=src/Code.fs&title=My Title>`` () =
        let input    = @"---?code=src/Code.fs&title=My Title"
        let expected = ci @"src/Code.fs" None (Some "My Title")
        testParse input expected

    [<Fact>]
    let ``parse <---?code=src/Code.fs>`` () =
        let input    = @"---?code=src/Code.fs"
        let expected = ci @"src/Code.fs" None None
        testParse input expected

    [<Fact>]
    let ``parse <---?code=src/Code.fs&>`` () =
        let input    = @"---?code=src/Code.fs&"
        let expected = ci @"src/Code.fs" None None
        testParse input expected

    [<Fact>]
    let ``parse <---?code=src/Code.fs&something=value>`` () =
        let input    = @"---?code=src/Code.fs&something=value"
        let expected = ci @"src/Code.fs" None None
        testParse input expected

    [<Fact>]
    let ``parse <---?code=src/Code.fs&title=My Title&lang=FSharp>`` () =
        let input    = @"---?code=src/Code.fs&title=My Title&lang=FSharp"
        let expected = ci @"src/Code.fs" (Some "FSharp") (Some "My Title")
        testParse input expected

module ``Code Reference Tests`` =
    let cr startLine endLine title =
        CodeReference {
            startLine = startLine
            endLine = endLine
            title = title
        }
    let cr_s s = cr s None None
    let cr_se s e = cr s (Some e) None
    let cr_st s t = cr s None (Some t)
    let cr_set s e t = cr s (Some e) (Some t)

    [<Fact>]
    let ``parse <at[1](Slide Title)>`` () = // Can't include @ in an identifier name
        let input = @"@[1](Slide Title)"
        let expected = cr_st 1 "Slide Title"
        testParse input expected

    [<Fact>]
    let ``parse <at[1-3](Slide Title)>`` () = // Can't include @ in an identifier name
        let input = @"@[1-3](Slide Title)"
        let expected = cr_set 1 3 "Slide Title"
        testParse input expected

    [<Fact>]
    let ``parse <at[1-3]()>`` () = // Can't include @ in an identifier name
        let input = @"@[1-3]()"
        let expected = cr_se 1 3
        testParse input expected

    [<Fact>]
    let ``parse <at[1-3]>`` () = // Can't include @ in an identifier name
        let input = @"@[1-3]"
        let expected = cr_se 1 3
        testParse input expected

    [<Fact>]
    let ``parse <at[1]()>`` () = // Can't include @ in an identifier name
        let input = @"@[1]()"
        let expected = cr_s 1
        testParse input expected

    [<Fact>]
    let ``parse <at[1]>`` () = // Can't include @ in an identifier name
        let input = @"@[1]"
        let expected = cr_s 1
        testParse input expected

module ``parseLines Tests`` =
    [<Fact>]
    let ``parseLines singleLine returns single Document``() =
        let lines = [ "some content" ]
        let expected = [ Content "some content" ]
        testParseLines lines expected

    [<Fact>]
    let ``parseLines no lines returns empty list``() =
        testParseLines [ ] [ ] 

    [<Fact>]
    let ``parseLines several lines returns several docuemtns``() =
        let lines = [ 
            "some content";
             "---?include=md/SomeFile.md";
              @"+++?code=src/Code.fs";
              @"@[1](Slide Title)"
        ]
        let expected = [ 
            Content "some content";
            Include @"md/SomeFile.md";
            ci @"src/Code.fs" None None
            codeReference 1 None (Some "Slide Title")
        ]
        testParseLines lines expected