namespace Website

open IntelliFactory.WebSharper

module MyResources =
    type ModernizrJs() =
        inherit Resources.BaseResource("modernizr.custom.17231.js")

type Action = | Root

module ModernizrJs =
    open IntelliFactory.WebSharper.Html

    [<Inline("Modernizr[$s] ? $ifyes() : $ifno()")>]
    let has (s : string) (ifyes : unit -> Element) (ifno : unit -> Element) : Element = P []

    [<JavaScript>]
    let report s =
        has s
            <| fun () -> P [Text (s + " ")] -< [ Span [Attr.Style "color:green;font-style:italic"] -< [Text "true"]]
            <| fun () -> P [Text (s + " ")] -< [ Span [Attr.Style "color:red;font-style:italic"] -< [Text "false"]]

    [<JavaScript>]
    let Test () =
        Div [
            P [Text "This is a test using modernizr ..."]
            report "canvas"
            report "canvastext"
            report "webgl"
            report "audio"
            report "webaudio"
            report "video"
            report "websockets"
            report "geolocation"
        ]

[<Require(typeof<MyResources.ModernizrJs>)>]
type ModernizrTester() =
    inherit Web.Control()

    [<JavaScript>]
    override this.Body = ModernizrJs.Test() :> _

module Pages =
    open IntelliFactory.WebSharper.Sitelets
    open IntelliFactory.Html

    let RootPage =
        Content.PageContent <| fun ctx ->
            {
                Page.Default with
                    Title = Some "Modernizer Feature Detection"
                    Body =
                        [
                            Div [new ModernizrTester()]
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
