#I @"CInteropDLL\Debug"
System.Environment.CurrentDirectory <- __SOURCE_DIRECTORY__ + @"\CInteropDLL\Debug"
open System.Runtime.InteropServices

[<DllImport("CInteropDLL", CallingConvention = CallingConvention.Cdecl)>]
extern void echo(string s)

echo "abc"

//--> Added 'C:\...\CInteropDLL\Debug' to library include path
//
//val echo : s:string -> unit
//val it : unit = ()

//C:\...>fsi Echo.fsx
//abc