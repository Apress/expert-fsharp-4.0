#I @"CInteropDLL\Debug"
System.Environment.CurrentDirectory <- __SOURCE_DIRECTORY__ + @"\CInteropDLL\Debug"
open System.Runtime.InteropServices

module CInterop =
    [<StructLayout(LayoutKind.Sequential)>]
    type ObjComplex =
        val mutable re : double
        val mutable im : double

        new() = {re = 0.0; im = 0.0}
        new(r : double, i : double) = {re = r; im = i}

     [<DllImport("CInteropDLL", EntryPoint = "ZeroC", CallingConvention = CallingConvention.Cdecl)>]
    extern void ObjZeroC(ObjComplex c)

let oc = CInterop.ObjComplex(2.0, 1.0)
printf "oc = %f + %fi\n" oc.re oc.im
CInterop.ObjZeroC(oc)
printf "oc = %f + %fi\n" oc.re oc.im

//--> Added 'C:\...\CInteropDLL\Debug' to library include path
//
//ObjZeroC.fsx(7,10): warning FS0009: Uses of this construct may result in the generation of unverifiable .NET IL code. This warning can be disabled using '--nowarn:9' or '#nowarn "9"'.
//oc = 2.000000 + 1.000000i
//oc = 0.000000 + 0.000000i
//
//module CInterop = begin
//  type ObjComplex =
//    class
//      new : unit -> ObjComplex
//      new : r:double * i:double -> ObjComplex
//      val mutable re: double
//      val mutable im: double
//    end
//  val ObjZeroC : c:ObjComplex -> unit
//end
//val oc : CInterop.ObjComplex
//val it : unit = ()

//C:\...>fsi ObjZeroC.fsx
//
//
//ObjZeroC.fsx(7,10): warning FS0009: Uses of this construct may result in the gen
//eration of unverifiable .NET IL code. This warning can be disabled using '--nowa
//rn:9' or '#nowarn "9"'.
//oc = 2.000000 + 1.000000i
//oc = 0.000000 + 0.000000i