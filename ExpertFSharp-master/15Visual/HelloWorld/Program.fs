open Eto
open Eto.Forms
open Eto.Drawing

let app = new Application()

let form = new Form(Title="Hello world Eto Forms", Topmost=true, Size=Size(640, 480))
let button = new Button(Text="Click me")
button.Click.Add(fun _ -> MessageBox.Show(form, "Hello world!", "Hey!") |> ignore)
form.Content <- button
form.Show()
app.Run(form)
