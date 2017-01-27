module Symbolic.Expressions.UI

open Symbolic.Expressions
open Symbolic.Expressions.Visual
open System.Windows.Forms
open System.Drawing

let CreateScrollableChildWindow parent =
    let scroll = new ScrollableControl(Dock = DockStyle.Fill, AutoScroll = true)
    let form2 = new Form(MdiParent=parent, BackColor=Color.White)
    form2.Controls.Add scroll
    form2, scroll

let NewExpression parent s es =
    let form, scroll = CreateScrollableChildWindow parent
    let AddLabel (top, maxw) (parent : Control) s =
        let l = new Label(Text = s, AutoSize = true, Top = top)
        parent.Controls.Add l
        (top + l.Height), max maxw l.Width
    let AddPic (top, maxw) (parent : Control) (e : Expr) =
        let e' = VisualExpr.OfExpr RenderOptions.Default e
        let bmp = e'.Render
        let pic = new PictureBox(Image = bmp, Height = bmp.Height,
                                 Width = bmp.Width, Top = top)
        parent.Controls.Add pic
        (top + bmp.Height), max maxw bmp.Width
    let height, width = List.fold (fun top (lab, e) ->
        AddPic (AddLabel top scroll lab) scroll e) (0, 0) es
    form.Text <- s
    form.Height <- min 640 (height + 40)
    form.Width <- min 480 (width + 40)
    form.Show()

let UpdatePreview (scroll : Control) e =
    let e' = VisualExpr.OfExpr RenderOptions.Default e
    let bmp = e'.Render
    let pic = new PictureBox(Image = bmp, Height = bmp.Height, Width = bmp.Width)
    scroll.Controls.Clear()
    scroll.Controls.Add pic

let NewExpressionError form s =
    let cform, scroll = CreateScrollableChildWindow form
    let label = new Label(Text = s, Font = new Font("Courier New", 10.f), AutoSize = true)
    scroll.Controls.Add label
    cform.Show()

exception SyntaxError

let Parse s =
    let lex = Lexing.LexBuffer<char>.FromString s
    try ExprParser.expr ExprLexer.main lex
    with _ -> raise SyntaxError

let NewStringExpression form s =
    try
        let e1 = Parse s
        let e2 = Utils.simplify e1
        let e3 = Utils.diff "x" e2
        let e4 = Utils.simplify e3
        NewExpression form s ["Original:", e1; "Simplified:", e2;
                              "Derivative:", e3; "Simplified:", e4]
    with
      | SyntaxError ->
          let msg = Printf.sprintf "Syntax error in:\n%s" s
          NewExpressionError form msg
      | Failure msg ->
          NewExpressionError form msg

let ConstructMainForm ()  = 
    let form  =  new Form(Text = "Symbolic Differentiation Example",
                          IsMdiContainer = true,
                          Visible = true, Height = 600, Width = 700)
    let label  =  new Label(Text = "Enter function = ", Width = 100, Height = 20)
    let tb  =  new TextBox(Width = 150, Left = 100)
    let panel  =  new Panel(Dock = DockStyle.Top, Height = tb.Height+50)
    let preview  =  new Panel(Dock = DockStyle.Bottom, BackColor = Color.White,
                            Height = 50, BorderStyle = BorderStyle.FixedSingle)
    panel.Controls.AddRange([|label; preview; tb|])
    form.Controls.Add(panel)
    tb.KeyUp.Add (fun arg ->
       if arg.KeyCode = Keys.Enter then
           NewStringExpression form tb.Text
           tb.Text <- ""
           tb.Focus() |> ignore
       else
           try
               let e = Parse tb.Text
               UpdatePreview preview e
           with
           | _ -> ())
    form

let form = ConstructMainForm ()
NewStringExpression form "cos(sin(1 / (x^2 + 1)))"
Application.Run(form)