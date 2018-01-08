let pi = System.Math.PI

type Shape =
    | Circle of float
    | Rectangle of float * float
    | Square of float

let area shape =
    match shape with
    | Circle radius -> radius * radius * pi
    | Rectangle (height, width) -> height * width
    | Square width -> width * width

let perimeter =
    function
    | Circle radius when radius > 0.0
                             -> 2.0 * radius * pi
    | Circle _               -> -1.0
    | Square 1.0             -> 4.0
    | Square width           -> width * 4.0
    | Rectangle (1.0, width) -> 2.0 + 2.0 * width
    | Rectangle (height, width)
                             -> (2.0 * height) + (2.0 * width)