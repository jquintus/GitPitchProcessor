let myTuple = ("apples", 5)
let (fruit, count) = myTuple

type FruitOrderTuple = string * float * int
let calculateTotal (order: FruitOrderTuple) =
  let (_, cost, count) = order
  cost * float count

let cost = calculateTotal ("apples", 0.35, 2)