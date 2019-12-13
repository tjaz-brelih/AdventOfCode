open System.IO
open System.Text.RegularExpressions

let rec readlines (filepath: string) = seq {
    use sr = new StreamReader (filepath)
    while not sr.EndOfStream do
        yield sr.ReadLine ()
}


let parseLine line =
    let matches = Regex.Matches(line, "[\d-]+")
    (matches.[0], matches.[1], matches.[2])

[<EntryPoint>]
let main _ =
    let niki = "input.txt" |> readlines |> parseLine

    0