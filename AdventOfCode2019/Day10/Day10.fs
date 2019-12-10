open System.IO

let rec readlines (filepath: string) = seq {
    use sr = new StreamReader (filepath)
    while not sr.EndOfStream do
        yield sr.ReadLine ()
}


let getCoordinates y (line: string) =
    line 
    |> Seq.mapi (fun x c -> match c with
                                | '#' -> Some (x |> double, y |> double)
                                | '.' -> None
                                |  _  -> invalidArg "c" "Invalid input character.")
    |> Seq.filter (fun x -> x.IsSome)
    |> Seq.map (fun x -> x.Value)
    |> Seq.toList

let arePointsSame (x1,y1) (x2,y2) =
    x1 = x2 && y1 = y2

let calculateSlope (x1: double,y1: double) (x2: double,y2: double) =
    match x2 - x1 with
        | 0.0 -> infinity |> double
        | dx  -> (y2 - y1) / dx
    

[<EntryPoint>]
let main _ =
    let input = "input.txt" |> readlines |> Seq.mapi getCoordinates

    let niki =[for p1 in input do for p2 in input do match arePointsSame p1 p2 with 
                                                        | true -> Some (p1,p2) 
                                                        | false -> None]

    0