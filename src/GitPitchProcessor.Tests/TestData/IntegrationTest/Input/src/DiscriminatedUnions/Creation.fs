type Shape =Circle of int| Rectangle of int * int | Square of int
let aCircle = Circle 3
let aRectangle = Rectangle (2, 3)

type Option<'a> = 
    | Some of 'a
    | None
 
type Person = { name: string; age: int}
type KitchenSink =
    | KewlDude of Person
    | Order of string * int
    | EmptyCase
    | Things of int list 
let sinkPerson = KewlDude { name= "Marty McFly"; age = 17 }