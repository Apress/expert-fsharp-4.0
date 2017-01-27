open IntelliFactory.WebSharper

module MyResources =
    type HasJs() =
        inherit Resources.BaseResource("http://[put-URL-here]/has.js")

[<Require(typeof<MyResources.HasJs>)>]
module HasJs =

    [<Inline("has($s) ? $ifyes() : $ifno()")>]
    let Has (s: string) (ifyes: unit -> unit) (ifno: unit -> unit) =
        ()

do HasJs.Has "video"
    <| fun () ->
        // Your enviroment supports HTML5 video
        ...
    <| fun () ->
       // It doesn’t, so you must use some kind of video polyfill
       ...


//<!DOCTYPE html>
//<html>
//  <head>
//    <meta name="generator" content="websharper" data-replace="scripts" />
//    <meta name="viewport" content="width=device-width, initial-scale=1"/>
//    <style type="text/css"><![CDATA[
//        *
//        {
//            margin: 0;
//            padding: 0;
//        }
//        
//        .main
//        {
//            position: absolute;
//            top: 0;
//            left: 0;
//            width: 100%;
//            height: 100%;
//            overflow: hidden;
//        }
//        
//        .head
//        {
//            position: absolute;
//            height: 30px;
//            text-align: center;
//            line-height: 30px;
//            background-color: Black;
//            color: White;
//            z-index: 1;
//            width: 100%;
//        }
//        
//        .head > *
//        {
//            float: left;
//            margin-right: 10px;
//        }
//        
//        .pan-image
//        {
//            display: none;
//        }
//        
//        .pan-canvas
//        {
//            position: absolute;
//            top: 30px;
//            left: 0;
//        }
//    ]]></style>
//  </head>
//  <body>
//    <div data-hole="body"></div>
//  </body>
//</html>

namespace MyNamespace

open System
open System.IO
open System.Web
open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Sitelets

