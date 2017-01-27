namespace GettingStarted

open FSharp.Data
open WebSharper
open WebSharper.UI.Next
open WebSharper.UI.Next.Html
open WebSharper.UI.Next.Client
open WebSharper.Charting

[<JavaScript>]
module Client =    
    type WorldBank = WorldBankDataProvider<Asynchronous=true>

    let data = WorldBank.GetDataContext()

    let randomColor =
        let r = System.Random()
        fun () -> Color.Rgba(r.Next 256, r.Next 256, r.Next 256, 1.)

    let countries =
        [| data.Countries.Austria
           data.Countries.Hungary
           data.Countries.``United Kingdom``
           data.Countries.``United States`` |]

    let colors = Array.map (fun _ -> randomColor()) countries

    let mkData (i: Runtime.WorldBank.Indicator) =
        Seq.zip (Seq.map string i.Years) i.Values

    let chart =
        let cfg = 
            ChartJs.LineChartConfiguration(
                PointDot = false,
                BezierCurve = true,
                DatasetFill = false)

        async {
            let! data =
                countries
                |> Seq.map (fun c -> c.Indicators.``School enrollment, tertiary (% gross)``)
                |> Async.Parallel
            return
                data
                |> Array.map mkData
                |> Array.zip colors
                |> Array.map (fun (c, e) -> 
                    Chart.Line(e)
                        .WithStrokeColor(c)
                        .WithPointColor(c))
                |> Chart.Combine
                |> fun c ->
                    Renderers.ChartJs.Render(c, Size = Size(600, 400), Config = cfg)
        }

    let legend =
        colors 
        |> Array.zip countries
        |> Array.map (fun (c, color) -> 
            div [
                spanAttr ["width:20px; height:20px;\
                           margin-right:10px;\
                           display:inline-block;\
                           background-color:" + color.ToString() |> attr.style] []
                span [text c.Name]
            ])
        |> Seq.cast
        |> div

    let Main =
        Doc.Concat [
            h2 [text "Tertiary school enrollment (% gross)"]
            Doc.Async chart
            legend
        ]
        |> Doc.RunById "main"
