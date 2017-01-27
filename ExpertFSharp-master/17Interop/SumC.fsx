#I @"CInteropDLL\Debug"
System.Environment.CurrentDirectory <- __SOURCE_DIRECTORY__ + @"\CInteropDLL\Debug"
open System.Runtime.InteropServices

module CInterop =
    [<Struct; StructLayout(LayoutKind.Sequential)>]
    type Complex =
        val mutable re : double
        val mutable im : double

        new(r, i) = {re = r; im = i}

    [<DllImport("CInteropDLL", CallingConvention = CallingConvention.Cdecl)>]
    extern Complex SumC(Complex c1, Complex c2)

//--> Added 'C:\...\CInteropDLL\Debug' to library include path
//
//SumC.fsx(7,10): warning FS0009: Uses of this construct may result in the generation of unverifiable .NET IL code. This warning can be disabled using '--nowarn:9' or '#nowarn "9"'.
//
//module CInterop = begin
//  type Complex =
//    struct
//      new : r:double * i:double -> Complex
//      val mutable re: double
//      val mutable im: double
//    end
//  val SumC : c1:Complex * c2:Complex -> Complex
//end

let c1 = CInterop.Complex(1.0, 0.0)
let c2 = CInterop.Complex(0.0, 1.0)

let mutable c3 = CInterop.SumC(c1, c2)
printf "c3 = SumC(c1, c2) = %f + %fi\n" c3.re c3.im

//c3 = SumC(c1, c2) = 1.000000 + 1.000000i
//
//val c1 : CInterop.Complex = FSI_0002+CInterop+Complex
//val c2 : CInterop.Complex = FSI_0002+CInterop+Complex
//val mutable c3 : CInterop.Complex = FSI_0002+CInterop+Complex
//val it : unit = ()

//--> Added 'C:\...\CInteropDLL\Debug' to library include path
//
//SumC.fsx(7,10): warning FS0009: Uses of this construct may result in the generation of unverifiable .NET IL code. This warning can be disabled using '--nowarn:9' or '#nowarn "9"'.
//> error FS0193: internal error: Could not load type 'CInterop' from assembly 'FSI-ASSEMBLY, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'.