namespace Website

open WebSharper
open WebSharper.Sitelets
open WebSharper.UI.Next
open WebSharper.UI.Next.Server

type Order =
    { ItemName: string; Quantity: int }

type OrderStore(items: Map<int, Order>) =
    let store = ref items

    new () = OrderStore(Map.empty)

    member this.Store = !store
    member this.Orders = !store |> Map.toList |> List.map snd

    member this.Save (id: int) (order: Order) =
        store := this.Store.Add(id, order)

    member this.FindById (id: int) =
        this.Store.TryFind id

    member this.Delete (id: int) =
        if this.Store.ContainsKey id then
            store := this.Store.Remove id

    member this.GetId () =
        if Map.isEmpty !store then 1 else
            !store |> Map.toList |> List.map fst |> List.max |> fun i -> i+1

type EndPoint =
    | [<EndPoint "/">] Home
    | [<EndPoint "GET /order">] GetOrder of int
    | [<EndPoint "PUT /order"; Json "order">] UpdateOrder of int * order: Order
    | [<EndPoint "POST /order"; Json "order">] CreateOrder of order: Order
    | [<EndPoint "DELETE /order"; Json "id">] DeleteOrder of id: int
    | [<EndPoint "/edit">] EditOrder of int
    | [<EndPoint "/orders">] ListOrders

module Templating =
    open WebSharper.UI.Next.Html

    type MainTemplate = Templating.Template<"Main.html">

    // Compute a menubar where the menu item for the given endpoint is active
    let MenuBar (ctx: Context<EndPoint>) endpoint : Doc list =
        let (=>) txt act =
             liAttr [if endpoint = act then yield attr.``class`` "active"] [
                aAttr [attr.href (ctx.Link act)] [text txt]
             ] :> Doc
        [
            "Home" => EndPoint.Home
            "List orders" => EndPoint.ListOrders
        ]

    let Main ctx action title body =
        Content.Page(
            MainTemplate.Doc(
                title = title,
                menubar = MenuBar ctx action,
                body = body
            )
        )

[<JavaScript>]
module Client =
    open WebSharper.JavaScript
    open WebSharper.UI.Next
    open WebSharper.UI.Next.Html
    open WebSharper.UI.Next.Client
    open WebSharper.Forms
    open WebSharper.JQuery

    /// General function to send an AJAX request with a body.
    let Ajax (met: string) (url: string) (serializedData: string) : Async<string> =
        Async.FromContinuations (fun (ok, ko, _) ->
            JQuery.Ajax(
                JQuery.AjaxSettings(
                    Url = url,
                    Type = As<JQuery.RequestType> met,
                    ContentType = "application/json",
                    DataType = JQuery.DataType.Text,
                    Data = serializedData,
                    Success = (fun (result, _, _) -> ok (result :?> string)),
                    Error = (fun (jqXHR, _, _) -> ko (System.Exception(jqXHR.ResponseText)))
                )
            )
            |> ignore)

    let private OP(met, url, data) =
        async {
            let! response = Ajax met ("/order" + url) data
            JS.Window.Location.Replace "/"
        } |> Async.StartImmediate

    let private LINK(met, url, data) =
        aAttr [
            on.click (fun e arg -> OP(met, url, data))
        ] [text met]

    let DeleteOrder id = LINK("DELETE", "", string id)
    let CreateOrder data = OP("POST", "", data)
    let UpdateOrder(id, data) = OP("PUT", "/" + string id, data)

    let OrderForm (orderOpt: (int * Order) option) =
        let title, quantity =
            match orderOpt with
            | None ->
                Var.Create "", Var.Create (CheckedInput.Blank "")
            | Some (id, o) ->
                Var.Create o.ItemName,
                Var.Create (CheckedInput.Valid(o.Quantity, string o.Quantity))
        Form.Return (fun title qty -> { ItemName = title; Quantity = qty })
        <*> (Form.YieldVar title
            |> Validation.IsNotEmpty "Must enter a title")
        <*> (Form.YieldVar quantity
            |> Validation.MapValidCheckedInput "Must enter a valid quantity")
        |> Form.WithSubmit
        |> Form.Run (fun order ->
            match orderOpt with
            | None -> CreateOrder (Json.Serialize order)
            | Some (id, _) -> UpdateOrder(id, Json.Serialize order)
        )
        |> Form.Render (fun title quantity submit ->
            form [
                label [
                    text "Title: "
                    Doc.Input [] title
                ]
                br []
                label [
                    text "Quantity: "
                    Doc.IntInput [] quantity
                ]
                br []
                Doc.Button "Submit" [] submit.Trigger
                div [
                    submit.View.Doc (function
                        | Success _ -> Doc.Empty
                        | Failure msgs ->
                            Doc.Concat (msgs |> List.map (fun m -> p [text m.Text] :> _))
                    )
              ]
            ]
        )

module Site =
    open WebSharper.UI.Next.Html

    let MyStore = new OrderStore(Map.ofList [1, { ItemName="Pair of socks"; Quantity=2 }])

    let CreateOrEditOrderPage ctx orderOpt =
        let (=>) txt endpoint = aAttr [attr.href (ctx.Link endpoint)] [text txt]
        Templating.Main ctx EndPoint.Home "Create order" [
            h1 [text "Create/edit order"]
            hr []
            div [client <@ Client.OrderForm orderOpt @>]
            h1 [text "Orders"]
            tableAttr [attr.``class`` "table table-striped table-hover"] [
                thead [
                    td [text "Order #"]
                    td [text "Item"]
                    td [text "Quantity"]
                ]
                tbody
                    (MyStore.Store
                    |> Map.toList
                    |> Seq.map (fun (id, order) ->
                        tr [
                            td [sprintf "#%d" id => EndPoint.GetOrder id]
                            td [text order.ItemName]
                            td [text (string order.Quantity)]
                            td [
                                client <@ Client.DeleteOrder id @>
                                text " | "
                                "EDIT" => EndPoint.EditOrder id                            ]
                        ] :> Doc
                    ))
            ]
        ]

    let GetOrder id =
        match MyStore.FindById id with
        | None -> Content.NotFound
        | Some order -> Content.Json order

    [<Website>]
    let Main =
        Application.MultiPage (fun ctx endpoint ->
            match endpoint with
            | EndPoint.Home ->
                CreateOrEditOrderPage ctx None
            | EndPoint.EditOrder i ->
                MyStore.FindById i
                |> Option.bind (fun order -> Some (i, order))
                |> CreateOrEditOrderPage ctx
            | EndPoint.CreateOrder order ->
                MyStore.Save (MyStore.GetId()) order
                Content.Text "Order created successfully."
            | EndPoint.DeleteOrder id ->
                MyStore.Delete id
                Content.Text "Order deleted successfully."
            | EndPoint.GetOrder id ->
                GetOrder id
            | EndPoint.ListOrders ->
                Content.Json MyStore.Orders
            | EndPoint.UpdateOrder (id, order) ->
                MyStore.Save id order
                Content.RedirectTemporary EndPoint.ListOrders
        )
