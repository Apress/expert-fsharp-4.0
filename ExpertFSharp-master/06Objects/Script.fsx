/// A two-dimensional vector as a record
type Vector2D =
    {DX : float; DY : float}
    
    /// The length of this vector
    member v.Length = sqrt(v.DX * v.DX + v.DY * v.DY)
    
    /// A vector scaled from this vector
    member v.Scale(k) = {DX = k * v.DX; DY = k * v.DY}
    
    /// A vector translated in X from this vector
    member v.ShiftX(x) = {v with DX = v.DX + x}

    /// A vector translated in Y from this vector
    member v.ShiftY(y) = {v with DY = v.DY + y}

    /// A vector translated from this vector
    member v.ShiftXY(x, y) = {DX = v.DX + x; DY = v.DY + y}

    /// The zero vector
    static member Zero = {DX = 0.0; DY = 0.0}

    /// A constant vector along the X axis
    static member ConstX(dx) = {DX = dx; DY = 0.0}

    /// A constant vector along the Y axis
    static member ConstY(dy) = {DX = 0.0; DY = dy}

//type Vector2D =
//  {DX: float;
//   DY: float;}
//  with
//    member Scale : k:float -> Vector2D
//    member ShiftX : x:float -> Vector2D
//    member ShiftXY : x:float * y:float -> Vector2D
//    member ShiftY : y:float -> Vector2D
//    member Length : float
//    static member ConstX : dx:float -> Vector2D
//    static member ConstY : dy:float -> Vector2D
//    static member Zero : Vector2D
//  end

let v = {DX = 3.0; DY = 4.0}
//val v : Vector2D = {DX = 3.0;
//                    DY = 4.0;}

v.Length
//val it : float = 5.0

v.Scale(2.0).Length
//val it : float = 10.0

Vector2D.ConstX(3.0)
//val it : Vector2D = {DX = 3.0;
//                     DY = 0.0;}

type Vector2D =
    {DX : float; DY : float}
    member v.Scale(k) = {DX = k * v.DX; DY = k * v.DY}
    member v.ShiftX(x) = {v with DX = v.DX + x}
    member v.ShiftY(y) = {v with DY = v.DY + y}
    member v.ShiftXY(x, y) = {DX = v.DX + x; DY = v.DY + y}
    static member Zero = {DX = 0.0; DY = 0.0}
    static member ConstX(dx) = {DX = dx; DY = 0.0}
    static member ConstY(dy) = {DX = 0.0; DY = dy}
    member v.Length = sqrt(v.DX * v.DX + v.DY * v.DY)
    member v.LengthWithSideEffect =
        printfn "Computing!"
        sqrt(v.DX * v.DX + v.DY * v.DY)

//type Vector2D =
//  {DX: float;
//   DY: float;}
//  with
//    member Scale : k:float -> Vector2D
//    member ShiftX : x:float -> Vector2D
//    member ShiftXY : x:float * y:float -> Vector2D
//    member ShiftY : y:float -> Vector2D
//    member Length : float
//    member LengthWithSideEffect : float
//    static member ConstX : dx:float -> Vector2D
//    static member ConstY : dy:float -> Vector2D
//    static member Zero : Vector2D
//  end

let x = {DX = 3.0; DY = 4.0}
//val x : Vector2D = {DX = 3.0;
//                    DY = 4.0;}

x.LengthWithSideEffect
//Computing!
//val it : float = 5.0

x.LengthWithSideEffect
//Computing!
//val it : float = 5.0

type Tree<'T> =
    | Node of 'T * Tree<'T> * Tree<'T>
    | Tip 

    member t.Size =
        match t with
        | Node(_, l, r) -> 1 + l.Size + r.Size
        | Tip -> 0

//type Tree<'T> =
//  | Node of 'T * Tree<'T> * Tree<'T>
//  | Tip
//  with
//    member Size : int
//  end

type Vector2D(dx : float, dy : float) =
    
    let len = sqrt(dx * dx + dy * dy)

    /// The X component of this vector
    member v.DX = dx

    /// The Y component of this vector
    member v.DY = dy

    member v.Length = len
    member v.Scale(k) = Vector2D(k * dx, k * dy)
    member v.ShiftX(x) = Vector2D(dx = dx + x, dy = dy)
    member v.ShiftY(y) = Vector2D(dx = dx, dy = dy + y)
    member v.ShiftXY(x, y) = Vector2D(dx = dx + x, dy = dy + y)
    static member Zero = Vector2D(dx = 0.0, dy = 0.0)

    /// A unit vector in the X axis
    static member OneX = Vector2D(dx = 1.0, dy = 0.0)

    /// A unit vector in the Y axis
    static member OneY = Vector2D(dx = 0.0, dy = 1.0)

