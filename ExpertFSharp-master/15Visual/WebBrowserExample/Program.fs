open Eto
open Eto.Forms
open Eto.Drawing
open EtoUtils

let app = new Application()

let home = "about:blank"
let browser = new WebView(Url=System.Uri(home))
let btnGo = new Button(Text="Go")
let txtAddr = new TextBox(Text=home)
let lblStatus = new Label(Text="ok",VerticalAlignment=VerticalAlignment.Center)
let progress = new ProgressBar(Indeterminate=true,Visible=false,Width=200)

btnGo.Click.Add(fun _ -> browser.Url <- System.Uri(txtAddr.Text))
txtAddr.KeyUp.Add(fun e -> if e.Key = Keys.Enter then browser.Url <- System.Uri(txtAddr.Text))

browser.DocumentLoading.Add(fun _ ->
  txtAddr.Text <- browser.Url.ToString()
  lblStatus.Text <- "loading..."
  progress.Visible <- true
)

browser.DocumentLoaded.Add(fun _ ->
  lblStatus.Text <- "ok"
  progress.Visible <- false
)

let layout = 
    Tbl [
        Spacing(Size(2, 2))
        Row [ TableEl(Tbl [ 
                            Pad(Padding(2))
                            Row [
                                 El(new Label(Text="Address:",VerticalAlignment=VerticalAlignment.Center))
                                 StretchedEl(txtAddr)
                                 El(btnGo)
                                ] 
                           ]) ];
        StretchedRow [ StretchedEl(browser) ];
        Row [ TableEl(Tbl [ Pad(Padding(2)); Row [ El(lblStatus); El(progress); EmptyElement ]]) ]
        ] |> makeLayout

let form = new Form(Title="Hello world Eto Forms", Topmost=true, Size=Size(640, 480))
form.Content <- layout
form.Show()

[<System.STAThread>]
do app.Run(form)
