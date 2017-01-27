#load "APoint.fs"
#load "RadialPoint.fs"

//type APoint =
//    class
//        new : unit -> APoint
//        new : angle:float * radius:float -> APoint
//        member Stretch : l:float -> APoint
//        member Warp : f:(float -> float) -> APoint
//        member Angle : float
//        member Radius : float
//        static member Circle : n:int -> APoint list
//    end

// C# signature for the unadjusted APoint class of Listing 19-1
//[Serializable]
//public class APoint
//{
//    public APoint();
//    public APoint(double angle, double radius);
//
//    public double Angle { get; }
//    public double Radius { get; }
//
//    public static Microsoft.FSharp.Collections.FSharpList<APoint> Circle(int n);
//    public APoint Stretch(double l);
//    public APoint Warp(Microsoft.FSharp.Core.FSharpFunc<double, double> f);
//}

//type RadialPoint =
//  class
//    new : unit -> RadialPoint
//    new : angle:float * radius:float -> RadialPoint
//    member Stretch : factor:float -> RadialPoint
//    member Warp : transform:System.Converter<float,float> -> RadialPoint
//    member Angle : float
//    member Radius : float
//    static member Circle : count:int -> seq<RadialPoint>
//  end

// C# signature for the unadjusted RadialPoint class of Listing 19-2
//[Serializable]
//public class RadialPoint
//{
//    public RadialPoint();
//    public RadialPoint(double angle, double radius);
//
//    public double Angle { get; }
//    public double Radius { get; }
//
//    public static IEnumerable<RadialPoint> Circle(int count);
//    public RadialPoint Stretch(double factor);
//    public RadialPoint Warp(Converter<double, double> transform);
//}

let map2 f inp = List.map (List.map f) inp
//val map2 : f:('a -> 'b) -> inp:'a list list -> 'b list list

//List.hd (x :: rest) =>  x 
//List.concat (List.map (List.filter f) inp)  =>  List.filter f (List.concat inp) 

type Var =  string
type Prop = 
    | And of Prop * Prop
    | Var of Var
    | Not of Prop
    | Exists of Var * Prop
    | False 
//type Var = string
//type Prop =
//  | And of Prop * Prop
//  | Var of Var
//  | Not of Prop
//  | Exists of Var * Prop
//  | False

let x = 1
//val x : int = 1

let now = System.DateTime.Now
//val now : System.DateTime = 29/07/2012 11:12:16 PM

let add I J = I + J
//val add : I:int -> J:int -> int

let add i j = i + j
//val add : i:int -> j:int -> int

#r @".\..\..\20Libraries\packages\FSPowerPack.Community.2.1.3.1\Lib\Net40\FSharp.PowerPack.dll"
open Microsoft.FSharp.Math
let f (A : matrix) (B : matrix) = A + B
//val f : A:matrix -> B:matrix -> Matrix<float>

let Monday = 1
//val Monday : int = 1

let I x = x
//val I : x:'a -> 'a

type Protocol = unit

type HardwareDevice =
    abstract member ID : string
    abstract member SupportedProtocols : seq<Protocol>
//type HardwareDevice =
//  interface
//    abstract member ID : string
//    abstract member SupportedProtocols : seq<Protocol>
//  end

type HashTable<'Key, 'Value> =
    abstract member Add : 'Key * 'Value -> unit
    abstract member ContainsKey : 'Key -> bool
    abstract member ContainsValue : 'Value -> bool
//type HashTable<'Key,'Value> =
//  interface
//    abstract member Add : 'Key * 'Value -> unit
//    abstract member ContainsKey : 'Key -> bool
//    abstract member ContainsValue : 'Value -> bool
//  end

// From the docs. The actual source is Microsoft.Practices.EnterpriseLibrary.Security.Cryptography
type IHashProvider<'Key> =
    abstract member CreateHash : 'Key -> byte[]
    abstract member CompareHash : byte[] * byte[] -> bool
//type IHashProvider<'Key> =
//  interface
//    abstract member CompareHash : byte [] * byte [] -> bool
//    abstract member CreateHash : 'Key -> byte []
//  end

type HashTable<'Key,'Value> =
    abstract member Create : IHashProvider<'Key> -> HashTable<'Key,'Value> 
//type HashTable<'Key,'Value> =
//  interface
//    abstract member Create : IHashProvider<'Key> -> HashTable<'Key,'Value>
//  end

let f x y z = x + y + z
//val f : x:int -> y:int -> z:int -> int

let f (x, y, z) = x + y + z
//val f : x:int * y:int * z:int -> int

let divmod n m = (n / m, n % m)
let map f x = ...
let fold f z x = ...

//val divmod : n:int -> m:int -> int * int

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
x ||| y  -- bitwise or, also for working with enumeration flags 
x &&& y  -- bitwise and, also for working with enumeration flags
x ^^^ y  -- bitwise exclusive or, also for working with enumeration flags

x && y   -- lazy/short-circuit and
x || y   -- lazy/short-circuit or 

let methods = 
    System.AppDomain.CurrentDomain.GetAssemblies()
    |> List.ofArray 
    |> List.map (fun assem -> assem.GetTypes()) 
    |> Array.concat

open System

[<AbstractClass>]
type Organization() =
    abstract member Chief : string
    abstract member Underlings : string list
//type Organization =
//  class
//    new : unit -> Organization
//    abstract member Chief : string
//    abstract member Underlings : string list
//  end

let thePlayers = {
    new Organization() with
        member x.Chief = "Peter Quince" 
        member x.Underlings = 
            ["Francis Flute"; "Robin Starveling"; "Tom Snout"; "Snug"; "Nick Bottom"] 
    interface IDisposable with
        member x.Dispose() = ()}
//val thePlayers : Organization
