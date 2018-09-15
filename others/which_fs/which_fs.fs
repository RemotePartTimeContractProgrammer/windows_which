
[<EntryPoint>]
let main argv =

    let cmd = argv.[0]

    let result = 
        System.Environment.GetEnvironmentVariable("PATH").Split(';')
        |> Array.filter(fun s -> s.Length > 0)
        |> Array.tryFind (fun elem -> System.IO.File.Exists(elem + "\\" + cmd))

    match result with
        | Some x -> printfn "found at: %s" (x + "\\" + cmd)
        | None -> printfn "command not found.\n"

    0