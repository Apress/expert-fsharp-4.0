type index = int
type flags = int64
type results = string * System.TimeSpan * int * int

type Person = 
    { Name : string
      DateOfBirth : System.DateTime }

//type index = int
//type flags = int64
//type results = string * System.TimeSpan * int * int
//type Person =
//  {Name: string;
//   DateOfBirth: System.DateTime;}

{Name = "Bill"; DateOfBirth = new System.DateTime(1962, 09, 02)}
//val it : Person =
//  {Name = "Bill";
//   DateOfBirth = 2/09/1962 ...;}

type PageStats = 
    { Site : string
      Time : System.TimeSpan
      Length : int
      NumWords : int
      NumHRefs : int }
//type PageStats =
//  {Site: string;
//   Time: System.TimeSpan;
//   Length: int;
//   NumWords: int;
//   NumHRefs: int;}

open System
open System.IO
open System.Net

//Using the time, http and getWords functions from Chapter 3.
let time f =
    let start = DateTime.Now
    let res = f()
    let finish = DateTime.Now
    (res, finish - start)

let http (url : string) =
    let req = System.Net.WebRequest.Create(url)
    let resp = req.GetResponse()
    let stream = resp.GetResponseStream()
    let reader = new StreamReader(stream)
    let html = reader.ReadToEnd()
    resp.Close()
    html

let delimiters = [|' '; '\n'; '\t'; '<'; '>'; '='|]
let getWords (s:string) = s.Split delimiters
//val time : f:(unit -> 'a) -> 'a * TimeSpan
//val http : url:string -> string
//val delimiters : char [] = [|' '; '\010'; '\009'; '<'; '>'; '='|]
//val getWords : s:string -> string []

let stats site = 
    let url = "http://" + site
    let html, t = time (fun () -> http url)
    let words = html |> getWords
    let hrefs = words |> Array.filter (fun s -> s = "href")
    { Site = site
      Time = t
      Length = html.Length
      NumWords = words.Length
      NumHRefs = hrefs.Length }
//val stats  : string -> PageStats

stats "www.google.com"
//val it : PageStats = {Site = "www.google.com";
//                      Time = 00:00:00.5517834 {Days = 0; ...;};
//                      Length = 51803;
//                      NumWords = 2927;
//                      NumHRefs = 30;}

type Person = 
    { Name : string
      DateOfBirth : System.DateTime }

type Company = 
    { Name : string
      Address : string }
//type Person =
//  {Name: string;
//   DateOfBirth: DateTime;}
//type Company =
//  {Name: string;
//   Address: string;}

type Dot = {X : int; Y : int}
type Point = {X : float; Y : float}
//type Dot =
//  {X: int;
//   Y: int;}
//type Point =
//  {X: float;
//   Y: float;}

let coords1 (p : Point) = (p.X, p.Y)
let coords2 (d : Dot) = (d.X, d.Y)
let dist p = sqrt (p.X * p.X + p.Y * p.Y) // use of X and Y implies type "Point"
//val coords1 : p:Point -> float * float
//val coords2 : d:Dot -> int * int
//val dist : p:Point -> float

({Name = "Anna"; DateOfBirth = new System.DateTime(1968, 07, 23)} : Person)
//val it : Person =
//  {Name = "Anna";
//   DateOfBirth = 7/23/1968 ...;}

type Point3D = {X : float; Y : float; Z : float}
let p1 = {X = 3.0; Y = 4.0; Z = 5.0}
let p2 = {p1 with Y = 0.0; Z = 0.0}
//type Point3D =
//  {X: float;
//   Y: float;
//   Z: float;}
//val p1 : Point3D = {X = 3.0;
//                    Y = 4.0;
//                    Z = 5.0;}
//val p2 : Point3D = {X = 3.0;
//                    Y = 0.0;
//                    Z = 0.0;}

let p2 = {X = 3.0; Y = 0.0; Z = 0.0}
//val p2 : Point3D = {X = 3.0;
//                    Y = 0.0;
//                    Z = 0.0;}

type Route = int
type Make = string
type Model = string
type Transport =
    | Car of Make * Model
    | Bicycle
    | Bus of Route
//type Route = int
//type Make = string
//type Model = string
//type Transport =
//  | Car of Make * Model
//  | Bicycle
//  | Bus of Route

