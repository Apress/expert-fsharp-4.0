open System.IO
let watcher = new FileSystemWatcher(__SOURCE_DIRECTORY__, EnableRaisingEvents = true)
watcher.Changed.Add(fun args -> printfn "File %s was changed!" args.Name)
//val watcher : System.IO.FileSystemWatcher = System.IO.FileSystemWatcher
//val it : unit = ()
//File f3ovfhv3.xe4~ was changed!
//File Script.fsx~RF96906f.TMP was changed!

watcher.Dispose()
//val it : unit = ()

open System
open System.Windows.Forms

type RandomTicker(approxInterval) =
    let timer = new Timer()
    let rnd = new System.Random(99)
    let tickEvent = new Event<int> ()

    let chooseInterval() : int =
        approxInterval + approxInterval / 4 - rnd.Next(approxInterval / 2)

    do timer.Interval <- chooseInterval()

    do timer.Tick.Add(fun args ->
        let interval = chooseInterval()
        tickEvent.Trigger interval;
        timer.Interval <- interval)

    member x.RandomTick = tickEvent.Publish
    member x.Start() = timer.Start()
    member x.Stop() = timer.Stop()
    interface IDisposable with
        member x.Dispose() = timer.Dispose()
//type RandomTicker =
//  class
//    interface System.IDisposable
//    new : approxInterval:int -> RandomTicker
//    member Start : unit -> unit
//    member Stop : unit -> unit
//    member RandomTick : IEvent<int>
//  end

let rt = new RandomTicker(1000)
rt.RandomTick.Add(fun nextInterval -> printfn "Tick, next = %A" nextInterval)
rt.Start()
//val rt : RandomTicker
//val it : unit = ()
//
//Tick, next = 818
//Tick, next = 767
//Tick, next = 906
//Tick, next = 799
//Tick, next = 1213
//Tick, next = 1119
//Tick, next = 1196
//Tick, next = 1138
//...

rt.Stop()
//val it : unit = ()

open System.Windows.Forms
let form = new Form(Text = "Mouse Move Form", Visible = true, TopMost = true)
form.MouseMove.Add(fun args -> printfn "Mouse, (X, Y) = (%A, %A)" args.X args.Y)

form.MouseMove
    |> Event.filter (fun args -> args.X > 100)
    |> Event.add (fun args -> printfn "Mouse, (X, Y) = (%A, %A)" args.X args.Y)

//val form : System.Windows.Forms.Form =
//  System.Windows.Forms.Form, Text: Mouse Move Form
//val it : unit = ()
//
//Mouse, (X, Y) = (27, 2)
//Mouse, (X, Y) = (26, 3)
//Mouse, (X, Y) = (26, 4)
//Mouse, (X, Y) = (24, 6)
//Mouse, (X, Y) = (23, 7)
//Mouse, (X, Y) = (23, 8)
//Mouse, (X, Y) = (20, 10)
//Mouse, (X, Y) = (19, 11)
//Mouse, (X, Y) = (18, 14)
//Mouse, (X, Y) = (16, 15)
//Mouse, (X, Y) = (15, 16)
//Mouse, (X, Y) = (14, 18)
//Mouse, (X, Y) = (11, 20)
//Mouse, (X, Y) = (10, 22)
//Mouse, (X, Y) = (8, 23)
//Mouse, (X, Y) = (6, 26)
//Mouse, (X, Y) = (3, 28)
//Mouse, (X, Y) = (2, 30)

Event.add
Event.choose
Event.filter
Event.map
Event.merge
Event.pairwise
Event.partition
Event.scan
Event.split
// NOTE: I get the following error when trying to get F# interactive to give me the types.
// error FS0335: Could not resolve the ambiguity in the use of a generic construct with a 'delegate' constraint at or near this position

let rt = new RandomTicker(1000)
rt.Start()
rt.RandomTick |> Observable.add (fun evArgs -> printfn "Tick")
//val rt : RandomTicker
//val it : unit = ()
//
//Tick
//Tick
//Tick
//Tick
//Tick

rt.Stop()
//val it : unit = ()

open System.Net
open System.IO

let museums = 
    [ "MOMA", "http://moma.org/";
      "British Museum", "http://www.thebritishmuseum.ac.uk/";
      "Prado", "http://www.museodelprado.es/" ]

let fetchAsync(nm, url : string) = 
    async {
        printfn "Creating request for %s..." nm
        let req = WebRequest.Create(url)

        let! resp = req.AsyncGetResponse()

        printfn "Getting response stream for %s..." nm
        let stream = resp.GetResponseStream()

        printfn "Reading response for %s..." nm
        let reader = new StreamReader(stream)
        let html = reader.ReadToEnd()

        printfn "Read %d characters for %s..." html.Length nm
    }

Async.Parallel [for nm, url in museums -> fetchAsync(nm, url)]
    |> Async.Ignore
    |> Async.RunSynchronously

