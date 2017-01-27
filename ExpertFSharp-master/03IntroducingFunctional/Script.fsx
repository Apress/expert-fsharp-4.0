let squareAndAdd a b = a * a + b
//val squareAndAdd : a:int -> b:int -> int

let squareAndAdd (a:float) b = a * a + b
//val squareAndAdd : a:float -> b:float -> float

let squareAndAdd (a:float) (b:float) : float = a * a + b
//val squareAndAdd : a:float -> b:float -> float

int 17.8
// val it : int = 17

int -17.8
//val it : int = -17

string 65
//val it : string = "65"

float 65
//val it : float = 65.0

double 65
//val it : float = 65.0

let s = "Couldn't put Humpty"
//val s : string = "Couldn't put Humpty"

s.Length
//val it : int = 19

s.[13]
//val it : char = 'H'

s.[13..16]
//val it : string = "Hump"

let s = "Couldn't put Humpty"
//val s : string = "Couldn't put Humpty"

s.[13] <- 'h'
//Script.fsx(35,1): error FS0810: Property 'Chars' cannot be set

"Couldn't put Humpty" + " " + "together again";;
//val it : string = "Couldn't put Humpty together again"

let round x =
    if x >= 100 then 100
    elif x < 0 then 0
    else x
//val round : x:int -> int

let round x =
    match x with
    | _ when x >= 100 -> 100
    | _ when x < 0 -> 0
    | _ -> x
//val round : x:int -> int

let round2 (x, y) =
    if x >= 100 || y >= 100 then 100, 100
    elif x < 0 || y < 0 then 0, 0
    else x, y
//val round2 : x:int * y:int -> int * int

let rec factorial n = if n <= 1 then 1 else n * factorial (n - 1)
//val factorial : n:int -> int

factorial 5
//val it : int = 120

let rec length l =
    match l with
    | [] -> 0
    | h :: t -> 1 + length t
//val length : l:'a list -> int

open System.IO
open System.Net

/// Get the contents of the URL via a web request.
let http (url : string) =
    let req = System.Net.WebRequest.Create(url)
    let resp = req.GetResponse()
    let stream = resp.GetResponseStream()
    let reader = new StreamReader(stream)
    let html = reader.ReadToEnd()
    resp.Close()
    html
//val http : url:string -> string
    
let rec repeatFetch url n =
    if n > 0 then
        let html = http url
        printfn "fetched <<< %s >>> on iteration %d" html n
        repeatFetch url (n - 1)
//val repeatFetch : url:string -> n:int -> unit

let rec badFactorial n = if n <= 0 then 1 else n * badFactorial n
//val badFactorial : n:int -> int

let rec even n = (n = 0u) || odd(n - 1u)
and odd n = (n <> 0u) && even(n - 1u)
//val even : n:uint32 -> bool
//val odd : n:uint32 -> bool

let even n = (n % 2u) = 0u
let odd n = (n % 2u) = 1u
//val even : n:uint32 -> bool
//val odd : n:uint32 -> bool

[]
//val it : 'a list = []

1 :: [2; 3]
//val it : int list = [1; 2; 3]

[1; 2; 3]
//val it : int list = [1; 2; 3]

[1 .. 99]
//val it : int list =
//  [1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 11; 12; 13; 14; 15; 16; 17; 18; 19; 20; 21;
//   22; 23; 24; 25; 26; 27; 28; 29; 30; 31; 32; 33; 34; 35; 36; 37; 38; 39; 40;
//   41; 42; 43; 44; 45; 46; 47; 48; 49; 50; 51; 52; 53; 54; 55; 56; 57; 58; 59;
//   60; 61; 62; 63; 64; 65; 66; 67; 68; 69; 70; 71; 72; 73; 74; 75; 76; 77; 78;
//   79; 80; 81; 82; 83; 84; 85; 86; 87; 88; 89; 90; 91; 92; 93; 94; 95; 96; 97;
//   98; 99]

