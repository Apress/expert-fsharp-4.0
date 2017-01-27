open System.Windows.Forms

let form = new Form(Width = 400, Height = 300,
                    Visible = true, Text = "F# Forms Sample")
//val form : System.Windows.Forms.Form =
//  System.Windows.Forms.Form, Text: F# Forms Sample

#if COMPILED
// Run the main code
System.Windows.Forms.Application.Run(form)
#endif

C:\...\19Debugging>fsc -a --doc:whales.xml ..\07Encapsulate\Whales\whales.fs 
//Microsoft (R) F# Compiler version 14.0.23020.0
//Copyright (c) Microsoft Corporation. All Rights Reserved.
//
//<?xml version="1.0" encoding="utf-8"?>
//<doc>
//<assembly><name>whales</name></assembly>
//<members>
//<member name="T:Whales.Fictional.WhaleKind">
//<summary>
// The three kinds of whales we cover in this release
//</summary>
//</member>
//<member name="P:Whales.Fictional.whales">
//<summary>
// The collected whales
//</summary>
//</member>
//<member name="P:Whales.Fictional.orca">
//<summary>
// This whale is for experimental use only
//</summary>
//</member>
//<member name="P:Whales.Fictional.bluey">
//<summary>
// The backup whale
//</summary>
//</member>
//<member name="P:Whales.Fictional.moby">
//<summary>
// The main whale
//</summary>
//</member>
//</members>
//</doc>

let isPalindrome (str : string) =
    let rec check(s : int, e : int) =
        if s = e then true
        elif str.[s] <> str.[e] then false
        else check(s + 1, e - 1)

    check(0, str.Length - 1)
//val isPalindrome : str:string -> bool

isPalindrome "abba"
//System.IndexOutOfRangeException: Index was outside the bounds of the array.
//   at FSI_0005.check@48-2(String str, Int32 s, Int32 e) in C:\...\Script.fsx:line 49
//   at FSI_0005.isPalindrome(String str) in C:\...\Script.fsx:line 48
//   at <StartupCode$FSI_0006>.$FSI_0006.main@() in C:\...\Script.fsx:line 70
//Stopped due to error

open System.Diagnostics

let isPalindrome (str : string) =
    let rec check(s : int, e : int) =
        Debug.WriteLine("check call")
        Debug.WriteLineIf((s = 0), "check: First call")
        Debug.Assert((s >= 0 || s < str.Length), sprintf "s is out of bounds: %d" s)
        Debug.Assert((e >= 0 || e < str.Length), sprintf "e is out of bounds: %d" e)
        if s = e || s = e + 1 then true
        else if str.[s] <> str.[e] then false
        else check(s + 1, e - 1)
    check(0, str.Length - 1)
//val isPalindrome : str:string -> bool

isPalindrome "abba"
//val it : bool = true

open System

[<DebuggerDisplay("{re}+{im}i")>]
type MyComplex = {re : double; im : double}

let c = {re = 0.0; im = 0.0}
Console.WriteLine("{0}+{1}i", c.re, c.im)
//>
//0+0i
//
//type MyComplex =
//  {re: double;
//   im: double;}
//val c : MyComplex = {re = 0.0;
//                     im = 0.0;}
//val it : unit = ()

open System
open System.Threading

let t1 = Thread(fun () -> while true do printf "Thread 1\n")
let t2 = Thread(fun () -> while true do printf "Thread 2\n")

t1.Start(); t2.Start()
//val t1 : Threading.Thread
//val t2 : Threading.Thread

type APoint(angle, radius) =
    member x.Angle = angle
    member x.Radius = radius
    new() = APoint(angle = 0.0, radius = 0.0)
//type APoint =
//  class
//    new : unit -> APoint
//    new : angle:float * radius:float -> APoint
//    member Angle : float
//    member Radius : float
//  end

let p = APoint()
//val p : APoint

p.GetType()
//val it : System.Type =
//  FSI_0002+APoint
//    {Assembly = FSI-ASSEMBLY, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;
//     AssemblyQualifiedName = "FSI_0002+APoint, FSI-ASSEMBLY, Version=0.0.0.0, ...}

type APoint(angle, radius) =
    member x.Angle = angle
    member x.Radius = radius
    member x.Stretch (k : double) = APoint(angle = x.Angle, radius = x.Radius + k)
    new() = APoint(angle = 0.0, radius = 0.0)
//type APoint =
//  class
//    new : unit -> APoint
//    new : angle:float * radius:float -> APoint
//    member Stretch : k:double -> APoint
//    member Angle : float
//    member Radius : float
//  end