let ian = Car("BMW","360")
let don = [Bicycle; Bus 8]
let peter = [Car ("Ford","Fiesta"); Bicycle]
//val ian : Transport = Car ("BMW","360")
//val don : Transport list = [Bicycle; Bus 8]
//val peter : Transport list = [Car ("Ford","Fiesta"); Bicycle]

let averageSpeed (tr : Transport) =
    match tr with
    | Car _ -> 35
    | Bicycle -> 16
    | Bus _ -> 24

type Proposition =
    | True
    | And of Proposition * Proposition
    | Or of Proposition * Proposition
    | Not of Proposition

let rec eval (p : Proposition) =
    match p with
    | True -> true
    | And(p1,p2) -> eval p1 && eval p2
    | Or (p1,p2) -> eval p1 || eval p2
    | Not(p1) -> not (eval p1)

type 'T option =
    | None
    | Some of 'T

type 'T list =
    | ([])
    | (::) of 'T * 'T list
//val averageSpeed : tr:Transport -> int
//type Proposition =
//  | True
//  | And of Proposition * Proposition
//  | Or of Proposition * Proposition
//  | Not of Proposition
//val eval : p:Proposition -> bool
//type 'T option =
//  | None
//  | Some of 'T
//type 'T list =
//  | ( [] )
//  | ( :: ) of 'T * 'T list

type Tree<'T> =
    | Tree of 'T * Tree<'T> * Tree<'T>
    | Tip of 'T

let rec sizeOfTree tree =
    match tree with
    | Tree(_, l, r) -> 1 + sizeOfTree l + sizeOfTree r
    | Tip _ -> 1
//type Tree<'T> =
//  | Tree of 'T * Tree<'T> * Tree<'T>
//  | Tip of 'T
//val sizeOfTree : tree:Tree<'a> -> int
 
let smallTree = Tree ("1", Tree ("2", Tip "a", Tip "b"), Tip "c")
//val smallTree : Tree<string> = Tree ("1",Tree ("2",Tip "a",Tip "b"),Tip "c")

sizeOfTree smallTree
//val it : int = 5

type Point3D = Vector3D of float * float * float

let origin = Vector3D(0., 0., 0.)
let unitX = Vector3D(1., 0., 0.)
let unitY = Vector3D(0., 1., 0.)
let unitZ = Vector3D(0., 0., 1.)

let length (Vector3D(dx, dy, dz)) = sqrt(dx * dx + dy * dy + dz * dz)
//type Point3D = | Vector3D of float * float * float
//val origin : Point3D = Vector3D (0.0,0.0,0.0)
//val unitX : Point3D = Vector3D (1.0,0.0,0.0)
//val unitY : Point3D = Vector3D (0.0,1.0,0.0)
//val unitZ : Point3D = Vector3D (0.0,0.0,1.0)
//val length : Point3D -> float

type Node =
    {Name : string;
     Links : Link list}
and Link =
    | Dangling
    | Link of Node
//type Node =
//  {Name: string;
//   Links: Link list;}
//and Link =
//  | Dangling
//  | Link of Node

type StringMap<'T> = Map<string, 'T>
type Projections<'T, 'U> = ('T -> 'U) * ('U -> 'T)
//type StringMap<'T> = Map<string,'T>
//type Projections<'T,'U> = ('T -> 'U) * ('U -> 'T)

let fetch url = (url, http url)
//val fetch : url:string -> string * string

