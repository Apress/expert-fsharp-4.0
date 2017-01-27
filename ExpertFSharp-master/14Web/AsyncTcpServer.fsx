open System.Net
open System.Net.Sockets
open System.IO
open System.Text

type AsyncTcpServer(addr, port, handleServerRequest) = 
    let socket = new TcpListener(addr, port)

    member x.Start() = async { do x.Run() } |> Async.Start

    member x.Run() = 
        socket.Start()
        while true do
            let client = socket.AcceptTcpClient()
            async {
                try do! handleServerRequest client with e -> ()
            }
            |> Async.Start

module Quotes =
    let private quoteSize = 8
    let private quoteHeaderSize = 4
    let private quoteSeriesLength = 3

    module Server =
        let HandleRequest (client: TcpClient) =
            // Dummy header and quote
            let header = Array.init<byte> quoteSize (fun i -> 1uy)
            let quote = Array.init<byte> quoteSize (fun i -> byte(i % 256))
            async {
                use stream = client.GetStream()
                do! stream.AsyncWrite(header, 0, quoteHeaderSize) // Header
                for _ in [0 .. quoteSeriesLength] do
                    do! stream.AsyncWrite(quote, 0, quote.Length) 
                    // Mock an I/O wait for the next quote
                    do! Async.Sleep 1000
            }

        let Start () =
            let S = new AsyncTcpServer(IPAddress.Loopback,10003,HandleRequest)
            S.Start()

    module Client =
        let RequestQuote =
            async {
                let client = new TcpClient()
                client.Connect(IPAddress.Loopback, 10003)
                use stream = client.GetStream()
                let header = Array.create quoteHeaderSize 0uy
                let! read = stream.AsyncRead(header, 0, quoteHeaderSize)
                if read = 0 then return () else printfn "Header: %A" header
                while true do
                    let buffer = Array.create quoteSize 0uy
                    let! read = stream.AsyncRead(buffer, 0, quoteSize)
                    if read = 0 then return () else printfn "Quote: %A" buffer
            }
            |> Async.Start