//type Vector2D =
//  class
//    new : dx:float * dy:float -> Vector2D
//    member Scale : k:float -> Vector2D
//    member ShiftX : x:float -> Vector2D
//    member ShiftXY : x:float * y:float -> Vector2D
//    member ShiftY : y:float -> Vector2D
//    member DX : float
//    member DY : float
//    member Length : float
//    static member OneX : Vector2D
//    static member OneY : Vector2D
//    static member Zero : Vector2D
//  end

let v = Vector2D(3.0, 4.0)
//val v : Vector2D

v.Length
//val it : float = 5.0

v.Scale(2.0).Length
//val it : float = 10.0

/// Vectors whose length is checked to be close to length one.
type UnitVector2D(dx, dy) =
    let tolerance = 0.000001

    let length = sqrt (dx * dx + dy * dy)

    do if abs (length - 1.0) >= tolerance then failwith "not a unit vector";

    member v.DX = dx

    member v.DY = dy

    new() = UnitVector2D (1.0, 0.0)

//type UnitVector2D =
//  class
//    new : unit -> UnitVector2D
//    new : dx:float * dy:float -> UnitVector2D
//    member DX : float
//    member DY : float
//  end

/// A class including some static bindings.
type Vector2D(dx : float, dy : float) =
    
    static let zero = Vector2D(0.0, 0.0)
    static let onex = Vector2D(1.0, 0.0)
    static let oney = Vector2D(0.0, 1.0)

    /// The zero vector
    static member Zero = zero

    /// A unit vector in the X axis
    static member OneX = onex

    /// A unit vector in the Y axis
    static member OneY = oney

//type Vector2D =
//  class
//    new : dx:float * dy:float -> Vector2D
//    static member OneX : Vector2D
//    static member OneY : Vector2D
//    static member Zero : Vector2D
//  end

open System.Collections.Generic

type SparseVector(items : seq<int * float>)=
    let elems = new SortedDictionary<_, _>()
    do items |> Seq.iter (fun (k, v) -> elems.Add(k, v))

    /// This defines an indexer property
    member t.Item
        with get(idx) =
            if elems.ContainsKey(idx) then elems.[idx]
            else 0.0

//type SparseVector =
//  class
//    new : items:seq<int * float> -> SparseVector
//    member Item : idx:int -> float with get
//  end

let v = SparseVector [(3, 547.0)]
//val v : SparseVector

v.[4]
//val it : float = 0.0

v.[3]
//val it : float = 547.0

type Vector2DWithOperators(dx : float, dy : float) =
    member x.DX = dx
    member x.DY = dy

    static member (+) (v1 : Vector2DWithOperators, v2 : Vector2DWithOperators) =
        Vector2DWithOperators(v1.DX + v2.DX, v1.DY + v2.DY)

    static member (-) (v1 : Vector2DWithOperators, v2 : Vector2DWithOperators) =
        Vector2DWithOperators (v1.DX - v2.DX, v1.DY - v2.DY)

//type Vector2DWithOperators =
//  class
//    new : dx:float * dy:float -> Vector2DWithOperators
//    member DX : float
//    member DY : float
//    static member
//      ( + ) : v1:Vector2DWithOperators * v2:Vector2DWithOperators ->
//                Vector2DWithOperators
//    static member
//      ( - ) : v1:Vector2DWithOperators * v2:Vector2DWithOperators ->
//                Vector2DWithOperators
//  end

let v1 = new Vector2DWithOperators (3.0, 4.0)
//val v1 : Vector2DWithOperators

v1 + v1
//val it : Vector2DWithOperators = FSI_0032+Vector2DWithOperators {DX = 6.0;
//                                                                 DY = 8.0;}

v1 - v1
//val it : Vector2DWithOperators = FSI_0032+Vector2DWithOperators {DX = 0.0;
//                                                                 DY = 0.0;}

open System.Drawing

type LabelInfo(?text : string, ?font : Font) =
    let text = defaultArg text ""
    let font = match font with
               | None -> new Font(FontFamily.GenericSansSerif, 12.0f)
               | Some v -> v
    member x.Text = text
    member x.Font = font

    /// Define a static method which creates an instance
    static member Create(?text, ?font) =  new LabelInfo(?text = text, ?font = font)

//type LabelInfo =
//  class
//    new : ?text:string * ?font:System.Drawing.Font -> LabelInfo
//    member Font : System.Drawing.Font
//    member Text : string
//    static member
//      Create : ?text:string * ?font:System.Drawing.Font -> LabelInfo
//  end

