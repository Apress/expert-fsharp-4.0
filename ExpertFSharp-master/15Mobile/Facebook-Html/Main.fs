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
         let (||?) (x : 'a) (y : 'a) =
            if As<bool> x then x else y

         [<JavaScript>]
         override this.Body =
            Mobile.Use()
            Mobile.Instance.DefaultPageTransition <- "slide"
            FB.Init(InitOptions(AppId="<APP-ID-GOES-HERE>"))
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

[<assembly : Website(typeof<MyWebsite>)>]
do ()
