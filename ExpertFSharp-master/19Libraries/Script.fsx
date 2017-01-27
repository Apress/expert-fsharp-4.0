namespace global
open System
type APoint(angle,radius) = 
    member x.Angle = angle
    member x.Radius = radius
    member x.Stretch(l) = APoint(angle=x.Angle, radius=x.Radius * l)
    member x.Warp(f) = APoint(angle=f(x.Angle), radius=x.Radius)
    static member Circle(n) = 
        [ for i in 1..n -> APoint(angle=2.0*Math.PI/float(n), radius=1.0) ]
    new() = APoint(angle=0.0, radius=0.0)

//type APoint = 
//     new : unit -> APoint
//     new : angle:double * radius:double -> APoint
//     static member Circle : n:int -> APoint list
//     member Stretch : l:double -> APoint
//     member Warp : f:(double -> double) -> APoint
//     member Angle : double
//     member Radius : double

// C# signature for the unadjusted APoint class of Listing 19-1
//public class APoint {
//     public APoint();
//     public APoint(double angle, double radius);
//     public static Microsoft.FSharp.Collections.List<APoint> Circle(int count);
//     public APoint Stretch(double factor);
//     public APoint Warp(Microsoft.FSharp.Core.FastFunc<double,double> transform);
//     public double Angle { get; }
//     public double Radius  { get; }
//}

namespace ExpertFSharp.Types

open System

module AssemblyAttributes = 
    [<assembly: System.Runtime.InteropServices.ComVisible(false);
      assembly: System.CLSCompliant(true)>]
    do()

type RadialPoint(angle,radius) = 
    member x.Angle = angle
    member x.Radius = radius
    member x.Stretch(factor) = RadialPoint(angle=x.Angle, radius=x.Radius * factor)
    member x.Warp(transform:Converter<_,_>) = 
        RadialPoint(angle=transform.Invoke(x.Angle), radius=x.Radius)
    static member Circle(count) = 
        seq { for i in 1..count ->
                  RadialPoint(angle=2.0*Math.PI/float(count), radius=1.0) }
    new() = RadialPoint(angle=0.0, radius=0.0)

//type RadialPoint = 
//     new : unit -> RadialPoint
//     new : angle:double * radius:double -> RadialPoint
//     static member Circle : count:int -> seq<RadialPoint>
//     member Stretch : factor:double -> RadialPoint
//     member Warp : transform:System.Converter<double,double> -> RadialPoint
//     member Angle : double
//     member Radius : double

// C# signature for the unadjusted RadialPoint class of Listing 19-2
//public class RadialPoint {
//   public RadialPoint();
//   public RadialPoint(double angle, double radius);
//   public static System.Collections.Generic.IEnumerable<RadialPoint> Circle(int count);
//   public RadialPoint Stretch(double factor);
//   public RadialPoint Warp(System.Converter<double,double> transform);
//   public double Angle { get; }
//   public double Radius  { get; }
//}

let map2 f inp = List.map (List.map f) inp

List.hd (x :: rest) =>  x 

List.concat (List.map (List.filter f) inp)  =>  List.filter f (List.concat inp) 

type Var =  string
type Prop = 
    | And of Prop * Prop
    | Var of Var
    | Not of Prop
    | Exists of Var * Prop
    | False 

let x = 1
let now = System.DateTime.Now

let add I J = I+J
let add i j = i + j

let f (A:matrix) (B:matrix) = A+B
let Monday = 1
let I x = x

type HardwareDevice with 
      ...
      member ID: string
      member SupportedProtocols: seq<Protocol>

type HashTable<'Key,'Value> with
      ...
      member Add           : 'Key * 'Value -> unit
      member ContainsKey   : 'Key -> bool
      member ContainsValue : 'Value -> bool

type HashTable<'Key,'Value> with
       static member Create : IHashProvider<'Key> -> HashTable<'Key,'Value> 

let f x y z = x + y + z
let f (x,y,z) = x + y + z

let divmod n m = ...
let map f x = ...
let fold f z x = ...

val divmod : int -> int -> int * int

f >> g   -- forward composition
g << f   -- reverse composition
x |> f   -- forward pipeline 
f <| x   -- reverse pipeline 

x |> ignore   -- throwing away a value

x + y    -- overloaded addition (including string concatenation) 
x - y    -- overloaded subtraction 
x * y    -- overloaded multiplication 
x / y    -- overloaded division 
x % y    -- overloaded modulus 

x <<< y  -- bitwise left shift 
x >>> y  -- bitwise right shift 
x ||| y  -- bitwise left shift, also for working with enumeration flags 
x &&& y  -- bitwise right shift, also for working with enumeration flags
x ^^^ y  -- bitwise left shift, also for working with enumeration flags

x && y   -- lazy/short-circuit and
x || y   -- lazy/short-circuit or 

let methods = 
    System.AppDomain.CurrentDomain.GetAssemblies
    |> List.ofArray 
    |> List.map (fun assem -> assem.GetTypes()) 
    |> Array.concat

let thePlayers = 
       { new Organization() with
             member x.Chief = "Peter Quince" 
             member x.Underlings = 
                 [ "Francis Flute"; "Robin Starveling"; 
                   "Tom Snout"; "Snug"; "Nick Bottom"] 
         interface IDisposable with
             member x.Dispose() = ()  }
