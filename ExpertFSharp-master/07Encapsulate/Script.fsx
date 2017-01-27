let generateTicket =
    let count = ref 0
    (fun () -> incr count; !count)
//val generateTicket : (unit -> int)

type IPeekPoke =
    abstract member Peek: unit -> int
    abstract member Poke: int -> unit
//type IPeekPoke =
//  interface
//    abstract member Peek : unit -> int
//    abstract member Poke : int -> unit
//  end

let makeCounter initialState =
    let mutable state = initialState
    {new IPeekPoke with
        member x.Poke n = state <- state + n
        member x.Peek() = state}
//val makeCounter : initialState:int -> IPeekPoke

type TicketGenerator() =
    // Note: let bindings in a type definition are implicitly private to the object
    // being constructed. Members are implicitly public.
    let mutable count = 0

    member x.Next() =
        count <- count + 1;
        count

    member x.Reset () =
        count <- 0
//type TicketGenerator =
//  class
//    new : unit -> TicketGenerator
//    member Next : unit -> int
//    member Reset : unit -> unit
//  end

type IStatistic<'T, 'U> =
    abstract Record : 'T -> unit
    abstract Value : 'U
//type IStatistic<'T,'U> =
//  interface
//    abstract member Record : 'T -> unit
//    abstract member Value : 'U
//  end