let rec map (f : 'T -> 'U) (l : 'T list) =
    match l with
    | h :: t -> f h :: map f t
    | [] -> []
//val map : f:('T -> 'U) -> l:'T list -> 'U list

let rec map<'T, 'U> (f : 'T -> 'U) (l : 'T list) =
    match l with
    | h :: t -> f h :: map f t
    | [] -> []
//val map : f:('T -> 'U) -> l:'T list -> 'U list

let getFirst (a, b, c) = a
//val getFirst : a:'a * b:'b * c:'c -> 'a

let mapPair f g (x, y) = (f x, g y)
//val mapPair : f:('a -> 'b) -> g:('c -> 'd) -> x:'a * y:'c -> 'b * 'd

compare
//val it : ('a -> 'a -> int) when 'a : comparison = <fun:it@269>

(=)
//val it : ('a -> 'a -> bool) when 'a : equality = <fun:it@272-1>

(<)
//val it : ('a -> 'a -> bool) when 'a : comparison = <fun:it@275-2>

(<=)
//val it : ('a -> 'a -> bool) when 'a : comparison = <fun:it@278-3>

(>)
//val it : ('a -> 'a -> bool) when 'a : comparison = <fun:it@281-4>

(>=)
//val it : ('a -> 'a -> bool) when 'a : comparison = <fun:it@284-5>

(min)
//val it : ('a -> 'a -> 'a) when 'a : comparison = <fun:it@287-6>

(max)
//val it : ('a -> 'a -> 'a) when 'a : comparison = <fun:it@290-7>


("abc", "def") < ("abc", "xyz")
//val it : bool = true

compare (10, 30) (10, 20)
//val it : int = 1

compare [10; 30] [10; 20]
//val it : int = 1

compare [|10; 30|] [|10; 20|]
//val it : int = 1

compare [|10; 20|] [|10; 30|]
//val it : int = -1

hash
//val it : ('a -> int) when 'a : equality = <fun:it@309-9>

hash 100
//val it : int = 100

hash "abc"
//val it : int = 536991770

hash (100, "abc")
//val it : int = 536990974

open NonStructuralComparison 

compare 4 1
//val it : int = 1

compare DateTime.Now (DateTime.Now.AddDays(1.0))
//val it : int = -1

compare (1,3) (5,4)
//error FS0001: Expecting a type supporting the operator '<' but given a tuple type

sprintf "result = %A" ([1], [true])
//val it : string = "result = ([1], [true])"

box
//val it : ('a -> obj) = <fun:it@335-12>

unbox
//val it : ('a -> 'b) = <fun:it@336-13>

box 1
//val it : obj = 1

box "abc"
//val it : obj = "abc"

let stringObj = box "abc"
//val stringObj: obj = "abc"

(unbox<string> stringObj)
//val it : string = "abc"

(unbox stringObj : string)
//val it : string = "abc"

(unbox stringObj : int)
//System.InvalidCastException: Specified cast is not valid.
//   at <StartupCode$FSI_0050>.$FSI_0050.main@()
//Stopped due to error

open System.IO
open System.Runtime.Serialization.Formatters.Binary

let writeValue outputStream x =
    let formatter = new BinaryFormatter()
    formatter.Serialize(outputStream, box x)

let readValue inputStream =
    let formatter = new BinaryFormatter()
    let res = formatter.Deserialize(inputStream)
    unbox res
//val writeValue : outputStream:Stream -> x:'T -> unit
//val readValue : inputStream:Stream -> 'a

open System.IO

let addresses = 
    Map.ofList [ "Jeff", "123 Main Street, Redmond, WA 98052"
                 "Fred", "987 Pine Road, Phila., PA 19116"
                 "Mary", "PO Box 112233, Palo Alto, CA 94301" ]

let fsOut = new FileStream("Data.dat", FileMode.Create)
writeValue fsOut addresses
fsOut.Close()
let fsIn = new FileStream("Data.dat", FileMode.Open)
let res : Map<string, string> = readValue fsIn
fsIn.Close()

res
//val it : Map<string,string> =
//  map
//    [("Fred", "987 Pine Road, Phila., PA 19116");
//     ("Jeff", "123 Main Street, Redmond, WA 98052");
//     ("Mary", "PO Box 112233, Palo Alto, CA 94301")]

let rec hcf a b =
    if a = 0 then b
    elif a < b then hcf a (b - a)
    else hcf (a - b) b
//val hcf : a:int -> b:int -> int

hcf 18 12
//val it : int = 6

hcf 33 24
//val it : int = 3

let hcfGeneric (zero, sub, lessThan) =
    let rec hcf a b =
        if a = zero then b
        elif lessThan a b then hcf a (sub b a)
        else hcf (sub a b) b
    hcf
//val hcfGeneric :
//  zero:'a * sub:('a -> 'a -> 'a) * lessThan:('a -> 'a -> bool) ->
//    ('a -> 'a -> 'a) when 'a : equality

let hcfInt = hcfGeneric (0, (-), (<))
let hcfInt64  = hcfGeneric (0L, (-), (<))
let hcfBigInt = hcfGeneric (0I, (-), (<))
//val hcfInt : (int -> int -> int)
//val hcfInt64 : (int64 -> int64 -> int64)
//val hcfBigInt :
//  (System.Numerics.BigInteger -> System.Numerics.BigInteger ->
//     System.Numerics.BigInteger)

hcfInt 18 12
//val it : int = 6

hcfBigInt 1810287116162232383039576I 1239028178293092830480239032I
//val it : System.Numerics.BigInteger = 33224 {IsEven = true;
//                                             IsOne = false;
//                                             IsPowerOfTwo = false;
//                                             IsZero = false;
//                                             Sign = 1;}

type Numeric<'T> = 
    { Zero : 'T
      Subtract : 'T -> 'T -> 'T
      LessThan : 'T -> 'T -> bool }

let intOps = {Zero = 0; Subtract = (-); LessThan = (<)}
let bigintOps = {Zero = 0I; Subtract = (-); LessThan = (<)}
let int64Ops = {Zero = 0L; Subtract = (-); LessThan = (<)}

let hcfGeneric (ops : Numeric<'T>) =
    let rec hcf a b =
        if a = ops.Zero then b
        elif ops.LessThan a b then hcf a (ops.Subtract b a)
        else hcf (ops.Subtract a b) b
    hcf

let hcfInt = hcfGeneric intOps
let hcfBigInt = hcfGeneric bigintOps
//type Numeric<'T> =
//  {Zero: 'T;
//   Subtract: 'T -> 'T -> 'T;
//   LessThan: 'T -> 'T -> bool;}
//val intOps : Numeric<int> = {Zero = 0;
//                             Subtract = <fun:intOps@438>;
//                             LessThan = <fun:intOps@438-1>;}
//val bigintOps : Numeric<System.Numerics.BigInteger> =
//  {Zero = 0;
//   Subtract = <fun:bigintOps@439>;
//   LessThan = <fun:bigintOps@439-1>;}
//val int64Ops : Numeric<int64> = {Zero = 0L;
//                                 Subtract = <fun:int64Ops@440>;
//                                 LessThan = <fun:int64Ops@440-1>;}
//val hcfGeneric : ops:Numeric<'T> -> ('T -> 'T -> 'T) when 'T : equality
//val hcfInt : (int -> int -> int)
//val hcfBigInt :
//  (System.Numerics.BigInteger -> System.Numerics.BigInteger ->
//     System.Numerics.BigInteger)

hcfInt 18 12
//val it : int = 6

hcfBigInt 1810287116162232383039576I 1239028178293092830480239032I
//val it : System.Numerics.BigInteger = 33224 {IsEven = true;
//                                             IsOne = false;
//                                             IsPowerOfTwo = false;
//                                             IsZero = false;
//                                             Sign = 1;}

type INumeric<'T> = 
    abstract Zero : 'T
    abstract Subtract : 'T * 'T -> 'T
    abstract LessThan : 'T * 'T -> bool

let intOps = 
    { new INumeric<int> with
          member ops.Zero = 0
          member ops.Subtract(x, y) = x - y
          member ops.LessThan(x, y) = x < y }

let hcfGeneric (ops : INumeric<'T>) = 
    let rec hcf a b = 
        if a = ops.Zero then b
        elif ops.LessThan(a, b) then hcf a (ops.Subtract(b, a))
        else hcf (ops.Subtract(a, b)) b
    hcf
//type INumeric<'T> =
//  interface
//    abstract member LessThan : 'T * 'T -> bool
//    abstract member Subtract : 'T * 'T -> 'T
//    abstract member Zero : 'T
//  end
//val intOps : INumeric<int>
//val hcfGeneric : ops:INumeric<'T> -> ('T -> 'T -> 'T) when 'T : equality

let convertToFloat x = float x
//val convertToFloat : x:int -> float

float 3.0 + float 1 + float 3L
//val it : float = 7.0

let inline convertToFloatAndAdd x y = float x + float y
//val inline convertToFloatAndAdd :
//  x: ^a -> y: ^b -> float
//    when  ^a : (static member op_Explicit :  ^a -> float) and
//          ^b : (static member op_Explicit :  ^b -> float)

let hcfGeneric (ops : INumeric<'T>) =
    let rec hcf a b =
        if a = ops.Zero then b
        elif ops.LessThan(a, b) then hcf a (ops.Subtract(b, a))
        else hcf (ops.Subtract(a, b)) b
    hcf
//val hcfGeneric : ops:INumeric<'T> -> ('T -> 'T -> 'T) when 'T : equality

let inline hcf a b = 
    hcfGeneric { new INumeric<'T> with
                     member ops.Zero = LanguagePrimitives.GenericZero<'T>
                     member ops.Subtract(x, y) = x - y
                     member ops.LessThan(x, y) = x < y } a b
//val inline hcf :
//  a: ^T -> b: ^T ->  ^T
//    when  ^T : (static member get_Zero : ->  ^T) and
//          ^T : (static member ( - ) :  ^T *  ^T ->  ^T) and  ^T : comparison

hcf 18 12
//val it : int = 6

hcf 18I 12I
//val it : System.Numerics.BigInteger = 6 {IsEven = true;
//                                         IsOne = false;
//                                         IsPowerOfTwo = false;
//                                         IsZero = false;
//                                         Sign = 1;}

let xobj = (1 :> obj)
//val xobj : obj = 1

let sobj = ("abc" :> obj)
//val sobj : obj = "abc"

let boxedObject = box "abc"
//val boxedObject : obj = "abc"

let downcastString = (boxedObject :?> string)
//val downcastString : string = "abc"

let xobj = box 1
//val xobj : obj = 1

let x = (xobj :?> string)
//System.InvalidCastException: Unable to cast object of type 'System.Int32' to type 'System.String'.

let checkObject (x : obj) =
    match x with
    | :? string -> printfn "The object is a string"
    | :? int -> printfn "The object is an integer"
    | _ -> printfn "The input is something else"
//val checkObject : x:obj -> unit

checkObject (box "abc")
//The object is a string
//val it : unit = ()

let reportObject (x : obj) =
    match x with
    | :? string as s -> printfn "The input is the string '%s'" s
    | :? int as d -> printfn "The input is the integer '%d'" d
    | _ -> printfn "the input is something else"
//val reportObject : x:obj -> unit

reportObject (box 17)
//The input is the integer '17'
//val it : unit = ()

open System
open System.Net

let dispose (c : IDisposable) = c.Dispose()

let obj1 = new WebClient()
let obj2 = new HttpListener()

dispose obj1
dispose obj2
//val dispose : c:IDisposable -> unit
//val obj1 : WebClient = System.Net.WebClient
//val obj2 : HttpListener

open System
open System.IO

let textReader  =
    if DateTime.Today.DayOfWeek = DayOfWeek.Monday
    then Console.In
    else File.OpenText("input.txt")
//error FS0001: This expression was expected to have type
//    TextReader    
//but here has type
//    StreamReader

let textReader =
    if DateTime.Today.DayOfWeek = DayOfWeek.Monday
    then Console.In
    else (File.OpenText("input.txt") :> TextReader)
//val textReader : TextReader
// WARNING: If input.txt does not exist then the above expression will error.
//System.IO.FileNotFoundException: Could not find file 'C:\Users\pdejoux\AppData\Local\Temp\input.txt'.

let getTextReader () : TextReader = (File.OpenText("input.txt") :> TextReader)
//val getTextReader : unit -> TextReader

open System

let disposeMany (cs : seq<#IDisposable>) = 
    for c in cs do c.Dispose()
//val disposeMany : cs:seq<#IDisposable> -> unit

let disposeMany (cs : seq<'T :> IDisposable>) = 
    for c in cs do c.Dispose()
//val disposeMany : cs:seq<#IDisposable> -> unit

Seq.concat
//val it : (seq<#seq<'b>> -> seq<'b>) = <fun:clo@638>

Seq.concat [[1;2;3]; [4;5;6]]
//val it : seq<int> = seq [1; 2; 3; 4; ...]

Seq.concat [[|1; 2; 3|]; [|4; 5; 6|]]
//val it : seq<int> = seq [1; 2; 3; 4; ...]

let getLengths inp = inp |> Seq.map (fun y -> y.Length)
//error FS0072: Lookup on object of indeterminate type based on information prior to this program point. A type annotation may be needed prior to this program point to constrain the type of the object. This may allow the lookup to be resolved.

let getLengths inp =
    inp |> Seq.map (fun (y : string) -> y.Length)
//val getLengths : inp:seq<string> -> seq<int>

let printSecondElements (inp : seq<'T * int>) =
    inp
    |> Seq.iter (fun (x, y) -> printfn "y = %d" x)
//warning FS0064: This construct causes code to be less generic than indicated by the type annotations. The type variable 'T has been constrained to be type 'int'.
//
//val printSecondElements : inp:seq<int * int> -> unit

type PingPong = Ping | Pong

let printSecondElements (inp : seq<PingPong * int>) =
    inp
    |> Seq.iter (fun (x, y) -> printfn "y = %d" x)
//error FS0001: The type 'PingPong' is not compatible with any of the types byte,int16,int32,int64,sbyte,uint16,uint32,uint64,nativeint,unativeint, arising from the use of a printf-style format string

let empties = Array.create 100 []
//error FS0030: Value restriction. The value 'empties' has been inferred to have generic type
//>     val empties : '_a list []    
//Either define 'empties' as a simple data term, make it a function with explicit arguments or, if you do not intend for it to be generic, add a type annotation.

let emptyList = []
let initialLists = ([], [2])
let listOfEmptyLists = [[]; []]
let makeArray () = Array.create 100 []
//val emptyList : 'a list
//val initialLists : 'a list * int list
//val listOfEmptyLists : 'a list list
//val makeArray : unit -> 'a list []

let empties = Array.create 100 []
//error FS0030: Value restriction. The value 'empties' has been inferred to have generic type
//    val empties : '_a list []    
//> Either define 'empties' as a simple data term, make it a function with explicit arguments or, if you do not intend for it to be generic, add a type annotation.

let empties : int list [] = Array.create 100 []
//val empties : int list [] =
//  [|[]; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; [];
//    []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; [];
//    []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; [];
//    []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; [];
//    []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; [];
//    []; []; []; []; []|]

let mapFirst = List.map fst
//error FS0030: Value restriction. The value 'mapFirst' has been inferred to have generic type
//    val mapFirst : (('_a * '_b) list -> '_a list)    
//Either make the arguments to 'mapFirst' explicit or, if you do not intend for it to be generic, add a type annotation.

let mapFirst inp = List.map fst inp
//val mapFirst : inp:('a * 'b) list -> 'a list

let mapFirst inp = inp |> List.map (fun (x, y) -> x)
//val mapFirst : inp:('a * 'b) list -> 'a list

let printFstElements = List.map fst >> List.iter (printf "res = %d")
//error FS0030: Value restriction. The value 'printFstElements' has been inferred to have generic type
//    val printFstElements : ((int * '_a) list -> unit)    
//Either make the arguments to 'printFstElements' explicit or, if you do not intend for it to be generic, add a type annotation.

let printFstElements inp = inp |> List.map fst |> List.iter (printf "res = %d")
//val printFstElements : inp:(int * 'a) list -> unit

let empties = Array.create 100 []
//error FS0030: Value restriction. The value 'empties' has been inferred to have generic type
//>     val empties : '_a list []    
//Either define 'empties' as a simple data term, make it a function with explicit arguments or, if you do not intend for it to be generic, add a type annotation.

let empties _ = Array.create 100 []
//val empties : 'a -> 'b list []

let empties () = Array.create 100 []
//val empties : unit -> 'a list []

let intEmpties : int list [] = empties ()
let stringEmpties : string list [] = empties ()
//val intEmpties : int list [] =
//  [|[]; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; [];
//    []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; [];
//    []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; [];
//    []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; [];
//    []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; [];
//    []; []; []; []; []|]
//val stringEmpties : string list [] =
//  [|[]; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; [];
//    []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; [];
//    []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; [];
//    []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; [];
//    []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; []; [];
//    []; []; []; []; []|]

let emptyLists = Seq.init 100 (fun _ -> [])
//error FS0030: Value restriction. The value 'emptyLists' has been inferred to have generic type
//    val emptyLists : seq<'_a list>    
//Either define 'emptyLists' as a simple data term, make it a function with explicit arguments or, if you do not intend for it to be generic, add a type annotation.

let emptyLists<'T> : seq<'T list> = Seq.init 100 (fun _ -> [])
//val emptyLists<'T> : seq<'T list>

Seq.length emptyLists
//val it : int = 100

emptyLists<int>
//val it : seq<int list> = seq [[]; []; []; []; ...]

emptyLists<string>
//val it : seq<string list> = seq [[]; []; []; []; ...]

let twice x = (x + x)
//val twice : int -> int

let twiceFloat (x : float) = x + x
//val twiceFloat : x:float -> float

let threeTimes x = (x + x + x)
let sixTimesInt64 (x : int64) = threeTimes x + threeTimes x
//val threeTimes : x:int64 -> int64
//val sixTimesInt64 : x:int64 -> int64