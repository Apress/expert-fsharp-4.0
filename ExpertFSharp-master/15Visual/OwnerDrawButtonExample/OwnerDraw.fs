namespace ExpertFSharp4.UserControls

open System
open System.ComponentModel
open Eto
open Eto.Forms
open Eto.Drawing

type OwnerDrawButton() =
  inherit Drawable()

  let mutable text = ""
  let mutable pressed = false
  let clickevt = new Event<System.EventArgs>()

  override x.OnPaint (e : PaintEventArgs) =
    let changeLuminance factor c =
      let mutable hc = ColorHSL(c)
      hc.L <- hc.L * factor
      hc.ToColor()
      
    let lighter = changeLuminance 1.1f

    let darker = changeLuminance (1.f / 1.1f)

    let g = e.Graphics
    let bc = Colors.LightGrey
    use pll = new Pen(bc |> lighter |> lighter)
    use pl = new Pen(bc |> lighter)
    use pd = new Pen(bc |> darker)
    use pdd = new Pen(bc |> darker |> darker)
    use bfg = new SolidBrush(Colors.Black)
    use bbg = new SolidBrush(bc)
    let f = new Font(SystemFont.Default)
    let szf = g.MeasureString(f, text)
    let off = if pressed then 1.0f else 0.0f
    let spt = PointF((float32(x.Width) - szf.Width) / 2.0f + off,
                     (float32(x.Height) - szf.Height) / 2.0f + off)
    let ptt, pt, pb, pbb = 
                           if pressed then pdd, pd, pll, pl 
                           else pl, pll, pd, pdd

    g.Clear(bbg)
    g.DrawLine(ptt, 0.f, 0.f, single(x.Width - 1), 0.f)
    g.DrawLine(ptt, 0.f, 0.f, 0.f, single(x.Height - 1))
    g.DrawLine(pt, 1.f, 1.f, single(x.Width - 2), 1.f)
    g.DrawLine(pt, 1.f, 1.f, 1.f, single(x.Height - 2))
    g.DrawLine(pbb, 0.f, single(x.Height - 1), single(x.Width - 1), single(x.Height - 1))
    g.DrawLine(pbb, single(x.Width - 1), 0.f, single(x.Width - 1), single(x.Height - 1))
    g.DrawLine(pb, 1.f, single(x.Height - 2), single(x.Width - 2), single(x.Height - 2))
    g.DrawLine(pb, single(x.Width - 2), 1.f, x.Width - 2 |> single, x.Height - 2 |> single)
    g.DrawText(f, bfg, spt, text)

  override x.OnMouseUp (e : MouseEventArgs) =
    pressed <- false
    clickevt.Trigger(new System.EventArgs())
    x.Invalidate()

  override x.OnMouseDown (e : MouseEventArgs) =
    pressed <- true
    x.Invalidate()

  [<Category("Behavior")>]
  [<Browsable(true)>]
  member x.Text
    with get() = text
    and  set(t : string) = text <- t; x.Invalidate()

  member x.Click = clickevt.Publish