//Creating request for British Museum...
//Creating request for Prado...
//Creating request for MOMA...
//Getting response stream for MOMA...
//Reading response for MOMA...
//Read 43570 characters for MOMA...
//Getting response stream for British Museum...
//Reading response for British Museum...
//Read 42217 characters for British Museum...
//Getting response stream for Prado...
//Reading response for Prado...
//Read 22406 characters for Prado...
//
//val museums : (string * string) list =
//  [("MOMA", "http://moma.org/");
//   ("British Museum", "http://www.thebritishmuseum.ac.uk/");
//   ("Prado", "http://www.museodelprado.es/")]
//val fetchAsync : nm:string * url:string -> Async<unit>
//val it : unit = ()

let tprintfn fmt =
    printf "[Thread %d]" System.Threading.Thread.CurrentThread.ManagedThreadId;
    printfn fmt

let fetchAsync'(nm, url : string) = 
    async {
        tprintfn "Creating request for %s..." nm
        let req = WebRequest.Create(url)

        let! resp = req.AsyncGetResponse()

        tprintfn "Getting response stream for %s..." nm
        let stream = resp.GetResponseStream()

        tprintfn "Reading response for %s..." nm
        let reader = new StreamReader(stream)
        let html = reader.ReadToEnd()

        tprintfn "Read %d characters for %s..." html.Length nm
    }

Async.Parallel [for nm, url in museums -> fetchAsync'(nm, url)]
    |> Async.Ignore
    |> Async.RunSynchronously

//[Thread 5][Thread 4]Creating request for Prado...
//Creating request for British Museum...
//[Thread 6]Creating request for MOMA...
//[Thread 20]Getting response stream for British Museum...
//[Thread 20]Reading response for British Museum...
//[Thread 20]Read 42216 characters for British Museum...
//[Thread 22]Getting response stream for MOMA...
//[Thread 22]Reading response for MOMA...
//[Thread 22]Read 44851 characters for MOMA...
//[Thread 21]Getting response stream for Prado...
//[Thread 21]Reading response for Prado...
//[Thread 21]Read 22406 characters for Prado...
//
//val tprintfn : fmt:Printf.TextWriterFormat<'a> -> 'a
//val fetchAsync' : nm:string * url:string -> Async<unit>
//val it : unit = ()

let fetchAsync''(nm, url : string) = async.Delay(fun () ->
    printfn "Creating request for %s..." nm
    let req = WebRequest.Create(url)
    async.Bind(req.AsyncGetResponse(), (fun resp ->
        printfn "Reading response for %s..." nm
        let stream = resp.GetResponseStream() 

        printfn "Reading response for %s..." nm
        let reader = new StreamReader(stream)  
        async.Bind(async.Return(reader.ReadToEnd()), (fun html ->
            printfn "Read %d characters for %s..." html.Length nm
            async.Return html)))))

Async.Parallel [for nm, url in museums -> fetchAsync''(nm, url)]
    |> Async.Ignore
    |> Async.RunSynchronously

#I "packages/FSharpx.Async/lib/net40"
#r "FSharpx.Async.dll"
open System.IO
open System.Net
open FSharp.Control.WebExtensions
open FSharpx.Control

//AsyncGetResponse: This construct is deprecated. The extension method now resides in the 'WebExtensions' module in the F# core library. Please add 'open Microsoft.FSharp.Control.WebExtensions' to access this method

//"AsyncReadToEnd() extension method of StreamReader is part of FSharpPowerPack now."
//"AsyncReadToEnd extension method has disappeared from latest PowerPack releases"
//SOURCE: http://stackoverflow.com/questions/8695493/the-field-constructor-or-member-asyncreadtoend-is-not-defined-errorI think that AsyncReadToEnd that just synchronously calls ReadToEnd on a separate thread is wrong.

//The F# PowerPack also contains a type AsyncStreamReader that contains proper asynchronous implementation of stream reading.
//SOURCE: http://stackoverflow.com/questions/7925318/expensive-asynchronous-reading-of-response-stream/7925440#7925440

let moma : Async<string> = async { 
        let req = WebRequest.Create("http://moma.org/")
        let! resp = req.AsyncGetResponse()
        let stream = resp.GetResponseStream()
        let reader = new StreamReader(stream)
        let! html = reader.AsyncReadToEnd()
        return html
    }

let moma' : Async<string> = async.Delay(fun () ->
    let req = WebRequest.Create("http://moma.org/")
    async.Bind(req.AsyncGetResponse(), (fun resp ->
        let stream = resp.GetResponseStream() 
        let reader = new StreamReader(stream)  
        async.Bind(reader.AsyncReadToEnd(), (fun html ->
            async.Return html)))))
//val moma : Async<string>
//val moma' : Async<string>

