open Eto
open Eto.Forms
open Eto.Drawing

open TransformableLightWeightControls

type Handle() =
  inherit LightWeightControl(CoordinateSystem=World,Size=SizeF(10.f,10.f))

  let mutable off : PointF option = None

  override this.OnMouseDown e =
    off <- Some(e.Location)

  override this.OnMouseMove e =
    match off with
    | Some p ->
      this.Location <- PointF(e.Location.X + this.Location.X - p.X, e.Location.Y + this.Location.Y - p.Y)
    | None -> ()

  override this.OnMouseUp _ =
    off <- None

  override this.OnPaint e =
    let g = e.Graphics
    let p = PointF(-(this.Size.Width / 2.f), -(this.Size.Height / 2.f))
    g.DrawEllipse(Colors.Black, RectangleF(p, this.Size))

  override this.HitTest p =
    let xa, yb = p.X / this.Size.Width, p.Y / this.Size.Height
    xa * xa + yb * yb <= 1.f

type Cross() =
  inherit LightWeightControl(CoordinateSystem=View,Size=SizeF(20.f,20.f))

  override this.OnPaint e =
    let g = e.Graphics
    g.DrawLine(Colors.Black, -10.f, 0.f, 10.f, 0.f)
    g.DrawLine(Colors.Black, 0.f, -10.f, 0.f, 10.f)

type Line() as this =
  inherit LightWeightContainerControl()

  let handles = [| 
    Handle(Parent=Some(this :> LightWeightContainerControl), Location=PointF(0.f, 0.f));
    Handle(Parent=Some(this :> LightWeightContainerControl), Location=PointF(100.f, 100.f))
  |]

  let cross = new Cross()

  do 
    for h in handles do this.AddControl(h)
    this.AddControl(cross)

  override this.OnSizeChanged _ =
    cross.Location <- PointF(single(this.Width / 2), single(this.Height / 2))

  override this.OnWorldPaint e =
    let g = e.Graphics
    let h1, h2 = handles.[0].Location, handles.[1].Location
    g.DrawLine(Colors.Red, h1, h2)
    base.OnWorldPaint(e)


let app = new Application()

let form = new Form(Title="Hello world Eto Forms", Topmost=true, Size=Size(640, 480))
let my = new Line()
form.Content <- my
form.Show()

app.Run(form)
