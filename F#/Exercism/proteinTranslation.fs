module proteinTranslation


let translate (code: string): string = 
    match code with
        | "AUG" -> "Methionine"
        | "UUU" | "UUC" -> "Phenylalanine"
        | "UUA" | "UUG" -> "Leucine"
        | "UCU" | "UCC" | "UCA" | "UCG" -> "Serine"
        | "UAU" | "UAC" -> "Tyrosine"
        | "UGU" | "UGC" -> "Cysteine"
        | "UGG" -> "Tryptophan"
        | "UAA" | "UAG" | "UGA" -> "STOP"
        | _ -> failwith $"unknown codon {code}"

let proteins rna = 
    Seq.chunkBySize 3 rna 
    |> Seq.map (System.String >> translate) 
    |> Seq.takeWhile (fun x -> x <> "STOP")
    |> Seq.toList