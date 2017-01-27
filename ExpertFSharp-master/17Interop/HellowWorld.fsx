#I @"CInteropDLL\Debug"
System.Environment.CurrentDirectory <- __SOURCE_DIRECTORY__ + @"\CInteropDLL\Debug"
open System.Runtime.InteropServices

module CInterop =
    [<DllImport("CInteropDLL", CallingConvention = CallingConvention.Cdecl)>]
    extern void HelloWorld()

CInterop.HelloWorld()

//--> Added 'C:\...\CInteropDLL\Debug' to library include path
//
//module CInterop = begin
//  val HelloWorld : unit -> unit
//end
//val it : unit = ()

//C:\...>fsi HellowWorld.fsx
//Hello C world invoked by F#!