LabelInfo (text = "Hello World")
//val it : LabelInfo =
//  FSI_0036+LabelInfo
//    {Font = [Font: Name=Microsoft Sans Serif, Size=12, Units=3, GdiCharSet=1, GdiVerticalFont=False];
//     Text = "Hello World";}

LabelInfo("Goodbye Lenin")
//val it : LabelInfo =
//  FSI_0036+LabelInfo
//    {Font = [Font: Name=Microsoft Sans Serif, Size=12, Units=3, GdiCharSet=1, GdiVerticalFont=False];
//     Text = "Goodbye Lenin";}

LabelInfo(font = new Font(FontFamily.GenericMonospace, 36.0f), text = "Imagine")
//val it : LabelInfo =
//  FSI_0036+LabelInfo
//    {Font = [Font: Name=Courier New, Size=36, Units=3, GdiCharSet=1, GdiVerticalFont=False];
//     Text = "Imagine";}

type Interval(lo, hi) =
    member r.Lo = lo
    member r.Hi = hi
    member r.IsEmpty = hi <= lo
    member r.Contains v = lo < v && v < hi

    static member Empty = Interval(0.0, 0.0)

    /// Return the smallest interval that covers both the intervals
    /// This method is overloaded.
    static member Span (r1 : Interval, r2 : Interval) =
        if r1.IsEmpty then r2 else
        if r2.IsEmpty then r1 else
        Interval(min r1.Lo r2.Lo, max r1.Hi r2.Hi)

    /// Return the smallest interval that covers all the intervals
    /// This method is overloaded.
    static member Span(ranges : seq<Interval>) =
        Seq.fold (fun r1 r2 -> Interval.Span(r1, r2)) Interval.Empty ranges

//type Interval =
//  class
//    new : lo:float * hi:float -> Interval
//    member Contains : v:float -> bool
//    member Hi : float
//    member IsEmpty : bool
//    member Lo : float
//    static member Span : ranges:seq<Interval> -> Interval
//    static member Span : r1:Interval * r2:Interval -> Interval
//    static member Empty : Interval
//  end

type Vector =
    {DX : float; DY : float}
    member v.Length = sqrt( v.DX * v.DX + v.DY * v.DY)

//type Vector =
//  {DX: float;
//   DY: float;}
//  with
//    member Length : float
//  end

type Point =
    {X : float; Y : float }

    static member (-) (p1 : Point, p2 : Point) =
        {DX = p1.X - p2.X; DY = p1.Y - p2.Y}

    static member (-) (p : Point, v : Vector) =
        {X = p.X - v.DX; Y = p.Y - v.DY}

//type Point =
//  {X: float;
//   Y: float;}
//  with
//    static member ( - ) : p1:Point * p2:Point -> Vector
//    static member ( - ) : p:Point * v:Vector -> Point
//  end

type MutableVector2D(dx : float, dy : float) =
    let mutable currDX = dx
    let mutable currDY = dy

    member vec.DX with get() = currDX and set v = currDX <- v
    member vec.DY with get() = currDY and set v = currDY <- v

    member vec.Length
         with get () = sqrt (currDX * currDX + currDY * currDY)
         and set len =
             let theta = vec.Angle
             currDX <- cos theta * len
             currDY <- sin theta * len

    member vec.Angle
         with get () = atan2 currDY currDX
         and set theta =
             let len = vec.Length
             currDX <- cos theta * len
             currDY <- sin theta * len

//type MutableVector2D =
//  class
//    new : dx:float * dy:float -> MutableVector2D
//    member Angle : float
//    member DX : float
//    member DY : float
//    member Length : float
//    member Angle : float with set
//    member DX : float with set
//    member DY : float with set
//    member Length : float with set
//  end

    
let v = MutableVector2D(3.0, 4.0)
//val v : MutableVector2D

(v.DX, v.DY)
//val it : float * float = (3.0, 4.0)

(v.Length, v.Angle)
//val it : float * float = (5.0, 0.927295218)

v.Angle <- System.Math.PI / 6.0 // "30 degrees"
//val it : unit = ()

(v.DX, v.DY)
//val it : float * float = (4.330127019, 2.5)

(v.Length, v.Angle)
//val it : float * float = (5.0, 0.523598775)

open System.Collections.Generic

type IntegerMatrix(rows : int, cols : int)=
    let elems = Array2D.zeroCreate<int> rows cols 

    /// This defines an indexer property with getter and setter
    member t.Item
        with get (idx1, idx2) = elems.[idx1, idx2]
        and set (idx1, idx2) v = elems.[idx1, idx2] <- v

//type IntegerMatrix =
//  class
//    new : rows:int * cols:int -> IntegerMatrix
//    member Item : idx1:int * idx2:int -> int with get
//    member Item : idx1:int * idx2:int -> int with set
//  end

