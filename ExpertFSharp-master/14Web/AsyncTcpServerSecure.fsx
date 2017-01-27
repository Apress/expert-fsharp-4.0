open System.Net
open System.Net.Sockets
open System.Net.Security
open System.Security.Authentication
open System.Security.Cryptography.X509Certificates

type AsyncTcpServer(addr, port, handleServerRequest) = 
    let socket = new TcpListener(addr, port)

    member x.Start() = async { do x.Run() } |> Async.Start

    member x.Run() = 
        socket.Start()
        while true do
            let client = socket.AcceptTcpClient()
            async {
                try do! handleServerRequest (client.GetStream()) with e -> ()
            }
            |> Async.Start

type AsyncTcpServerSecure(addr, port, handleServerRequest) = 

    // Gets the first certificate with a friendly name of localhost.
    let getCertficate() =
        let store = new X509Store(StoreName.My, StoreLocation.LocalMachine)
        store.Open(OpenFlags.ReadOnly)
        let certs =
            store.Certificates.Find(
                findType = X509FindType.FindBySubjectName,
                findValue = Dns.GetHostName(),
                validOnly = true)
        seq {
        for c in certs do if c.FriendlyName = "localhost" then yield Some(c)
        yield None}
        |> Seq.head

    let handleServerRequestSecure (stream: NetworkStream) = 
        async {
            let cert = getCertficate()
            if cert.IsNone then printfn "No cert"; return ()
            let sslStream = new SslStream(innerStream = stream, leaveInnerStreamOpen = true)
            try
                sslStream.AuthenticateAsServer(
                    serverCertificate = cert.Value,
                    clientCertificateRequired = false,
                    enabledSslProtocols = SslProtocols.Default,
                    checkCertificateRevocation = false)
            with _ -> printfn "Can't authenticate"; return()
            
            printfn "IsAuthenticated: %A" sslStream.IsAuthenticated
            if sslStream.IsAuthenticated then
                // In this example only the server is authenticated.
                printfn "IsEncrypted: %A" sslStream.IsEncrypted
                printfn "IsSigned: %A" sslStream.IsSigned

                // Indicates whether the current side of the connection 
                // is authenticated as a server.
                printfn "IsServer: %A" sslStream.IsServer

            return! handleServerRequest stream
        }

    let server = AsyncTcpServerSecure(addr, port, handleServerRequestSecure)

    member x.Start() = server.Start()
