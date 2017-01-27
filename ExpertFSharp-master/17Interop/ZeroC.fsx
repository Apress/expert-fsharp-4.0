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

    [<DllImport("CInteropDLL", CallingConvention = CallingConvention.Cdecl)>]
    extern void ZeroC(Complex* c)

//--> Added 'C:\...\CInteropDLL\Debug' to library include path
//
//ZeroC.fsx(7,10): warning FS0009: Uses of this construct may result in the generation of unverifiable .NET IL code. This warning can be disabled using '--nowarn:9' or '#nowarn "9"'.
//
//module CInterop = begin
//  type Complex =
//    struct
//      new : r:double * i:double -> Complex
//      val mutable re: double
//      val mutable im: double
//    end
//  val SumC : c1:Complex * c2:Complex -> Complex
//  val ZeroC : c:nativeptr<Complex> -> unit
//end

let c1 = CInterop.Complex(1.0, 0.0)
let c2 = CInterop.Complex(0.0, 1.0)

let mutable c4 = CInterop.SumC(c1, c2)

printf "c4 = SumC(c1, c2) = %f + %fi\n" c4.re c4.im

CInterop.ZeroC(&&c4)

printf "c4 = %f + %fi\n" c4.re c4.im

//ZeroC.fsx(42,16): warning FS0051: The use of native pointers may result in unverifiable .NET IL code
//c4 = SumC(c1, c2) = 1.000000 + 1.000000i
//c4 = 0.000000 + 0.000000i
//
//val c1 : CInterop.Complex = FSI_0002+CInterop+Complex
//val c2 : CInterop.Complex = FSI_0002+CInterop+Complex
//val mutable c4 : CInterop.Complex = FSI_0002+CInterop+Complex
//val it : unit = ()

//C:\...>fsi ZeroC.fsx
//
//
//ZeroC.fsx(7,10): warning FS0009: Uses of this construct may result in the genera
//tion of unverifiable .NET IL code. This warning can be disabled using '--nowarn:
//9' or '#nowarn "9"'.
//
//
//ZeroC.fsx(42,16): warning FS0051: The use of native pointers may result in unver
//ifiable .NET IL code
//
//
//error FS0193: internal error: Could not load type 'CInterop' from assembly 'FSI-
//ASSEMBLY, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'.