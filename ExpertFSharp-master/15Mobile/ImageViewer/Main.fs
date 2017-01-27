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
            mutable x : float
            mutable y : float
            mutable scale : float
            mutable angle : float
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
                  Img [Attr.Src "map.jpg"; Attr.Class "pan-image"]
                  |> Events.OnLoad (fun img ->
                     let state = {x = 0.; y = 0.; scale = 1.; angle = 0.}
                     let delta = {x = 0.; y = 0.; scale = 1.; angle = 0.}
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
                     e.Body.AddEventListener("gesturechange", (fun (ev : Dom.Event) ->
                        delta.scale <- ev?scale
                        delta.angle <- ev?rotation * System.Math.PI / 180.
                        redraw()
                        ev.PreventDefault()
                        ), false)
                     e.Body.AddEventListener("gestureend", (fun (ev : Dom.Event) ->
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

[<assembly : Website(typeof<MyWebsite>)>]
do ()
