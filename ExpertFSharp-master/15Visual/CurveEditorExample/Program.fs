open Eto
open Eto.Forms
open Eto.Drawing

open EtoUtils

let app = new Application()

let form = new Form(Title="Curves", Topmost=true, Size=Size(640, 480))
let cpt = [|PointF(20.f, 60.f); PointF(40.f, 50.f); PointF(130.f, 60.f); PointF(200.f, 200.f)|]
let mutable movingPoint = -1
let draw = new Drawable()

let menuBezier = new CheckMenuItem(Text="Show &Beziér",Checked=true)
let menuCanonical = new CheckMenuItem(Text="Show &Canonical spline")
let menuControlPoints = new CheckMenuItem(Text="Show control &points") 

let tension = new Slider(Orientation=Orientation.Horizontal,
                         MinValue=0,MaxValue=10,TickFrequency=1,Visible=false)

let drawPoint (g : Graphics) (p : PointF) =
    g.DrawEllipse(Pens.Red, p.X - 2.f, p.Y - 2.f, 4.f, 4.f) 

let paint (g : Graphics) =
    if (menuBezier.Checked) then
        g.DrawLine(Pens.Red, cpt.[0], cpt.[1])
        g.DrawLine(Pens.Red, cpt.[2], cpt.[3])
        let path = new GraphicsPath()
        path.AddBezier(cpt.[0], cpt.[1], cpt.[2], cpt.[3])
        g.DrawPath(Pens.Black, path)
    if (menuCanonical.Checked) then
        let path = new GraphicsPath()
        path.AddCurve(cpt, single tension.Value)
        g.DrawPath(Pens.Blue, path)
    if (menuControlPoints.Checked) then
        for i = 0 to cpt.Length - 1 do
            drawPoint g cpt.[i] 

let isClose (p : PointF) (l : PointF) =
    let dx = p.X - l.X
    let dy = p.Y - l.Y
    (dx * dx + dy * dy) < 6.f 

let mouseDown (p : PointF) =
    try
      let idx = cpt |> Array.findIndex (isClose p)
      movingPoint <- idx
    with _ -> () 

let mouseMove (p : PointF) =
    if (movingPoint <> -1) then
        cpt.[movingPoint] <- p
        draw.Invalidate() 

let updatemenu _ = draw.Invalidate()

let menu = new MenuBar()
let menuFile = SubMenu ("&File", [ MenuItem("E&xit").WithAction(fun _ -> app.Quit()) ]) |> makeMenu
let menuSettings = 
  SubMenu
   ("&Settings",
    [
      Item(menuBezier).WithAction(updatemenu)
      Item(menuCanonical).WithAction(fun _ -> draw.Invalidate(); tension.Visible <- menuCanonical.Checked)
      Item(menuControlPoints).WithAction(updatemenu)
    ]) |> makeMenu

[ menuFile; menuSettings ] |> List.iter(fun m -> menu.Items.Add(m))

form.Menu <- menu

tension.ValueChanged.Add(fun _ -> draw.Invalidate())
draw.Paint.Add(fun e -> paint e.Graphics)
draw.MouseDown.Add(fun e -> mouseDown(e.Location))
draw.MouseMove.Add(fun e -> mouseMove(e.Location))
draw.MouseUp.Add(fun e -> movingPoint <- -1)

let l = Tbl [ StretchedRow[ StretchedEl(draw); El(tension) ] ] |> makeLayout

form.Content <- l
form.Show()
app.Run(form)
