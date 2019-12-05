let rec intToArray input =
    match input with
    | x when x < 10 -> [x]
    | x -> (x / 10) |> intToArray |> List.append <| [x % 10]

let rec compareAdjacentDigits op input =
    match input with
    | h1::h2::tail -> [h1 <= h2] |> List.append <| compareAdjacentDigits op (h2::tail)
    | [_] -> []
    | []  -> []

let rec splitAtChangedDigit input =
    List.groupBy (fun x -> x) input |> List.map snd
    
let folder acc el =
    let intList = el |> intToArray
    
    let adj = intList |> splitAtChangedDigit |> List.exists (fun x -> x.Length >= 2)
    let inc = intList |> compareAdjacentDigits (<=) |> List.forall (fun x -> x)

    match (adj, inc) with
    | (true, true) -> acc + 1
    | (_, _) -> acc

let folder2 acc el =
    let intList = el |> intToArray
        
    let adj = intList |> splitAtChangedDigit |> List.exists (fun x -> x.Length = 2)
    let inc = intList |> compareAdjacentDigits (<=) |> List.forall (fun x -> x)
    
    match (adj, inc) with
    | (true, true) -> acc + 1
    | (_, _) -> acc


[<EntryPoint>]
let main _ =
    let min = 245182
    let max = 790572

    let range = [min .. max]
    
    printfn "%d" (List.fold folder  0 range)
    printfn "%d" (List.fold folder2 0 range)

    0 
