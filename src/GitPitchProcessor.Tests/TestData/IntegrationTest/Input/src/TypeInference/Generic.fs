type Result<'a> = Success of 'a | Failure 

let check f x =
    if f x then
        Success x
    else
        Failure

let speedPredicate speed = speed >= 88
let checkSpeed = check speedPredicate

let checkName name = check (fun n -> n <> "Biff") name