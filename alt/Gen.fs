module alt.Gen

let private pos x = 
    match x with
    | 1 -> "a. "
    | 2 -> "b. "
    | 3 -> "c. "
    | 4 -> "d. "
    | _ -> "e. "

let printQuestion (q:array<array<string>>) (x) =
    System.Console.Clear()
    printfn "%i. %s\n\t%s%s\n\t%s%s\n\t%s%s\n\t%s%s" (x + 1) q.[0].[x] (pos 1) q.[1].[x] (pos 2) q.[2].[x] (pos 3) q.[3].[x] (pos 4) q.[4].[x]
    printf "User@Response> "
    
let printAllQuestions (q:array<array<string>>) = 
    for i in [1..q.[0].Length] do
        printfn "%i. %s\n\t%s%s\n\t%s%s\n\t%s%s\n\t%s%s" i q.[0].[i-1] (pos 1) q.[1].[i-1] (pos 2) q.[2].[i-1] (pos 3) q.[3].[i-1] (pos 4) q.[4].[i-1]

let decodeAnswers = 
    let ansEncoded = (Parse.parseAnswers "../../../data/data1.bin").[0]
    let bytes1 = System.Convert.FromBase64String ansEncoded
    let encode = System.Text.Encoding.UTF8.GetString bytes1
    let bytes2 = System.Convert.FromBase64String encode
    let answers = System.Text.Encoding.UTF8.GetString bytes2
    answers.ToCharArray() |> Array.map string