#I @"CInteropDLL\Debug"
System.Environment.CurrentDirectory <- __SOURCE_DIRECTORY__ + @"\CInteropDLL\Debug"
open System.Runtime.InteropServices

[<DllImport("CInteropDLL", CallingConvention = CallingConvention.Cdecl)>]
extern void sayhello(System.Text.StringBuilder sb, int sz)

let sb = new System.Text.StringBuilder(50)
sayhello(sb, 50)
printf "%s\n" (sb.ToString())

//--> Added 'C:\...\CInteropDLL\Debug' to library include path
//
//Hello from C code!
//
//val sayhello : sb:System.Text.StringBuilder * sz:int -> unit
//val sb : System.Text.StringBuilder = Hello from C code!
//val it : unit = ()

//C:\...>fsi SayHello.fsx
//Hello from C code!