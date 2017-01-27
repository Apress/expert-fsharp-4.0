module MyApplication

open WebSharper
open WebSharper.Sitelets
open WebSharper.UI.Next
open WebSharper.UI.Next.Server

type EndPoint =
    | [<EndPoint "/">] Home
    | [<EndPoint "/authenticated">] Authenticated
    | [<EndPoint "/login">] Login of EndPoint option
    | [<EndPoint "/logout">] Logout

module Site =
    open WebSharper.UI.Next.Html

    module Pages =
        /// A helper function to create a 'fresh' URL with a random parameter
        /// in order to make sure that browsers don't show a cached version.
        let R url =
            url + "?d=" + System.Uri.EscapeUriString (System.DateTime.Now.ToString())

        let Links (ctx: Context<_>) =
            let ( => ) title ep = aAttr [attr.href (ctx.Link ep)] [text title]
            let user = ctx.UserSession.GetLoggedInUser() |> Async.RunSynchronously
            ul [
                li ["Home" => EndPoint.Home]
                li ["Authenticated" => EndPoint.Authenticated]
                (if user.IsNone then
                    li ["Login" => EndPoint.Login None]
                else
                    li ["Logout" => EndPoint.Logout]) 
            ]

        let Home ctx =
            Content.Page(
                Title = "Home",
                Body = [Links ctx; h1 [text "Home page, use links above"]])

        let Authenticated ctx =
            Content.Page(
                Title = "Authenticated",
                Body = [Links ctx; h1 [text "This page requires a login!"]])

        let Logout ctx =
            Content.Page(
                Title = "Logout",
                Body = [Links ctx; h1 [text "You have been logged out."]])

        let Login ctx endpoint =
            let redirectUrl =
                match endpoint with
                | None -> EndPoint.Home
                | Some ep -> ep
                |> ctx.Link
                |> R
            Content.Page(
                Title = "Login",
                Body = [
                    h1 [text "Login"]
                    p [text "and... you are logged in magically..."]
                    aAttr [attr.href redirectUrl] [text "Proceed further"]
                ])

    let Authenticated =
        let filter : Sitelet.Filter<EndPoint> =
            { VerifyUser = fun _ -> true
              LoginRedirect = Some >> EndPoint.Login }

        Sitelet.Protect filter
            (Sitelet.Content "/authenticated" EndPoint.Authenticated Pages.Authenticated)

    [<Website>]
    let Main =
        Authenticated
        <|> Application.MultiPage (fun ctx endpoint ->
            match endpoint with
            | EndPoint.Home -> Pages.Home ctx
            | EndPoint.Login endpoint ->
                async {
                    // Log in as "visitor" without requiring anything
                    do! ctx.UserSession.LoginUser "visitor"
                    return! Pages.Login ctx endpoint
                }
            | EndPoint.Logout ->
                async {
                    // Log out the "visitor" user and redirect to home
                    do! ctx.UserSession.Logout ()
                    return! Pages.Logout ctx
                }
            | EndPoint.Authenticated -> Content.ServerError
        )
