// NOTE: The http function is from chapter 2.
open System.IO
open System.Net

let http (url : string) =
    let req = System.Net.WebRequest.Create(url)
    let resp = req.GetResponse()
    let stream = resp.GetResponseStream()
    let reader = new StreamReader(stream)
    let html = reader.ReadToEnd()
    resp.Close()
    html

let repeatFetch url n =
    for i = 1 to n do
        let html = http url
        printf "fetched <<< %s >>>\n" html 
    printfn "Done!"
//val http : url:string -> string
//val repeatFetch : url:string -> n:int -> unit

open System

let loopUntilSaturday() =
    while (DateTime.Now.DayOfWeek <> DayOfWeek.Saturday) do
        printfn "Still working!"

    printfn "Saturday at last!"
//val loopUntilSaturday : unit -> unit

loopUntilSaturday()
//"Still working!"
//"Still working!"
//"Still working!"
//"Still working!"
//...

for (b, pj) in [ ("Banana 1", false)
                 ("Banana 2", true) ] do
    if pj then printfn "%s is in pyjamas today!" b
//Banana 2 is in pyjamas today!
//val it : unit = ()

open System.Text.RegularExpressions

for m in Regex.Matches("All the Pretty Horses", "[a-zA-Z]+") do
    printfn "res = %s" m.Value
//res = All
//res = the
//res = Pretty
//res = Horses
//
//val it : unit = ()

type DiscreteEventCounter = 
    { mutable Total : int
      mutable Positive : int
      Name : string }

let recordEvent (s : DiscreteEventCounter) isPositive = 
    s.Total <- s.Total + 1
    if isPositive then s.Positive <- s.Positive + 1

let reportStatus (s : DiscreteEventCounter) =
    printfn "We have %d %s out of %d" s.Positive s.Name s.Total

let newCounter nm = 
    { Total = 0
      Positive = 0
      Name = nm }

let longPageCounter = newCounter "long page(s)"

let fetch url = 
    let page = http url
    recordEvent longPageCounter (page.Length > 10000)
    page
//type DiscreteEventCounter =
//  {mutable Total: int;
//   mutable Positive: int;
//   Name: string;}
//val recordEvent : s:DiscreteEventCounter -> isPositive:bool -> unit
//val reportStatus : s:DiscreteEventCounter -> unit
//val newCounter : nm:string -> DiscreteEventCounter
//val longPageCounter : DiscreteEventCounter = {Total = 0;
//                                              Positive = 0;
//                                              Name = "long page(s)";}
//val fetch : url:string -> string

fetch "http://www.smh.com.au" |> ignore
fetch "http://www.theage.com.au" |> ignore
reportStatus longPageCounter
//We have 2 long page(s) out of 2
//
//val it : unit = ()

type Cell = { mutable data : int }
let cell1 = { data = 3 }
let cell2 = cell1
//type Cell =
//  {mutable data: int;}
//val cell1 : Cell = {data = 3;}
//val cell2 : Cell = {data = 3;}

cell1.data <- 7

cell1
//val it : Cell = {data = 7;}

cell2
//val it : Cell = {data = 7;}

let mutable cell1 = 1
//val mutable cell1 : int = 1

cell1 <- 3
cell1
//val it : int = 3

let sum n m = 
    let mutable res = 0
    for i = n to m do
        res <- res + i
    res
//val sum : n:int -> m:int -> int

sum 3 6
//val it : int = 18

let generateStamp =
    let mutable count = 0
    (fun () -> count <- count + 1; count)
//val generateStamp : (unit -> int)

generateStamp()
//val it : int = 1

generateStamp()
//val it : int = 2

let arr = [|1.0; 1.0; 1.0|]
//val arr : float [] = [|1.0; 1.0; 1.0|]

arr.[1]
//val it : float = 1.0

arr.[1] <- 3.0
arr
//val it : float[] = [| 1.0; 3.0; 1.0 |]

Array.append
//val it : ('a [] -> 'a [] -> 'a []) = <fun:clo@151>

Array.sub
//val it : ('a [] -> int -> int -> 'a []) = <fun:clo@154-1>

Array.copy
//val it : ('a [] -> 'a []) = <fun:clo@157-2>

Array.iter
//val it : (('a -> unit) -> 'a [] -> unit) = <fun:it@160-3>

Array.filter
//val it : (('a -> bool) -> 'a [] -> 'a []) = <fun:clo@163-3>

