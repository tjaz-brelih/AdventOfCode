open System
open System.IO

let rec readlines (filepath: string) = seq {
    use sr = new StreamReader (filepath)
    while not sr.EndOfStream do
        yield sr.ReadLine ()
}



let getValueParam (program: int64[]) (relBase: int) (index: int) param =
    match param with
        | 0 -> program.[program.[index] |> int]
        | 1 -> program.[index]
        | 2 -> program.[(program.[index] |> int) + relBase]
        | _ -> invalidOp "Unexpected parameter value." 

let setValueParam (program: int64[]) (relBase: int) (index: int) param value =
    match param with
        | 0 -> Array.set program (program.[index] |> int) value
        | 2 -> Array.set program ((program.[index] |> int) + relBase) value
        | _ -> invalidOp "Invalid mode for setting value."

let op1_2 (program: int64[]) (index: int) relBase op (prms: int list) =
    prms.[0..1]
    |> List.mapi (fun i x -> getValueParam program relBase (index + 1 + i) x)
    |> List.reduce (fun acc el -> op acc el)
    |> setValueParam program relBase (index + 3) prms.[2]

let op3 (program: int64[]) (index: int) relBase (prms: int list) =
    printf "(%d) Input:  " index
    let value = Console.ReadLine() |> int64
    value |> setValueParam program relBase (index + 1) prms.[0]

let op4 (program: int64[]) (index: int) relBase (prms: int list) =
    prms.[0]
    |> getValueParam program relBase (index + 1)
    |> printfn "(%d) Output: %d" index

let op5_6 (program: int64[]) (index: int) relBase op (prms: int list) =
    let value = getValueParam program relBase (index + 1) prms.[0]
    match op value (0L) with
        | true  -> (getValueParam program relBase (index + 2) prms.[1]) |> int
        | false -> (index + 3)

let op7_8 (program: int64[]) (index: int) relBase op (prms: int list) =
    let v1::v2::_ = prms.[0..1] |> List.mapi (fun i x -> getValueParam program relBase (index + i + 1) x)
    match op v1 v2 with
        | true  -> 1L
        | false -> 0L
    |> setValueParam program relBase (index + 3) prms.[2]

let op9 (program: int64[]) (index: int) relBase (prms: int list) =
    prms.[0]
    |> getValueParam program relBase (index + 1)
    |> int

let expand op =
    (op % 100, [op / 100 % 10; op / 1000 % 10; op / 10000 % 10])

let decode (data: int64[]) (index: int) relBase  =
    let opCode, parameters = (data.[index] |> int) |> expand

    match opCode with
        | 1  -> Some ( parameters |> (op1_2 data index relBase (+)) |> fun _ -> (index + 4, relBase) )
        | 2  -> Some ( parameters |> (op1_2 data index relBase (*)) |> fun _ -> (index + 4, relBase) )
        | 3  -> Some ( parameters |> (op3 data index relBase) |> fun _ -> (index + 2, relBase) )
        | 4  -> Some ( parameters |> (op4 data index relBase) |> fun _ -> (index + 2, relBase) )
        | 5  -> Some ( parameters |> (op5_6 data index relBase (<>)) |> fun x -> (x |> int, relBase) )
        | 6  -> Some ( parameters |> (op5_6 data index relBase (=))  |> fun x -> (x |> int, relBase) )
        | 7  -> Some ( parameters |> (op7_8 data index relBase (<)) |> fun _ -> (index + 4, relBase) )
        | 8  -> Some ( parameters |> (op7_8 data index relBase (=)) |> fun _ -> (index + 4, relBase) )
        | 9  -> Some ( parameters |> (op9 data index relBase) |> fun x -> (index + 2, relBase + x ) )
        | 99 -> None
        | _  -> invalidOp "Unexpected opcode." 


let runProgram program =
    let mutable status = Some((0,0))
    while status.IsSome do
        let index, relBase = status.Value
        status <- decode program index relBase



[<EntryPoint>]
let main _ =
    let mutable input = ("input.txt" |> readlines |> Seq.item 0).Split ',' |> Seq.map int64 |> Seq.toArray

    Array.Resize(&input, 10000)

    input |> runProgram

    0
