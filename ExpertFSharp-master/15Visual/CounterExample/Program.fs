open Eto
open Eto.Forms
open Eto.Drawing

open EtoUtils

let app = new Application()
let form = new Form(Title="Hello world Eto Forms", Topmost=true, Size=Size(640, 480))
let mutable counter = 0
let button = new Button(Text="+1")
let display = new Label(Text=string(counter))
let table = Tbl[ Row[ El(display) ]; Row[ El(button) ]]
let updateCounter c = counter <- c; display.Text <- string(c)
button.Click.Add(fun _ -> updateCounter(counter + 1))
form.Content <- table |> makeLayout
let menu = new MenuBar()
let resetMenu = SubMenu("&File", 
                        [ 
                         MenuItem("Reset").WithAction(fun _ -> updateCounter(0));
                         MenuItem("Exit").WithAction(fun _ -> form.Close())
                        ])
menu.Items.Add(resetMenu |> makeMenu)
form.Menu <- menu
form.Show()
app.Run(form)