module MySite =
   open IntelliFactory.Html

   type Action = | Index

   module Skin =
      type Page =
         {
             Body: Content.HtmlElement list
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
      open IntelliFactory.WebSharper.JQuery

      type State =
         {
            mutable x: float
            mutable y: float
            mutable scale: float
            mutable angle: float
         }

      type MyControl() =
         inherit IntelliFactory.WebSharper.Web.Control()

         [<JavaScript>]
         override this.Body =
            let main = Div [Attr.Class "main"]
            let head = Div [Attr.Class "head"] -< [Text "WebSharper Image Viewer"]
            main -< [
               head
               HTML5.Tags.Canvas [Attr.Class "pan-canvas"; Attr.Width "600";Attr.Height "600"]
               |>! OnAfterRender (fun e ->
                  let canvas = As<Html5.CanvasElement> e.Body
                  let ctx = canvas.GetContext "2d"
                  Img [Attr.Src "images/map.jpg"; Attr.Class "pan-image"]
                  |> Events.OnLoad (fun img ->
                     let state = { x = 0.; y = 0.; scale = 1.; angle = 0. }
                     let delta = { x = 0.; y = 0.; scale = 1.; angle = 0. }
                     let redraw() =
                        // Reset the transformation matrix and clear the canvas.
                        ctx.SetTransform(1., 0., 0., 1., 0., 0.)
                        ctx.ClearRect(0., 0., float canvas.Width, float canvas.Height)
                        // In order to have centered rotation and zoom, we must first
                        // put the center of the image at the origin of the coordinate system.
                        ctx.Translate(float canvas.Width / 2., float canvas.Height / 2.)
                        ctx.Scale(state.scale * delta.scale, state.scale * delta.scale)
                        ctx.Rotate(state.angle + delta.angle)
                        // Then, when the rotation and zoom are done, we put the image back
                        // at the center of the screen, plus the (x, y) translation.
                        ctx.Translate(state.x + delta.x - float canvas.Width / 2.,
                           state.y + delta.y - float canvas.Height / 2.)
                        ctx.DrawImage(img.Body, 0., 0.)
                     let settleDelta() =
                        state.x <- state.x + delta.x
                        state.y <- state.y + delta.y
                        state.angle <- state.angle + delta.angle
                        state.scale <- state.scale * delta.scale
                        delta.x <- 0.
                        delta.y <- 0.
                        delta.scale <- 1.
                        delta.angle <- 0.
                        redraw()
                     let panStartPosition = ref None
                        // Pan events
                     Mobile.Events.VMouseDown.On(JQuery.Of(e.Body), fun ev ->
                        panStartPosition := Some (ev.Event.PageX, ev.Event.PageY)
                        ev.Event.PreventDefault())
                     Mobile.Events.VMouseMove.On(JQuery.Of(e.Body), fun ev ->
                        match !panStartPosition with
                        | None -> ()
                        | Some (sx, sy) ->
                           let dx, dy = float(ev.Event.PageX - sx), float(ev.Event.PageY - sy)
                           let angle = state.angle + delta.angle
                           let scale = state.scale * delta.scale
                           delta.x <- (dx * Math.Cos angle + dy * Math.Sin angle) / scale
                           delta.y <- (dy * Math.Cos angle - dx * Math.Sin angle) / scale
                           redraw()
                           ev.Event.PreventDefault())
                     Mobile.Events.VMouseUp.On(JQuery.Of("body"), fun ev ->
                        if (!panStartPosition).IsSome then
                           settleDelta()
                           panStartPosition := None
                           ev.Event.PreventDefault())
                     // iOS-only rotozoom events
                     e.Body.AddEventListener("gesturechange", (fun (ev: Dom.Event) ->
                        delta.scale <- ev?scale
                        delta.angle <- ev?rotation * System.Math.PI / 180.
                        redraw()
                        ev.PreventDefault()
                        ), false)
                     e.Body.AddEventListener("gestureend", (fun (ev: Dom.Event) ->
                        settleDelta()
                        ev.PreventDefault()
                        ), false)
                     redraw()
                   )
               )
             ] :> IPagelet

   let Index =
      Skin.WithTemplate <| fun ctx ->
         [
            Div [new Client.MyControl()]
         ]

   let MySitelet =
      Sitelet.Content "/index" Action.Index Index

type MyWebsite() =
    interface IWebsite<MySite.Action> with
        member this.Sitelet = MySite.MySitelet
        member this.Actions = [MySite.Action.Index]

[<assembly: Website(typeof<MyWebsite>)>]
do ()

//<div class="main">
//    <div class="head">WebSharper Image Viewer</div>
//    <canvas class="pan-canvas" width="600" height="600">
//    </canvas>
//</div>

//                     e.Body.AddEventListener("gesturechange", (fun (ev: Dom.Event) ->
//                        delta.scale <- ev?scale
//                        delta.angle <- ev?rotation * System.Math.PI / 180.
//                        redraw()
//                        ev.PreventDefault()
//                        ), false)

namespace IntelliFactory.WebSharper.Facebook

module Definition =
    open IntelliFactory.WebSharper.InterfaceGenerator
    open IntelliFactory.WebSharper.Dom

    module Res =
        let FacebookAPI =
            Resource "FacebookAPI" "https://connect.facebook.net/en_US/all.js"

    let FlashHidingArgs =
        Class "FB.FlashHidingArgs"
        |+> Protocol [
                "state" =? T<string>
                "elem" =? T<Element>
            ]

    let InitOptions =
        Pattern.Config "FB.InitOptions" {
            Required = []
            Optional =
                [
                    "appId", T<string>
                    "cookie", T<bool>
                    "logging", T<bool>
                    "status", T<bool>
                    "xfbml", T<bool>
                    "channelUrl", T<string>
                    "authResponse", T<obj>
                    "frictionlessRequests", T<bool>
                    "hideFlashCallback", FlashHidingArgs ^-> T<unit>
                ]
        }

    let AuthResponse =
        Class "FB.AuthResponse"
        |+> Protocol [
                "accessToken" =? T<string>
                "expiresIn" =? T<string>
                "signedRequest" =? T<string>
                "userId" =? T<string>
            ]

    let UserStatus =
        Pattern.EnumStrings "FB.UserStatus"
            ["connected"; "not_authorized"; "unknown"]

    let LoginResponse =
        Class "FB.LoginResponse"
        |+> Protocol [
                "authResponse" =? AuthResponse
                "status" =? UserStatus
            ]

    let LoginOptions =
        Pattern.Config "FB.LoginOptions" {
            Optional =
                [
                    "scope", T<string>
                    "display", T<string>
                ]
            Required = []
        }

    let FB =
        Class "FB"
        |+> [
                "init" => !?InitOptions ^-> T<unit>
                "login" => (LoginResponse ^-> T<unit>) * !?LoginOptions ^-> T<unit>
                "logout" => (LoginResponse ^-> T<unit>) ^-> T<unit>
                "getLoginStatus" => (LoginResponse ^-> T<unit>) ^-> T<unit>
                "getAuthResponse" => T<unit> ^-> AuthResponse
                "api" => T<string>?url * !?T<string>?``method`` * !?T<obj>?options * (T<obj> ^-> T<unit>)?callback ^-> T<unit>
            ]
        |> Requires [Res.FacebookAPI]

    let Assembly =
        Assembly [
            Namespace "IntelliFactory.WebSharper.Facebook" [
                FlashHidingArgs
                InitOptions
                AuthResponse
                UserStatus
                LoginResponse
                LoginOptions
                FB
            ]
            Namespace "IntelliFactory.WebSharper.Facebook.Resources" [
                Res.FacebookAPI
            ]
        ]

module Main =
    open IntelliFactory.WebSharper.InterfaceGenerator

    do Compiler.Compile stdout Definition.Assembly

//<!DOCTYPE html>
//<html>
//  <head>
//    <title>My Facebook Wall</title>
//    <meta name="viewport" content="width=device-width, initial-scale=1" />
//    <meta name="generator" content="websharper" data-replace="scripts" />
//  </head>
//  <body>
//    <div data-replace="body" />
//    <div data-role="page" id="dummy" />
//  </body>
//</html>

namespace IntelliFactory.Facebook.Application

open System
open System.IO
open IntelliFactory.WebSharper.Sitelets

module MySite =
   open IntelliFactory.WebSharper
   open IntelliFactory.Html

   type Action = | Index

   module Skin =

      type Page =
         {
             Body : Content.HtmlElement list
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
      open IntelliFactory.WebSharper.Facebook
      open IntelliFactory.WebSharper.JQuery
      open IntelliFactory.WebSharper.JQuery.Mobile

      type Control() =
         inherit IntelliFactory.WebSharper.Web.Control()

         [<JavaScript>]
         let (||?) (x: 'a) (y: 'a) =
            if As<bool> x then x else y

         [<JavaScript>]
         override this.Body =
            Mobile.Use()
            Mobile.Instance.DefaultPageTransition <- "slide"
            FB.Init(InitOptions(AppId="158910217579395"))
            let btncStatus = Span [Text "Log in"]
            let btnGetPosts =
               A [Text "Get latest wall posts"; Attr.HRef "#"; Attr.Style "display: none;"]
            let updateStatus (resp: LoginResponse) =
               if resp.Status = UserStatus.Connected then
                  btncStatus.Text <- "Log out"
                  JQuery.Of(btnGetPosts.Body).Show().Ignore
               else
                  btncStatus.Text <- "Log in"
                  JQuery.Of(btnGetPosts.Body).Hide().Ignore
            let wallPage =
                Div [
                   Attr.Id "wall"; HTML5.Attr.Data "role" "page"
                   HTML5.Attr.Data "url" "#wall"
                ]
            let wall =
                UL [HTML5.Attr.Data "role" "listview"; HTML5.Attr.Data "inset" "true"]
            wallPage -< [
               Div [
                  HTML5.Attr.Data "role" "header"; HTML5.Attr.Data "position" "fixed"
               ] -< [
                  H1 [Text "My Facebook Wall"]
                  A [Attr.HRef "#"] -< [btncStatus]
                  |>! OnClick (fun el ev ->
                     FB.GetLoginStatus <| fun resp ->
                        if resp.Status = UserStatus.Connected then
                           FB.Logout updateStatus
                        else
                           FB.Login(updateStatus, LoginOptions(Scope = "read_stream"))
                  )
                  |>! OnAfterRender (fun _ -> FB.GetLoginStatus updateStatus)
                  btnGetPosts
                  |>! OnClick (fun el ev ->
                     Mobile.Instance.ShowPageLoadingMsg("a", "Receiving wall posts...")
                     FB.Api("/me/home", fun o ->
                        wall.Clear()
                        o?data |> Array.iter (fun x ->
                           let message = x?message ||? x?story ||? x?caption
                           LI [
                              yield H6 [Text message]
                              yield P [Text ("From: " + x?from?name)]
                              let numComments =
                                 if x?comments && x?comments?count > 0 then
                                    "See " + x?comments?count
                                 else
                                    "0"
                              yield P [Text (numComments + " comments")]
                              yield Img [Attr.Src (if x?picture then x?picture else "")]
                              if x?comments && x?comments?data then
                                 yield UL [
                                    yield! x?comments?data |> Array.map (fun comment ->
                                       LI [
                                          H6 [Text comment?message]
                                          P [Text comment?from?name]
                                       ]
                                    )
                                    yield LI [
                                       HTML5.Attr.Data "iconpos" "left";
                                       HTML5.Attr.Data "icon" "arrow-l"] -< [
                                          A [Attr.HRef "#"] -< [Text "Back"]
                                          |>! OnClick (fun _ _ ->
                                             Mobile.Instance.ChangePage(
                                                JQuery.Of(wallPage.Body),
                                                ChangePageConfig(Reverse = true))
                                          )
                                       ]
                                    ]
                           ]
                           |> wall.Append
                           Mobile.Instance.HidePageLoadingMsg()
                        )
                        JQuery.Of(wall.Body) |> ListView.Refresh
                     )
                  )
               ]
               Div [HTML5.Attr.Data "role" "content"] -< [wall]
            ]
            |>! OnAfterRender (fun el ->
               JQuery.Of(el.Body)
               |> JQuery.Page
               |> Mobile.Instance.ChangePage
            ) :> IPagelet

   let Index =
      Skin.WithTemplate <| fun ctx ->
         [
             Div [new Client.Control()]
         ]

   let MySitelet =
      Sitelet.Content "/" Action.Index Index

type MyWebsite() =
   interface IWebsite<MySite.Action> with
      member this.Sitelet = MySite.MySitelet
      member this.Actions = [MySite.Action.Index]

[<assembly: Website(typeof<MyWebsite>)>]
do ()

//<div id="wall" data-role="page" data-url="#wall">
//   <div data-role="header" data-position="fixed">
//      ...
//   <div>
//   <div data-role="content">
//      <ul data-role="listview" data-inset="true">
//         ...
//      </ul>
//   </div>
//</div>

//<!DOCTYPE html>
//<html>
//  <head>
//    <title>Your Android Application</title>
//    <meta name="viewport" content="width=device-width, initial-scale=1" />
//    <meta name="generator" content="websharper" data-replace="scripts" />
//  </head>
//  <body>
//    <div data-hole="body" />
//  </body>
//</html>

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

[<assembly: Website(typeof<MySite.MyWebsite>)>]
do ()

            let map =
                Div []
                |>! OnAfterRender (fun this ->
                    let map = Bing.Map(this.Body, MapOptions)
                    map.SetMapType(Bing.MapTypeId.Road)
                    setMap map)
            ...

            Formlet.Do {
                let! username, password =
                    Formlet.Yield (fun user pass -> user, pass)
                    <*> <... formlet-1 ...>
                    <*> <... formlet-2 ...>
                    |> Enhance.WithSubmitButton "Log in" Enums.Theme.C
                do! Formlet.OfElement (fun _ ->
                    Div [
                        H3 [Text ("Welcome " + username + "!")]
                        ShowMap()
                    ])
            }
            |> Formlet.Flowlet
