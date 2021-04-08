module alt.Program

open System
open System.Threading

[<EntryPoint>]
let main _argv =
    let qanda = Array.append Parse.parseQuestions [|Gen.decodeAnswers|]

    printf "How many questions do you want to attempt? (Must be less than or equal to 68) \nUser@Response> "
    let noOfQuestions = Console.ReadLine()
    Console.Clear()
    printfn "Please enter 1, 2, 3, 4 or a, b, c, d as your answer."
    Thread.Sleep(2000)

    let session = Al.generate noOfQuestions qanda

    let userResponses = Al.getUserResponses session

    let answers, missed = Al.checkAnswers session.[5] userResponses session
    
    Al.showCorrections answers missed session |> ignore

    Thread.Sleep(2000)
    printfn "Press any key to continue..."
    Console.ReadKey() |> ignore

    Console.Clear()
    0 // return an integer exit code
