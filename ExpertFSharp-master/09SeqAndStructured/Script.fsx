seq {0 .. 2}
//val it : seq<int> = seq [0; 1; 2]

seq {-100.0 .. 100.0}
//val it : seq<float> = seq [-100.0; -99.0; -98.0; -97.0; ...]

seq {1I .. 1000000000000I}
//val it : seq<System.Numerics.BigInteger> =
//  seq [1 {IsEven = false;
//          IsOne = true;
//          IsPowerOfTwo = true;
//          IsZero = false;
//          Sign = 1;}; 2 {IsEven = true;
//                         IsOne = false;
//                         IsPowerOfTwo = true;
//                         IsZero = false;
//                         Sign = 1;}; 3 {IsEven = false;
//                                        IsOne = false;
//                                        IsPowerOfTwo = false;
//                                        IsZero = false;
//                                        Sign = 1;}; 4 {IsEven = true;
//                                                       IsOne = false;
//                                                       IsPowerOfTwo = true;
//                                                       IsZero = false;
//                                                       Sign = 1;}; ...]

seq {1 .. 2 .. 5}
//val it : seq<int> = seq [1; 3; 5]

seq {0 .. 2 .. 5}
//val it : seq<int> = seq [0; 2; 4]

seq {1 .. -2 .. -5}
//val it : seq<int> = seq [1; -1; -3; -5]

let range = seq {0 .. 2 .. 6}
//val range : seq<int>

for i in range do printfn "i = %d" i
//i = 0
//i = 2
//i = 4
//i = 6
//val it : unit = ()

let range = seq {0 .. 10}
//val range : seq<int>

range |> Seq.map (fun i -> (i, i*i ))
//val it : seq<int * int> = seq [(0, 0); (1, 1); (2, 4); (3, 9); ...]

Seq.append
//val it : (seq<'a> -> seq<'a> -> seq<'a>) = <fun:clo@52-13>

Seq.concat
//val it : (seq<#seq<'b>> -> seq<'b>) = <fun:clo@55-14>

Seq.choose
//val it : (('a -> 'b option) -> seq<'a> -> seq<'b>) = <fun:clo@58-15>

Seq.delay
//val it : ((unit -> seq<'a>) -> seq<'a>) = <fun:clo@61-16>

Seq.empty
//val it : seq<'a> = seq []

Seq.iter
//val it : (('a -> unit) -> seq<'a> -> unit) = <fun:clo@67-17>

Seq.filter
//val it : (('a -> bool) -> seq<'a> -> seq<'a>) = <fun:clo@70-18>

Seq.map
//val it : (('a -> 'b) -> seq<'a> -> seq<'b>) = <fun:clo@73-19>

Seq.singleton
//val it : ('a -> seq<'a>) = <fun:clo@76-20>

Seq.truncate
//val it : (int -> seq<'a> -> seq<'a>) = <fun:clo@79-21>

Seq.toList
//val it : (seq<'a> -> 'a list) = <fun:clo@82-22>

Seq.ofList
//val it : ('a list -> seq<'a>) = <fun:clo@85-23>

Seq.toArray
//val it : (seq<'a> -> 'a []) = <fun:clo@88-24>

Seq.ofArray
//val it : ('a [] -> seq<'a>) = <fun:clo@91-25>

open System
open System.IO

let rec allFiles dir =
    Seq.append
        (dir |> Directory.GetFiles)
        (dir |> Directory.GetDirectories |> Seq.map allFiles |> Seq.concat)
//val allFiles : dir:string -> seq<string>

allFiles Environment.SystemDirectory
//val it : seq<string> =
//  seq
//    ["C:\Windows\system32\12520437.cpx"; "C:\Windows\system32\12520850.cpx";
//     "C:\Windows\system32\@OpenWithToastLogo.png";
//     "C:\Windows\system32\aaclient.dll"; ...]

let squares = seq { for i in 0 .. 10 -> (i, i * i) }
//val squares : seq<int * int>

seq { for (i, iSquared) in squares -> (i, iSquared, i * iSquared) }
//val it : seq<int * int * int> =
//  seq [(0, 0, 0); (1, 1, 1); (2, 4, 8); (3, 9, 27); ...]

let checkerboardCoordinates n = 
   seq { for row in 1 .. n do
            for col in 1 .. n do
                let sum = row + col
                if sum % 2 = 0 then
                    yield (row, col)}
//val checkerboardCoordinates : n:int -> seq<int * int>

checkerboardCoordinates 3
//val it : seq<int * int> = seq [(1, 1); (1, 3); (2, 2); (3, 1); ...]

let fileInfo dir =
    seq { for file in Directory.GetFiles dir do
            let creationTime = File.GetCreationTime file 
            let lastAccessTime = File.GetLastAccessTime file
            yield (file, creationTime, lastAccessTime)}
//val fileInfo : dir:string -> seq<string * DateTime * DateTime>

let rec allFiles dir =
    seq { for file in Directory.GetFiles dir do
             yield file
          for subdir in Directory.GetDirectories dir do 
             yield! allFiles subdir}
//val allFiles : dir:string -> seq<string>

[1 .. 4]
//val it: int list = [1; 2; 3; 4]

[for i in 0 .. 3 -> (i, i * i)]
//val it : (int * int) list = [(0, 0); (1, 1); (2, 4); (3, 9)]

[|for i in 0 .. 3 -> (i, i * i)|]
//val it : (int * int) [] = [|(0, 0); (1, 1); (2, 4); (3, 9)|]

/// A table of people in our startup
let people = 
    [("Amber", 27, "Design")
     ("Wendy", 35, "Events")
     ("Antonio", 40, "Sales")
     ("Petra", 31, "Design")
     ("Carlos", 34, "Marketing")]

/// Extract information from the table of people 
let namesOfPeopleStartingWithA =
    people 
      |> Seq.map (fun (name, _, _) -> name)
      |> Seq.filter (fun name -> name.StartsWith "A")
      |> Seq.toList

/// Extract the names of designers from the table of people 
let namesOfDesigners =
    people 
      |> Seq.filter  (fun (_, _, dept) -> dept = "Design")
      |> Seq.map (fun (name, _, _) -> name)
      |> Seq.toList

//val people : (string * int * string) list =
//  [("Amber", 27, "Design"); ("Wendy", 35, "Events"); ("Antonio", 40, "Sales");
//   ("Petra", 31, "Design"); ("Carlos", 34, "Marketing")]
//
//val namesOfPeopleStartingWithA : string list = ["Amber"; "Antonio"]
//
//val namesOfDesigners : string list = ["Amber"; "Petra"]

/// A random-number generator
let rand = System.Random()

/// An infinite sequence of numbers
let randomNumbers = seq { while true do yield rand.Next(100000) }

