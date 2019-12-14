open System.IO
open System.Text.RegularExpressions
open System.Collections.Generic

let rec readlines (filepath: string) = seq {
    use sr = new StreamReader (filepath)
    while not sr.EndOfStream do
        yield sr.ReadLine ()
}


let parseLine line =
    let matches = Regex.Matches(line, "[\d-]+")
    ([|matches.[0].Value |> int; matches.[1].Value |> int; matches.[2].Value |> int|], [|0;0;0|])


let calculateDifference v1 v2 =
    match v1 - v2 with
        | d when d > 0 ->  1
        | d when d < 0 -> -1
        | _ -> 0

let calculateGravity (p1: int[], v1: int[]) (p2: int[], v2: int[]) =
    for i in 0 .. p1.Length - 1 do
        let gravity = calculateDifference p1.[i] p2.[i]
        Array.set v1 i (v1.[i] - gravity)
        Array.set v2 i (v2.[i] + gravity)

let move (p: int[],v: int[]) =
    for i in 0 .. p.Length - 1 do
        Array.set p i (p.[i] + v.[i])

let calculateStarEnergy (p: int[],v: int[]) =
    let folder = Array.fold (fun acc el -> acc + abs el) 0
    (p |> folder) * (v |> folder)

let calculateStateEnergy =
    List.fold (fun acc el -> acc + calculateStarEnergy el) 0

let compareStars (p1: int[], v1: int[]) (p2: int[], v2: int[]) =
    let folder = Array.fold2 (fun acc e1 e2 -> acc && (e1 = e2)) true
    (folder p1 p2) && (folder v1 v2)

let compareStates =
    List.fold2 (fun acc e1 e2 -> acc && compareStars e1 e2) true

let copyState =
    List.map (fun (p,v) -> (p |> Array.copy, v |> Array.copy))

[<EntryPoint>]
let main _ =
    let niki = "input.txt" |> readlines |> Seq.map parseLine |> Seq.toList

    let states = new Dictionary<int, (int[] * int []) list list>()
    states.Add(niki |> calculateStateEnergy, [niki |> copyState])

    let mutable iteration = 1
    let mutable iterate = Some ()

    while iterate.IsSome do
        for i in 0 .. niki.Length - 1 do
            for j in i + 1 .. niki.Length - 1 do
                calculateGravity niki.[i] niki.[j]

        niki |> List.iter move
        iteration <- iteration + 1

        let energy = niki |> calculateStateEnergy

        if states.ContainsKey(energy) then
            if states.[energy] |> List.exists (fun x -> compareStates x niki) then
                printfn "%d" (iteration - 1)
                iterate <- None
            else
                let prevStates = states.[energy]
                states.Remove(energy) |> ignore
                states.Add(energy, (niki |> copyState) :: prevStates)
        else
            states.Add(energy, [niki |> copyState])

    0