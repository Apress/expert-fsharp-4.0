module Mandelbrot.Main

open Eto
open Eto.Forms
open Eto.Drawing
open System.Threading
open System.Numerics
open System.Xml

open EtoUtils

let app = new Application()

let form = new Form(Title="Mandelbrot", Size=Size(800, 600))
let draw = new Drawable()

let timer = new UITimer(Interval = 0.1)
timer.Elapsed.Add(fun _ -> draw.Invalidate())


let mutable worker = Thread.CurrentThread
let mutable bitmap = new Bitmap(form.Width, form.Height, PixelFormat.Format32bppRgba)
let mutable bmpw = form.Width
let mutable bmph = form.Height
let mutable rect = RectangleF.Empty
let mutable tl = (-3.0, 2.0)
let mutable br = (2.0, -2.0)
let clipboard = new Clipboard()

let mutable menuIterations = 150

let iterations (tlx, tly) (brx, bry) =
    menuIterations

let RGBtoHSV (r, g, b) =
//    let cc = Color(single r, single g, single b)
//    let ret = ColorHSL(cc)
//    (float ret.H, float ret.S, float ret.L)
    let (m : float) = min r (min g b)
    let (M : float) = max r (max g b)
    let delta = M - m
    let posh (h : float) = if h < 0.0 then h + 360.0 else h
    let deltaf (f : float) (s : float) = (f - s) / delta
    if M = 0.0 then (-1.0, 0.0, M) else
        let s = (M - m) / M
        if r = M then (posh(60.0 * (deltaf g b)), s, M)
        elif g = M then (posh(60.0 * (2.0 + (deltaf b r))), s, M)
        else (posh(60.0 * (4.0 + (deltaf r g))), s, M)

let HSVtoRGB (h, s, v) =
//    let cc = ColorHSL(single(h), single(s), single(v))
//    let ret = cc.ToColor()
//    (float ret.R, float ret.G, float ret.B)
    if s = 0.0 then (v, v, v) else
    let hs = h / 60.0
    let i = floor (hs)
    let f = hs - i
    let p = v * ( 1.0 - s )
    let q = v * ( 1.0 - s * f )
    let t = v * ( 1.0 - s * ( 1.0 - f ))
    match int i with
      | 0 -> (v, t, p)
      | 1 -> (q, v, p)
      | 2 -> (p, v, t)
      | 3 -> (p, q, v)
      | 4 -> (t, p, v)
      | _ -> (v, p, q)

let makeColor (r, g, b) =
    let f x = int32(x * 255.0)
    Color.FromArgb(f(r), f(g), f(b))

let defaultColor i = makeColor(HSVtoRGB(360.0 * (float i / 250.0), 1.0, 1.0))

let coloring =
    [|
        defaultColor;
        (fun i -> Color.FromArgb(i, i, i));
        (fun i -> Color.FromArgb(i, 0, 0));
        (fun i -> Color.FromArgb(0, i, 0));
        (fun i -> Color.FromArgb(0, 0, i));
        (fun i -> Color.FromArgb(i, i, 0));
        (fun i -> Color.FromArgb(i, 250 - i, 0));
        (fun i -> Color.FromArgb(250 - i, i, i));
        (fun i -> if i % 2 = 0 then Colors.White else Colors.Black);
        (fun i -> Color.FromArgb(250 - i, 250 - i, 250 - i))
    |]

let createPalette c =
    Array.init 253 (function
        | 250 -> Colors.Black
        | 251 -> Colors.White
        | 252 -> Colors.LightGrey
        | i ->   c i)

let mutable palette = createPalette coloring.[0]

let pickColor maxit it =
    palette.[int(250.0 * float it / float maxit)]

let sqrMod (x : Complex) = x.Real * x.Real + x.Imaginary * x.Imaginary

let rec mandel maxit (z : Complex) (c : Complex) count =
    if (sqrMod(z) < 4.0) &&  (count < maxit) then
        mandel maxit ((z * z) + c) c (count + 1)
    else count