['A' .. 'Z']
//val it : char list =
//  ['A'; 'B'; 'C'; 'D'; 'E'; 'F'; 'G'; 'H'; 'I'; 'J'; 'K'; 'L'; 'M'; 'N'; 'O';
//   'P'; 'Q'; 'R'; 'S'; 'T'; 'U'; 'V'; 'W'; 'X'; 'Y'; 'Z']

[for x in 1..99 -> x * x]
////val it : int list =
////  [1; 4; 9; 16; 25; 36; 49; 64; 81; 100; 121; 144; 169; 196; 225; 256; 289;
////   324; 361; 400; 441; 484; 529; 576; 625; 676; 729; 784; 841; 900; 961; 1024;
////   1089; 1156; 1225; 1296; 1369; 1444; 1521; 1600; 1681; 1764; 1849; 1936;
////   2025; 2116; 2209; 2304; 2401; 2500; 2601; 2704; 2809; 2916; 3025; 3136;
////   3249; 3364; 3481; 3600; 3721; 3844; 3969; 4096; 4225; 4356; 4489; 4624;
////   4761; 4900; 5041; 5184; 5329; 5476; 5625; 5776; 5929; 6084; 6241; 6400;
////   6561; 6724; 6889; 7056; 7225; 7396; 7569; 7744; 7921; 8100; 8281; 8464;
////   8649; 8836; 9025; 9216; 9409; 9604; 9801]

[1; 2] @ [3]
//val it : int list = [1; 2; 3]

let oddPrimes = [3; 5; 7; 11]
let morePrimes = [13; 17]
let primes = 2 :: (oddPrimes @ morePrimes)
//val oddPrimes : int list = [3; 5; 7; 11]
//val morePrimes : int list = [13; 17]
//val primes : int list = [2; 3; 5; 7; 11; 13; 17]

let people = ["Adam"; "Dominic"; "James"]
//val people : string list = ["Adam"; "Dominic"; "James"]

"Chris" :: people
//val it : string list = ["Chris"; "Adam"; "Dominic"; "James"]

people
//val it : string list = ["Adam"; "Dominic"; "James"]

List.length
//val it : ('a list -> int) = <fun:clo@164-14>

List.head
//val it : ('a list -> 'a) = <fun:clo@167-15>

List.tail
//val it : ('a list -> 'a list) = <fun:clo@170-16>

List.init
//val it : (int -> (int -> 'a) -> 'a list) = <fun:clo@173-17>

List.append
//val it : ('a list -> 'a list -> 'a list) = <fun:clo@176-18>

List.filter
//val it : (('a -> bool) -> 'a list -> 'a list) = <fun:clo@179-19>

List.map
//val it : (('a -> 'b) -> 'a list -> 'b list) = <fun:clo@182-20>

List.iter
//val it : (('a -> unit) -> 'a list -> unit) = <fun:clo@185-21>

List.unzip
//val it : (('a * 'b) list -> 'a list * 'b list) = <fun:clo@188-22>

List.zip
//val it : ('a list -> 'b list -> ('a * 'b) list) = <fun:clo@191-23>

List.toArray
//val it : ('a list -> 'a []) = <fun:clo@194-24>

List.ofArray
//val it : ('a [] -> 'a list) = <fun:clo@197-25>

List.head [5; 4; 3]
//val it : int = 5

List.tail [5; 4; 3]
//val it : int list = [4; 3]

List.map (fun x -> x * x) [1; 2; 3]
//val it : int list = [1; 4; 9]

List.filter (fun x -> x % 3 = 0) [2; 3; 5; 7; 9]
//val it : int list = [3; 9]

type 'T option =
    | None
    | Some of 'T

let people = [("Adam", None);
              ("Eve" , None);
              ("Cain", Some("Adam","Eve"));
              ("Abel", Some("Adam","Eve"))]
//val people : (string * (string * string) option) list =
//  [("Adam", None); ("Eve", None); ("Cain", Some ("Adam", "Eve"));
//   ("Abel", Some ("Adam", "Eve"))]

