/// Split a string into words at spaces
let splitAtSpaces (text : string) = 
    text.Split ' ' 
    |> Array.toList 

/// Analyze a string for duplicate words
let wordCount text =
    let words = splitAtSpaces text
    let numWords = words.Length
    let distinctWords = List.distinct words
    let numDups  = numWords - distinctWords.Length
    (numWords, numDups)

/// Analyze a string for duplicate words and display the results.
let showWordCount text =
    let numWords, numDups = wordCount text
    printfn "--> %d words in the text" numWords
    printfn "--> %d duplicate words" numDups

//val splitAtSpaces : text:string -> string list
//val wordCount : text:string -> int * int
//val showWordCount : text:string -> unit

let (numWords, numDups) = wordCount "All the king's horses and all the king's men";;
//val numWords : int = 9
//val numDups : int = 2

showWordCount "Couldn't put Humpty together again";;
//--> 5 words in the text
//--> 0 duplicate words
//val it : unit = ()

//let wordCount (text:string) =
//    let words = ...

wordCount;;
//val it : (string -> int * int) = <fun:it@36>

//let wordCount (text: string) =
//    let words = splitAtSpaces text

splitAtSpaces "hello world";;
//val it : string list = [ "hello"; "world" ]

/// Analyze a string for duplicate words
let wordCount text =
    let words = splitAtSpaces text in
    let distinctWords = List.distinct words in
    let numWords = words.Length in
    let numDups = numWords - distinctWords.Length in
    (numWords, numDups)

let powerOfFour n =
    let nSquared = n * n in nSquared * nSquared

powerOfFour 3;;
//val it : int = 81

let badDefinition1 =
    let words = splitAtSpaces text
    let text = "We three kings"
    words.Length
//error FS0039: The value or constructor 'text' is not defined

let badDefinition2 = badDefinition2 + 1
//error FS0039: The value or constructor 'badDefinition2' is not defined

let powerOfFourPlusTwo n =
    let n = n * n
    let n = n * n
    let n = n + 2
    n

let powerOfFourPlusTwo n =
    let n1 = n * n
    let n2 = n1 * n1
    let n3 = n2 + 2
    n3

let powerOfFourPlusTwoTimesSix n =
    let n3 =
        let n1 = n * n
        let n2 = n1 * n1
        n2 + 2
    let n4 = n3 * 6
    n4

let invalidFunction n =
    let n3 =
        let n1 = n + n
        let n2 = n1 * n1
        n1 * n2
    let n4 = n1 + n2 + n3     // Error! n3 is in scope, but n1 and n2 are not!
    n4
//Script.fsx(110,14): error FS0039: The value or constructor 'n1' is not defined

//let wordCount (text : string) =
//    let words = splitAtSpaces text
//    let distinctWords = List.distinct words 
//    ...

List.distinct ["b"; "a"; "b"; "b"; "c"];;
//val it : string list = ["b"; "a"; "c"]

List.distinct (List.distinct ["abc"; "ABC"]);;
//val it : string list = ["abc"; "ABC"]

//  let numWords = words.Length
//  let numDups = numWords - distinctWords.Length

