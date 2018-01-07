module IntegrationTests
open Program
open Input
open Xunit
open Swensen.Unquote


[<Fact>]
let ``main --input in.md --output out.md `` () =
    // Assemble
    let argv = 
        "--inputfile TestData\IntegrationTest\Input\Pitchme.md --outputfile ActualOutput.md" 
        |> split ' '

    // Act
    main argv |> ignore

    // Assert
    let actual = fromFile "ActualOutput.md"
    let expected = fromFile "TestData\IntegrationTest\Expected\Pitchme.md"

    test <@ actual = expected @>