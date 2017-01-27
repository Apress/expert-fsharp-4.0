namespace Piglets

open WebSharper
open WebSharper.JavaScript
open WebSharper.Html.Client
open WebSharper.Piglets

[<JavaScript>]
module LoginForm =

    let Main =
        Piglet.Return (fun user pass -> (user, pass))
        <*> (Piglet.Yield ""
            |> Validation.IsNotEmpty "Must enter a valid username")
        <*> (Piglet.Yield ""
            |> Validation.IsNotEmpty "Must enter a valid password")
        |> Piglet.WithSubmit
        |> Piglet.Run (fun (user, pass) ->
            JS.Alert ("Welcome, " + user + "!"))
        |> Piglet.Render (fun user pass submit ->
            Div [
                Div [Label [Text "Username: "]; Controls.Input user]
                Div [Label [Text "Password: "]; Controls.Password pass]
                Controls.Button submit -< [Text "Log in"]
                Div [] |> Controls.ShowErrors submit (fun errors ->
                    List.map (fun error -> P [Text error]) errors)
            ]
        )
        |> fun s -> s.AppendTo "main"
