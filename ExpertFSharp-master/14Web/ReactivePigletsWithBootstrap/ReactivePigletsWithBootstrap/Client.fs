namespace ReactivePiglets

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI.Next
open WebSharper.UI.Next.Html
open WebSharper.UI.Next.Client
open WebSharper.Forms
open WebSharper.Forms.Bootstrap

[<JavaScript>]
module LoginForm =
    module C = Controls.Simple

    let Main =
        Form.Return (fun user pass -> (user, pass))
        <*> (Form.Yield ""
            |> Validation.IsNotEmpty "Must enter a valid username")
        <*> (Form.Yield ""
            |> Validation.IsNotEmpty "Must enter a valid password")
        |> Form.WithSubmit
        |> Form.Run (fun (user, pass) ->
            JS.Alert ("Welcome, " + user + "!"))
        |> Form.Render (fun user pass submit ->
            div [
                C.InputWithError "Username" user submit.View
                C.InputPasswordWithError "Password" pass submit.View
                C.Button "Log in" submit.Trigger
                C.ShowErrors submit.View  // Optional to get a list of errors
            ]
        )
        |> Doc.RunById "main"
