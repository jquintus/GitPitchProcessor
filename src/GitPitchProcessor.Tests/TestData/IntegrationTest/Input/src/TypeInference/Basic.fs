let genericEquals x y = x = y
let intsEqual = genericEquals 1 2
let floatEqual = genericEquals 1.0 2.0
let stringEqual = genericEquals "Hello" "world"

let addInts x y = x + y
let anInt = addInts 1 2
// let aFloat = addInts 1.0 2.0 // Fails

let length (s:string) = s.Length