Async.RunSynchronously moma
Async.RunSynchronously moma'
//val it : string =
//  "<!DOCTYPE html>
//<html lang='en'>
//<head>
//...
//<title>MoMA | Museum of Modern Art</title>
//...
//</html>

open System.IO
let numImages = 200
let size = 512
let numPixels = size * size

let makeImageFiles () =
    printfn "making %d %dx%d images... " numImages size size
    let pixels = Array.init numPixels (fun i -> byte i)
    for i = 1 to numImages  do
        System.IO.File.WriteAllBytes(sprintf "Image%d.tmp" i, pixels)
    printfn "done."

let processImageRepeats = 20

let transformImage (pixels, imageNum) =
    printfn "transformImage %d" imageNum;
    // Perform a CPU-intensive operation on the image.
    for i in 1 .. processImageRepeats do 
        pixels |> Array.map (fun b -> b + 1uy) |> ignore
    pixels |> Array.map (fun b -> b + 1uy)

let processImageSync i =
    use inStream =  File.OpenRead(sprintf "Image%d.tmp" i)
    let pixels = Array.zeroCreate numPixels
    let nPixels = inStream.Read(pixels,0,numPixels);
    let pixels' = transformImage(pixels,i)
    use outStream =  File.OpenWrite(sprintf "Image%d.done" i)
    outStream.Write(pixels',0,numPixels)

let processImagesSync () =
    printfn "processImagesSync...";
    for i in 1 .. numImages do
        processImageSync(i)

let processImageAsync i =
    async { 
        use inStream = File.OpenRead(sprintf "Image%d.tmp" i)
        let! pixels = inStream.AsyncRead(numPixels)
        let  pixels' = transformImage(pixels, i)
        use outStream = File.OpenWrite(sprintf "Image%d.done" i)
        do! outStream.AsyncWrite(pixels')
    }

let processImagesAsync() =
    printfn "processImagesAsync...";
    let tasks = [for i in 1 .. numImages -> processImageAsync(i)]
    Async.RunSynchronously (Async.Parallel tasks) |> ignore
    printfn "processImagesAsync finished!"

System.Environment.CurrentDirectory <- Path.Combine(__SOURCE_DIRECTORY__, "temp")
makeImageFiles()

//making 200 512x512 images... 
//done.
//
//val numImages : int = 200
//val size : int = 512
//val numPixels : int = 262144
//val makeImageFiles : unit -> unit
//val processImageRepeats : int = 20
//val transformImage : pixels:byte [] * imageNum:int32 -> byte []
//val processImageSync : i:int32 -> unit
//val processImagesSync : unit -> unit
//val processImageAsync : i:int32 -> Async<unit>
//val processImagesAsync : unit -> unit
//val it : unit = ()

#time "on"

//TODO: Try GC.Collect() to force disposal.
processImagesSync()
processImagesAsync()

//--> Timing now on
//
//processImagesSync...
//...
//transformImage 198
//transformImage 199
//transformImage 200
//Real: 00:00:02.973, CPU: 00:00:03.681, GC gen0: 29, gen1: 29, gen2: 29
//val it : unit = ()

//processImagesSync...
//...
//transformImage 159
//transformImage 160
//transformImage 161
//System.IO.IOException: The process cannot access the file 'C:\...\Image161.done' because it is being used by another process.
//   at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)
//   at System.IO.FileStream.Init(String path, FileMode mode, FileAccess access, Int32 rights, Boolean useRights, FileShare share, Int32 bufferSize, FileOptions options, SECURITY_ATTRIBUTES secAttrs, String msgPath, Boolean bFromProxy, Boolean useLongPath, Boolean checkHost)
//   at System.IO.FileStream..ctor(String path, FileMode mode, FileAccess access, FileShare share)
//   at FSI_0002.processImageSync(Int32 i) in C:\...\Script.fsx:line 301
//   at FSI_0002.processImagesSync() in C:\...\Script.fsx:line 307
//   at <StartupCode$FSI_0004>.$FSI_0004.main@() in C:\...\Script.fsx:line 344
//Stopped due to error

//processImagesAsync...
//...
//transformImage 199
//transformImage 198
//transformImage 200
//System.IO.IOException: The process cannot access the file 'C:\...\Image173.done' because it is being used by another process.
//   at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)
//   at System.IO.FileStream.Init(String path, FileMode mode, FileAccess access, Int32 rights, Boolean useRights, FileShare share, Int32 bufferSize, FileOptions options, SECURITY_ATTRIBUTES secAttrs, String msgPath, Boolean bFromProxy, Boolean useLongPath, Boolean checkHost)
//   at System.IO.FileStream..ctor(String path, FileMode mode, FileAccess access, FileShare share)
//   at FSI_0003.processImageAsync@332-3.Invoke(Byte[] _arg2) in C:\...\Script.fsx:line 333
//   at Microsoft.FSharp.Control.AsyncBuilderImpl.args@835-1.Invoke(a a)
//--- End of stack trace from previous location where exception was thrown ---
//   at Microsoft.FSharp.Control.AsyncBuilderImpl.commit[a](Result`1 res)
//   at Microsoft.FSharp.Control.CancellationTokenOps.RunSynchronously[a](CancellationToken token, FSharpAsync`1 computation, FSharpOption`1 timeout)
//   at Microsoft.FSharp.Control.FSharpAsync.RunSynchronously[T](FSharpAsync`1 computation, FSharpOption`1 timeout, FSharpOption`1 cancellationToken)
//   at FSI_0003.processImagesAsync() in C:\...\Script.fsx:line 340
//   at <StartupCode$FSI_0006>.$FSI_0006.main@() in C:\...\Script.fsx:line 346
//Stopped due to error

//processImagesAsync...
//...
//transformImage 193
//transformImage 192
//transformImage 191
//processImagesAsync finished!
//Real: 00:00:00.874, CPU: 00:00:02.418, GC gen0: 9, gen1: 8, gen2: 8
//val it : unit = ()

#time "off"

let failingAsync = async { do failwith "fail" }
//val failingAsync : Async<unit>

Async.RunSynchronously failingAsync
//System.Exception: fail
//   at FSI_0012.failingAsync@403.Invoke(Unit unitVar) in C:\...\Script.fsx:line 403
//   at Microsoft.FSharp.Control.AsyncBuilderImpl.callA@851.Invoke(AsyncParams`1 args)
// --- End of stack trace from previous location where exception was thrown ---
//   at Microsoft.FSharp.Control.AsyncBuilderImpl.commit[a](Result`1 res)
//   at Microsoft.FSharp.Control.CancellationTokenOps.RunSynchronously[a](CancellationToken token, FSharpAsync`1 computation, FSharpOption`1 timeout)
//   at Microsoft.FSharp.Control.FSharpAsync.RunSynchronously[T](FSharpAsync`1 computation, FSharpOption`1 timeout, FSharpOption`1 cancellationToken)
//   at <StartupCode$FSI_0013>.$FSI_0013.main@() in C:\...\Script.fsx:line 406
//Stopped due to error

let failingAsyncs = [async {do failwith "fail A"}; async {do failwith "fail B"}]
//val failingAsyncs : Async<unit> list =
//  [Microsoft.FSharp.Control.FSharpAsync`1[Microsoft.FSharp.Core.Unit];
//   Microsoft.FSharp.Control.FSharpAsync`1[Microsoft.FSharp.Core.Unit]]

Async.RunSynchronously (Async.Parallel failingAsyncs)
//System.Exception: fail A
//   at FSI_0008.failingAsyncs@420-4.Invoke(Unit unitVar) in C:\...\Script.fsx:line 420
//   at Microsoft.FSharp.Control.AsyncBuilderImpl.callA@851.Invoke(AsyncParams`1 args)
//--- End of stack trace from previous location where exception was thrown ---
//   at Microsoft.FSharp.Control.AsyncBuilderImpl.commit[a](Result`1 res)
//   at Microsoft.FSharp.Control.CancellationTokenOps.RunSynchronously[a](CancellationToken token, FSharpAsync`1 computation, FSharpOption`1 timeout)
//   at Microsoft.FSharp.Control.FSharpAsync.RunSynchronously[T](FSharpAsync`1 computation, FSharpOption`1 timeout, FSharpOption`1 cancellationToken)
//   at <StartupCode$FSI_0009>.$FSI_0009.main@() in C:\...\Script.fsx:line 422
//Stopped due to error

Async.RunSynchronously (Async.Parallel failingAsyncs)
//System.Exception: fail B
//    at FSI_0008.failingAsyncs@420-5.Invoke(Unit unitVar) in C:\...\Script.fsx:line 420
//   at Microsoft.FSharp.Control.AsyncBuilderImpl.callA@851.Invoke(AsyncParams`1 args)
//--- End of stack trace from previous location where exception was thrown ---
//   at Microsoft.FSharp.Control.AsyncBuilderImpl.commit[a](Result`1 res)
//   at Microsoft.FSharp.Control.CancellationTokenOps.RunSynchronously[a](CancellationToken token, FSharpAsync`1 computation, FSharpOption`1 timeout)
//   at Microsoft.FSharp.Control.FSharpAsync.RunSynchronously[T](FSharpAsync`1 computation, FSharpOption`1 timeout, FSharpOption`1 cancellationToken)
//   at <StartupCode$FSI_0011>.$FSI_0011.main@() in C:\...\Script.fsx:line 434
//Stopped due to error

Async.RunSynchronously (Async.Catch failingAsync)
//val it : Choice<unit,exn> =
//  Choice2Of2
//    System.Exception: fail
//   at FSI_0012.failingAsync@403.Invoke(Unit unitVar) in C:\...\Script.fsx:line 403
//   at Microsoft.FSharp.Control.AsyncBuilderImpl.callA@851.Invoke(AsyncParams`1 args)
//      {Data = dict [];
//       HResult = -2146233088;
//       HelpLink = null;
//       InnerException = null;
//       Message = "fail";
//       Source = "FSI-ASSEMBLY";
//       StackTrace = "   at FSI_0012.failingAsync@403.Invoke(Unit unitVar) in C:\...\Script.fsx:line 403
//   at Microsoft.FSharp.Control.AsyncBuilderImpl.callA@851.Invoke(AsyncParams`1 args)";
//       TargetSite = Microsoft.FSharp.Control.FSharpAsync`1[Microsoft.FSharp.Core.Unit] Invoke(Microsoft.FSharp.Core.Unit);}

type Agent<'T> = MailboxProcessor<'T>

let counter =
    new Agent<_>(fun inbox ->
        let rec loop n =
            async {printfn "n = %d, waiting..." n
                   let! msg = inbox.Receive()
                   return! loop (n + msg)}
        loop 0)
//type Agent<'T> = MailboxProcessor<'T>
//val counter : Agent<int>

counter.Start()
//n = 0, waiting...

counter.Post(1)
//n = 1, waiting...

counter.Post(2)
//n = 3, waiting...

counter.Post(1)
//n = 4, waiting...


/// The internal type of messages for the agent
type internal msg = Increment of int | Fetch of AsyncReplyChannel<int> | Stop

type CountingAgent() =
    let counter = MailboxProcessor.Start(fun inbox ->
         // The states of the message-processing state machine...
         let rec loop n =
             async {let! msg = inbox.Receive()
                    match msg with
                    | Increment m ->
                        // increment and continue...
                        return! loop(n + m)
                    | Stop ->
                        // exit
                        return ()
                    | Fetch replyChannel  ->
                        // post response to reply channel and continue
                        do replyChannel.Reply n
                        return! loop n}

         // The initial state of the message-processing state machine...
         loop(0))

    member a.Increment(n) = counter.Post(Increment n)
    member a.Stop() = counter.Post Stop
    member a.Fetch() = counter.PostAndReply(fun replyChannel -> Fetch replyChannel)

let counter = new CountingAgent()
//type internal msg =
//  | Increment of int
//  | Fetch of AsyncReplyChannel<int>
//  | Stop
//type CountingAgent =
//  class
//    new : unit -> CountingAgent
//    member Fetch : unit -> int
//    member Increment : n:int -> unit
//    member Stop : unit -> unit
//  end
//val counter : CountingAgent

counter.Increment(1)
//val it : unit = ()

counter.Fetch()
//val it : int = 1

counter.Increment(2)
//val it : unit = ()

counter.Fetch()
//val it : int = 3

counter.Stop()
//val it : unit = ()


type Message =
    | Message1
    | Message2 of int
    | Message3 of string

let agent =
    MailboxProcessor.Start(fun inbox ->
        let rec loop() =
            inbox.Scan(function
                | Message1 ->
                   Some (async {do printfn "message 1!"
                                return! loop()})
                | Message2 n ->
                   Some (async {do printfn "message 2!"
                                return! loop()})
                | Message3 _ ->
                   None)
        loop())

agent.Post(Message1);
agent.Post(Message2(100))
agent.Post(Message3("abc"))
agent.Post(Message2(100))
agent.CurrentQueueLength

//message 1!
//message 2!
//message 2!
//
//type Message =
//  | Message1
//  | Message2 of int
//  | Message3 of string
//val agent : MailboxProcessor<Message>
//val it : int = 4

open System.Collections.Generic
open System.Net
open System.IO
open System.Threading
open System.Text.RegularExpressions

let limit = 50
let linkPat = "href=\s*\"[^\"h]*(http://[^&\"]*)\""
let getLinks (txt:string) =
    [ for m in Regex.Matches(txt,linkPat)  -> m.Groups.Item(1).Value ]

// A type that helps limit the number of active web requests
type RequestGate(n:int) =
    let semaphore = new Semaphore(initialCount=n,maximumCount=n)
    member x.AsyncAcquire(?timeout) =
        async { 
            let! ok = Async.AwaitWaitHandle(semaphore,
                                            ?millisecondsTimeout=timeout)
            if ok then
               return
                 { new System.IDisposable with
                     member x.Dispose() =
                         semaphore.Release() |> ignore }
            else
               return! failwith "couldn't acquire a semaphore" 
        }

// Gate the number of active web requests
let webRequestGate = RequestGate(5)

// Fetch the URL, and post the results to the urlCollector.
let collectLinks (url:string) =
    async { 
        // An Async web request with a global gate
        let! html =
            async { 
                // Acquire an entry in the webRequestGate. Release
                // it when 'holder' goes out of scope
                use! holder = webRequestGate.AsyncAcquire()

                let req = WebRequest.Create(url,Timeout=5)

                // Wait for the WebResponse
                use! response = req.AsyncGetResponse()

                // Get the response stream
                use reader = new StreamReader(response.GetResponseStream())

                // Read the response stream (note: a synchronous read)
                return reader.ReadToEnd() 
            }

        // Compute the links, synchronously
        let links = getLinks html

        // Report, synchronously
        printfn "finished reading %s, got %d links" url (List.length links)

        // We're done
        return links 
    }

/// 'urlCollector' is a single agent that receives URLs as messages. It creates new
/// asynchronous tasks that post messages back to this object.
let urlCollector =
    MailboxProcessor.Start(fun self ->

        // This is the main state of the urlCollector
        let rec waitForUrl (visited : Set<string>) =

           async { 
               // Check the limit
               if visited.Count < limit then

                   // Wait for a URL...
                   let! url = self.Receive()
                   if not (visited.Contains(url)) then
                       // Start off a new task for the new url. Each collects
                       // links and posts them back to the urlCollector.
                       do! Async.StartChild
                               (async { let! links = collectLinks url
                                        for link in links do
                                           self.Post link }) |> Async.Ignore

                   // Recurse into the waiting state
                   return! waitForUrl(visited.Add(url)) 
            }

        // This is the initial state.
        waitForUrl(Set.empty))

urlCollector.Post "http://news.google.com"

//val limit : int = 50
//val linkPat : string = "href=\s*"[^"h]*(http://[^&"]*)""
//val getLinks : txt:string -> string list
//type RequestGate =
//  class
//    new : n:int -> RequestGate
//    member AsyncAcquire : ?timeout:int -> Async<System.IDisposable>
//  end
//val webRequestGate : RequestGate
//val collectLinks : url:string -> Async<string list>
//val urlCollector : MailboxProcessor<string>
//val it : unit = ()
//
//finished reading http://news.google.com, got 181 links
//finished reading http://www.gstatic.com/news-static/img/favicon.ico, got 0 links
//finished reading http://www.cbc.ca/news/politics/military-suicide-afghanistan-mental-1.3312860, got 99 links
//finished reading http://www.google.ca/preferences?hl=en, got 3 links
//finished reading http://www.google.ca/preferences, got 4 links
//finished reading http://www.cbc.ca/news/politics/parliamentary-budget-officer-trudeau-plans-1.3312757, got 100 links
//finished reading http://www.google.ca/finance?tab=ne, got 12 links
//finished reading http://www.metronews.ca/news/canada/2015/11/10/suicide-in-military-a-concern-those-at-risk-should-seek-help-says-vance.html, got 69 links
//finished reading http://www.huffingtonpost.ca/2015/11/10/pbo-economic-outlook-deficit-housing_n_8521938.html, got 336 links
//finished reading http://globalnews.ca/news/2331765/army-suicide-rate-3-times-higher-than-other-branches-of-canadian-military/, got 103 links
//finished reading http://www.blogger.com/?tab=nj, got 1 links
//finished reading http://ckom.com/article/288763/trudeau-government-facing-bigger-baseline-deficits-amid-weaker-economy-pbo, got 15 links
//finished reading http://indianexpress.com/article/world/world-news/russia-to-deploy-new-weapons-to-counter-us-missile-shield/, got 111 links
//finished reading http://www.reuters.com/article/2015/11/10/us-mideast-crisis-syria-draft-idUSKCN0SZ2F720151110, got 103 links
//finished reading http://www.denverpost.com/opinion/ci_29099212/dobbs-why-putin-doesnt-want-plane-crash-have, got 56 links
//finished reading http://www.youtube.com/?tab=n1, got 0 links
//finished reading http://www.bustle.com/articles/122910-whos-returning-to-star-wars-the-force-awakens-features-lots-of-familiar-faces, got 1 links
//finished reading http://www.mapleridgenews.com/community/344927012.html, got 52 links
//finished reading http://www.dailymail.co.uk/wires/ap/article-3312876/Russia-calls-new-Syrian-constitution-18-months.html, got 22 links
//finished reading http://www.stuff.co.nz/entertainment/film/73920909/star-wars-rebel-alliance-freedom-fighters-or-terrorists, got 52 links
//finished reading http://www.cbc.ca/news/canada/toronto/toronto-police-college-park-assaults-arrest-1.3312980, got 89 links
//finished reading http://www.torontosun.com/2015/11/10/cineplex-expects-huge-profits-from-the-force-of-star-wars-other-big-hits, got 305 links
//finished reading http://www.reuters.com/article/2015/11/10/us-mideast-crisis-syria-idUSKCN0SZ15E20151110, got 103 links
//finished reading http://www.latimes.com/world/middleeast/la-fg-syria-shelling-20151110-story.html, got 13 links
//finished reading http://www.torontosun.com/2015/11/10/lest-we-forget-why-we-are-free, got 310 links
//finished reading http://www.huffingtonpost.com/entry/netanyahu-center-for-american-progress_564283a8e4b060377346d5a1, got 54 links
//finished reading http://www.jpost.com/Israel-News/Study-84-percent-of-Israelis-say-they-wouldnt-emigrate-432638, got 22 links
//finished reading http://www.telegraph.co.uk/news/worldnews/middleeast/syria/11987256/Russia-and-Iran-backed-offensive-helps-regime-break-Isils-two-year-siege-on-Syrian-airbase.html, got 99 links
//finished reading http://www.bloomberg.com/politics/articles/2015-11-11/netanyahu-works-to-assuage-democrats-after-his-visit-with-obama, got 71 links
//finished reading http://timesofindia.indiatimes.com/life-style/home-garden/Wide-range-of-ornate-diyas-light-up-homes-this-Diwali/articleshow/49687329.cms, got 89 links
//finished reading http://www.business-standard.com/article/news-ians/trinidad-celebrates-diwali-in-fine-style-115111100061_1.html, got 28 links
//finished reading http://www.ibtimes.com/what-diwali-2015-festival-light-celebrates-good-defeating-evil-2173793, got 66 links
//finished reading http://www.torontosun.com/2015/11/10/arrest-made-in-college-park-assault-rampage, got 307 links
//finished reading http://www.cbc.ca/news/technology/betamax-death-sony-1.3312556, got 91 links
//finished reading http://www.thestar.com/business/2015/11/10/40-years-on-sony-finally-kills-betamax.html, got 171 links
//finished reading http://tribune.com.pk/story/988986/festival-of-lights-diwali-brings-citizens-together/, got 105 links
//finished reading http://www.ctvnews.ca/sci-tech/sony-to-stop-selling-betamax-tapes-in-2016-yes-they-still-exist-1.2652002, got 270 links
//finished reading http://www.theverge.com/2015/11/9/9703004/sony-is-finally-killing-betamax, got 199 links
//finished reading http://toronto.ctvnews.ca/suspect-in-custody-after-violent-assaults-in-college-park-1.2651908, got 202 links
//finished reading http://www.fortmcmurraytoday.com/2015/11/09/notley-focusing-on-cleaner-pipeline-plan, got 130 links
//finished reading http://www.theprovince.com/abusive+pimp+handed+year+sentence+luring+vulnerable+teens+into+prostitution/11507780/story.html, got 157 links
//finished reading http://www.cknw.com/2015/11/10/23-years-sentence-for-first-person-in-b-c-ever-charged-with-human-trafficking/, got 74 links
//finished reading http://calgaryherald.com/news/national/alberta-waiting-on-ottawa-before-committing-to-syrian-refugees, got 279 links
//finished reading http://www.ctvnews.ca/canada/b-c-man-who-trafficked-teen-girls-sentenced-to-23-years-in-prison-1.2651729, got 272 links
//finished reading http://www.castanet.net/news/Kamloops/151555/Medical-issue-triggers-crash, got 84 links
//finished reading http://www.kamloopsthisweek.com/walmart-security-guard-stabbed-19-year-old-man-in-custody/, got 177 links
//finished reading http://www.ctvnews.ca/canada/wounded-afghan-soldier-trevor-greene-receives-honorary-uvic-degree-1.2652110, got 273 links
//finished reading http://globalnews.ca/news/2331292/suspected-shoplifter-stabs-store-employee-in-kamloops/, got 81 links
//finished reading http://www.timescolonist.com/wounded-soldier-trevor-greene-receives-honorary-uvic-degree-1.2107892, got 57 links
//finished reading http://aranews.net/2015/11/syrian-regime-troops-break-isis-siege-of-major-air-base-in-aleppo/, got 268 links

open System.Threading
open System

// Initialize an array by a parallel init using all available processors
// Note, this primitive doesn't support cancellation.
let parallelArrayInit n f = 
   let currentLine = ref -1
   let res = Array.zeroCreate n
   let rec loop () = 
       let y = Interlocked.Increment(currentLine)
       if y < n then res.[y] <- f y; loop()

   // Start just the right number of tasks, one for each physical CPU
   Async.Parallel [for i in 1 .. Environment.ProcessorCount -> async {do loop()}]
      |> Async.Ignore 
      |> Async.RunSynchronously

   res

let rec fib x = if x < 2 then 1 else fib (x - 1) + fib (x - 2)

parallelArrayInit 25 (fun x -> fib x)
//val parallelArrayInit : n:int -> f:(int -> 'a) -> 'a []
//val fib : x:int -> int
//val it : int [] =
//  [|1; 1; 2; 3; 5; 8; 13; 21; 34; 55; 89; 144; 233; 377; 610; 987; 1597; 2584;
//    4181; 6765; 10946; 17711; 28657; 46368; 75025|]

open System.Threading
let t = new Thread(ThreadStart(fun _ ->
                printfn "Thread %d: Hello" Thread.CurrentThread.ManagedThreadId));
t.Start();
printfn "Thread %d: Waiting!" Thread.CurrentThread.ManagedThreadId
t.Join();
printfn "Done!"

//val t : Thread
//Thread 1: Waiting!
//Thread 10: Hello
//Done!

open System.Threading
open System.Threading.Tasks

let doSome1Thing() = 
    printfn "doing some 1 thing ..."
    System.Threading.Thread.Sleep 100
    printfn "... done some 1 thing."

let doSome2Thing(_ : CancellationToken) = 
    printfn "doing some 2 thing ..."
    System.Threading.Thread.Sleep 100
    printfn "... done some 2 thing."

let doSome3Thing(ct : CancellationToken) = 
    printfn "doing some 3 thing ..."
    ct.ThrowIfCancellationRequested() 
    System.Threading.Thread.Sleep 100
    ct.ThrowIfCancellationRequested() 
    printfn "... done some 3 thing."

let cts = new CancellationTokenSource()

let task1 = Task.Run (fun () -> doSome1Thing())
let task2 = Task.Run (fun () -> doSome2Thing(cts.Token), cts.Token)
let task3 = Task.Run (fun () -> doSome3Thing(cts.Token), cts.Token)

cts.Cancel()
//doing some 1 thing ...
//doing some 2 thing ...
//doing some 3 thing ...
//
//val doSome1Thing : unit -> unit
//val doSome2Thing : System.Threading.CancellationToken -> unit
//val doSome3Thing : ct:System.Threading.CancellationToken -> unit
//val cts : System.Threading.CancellationTokenSource
//val task1 : System.Threading.Tasks.Task
//val task2 :
//  System.Threading.Tasks.Task<unit * System.Threading.CancellationToken>
//val task3 :
//  System.Threading.Tasks.Task<unit * System.Threading.CancellationToken>
//val it : unit = ()
//
//... done some 1 thing.
//... done some 2 thing.



type MutablePair<'T, 'U>(x : 'T, y : 'U) =
    let mutable currentX = x
    let mutable currentY = y
    member p.Value = (currentX, currentY)
    member p.Update(x, y) =
        // Race condition: This pair of updates is not atomic
        currentX <- x
        currentY <- y

let p = new MutablePair<_, _>(1, 2)
do Async.Start (async {do (while true do p.Update(10, 10))})
do Async.Start (async {do (while true do p.Update(20, 20))})

open System.Threading
let lock (lockobj : obj) f  =
    Monitor.Enter lockobj
    try
        f()
    finally
        Monitor.Exit lockobj

do Async.Start (async {do (while true do lock p (fun () -> p.Update(10, 10)))})
do Async.Start (async {do (while true do lock p (fun () -> p.Update(20, 20)))})
//type MutablePair<'T,'U> =
//  class
//    new : x:'T * y:'U -> MutablePair<'T,'U>
//    member Update : x:'T * y:'U -> unit
//    member Value : 'T * 'U
//  end
//val p : MutablePair<int,int>
//val lock : lockobj:obj -> f:(unit -> 'a) -> 'a
//val it : unit = ()

open System.Threading

let readLock (rwlock : ReaderWriterLock) f  =
  rwlock.AcquireReaderLock(Timeout.Infinite)
  try
      f()
  finally
      rwlock.ReleaseReaderLock()

let writeLock (rwlock : ReaderWriterLock) f  =
  rwlock.AcquireWriterLock(Timeout.Infinite)
  try
      f()
      Thread.MemoryBarrier()
  finally
      rwlock.ReleaseWriterLock()

type MutablePair<'T, 'U>(x : 'T, y : 'U) =
    let mutable currentX = x
    let mutable currentY = y
    let rwlock = new ReaderWriterLock()
    member p.Value =
        readLock rwlock (fun () ->
            (currentX, currentY))
    member p.Update(x, y) =
        writeLock rwlock (fun () ->
            currentX <- x
            currentY <- y)
//val readLock :
//  rwlock:System.Threading.ReaderWriterLock -> f:(unit -> 'a) -> 'a
//val writeLock :
//  rwlock:System.Threading.ReaderWriterLock -> f:(unit -> unit) -> unit
//type MutablePair<'T,'U> =
//  class
//    new : x:'T * y:'U -> MutablePair<'T,'U>
//    member Update : x:'T * y:'U -> unit
//    member Value : 'T * 'U
//  end

// NOTE: For let () = expr of the async workflow expressions.
let () = ()
let () = 1 |> ignore
let 1 = 1
//warning FS0025: Incomplete pattern matches on this expression. For example, the value '0' may indicate a case not covered by the pattern(s).