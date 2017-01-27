open Eto
open Eto.Forms
open Eto.Drawing

type AnalogClock1() as x =
    inherit Drawable()
    let timer = new UITimer(Interval=1.)

    do 
        timer.Elapsed.Add(fun _ -> x.Invalidate())
        timer.Start()

    override this.OnPaint e =
        let time = System.DateTime.Now

        let deg2rad a =
          single(System.Math.PI) * (a - 90.f) / 180.f

        let drawQuadrant (g:Graphics) =
          for i = 1 to 12 do
            let a = single(i - 1) * 30.f |> deg2rad
            g.DrawLine(Pens.Black, 50.f + 40.f * cos(a), 50.f + 40.f * sin(a), 50.f + 50.f * cos(a), 50.f + 50.f * sin(a))

        let drawHandle (g:Graphics) (p:Pen) (a:single) (len:single) =
          let ca = deg2rad(a)
          let pi = single(System.Math.PI)
          g.DrawLine(p, 50.f, 50.f, 50.f + 15.f * cos(ca + pi), 50.f + 15.f * sin(ca + pi))
          g.DrawLine(p, 50.f, 50.f, 50.f + len * cos(ca), 50.f + len * sin(ca))

        let g = e.Graphics
        drawQuadrant g
        use p = new Pen(Colors.Black)
        p.Thickness <- 1.f
        drawHandle g p 50.f (single(time.Second) * 6.f)
        p.Thickness <- 2.f
        drawHandle g p 50.f (single(time.Minute) * 6.f)
        drawHandle g p 30.f (single(time.Hour) * 30.f)
        g.FillEllipse(Brushes.Red, 48.f, 48.f, 4.f, 4.f)

type AnalogClock() as x =
    inherit Drawable()
    let timer = new UITimer(Interval=1.)
    let sz = SizeF(100.f, 100.f)
    let r = sz.Width / 2.f

    do 
        timer.Elapsed.Add(fun _ -> x.Invalidate())
        timer.Start()
    override this.OnPaint e =
        let time = System.DateTime.Now

        let drawQuadrant (g:Graphics) =
          g.SaveTransform()
          for i = 1 to 12 do
            g.DrawLine(Pens.Black, 0.9f * r, 0.f, r, 0.f)
            g.RotateTransform(30.f)
          g.RestoreTransform()


        let drawHand (g:Graphics) (p:Pen) (len:single) (a:single) =
          g.SaveTransform()
          g.RotateTransform(a)
          g.DrawLine(p, -0.2f * r, 0.f, len * r, 0.f)
          g.RestoreTransform()

        let g = e.Graphics
        g.TranslateTransform(PointF(sz.Width / 2.f, sz.Height / 2.f))
        g.RotateTransform(-90.f)
        drawQuadrant g
        use p = new Pen(Colors.Black)
        p.Thickness <- 1.f
        drawHand g p 1.f (single(time.Second) * 6.f)
        p.Thickness <- 2.f
        drawHand g p 1.f (single(time.Minute) * 6.f)
        p.Thickness <- 2.f
        drawHand g p 0.7f (single(time.Hour) * 30.f)
        g.FillEllipse(Brushes.Red, -2.f, -2.f, 4.f, 4.f)

let app = new Application()
let form = new Form(Title="The Clock", Topmost=true, Size=Size(640, 480))
let my = new AnalogClock()
form.Content <- my
form.Show()
app.Run(form)