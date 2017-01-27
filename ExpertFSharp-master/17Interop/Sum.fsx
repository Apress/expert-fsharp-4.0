#I @"CInteropDLL\Debug"
System.Environment.CurrentDirectory <- __SOURCE_DIRECTORY__ + @"\CInteropDLL\Debug"
open System.Runtime.InteropServices

module CInterop =
    [<DllImport("CInteropDLL", CallingConvention = CallingConvention.Cdecl)>]
    extern int Sum(int i, int j)

printf "Sum(1, 1) = %d\n" (CInterop.Sum(1, 1));

//--> Added 'C:\...\CInteropDLL\Debug' to library include path
//
//Sum(1, 1) = 2
//
//module CInterop = begin
//  val Sum : i:int * j:int -> int
//end
//val it : unit = ()

//C:\...>fsi Sum.fsx
//Sum(1, 1) = 2