open System.Windows.Forms

let form = new Form(Visible = true, TopMost = true, Text = "Welcome to F#")

open System.Windows.Forms

let form =
    let tmp = new Form()
    tmp.Visible <- true
    tmp.TopMost <- true
    tmp.Text <- "Welcome to F#"
    tmp

open System.Drawing

type LabelInfoWithPropertySetting() =
    let mutable text = "" // the default
    let mutable font = new Font(FontFamily.GenericSansSerif, 12.0f)
    member x.Text with get() = text and set v = text <- v
    member x.Font with get() = font and set v = font <- v

//type LabelInfoWithPropertySetting =
//  class
//    new : unit -> LabelInfoWithPropertySetting
//    member Font : Font
//    member Text : string
//    member Font : Font with set
//    member Text : string with set
//  end

LabelInfoWithPropertySetting(Text="Hello World")
//val it : LabelInfoWithPropertySetting =
//  FSI_0053+LabelInfoWithPropertySetting
//    {Font = [Font: Name=Microsoft Sans Serif, Size=12, Units=3, GdiCharSet=1, GdiVerticalFont=False];
//     Text = "Hello World";}

type LabelInfoWithPropertySetting() =
    member val Name = "label" 
    member val Text = "" with get, set 
    member val Font = new Font(FontFamily.GenericSansSerif, 12.0f) with get, set

//type LabelInfoWithPropertySetting =
//  class
//    new : unit -> LabelInfoWithPropertySetting
//    member Font : Font
//    member Name : string
//    member Text : string
//    member Font : Font with set
//    member Text : string with set
//  end

open System.Drawing

type IShape =
    abstract Contains : Point -> bool
    abstract BoundingBox : Rectangle

let circle (center : Point, radius : int) =
    {new IShape with

          member x.Contains(p : Point) =
              let dx = float32 (p.X - center.X)
              let dy = float32 (p.Y - center.Y)
              sqrt(dx * dx + dy * dy) <= float32 radius

          member x.BoundingBox =
              Rectangle(
                  center.X - radius, center.Y - radius,
                  2 * radius + 1, 2 * radius + 1)}

let square (center : Point, side : int) =
    {new IShape with

          member x.Contains(p : Point) =
              let dx = p.X - center.X
              let dy = p.Y - center.Y
              abs(dx) < side / 2 && abs(dy) < side / 2

          member x.BoundingBox =
              Rectangle(center.X - side, center.Y - side, side * 2, side * 2)}

type MutableCircle() =

    member val Center = Point(x = 0, y = 0) with get, set
    member val Radius = 10 with get, set

    member c.Perimeter = 2.0 * System.Math.PI * float c.Radius

    interface IShape with

        member c.Contains(p : Point) =
            let dx = float32 (p.X - c.Center.X)
            let dy = float32 (p.Y - c.Center.Y)
            sqrt(dx * dx + dy * dy) <= float32 c.Radius

        member c.BoundingBox =
            Rectangle(
                c.Center.X - c.Radius, c.Center.Y - c.Radius,
                2 * c.Radius + 1, 2 * c.Radius + 1)

//type IShape =
//  interface
//    abstract member Contains : Point -> bool
//    abstract member BoundingBox : Rectangle
//  end
//val circle : center:Point * radius:int -> IShape
//val square : center:Point * side:int -> IShape
//type MutableCircle =
//  class
//    interface IShape
//    new : unit -> MutableCircle
//    member Center : Point
//    member Perimeter : float
//    member Radius : int
//    member Center : Point with set
//    member Radius : int with set
//  end

open System.Drawing

type IShape =
    abstract Contains : Point -> bool
    abstract BoundingBox : Rectangle

let circle(center : Point, radius : int) =
    {new IShape with
          member x.Contains(p : Point) =
              let dx = float32 (p.X - center.X)
              let dy = float32 (p.Y - center.Y)
              sqrt(dx * dx + dy * dy) <= float32 radius
          member x.BoundingBox =
              Rectangle(
                  center.X - radius, center.Y - radius,
                  2 * radius + 1, 2 * radius + 1)}

//type IShape =
//  interface
//    abstract member Contains : Point -> bool
//    abstract member BoundingBox : Rectangle
//  end
//val circle : center:Point * radius:int -> IShape

let bigCircle = circle(Point(0, 0), 100)
//val bigCircle : IShape

bigCircle.BoundingBox
//val it : Rectangle =
//  {X=-100,Y=-100,Width=201,Height=201} {Bottom = 101;
//                                        Height = 201;
//                                        IsEmpty = false;
//                                        Left = -100;
//                                        Location = {X=-100,Y=-100};
//                                        Right = 101;
//                                        Size = {Width=201, Height=201};
//                                        Top = -100;
//                                        Width = 201;
//                                        X = -100;
//                                        Y = -100;}