/// The first 10 random numbers, sorted
let firstTenRandomNumbers =
    randomNumbers 
      |> Seq.truncate 10
      |> Seq.sort                         // sort ascending
      |> Seq.toList

/// The first 3000 even random numbers and sort them
let firstThreeThousandEvenNumbersWithSquares =
    randomNumbers 
      |> Seq.filter (fun i -> i % 2 = 0)  // "where"
      |> Seq.truncate 3000
      |> Seq.sort                         // sort ascending
      |> Seq.map (fun i -> i, i*i)        // "select"
      |> Seq.toList

//val rand : Random
//val randomNumbers : seq<int>
//val firstTenRandomNumbers : int list =
//  [5407; 5990; 9199; 11769; 16271; 49844; 59544; 69645; 89379; 98263]
//val firstThreeThousandEvenNumbersWithSquares : (int * int) list =
//  [(22, 484); (26, 676); (34, 1156); (34, 1156); (110, 12100); (180, 32400);
//   (228, 51984); (294, 86436); (312, 97344); (318, 101124); (364, 132496);
//   (370, 136900); (404, 163216); (458, 209764); (490, 240100); (506, 256036);
//   (508, 258064); (548, 300304); (586, 343396); (648, 419904); (656, 430336);
//   (694, 481636); (764, 583696); (864, 746496); (876, 767376); (908, 824464);
//   (920, 846400); (930, 864900); (1038, 1077444); (1098, 1205604);
//   (1122, 1258884); (1134, 1285956); (1144, 1308736); (1218, 1483524);
//   (1252, 1567504); (1290, 1664100); (1298, 1684804); (1316, 1731856);
//   (1324, 1752976); (1332, 1774224); (1372, 1882384); (1398, 1954404);
//   (1418, 2010724); (1478, 2184484); (1486, 2208196); (1516, 2298256);
//   (1544, 2383936); (1570, 2464900); (1572, 2471184); (1578, 2490084);
//   (1734, 3006756); (1784, 3182656); (1828, 3341584); (1906, 3632836);
//   (1912, 3655744); (1926, 3709476); (1932, 3732624); (1936, 3748096);
//   (1936, 3748096); (1982, 3928324); (2030, 4120900); (2070, 4284900);
//   (2146, 4605316); (2168, 4700224); (2196, 4822416); (2256, 5089536);
//   (2282, 5207524); (2322, 5391684); (2386, 5692996); (2444, 5973136);
//   (2518, 6340324); (2544, 6471936); (2548, 6492304); (2558, 6543364);
//   (2610, 6812100); (2668, 7118224); (2678, 7171684); (2688, 7225344);
//   (2702, 7300804); (2712, 7354944); (2742, 7518564); (2760, 7617600);
//   (2792, 7795264); (2846, 8099716); (2864, 8202496); (2914, 8491396);
//   (2950, 8702500); (2966, 8797156); (3020, 9120400); (3034, 9205156);
//   (3148, 9909904); (3154, 9947716); (3186, 10150596); (3252, 10575504);
//   (3266, 10666756); (3308, 10942864); (3320, 11022400); (3344, 11182336);
//   (3356, 11262736); (3370, 11356900); ...]

/// The first 10 random numbers, sorted by last digit
let firstTenRandomNumbersSortedByLastDigit = 
    randomNumbers 
      |> Seq.truncate 10 
      |> Seq.sortBy (fun x -> x % 10) 
      |> Seq.toList

//val firstTenRandomNumbersSortedByLastDigit : int list =
//  [51220; 56640; 88543; 97424; 90744; 11784; 23316; 1368; 71878; 89719]

Seq.choose
//val it : (('a -> 'b option) -> seq<'a> -> seq<'b>) = <fun:clo@243-29>

Seq.collect
//val it : (('a -> #seq<'c>) -> seq<'a> -> seq<'c>) = <fun:clo@246-30>

Seq.map
//val it : (('a -> 'b) -> seq<'a> -> seq<'b>) = <fun:clo@249-31>

// Take the first 10 numbers and build a triangle 1, 1, 2, 1, 2, 3, 1, 2, 3, 4, …
let triangleNumbers = 
    [ 1 .. 10 ] 
    |> Seq.collect (fun i -> [ 1 .. i ]) 
    |> Seq.toList 
//val triangleNumbers : int list =
//  [1; 1; 2; 1; 2; 3; 1; 2; 3; 4; 1; 2; 3; 4; 5; 1; 2; 3; 4; 5; 6; 1; 2; 3; 4;
//   5; 6; 7; 1; 2; 3; 4; 5; 6; 7; 8; 1; 2; 3; 4; 5; 6; 7; 8; 9; 1; 2; 3; 4; 5;
//   6; 7; 8; 9; 10]

let gameBoard = 
    [
        for i in 0 .. 7 do 
        for j in 0 .. 7 do 
        yield (i,j,rand.Next(10))
    ] 

let evenPositions = 
    gameBoard
    |> Seq.choose (fun (i,j,v) -> if v % 2 = 0 then Some (i,j) else None) 
    |> Seq.toList 

//val gameBoard : (int * int * int) list =
//  [(0, 0, 2); (0, 1, 1); (0, 2, 6); (0, 3, 9); (0, 4, 3); (0, 5, 4); (0, 6, 0);
//   (0, 7, 7); (1, 0, 5); (1, 1, 7); (1, 2, 1); (1, 3, 2); (1, 4, 7); (1, 5, 9);
//   (1, 6, 5); (1, 7, 5); (2, 0, 3); (2, 1, 9); (2, 2, 7); (2, 3, 0); (2, 4, 9);
//   (2, 5, 8); (2, 6, 4); (2, 7, 6); (3, 0, 0); (3, 1, 1); (3, 2, 1); (3, 3, 3);
//   (3, 4, 3); (3, 5, 4); (3, 6, 1); (3, 7, 1); (4, 0, 4); (4, 1, 6); (4, 2, 1);
//   (4, 3, 4); (4, 4, 5); (4, 5, 2); (4, 6, 0); (4, 7, 3); (5, 0, 1); (5, 1, 7);
//   (5, 2, 9); (5, 3, 2); (5, 4, 3); (5, 5, 4); (5, 6, 8); (5, 7, 0); (6, 0, 2);
//   (6, 1, 3); (6, 2, 0); (6, 3, 0); (6, 4, 5); (6, 5, 3); (6, 6, 4); (6, 7, 3);
//   (7, 0, 7); (7, 1, 0); (7, 2, 9); (7, 3, 2); (7, 4, 2); (7, 5, 6); (7, 6, 4);
//   (7, 7, 0)]
//val evenPositions : (int * int) list =
//  [(0, 0); (0, 2); (0, 5); (0, 6); (1, 3); (2, 3); (2, 5); (2, 6); (2, 7);
//   (3, 0); (3, 5); (4, 0); (4, 1); (4, 3); (4, 5); (4, 6); (5, 3); (5, 5);
//   (5, 6); (5, 7); (6, 0); (6, 2); (6, 3); (6, 6); (7, 1); (7, 3); (7, 4);
//   (7, 5); (7, 6); (7, 7)]

