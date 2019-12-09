open System
open System.IO

let rec readlines (filepath: string) = seq {
    use sr = new StreamReader (filepath)
    while not sr.EndOfStream do
        yield sr.ReadLine ()
}


let rec intToArray input =
    match input with
    | x when x < 10 -> [x]
    | x -> (x / 10) |> intToArray |> List.append <| [x % 10]

let matchParam (program: int64[]) (relBase: int) (index: int) param =
    match param with
    | 0 -> program.[program.[index] |> int]
    | 1 -> program.[index]
    | 2 -> program.[program.[relBase] |> int]
    | _ -> invalidOp "Unexpected parameter value." 


let op1_2 (program: int64[]) (index: int) relBase op (prms: int list) =
    prms 
    |> List.mapi (fun i x -> matchParam program relBase (index + i + 1) x)
    |> List.reduce (fun acc el -> op acc el)

let op3 index =
    printf "(%d) Input:  " index
    Console.ReadLine() |> int64

let op4 (program: int64[]) (index: int) relBase (prms: int list) =
    prms
    |> List.item 0
    |> matchParam program relBase (index + 1)
    |> printfn "Output: %d"

let op5_6 (program: int64[]) (index: int) relBase op (prms: int list) =
    let values = prms |> List.mapi (fun i x -> matchParam program relBase (index + i + 1) x)
    match op (values |> List.head) (0 |> int64) with
    | true  -> values |> List.item 1 |> int
    | false -> (index + 3)

let op7_8 (program: int64[]) (index: int) relBase op (prms: int list) =
    let v1::v2::_ = prms |> List.mapi (fun i x -> matchParam program relBase (index + i + 1) x)
    match op v1 v2 with
    | true  -> 1 |> int64
    | false -> 0 |> int64

let op9 (program: int64[]) (index: int) relBase (prms: int list) =
    prms
    |> List.item 0
    |> matchParam program relBase (index + 1)
    |> int

let expand op =
    (op % 100, [op / 100 % 10; op / 1000 % 10])

let decode (data: int64[]) (index: int) relBase  =
    let opCode, parameters = (data.[index] |> int) |> expand

    match opCode with
    | 1  -> Some ( parameters |> (op1_2 data index relBase (+)) |> Array.set data (data.[index + 3] |> int) |> fun _ -> (index + 4, relBase) )
    | 2  -> Some ( parameters |> (op1_2 data index relBase (*)) |> Array.set data (data.[index + 3] |> int) |> fun _ -> (index + 4, relBase) )
    | 3  -> Some ( index |> op3 |> Array.set data (data.[index + 1] |> int) |> fun _ -> (index + 2, relBase) )
    | 4  -> Some ( parameters |> (op4 data index relBase) |> fun _ -> (index + 2, relBase) )
    | 5  -> Some ( parameters |> op5_6 data index relBase (<>) |> fun x -> (x |> int, relBase) )
    | 6  -> Some ( parameters |> op5_6 data index relBase (=)  |> fun x -> (x |> int, relBase) )
    | 7  -> Some ( parameters |> (op7_8 data index relBase (<)) |> Array.set data (data.[index + 3] |> int) |> fun _ -> (index + 4, relBase) )
    | 8  -> Some ( parameters |> (op7_8 data index relBase (=)) |> Array.set data (data.[index + 3] |> int) |> fun _ -> (index + 4, relBase) )
    | 9  -> Some ( parameters |> op9 data index relBase |> fun x -> (index + 2, relBase + x ) )
    | 99 -> None
    | _  -> invalidOp "Unexpected opcode." 


let runProgram program =
    let mutable status = Some((0,0))
    while status.IsSome do
        let index, relBase = status.Value
        status <- decode program index relBase



[<EntryPoint>]
let main _ =
    let input = ("input.txt" |> readlines |> Seq.item 0).Split ',' |> Seq.map int64 |> Seq.toArray

    Array.Resize(, 1000)

    input |> runProgram

    0
