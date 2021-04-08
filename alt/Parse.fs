module alt.Parse

open System.IO

let private filteri f s =
  s
  |> Seq.mapi (fun i v -> (i, v))
  |> Seq.filter (fun v -> f (fst v) (snd v))
  |> Seq.map (fun v -> snd v)

let parseQuestions =
    let file = File.ReadAllLines "../../../data/data2.bin" |> Array.toSeq

    let filt rem = file |> filteri (fun i _ -> i % 5 = rem) |> Seq.toArray
    let qanda = [|for x in [0..4] do yield filt x|]

    qanda

let parseAnswers filename =
    File.ReadAllLines filename
