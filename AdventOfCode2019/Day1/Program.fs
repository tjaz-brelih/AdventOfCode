open System.IO

let rec readlines (filepath: string) = seq {
    use sr = new StreamReader (filepath)
    while not sr.EndOfStream do
        yield sr.ReadLine ()
}



let calculateFuel mass =
    (mass / 3) - 2

let calculateModulesFuel list =
    list
    |> Seq.map int
    |> Seq.map calculateFuel 
    |> Seq.sum

let rec calculateFuelFuel mass =
    let fuelMass = mass |> calculateFuel

    match fuelMass with
    | x when x > 0 -> x + (x |> calculateFuelFuel)
    | _ -> 0


[<EntryPoint>]
let main _ =
    let input = readlines "input.txt"
    let fuelPerModules = input |> Seq.map int |> Seq.map calculateFuel

    let modulesFuel = fuelPerModules |> Seq.sum
    modulesFuel |> printfn "%d"
    
    let totalModulesFuelFold = fuelPerModules |> Seq.fold (fun acc el -> acc + (el |> calculateFuelFuel)) 0 
    (totalModulesFuelFold + modulesFuel) |> printfn "%d"
    

    0