bigCircle.Contains(Point(70, 70))
//val it : bool = true

bigCircle.Contains(Point(71, 71))
//val it : bool = false

let smallSquare = square(Point(1, 1), 1)
//val smallSquare : IShape

smallSquare.BoundingBox
//val it : Rectangle = {X=0,Y=0,Width=2,Height=2} {Bottom = 2;
//                                                 Height = 2;
//                                                 IsEmpty = false;
//                                                 Left = 0;
//                                                 Location = {X=0,Y=0};
//                                                 Right = 2;
//                                                 Size = {Width=2, Height=2};
//                                                 Top = 0;
//                                                 Width = 2;
//                                                 X = 0;
//                                                 Y = 0;}

smallSquare.Contains(Point(0, 0))
//val it : bool = false

type MutableCircle() =
    let radius = 10
    member val Center = Point(x = 0, y = 0) with get, set
    member val Radius = radius with get, set
    member c.Perimeter = 2.0 * System.Math.PI * float radius

    interface IShape with
        member c.Contains(p : Point) =
            let dx = float32 (p.X - c.Center.X)
            let dy = float32 (p.Y - c.Center.Y)
            sqrt(dx * dx + dy * dy) <= float32 c.Radius

        member c.BoundingBox =
            Rectangle(
                c.Center.X - c.Radius, c.Center.Y - c.Radius,
                2 * c.Radius + 1, 2 * c.Radius + 1)

//type MutableCircle =
//  class
//    interface IShape
//    new : unit -> MutableCircle
//    member Center : Point
//    member Perimeter : float
//    member Radius : int
//    member Center : Point with set
//    member Radius : int with set
//  end
    
let circle2 = MutableCircle()
//val circle2 : MutableCircle

circle2.Radius
//val it : int = 10

(circle2 :> IShape).BoundingBox
//val it : Rectangle =
//  {X=-10,Y=-10,Width=21,Height=21} {Bottom = 11;
//                                    Height = 21;
//                                    IsEmpty = false;
//                                    Left = -10;
//                                    Location = {X=-10,Y=-10};
//                                    Right = 11;
//                                    Size = {Width=21, Height=21};
//                                    Top = -10;
//                                    Width = 21;
//                                    X = -10;
//                                    Y = -10;}

open System.Text

/// An object interface type that consumes characters and strings
type ITextOutputSink =

    /// When implemented, writes one Unicode character to the sink
    abstract WriteChar : char -> unit

    /// When implemented, writes one Unicode string to the sink
    abstract WriteString : string -> unit

/// Returns an object that implements ITextOutputSink by using writeCharFunction
let simpleOutputSink writeCharFunction =
    {new ITextOutputSink with
         member x.WriteChar(c) = writeCharFunction c
         member x.WriteString(s) = s |> String.iter x.WriteChar}

let stringBuilderOuputSink (buf : StringBuilder) =
    simpleOutputSink (fun c -> buf.Append(c) |> ignore)

//type ITextOutputSink =
//  interface
//    abstract member WriteChar : char -> unit
//    abstract member WriteString : string -> unit
//  end
//val simpleOutputSink : writeCharFunction:(char -> unit) -> ITextOutputSink
//val stringBuilderOuputSink : buf:StringBuilder -> ITextOutputSink

let buf = new StringBuilder()
//val buf : StringBuilder = 

let c = stringBuilderOuputSink(buf)
//val c : ITextOutputSink

["Incy"; " "; "Wincy"; " "; "Spider"] |> List.iter c.WriteString
//val it : unit = ()

buf.ToString()
//val it : string = "Incy Wincy Spider"

/// A type which fully implements the ITextOutputSink object interface
type CountingOutputSink(writeCharFunction : char -> unit) =

    let mutable count = 0

    interface ITextOutputSink with
        member x.WriteChar(c) = count <- count + 1; writeCharFunction(c)
        member x.WriteString(s) = s |> String.iter (x :> ITextOutputSink).WriteChar

    member x.Count = count

//type CountingOutputSink =
//  class
//    interface ITextOutputSink
//    new : writeCharFunction:(char -> unit) -> CountingOutputSink
//    member Count : int
//  end

/// A type whose members are partially implemented
[<AbstractClass>]
type TextOutputSink() =
    abstract WriteChar : char -> unit
    abstract WriteString : string -> unit
    default x.WriteString s = s |> String.iter x.WriteChar

//type TextOutputSink =
//  class
//    new : unit -> TextOutputSink
//    abstract member WriteChar : char -> unit
//    abstract member WriteString : string -> unit
//    override WriteString : s:string -> unit
//  end