Seq.find
//val it : (('a -> bool) -> seq<'a> -> 'a) = <fun:clo@289-32>

Seq.findIndex
//val it : (('a -> bool) -> seq<'a> -> int) = <fun:clo@292-33>

Seq.pick
//val it : (('a -> 'b option) -> seq<'a> -> 'b) = <fun:clo@295-34>

Seq.tryFind
//val it : (('a -> bool) -> seq<'a> -> 'a option) = <fun:clo@298-35>

Seq.tryFindIndex
//val it : (('a -> bool) -> seq<'a> -> int option) = <fun:clo@301-36>

Seq.tryPick 
//val it : (('a -> 'b option) -> seq<'a> -> 'b option) = <fun:clo@304-37>

let firstElementScoringZero = 
   gameBoard |> Seq.tryFind (fun (i, j, v) -> v = 0)

let firstPositionScoringZero = 
   gameBoard |> Seq.tryPick (fun (i, j, v) -> if v = 0 then Some(i, j) else None)

//val firstElementScoringZero : (int * int * int) option = Some (0, 6, 0)
//val firstPositionScoringZero : (int * int) option = Some (0, 6)

let positionsGroupedByGameValue = 
    gameBoard 
    |> Seq.groupBy (fun (i, j, v) -> v) 
    |> Seq.sortBy (fun (k, v) -> k)
    |> Seq.toList 
//val positionsGroupedByGameValue : (int * seq<int * int * int>) list =
//  [(0, <seq>); (1, <seq>); (2, <seq>); (3, <seq>); (4, <seq>); (5, <seq>);
//   (6, <seq>); (7, <seq>); (8, <seq>); (9, <seq>)] 

let positionsIndexedByGameValue = 
    gameBoard 
    |> Seq.groupBy (fun (i, j, v) -> v) 
    |> Seq.sortBy (fun (k, v) -> k)
    |> Seq.map (fun (k, v) -> (k, Seq.toList v))
    |> dict

let worstPositions = positionsIndexedByGameValue.[0]
let bestPositions = positionsIndexedByGameValue.[9]

//val positionsIndexedByGameValue :
//  Collections.Generic.IDictionary<int,(int * int * int) list>
//val worstPositions : (int * int * int) list =
//  [(0, 6, 0); (2, 3, 0); (3, 0, 0); (4, 6, 0); (5, 7, 0); (6, 2, 0); (6, 3, 0);
//   (7, 1, 0); (7, 7, 0)]
//val bestPositions : (int * int * int) list =
//  [(0, 3, 9); (1, 5, 9); (2, 1, 9); (2, 4, 9); (5, 2, 9); (7, 2, 9)]

List.fold (fun acc x -> acc + x) 0 [4; 5; 6]
//val it : int = 15

Seq.fold (fun acc x -> acc + x) 0.0 [4.0; 5.0; 6.0]
//val it : float = 15.0

List.foldBack (fun x acc -> min x acc) [4; 5; 6; 3; 5] System.Int32.MaxValue
//val it : int = 3

List.fold (+) 0 [4; 5; 6]
//val it : int = 15

Seq.fold (+) 0.0 [4.0; 5.0; 6.0]
//val it : float = 15.0

List.foldBack min [4; 5; 6; 3; 5] System.Int32.MaxValue
//val it : int = 3

List.foldBack (fst >> min) [(3, "three"); (5, "five")] System.Int32.MaxValue
//val it : int = 3

let firstTwoLines file =
    seq {
        use s = File.OpenText(file)
        yield s.ReadLine()
        yield s.ReadLine()
    }

File.WriteAllLines("test1.txt", [|"Es kommt ein Schiff"; "A ship is coming"|])

let twolines() = firstTwoLines "test1.txt"
//val firstTwoLines : file:string -> seq<string>
//val twolines : unit -> seq<string>

twolines() |> Seq.iter (printfn "line = '%s'")
//line = 'Es kommt ein Schiff'
//line = 'A ship is coming'
//val it : unit = ()

let triangleNumbers = 
    [for i in 1 .. 10 do 
         for j in 1 .. i do
             yield (i, j)] 

let evenPositions = 
    [for (i, j, v) in gameBoard do 
         if v % 2 = 0 then 
             yield (i, j)] 

//val triangleNumbers : (int * int) list =
//  [(1, 1); (2, 1); (2, 2); (3, 1); (3, 2); (3, 3); (4, 1); (4, 2); (4, 3);
//   (4, 4); (5, 1); (5, 2); (5, 3); (5, 4); (5, 5); (6, 1); (6, 2); (6, 3);
//   (6, 4); (6, 5); (6, 6); (7, 1); (7, 2); (7, 3); (7, 4); (7, 5); (7, 6);
//   (7, 7); (8, 1); (8, 2); (8, 3); (8, 4); (8, 5); (8, 6); (8, 7); (8, 8);
//   (9, 1); (9, 2); (9, 3); (9, 4); (9, 5); (9, 6); (9, 7); (9, 8); (9, 9);
//   (10, 1); (10, 2); (10, 3); (10, 4); (10, 5); (10, 6); (10, 7); (10, 8);
//   (10, 9); (10, 10)]
//val evenPositions : (int * int) list =
//  [(0, 0); (0, 2); (0, 5); (0, 6); (1, 3); (2, 3); (2, 5); (2, 6); (2, 7);
//   (3, 0); (3, 5); (4, 0); (4, 1); (4, 3); (4, 5); (4, 6); (5, 3); (5, 5);
//   (5, 6); (5, 7); (6, 0); (6, 2); (6, 3); (6, 6); (7, 1); (7, 3); (7, 4);
//   (7, 5); (7, 6); (7, 7)]

// From chapter 8 on text, the definition of Scene.
open System.Xml
open System.Drawing

type Scene =
    | Ellipse of RectangleF
    | Rect of RectangleF
    | Composite of Scene list

    static member Circle(center:PointF,radius) =
        Ellipse(RectangleF(center.X-radius,center.Y-radius,
                           radius*2.0f,radius*2.0f))

    /// A derived constructor
    static member Square(left,top,side) =
        Rect(RectangleF(left,top,side,side))

let rec flatten scene =
    seq {
        match scene with
        | Composite scenes -> for x in scenes do yield! flatten x 
        | Ellipse _ | Rect _ -> yield scene
    }

let rec flattenAux scene acc =
    match scene with
    | Composite(scenes) -> List.foldBack flattenAux scenes acc
    | Ellipse _
    | Rect _ -> scene :: acc

let flatten2 scene = flattenAux scene [] |> Seq.ofList