let length (inp:'T list) = inp.Length
//val length : inp:'T list -> int

//let numWords = List.length words
//let numDups = numWords - distinctWords.Length

let length inp = inp.Length;;
//error FS0072: Lookup on object of indeterminate type based on information prior to this program point. A type annotation may be needed prior to this program point to constrain the type of the object. This may allow the lookup to be resolved.

//    ...
//    let numWords = words.Length
//    let numDups = numWords - distinctWords.Length
//    (numWords, numDups)

let site1 = ("www.cnn.com", 10)
let site2 = ("news.bbc.com", 5)
let site3 = ("www.msnbc.com", 4)
let sites = (site1, site2, site3)
//val site1 : string * int = ("www.cnn.com", 10)
//val site2 : string * int = ("news.bbc.com", 5)
//val site3 : string * int = ("www.msnbc.com", 4)
//val sites : (string * int) * (string * int) * (string * int) =
//  (("www.cnn.com", 10), ("news.bbc.com", 5), ("www.msnbc.com", 4))

fst site1;;
//val it : string = "www.cnn.com"

let relevance = snd site1;;
//val relevance : int = 10

//let fst (a, b) = a
//let snd (a, b) = b

let url, relevance = site1
let siteA, siteB, siteC = sites
//val url : string = "www.cnn.com"
//val relevance : int = 10
//val siteC : string * int = ("www.msnbc.com", 4)
//val siteB : string * int = ("news.bbc.com", 5)
//val siteA : string * int = ("www.cnn.com", 10)

let a, b = (1, 2, 3);;
//error FS0001: Type mismatch. Expecting a
//    'a * 'b    
//but given a
//    'a * 'b * 'c    
//The tuples have differing lengths of 2 and 3

let showResults (numWords, numDups) =
    printfn "--> %d words in the text" numWords
    printfn "--> %d duplicate words" numDups

let showWordCount text = showResults (wordCount text)

//val showResults : int * int -> unit
//val showWordCount : string -> unit

printfn "--> %d words in the text" numWords
printfn "--> %d duplicate words" numDups
//--> 9 words in the text
//--> 2 duplicate words

//System.Console.WriteLine("--> {0} words in the text", box numWords)
//System.Console.WriteLine("--> {0} duplicate words", box numDups)

let two = (printfn "Hello World"; 1 + 1)
let four = two + two
//Hello World
//
//val two : int = 2
//val four : int = 4

(printfn "--> %d words in the text" numWords;
 printfn "--> %d duplicate words" numDups)
//--> 9 words in the text
//--> 2 duplicate words
//val it : unit = ()

/// Split a string into words at spaces
let splitAtSpaces (text : string) = 
    text.Split ' ' 
    |> Array.toList 
//val splitAtSpaces : string -> string list

open System.IO
open System.Net

/// Get the contents of the URL via a web request
let http (url : string) =
    let req = WebRequest.Create(url)
    let resp = req.GetResponse()
    let stream = resp.GetResponseStream()
    let reader = new StreamReader(stream)
    let html = reader.ReadToEnd()
    resp.Close()
    html

http "http://news.bbc.co.uk"
//val http : url:string -> string
//val it : string =
//  "
//<!DOCTYPE html>
//<html lang="en-GB" id="responsive-news" pref"+[198707 chars]

// Reset the interactive session and don't open these namespaces
//open System.IO;;
//open System.Net;;

let req = System.Net.WebRequest.Create("http://www.microsoft.com");;
//val req : System.Net.WebRequest

open System.IO;;
open System.Net;;

let req = WebRequest.Create("http://www.microsoft.com");;
//val req : WebRequest

let resp = req.GetResponse();;
//val resp : WebResponse

let stream = resp.GetResponseStream();;
//val stream : Stream

let reader = new StreamReader(stream);;
//val reader : StreamReader

let html = reader.ReadToEnd();;
//val html : string =
//  "<html><head><title>Microsoft Corporation</title><meta http-eq"+[959 chars]

//------------------------------------------
// Step 0. Get the package bootstrap

open System
open System.IO

Environment.CurrentDirectory <- __SOURCE_DIRECTORY__

if not (File.Exists "paket.exe") then
    let url = "https://github.com/fsprojects/Paket/releases/download/0.27.2/paket.exe"
    use wc = new Net.WebClient()
    let tmp = Path.GetTempFileName()
    wc.DownloadFile(url, tmp)
    File.Move(tmp,Path.GetFileName url);;
//val it : unit = ()

// Step 1. Resolve and install the packages 

#r "paket.exe"

Paket.Dependencies.Install """
    source https://nuget.org/api/v2
    nuget Suave
    nuget FSharp.Data 
    nuget FSharp.Charting
""";;
//--> Referenced 'C:\Dev\Src\apressF#4\clone-EFS-20150423b\02GettingStarted\paket.exe'
//
//Binding session to 'C:\Dev\Src\apressF#4\clone-EFS-20150423b\02GettingStarted\paket.exe'...
//found: C:\Dev\Src\apressF#4\clone-EFS-20150423b\02GettingStarted\paket.dependencies
//Resolving packages:
//  - fetching versions for FSharp.Charting
//    - exploring FSharp.Charting 0.90.10
//  - fetching versions for FSharp.Data
//    - exploring FSharp.Data 2.2.0
//  - fetching versions for Zlib.Portable
//    - exploring Zlib.Portable 1.10.0
//  - fetching versions for Suave
//    - exploring Suave 0.26.1
//  - fetching versions for FSharp.Core
//    - exploring FSharp.Core 4.0.0
//    - exploring FSharp.Core 3.1.2.1
//  - fetching versions for FsPickler
//    - exploring FsPickler 1.0.17
//Locked version resolutions written to C:\Dev\Src\apressF#4\clone-EFS-20150423b\02GettingStarted\paket.lock
//Downloading FsPickler 1.0.17 to C:\Users\pdejoux\AppData\Local\NuGet\Cache\FsPickler.1.0.17.nupkg
//Downloading FSharp.Charting 0.90.10 to C:\Users\pdejoux\AppData\Local\NuGet\Cache\FSharp.Charting.0.90.10.nupkg
//Downloading FSharp.Core 3.1.2.1 to C:\Users\pdejoux\AppData\Local\NuGet\Cache\FSharp.Core.3.1.2.1.nupkg
//Downloading Zlib.Portable 1.10.0 to C:\Users\pdejoux\AppData\Local\NuGet\Cache\Zlib.Portable.1.10.0.nupkg
//Downloading FSharp.Data 2.2.0 to C:\Users\pdejoux\AppData\Local\NuGet\Cache\FSharp.Data.2.2.0.nupkg
//Downloading Suave 0.26.1 to C:\Users\pdejoux\AppData\Local\NuGet\Cache\Suave.0.26.1.nupkg
//FSharp.Charting 0.90.10 unzipped to C:\Dev\Src\apressF#4\clone-EFS-20150423b\02GettingStarted\packages\FSharp.Charting
//FsPickler 1.0.17 unzipped to C:\Dev\Src\apressF#4\clone-EFS-20150423b\02GettingStarted\packages\FsPickler
//Zlib.Portable 1.10.0 unzipped to C:\Dev\Src\apressF#4\clone-EFS-20150423b\02GettingStarted\packages\Zlib.Portable
//Suave 0.26.1 unzipped to C:\Dev\Src\apressF#4\clone-EFS-20150423b\02GettingStarted\packages\Suave
//FSharp.Data 2.2.0 unzipped to C:\Dev\Src\apressF#4\clone-EFS-20150423b\02GettingStarted\packages\FSharp.Data
//FSharp.Core 3.1.2.1 unzipped to C:\Dev\Src\apressF#4\clone-EFS-20150423b\02GettingStarted\packages\FSharp.Core
//val it : unit = ()

#r "packages/FSharp.Data/lib/net40/FSharp.Data.dll"

open FSharp.Data

type Species = HtmlProvider<"http://en.wikipedia.org/wiki/The_world's_100_most_threatened_species">

let species = 
    [ for x in Species.GetSample().Tables.``Species list``.Rows -> x.Type, x.``Common name`` ]

//--> Referenced 'C:\Dev\Src\apressF#4\clone-EFS-20150423b\02GettingStarted\packages/FSharp.Data/lib/net40/FSharp.Data.dll'
//
//
//type Species = FSharp.Data.HtmlProvider<...>
//val species : (string * string) list =
//  [("Plant (tree)", "Baishan Fir"); ("Insect (butterfly)", "");
//   ("Reptile", "Leaf scaled sea-snake");
//   ("Insect (damselfly)", "Amani flatwing"); ("Bird", "Araripe manakin");
//   ("Insect", "(earwig)"); ("Fish", "Aci Göl toothcarp");
//   ("Mammal (bat)", "Bulmer’s fruit bat"); ("Bird", "White bellied heron");
//   ("Bird", "Great Indian bustard");
//   ("Reptile (tortoise)", "Ploughshare tortoise Angonoka");
//   ("Amphibian (toad)", "Rio Pescado stubfoot toad");
//   ("Bird", "Madagascar pochard"); ("Fish", "Galapagos damsel fish");
//   ("Fish", "Giant yellow croaker");
//   ("Reptile (turtle)", "Common batagur Four-toed terrapin");
//   ("Plant", "(liverwort)"); ("Mammal", "Hirola (antelope)");
//   ("Insect (bee)", "Franklin’s bumblebee");
//   ("Mammal (primate)", "Northern muriqui woolly spider monkey");
//   ("Mammal", "Pygmy three-toed sloth");
//   ("Plant (freshwater)", "(water-starwort)");
//   ("Reptile", "Tarzan’s chameleon");
//   ("Mammal (rodent)", "Santa Catarina’s guinea pig");
//   ("Mammal (primate)", "Roloway guenon (monkey)");
//   ("Mammal (bat)", "Seychelles sheath-tailed bat");
//   ("Fungi", "Willow blister");
//   ("Mammal (shrew)", "Nelson’s small-eared shrew");
//   ("Reptile", "Jamaican iguana Jamaican rock iguana");
//   ("Plant (orchid)", "Cayman Islands ghost orchid");
//   ("Mammal (rhino)", "Sumatran rhino"); ("Bird", "Amsterdam albatross");
//   ("Plant", "Wild yam"); ("Plant (tree)", ""); ("Plant (tree)", "");
//   ("Amphibian (frog)", "Hula painted frog"); ("Plant", "");
//   ("Plant (tree)", ""); ("Amphibian (frog)", "La Hotte glanded frog");
//   ("Amphibian (frog)", "Macaya breast-spot frog");
//   ("Plant", "Chilenito (cactus)"); ("Plant (tree)", "Coral tree");
//   ("Plant (tree)", ""); ("Bird", "Spoon-billed sandpiper"); ("Plant", "");
//   ("Bird", "Northern bald ibis");
//   ("Plant", "(flowering plant in legume family)");
//   ("Mollusc", "(type of gastropod)");
//   ("Amphibian (frog)", "Table mountain ghost frog");
//   ("Mollusc", "(type of land snail)"); ("Bird", "Liben lark");
//   ("Plant (small tree)", ""); ("Fish", "Sakhalin taimen");
//   ("Crustacean", "Singapore freshwater crab");
//   ("Plant",
//    "Belin vetchling (flowering plant related to Lathyrus odoratus"+[13 chars]);
//   ("Amphibian (frog)", "Archey’s frog");
//   ("Amphibian (frog)", "Dusky gopher frog"); ("Bird", "Edwards’s pheasant");
//   ("Plant", "(type of Magnolia tree)");
//   ("Mollusc", "(type of freshwater mussel)"); ("Mollusc", "(snail)");
//   ("Mammal (bat)", "Cuban greater funnel eared bat");
//   ("Plant", "Attenborough’s pitcher plant");
//   ("Mammal (primate)", "Hainan gibbon"); ("Amphibian", "Luristan newt");
//   ("Insect (damselfly)", "Mulanje red damsel (damselfly)");
//   ("Fish", "Pangasid catfish"); ("Insect (butterfly)", "(butterfly)");
//   ("Mammal (cetacean)", "Vaquita (porpoise)");
//   ("Plant (tree)", "Type of spruce tree"); ("Plant (tree)", "Qiaojia pine");
//   ("Spider",
//    "Gooty tarantula, metallic tarantula, peacock parachute spider"+[30 chars]);
//   ("Bird", "Fatuhiva monarch"); ("Fish", "Common sawfish");
//   ("Mammal (primate)", "Greater bamboo lemur");
//   ("Mammal (primate)", "Silky sifaka");
//   ("Reptile (tortoise)", "Geometric tortoise"); ("Mammal", "Saola");
//   ("Plant", ""); ("Insect", "Beydaglari bush-cricket");
//   ("Reptile (turtle)", "Red River giant softshell turtle");
//   ("Mammal (rhino)", "Javan rhino");
//   ("Mammal (primate)", "Tonkin snub-nosed monkey");
//   ("Plant (orchid)", "West Australian underground orchid");
//   ("Mammal (shrew)", "Boni giant sengi");
//   ("Insect (damselfly)", "Cebu frill-wing (damselfly)"); ("Plant", "");
//   ("Mammal", "Durrell’s vontsira (type of mongoose)");
//   ("Mammal (rodent)", "Red crested tree rat");
//   ("Fish", "Red-finned Blue-eye"); ("Fish (shark)", "Angel shark");
//   ("Bird", "Chinese crested tern"); ("Fish", "Estuarine pipefish");
//   ("Plant", "Suicide Palm Dimaka");
//   ("Amphibian (frog)", "Bullock’s false toad");
//   ("Mammal (rodent)", "Okinawa spiny rat"); ("Fish", "Somphongs’s rasbora");
//   ("Fish", ""); ("Plant", "Forest coconut");
//   ("Mammal", "Attenborough’s echidna")]

let speciesSorted = 
    species 
      |> List.countBy fst 
      |> List.sortByDescending snd
//val speciesSorted : (string * int) list =
//  [("Plant", 13); ("Bird", 11); ("Fish", 10); ("Plant (tree)", 8);
//   ("Amphibian (frog)", 7); ("Mammal (primate)", 6); ("Mammal", 5);
//   ("Mollusc", 4); ("Reptile", 3); ("Insect (damselfly)", 3);
//   ("Mammal (bat)", 3); ("Mammal (rodent)", 3); ("Insect (butterfly)", 2);
//   ("Insect", 2); ("Reptile (tortoise)", 2); ("Reptile (turtle)", 2);
//   ("Mammal (shrew)", 2); ("Plant (orchid)", 2); ("Mammal (rhino)", 2);
//   ("Amphibian (toad)", 1); ("Insect (bee)", 1); ("Plant (freshwater)", 1);
//   ("Fungi", 1); ("Plant (small tree)", 1); ("Crustacean", 1);
//   ("Amphibian", 1); ("Mammal (cetacean)", 1); ("Spider", 1);
//   ("Fish (shark)", 1)]

#r "packages/Suave/lib/net40/Suave.dll"

open Suave                 
open Suave.Http.Successful 
open Suave.Web             

let html = 
    [ yield "<html><body><ul>"
      for (category,count) in speciesSorted do
         yield sprintf "<li>Category <b>%s</b>: <b>%d</b></li>" category count 
      yield "</ul></body></html>" ]
    |> String.concat "\n"
//val html : string =
//  "<html><body><ul>
//<li>Category <b>Plant</b>: <b>13</b></li>
//<l"+[1367 chars]

startWebServer defaultConfig (OK html)
//--> Referenced 'C:\Dev\Src\apressF#4\clone-EFS-20150423b\02GettingStarted\packages/Suave/lib/net40/Suave.dll'
//
//[I] 2015-04-24T00:17:27.2824862Z: listener started in 16.019 ms with binding 127.0.0.1:8083 [Suave.Tcp.tcp_ip_server]

let angularHeader = """<head>
<link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css">
<script src="http://ajax.googleapis.com/ajax/libs/angularjs/1.2.26/angular.min.js"></script>
</head>"""

let fancyText = 
    [ yield """<html>"""
      yield angularHeader
      yield """ <body>"""
      yield """  <table class="table table-striped">"""
      yield """   <thead><tr><th>Category</th><th>Count</th></tr></thead>"""
      yield """   <tbody>"""
      for (category,count) in speciesSorted do
         yield sprintf "<tr><td>%s</td><td>%d</td></tr>" category count 
      yield """   </tbody>"""
      yield """  </table>"""
      yield """ </body>""" 
      yield """</html>""" ]
    |> String.concat "\n"
//val angularHeader : string =
//  "<head>
//<link rel="stylesheet" href="http://maxcdn.bootstrapcd"+[146 chars]
//val fancyText : string =
//  "<html>
//<head>
//<link rel="stylesheet" href="http://maxcdn.boot"+[1498 chars]

startWebServer defaultConfig (OK fancyText)
[I] 2015-04-24T00:23:55.8746486Z: listener started in 15.470 ms with binding 127.0.0.1:8083 [Suave.Tcp.tcp_ip_server]