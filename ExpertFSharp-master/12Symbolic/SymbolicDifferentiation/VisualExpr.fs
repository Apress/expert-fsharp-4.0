namespace Symbolic.Expressions.Visual

open Symbolic.Expressions
open System.Drawing
open System.Drawing.Imaging

type RenderOptions =
    {NormalFont : Font; SmallFont : Font; IsSuper : bool; Pen : Pen}

    static member Default =
        {
            NormalFont = new Font("Courier New", 18.0f, FontStyle.Regular)
            SmallFont = new Font("Courier New", 12.0f, FontStyle.Regular)
            IsSuper = false
            Pen = new Pen(Color.Black, 1.0f)
        }

    member self.Brush =
        (new SolidBrush(Color.FromArgb(255, self.Pen.Color)) :> Brush)

type VisualElement =
    | Symbol of string * ExprSize
    | Power of VisualElement * VisualElement * ExprSize
    | Sequence of VisualElement list * ExprSize
    | Fraction of VisualElement * VisualElement * ExprSize
    member self.Size =
        match self with
        | Symbol (_, size) | Power (_, _, size)
        | Sequence (_, size) | Fraction (_, _, size) -> size

    member self.Height = self.Size.height
    member self.Width = self.Size.width
    member self.Midline = self.Size.midline

and ExprSize =
    {
        width : int
        height : int
        midline : int
    }

    member self.CenterOnMidline size x y =
        x + (size.width-self.width) / 2, y + (size.midline - self.midline)

    member self.Frac size opt =
        {
            width = max self.width size.width
            height = self.height + size.height + self.FracSepHeight opt
            midline = self.height + (self.FracSepHeight opt) / 2
        }

    member self.FracSepHeight (opt: RenderOptions) =
        max (int (opt.Pen.Width * 5.0f)) 4

    member self.AddPower (e : VisualElement) =
        {
            width = self.width + e.Width
            height = self.height + e.Height
            midline = self.midline + e.Height
        }

    static member ExpandOne (size : ExprSize) (e : VisualElement) =
        {
            width   = size.width + e.Width
            height  = max size.height e.Height
            midline = max size.midline e.Midline
        }

    member self.Expand (exprs : VisualElement list) =
        List.fold ExprSize.ExpandOne self exprs

    static member Seq (exprs : VisualElement list) =
        List.fold ExprSize.ExpandOne ExprSize.Zero exprs

    static member Zero = {width = 0; height = 0; midline = 0}

