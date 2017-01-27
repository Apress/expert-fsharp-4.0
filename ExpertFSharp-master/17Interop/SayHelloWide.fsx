#I @"CInteropDLL\Debug"
System.Environment.CurrentDirectory <- __SOURCE_DIRECTORY__ + @"\CInteropDLL\Debug"
open System.Runtime.InteropServices

open System.Text
[<DllImport("CInteropDLL", CallingConvention = CallingConvention.Cdecl)>]
extern void sayhellow([<MarshalAs(UnmanagedType.LPWStr)>]StringBuilder sb, int sz)

let sb = new System.Text.StringBuilder(50)
sayhellow(sb, 50)
printf "%s\n" (sb.ToString())

//--> Added 'C:\...\CInteropDLL\Debug' to library include path
//
//Hello from C code!
//
//val sayhellow : sb:System.Text.StringBuilder * sz:int -> unit
//val sb : System.Text.StringBuilder = Hello from C code!
//val it : unit = ()

//C:\...>fsi SayHelloWide.fsx
//Hello from C code!