Array.length
//val it : ('a [] -> int) = <fun:clo@166-7>

Array.map
//val it : (('a -> 'b) -> 'a [] -> 'b []) = <fun:it@169-7>

Array.fold
//val it : (('a -> 'b -> 'a) -> 'a -> 'b [] -> 'a) = <fun:clo@172-5>

Array.foldBack
//val it : (('a -> 'b -> 'b) -> 'a [] -> 'b -> 'b) = <fun:clo@175-6>

let bigArray = Array.zeroCreate<int> 100000000
//val bigArray : int [] =
//  [|0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0;
//    0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0;
//    0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0;
//    0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0; 0;
//    ...|]

let tooBig = Array.zeroCreate<int> 1000000000
//System.OutOfMemoryException: Exception of type 'System.OutOfMemoryException' was thrown.

let arr = [|for i in 0 .. 5 -> (i, i * i)|]
//val arr : (int * int) [] =
//  [|(0, 0); (1, 1); (2, 4); (3, 9); (4, 16); (5, 25)|]

arr.[1..3]
//val it : (int * int) [] = [|(1, 1); (2, 4); (3, 9);|]

arr.[..2]
//val it : (int * int) [] = [|(0, 0); (1, 1); (2, 4);|]

arr.[3..]
//val it : (int * int) [] = [|(3, 9); (4, 16); (5, 25)|]

let names = new ResizeArray<string>()
//val names : ResizeArray<string>

for name in [ "Claire"; "Sophie"; "Jane" ] do
    names.Add(name)
//val it : unit = ()

names.Count
//val it : int = 3

names.[0]
//val it : string = "Claire"

names.[1]
//val it : string = "Sophie"

names.[2]
//val it : string = "Jane"

let squares = new ResizeArray<int>(seq {for i in 0 .. 100 -> i * i})
//val squares : ResizeArray<int>

for x in squares do
    printfn "square: %d" x
//square: 0
//square: 1
//square: 4
//square: 9
//...
//square: 9801
//square: 10000
//val it : unit = ()

open System
open System.Collections.Generic

let capitals = new Dictionary<string, string>(HashIdentity.Structural)
//val capitals : Dictionary<string,string> = dict []

capitals.["USA"] <- "Washington"
capitals.["Bangladesh"] <- "Dhaka"

capitals.ContainsKey("USA")
//val it : bool = true

capitals.ContainsKey("Australia")
//val it : bool = false

capitals.Keys
//val it : Dictionary`2.KeyCollection<string,string> = seq ["USA"; "Bangladesh"]

capitals.["USA"]
//val it : string = "Washington"

for kvp in capitals do
    printfn "%s has capital %s" kvp.Key kvp.Value
//USA has capital Washington
//Bangladesh has capital Dhaka
//val it : unit = ()

let lookupName nm (dict : Dictionary<string, string>) = 
    let mutable res = ""
    let foundIt = dict.TryGetValue(nm, &res)
    if foundIt then res
    else failwithf "Didn’t find %s" nm
//val lookupName : nm:string -> dict:Dictionary<string,string> -> string

let res = ref ""
//val res : string ref = {contents = "";}

capitals.TryGetValue("Australia", res)
//val it : bool = false

capitals.TryGetValue("USA", res)
//val it : bool = true

res
//val it : string ref = {contents = "Washington";}

capitals.TryGetValue("Australia")
//val it: bool * string = (false, null)

capitals.TryGetValue("USA")
//val it: bool * string = (true, "Washington")

open System.Collections.Generic
open Microsoft.FSharp.Collections

let sparseMap = new Dictionary<(int * int), float>()
//val sparseMap : Dictionary <(int * int),float> = dict []

sparseMap.[(0,2)] <- 4.0
sparseMap.[(1021,1847)] <- 9.0
sparseMap.Keys
//val it : Dictionary`2.KeyCollection<(int * int),float> =
//  seq [(0, 2); (1021, 1847)]

let req = System.Net.WebRequest.Create("not a URL")
//System.UriFormatException: Invalid URI: The format of the URI could not be determined.
//   at System.Uri.CreateThis(String uri, Boolean dontEscape, UriKind uriKind)
//   at System.Uri..ctor(String uriString)
//   at System.Net.WebRequest.Create(String requestUriString)

(raise (System.InvalidOperationException("not today thank you")) : unit)
//System.InvalidOperationException: not today thank you

if false then 3 else failwith "hit the wall"
//System.Exception: hit the wall

