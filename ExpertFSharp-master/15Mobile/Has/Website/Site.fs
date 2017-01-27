namespace Website

open IntelliFactory.WebSharper

module MyResources =
    type HasJs() =
        inherit Resources.BaseResource("has.js")

    type HasJsDetections() =
        inherit Resources.BaseResource(
            "detect/",
            "bugs.js",
            "array.js",
            "graphics.js",
            "css.js",
            "dates.js",
            "dom.js",
            "features.js",
            "form.js",
            "function.js",
            "json.js",
            "object.js",
            "strings.js",
            "events.js",
            "script.js",
            "audio.js",
            "video.js")

type Action = | Root

module HasJs =
    open IntelliFactory.WebSharper.Html

    [<Inline("has($s) ? $ifyes() : $ifno()")>]
    let has (s : string) (ifyes : unit -> Element) (ifno : unit -> Element) : Element = P []

    [<JavaScript>]
    let report s =
        has s
            <| fun () -> P [Text (s + " ")] -< [ Span [Attr.Style "color:green;font-style:italic"] -< [Text "true"]]
            <| fun () -> P [Text (s + " ")] -< [ Span [Attr.Style "color:red;font-style:italic"] -< [Text "false"]]

    [<JavaScript>]
    let Test () =
        Div [
            P [Text "This is a test using has.js ..."]
            report "activex"
            report "canvas"
            report "canvas-text"
            report "canvas-webgl"
            report "audio"
            report "audio-m4a"
            report "audio-mp3"
            report "audio-ogg"
            report "audio-wav"
            report "video"
            report "video-h264-baseline"
            report "video-ogg-theora"
            report "video-webm"
        ]

[<Require(typeof<MyResources.HasJs>)>]
[<Require(typeof<MyResources.HasJsDetections>)>]
type HasTester() =
    inherit Web.Control()

    [<JavaScript>]
    override this.Body = HasJs.Test() :> _

module Pages =
    open IntelliFactory.WebSharper.Sitelets
    open IntelliFactory.Html

    let RootPage =
        Content.PageContent <| fun ctx ->
            {
                Page.Default with
                    Title = Some "Has.js Feature Detection"
                    Body =
                        [
                            Div [new HasTester()]
                        ]
            }

module Site =
    open IntelliFactory.WebSharper.Sitelets
    let Main = Sitelet.Content "/" Root Pages.RootPage

    type Website() =
        interface IWebsite<Action> with
            member this.Sitelet = Main
            member this.Actions = [Root]

[<assembly : Sitelets.Website(typeof<Site.Website>)>]
do ()
