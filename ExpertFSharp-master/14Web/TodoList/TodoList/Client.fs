namespace TODOList

open WebSharper
open WebSharper.JavaScript
open WebSharper.JQuery
open WebSharper.UI.Next
open WebSharper.UI.Next.Client

[<JavaScript>]
module Code =

    type IndexTemplate = Templating.Template<"index.html">

    [<NoComparison>]
    type Task = { Name: string; Done: Var<bool> }

    let Tasks =
        ListModel.Create (fun task -> task.Name)
            [ { Name = "Have breakfast"; Done = Var.Create true }
              { Name = "Have lunch"; Done = Var.Create false } ]

    let NewTaskName = Var.Create ""

    let Main =
        IndexTemplate.Main.Doc(
            ListContainer =
                [ListModel.View Tasks |> Doc.Convert (fun task ->
                    IndexTemplate.ListItem.Doc(
                        Task = task.Name,
                        Clear = (fun _ _ -> Tasks.RemoveByKey task.Name),
                        Done = task.Done,
                        ShowDone = Attr.DynamicClass "checked" task.Done.View id)
                )],
            NewTaskName = NewTaskName,
            Add = (fun _ _ ->
                Tasks.Add { Name = NewTaskName.Value; Done = Var.Create false }
                Var.Set NewTaskName ""),
            ClearCompleted = (fun _ _ -> Tasks.RemoveBy (fun task -> task.Done.Value))
        )
        |> Doc.RunById "tasks"

