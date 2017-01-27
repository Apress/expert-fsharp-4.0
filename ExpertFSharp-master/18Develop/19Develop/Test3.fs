#if INTERACTIVE
#r "bin/Debug/IsPalindrome.dll "
#r "../packages/NUnit/lib/nunit.framework.dll"
#else
module Test3
#endif

open System
open NUnit.Framework
open IsPalindrome

[<Test>]
let ``isPalindrome returns true on the empty string`` () =
    Assert.That(isPalindrome(""), Is.True,
                  "isPalindrome must return true on an empty string")
//--> Referenced 'C:\...\bin\Debug\IsPalindrome.dll'
//--> Referenced 'C:\...\../packages/NUnit/lib/nunit.framework.dll'
//val ( isPalindrome returns true on the empty string ) : unit -> unit