{new TextOutputSink() with
     member x.WriteChar c = System.Console.Write(c)}
//val it : TextOutputSink = FSI_0080+it@751-1

/// A type which uses a TextOutputSink internally
type HtmlWriter() =
    let mutable count = 0
    let sink =
        {new TextOutputSink() with
             member x.WriteChar c =
                 count <- count + 1;
                 System.Console.Write c}

    member x.CharCount = count
    member x.OpenTag(tagName) = sink.WriteString(sprintf "<%s>" tagName)
    member x.CloseTag(tagName) = sink.WriteString(sprintf "</%s>" tagName)
    member x.WriteString(s) = sink.WriteString(s)

//type HtmlWriter =
//  class
//    new : unit -> HtmlWriter
//    member CloseTag : tagName:string -> unit
//    member OpenTag : tagName:string -> unit
//    member WriteString : s:string -> unit
//    member CharCount : int
//  end

/// An implementation of TextOutputSink, counting the number of bytes written
type CountingOutputSinkByInheritance() =
    inherit TextOutputSink()

    let mutable count = 0

    member sink.Count = count

    default sink.WriteChar c = 
        count <- count + 1; 
        System.Console.Write c

//type CountingOutputSinkByInheritance =
//  class
//    inherit TextOutputSink
//    new : unit -> CountingOutputSinkByInheritance
//    override WriteChar : c:char -> unit
//    member Count : int
//  end

{new TextOutputSink() with
     member sink.WriteChar c = System.Console.Write c
     member sink.WriteString s = System.Console.Write s }
//val it : TextOutputSink = FSI_0083+it@798-2

open System.Text

/// A component to write bytes to an output sink
[<AbstractClass>]
type ByteOutputSink() =
    inherit TextOutputSink()

    /// When implemented, writes one byte to the sink
    abstract WriteByte : byte -> unit

    /// When implemented, writes multiple bytes to the sink
    abstract WriteBytes : byte[] -> unit

    default sink.WriteChar c = sink.WriteBytes(Encoding.UTF8.GetBytes [|c|])

    override sink.WriteString s = sink.WriteBytes(Encoding.UTF8.GetBytes s) 

    default sink.WriteBytes b = b |> Array.iter sink.WriteByte 
//
//type ByteOutputSink =
//  class
//    inherit TextOutputSink
//    new : unit -> ByteOutputSink
//    abstract member WriteByte : byte -> unit
//    abstract member WriteBytes : byte [] -> unit
//    override WriteBytes : b:byte [] -> unit
//    override WriteChar : c:char -> unit
//    override WriteString : s:string -> unit
//  end

open System.IO

let myWriteStringToFile() =
    use outp = File.CreateText("playlist.txt")
    outp.WriteLine("Enchanted")
    outp.WriteLine("Put your records on")
//val myWriteStringToFile : unit -> unit

let myWriteStringToFile () =
    let outp = File.CreateText("playlist.txt")
    try 
        outp.WriteLine("Enchanted")
        outp.WriteLine("Put your records on")
    finally
       (outp :> System.IDisposable).Dispose()
//val myWriteStringToFile : unit -> unit

let http (url : string) =
    let req = System.Net.WebRequest.Create url
    use resp = req.GetResponse()
    use stream = resp.GetResponseStream()
    use reader = new System.IO.StreamReader(stream)
    let html = reader.ReadToEnd()
    html
//val http : url:string -> string

open System.IO

type LineChooser(fileName1, fileName2) =
    let file1 = File.OpenText(fileName1)
    let file2 = File.OpenText(fileName2)
    let rnd = new System.Random()

    let mutable disposed = false

    let cleanup() =
        if not disposed then
            disposed <- true;
            file1.Dispose();
            file2.Dispose();

    interface System.IDisposable with
        member x.Dispose() = cleanup()

    member obj.CloseAll() = cleanup()

    member obj.GetLine() =
        if not file1.EndOfStream &&
           (file2.EndOfStream  || rnd.Next() % 2 = 0) then file1.ReadLine()
        elif not file2.EndOfStream then file2.ReadLine()
        else raise (new EndOfStreamException())

//type LineChooser =
//  class
//    interface System.IDisposable
//    new : fileName1:string * fileName2:string -> LineChooser
//    member CloseAll : unit -> unit
//    member GetLine : unit -> string
//  end

open System; open System.IO
File.WriteAllLines("test1.txt", [|"Daisy, Daisy"; "Give me your hand oh do"|])
File.WriteAllLines("test2.txt", [|"I'm a little teapot"; "Short and stout"|])
let chooser = new LineChooser ("test1.txt", "test2.txt")
//val chooser : LineChooser

