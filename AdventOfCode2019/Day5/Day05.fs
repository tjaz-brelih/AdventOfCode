open System
open System.IO

let rec readlines (filepath: string) = seq {
    use sr = new StreamReader (filepath)
    while not sr.EndOfStream do
        yield sr.ReadLine ()
}



let matchParam (program: int[]) index param =
    match param with
    | 0 -> program.[program.[index]]
    | 1 -> program.[index]
    | _ -> invalidOp "Unexpected parameter value." 


let op1_2 (index: int) (program: int[]) op (prms: int list) =
    prms 
    |> List.mapi (fun i x -> matchParam program (index + i + 1) x)
    |> List.reduce (fun acc el -> op acc el)

let op3 =
    printf "Input:  "
    Console.ReadLine() |> int

let op4 d =
    printfn "Output: %d" d

let op5_6 (index: int) (program: int[]) op (prms: int list) =
    let values = prms |> List.mapi (fun i x -> matchParam program (index + i + 1) x)
    match op (values |> List.head) 0 with
    | true  -> List.item 1 values
    | false -> index + 3

let op7_8 (index: int) (program: int[]) op (prms: int list) =
    let v1::v2::_ = prms |> List.mapi (fun i x -> matchParam program (index + i + 1) x)
    match op v1 v2 with
    | true  -> 1
    | false -> 0

let expand op =
    (op % 100, [op / 100 % 10; op / 1000 % 10])

let decode (index: int) (data: int[]) =
    let operation = data.[index] |> expand

    match operation |> fst with
    | 1  -> Some ( operation |> snd |> (op1_2 index data (+)) |> Array.set data data.[index + 3] |> fun _ -> index + 4 )
    | 2  -> Some ( operation |> snd |> (op1_2 index data (*)) |> Array.set data data.[index + 3] |> fun _ -> index + 4 )
    | 3  -> Some ( op3 |> Array.set data data.[index + 1] |> fun _ -> index + 2 )
    | 4  -> Some ( data.[data.[index + 1]] |> op4 |> fun _ -> index + 2 )
    | 5  -> Some ( operation |> snd |> op5_6 index data (<>) )
    | 6  -> Some ( operation |> snd |> op5_6 index data (=)  )
    | 7  -> Some ( operation |> snd |> (op7_8 index data (<)) |> Array.set data data.[index + 3] |> fun _ -> index + 4 )
    | 8  -> Some ( operation |> snd |> (op7_8 index data (=)) |> Array.set data data.[index + 3] |> fun _ -> index + 4 )
    | 99 -> None
    | _  -> invalidOp "Unexpected opcode." 

let setInputs noun verb program =
    noun |> Array.set program 1
    verb |> Array.set program 2 

let runProgram program =
    let mutable status = Some(0)
    while status.IsSome do
        let index = status.Value
        status <- (decode index program)


[<EntryPoint>]
let main _ =
    let input = ("input.txt" |> readlines |> Seq.item 0).Split ',' |> Seq.map int |> Seq.toArray

    input |> runProgram

    0