let flatten3 scene =
    let acc = new ResizeArray<_>()
    let rec flattenAux s =
        match s with
        | Composite(scenes) -> scenes |> List.iter flattenAux
        | Ellipse _ | Rect _ -> acc.Add s
    flattenAux scene
    Seq.readonly acc

//type Scene =
//  | Ellipse of System.Drawing.RectangleF
//  | Rect of System.Drawing.RectangleF
//  | Composite of Scene list
//  with
//    static member
//      Circle : center:System.Drawing.PointF * radius:float32 -> Scene
//    static member Square : left:float32 * top:float32 * side:float32 -> Scene
//  end
//val flatten : scene:Scene -> seq<Scene>
//val flattenAux : scene:Scene -> acc:Scene list -> Scene list
//val flatten2 : scene:Scene -> seq<Scene>
//val flatten3 : scene:Scene -> seq<Scene>

let rec rectanglesOnly scene =
    match scene with
    | Composite scenes -> Composite (scenes |> List.map rectanglesOnly)
    | Ellipse rect | Rect rect -> Rect rect

let rec mapRects f scene =
    match scene with
    | Composite scenes -> Composite (scenes |> List.map (mapRects f))
    | Ellipse rect -> Ellipse (f rect)
    | Rect rect -> Rect (f rect)


//val rectanglesOnly : scene:Scene -> Scene
//val mapRects : f:(RectangleF -> RectangleF) -> scene:Scene -> Scene

let adjustAspectRatio scene =
    scene |> mapRects (fun r -> RectangleF.Inflate(r, 1.1f, 1.0f / 1.1f)) 
//val adjustAspectRatio : scene:Scene -> Scene

//<Composite>
//     <File file='spots.xml'/>
//     <File file='dots.xml'/>
//</Composite>

open System.Drawing
open System.Xml

// This Scene type and extractScene function are from chapter 8.
type Scene =
    | Ellipse of RectangleF
    | Rect of RectangleF
    | Composite of Scene list
    | Delay of Lazy<Scene>

    /// A derived constructor
    static member Circle(center : PointF, radius) =
        Ellipse(RectangleF(center.X - radius, center.Y - radius,
                           radius * 2.0f, radius * 2.0f))

    /// A derived constructor
    static member Square(left, top, side) =
        Rect(RectangleF(left, top, side, side))

let extractFloat32 attrName (attribs : XmlAttributeCollection) =
    float32 (attribs.GetNamedItem(attrName).Value)

let extractPointF (attribs : XmlAttributeCollection) =
    PointF(extractFloat32 "x" attribs, extractFloat32 "y" attribs)

let extractRectangleF (attribs : XmlAttributeCollection) =
    RectangleF(extractFloat32 "left" attribs, extractFloat32 "top" attribs,
               extractFloat32 "width" attribs, extractFloat32 "height" attribs)
               
let rec extractScene (node : XmlNode) =
    let attribs = node.Attributes
    let childNodes = node.ChildNodes
    match node.Name with
    | "Circle"  ->
        Scene.Circle(extractPointF(attribs), extractFloat32 "radius" attribs)
    | "Ellipse"  ->
        Scene.Ellipse(extractRectangleF(attribs))
    | "Rectangle"  ->
        Scene.Rect(extractRectangleF(attribs))
    | "Square"  ->
        Scene.Square(extractFloat32 "left" attribs, extractFloat32 "top" attribs,
                     extractFloat32 "side" attribs)
    | "Composite"   ->
        Scene.Composite [for child in childNodes -> extractScene(child)]
    | "File" ->
        let file = attribs.GetNamedItem("file").Value
        let scene = lazy (let d = XmlDocument()
                          d.Load(file)
                          extractScene(d :> XmlNode))
        Scene.Delay scene
    | _ -> failwithf "unable to convert XML '%s'" node.OuterXml 

let rec getScene scene =
    match scene with
    | Delay d -> getScene (d.Force())
    | _ -> scene

let rec flattenAux scene acc =
    match getScene(scene) with
    | Composite scenes -> List.foldBack flattenAux scenes acc
    | Ellipse _ | Rect _ -> scene :: acc
    | Delay _ -> failwith "this lazy value should have been eliminated by getScene"

let flatten2 scene = flattenAux scene []

type SceneVeryLazy =
    | Ellipse of Lazy<RectangleF>
    | Rect of Lazy<RectangleF>
    | Composite of seq<SceneVeryLazy>
    | LoadFile of string

type SceneWithCachedBoundingBox =
    | Ellipse of RectangleF
    | Rect of RectangleF
    | CompositeRepr of SceneWithCachedBoundingBox list * RectangleF option ref

    member x.BoundingBox =
        match x with
        | Ellipse rect | Rect rect -> rect
        | CompositeRepr (scenes, cache) ->
            match !cache with
            | Some v -> v
            | None ->
                let bbox =
                    scenes
                    |> List.map (fun s -> s.BoundingBox)
                    |> List.reduce (fun r1 r2 -> RectangleF.Union(r1, r2))
                cache := Some bbox
                bbox

    /// Create a Composite node with an initially empty cache
    static member Composite(scenes) = CompositeRepr(scenes, ref None)

//type Scene =
//  | Ellipse of System.Drawing.RectangleF
//  | Rect of System.Drawing.RectangleF
//  | Composite of Scene list
//  | Delay of Lazy<Scene>
//  with
//    static member
//      Circle : center:System.Drawing.PointF * radius:float32 -> Scene
//    static member Square : left:float32 * top:float32 * side:float32 -> Scene
//  end
//val extractFloat32 :
//  attrName:string -> attribs:System.Xml.XmlAttributeCollection -> float32
//val extractPointF :
//  attribs:System.Xml.XmlAttributeCollection -> System.Drawing.PointF
//val extractRectangleF :
//  attribs:System.Xml.XmlAttributeCollection -> System.Drawing.RectangleF
//val extractScene : node:System.Xml.XmlNode -> Scene
//val getScene : scene:Scene -> Scene
//val flattenAux : scene:Scene -> acc:Scene list -> Scene list
//val flatten2 : scene:Scene -> Scene list
//type SceneVeryLazy =
//  | Ellipse of Lazy<System.Drawing.RectangleF>
//  | Rect of Lazy<System.Drawing.RectangleF>
//  | Composite of seq<SceneVeryLazy>
//  | LoadFile of string
//type SceneWithCachedBoundingBox =
//  | Ellipse of System.Drawing.RectangleF
//  | Rect of System.Drawing.RectangleF
//  | CompositeRepr of
//    SceneWithCachedBoundingBox list * System.Drawing.RectangleF option ref
//  with
//    member BoundingBox : System.Drawing.RectangleF
//    static member
//      Composite : scenes:SceneWithCachedBoundingBox list ->
//                    SceneWithCachedBoundingBox
//  end