chooser.GetLine()
//val it : string = "Daisy, Daisy"

chooser.GetLine()
//val it : string = "Give me your hand oh do"

(chooser :> IDisposable).Dispose()
chooser.GetLine()
//System.ObjectDisposedException: Cannot read from a closed TextReader.
//   at System.IO.__Error.ReaderClosed()
//>    at System.IO.StreamReader.get_EndOfStream()
//   at FSI_0088.LineChooser.GetLine() in C:\...\06Objects\Script.fsx:line 880
//   at <StartupCode$FSI_0097>.$FSI_0097.main@() in C:\...\06Objects\Script.fsx:line 905
//Stopped due to error

open System

type TicketGenerator() =
    let mutable free = []
    let mutable max = 0
    member h.Alloc() =
        match free with
        | [] -> max <- max + 1; max
        | h :: t -> free <- t; h
    member h.Dealloc(n:int) =
        printfn "returning ticket %d" n
        free <- n :: free

let ticketGenerator = new TicketGenerator()

type Customer() =
    let myTicket = ticketGenerator.Alloc()
    let mutable disposed = false
    let cleanup() =
         if not disposed then
             disposed <- true
             ticketGenerator.Dealloc(myTicket)
    member x.Ticket = myTicket
    override x.Finalize() = cleanup()
    interface IDisposable with
         member x.Dispose() = cleanup(); GC.SuppressFinalize(x)

//type TicketGenerator =
//  class
//    new : unit -> TicketGenerator
//    member Alloc : unit -> int
//    member Dealloc : n:int -> unit
//  end
//val ticketGenerator : TicketGenerator
//type Customer =
//  class
//    interface IDisposable
//    new : unit -> Customer
//    override Finalize : unit -> unit
//    member Ticket : int
//  end

let bill = new Customer()
//val bill : Customer

bill.Ticket
//val it : int = 1

begin
    use joe = new Customer()
    printfn "joe.Ticket = %d" joe.Ticket
end
//joe.Ticket = 2
//returning ticket 2
//val it : unit = ()

begin
    use jane = new Customer()
    printfn "jane.Ticket = %d" jane.Ticket
end
//jane.Ticket = 2
//returning ticket 2
//val it : unit = ()

module NumberTheoryExtensions =
    let factorize i =
        let lim = int (sqrt (float i))
        let rec check j =
           if j > lim  then None
           elif (i %  j) = 0 then Some (i / j, j)
           else check (j + 1)
        check 2

    type System.Int32 with
        member i.IsPrime = (factorize i).IsNone
        member i.TryFactorize() = factorize i

//module NumberTheoryExtensions = begin
//  val factorize : i:int -> (int * int) option
//  type Int32 with
//    member IsPrime : bool
//  type Int32 with
//    member TryFactorize : unit -> (int * int) option
//end

open NumberTheoryExtensions

(2 + 1).IsPrime
//val it : bool = true

(6093704 + 11).TryFactorize()
//val it : (int * int) option = Some (1218743, 5)

open System.Runtime.CompilerServices

