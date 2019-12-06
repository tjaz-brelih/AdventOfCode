﻿open System.IO

let rec readlines (filepath: string) = seq {
    use sr = new StreamReader (filepath)
    while not sr.EndOfStream do
        yield sr.ReadLine ()
}


let lineFolder acc (el: string) =
    let dir = el.[0]
    let length = el.Remove(0, 1) |> int
    let x, y = List.head acc

    match dir with
    | 'U' -> (x, y + length) :: acc
    | 'D' -> (x, y - length) :: acc 
    | 'L' -> (x - length, y) :: acc
    | 'R' -> (x + length, y) :: acc
    |  _  -> invalidOp "Invalid direction."

let calculateLines wire =
    wire 
    |> Array.fold lineFolder [(0, 0)] 
    |> List.pairwise

let lineIntersect ((x1,y1),(x2,y2)) ((x3,y3),(x4,y4)) =
    Some()


[<EntryPoint>]
let main _ =
    let wires = "input.txt" |> readlines |> Seq.map (fun x -> x.Split ',')

    let extractWire x = wires |> Seq.item x |> calculateLines
    
    let w1 = extractWire 0
    let w2 = extractWire 1

    let intersections = [for l1 in w1 do for l2 in w2 do yield lineIntersect l1 l2] |> List.filter (fun x -> x.IsSome)

    0