let fetch url =
    try Some (http url)
    with :? System.Net.WebException -> None
//val fetch : url:string -> string option

match (fetch "http://www.nature.com") with
  | Some text -> printfn "text = %s" text
  | None -> printfn "**** no web page found"

//text = <!DOCTYPE html PUBLIC ...  (note: the HTML is shown here if connected to the web)

let isLikelySecretAgent url agent =
    match (url, agent) with
    | "http://www.control.org", 99 -> true
    | "http://www.control.org", 86 -> true
    | "http://www.kaos.org", _ -> true
    | _ -> false
//val isLikelySecretAgent : url:string -> agent:int -> bool

let printFirst primes =
    match primes with
    | h :: t -> printfn "The first prime in the list is %d" h
    | [] -> printfn "No primes found in the list"
//val printFirst : primes:int list -> unit

printFirst oddPrimes
//The first prime in the list is 3
//val it : unit = ()

let showParents (name, parents) =
    match parents with
    | Some (dad, mum) -> printfn "%s has father %s, mother %s" name dad mum
    | None -> printfn "%s has no parents!" name;;
//val showParents : name:string * parents:(string * string) option -> unit

for person in people do showParents person
//Adam has no parents!
//Eve has no parents!
//Cain has father Adam, mother Eve
//Abel has father Adam, mother Eve
//val it : unit = ()

let highLow a b =
    match (a, b) with
    | ("lo", lo), ("hi", hi) -> (lo, hi)
    | ("hi", hi), ("lo", lo) -> (lo, hi)
    | _ -> failwith "expected a both a high and low value"
//val highLow : string * 'a -> string * 'a -> 'a * 'a

highLow ("hi", 300) ("lo", 100)
//val it : int * int = (100, 300)

let urlFilter3 url agent =
    match url, agent with
    | "http://www.control.org", 86 -> true
    | "http://www.kaos.org", _ -> false
//Script.fsx(277,11): warning FS0025: Incomplete pattern matches on this expression. For example, the value '(_,0)' may indicate a case not covered by the pattern(s).
//val urlFilter3 : url:string -> agent:int -> bool

let urlFilter4 url agent =
    match url, agent with
    | "http://www.control.org", 86 -> true
    | "http://www.kaos.org", _ -> false
    | _ -> failwith "unexpected input"
//val urlFilter4 : url:string -> agent:int -> bool

let urlFilter2 url agent =
    match url, agent with
    | "http://www.control.org", _ -> true
    | "http://www.control.org", 86 -> true
    | _ -> false
//Script.fsx(293,7): warning FS0026: This rule will never be matched
//val urlFilter2 : url:string -> agent:int -> bool

let sign x =
    match x with
    | _ when x < 0 -> -1
    | _ when x > 0 ->  1
    | _ -> 0
//val sign : x:int -> int

let getValue a =
    match a with
    | (("lo" | "low"), v) -> v
    | ("hi", v) | ("high", v) -> v
    | _ -> failwith "expected a both a high and low value"
//val getValue : string * 'a -> 'a

let sites = ["http://www.bing.com"; "http://www.google.com"];;
//val sites : string list = ["http://www.bing.com"; "http://www.google.com"]

let fetch url = (url, http url)
//val fetch : url:string -> string * string

List.map fetch sites
//val it : (string * string) list =
//  [("http://www.bing.com",
//    "<!DOCTYPE html PUBLIC ...

List.map
//val it : (('a -> 'b) -> 'a list -> 'b list) = <fun:clo@210-2>

let primes = [2; 3; 5; 7]
//val primes : int list = [2; 3; 5; 7]

let primeCubes = List.map (fun n -> n * n * n) primes
//val primeCubes: int list = [8; 27; 125; 343] 

let resultsOfFetch = List.map (fun url -> (url, http url)) sites
//val resultsOfFetch : (string * string) list =
//  [("http://www.bing.com",
//    "<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"+[56540 chars]);
//   ("http://www.google.com",
//    "<!doctype html><html itemscope="" itemtype="http://schema.org"+[52260 chars])]

