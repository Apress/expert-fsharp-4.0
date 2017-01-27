namespace TransformableLightWeightControls

open Eto
open Eto.Forms
open Eto.Drawing

type CoordinateSystem =
  | World
  | View

type TransformableViewControl() as this =
    inherit Drawable()

    let w2v = Matrix.Create()
    let v2w = Matrix.Create()
    let viewpaintEvt = new Event<PaintEventArgs>()
    let worldpaintEvt = new Event<PaintEventArgs>()

    do this.CanFocus <- true

    member this.Translate (v:CoordinateSystem) (p:PointF) =
      let am, bm = match v with World -> w2v, v2w | View -> v2w, w2v
      am.Translate(p)
      bm.Append(Matrix.FromTranslation(-p.X, -p.Y))

    member this.Rotate (v:CoordinateSystem) (a:single) =
      let am, bm = match v with World -> w2v, v2w | View -> v2w, w2v
      am.Rotate(a)
      bm.Append(Matrix.FromRotation(-a))

    member this.Scale (v:CoordinateSystem) (s:SizeF) =
      let am, bm = match v with World -> w2v, v2w | View -> v2w, w2v
      am.Scale(s)
      bm.Append(Matrix.FromScale(1.f/s.Width, 1.f/s.Height))

    member this.RotateAt (v:CoordinateSystem) (a:single, p:PointF) =
      let am, bm = match v with World -> w2v, v2w | View -> v2w, w2v
      am.RotateAt(a, p)
      bm.Append(Matrix.FromRotationAt(-a, p))

    member this.ScaleAt (v:CoordinateSystem) (s:SizeF, p:PointF) =
      let am, bm = match v with World -> w2v, v2w | View -> v2w, w2v
      am.ScaleAt(s, p)
      bm.Append(Matrix.FromScaleAt(1.f/s.Width, 1.f/s.Height, p.X, p.Y))

    member this.View2WorldMatrix = v2w
    member this.World2ViewMatrix = w2v

    member this.WorldPaint = worldpaintEvt.Publish
    member this.ViewPaint = viewpaintEvt.Publish

    abstract OnViewPaint : e:PaintEventArgs -> unit
    default this.OnViewPaint _ = ()
    abstract OnWorldPaint : e:PaintEventArgs -> unit
    default this.OnWorldPaint _ = ()

    override this.OnPaint e =
      let g = e.Graphics
      let wr = e.ClipRectangle |> v2w.TransformRectangle

      g.SaveTransform()
      g.MultiplyTransform(w2v)
      let wpe = PaintEventArgs(g, wr)
      this.OnWorldPaint(wpe)
      worldpaintEvt.Trigger(wpe)
      g.RestoreTransform()

      this.OnViewPaint(e)
      viewpaintEvt.Trigger(e)
      base.OnPaint e

    override this.OnKeyDown e =
      let translateV = this.Translate View
      let rotateAtV = this.RotateAt View
      let scaleAtV = this.ScaleAt View
      let mid = PointF(single(this.ClientSize.Width) / 2.f, single(this.ClientSize.Height) / 2.f)
      match e.Key with
      | Keys.W -> translateV(PointF(0.f, -10.f)); this.Invalidate()
      | Keys.A -> translateV(PointF(-10.f, 0.f)); this.Invalidate()
      | Keys.S -> translateV(PointF(0.f, 10.f)); this.Invalidate()
      | Keys.D -> translateV(PointF(10.f, 0.f)); this.Invalidate()
      | Keys.Q -> rotateAtV(10.f, mid); this.Invalidate()
      | Keys.E -> rotateAtV(-10.f, mid); this.Invalidate()
      | Keys.Z -> scaleAtV(SizeF(1.1f, 1.1f), mid); this.Invalidate()
      | Keys.X -> scaleAtV(SizeF(1.f/1.1f, 1.f/1.1f), mid); this.Invalidate()
      | _ -> ()
