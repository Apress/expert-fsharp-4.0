namespace ReactivePiglets

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI.Next
open WebSharper.UI.Next.Html
open WebSharper.UI.Next.Client
open WebSharper.Forms

[<JavaScript>]
module LoginForm =

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
                div [label [text "Username: "]; Doc.Input [] user]
                div [label [text "Password: "]; Doc.PasswordBox [] pass]
                Doc.Button "Log in" [] submit.Trigger
                div [
                    Doc.ShowErrors submit.View (fun errors ->
                        errors
                        |> List.map (fun error -> p [text error.Text])
                        |> Seq.cast
                        |> Doc.Concat)
                ]
            ]
        )
        |> Doc.RunById "main"
