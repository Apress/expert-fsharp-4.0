open System.Net
open System.Net.Sockets
open System.IO
open System.Text.RegularExpressions
open System.Text

/// A table of MIME content types.
let mimeTypes =
    dict [".html", "text/html";
          ".htm", "text/html";
          ".txt", "text/plain";
          ".gif", "image/gif";
          ".jpg", "image/jpeg";
          ".png", "image/png"]

/// Compute a MIME type from a file extension.
let getMimeType(ext) =
    if mimeTypes.ContainsKey(ext) then mimeTypes.[ext]
    else "binary/octet"

/// The pattern Regex1 uses a regular expression to match one element.
let (|Regex1|_|) (patt : string) (inp : string) =
    try Some(Regex.Match(inp, patt).Groups.Item(1).Captures.Item(0).Value)
    with _ -> None

/// The root for the data we serve
let root = @"c:\inetpub\wwwroot"

/// Handle a TCP connection for an HTTP GET. We use an asynchronous task in
/// case any future actions in handling a request need to be asynchronous.
let handleRequest(client : TcpClient) = async {
    use stream = client.GetStream()
    let out = new StreamWriter(stream)
    let headers (lines : seq<string>) =
        let printLine s = s |> fprintf out "%s\r\n"
        lines |> Seq.iter printLine
        // An empty line is required before content, if any.
        printLine ""
        out.Flush()
    let notFound () = headers ["HTTP/1.0 404 Not Found"]
    let inp = new StreamReader(stream)
    let request = inp.ReadLine()
    match request with
    | "GET / HTTP/1.0" | "GET / HTTP/1.1" ->
        // From the root, redirect to the start page.
        headers ["HTTP/1.0 302 Found"; "Location: http://localhost:8090/iisstart.htm"]
    | Regex1 "GET /(.*?) HTTP/1\\.[01]$" fileName ->
        let fname = Path.Combine(root, fileName)
        let mimeType = getMimeType(Path.GetExtension(fname))
        if not(File.Exists(fname)) then notFound()
        else
            let content = File.ReadAllBytes fname
            ["HTTP/1.0 200 OK";
            sprintf "Content-Length: %d" content.Length;
            sprintf "Content-Type: %s" mimeType]
            |> headers
            stream.Write(content, 0, content.Length)
    | _ -> notFound()}

/// The server as an asynchronous process. We handle requests sequentially.
let server = async { 
    let socket = new TcpListener(IPAddress.Parse("127.0.0.1"), 8090)
    socket.Start()
    while true do
        use client = socket.AcceptTcpClient()
        do! handleRequest client}

//Async.Start server;;
//val it : unit = ()
