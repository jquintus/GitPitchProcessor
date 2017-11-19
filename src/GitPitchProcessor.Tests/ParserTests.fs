module Tests

open Parser
open Swensen.Unquote
open Xunit

// Content Tests
[<Fact>]
let ``parse <content>`` () =
    let input = @"content"
    test <@ parse input = Content input  @>

[<Fact>]
let ``parse <>`` () =
    let input = @""
    test <@ parse input = Content input  @>

[<Fact>]
let ``parse < ---?include=md/SomeFile.md>`` () =
    let input = @"---?include=md/SomeFile.md"
    test <@ parse input = Content input  @>

// Include Tests
[<Fact>]
let ``parse <---?include=md/SomeFile.md>`` () =
    let input = @"---?include=md/SomeFile.md"
    test <@ parse input = Include @"md/SomeFile.md"  @>

// Code IncludeTests
[<Fact>]
let ``parse <+++?code=src/Code.fs&lang=FSharp&title=MyTitle>`` () =
    let input = 
        @"+++?code=src/Code.fs&lang=FSharp&title=MyTitle"
    test <@ parse input = CodeInclude { file = @"src/Code.fs"; lang = Some "FSharp"; title = Some "MyTitle" } @>

// Code Reference Tests
[<Fact>]
let ``parse <at[1](Slide Title)>`` () = // Can't include @ in an identifier name
    let input = @"@[1](Slide Title)"
    test <@ parse input = CodeReference { title = "Slide Title"; startLine = 1; endLine = None } @>

[<Fact>]
let ``parse <at[1-3](Slide Title)>`` () = // Can't include @ in an identifier name
    let input = @"@[1-3](Slide Title)"
    test <@ parse input = CodeReference { title = "Slide Title"; startLine = 1; endLine = Some 3 } @>