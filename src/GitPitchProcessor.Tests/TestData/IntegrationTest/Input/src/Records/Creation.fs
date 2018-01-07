type Person = { firstName: string; lastName: string }
let mcFly = { firstName = "Marty"; lastName = "McFly" }

type FruitOrderRecord = {
  fruit: string
  price: float
  count: int
}

let calcTotal order =
  let { price = _; count = myCount } = order
  order.price * float(myCount)

let total = calcTotal { fruit = "apples"; price = 0.35; count = 2 }