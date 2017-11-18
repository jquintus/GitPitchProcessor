namespace GitPitchProcessor.Tests

open Fuchu

module Say =
    let hello name =
        printfn "Hello %s" name
    
    [<Tests>] 
    let tests = 
        testList "Simple Tests" [
            testCase "2 + 2 = 4" <| fun _ ->
                Assert.Equal("2 + 2", 4, 2 + 2) 
            testCase "2 + 2 = 5" <| fun _ ->
                Assert.Equal("2 + 5", 4, 2 + 2) 
        ]