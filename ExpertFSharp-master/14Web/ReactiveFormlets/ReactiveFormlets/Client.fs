namespace ReactiveFormlets

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI.Next
open WebSharper.UI.Next.Html
open WebSharper.UI.Next.Client
open WebSharper.UI.Next.Formlets

[<JavaScript>]
module LoginForm =

    let Main =
        let username = Var.Create ""
        let password = Var.Create ""
        Formlet.Return (fun user pass -> (user, pass))
        <*> (Controls.InputVar username
            |> Formlet.WithLabel (text "Username: "))
        <*> (Controls.InputVar password
            |> Formlet.WithLabel (text "Password: "))
        |> Formlet.WithSubmit "Log in"
        |> Formlet.WithFormContainer
        |> Formlet.Run (fun (user, pass) ->
            JS.Alert ("Welcome, " + user + "!"))
        |> Doc.RunById "main"