let makeAverager(toFloat: 'T -> float ) =

    let mutable count = 0
    let mutable total = 0.0

    {new IStatistic<'T, float> with
          member stat.Record(x) = count <- count + 1; total <- total + toFloat x
          member stat.Value = (total / float count)}
//val makeAverager : toFloat:('T -> float) -> IStatistic<'T,float>

open System

module public VisitorCredentials =

   /// The internal table of permitted visitors and the
   /// days they are allowed to visit.
   let private  visitorTable =
       dict [("Anna", set [DayOfWeek.Tuesday; DayOfWeek.Wednesday]);
             ("Carolyn", set [DayOfWeek.Friday]) ]

   /// This is the function to check if a person is a permitted visitor.
   /// Note: this is public and can be used by external code
   let public checkVisitor(person) =
       visitorTable.ContainsKey(person) &&
       visitorTable.[person].Contains(DateTime.Today.DayOfWeek)

   /// This is the function to return all known permitted visitors.
   /// Note: this is internal and can only be used by code in this assembly.
   let internal allKnownVisitors() =
       visitorTable.Keys
//module VisitorCredentials = begin
//  val private visitorTable :
//    Collections.Generic.IDictionary<string,Set<DayOfWeek>>
//  val checkVisitor : person:string -> bool
//  val internal allKnownVisitors :
//    unit -> Collections.Generic.ICollection<string>
//end

module public GlobalClock =

    type TickTock = Tick | Tock

    let mutable private clock = Tick

    let private tick = new Event<TickTock>()

    let internal oneTick() =
        (clock <- match clock with Tick -> Tock | Tock -> Tick);
        tick.Trigger (clock)

    let tickEvent = tick.Publish
//module GlobalClock = begin
//  type TickTock =
//    | Tick
//    | Tock
//  val mutable private clock : TickTock = Tick
//  val private tick : Event<TickTock>
//  val internal oneTick : unit -> unit
//  val tickEvent : IEvent<TickTock> = <published event>
//end

module internal TickTockDriver =

    open System.Threading

    let timer = new Timer(callback = (fun _ -> GlobalClock.oneTick()),
                          state = null, dueTime = 0, period = 100)
//module internal TickTockDriver = begin
//  val timer : Threading.Timer
//end

module TickTockListener =
   GlobalClock.tickEvent.Add(function
       | GlobalClock.Tick -> printfn "tick!"
       | GlobalClock.Tock -> printfn "tock!")
//> tick!
//tock!
//tick!
//tock!
//tick!
//tock!
//...

open System.Collections.Generic

type public SparseVector () =

    let elems = new SortedDictionary<int, float>()

    member internal vec.Add (k, v) = elems.Add(k ,v)

    member public vec.Count = elems.Keys.Count

    member vec.Item
        with public get i =
            if elems.ContainsKey(i) then elems.[i]
            else 0.0
        and internal set i v =
            elems.[i] <- v
//type SparseVector =
//  class
//    new : unit -> SparseVector
//    member internal Add : k:int * v:float -> unit
//    member Count : int
//    member Item : i:int -> float with get
//    member internal Item : i:int -> float with set
//  end

type Vector2D =
    {DX : float; DY : float}

module Vector2DOps =
    let length v = sqrt (v.DX * v.DX + v.DY * v.DY)
    let scale k v = {DX = k * v.DX; DY = k * v.DY}
    let shiftX x v = {v with DX = v.DX + x}
    let shiftY y v = {v with DY = v.DY + y}
    let shiftXY (x, y) v = {DX = v.DX + x; DY = v.DY + y}
    let zero = {DX = 0.0; DY = 0.0}
    let constX dx = {DX = dx; DY = 0.0}
    let constY dy = {DX = 0.0; DY = dy} 
//type Vector2D =
//  {DX: float;
//   DY: float;}
//module Vector2DOps = begin
//  val length : v:Vector2D -> float
//  val scale : k:float -> v:Vector2D -> Vector2D
//  val shiftX : x:float -> v:Vector2D -> Vector2D
//  val shiftY : y:float -> v:Vector2D -> Vector2D
//  val shiftXY : x:float * y:float -> v:Vector2D -> Vector2D
//  val zero : Vector2D = {DX = 0.0;
//                         DY = 0.0;}
//  val constX : dx:float -> Vector2D
//  val constY : dy:float -> Vector2D
//end

#load "AcmeWidgetsTwoTypes.fs"
//[Loading C:\...\AcmeWidgetsTwoTypes.fs]
//
//namespace FSI_0007.Acme.Widgets
//  type Wheel =
//    | Square
//    | Round
//    | Triangle
//  type Widget =
//    {id: int;
//     wheels: Wheel list;
//     size: string;}

#load "AcmeWidgetsTwoNamespacesFail.fs"
//[Loading C:\...\AcmeWidgetsTwoNamespacesFail.fs]
//
//
//AcmeWidgetsTwoNamespacesFail.fs(5,54): error FS0039: The namespace or module 'Acme' is not defined

#load "AcmeWidgetsTwoNamespaces1Of2.fs"
#load "AcmeWidgetsTwoNamespaces2Of2.fs"
//[Loading C:\...\AcmeWidgetsTwoNamespaces1Of2.fs]
//
//namespace FSI_0002.Acme.Widgets
//  type Lever =
//    | PlasticLever
//    | WoodenLever
//
//[Loading C:\...\AcmeWidgetsTwoNamespaces2Of2.fs]
//
//namespace FSI_0003.Acme.Suppliers
//  type LeverSupplier =
//    {name: string;
//     leverKind: Acme.Widgets.Lever;}

#load "NamespacesVersusLayout.fs"
//[Loading C:\...\NamespacesVersusLayout.fs]
//
//namespace FSI_0002.WithoutIndent
//  type WatchFace =
//    | Digital
//    | Analogue
//namespace FSI_0002.WithIndent
//  type WatchPower =
//    | Battery
//    | Spring
//namespace FSI_0002.EmptyOuter.Inner
//  type WatchKind =
//    | Wrist
//    | Fob

type Vector2D =
    {DX: float; DY: float}

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Vector2D =
     let length v = sqrt(v.DX * v.DX + v.DY * v.DY)

//type Vector2D =
//  {DX: float;
//   DY: float;}
//module Vector2D = begin
//  val length : v:Vector2D -> float
//end

[<RequireQualifiedAccess>]
module Vector2D =
     let length v = sqrt(v.DX * v.DX + v.DY * v.DY)
     let zero = {DX=0.0; DY=0.0}
//module Vector2D = begin
//  val length : v:Vector2D -> float
//  val zero : Vector2D = {DX = 0.0;
//                         DY = 0.0;}
//end

open Vector2D 
//error FS0892: This declaration opens the module 'FSI_0004.Vector2D', which is marked as 'RequireQualifiedAccess'. Adjust your code to use qualified references to the elements of the module instead, e.g. 'List.map' instead of 'map'. This change will ensure that your code is robust as new constructs are added to libraries.

#load "Acme.Widgets.WidgetWheels.fs"
//[Loading C:\...\Acme.Widgets.WidgetWheels.fs]
//
//namespace FSI_0006.Acme.Widgets
//  type Wheel =
//    | Square
//    | Triangle
//    | Round
//  val wheelCornerCount : System.Collections.Generic.IDictionary<Wheel,int>

#load "Acme.Components.fs"

swap (3,4)
//[Loading C:\...\Acme.Components.fs]
//
//namespace FSI_0011.Acme.Compenents
//  val swap : x:'a * y:'b -> 'b * 'a
//
//
//
// error FS0039: The value or constructor 'swap' is not defined

#load "AutoOpenModule.fs"
open Acme.NumberTheory

(7).IsPrime
//[Loading C:\...\AutoOpenModule.fs]
//
//namespace FSI_0002.Acme.NumberTheory
//  val private isPrime : i:int -> bool
//  type Int32 with
//    member IsPrime : bool
//
//
//val it : bool = true

5.IsPrime
// error FS1156: This is not a valid numeric literal. Sample formats include 4, 0x4, 0b0100, 4L, 4UL, 4u, 4s, 4us, 4y, 4uy, 4.0, 4.0f, 4I.

#load "AutoOpenAssembly.fs"
open Acme.NumberTheory

(3).IsPrime
// error FS0039: The field, constructor or member 'IsPrime' is not defined
 
#I "Vector"
//--> Added 'C:\...\Vector' to library include path

#load "vector.fs"
//[Loading C:\...\vector.fs]
//
//namespace FSI_0002.Acme.Collections
//  type SparseVector =
//    class
//      new : unit -> SparseVector
//      member Add : k:int * v:float -> unit
//      member Item : i:int -> float with get
//      member Item : i:int -> float with set
//    end

#load "vector.fsi" "vector.fs"
//[Loading C:\...\vector.fsi
// Loading C:\...\vector.fs]
//
//namespace FSI_0003.Acme.Collections
//  type SparseVector =
//    class
//      new : unit -> SparseVector
//      member Add : int * float -> unit
//      member Item : int -> float with get
//      member Item : i:int -> float with set
//    end

//TODO: Add these source files to  /Charting
#I "Charting"
#load "charting.fsi" "charting.fs" "charting.fsx"

//TODO: Add these source files to  /Matrix
#I "Matrix"
#load "matrix.fsi" "matrix.fs" "vector.fsi" "vector.fs"