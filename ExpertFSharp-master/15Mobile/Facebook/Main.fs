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
