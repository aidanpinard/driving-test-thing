module alt.Al

open System

let private rand = new System.Random()

let private swap (a: _[]) x y =
   let tmp = a.[x]
   a.[x] <- a.[y]
   a.[y] <- tmp

// shuffle an array (in-place)
let private shuffle a =  
    a |> Array.iteri (fun i _ -> swap a i (rand.Next(i, Array.length a)))
    a

let private mapper (array:array<string>) map =
    let mutable newArr = [||]
    map 
    |> Array.iter (fun x -> 
        newArr <- Array.append newArr [|array.[x]|]
    )
    newArr

let private remap (a:array<array<string>>) map =
    let remappedArray = a |> Array.map (fun x -> (mapper x map))
    remappedArray

let generate noOfQuestions (q) =
    let map = shuffle [|0..67|]
    let questions = remap q map
    

    match noOfQuestions with
        | "all" -> questions
        | x -> [| 
            questions.[0].[0..((int x)-1)] 
            questions.[1].[0..((int x)-1)] 
            questions.[2].[0..((int x)-1)] 
            questions.[3].[0..((int x)-1)] 
            questions.[4].[0..((int x)-1)]
            questions.[5].[0..((int x)-1)]
        |]

let private inputChecker input =
    match input with
        | "a" | "A" -> "1"
        | "b" | "B" -> "2"
        | "c" | "C" -> "3"
        | "d" | "D" -> "4"
        | x -> x

let getUserResponses (session:array<array<string>>) = 
    let mutable responses = [||]
    session.[0] |> Array.iteri (fun i _ -> 
        Gen.printQuestion session i
        let resp = inputChecker (Console.ReadLine())
        responses <- Array.append responses [|resp|]
    )
    responses

let checkAnswers (answers:array<string>) (user:array<string>) (session:array<array<string>>) =
    let correct = [for i in [0..(answers.Length-1)] do if answers.[i] = user.[i] then yield i+1]
    let incorrect = [for i in [0..(answers.Length-1)] do if not (answers.[i] = user.[i]) then yield (i, (session.[0].[i]), (session.[(answers.[i] |> int)].[i]), (session.[(user.[i] |> int)].[i]) )]
    (correct, incorrect)

let private sChecker (incorrect:List<int * string * string * string>) =
    if incorrect.Length = 1 then "" else "s"

let private incorrections (incorrect:List<int * string * string * string>) =
    printfn "You have gotten %i question%s wrong.\n" incorrect.Length (sChecker incorrect)
    incorrect |> List.map (fun (questionNo, question, correctAnswer, userAnswer) ->
        printfn "%i. %s" questionNo question
        printfn "   Correct answer: %s" correctAnswer
        printfn "   Your answer: %s\n\n" userAnswer
    ) |> ignore
    ()

let private printCorrections (incorrect:List<int * string * string * string>) =
    match incorrect.Length with
        | 0 -> printfn "Good job. You have no Corrections."
        | _ -> incorrections incorrect

let showCorrections (answers:List<int>) (incorrect:List<int * string * string * string>) (session:array<array<string>>) =
    Console.Clear()
    printfn "Congrats. you have gotten %i questions correct. Your percentage is %.2f" answers.Length (((float answers.Length)/(float session.[0].Length))*100.0)

    incorrect |> printCorrections

    0