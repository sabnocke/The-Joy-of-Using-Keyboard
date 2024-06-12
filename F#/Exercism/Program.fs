// For more information see https://aka.ms/fsharp-console-apps

open yacht
let printline fmt = printfn "%A" fmt

let dice = [Die.Three; Die.Three; Die.Three; Die.Five; Die.Five]

score Category.FourOfAKind [Die.Three; Die.Three; Die.Three; Die.Five; Die.Five] |> printline
remap dice |> List.filter(fun y -> (count y (remap dice)) >= 4) |> printline