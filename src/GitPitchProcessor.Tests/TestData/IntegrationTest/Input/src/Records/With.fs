type Person = { firstName: string; lastName: string }

let marty = { firstName = "Marty"; lastName = "McFly" }
// val marty : Person = {firstName = "Marty"; lastName = "McFly";}

let george =  { marty with firstName = "George"}
// val george : Person = {firstName = "George";lastName = "McFly";}