failwith
//val it : (string -> 'a) = <fun:clo@309>

raise
//val it : (System.Exception -> 'a) = <fun:clo@312>

failwithf
//val it : (Printf.StringFormat<'a,'b> -> 'a) = <fun:clo@315-1>

invalidArg
//val it : (string -> string -> 'a) = <fun:it@318-3>


if (System.DateTime.Now > failwith "not yet decided") then
    printfn "you've run out of time!"
//System.Exception: not yet decided
//   at <StartupCode$FSI_0074>.$FSI_0074.main@() in C:\dev\apress\f-3.0code\4Imperative\Script.fsx:line 315
//Stopped due to error

try 
    raise (System.InvalidOperationException("it's just not my day"))
with :? System.InvalidOperationException -> printfn "caught!"
//caught!
//val it : unit = ()

open System.IO

let http (url : string) =
    try
        let req = System.Net.WebRequest.Create(url)
        let resp = req.GetResponse()
        let stream = resp.GetResponseStream()
        let reader = new StreamReader(stream)
        let html = reader.ReadToEnd()
        html
    with
        | :? System.UriFormatException -> ""
        | :? System.Net.WebException -> ""
//val http : url:string -> string

try 
    raise (new System.InvalidOperationException("invalid operation"))
with err -> printfn "oops, msg = '%s'" err.Message
//oops, msg = 'invalid operation'
//val it : unit = ()

let httpViaTryFinally(url : string) =
    let req = System.Net.WebRequest.Create(url)
    let resp = req.GetResponse()
    try
        let stream = resp.GetResponseStream()
        let reader = new StreamReader(stream)
        let html = reader.ReadToEnd()
        html
    finally
        resp.Close()

let httpViaUseBinding(url : string) =
    let req = System.Net.WebRequest.Create(url)
    use resp = req.GetResponse()
    let stream = resp.GetResponseStream()
    let reader = new StreamReader(stream)
    let html = reader.ReadToEnd()
    html
//val httpViaTryFinally : url:string -> string
//val httpViaUseBinding : url:string -> string

exception BlockedURL of string
//exception BlockedURL of string

let http2 url =
    if url = "http://www.kaos.org"
    then raise(BlockedURL(url))
    else http url
//val http2 : url:string -> string

try 
    raise (BlockedURL("http://www.kaos.org"))
with BlockedURL(url) -> printfn "blocked! url = '%s'" url
//blocked! url = 'http://www.kaos.org'
//val it : unit = ()

open System.IO
File.WriteAllLines("test.txt", [| "This is a test file."; "It is easy to read." |])
// NOTE: The file is written in the temp folder as %TEMP%\test.txt, eg. C:\Users\pdejoux\AppData\Local\Temp\test.txt

open System.IO
File.ReadAllLines "test.txt"
//val it : string [] = [|"This is a test file."; "It is easy to read."|]

File.ReadAllText "test.txt"
//val it : string = "This is a test file.
//It is easy to read.
//"

seq { 
    for line in File.ReadLines("test.txt") do
        let words = line.Split [| ' ' |]
        if words.Length > 3 && words.[2] = "easy" then yield line
}
//val it : seq<string> = seq ["It is easy to read."]

let outp = File.CreateText "playlist.txt"
outp.WriteLine "Enchanted"
outp.WriteLine "Put your records on"
outp.Close()
//val outp : StreamWriter
//val it : unit = ()

let inp = File.OpenText("playlist.txt")
//val inp : StreamReader

inp.ReadLine()
//val it : string = "Enchanted"

inp.ReadLine()
//val it : string = "Put your records on"

inp.Close()

System.Console.WriteLine "Hello World"
//Hello World
//val it : unit = ()

System.Console.ReadLine()
//I'm still here
//val it : string = "I'm still here"

let isWord (words : string list) =
    let wordTable = Set.ofList words
    fun w -> wordTable.Contains(w)
//val isWord : words:string list -> (string -> bool)

let isCapital = isWord ["London"; "Paris"; "Warsaw"; "Tokyo"]
//val isCapital : (string -> bool)

isCapital "Paris"
//val it : bool = true

isCapital "Manchester"
//val it : bool = false

let isCapitalSlow = isWord ["London"; "Paris"; "Warsaw"; "Tokyo"]

let isWordSlow2 (words : string list) (word : string) =
    List.exists (fun word2 -> word = word2) words

