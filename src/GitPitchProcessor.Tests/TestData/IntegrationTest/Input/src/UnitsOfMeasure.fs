[<Measure>] type mile
[<Measure>] type km
[<Measure>] type hour
[<Measure>] type minute
[<Measure>] type second

let marathon = 26.2188<mile>
let halfMarathon = marathon / 2.0
// val halfMarathon : float<mile> = 13.1094
let marathon' = halfMarathon + halfMarathon
// val marathon' : float<mile> = 26.2188

let milesToKm m = m * 0.621371<km/mile>
let mphToMps h = h / 60.0<minute/hour> / 60.0<second/minute> 

let desiredSpeed = 88.0<mile/hour> 
                   |> mphToMps 
                   |> milesToKm
// val desiredSpeed : float<km/second> = 0.01518906889