type Prop =
    | And of Prop * Prop
    | Or of Prop * Prop
    | Not of Prop
    | Var of string
    | True
//type Prop =
//  | And of Prop * Prop
//  | Or of Prop * Prop
//  | Not of Prop
//  | Var of string
//  | True

type Prop =
    | Prop of int

#if INTERACTIVE
and PropRepr =
#else
and internal PropRepr =
#endif
    | AndRepr of Prop * Prop
    | OrRepr of Prop * Prop
    | NotRepr of Prop
    | VarRepr of string
    | TrueRepr

open System.Collections.Generic

module PropOps =

    #if INTERACTIVE
    let uniqStamp = ref 0
    type PropTable() =
    #else
    let internal uniqStamp = ref 0
    type internal PropTable() =
    #endif
        let fwdTable = new Dictionary<PropRepr, Prop>(HashIdentity.Structural)
        let bwdTable = new Dictionary<int, PropRepr>(HashIdentity.Structural)
        member t.ToUnique repr =
            if fwdTable.ContainsKey repr then fwdTable.[repr]
            else let stamp = incr uniqStamp; !uniqStamp
                 let prop = Prop stamp
                 fwdTable.Add (repr, prop)
                 bwdTable.Add (stamp, repr)
                 prop
        member t.FromUnique (Prop stamp) =
            bwdTable.[stamp]

    #if INTERACTIVE
    let table = PropTable ()
    #else
    let internal table = PropTable ()
    #endif


    // Public construction functions
    let And (p1, p2) = table.ToUnique (AndRepr (p1, p2))
    let Not p = table.ToUnique (NotRepr p)
    let Or (p1, p2)  = table.ToUnique (OrRepr (p1, p2))
    let Var p = table.ToUnique (VarRepr p)
    let True = table.ToUnique TrueRepr
    let False = Not True

    // Deconstruction function
    #if INTERACTIVE
    let getRepr p = table.FromUnique p
    #else
    let internal getRepr p = table.FromUnique p
    #endif
//type Prop = | Prop of int
//and PropRepr =
//  | AndRepr of Prop * Prop
//  | OrRepr of Prop * Prop
//  | NotRepr of Prop
//  | VarRepr of string
//  | TrueRepr
//module PropOps = begin
//  val uniqStamp : int ref = {contents = 2;}
//  type PropTable =
//    class
//      new : unit -> PropTable
//      member FromUnique : Prop -> PropRepr
//      member ToUnique : repr:PropRepr -> Prop
//    end
//  val table : PropTable
//  val And : p1:Prop * p2:Prop -> Prop
//  val Not : p:Prop -> Prop
//  val Or : p1:Prop * p2:Prop -> Prop
//  val Var : p:string -> Prop
//  val True : Prop = Prop 1
//  val False : Prop = Prop 2
//  val getRepr : p:Prop -> PropRepr
//end

open PropOps
True
//val it : Prop = Prop 1

And (Var "x", Var "y")
//val it : Prop = Prop 5

getRepr it
// NOTE: Without internal as an access modifier, the output is ...
//val it : PropRepr = AndRepr (Prop 3,Prop 4)

getRepr it
// WARNING: If internal is used as an access modifier, the output is ...
//error FS0410: The type 'PropRepr' is less accessible than the value, member or type 'val it : PropRepr' it is used in ...

And(Var "x", Var "y")
//val it : Prop = Prop 5

[<Struct>]
type Complex(r : float, i : float) =
    static member Polar(mag, phase) = Complex(mag * cos phase, mag * sin phase)
    member x.Magnitude = sqrt(r * r + i * i)
    member x.Phase = atan2 i r
    member x.RealPart = r
    member x.ImaginaryPart = i

let (|Rect|) (x : Complex) = (x.RealPart, x.ImaginaryPart)

let (|Polar|) (x : Complex) = (x.Magnitude, x.Phase)

let addViaRect a b =
    match a, b with
    | Rect (ar, ai), Rect (br, bi) -> Complex (ar + br, ai + bi)

let mulViaRect a b =
    match a, b with
    | Rect (ar, ai), Rect (br, bi) -> Complex (ar * br - ai * bi, ai * br + bi * ar)

let mulViaPolar a b =
    match a, b with
    | Polar (m, p), Polar (n, q) -> Complex.Polar (m * n, p + q)
//type Complex =
//  struct
//    new : r:float * i:float -> Complex
//    member ImaginaryPart : float
//    member Magnitude : float
//    member Phase : float
//    member RealPart : float
//    static member Polar : mag:float * phase:float -> Complex
//  end
//val ( |Rect| ) : x:Complex -> float * float
//val ( |Polar| ) : x:Complex -> float * float
//val addViaRect : a:Complex -> b:Complex -> Complex
//val mulViaRect : a:Complex -> b:Complex -> Complex
//val mulViaPolar : a:Complex -> b:Complex -> Complex

fsi.AddPrinter (fun (c : Complex) -> sprintf "%gr + %gi" c.RealPart c.ImaginaryPart)

let c = Complex (3.0, 4.0)
//val c : Complex = 3r + 4i

c
//val it : Complex = 3r + 4i

match c with
| Rect (x, y) -> printfn "x = %g, y = %g" x y
//x = 3, y = 4

match c with
| Polar (x, y) -> printfn "x = %g, y = %g" x y
//x = 5, y = 0.927295

addViaRect c c
//val it : Complex = 6r + 8i

mulViaRect c c
//val it : Complex = -7r + 24i

mulViaPolar c c
//val it : Complex = -7r + 24i

let mulViaRect a b =
    match a, b with
    | Rect (ar, ai), Rect (br, bi) ->
        Complex (ar * br - ai * bi, ai * br + bi * ar)
//val mulViaRect : a:Complex -> b:Complex -> Complex

let add2 (Rect (ar, ai)) (Rect (br, bi)) = Complex (ar + br, ai + bi)
let mul2 (Polar (r1, th1)) (Polar (r2, th2)) = Complex (r1 * r2, th1 + th2)
//val add2 : Complex -> Complex -> Complex
//val mul2 : Complex -> Complex -> Complex

let (|Named|Array|Ptr|Param|) (typ : System.Type) =
    if typ.IsGenericType
    then Named(typ.GetGenericTypeDefinition(), typ.GetGenericArguments())
    elif typ.IsGenericParameter then Param(typ.GenericParameterPosition)
    elif not typ.HasElementType then Named(typ, [||])
    elif typ.IsArray then Array(typ.GetElementType(), typ.GetArrayRank())
    elif typ.IsByRef then Ptr(true, typ.GetElementType())
    elif typ.IsPointer then Ptr(false, typ.GetElementType())
    else failwith "MSDN says this can't happen"
//val ( |Named|Array|Ptr|Param| ) :
//  typ:System.Type ->
//    Choice<(System.Type * System.Type []),(System.Type * int),
//           (bool * System.Type),int>

