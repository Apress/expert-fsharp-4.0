open System
let i = 2
Console.WriteLine("Input a number:")
let v = Int32.Parse(Console.ReadLine())
Console.WriteLine(i * v)
//.method public static void  main@() cil managed
//{
//  .entrypoint
//  // Code size       38 (0x26)
//  .maxstack  8
//  IL_0000:  ldstr      "Input a number:"
//  IL_0005:  call       void [mscorlib]System.Console::WriteLine(string)
//  IL_000a:  call       string [mscorlib]System.Console::ReadLine()
//  IL_000f:  call       int32 [mscorlib]System.Int32::Parse(string)
//  IL_0014:  stsfld     int32 '<StartupCode$program>'.$Program::v@4
//  IL_0019:  ldc.i4.2
//  IL_001a:  call       int32 Program::get_v()
//  IL_001f:  mul
//  IL_0020:  call       void [mscorlib]System.Console::WriteLine(int32)
//  IL_0025:  ret
//} // end of method $Program::main@
