namespace Acme.NumberTheory

module NumberTheoryExtensions =
    let private isPrime i =
        let lim = int (sqrt (float i))
        let rec check j =
           j > lim || (i % j <> 0 && check (j+1))
        check 2

    type System.Int32 with
        member i.IsPrime = isPrime i

// NOTE: Files loaded into F# interactive with load have their values and types
// placed in a namespace or module according to the leading module or namespace
// declaration in the file.
[<assembly:AutoOpen("Acme.NumberTheory.NumberTheoryExtensions")>]
do()