open System

let rec formatType typ =
    match typ with
    | Named (con, [||]) -> sprintf "%s" con.Name
    | Named (con, args) -> sprintf "%s<%s>" con.Name (formatTypes args)
    | Array (arg, rank) -> sprintf "Array(%d,%s)" rank (formatType arg)
    | Ptr(true, arg) -> sprintf "%s&" (formatType arg)
    | Ptr(false, arg) -> sprintf "%s*" (formatType arg)
    | Param(pos) -> sprintf "!%d" pos

and formatTypes typs =
    String.Join(",", Array.map formatType typs)

let rec freeVarsAcc typ acc =
    match typ with
    | Array (arg, rank) -> freeVarsAcc arg acc
    | Ptr (_, arg) -> freeVarsAcc arg acc
    | Param _ -> (typ :: acc)
    | Named (con, args) -> Array.foldBack freeVarsAcc args acc

let freeVars typ = freeVarsAcc typ []

let (|MulThree|_|) inp = if inp % 3 = 0 then Some(inp / 3) else None
let (|MulSeven|_|) inp = if inp % 7 = 0 then Some(inp / 7) else None

let (|MulN|_|) n inp = if inp % n = 0 then Some(inp / n) else None

//val formatType : typ:Type -> string
//val formatTypes : typs:Type [] -> string
//val freeVarsAcc : typ:Type -> acc:Type list -> Type list
//val freeVars : typ:Type -> Type list
//val ( |MulThree|_| ) : inp:int -> int option
//val ( |MulSeven|_| ) : inp:int -> int option
//val ( |MulN|_| ) : n:int -> inp:int -> int option

type Prop = Prop of int
and internal PropRepr =
    | AndRepr of Prop * Prop
    | OrRepr of Prop * Prop
    | NotRepr of Prop
    | VarRepr of string
    | TrueRepr

open System.Collections.Generic

module PropOps =

    let internal uniqStamp = ref 0
    type internal PropTable() =
        let fwdTable = new Dictionary<PropRepr, Prop>(HashIdentity.Structural)
        let bwdTable = new Dictionary<int, PropRepr>(HashIdentity.Structural)
        member t.ToUnique repr =
            if fwdTable.ContainsKey repr then fwdTable.[repr]
            else let stamp = incr uniqStamp; !uniqStamp
                 let prop = Prop stamp
                 fwdTable.Add (repr, prop)
                 bwdTable.Add (stamp, repr)
                 prop
        member t.FromUnique (Prop stamp) =
            bwdTable.[stamp]

    let internal table = PropTable ()

    // Public construction functions
    let And (p1, p2) = table.ToUnique (AndRepr (p1, p2))
    let Not p = table.ToUnique (NotRepr p)
    let Or (p1, p2)  = table.ToUnique (OrRepr (p1, p2))
    let Var p = table.ToUnique (VarRepr p)
    let True = table.ToUnique TrueRepr
    let False = Not True

    // Deconstruction function
    let internal getRepr p = table.FromUnique p
    
    let (|And|Or|Not|Var|True|) prop =
        match table.FromUnique prop with
        | AndRepr (x, y) -> And (x, y)
        | OrRepr (x, y) -> Or (x, y)
        | NotRepr x -> Not x
        | VarRepr v -> Var v
        | TrueRepr -> True

open PropOps

let rec showProp prec prop =
    let parenIfPrec lim s = if prec < lim then "(" + s + ")" else s
    match prop with
    | Or (p1, p2) -> parenIfPrec 4 (showProp 4 p1 + " || " + showProp 4 p2)
    | And (p1, p2) -> parenIfPrec 3 (showProp 3 p1 + " && " + showProp 3 p2)
    | Not p -> parenIfPrec 2 ("not " + showProp 1 p)
    | Var v -> v
    | True -> "T"

let rec nnf sign prop =
    match prop with
    | And (p1, p2) ->
        if sign then And (nnf sign p1, nnf sign p2)
        else Or (nnf sign p1, nnf sign p2)
    | Or (p1, p2) ->
        if sign then Or (nnf sign p1, nnf sign p2)
        else And (nnf sign p1, nnf sign p2)
    | Not p ->
        nnf (not sign) p
    | Var _ | True ->
        if sign then prop else Not prop

let NNF prop = nnf true prop

//type Prop = | Prop of int
//and internal PropRepr =
//  | AndRepr of Prop * Prop
//  | OrRepr of Prop * Prop
//  | NotRepr of Prop
//  | VarRepr of string
//  | TrueRepr
//module PropOps = begin
//  val internal uniqStamp : int ref = {contents = 2;}
//  type internal PropTable =
//    class
//      new : unit -> PropTable
//      member FromUnique : Prop -> PropRepr
//      member ToUnique : repr:PropRepr -> Prop
//    end
//  val internal table : PropTable
//  val And : p1:Prop * p2:Prop -> Prop
//  val Not : p:Prop -> Prop
//  val Or : p1:Prop * p2:Prop -> Prop
//  val Var : p:string -> Prop
//  val True : Prop = Prop 1
//  val False : Prop = Prop 2
//  val internal getRepr : p:Prop -> PropRepr
//  val ( |And|Or|Not|Var|True| ) :
//    prop:Prop -> Choice<(Prop * Prop),(Prop * Prop),Prop,string,unit>
//end
//val showProp : prec:int -> prop:Prop -> string
//val nnf : sign:bool -> prop:Prop -> Prop
//val NNF : prop:Prop -> Prop

let t1 = Not(And(Not(Var("x")), Not(Var("y"))))
//val t1 : Prop

fsi.AddPrinter(showProp 5)

t1
//val it : Prop = not (not x && not y)

let t2 = Or(Not(Not(Var("x"))), Var("y"))
//val t2 : Prop = not (not x) || y

t2
//val it : Prop = not (not x) || y

(t1 = t2)
//val it : bool = false

NNF t1
//val it : Prop = x || y

NNF t2
//val it : Prop = x || y

NNF t1 = NNF t2
//val it : bool = true

compare
//val it : ('a -> 'a -> int) when 'a : comparison = <fun:it@982>

(=)
//val it : ('a -> 'a -> bool) when 'a : equality = <fun:it@985-1>

(<)
//val it : ('a -> 'a -> bool) when 'a : comparison = <fun:it@988-2>

(<=)
//val it : ('a -> 'a -> bool) when 'a : comparison = <fun:it@991-3>

(>)
//val it : ('a -> 'a -> bool) when 'a : comparison = <fun:it@994-4>

(>=)
//val it : ('a -> 'a -> bool) when 'a : comparison = <fun:it@997-5>

min
//val it : ('a -> 'a -> 'a) when 'a : comparison = <fun:it@1000-6>