type VisualExpr =
    {Expression : VisualElement; RenderOptions: RenderOptions}

    static member OfExpr (opt : RenderOptions) e =
        use bmp = new Bitmap(100, 100, PixelFormat.Format32bppArgb)
        use gra = Graphics.FromImage(bmp)
        let sizeOf (opt : RenderOptions) s =
            use sFormat = new StringFormat(StringFormat.GenericTypographic)
            let font = if opt.IsSuper then opt.SmallFont else opt.NormalFont
            let size = gra.MeasureString(s, font, PointF(0.0f, 0.0f), sFormat)
            let height = int size.Height
            {
                width = int size.Width + 2;
                height = height;
                midline = height / 2;
            }
        let precPow = 70
        let precProd1, precProd2 = 30, 40
        let precAdd1, precAdd2 = 10, 11
        let precStart = 5
        let precNeg1, precNeg2 = 1, 20
        let sym opt s = Symbol (s, sizeOf opt s)

        let applyPrec opt pprec prec exprs (size : ExprSize) =
            if pprec > prec then
                sym opt "(" :: exprs @ [sym opt ")"],
                size.Expand [sym opt "("; sym opt ")"]
            else
                exprs, size

        let mkSequence opt pprec prec exprs =
            let size = ExprSize.Seq exprs
            let exprs, size = applyPrec opt pprec prec exprs size
            Sequence (exprs, size)

        let rec expFunc opt f par =
            let f' = sym opt f
            let exprs' = [sym opt "("; exp opt precStart par; sym opt ")"]
            Sequence (f' :: exprs', f'.Size.Expand exprs')

        and exp (opt : RenderOptions) prec = function
            | Num n -> let s = n.ToString() in Symbol (s, sizeOf opt s)
            | Var v -> Symbol (v, sizeOf opt v)

            | Neg e ->
                let e' = exp opt precNeg1 e
                let exprs, size = applyPrec opt prec precNeg1 [e'] e'.Size
                let exprs' = [sym opt "-"] @ exprs
                mkSequence opt prec precNeg2 exprs'

            | Add exprs ->
                let exprs' =
                    [for i, e in Seq.mapi (fun i x -> (i, x)) exprs do
                        let first = (i = 0)
                        let e' = exp opt (if first then precAdd1 else precAdd2) e
                        if first || e.IsNegative then yield! [e']
                        else yield! [sym opt "+"; e']]
                mkSequence opt prec precAdd1 exprs'

            | Sub (e1, exprs) ->
                let e1' = exp opt prec e1
                let exprs' =
                    [for e in exprs do
                        if e.IsNegative then
                            let e' = exp opt precAdd2 e.Negate
                            yield! [sym opt "+"; e']
                        else
                            let e' = exp opt precAdd2 e
                            yield! [sym opt "-"; e']]
                mkSequence opt prec precAdd1 (e1' :: exprs')

            | Prod (e1, e2) ->
                let e1' = exp opt precProd1 e1
                let e2' = exp opt precProd2 e2
                let exprs' =
                    if Expr.StarNeeded e1 e2 then [e1'; sym opt "*"; e2']
                    else [e1'; e2']
                mkSequence opt prec precProd1 exprs'

            | Pow (e1, e2) ->
                let e1' = exp opt precPow e1
                let e2' = exp {opt with IsSuper = true} precPow (Num e2)
                Power (e1', e2', e1'.Size.AddPower e2')

            | Sin e -> expFunc opt "sin" e
            | Cos e -> expFunc opt "cos" e

            | Exp expo ->
                let e' = sym opt "e"
                let expo' = exp {opt with IsSuper = true} precPow expo
                Power (e', expo', e'.Size.AddPower expo')

            | Frac (e1, e2) ->
                 let e1' = exp opt precStart e1
                 let e2' = exp opt precStart e2
                 Fraction (e1', e2', e1'.Size.Frac e2'.Size opt)

        let exp = exp opt precStart e
        {Expression = exp; RenderOptions = opt}

//| Prod (e1, e2) ->
//     let e1' = exp opt precProd1 e1
//     let e2' = exp opt precProd2 e2
//     let exprs' =
//         if Expr.StarNeeded e1 e2 then [e1'; sym opt "*"; e2']
//         else [e1'; e2']
//     mkSequence opt prec precProd1 exprs'
//
//type VisualExpr =
//    ...
    member self.Render =
        let pt x y = PointF(float32 x, float32 y)
        let rec draw (gra : Graphics) opt x y psize = function
            | Symbol (s, size) ->
                let font = if opt.IsSuper then opt.SmallFont else opt.NormalFont
                let x', y' = size.CenterOnMidline psize x y
                gra.DrawString(s, font, opt.Brush, pt x' y')

            | Power (e1, e2, size) ->
                let x', y' = size.CenterOnMidline psize x y
                draw gra opt x' (y' + e2.Height) e1.Size e1
                draw gra {opt with IsSuper = true} (x' + e1.Width) y' e2.Size e2

            | Sequence (exps, size) ->
                let x', y' = size.CenterOnMidline psize x y
                List.fold (fun (x, y) (e : VisualElement) ->
                     let psize' =
                        {
                            width = e.Width
                            height = psize.height;
                            midline=size.midline
                        }
                     draw gra opt x y psize' e
                     x + e.Width, y) (x', y') exps |> ignore

            | Fraction (e1, e2, size) as e ->
                let psize1 = {psize with height = e1.Height; midline = e1.Midline}
                let psize2 = {psize with height = e2.Height; midline = e2.Midline}
                draw gra opt x y psize1 e1
                gra.DrawLine(self.RenderOptions.Pen, x, y + size.midline,
                             x + psize.width, y+size.midline);
                draw gra opt x (y + e1.Height+size.FracSepHeight opt) psize2 e2
        let bmp = new Bitmap(self.Expression.Width, self.Expression.Height,
                             PixelFormat.Format32bppArgb)
        let gra = Graphics.FromImage(bmp)
        gra.FillRectangle(new SolidBrush(Color.White), 0, 0,
                          self.Expression.Width + 1, self.Expression.Height + 1)
        draw gra self.RenderOptions 0 0 self.Expression.Size self.Expression
        bmp