p.Stretch(22.0)
//error FS0039: The field, constructor or member 'Stretch' is not defined

let p2 = APoint()
//val p2 : APoint

p2.GetType()
//val it : System.Type =
//  FSI_0005+APoint
//    {Assembly = FSI-ASSEMBLY, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null;
//     AssemblyQualifiedName = "FSI_0005+APoint, FSI-ASSEMBLY, Version=0.0.0.0, ...}


module IsPalindrome =
    open System.Diagnostics

    let isPalindrome (str : string) =
        let rec check(s : int, e : int) =
            Debug.WriteLine("check call")
            Debug.WriteLineIf((s = 0), "check: First call")
            Debug.Assert((s >= 0 || s < str.Length), sprintf "s is out of bounds: %d" s)
            Debug.Assert((e >= 0 || e < str.Length), sprintf "e is out of bounds: %d" e)
            if s = e || s = e + 1 then true
            else if str.[s] <> str.[e] then false
            else check(s + 1, e - 1)
        check(0, str.Length - 1)
//module IsPalindrome = begin
//  val isPalindrome : str:string -> bool
//end

#I "packages/NUnit/lib"
#r "nunit.framework.dll"

open System
open NUnit.Framework
open IsPalindrome

let posTests(strings) =
    for s in strings do
        Assert.That(isPalindrome s, Is.True,
                      sprintf "isPalindrome(\"%s\") must return true" s)

let negTests(strings) =
    for s in strings do
        Assert.That(isPalindrome s, Is.False,
                       sprintf "isPalindrome(\"%s\") must return false" s)

[<Test>]
let ``isPalindrome returns true on the empty string`` () =
    Assert.That(isPalindrome(""), Is.True,
                  "isPalindrome must return true on an empty string")

[<Test>]
let ``isPalindrome returns true for a single character``() = 
    posTests ["a"]

[<Test>]
let ``isPalindrome returns true for even examples`` () = 
    posTests ["aa"; "abba"; "abaaba"]

[<Test>]
let ``isPalindrome returns true for odd examples`` () = 
    posTests ["aba"; "abbba"; "abababa"]

[<Test>]
let ``isPalindrome returns false for some examples`` () =
    negTests ["as"; "F# is wonderful"; "Nice"]

//--> Added 'C:\...\packages/NUnit/lib' to library include path
//--> Referenced 'C:\...\packages/NUnit/lib\nunit.framework.dll'
//val posTests : strings:seq<string> -> unit
//val negTests : strings:seq<string> -> unit
//val ( isPalindrome returns true on the empty string ) : unit -> unit
//val ( isPalindrome returns true for a single character ) : unit -> unit
//val ( isPalindrome returns true for even examples ) : unit -> unit
//val ( isPalindrome returns true for odd examples ) : unit -> unit
//val ( isPalindrome returns false for some examples ) : unit -> unit

open System
open NUnit.Framework

[<TestFixture;
  Description("Test fixture for the isPalindrome function")>]
type Test() =
    [<TestFixtureSetUp>]
    member x.InitTestFixture () =
        printfn "Before running Fixture"

    [<TestFixtureTearDown>]
    member x.DoneTestFixture () =
        printfn "After running Fixture"

    [<SetUp>]
    member x.InitTest () =
        printfn "Before running test"

    [<TearDown>]
    member x.DoneTest () =
        Console.WriteLine("After running test")

    [<Test;
      Category("Special case");
      Description("An empty string is palindrome")>]
    member x.EmptyString () =
        Assert.That(isPalindrome(""), Is.True,
                      "isPalindrome must return true on an empty string")
//type Test =
//  class
//    new : unit -> Test
//    member DoneTest : unit -> unit
//    member DoneTestFixture : unit -> unit
//    member EmptyString : unit -> unit
//    member InitTest : unit -> unit
//    member InitTestFixture : unit -> unit
//  end

#I "packages/FsCheck/lib/net45"
#r "FsCheck.dll"

open FsCheck

let revTwice (xs : list<int>) = List.rev(List.rev xs) = xs
let revOnce (xs : list<int>) = List.rev xs = xs

Check.Quick revTwice
Check.Quick revOnce
//--> Added 'C:\...\packages/FsCheck/lib/net45' to library include path
//--> Referenced 'C:\..\packages/FsCheck/lib/net45\FsCheck.dll'
//
//Ok, passed 100 tests.
//Falsifiable, after 2 tests (0 shrinks) (StdGen (1711668133,296077015)):
//Original:
//[0; 1]
//
//val revTwice : xs:int list -> bool
//val revOnce : xs:int list -> bool
//val it : unit = ()