max
//val it : ('a -> 'a -> 'a) when 'a : comparison = <fun:it@1003-7>

hash
//val it : ('a -> int) when 'a : equality = <fun:it@1006-8>

let client1 = new System.Net.WebClient()
let client2 = new System.Net.WebClient()
//val client1 : System.Net.WebClient = System.Net.WebClient
//val client2 : System.Net.WebClient = System.Net.WebClient

client1 = client1
//val it : bool = true

client1 = client2
//val it : bool = false

client1 <= client2
// error FS0001: The type 'System.Net.WebClient' does not support the 'comparison' constraint. For example, it does not support the 'System.IComparable' interface

(client1, client2) = (client1, client2)
//val it : bool = true

(client1, client2) = (client2, client1)
//val it : bool = false

(client1, "Data for client 1") <= (client2, " Data for client 2")
// error FS0001: The type 'System.Net.WebClient' does not support the 'comparison' constraint. For example, it does not support the 'System.IComparable' interface

[<StructuralEquality; StructuralComparison>] 
type MiniIntegerContainer = MiniIntegerContainer of int
//type MiniIntegerContainer = | MiniIntegerContainer of int

[<StructuralEquality; StructuralComparison>] 
type MyData = MyData of int * string * string * System.Windows.Forms.Form
// error FS1177: The struct, record or union type 'MyData' has the 'StructuralComparison' attribute but the component type 'System.Windows.Forms.Form' does not satisfy the 'comparison' constraint


/// A type abbreviation indicating we're using integers for unique stamps 
/// on objects 
type stamp = int

/// A structural type containing a function that can't be compared for equality   
[<CustomEquality; CustomComparison>]
type MyThing = 
    {Stamp : stamp;
     Behaviour : (int -> int)}  

    override x.Equals(yobj) = 
        match yobj with
        | :? MyThing as y -> (x.Stamp = y.Stamp)
        | _ -> false

    override x.GetHashCode() = hash x.Stamp
    interface System.IComparable with
      member x.CompareTo yobj = 
          match yobj with
          | :? MyThing as y -> compare x.Stamp y.Stamp
          | _ -> invalidArg "yobj" "cannot compare values of different types"

//type stamp = int
//type MyThing =
//  {Stamp: stamp;
//   Behaviour: int -> int;}
//  with
//    interface System.IComparable
//    override Equals : yobj:obj -> bool
//    override GetHashCode : unit -> int
//  end

let inline equalsOn f x (yobj : obj) = 
    match yobj with
    | :? 'T as y -> (f x = f y)
    | _ -> false

let inline hashOn f x =  hash (f x)

let inline compareOn f x (yobj : obj) = 
    match yobj with
    | :? 'T as y -> compare (f x) (f y)
    | _ -> invalidArg "yobj" "cannot compare values of different types"

type stamp = int

[<CustomEquality; CustomComparison>]
type MyUnionType = 
    | MyUnionType of stamp * (int -> int)  

    static member Stamp (MyUnionType (s, _)) = s

    override x.Equals y = equalsOn MyUnionType.Stamp x y
    override x.GetHashCode() = hashOn MyUnionType.Stamp x
    interface System.IComparable with
        member x.CompareTo y = compareOn MyUnionType.Stamp x y

//val inline equalsOn :
//  f:('T -> 'a) -> x:'T -> yobj:obj -> bool when 'a : equality
//val inline hashOn : f:('a -> 'b) -> x:'a -> int when 'b : equality
//val inline compareOn :
//  f:('T -> 'a) -> x:'T -> yobj:obj -> int when 'a : comparison
//type stamp = int
//type MyUnionType =
//  | MyUnionType of stamp * (int -> int)
//  with
//    interface System.IComparable
//    override Equals : y:obj -> bool
//    override GetHashCode : unit -> int
//    static member Stamp : MyUnionType -> stamp
//  end

[<ReferenceEquality>] 
type MyFormWrapper = MyFormWrapper of System.Windows.Forms.Form * (int -> int)
//type MyFormWrapper =
//  | MyFormWrapper of System.Windows.Forms.Form * (int -> int)

[<NoEquality; NoComparison>]
type MyProjections = 
    | MyProjections of (int * string) * (string -> int)  
//type MyProjections = | MyProjections of (int * string) * (string -> int)

type MiniContainer<'T> = MiniContainer of 'T
//type MiniContainer<'T> = | MiniContainer of 'T

type MiniContainer<[<EqualityConditionalOn; ComparisonConditionalOn >]'T>(x : 'T) =
    member x.Value = x
    override x.Equals(yobj) = 
        match yobj with
        | :? MiniContainer<'T> as y -> Unchecked.equals x.Value y.Value
        | _ -> false

    override x.GetHashCode() = Unchecked.hash x.Value

    interface System.IComparable with
      member x.CompareTo yobj = 
          match yobj with
          | :? MiniContainer<'T> as y -> Unchecked.compare x.Value y.Value
          | _ -> invalidArg "yobj" "cannot compare values of different types"
//type MiniContainer<'T> =
//  class
//    interface System.IComparable
//    new : x:'T -> MiniContainer<'T>
//    override Equals : yobj:obj -> bool
//    override GetHashCode : unit -> int
//    member Value : MiniContainer<'T>
//  end

let rec deepRecursion n =
    if n = 1000000 then () else
    if n % 100 = 0 then
        printfn "--> deepRecursion, n = %d" n
    deepRecursion (n+1)
    printfn "<-- deepRecursion, n = %d" n
//val deepRecursion : n:int -> unit

deepRecursion 0
//--> deepRecursion, n = 0
//...
//--> deepRecursion, n = 63500
//--> deepRecursion, n = 63600
//--> deepRecursion, n = 63700
//Session termination detected. Press Enter to restart.

let rec tailCallRecursion n : unit =
    if n = 1000000 then () else
    if n % 100 = 0 then
        printfn "--> tailCallRecursion, n = %d" n
    tailCallRecursion (n + 1)
//val tailCallRecursion : n:int -> unit

tailCallRecursion 0
//--> tailCallRecursion, n = 0
//...
//--> tailCallRecursion, n = 999600
//--> tailCallRecursion, n = 999700
//--> tailCallRecursion, n = 999800
//--> tailCallRecursion, n = 999900
//val it : unit = ()

let rec last l =
    match l with
    | [] -> invalidArg "l" "the input list should not be empty"
    | [h] -> h
    | h::t -> last t

let rec replicateNotTailRecursiveA n x =
    if n <= 0 then []
    else x :: replicateNotTailRecursiveA (n - 1) x

let rec replicateNotTailRecursiveB n x =
    if n <= 0 then []
    else
        let recursiveResult = replicateNotTailRecursiveB (n - 1) x
        x :: recursiveResult

let rec replicateAux n x acc =
    if n <= 0 then acc
    else replicateAux (n - 1) x (x :: acc)

