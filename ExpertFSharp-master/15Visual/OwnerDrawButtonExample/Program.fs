module Program

open Eto
open Eto.Forms
open Eto.Drawing

open ExpertFSharp4.UserControls

let app = new Application()
let form = new Form(Title="Hello world Eto Forms", Topmost=true, Size=Size(640, 480))
let c = new OwnerDrawButton(Text="Hello Button")
let p = new Panel()
p.Content <- c
p.Padding <- Padding(10)
form.Content <- p
c.Click.Add(fun _ -> MessageBox.Show("Clicked!") |> ignore)
form.Show()

app.Run(form)