List.map (fun (_, p) -> String.length p) resultsOfFetch
//val it : int list = [56601; 52321]

let delimiters = [|' '; '\n'; '\t'; '<'; '>'; '='|]
//val delimiters : char [] = [|' '; '\010'; '\009'; '<'; '>'; '='|]

let getWords (s : string) = s.Split delimiters
//val getWords : s:string -> string []

let getStats site =
    let url = "http://" + site
    let html = http url
    let hwords = html |> getWords
    let hrefs = html |> getWords |> Array.filter (fun s -> s = "href")
    (site, html.Length, hwords.Length, hrefs.Length)
//val getStats : site:string -> string * int * int * int

let sites = ["www.bing.com"; "www.google.com"; "search.yahoo.com"]
//val sites : string list =
//  ["www.bing.com"; "www.google.com"; "search.yahoo.com"]

sites |> List.map getStats
//val it : (string * int * int * int) list =
//  [("www.bing.com", 56601, 3230, 30); ("www.google.com", 52314, 2975, 31);
//   ("search.yahoo.com", 17691, 1568, 40)]

List.map
//val it : (('a -> 'b) -> 'a list -> 'b list) = <fun:clo@365-28>

Array.map
//val it : (('a -> 'b) -> 'a [] -> 'b []) = <fun:it@368-23>

Option.map
//val it : (('a -> 'b) -> 'a option -> 'b option) = <fun:clo@371-29>

Seq.map
//val it : (('a -> 'b) -> seq<'a> -> seq<'b>) = <fun:clo@374-30>

type List<'a> with
    member x.Select f = List.map f x
//type List<'T> with
//  member Select : f:('T -> 'a) -> 'a list

sites.Select getStats
//val it : (string * int * int * int) list =
//  [("www.bing.com", 56704, 3241, 30); ("www.google.com", 52343, 2972, 31);
//   ("search.yahoo.com", 17691, 1568, 40)]

type System.String with
    member x.Select f = f x
//type String with
//  member Select : f:(System.String -> 'a) -> 'a

"www.bing.com".Select getStats
//val it : string * int * int * int = ("www.bing.com", 56704, 3241, 30)

type Site(url) =
    member x.Select f = f url
//type Site =
//  class
//    new : url:string -> Site
//    member Select : f:(string -> 'a) -> 'a
//  end

Site("www.bing.com").Select getStats
//val it : string * int * int * int = ("www.bing.com", 56704, 3241, 30)

[1; 2; 3] |> List.map (fun x -> x * x * x)
//val it : int list = [1; 8; 27]

List.map (fun x -> x * x * x) [1; 2; 3]
//val it : int list = [1; 8; 27]

(|>)
//val it : ('a -> ('a -> 'b) -> 'b) = <fun:it@412-30>

let google = http "http://www.google.com"
//val google : string =
//  "<!doctype html><html itemscope="" itemtype="http://schema.org"+[52306 chars]

let countLinks = getWords >> Array.filter (fun s -> s = "href") >> Array.length
//val countLinks : (string -> int)

google |> countLinks
//val it : int = 31

(>>)
//val it : (('a -> 'b) -> ('b -> 'c) -> 'a -> 'c) = <fun:it@425-31>

let shift (dx, dy) (px, py) = (px + dx, py + dy)
//val shift : dx:int * dy:int -> px:int * py:int -> int * int

let shiftRight = shift (1, 0)
let shiftUp = shift (0, 1)
let shiftLeft = shift (-1, 0)
let shiftDown = shift (0, -1)

//val shiftRight : (int * int -> int * int)
//val shiftUp : (int * int -> int * int)
//val shiftLeft : (int * int -> int * int)
//val shiftDown : (int * int -> int * int)

shiftRight (10, 10)
//val it : int * int = (11, 10)

