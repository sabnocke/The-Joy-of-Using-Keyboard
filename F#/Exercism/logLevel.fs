module logLevel

let message (logLine: string): string = logLine.Split(": ")[1] |> (fun x -> x.Trim())

let parse(_i: string): string = _i[1.._i.Length - 2].ToLower()
let logLevel(logLine: string): string = logLine.Split(": ")[0] |> parse

let reformat(logLine: string): string = logLine.Split(": ") |> (fun x -> $"{x[1].Trim()} ({parse x[0]})" )