module NumberTheoryExtensionsCSharpStyle =
    let factorize i =
        let lim = int (sqrt (float i))
        let rec check j =
           if j > lim  then None
           elif (i %  j) = 0 then Some (i / j, j)
           else check (j + 1)
        check 2

    [<Extension>]
    type Int32Extensions() = 
        [<Extension>]
        static member IsPrime2(i:int) = (factorize i).IsNone

        [<Extension>]
        static member TryFactorize2(i:int) = factorize i

    [<Extension>]
    type ResizeArrayExtensions() = 
        [<Extension>]
        static member Product(values:ResizeArray<int>) = 
            let mutable total = 1
            for v in values do 
                total <- total * v
            total

        [<Extension>]
        static member inline GenericProduct(values:ResizeArray<'T>) = 
            let mutable total = LanguagePrimitives.GenericOne<'T>
            for v in values do 
                total <- total * v
            total

//module NumberTheoryExtensionsCSharpStyle = begin
//  val factorize : i:int -> (int * int) option
//  type Int32Extensions =
//    class
//      new : unit -> Int32Extensions
//      static member IsPrime2 : i:int -> bool
//      static member TryFactorize2 : i:int -> (int * int) option
//    end
//  type ResizeArrayExtensions =
//    class
//      new : unit -> ResizeArrayExtensions
//      static member
//        GenericProduct : values:ResizeArray< ^T> ->  ^T
//                           when  ^T : (static member get_One : ->  ^T) and
//                                 ^T : (static member ( * ) :  ^T *  ^T ->  ^T)
//      static member Product : values:ResizeArray<int> -> int
//    end
//end

open NumberTheoryExtensionsCSharpStyle

(2 + 1).IsPrime2()
//val it : bool = true

(6093704 + 11).TryFactorize2()
//val it : (int * int) option = Some (1218743, 5)

open System
open System.Collections.Generic

let arr = ResizeArray([1 .. 10])
//val arr : List<int>

let arr2 = ResizeArray([1L .. 10L])
//val arr2 : List<int64>

arr.Product()
//val it : int = 3628800

arr.GenericProduct()
//val it : int = 3628800

arr2.GenericProduct()
//val it : int64 = 3628800L

module List =
    let rec pairwise l =
        match l with
        | [] | [_] -> []
        | h1 :: (h2 :: _ as t) -> (h1, h2) :: pairwise t

//module List = begin
//  val pairwise : l:'a list -> ('a * 'a) list
//end

List.pairwise [1; 2; 3; 4]
//val it : (int * int) list = [(1,2); (2,3); (3,4)]

type Vector2D(dx : float, dy : float) =
    class
        let len = sqrt(dx * dx + dy * dy)
        member v.DX = dx
        member v.DY = dy
        member v.Length = len
    end
//type Vector2D =
//  class
//    new : dx:float * dy:float -> Vector2D
//    member DX : float
//    member DY : float
//    member Length : float
//  end

[<Class>]
type Vector2D(dx : float, dy : float) =
    let len = sqrt(dx * dx + dy * dy)
    member v.DX = dx
    member v.DY = dy
    member v.Length = len
//type Vector2D =
//  class
//    new : dx:float * dy:float -> Vector2D
//    member DX : float
//    member DY : float
//    member Length : float
//  end

open System.Drawing

type IShape =
    interface
        abstract Contains : Point -> bool
        abstract BoundingBox : Rectangle
    end
//type IShape =
//  interface
//    abstract member Contains : Point -> bool
//    abstract member BoundingBox : Rectangle
//  end

[<Interface>]
type IShape =
    abstract Contains : Point -> bool
    abstract BoundingBox : Rectangle
//type IShape =
//  interface
//    abstract member Contains : Point -> bool
//    abstract member BoundingBox : Rectangle
//  end

[<Struct>]
type Vector2DStruct(dx : float, dy : float) =
    member v.DX = dx
    member v.DY = dy
    member v.Length = sqrt (dx * dx + dy * dy)
//type Vector2DStruct =
//  struct
//    new : dx:float * dy:float -> Vector2DStruct
//    member DX : float
//    member DY : float
//    member Length : float
//  end

[<Struct>]
type Vector2DStructUsingExplicitVals = 
    val dx : float
    val dy : float
    member v.DX = v.dx
    member v.DY = v.dy
    member v.Length = sqrt (v.dx * v.dx + v.dy * v.dy)
//type Vector2DStructUsingExplicitVals =
//  struct
//    val dx: float
//    val dy: float
//    member DX : float
//    member DY : float
//    member Length : float
//  end

type ControlEventHandler = delegate of int -> bool
//type ControlEventHandler =
//  delegate of int -> bool

open System.Runtime.InteropServices

let ctrlSignal = ref false
//val ctrlSignal : bool ref = {contents = false;}

[<DllImport("kernel32.dll")>]
extern void SetConsoleCtrlHandler(ControlEventHandler callback, bool add)
//val SetConsoleCtrlHandler : callback:ControlEventHandler * add:bool -> unit

let ctrlEventHandler = new ControlEventHandler(fun i ->  ctrlSignal := true; true)
//val ctrlEventHandler : ControlEventHandler

SetConsoleCtrlHandler(ctrlEventHandler, true)

type Vowels =
    | A = 1
    | E = 5
    | I = 9
    | O = 15
    | U = 21

//type Vowels =
//  |  A  =  1
//  |  E  =  5
//  |  I  =  9
//  |  O  =  15
//  |  U  =  21

let parents = [("Adam", None); ("Cain", Some("Adam", "Eve"))]
//val parents : (string * (string * string) option) list =
//  [("Adam", null); ("Cain", Some ("Adam", "Eve"))]

match System.Environment.GetEnvironmentVariable("PATH") with
| null -> printf "the environment variable PATH is not defined\n"
| res -> printf "the environment variable PATH is set to %s\n" res
//the environment variable PATH is set to C:\Windows\system32;...
//val it : unit = ()

let switchOnType (a : obj) =
    match a with
    | null                     -> printf "null!"
    | :? System.Exception as e -> printf "An exception: %s!" e.Message
    | :? System.Int32 as i     -> printf "An integer: %d!" i
    | :? System.DateTime as d  -> printf "A date/time: %O!" d
    | _                        -> printf "Some other kind of object\n"
//val switchOnType : a:obj -> unit