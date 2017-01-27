namespace MyApplication

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Sitelets

module MySite =
    type Action = | Home

    module Skin =
        open System.IO

        type Page =
            {
                Body : list<Content.HtmlElement>
            }

        let MainTemplate =
            let path = Path.Combine(__SOURCE_DIRECTORY__, "Main.html")
            Content.Template<Page>(path)
                .With("body", fun x -> x.Body)

        let WithTemplate body : Content<Action> =
            Content.WithTemplate MainTemplate <| fun context ->
                {
                    Body = body context
                }

    module Client =
        open IntelliFactory.WebSharper.Html
        open IntelliFactory.WebSharper.Bing
        open IntelliFactory.WebSharper.JQuery
        open IntelliFactory.WebSharper.JQuery.Mobile
        open IntelliFactory.WebSharper.Formlet
        open IntelliFactory.WebSharper.Formlets.JQueryMobile

        [<JavaScript>]
        let BingMapsKey = "<put-your-bing-maps-key-here>"

        [<JavaScript>]
        let ShowMap () =
            let screenWidth = JQuery.Of("body").Width()
            let MapOptions = Bing.MapViewOptions(
                                Credentials = BingMapsKey,
                                Width = screenWidth - 20,
                                Height = screenWidth - 40,
                                Zoom = 16)
            let label = Span []
            let setMap (map : Bing.Map) =
                let updateLocation() =
                    // Gets the current location
                    match Android.Context.Get() with
                    | Some ctx ->
                        if ctx.Geolocator.IsSome then
                            async {
                                let! loc = ctx.Geolocator.Value.GetLocation()
                                // Sets the label to be the address of the current location
                                Rest.RequestLocationByPoint(
                                    BingMapsKey,
                                    loc.Latitude, loc.Longitude, [ "Address" ],
                                    fun result ->
                                        let locInfo = result.ResourceSets.[0].Resources.[0]
                                        label.Text <-
                                            "You are currently at " +
                                            JavaScript.Get "name" locInfo)
                                // Sets the map to point at the current location
                                let loc = Bing.Location(loc.Latitude, loc.Longitude)
                                let pin = Bing.Pushpin loc
                                map.Entities.Clear()
                                map.Entities.Push pin
                                map.SetView(Bing.ViewOptions(Center = loc))
                            }
                            |> Async.Start
                        else
                            ()
                    | None ->
                        ()
                JavaScript.SetInterval updateLocation 1000 |> ignore
            let map =
                Div []
                |>! OnAfterRender (fun this ->
                    let map = Bing.Map(this.Body, MapOptions)
                    map.SetMapType(Bing.MapTypeId.Road)
                    setMap map)
            Div [
                label
                Br []
                map
            ]

        [<JavaScript>]
        let LoginSequence () =
            Formlet.Do {
                let! username, password =
                    Formlet.Yield (fun user pass -> user, pass)
                    <*> (Controls.TextField "" Enums.Theme.C
                        |> Enhance.WithTextLabel "Username"
                        |> Validator.IsNotEmpty "Username cannot be empty!")
                    <*> (Controls.Password "" Enums.Theme.C
                        |> Enhance.WithTextLabel "Password: "
                        |> Validator.IsRegexMatch "^[1-4]{4,}[5-9]$" "The password is wrong!")
                    |> Enhance.WithSubmitButton "Log in" Enums.Theme.C
                do! Formlet.OfElement (fun _ ->
                    Div [
                        H3 [Text ("Welcome " + username + "!")]
                        ShowMap()
                    ])
            }
            |> Formlet.Flowlet

        type ApplicationControl() =
            inherit Web.Control()

            [<JavaScript>]
            override this.Body =
                Div [LoginSequence ()] :> _

    module Pages =
        open IntelliFactory.Html

        let Home =
            Skin.WithTemplate <| fun ctx ->
                [
                    Div [HTML5.Data "role" "page"; Id "main"; HTML5.Data "url" "main"] -< [
                        new Client.ApplicationControl()
                    ]
                ]

    type MyWebsite() =
        interface IWebsite<Action> with
            member this.Sitelet =
                Sitelet.Content "/index" Action.Home Pages.Home
            member this.Actions = [ Action.Home ]

[<assembly : Website(typeof<MySite.MyWebsite>)>]
do ()
