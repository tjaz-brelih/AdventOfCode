open System.IO

let rec readlines (filepath: string) = seq {
    use sr = new StreamReader (filepath)
    while not sr.EndOfStream do
        yield sr.ReadLine ()
}


let combine xs ys = [
    for x in xs do
    for y in ys do
    yield x, y ]

let execute op (index1: int) (index2: int) (data: int[]) =
    op data.[index1] data.[index2]

let operation (index: int) (data: int[]) =
    let opcode = data.[index]

    match opcode with
    | 1  -> Some( ((+) data.[data.[index + 1]] data.[data.[index + 2]]) |> Array.set data data.[index + 3] )
    | 2  -> Some( ((*) data.[data.[index + 1]] data.[data.[index + 2]]) |> Array.set data data.[index + 3] )
    | 99 -> None
    | _  -> invalidOp "Unexpected opcode." 

let setInputs noun verb program =
    noun |> Array.set program 1
    verb |> Array.set program 2 

let runProgram program =
    let mutable index = 0
    while (operation index program).IsSome do 
        index <- index + 4

    program.[0]


[<EntryPoint>]
let main _ =
    let input = ("input.txt" |> readlines |> Seq.item 0).Split ',' |> Seq.map int |> Seq.toArray

    // ----- PART 1 -----
    let program = Array.copy input

    setInputs 12 2 program

    program |> runProgram |> printfn "%d"

    
    // ----- PART 2 -----
    let output = 19690720
    let maxValue = 100

    let inputs = combine [0 .. maxValue] [0 .. maxValue]

    for (noun, verb) in inputs do
        let program = Array.copy input
        
        setInputs noun verb program

        if (program |> runProgram) = output then
            printfn "%d" (100 * noun + verb)

    0