List.map (shift (2,2)) [(0,0); (1,0); (1,1); (0,1)]
//val it : (int * int) list = [(2, 2); (3, 2); (3, 3); (2, 3)]

open System.Drawing

let remap (r1 : Rectangle) (r2 : Rectangle) = 
    let scalex = float r2.Width / float r1.Width
    let scaley = float r2.Height / float r1.Height
    let mapx x = int (float r2.Left + truncate (float (x - r1.Left) * scalex))
    let mapy y = int (float r2.Top + truncate (float (y - r1.Top) * scaley))
    let mapp (p : Point) = Point(mapx p.X, mapy p.Y)
    mapp
//val remap : r1:Rectangle -> r2:Rectangle -> (Point -> Point)

let mapp = remap (Rectangle(100, 100, 100, 100)) (Rectangle(50, 50, 200, 200))
//val mapp : Point -> Point

mapp (Point(100, 100))
//val it : Point = {X=50,Y=50} {IsEmpty = false;
//                              X = 50;
//                              Y = 50;}

mapp (Point(150, 150))
//val it : Point = {X=150,Y=150} {IsEmpty = false;
//                                X = 150;
//                                Y = 150;}

mapp (Point(200, 200))
//val it : Point = {X=250,Y=250} {IsEmpty = false;
//                                X = 250;
//                                Y = 250;}

let sites = ["http://www.bing.com";
             "http://www.google.com";
             "http://search.yahoo.com"]
//val sites : string list =
//  ["http://www.bing.com"; "http://www.google.com"; "http://search.yahoo.com"]

sites |> List.iter (fun site -> printfn "%s, length = %d" site (http site).Length)
//http://www.bing.com, length = 56704
//http://www.google.com, length = 52332
//http://search.yahoo.com, length = 17691
//val it : unit = ()

open System

let start = DateTime.Now
http "http://www.newscientist.com"
let finish = DateTime.Now
let elapsed = finish - start
//val start : DateTime = 5/4/2015 8:26:03 AM
//val finish : DateTime = 5/4/2015 8:26:04 AM
//val elapsed : TimeSpan = 00:00:00.9082377

open System

let time f =
    let start = DateTime.Now
    let res = f()
    let finish = DateTime.Now
    (res, finish - start)
//val time : f:(unit -> 'a) -> 'a * TimeSpan

time (fun () -> http "http://www.newscientist.com")
//val it : string * TimeSpan =
//  ("<!DOCTYPE html PUBLIC 
//</html>",
//   00:00:00.8937947 {Days = 0;
//                     Hours = 0;
//                     Milliseconds = 893;
//                     Minutes = 0;
//                     Seconds = 0;
//                     Ticks = 8937947L;
//                     TotalDays = 1.034484606e-05;
//                     TotalHours = 0.0002482763056;
//                     TotalMilliseconds = 893.7947;
//                     TotalMinutes = 0.01489657833;
//                     TotalSeconds = 0.8937947;})

open System.IO

[ "file1.txt"; "file2.txt"; "file3.sh" ] |> List.map Path.GetExtension
//val it : string list = [".txt"; ".txt"; ".sh"]

open System

let f = Console.WriteLine
//Script.fsx(530,9): error FS0041: A unique overload for method 'WriteLine' could not be determined based on type information prior to this program point. A type annotation may be needed. Candidates: Console.WriteLine(buffer: char []) : unit, Console.WriteLine(format: string, params arg: obj []) : unit, Console.WriteLine(value: bool) : unit, Console.WriteLine(value: char) : unit, Console.WriteLine(value: decimal) : unit, Console.WriteLine(value: float) : unit, Console.WriteLine(value: float32) : unit, Console.WriteLine(value: int) : unit, Console.WriteLine(value: int64) : unit, Console.WriteLine(value: obj) : unit, Console.WriteLine(value: string) : unit, Console.WriteLine(value: uint32) : unit, Console.WriteLine(value: uint64) : unit

let f = (Console.WriteLine:string -> unit)
//val f : arg00:string -> unit