module IntegrationTests
open Program
open Input
open Xunit

[<Fact>]
let ``main --input in.md --output out.md `` () =
    // Assemble
    let argv = 
        "--inputfile TestData\IntegrationTest\Input\Pitchme.md --outputfile ActualOutput.md" 
        |> splitString ' '

    // Act
    main argv |> ignore

    // Assert
    let actual   = fromFile "ActualOutput.md" 
                   |> asString
    let expected = fromFile "TestData\IntegrationTest\Expected\Pitchme.md" 
                   |> asString

    Assert.Equal(expected, actual)