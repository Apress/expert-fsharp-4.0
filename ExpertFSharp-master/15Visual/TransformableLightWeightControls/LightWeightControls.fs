namespace TransformableLightWeightControls

open Eto
open Eto.Forms
open Eto.Drawing


type LightWeightControl() =
  let mutable coordinateSystem = World
  let mutable location = PointF()
  let mutable size = SizeF()
  let mutable parent:LightWeightContainerControl option = None

  member this.CoordinateSystem
    with get () = coordinateSystem
    and set (v) = coordinateSystem <- v

  member this.Location
    with get() = location
    and set(v) = location <- v; this.Invalidate()

  member this.Size
    with get() = size
    and set(v) = size <- v; this.OnSizeChanged(new System.EventArgs())

  member this.Bounds = RectangleF(location, size)

  member this.Parent
    with get() = parent
    and set(v) = parent <- v

  member this.Invalidate () =
    match parent with
    | Some v -> v.Invalidate()
    | None -> ()

  member this.Invalidate (r:Rectangle) =
    match parent with
    | Some v ->
      let tr = RectangleF(r)
      tr.Offset(location)
      if coordinateSystem = World then
        v.Invalidate(tr |> v.World2ViewMatrix.TransformRectangle |> Rectangle)
      else
        v.Invalidate(tr |> Rectangle)
    | None -> ()

  member this.Focus () =
    match parent with
    | Some p -> p.SetFocus(this)
    | None -> ()

  member this.Unfocus () =
    match parent with
    | Some p -> p.UnsetFocus()
    | None -> ()

  abstract OnPaint : PaintEventArgs -> unit
  default this.OnPaint _ = ()
  abstract OnMouseDown : MouseEventArgs -> unit
  default this.OnMouseDown _ = ()
  abstract OnMouseUp : MouseEventArgs -> unit
  default this.OnMouseUp _ = ()
  abstract OnMouseMove : MouseEventArgs -> unit
  default this.OnMouseMove _ = ()
  abstract OnMouseEnter : MouseEventArgs -> unit
  default this.OnMouseEnter _ = ()
  abstract OnMouseLeave : MouseEventArgs -> unit
  default this.OnMouseLeave _ = ()
  abstract OnKeyDown : KeyEventArgs -> unit
  abstract OnMouseDoubleClick : MouseEventArgs -> unit
  default this.OnMouseDoubleClick _ = ()
  abstract OnMouseWheel : MouseEventArgs -> unit
  default this.OnMouseWheel _ = ()
  default this.OnKeyDown _ = ()
  abstract OnKeyUp : KeyEventArgs -> unit
  default this.OnKeyUp _ = ()
  abstract OnSizeChanged : System.EventArgs -> unit
  default this.OnSizeChanged _ = ()
  abstract HitTest : PointF -> bool
  default this.HitTest p = RectangleF(PointF(), size).Contains(p)

and LightWeightContainerControl() as this =
  inherit TransformableViewControl()

  let children = new ResizeArray<LightWeightControl>()
  let mutable gotFocus : LightWeightControl option = None
  let controlsIn view = children |> Seq.filter (fun v -> v.CoordinateSystem = view)
  let mutable captured : LightWeightControl option = None

  let correlate (loc:PointF) =
    let cv = controlsIn View |> Seq.tryFindBack (fun v -> v.HitTest(PointF(loc.X - v.Location.X, loc.Y - v.Location.Y)))
    match cv with
    | Some c -> Some c
    | None ->
      let l = this.View2WorldMatrix.TransformPoint(loc)
      controlsIn World 
        |> Seq.tryFindBack (fun v ->
              v.HitTest(PointF(l.X - v.Location.X, l.Y - v.Location.Y))
      )

  let cloneMouseEvent (e:MouseEventArgs) (newloc:PointF) =
    new MouseEventArgs(e.Buttons, e.Modifiers, newloc, new System.Nullable<SizeF>(e.Delta), e.Pressure)

  let lightweightCoord (c:LightWeightControl) (loc:PointF) =
    if c.CoordinateSystem = World then
      let l = this.View2WorldMatrix.TransformPoint(loc)
      PointF(l.X - c.Location.X, l.Y - c.Location.Y)
    else
      let l = loc
      PointF(l.X - c.Location.X, l.Y - c.Location.Y)
  
  let handleMouseEvent (e:MouseEventArgs) (f:LightWeightControl->MouseEventArgs->unit) =
    let cv = correlate e.Location
    match cv with
    | Some c -> f c (cloneMouseEvent e (lightweightCoord c e.Location))
    | None -> ()

  let mutable lastControl:LightWeightControl option = None

  member this.AddControl (c:LightWeightControl) = children.Add(c)

  member this.RemoveControl (c:LightWeightControl) = children.Remove(c)

  member this.BringToFront (c:LightWeightControl) = if children.Remove(c) then children.Add(c)

  member this.SendToBack (c:LightWeightControl) = if children.Remove(c) then children.Insert(0, c)

  member this.SetFocus(c:LightWeightControl) = gotFocus <- Some c

  member this.UnsetFocus() = gotFocus <- None

  override this.OnMouseDown e =
    handleMouseEvent e (fun c e -> captured <- Some c; c.OnMouseDown e)
    base.OnMouseDown(e)

  override this.OnMouseUp e =
    match captured with
    | Some c -> 
      c.OnMouseUp (cloneMouseEvent e (lightweightCoord c e.Location))
      captured <- None
    | None -> ()
    handleMouseEvent e (fun c e -> c.OnMouseUp e)
    base.OnMouseUp(e)

  override this.OnMouseMove e =
    let lc = lastControl
    let mutable ce = null
    lastControl <- None
    match captured with
    | Some c -> c.OnMouseMove (cloneMouseEvent e (lightweightCoord c e.Location))
    | None -> ()
    handleMouseEvent e (fun c e -> lastControl <- Some(c); ce <- e; c.OnMouseMove e)
    if lastControl <> lc then
      if lastControl = None then lc.Value.OnMouseLeave(ce)
      else lastControl.Value.OnMouseEnter(ce)
    base.OnMouseMove(e)

  override this.OnMouseDoubleClick e =
    handleMouseEvent e (fun c e -> c.OnMouseDoubleClick e)
    base.OnMouseDoubleClick(e)

  override this.OnMouseWheel e =
    handleMouseEvent e (fun c e -> c.OnMouseWheel e)
    base.OnMouseWheel(e)
  
  override this.OnKeyDown e =
    match gotFocus with
    | Some c -> c.OnKeyDown e
    | None -> ()

  override this.OnKeyUp e =
    match gotFocus with
    | Some c -> c.OnKeyUp e
    | None -> ()

  override this.OnViewPaint e =
    for v in controlsIn View do
      e.Graphics.SaveTransform()
      e.Graphics.TranslateTransform(v.Location)
      v.OnPaint(e)
      e.Graphics.RestoreTransform()

  override this.OnWorldPaint e =
    for v in controlsIn World do
      e.Graphics.SaveTransform()
      e.Graphics.TranslateTransform(v.Location)
      v.OnPaint(e)
      e.Graphics.RestoreTransform()