let isCapitalSlow2 = isWordSlow2 ["London"; "Paris"; "Warsaw"; "Tokyo"]

let isWordSlow3 (words : string list) (word : string) =
    let wordTable = Set<_>(words)
    wordTable.Contains(word)

let isCapitalSlow3 = isWordSlow3 ["London"; "Paris"; "Warsaw"; "Tokyo"]
//val isCapitalSlow : (string -> bool)
//val isWordSlow2 : words:string list -> word:string -> bool
//val isCapitalSlow2 : (string -> bool)
//val isWordSlow3 : words:string list -> word:string -> bool
//val isCapitalSlow3 : (string -> bool)

open System.Collections.Generic

let isWord (words : string list) =
    let wordTable = HashSet<_>(words)
    fun word -> wordTable.Contains word
//val isWord : words:string list -> (string -> bool)

open System

type NameLookupService =
    abstract Contains : string -> bool

let buildSimpleNameLookup (words : string list) =
    let wordTable = HashSet<_>(words)
    {new NameLookupService with
         member t.Contains w = wordTable.Contains w}
//type NameLookupService =
//  interface
//    abstract member Contains : string -> bool
//  end
//val buildSimpleNameLookup : words:string list -> NameLookupService

let capitalLookup = buildSimpleNameLookup ["London"; "Paris"; "Warsaw"; "Tokyo"]
capitalLookup.Contains "Paris"
//val capitalLookup : NameLookupService
//val it : bool = true

let rec fib n = if n <= 2 then 1 else fib (n - 1) + fib (n - 2)

let fibFast =
    let t = new System.Collections.Generic.Dictionary<int, int>()
    let rec fibCached n =
        if t.ContainsKey n then t.[n]
        elif n <= 2 then 1
        else let res = fibCached (n - 1) + fibCached (n - 2)
             t.Add (n, res)
             res
    fun n -> fibCached n

let time f =
    let sw = System.Diagnostics.Stopwatch.StartNew()
    let res = f()
    let finish = sw.Stop()
    (res, sw.Elapsed.TotalMilliseconds |> sprintf "%f ms")
//val fib : n:int -> int
//val fibFast : (int -> int)
//val time : f:(unit -> 'a) -> 'a * string

time(fun () -> fibFast 30)
//val it : int * string = (832040, "0.436200 ms")

time(fun () -> fibFast 30)
//val it : int * string = (832040, "0.092900 ms")

time(fun () -> fibFast 30)
//val it : int * string = (832040, "0.107200 ms")

open System.Collections.Generic 

#nowarn "40"
// warning FS0040: This and other recursive references to the object(s) being defined will be checked for initialization-soundness at runtime through the use of a delayed reference. This is because you are defining one or more recursive objects, rather than recursive functions. This warning may be suppressed by using '#nowarn "40"' or '--nowarn:40'.
let memoize (f : 'T -> 'U) =
    let t = new Dictionary<'T, 'U>(HashIdentity.Structural)
    fun n ->
        if t.ContainsKey n then t.[n]
        else let res = f n
             t.Add (n, res)
             res

let rec fibFast =
    memoize (fun n -> if n <= 2 then 1 else fibFast (n - 1) + fibFast (n - 2))

let rec fibNotFast n =
    memoize (fun n -> if n <= 2 then 1 else fibNotFast (n - 1) + fibNotFast (n - 2)) n
//val memoize : f:('T -> 'U) -> ('T -> 'U) when 'T : equality
//val fibFast : (int -> int)
//val fibNotFast : n:int -> int

open System.Collections.Generic 

#nowarn "40" // do not warn on recursive computed objects and functions

type Table<'T, 'U> =
    abstract Item : 'T -> 'U with get
    abstract Discard : unit -> unit

let memoizeAndPermitDiscard f = 
    let lookasideTable = new Dictionary<_, _>(HashIdentity.Structural)
    { new Table<'T, 'U> with
          
          member t.Item 
              with get (n) = 
                  if lookasideTable.ContainsKey(n) then lookasideTable.[n]
                  else 
                      let res = f n
                      lookasideTable.Add(n, res)
                      res
          
          member t.Discard() = lookasideTable.Clear() }

let rec fibFast = 
    memoizeAndPermitDiscard (fun n -> 
        printfn "computing fibFast %d" n
        if n <= 2 then 1
        else fibFast.[n - 1] + fibFast.[n - 2])