// t=top, l=left, r=right, b=bottom, bm=bitmap, p=pixel, w=width, h=height
let run filler (form : #Form) (bitmap : Bitmap) (tlx, tly) (brx, bry) =
    let dx = (brx - tlx) / float bmpw
    let dy = (tly - bry) / float bmph
    let maxit = iterations (tlx, tly) (brx, bry)
    let x = 0
    let y = 0
    let transform x y = new Complex(tlx + (float x)* dx, tly - (float y) * dy)
    // Cross thread...
    app.AsyncInvoke(fun _ ->
      form.Title <- sprintf "Mandelbrot set [it: %d] (%f, %f) -> (%f, %f)" maxit tlx tly brx bry
    )
    filler maxit transform
    timer.Stop()

let linearFill (bw : int) (bh : int) maxit map =
    for y = 0 to bh - 1 do
        for x = 0 to bw - 1 do
            let c = mandel maxit Complex.Zero (map x y) 0
            lock bitmap (fun () -> bitmap.SetPixel(x, y, pickColor maxit c))

let blockFill (bw : int) (bh : int) maxit map =
    let rec fillBlock first sz x y =
        if x < bw then
            let c = mandel maxit Complex.Zero (map x y) 0
            lock bitmap (fun () ->
                use g = new Graphics(bitmap)
                use b = new SolidBrush(pickColor maxit c)
                g.FillRectangle(b, single x, single y, single sz, single sz)
                )
            fillBlock first sz 
                      (if first || ((y / sz) % 2 = 1) then x + sz 
                                                      else x + 2 * sz) y
        elif y < bh then
            fillBlock first sz 
                      (if first || ((y / sz) % 2 = 0) then 0 else sz) (y + sz)
        elif sz > 1 then
            fillBlock false (sz / 2) (sz / 2) 0

    fillBlock true 64 0 0

let mutable fillFun = blockFill

let clearOffScreen (b : Bitmap) =
    use g = new Graphics(b)
    g.Clear(Brushes.White)

let paint (g : Graphics) =
    lock bitmap (fun () -> g.DrawImage(bitmap, 0.f, 0.f))
    g.DrawRectangle(Pens.Black, rect)
    use bg = new SolidBrush(Color.FromArgb(0x80FFFFFF))
    g.FillRectangle(bg, rect)

let stopWorker () =
    if worker <> Thread.CurrentThread then
        worker.Abort()
        worker <- Thread.CurrentThread

let drawMandel () =
    let bf = fillFun bmpw bmph
    stopWorker()
    timer.Start()
    worker <- new Thread(fun () -> run bf form bitmap tl br)
    worker.IsBackground <- true
    worker.Priority <- ThreadPriority.Lowest
    worker.Start()

let mutable startsel = PointF.Empty

let setCoord (tlx : float, tly : float) (brx : float, bry : float)  =
    let dx = (brx - tlx) / float bmpw
    let dy = (tly - bry) / float bmph
    let mapx x = tlx + float x * dx
    let mapy y = tly - float y * dy
    tl <- (mapx rect.Left, mapy rect.Top)
    br <- (mapx rect.Right, mapy rect.Bottom)

let ensureAspectRatio (tlx : float, tly : float) (brx : float, bry : float) =
    let ratio = (float bmpw / float bmph)
    let w, h = abs(brx - tlx), abs(tly - bry)
    if ratio * h > w then
        br <- (tlx + h * ratio, bry)
    else
        br <- (brx, tly - w / ratio)

let updateView () =
    if rect <> RectangleF.Empty then setCoord tl br
    ensureAspectRatio tl br
    rect <- RectangleF.Empty
    stopWorker()
    clearOffScreen bitmap
    drawMandel()

let click (arg : MouseEventArgs) =
    if rect.Contains(arg.Location) then
        updateView()
    else
        draw.Invalidate()
        rect <- RectangleF.Empty
        startsel <- arg.Location

let mouseMove (arg : MouseEventArgs) =
    if arg.Buttons = MouseButtons.Primary then
        let tlx = min startsel.X arg.Location.X
        let tly = min startsel.Y arg.Location.Y
        let brx = max startsel.X arg.Location.X
        let bry = max startsel.Y arg.Location.Y
        rect <- new RectangleF(tlx, tly, brx - tlx, bry - tly)
        draw.Invalidate()

let resize () =
    if bmpw <> form.ClientSize.Width ||
       bmph <> form.ClientSize.Height then
         stopWorker()
         rect <- new RectangleF(SizeF(single form.ClientSize.Width, single form.ClientSize.Height))
         bitmap <- new Bitmap(form.ClientSize.Width, form.ClientSize.Height, PixelFormat.Format32bppRgba)
         bmpw <- form.ClientSize.Width
         bmph <- form.ClientSize.Height

         updateView()

let zoom amount (tlx, tly) (brx, bry) =
    let w, h = abs(brx - tlx), abs(tly - bry)
    let nw, nh = amount * w, amount * h
    tl <- (tlx + (w - nw) / 2., tly - (h - nh) / 2.)
    br <- (brx - (w - nw) / 2., bry + (h - nh) / 2.)
    rect <- RectangleF.Empty
    updateView()

let setupMenu () =
  let m = new MenuBar()
  let setFillMode filler =
    fillFun <- filler
    drawMandel()

  let itchg = fun (m:MenuItem) ->
    menuIterations <- System.Int32.Parse(m.Text)
    stopWorker()
    drawMandel()

  let setPalette idx =
    palette <- createPalette coloring.[idx]
    stopWorker()
    drawMandel()

  let f = SubMenu ("&File", [ MenuItem "E&xit" |> action (fun _ -> app.Quit()) ])

  let c = 
    SubMenu 
      ("&Settings", 
       [
         SubMenu 
           ("Color Scheme",
            [
              RadioMenuItem("colscheme", "HSL Color") |> check |> action (fun _ -> setPalette 0)
              RadioMenuItem("colscheme", "Gray") |> action (fun _ ->  setPalette 1)
              RadioMenuItem("colscheme", "Red")  |> action (fun _ ->  setPalette 2)
              RadioMenuItem("colscheme", "Green") |> action(fun _ ->  setPalette 3)
            ])
         SubMenu 
           ("Iterations",
            [
              RadioMenuItem ("iter", "150")  |> check |> action itchg
              RadioMenuItem ("iter", "250")  |> action itchg
              RadioMenuItem ("iter", "500")  |> action itchg
              RadioMenuItem ("iter", "1000") |> action itchg
            ])
         SubMenu
           ("Fill mode",
            [
              RadioMenuItem ("fillmode", "Line")  |> action (fun _ -> setFillMode linearFill)
              RadioMenuItem ("fillmode", "Block") |> check |> action (fun _ -> setFillMode blockFill)
            ])
       ])
  
  let copyf () =
      let maxit = (iterations tl br)
      let tlx, tly = tl
      let brx, bry = br
      clipboard.Text <- sprintf "<Mandel iter=\"%d\"><topleft><re>%.14e</re><im>%.14e</im></topleft><bottomright><re>%.14e</re><im>%.14e</im></bottomright></Mandel>" maxit tlx tly brx bry

  let pastef () =
      if clipboard.Text.StartsWith("<Mandel") then
        let doc = new XmlDocument()
        try
          doc.LoadXml(clipboard.Text)
          menuIterations <- 
              int (doc.SelectSingleNode("/Mandel").Attributes.["iter"].Value)
          tl <- (float (doc.SelectSingleNode("//topleft/re").InnerText), 
                 float (doc.SelectSingleNode("//topleft/im").InnerText))
          br <- (float (doc.SelectSingleNode("//bottomright/re").InnerText),
                 float (doc.SelectSingleNode("//bottomright/im").InnerText))
          rect <- RectangleF.Empty
          updateView()
        with _ -> ()

  let e =
    SubMenu
      ("&Edit",
       [
         MenuItem "&Copy"  |> action (fun _ -> copyf()) |> shortcut (Keys.Control ||| Keys.C)
         MenuItem "&Paste" |> action (fun _ -> pastef()) |> shortcut (Keys.Control ||| Keys.V)
         MenuItem "Copy &bitmap"  |> action (fun _ -> lock bitmap (fun _ -> clipboard.Image <- bitmap)) |> shortcut (Keys.Control ||| Keys.Shift ||| Keys.C)
         MenuItem "Zoom &In"  |> action (fun _ -> zoom 0.9 tl br) |> shortcut (Keys.Control ||| Keys.T)
         MenuItem "Zoom &Out"  |> action (fun _ -> zoom 1.25 tl br) |> shortcut (Keys.Control ||| Keys.W)
       ])
  [f; c; e ] |> List.iter (fun i -> m.Items.Add(i |> mkmenu))
  m

clearOffScreen bitmap
form.Menu <- setupMenu()
draw.Paint.Add(fun arg ->  paint arg.Graphics)
draw.MouseDown.Add(click)
draw.MouseMove.Add(mouseMove)
form.Content <- draw
form.SizeChanged.Add(fun _ -> resize())

app.RunIteration()

drawMandel()

[<System.STAThread>]
do app.Run(form)


