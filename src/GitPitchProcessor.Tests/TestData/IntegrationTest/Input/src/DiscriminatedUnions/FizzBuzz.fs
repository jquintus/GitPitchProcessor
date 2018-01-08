let fizzBuzz max =
    let getFizzBuzz x = 
        match x % 3, x % 5 with  
        | 0, 0 -> "fizzbuzz"
        | _, 0 -> "fizz"
        | 0, _ -> "buzz"
        | _    -> x.ToString()
    [ 1 .. max ]
    |> Seq.map getFizzBuzz
    |> Seq.iter (printfn "%A")

fizzBuzz 16