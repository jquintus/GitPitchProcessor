type Person = { firstName: string; lastName: string}
 
let marty = { firstName = "Marty"; lastName = "McFly" }
let marty' = { firstName = "Marty"; lastName = "McFly" } 
 
let equals = marty = marty'
// val equals : bool = true
let systemEquals = System.Object.Equals(marty, marty')
// val systemEquals : bool = true
let sameInstance = System.Object.ReferenceEquals(marty, marty')
// val sameInstance : bool = false