//type Table<'T,'U> =
//  interface
//    abstract member Discard : unit -> unit
//    abstract member Item : 'T -> 'U with get
//  end
//val memoizeAndPermitDiscard : f:('T -> 'U) -> Table<'T,'U> when 'T : equality
//val fibFast : Table<int,int>

fibFast.[3]
//computing fibFast 3
//computing fibFast 2
//computing fibFast 1
//val it : int = 2

fibFast.[5]
//computing fibFast 5
//computing fibFast 4
//val it : int = 5

fibFast.Discard()

fibFast.[5]
//computing fibFast 5
//computing fibFast 4
//computing fibFast 3
//computing fibFast 2
//computing fibFast 1
//val it : int = 5

let sixty = lazy (30 + 30)
//val sixty : Lazy<int> = Value is not created.

sixty.Force()
//val it : int = 60 

let sixtyWithSideEffect = lazy (printfn "Hello world"; 30 + 30)
//val sixtyWithSideEffect : Lazy<int> = Value is not created.

sixtyWithSideEffect.Force()
//Hello world
//val it : int = 60

sixtyWithSideEffect.Force()
//val it : int = 60

let cell1 = ref 1
//val cell1 : int ref = {contents = 1;}

cell1.Value
//val it : int = 1

cell1 := 3
//val it : unit = ()

cell1
//val it : int ref = {contents = 3;}

cell1.Value
//val it : int = 3

let factorizeImperative n = 
    let mutable factor1 = 1
    let mutable factor2 = n
    let mutable i = 2
    let mutable fin = false
    while (i < n && not fin) do
        if (n % i = 0) then 
            factor1 <- i
            factor2 <- n / i
            fin <- true
        i <- i + 1
    if (factor1 = 1) then None
    else Some(factor1, factor2)
//val factorizeImperative : n:int -> (int * int) option

let factorizeRecursive n =
    let rec find i =
        if i >= n then None
        elif (n % i = 0) then Some(i, n / i)
        else find (i + 1)
    find 2
//val factorizeRecursive : n:int -> (int * int) option

open System.Collections.Generic

let divideIntoEquivalenceClasses keyf seq = 
    // The dictionary to hold the equivalence classes
    let dict = new Dictionary<'key, ResizeArray<'T>>()
    // Build the groupings
    seq |> Seq.iter (fun v -> 
               let key = keyf v
               let ok, prev = dict.TryGetValue(key)
               if ok then prev.Add(v)
               else 
                   let prev = new ResizeArray<'T>()
                   dict.[key] <- prev
                   prev.Add(v))
    // Return the sequence-of-sequences. Don't reveal the
    // internal collections: just reveal them as sequences
    dict |> Seq.map (fun group -> group.Key, Seq.readonly group.Value)

//val divideIntoEquivalenceClasses :
//  keyf:('T -> 'key) -> seq:seq<'T> -> seq<'key * seq<'T>> when 'key : equality

divideIntoEquivalenceClasses (fun n -> n % 3) [0 .. 10]
//val it : seq<int * seq<int>>
//= seq [(0, seq [0; 3; 6; 9]); (1, seq [1; 4; 7; 10]); (2, seq [2; 5; 8])]

open System.IO

let reader1, reader2 =
    let reader = new StreamReader(File.OpenRead("test.txt"))
    let firstReader() = reader.ReadLine()
    let secondReader() = reader.ReadLine()

    // Note: we close the stream reader here!
    // But we are returning function values which use the reader
    // This is very bad!
    reader.Close()
    firstReader, secondReader
//val reader2 : (unit -> string)
//val reader1 : (unit -> string)

// Note: stream reader is now closed! The next line will fail!
let firstLine = reader1()
let secondLine = reader2()
firstLine, secondLine
//System.ObjectDisposedException: Cannot read from a closed TextReader.
//   at System.IO.__Error.ReaderClosed()
//   at System.IO.StreamReader.ReadLine()

open System.IO

let line1, line2 =
    let reader = new StreamReader(File.OpenRead("test.txt"))
    let firstLine = reader.ReadLine()
    let secondLine = reader.ReadLine()
    reader.Close()
    firstLine, secondLine
//val line2 : string = "It is easy to read."
//val line1 : string = "This is a test file."

let linesOfFile = 
    seq { 
        use reader = new StreamReader(File.OpenRead("test.txt"))
        while not reader.EndOfStream do
            yield reader.ReadLine()
    }
//val linesOfFile : seq<string>