let replicate n x = replicateAux n x []
//val last : l:'a list -> 'a
//val replicateNotTailRecursiveA : n:int -> x:'a -> 'a list
//val replicateNotTailRecursiveB : n:int -> x:'a -> 'a list
//val replicateAux : n:int -> x:'a -> acc:'a list -> 'a list
//val replicate : n:int -> x:'a -> 'a list

let replicate n x =
    let rec loop i acc =
        if i >= n then acc
        else loop (i + 1) (x :: acc)
    loop 0 []

let rec mapNotTailRecursive f inputList =
    match inputList with
    | [] -> []
    | h :: t -> (f h) :: mapNotTailRecursive f t

let rec mapIncorrectAcc f inputList acc =
    match inputList with
    | [] -> acc            // whoops! Forgot to reverse the accumulator here!
    | h :: t -> mapIncorrectAcc f t (f h :: acc)

//let mapIncorrect f inputList = mapIncorrectAcc f inputList []
//val replicate : n:int -> x:'a -> 'a list
//val mapNotTailRecursive : f:('a -> 'b) -> inputList:'a list -> 'b list
//val mapIncorrectAcc :
//  f:('a -> 'b) -> inputList:'a list -> acc:'b list -> 'b list
//val mapIncorrect : f:('a -> 'b) -> inputList:'a list -> 'b list

mapIncorrect (fun x -> x * x) [1; 2; 3; 4]
//val it : int list = [16; 9; 4; 1]

let rec mapAcc f inputList acc =
    match inputList with
    | [] -> List.rev acc
    | h :: t -> mapAcc f t (f h :: acc)

let map f inputList = mapAcc f inputList []
//val mapAcc : f:('a -> 'b) -> inputList:'a list -> acc:'b list -> 'b list
//val map : f:('a -> 'b) -> inputList:'a list -> 'b list

map (fun x -> x * x) [1; 2; 3; 4]
//val it : int list = [1; 4; 9; 16]

type Chain =
    | ChainNode of int * string * Chain
    | ChainEnd of string

    member chain.LengthNotTailRecursive =
        match chain with
        | ChainNode(_, _, subChain) -> 1 + subChain.LengthNotTailRecursive
        | ChainEnd _ -> 0
//type Chain =
//  | ChainNode of int * string * Chain
//  | ChainEnd of string
//  with
//    member LengthNotTailRecursive : int
//  end

type Chain =
    | ChainNode of int * string * Chain
    | ChainEnd of string

    // The implementation of this property is tail recursive.
    member chain.Length =
        let rec loop c acc =
            match c with
            | ChainNode(_, _, subChain) -> loop subChain (acc + 1)
            | ChainEnd _ -> acc
        loop chain 0
//type Chain =
//  | ChainNode of int * string * Chain
//  | ChainEnd of string
//  with
//    member Length : int
//  end

type Tree =
    | Node of string * Tree * Tree
    | Tip of string

let rec sizeNotTailRecursive tree =
    match tree with
    | Tip _ -> 1
    | Node(_, treeLeft, treeRight) ->
        sizeNotTailRecursive treeLeft + sizeNotTailRecursive treeRight

let rec mkBigUnbalancedTree n tree =
    if n = 0 then tree
    else Node("node", Tip("tip"), mkBigUnbalancedTree (n - 1) tree)
//type Tree =
//  | Node of string * Tree * Tree
//  | Tip of string
//val sizeNotTailRecursive : tree:Tree -> int
//val mkBigUnbalancedTree : n:int -> tree:Tree -> Tree

let tree1 = Tip("tip")
let tree2 = mkBigUnbalancedTree 10000 tree1
let tree3 = mkBigUnbalancedTree 10000 tree2
let tree4 = mkBigUnbalancedTree 10000 tree3
let tree5 = mkBigUnbalancedTree 10000 tree4
let tree6 = mkBigUnbalancedTree 10000 tree5
//val tree1 : Tree = Tip "tip"
//val tree2 : Tree =
//  Node
//    ("node",Tip "tip",
//     Node
//       ("node",Tip "tip",
//        Node
//          ("node",Tip "tip",
//           Node
//             ("node",Tip "tip",
//              Node
//                ("node",Tip "tip", ...

let rec sizeAcc acc tree =
    match tree with
    | Tip _ -> 1 + acc
    | Node(_, treeLeft, treeRight) ->
        let acc = sizeAcc acc treeLeft
        sizeAcc acc treeRight

let size tree = sizeAcc 0 tree
//val sizeAcc : acc:int -> tree:Tree -> int
//val size : tree:Tree -> int

let rec sizeCont tree cont =
    match tree with
    | Tip _ -> cont 1
    | Node(_, treeLeft, treeRight) ->
        sizeCont treeLeft (fun leftSize ->
          sizeCont treeRight (fun rightSize ->
            cont (leftSize + rightSize)))

let size tree = sizeCont tree (fun x -> x)
//val sizeCont : tree:Tree -> cont:(int -> 'a) -> 'a
//val size : tree:Tree -> int

size tree6
//val it : int = 50001

let rec sizeContAcc acc tree cont =
    match tree with
    | Tip _ -> cont (1 + acc)
    | Node (_, treeLeft, treeRight) ->
        sizeContAcc acc treeLeft (fun accLeftSize ->
        sizeContAcc accLeftSize treeRight cont)

let size tree = sizeContAcc 0 tree (fun x -> x)
//val sizeContAcc : acc:int -> tree:Tree -> cont:(int -> 'a) -> 'a
//val size : tree:Tree -> int

type Expr =
    | Add of Expr * Expr
    | Bind of string * Expr * Expr
    | Var of string
    | Num of int

type Env = Map<string, int>

let rec eval (env : Env) expr =
    match expr with
    | Add (e1, e2) -> eval env e1 + eval env e2
    | Bind (var, rhs, body) -> eval (env.Add(var, eval env rhs)) body
    | Var var -> env.[var]
    | Num n -> n

let rec evalCont (env : Env) expr cont =
    match expr with
    | Add (e1, e2) ->
        evalCont env e1 (fun v1 ->
        evalCont env e2 (fun v2 ->
        cont (v1 + v2)))
    | Bind (var, rhs, body) ->
        evalCont env rhs (fun v1 ->
        evalCont (env.Add(var, v1)) body cont)
    | Num n ->
        cont n
    | Var var ->
        cont (env.[var])
//type Expr =
//  | Add of Expr * Expr
//  | Bind of string * Expr * Expr
//  | Var of string
//  | Num of int
//type Env = Map<string,int>
//val eval : env:Env -> expr:Expr -> int
//val evalCont : env:Env -> expr:Expr -> cont:(int -> 'a) -> 'a

let eval env expr = evalCont env expr (fun x -> x)
//val eval : env:Env -> expr:Expr -> int