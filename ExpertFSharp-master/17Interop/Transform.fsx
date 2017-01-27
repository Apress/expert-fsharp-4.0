#I @"CInteropDLL\Debug"
System.Environment.CurrentDirectory <- __SOURCE_DIRECTORY__ + @"\CInteropDLL\Debug"
open System.Runtime.InteropServices

type Callback = delegate of int -> int

[<DllImport("CInteropDLL", CallingConvention = CallingConvention.Cdecl)>]
extern void transformArray(int[] data, int count, Callback transform)

open System

let anyToString any = sprintf "%A" any
let data = [|1; 2; 3|]
printf "%s\n" (String.Join("; ", (Array.map anyToString data)))

transformArray(data, data.Length, new Callback(fun x -> x + 1))
printf "%s\n" (String.Join("; ", (Array.map anyToString data)))

//--> Added 'C:\...\17Interop\CInteropDLL\Debug' to library include path
//
//1; 2; 3
//2; 3; 4
//
//type Callback =
//  delegate of int -> int
//val transformArray : data:int [] * count:int * transform:Callback -> unit
//val anyToString : any:'a -> string
//val data : int [] = [|2; 3; 4|]
//val it : unit = ()

//C:\...\17Interop>fsi Transform.fsx
//1; 2; 3
//2; 3; 4