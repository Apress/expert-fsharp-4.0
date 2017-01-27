namespace Website

open WebSharper
open WebSharper.Formlets

[<JavaScript>]
module FormletSnippets =
    open WebSharper.Html.Client

    let Snippet1 = Controls.Input "initial value"

    let RunInBlock title f formlet =
        let output = Div []
        formlet
        |> Formlet.Run (fun res ->
            let elem = f res
            output -< [ elem ] |> ignore)
        |> fun form ->
            Div [Attr.Style "float:left;margin-right:20px;width:300px;min-height:200px"]-<[
                H5 [Text title]
                Div [form]
                output
            ]

    let RunSnippet<'T> title formlet =
        formlet
        |> RunInBlock title (fun (s: 'T) ->
            Div [
                P [Text ("You entered: " + string (box s))]
            ])

    let Snippet1a =
        Formlet.Yield id
        <*> (Controls.Input ""
             |> Validator.Is (fun s -> s.Length > 3) "Enter a valid name")

    let Snippet1b =
        Formlet.Yield id
        <*> (Controls.Input ""
             |> Validator.IsNotEmpty "Enter a valid name"
             |> Enhance.WithFormContainer)

    let Snippet1c =
        Formlet.Yield id
        <*> (Controls.Input ""
             |> Validator.IsNotEmpty "Enter a valid name"
             |> Enhance.WithFormContainer
             |> Enhance.WithSubmitAndResetButtons)

    let Snippet1d =
        Formlet.Yield id
        <*> (Controls.Input ""
             |> Validator.IsNotEmpty "Enter a valid name"
             |> Enhance.WithValidationIcon
             |> Enhance.WithErrorSummary "Errors"
             |> Enhance.WithSubmitAndResetButtons
             |> Enhance.WithFormContainer)

    let Snippet1e =
        Formlet.Yield id
        <*> (Controls.Input ""
             |> Validator.IsNotEmpty "Enter a valid name"
             |> Enhance.WithValidationIcon
             |> Enhance.WithTextLabel "Name"
             |> Enhance.WithSubmitAndResetButtons
             |> Enhance.WithFormContainer)

    let Snippet1f =
        Formlet.Yield id
        <*> (Controls.Input ""
             |> Validator.IsNotEmpty "Enter a valid name"
             |> Enhance.WithValidationIcon
             |> Enhance.WithLabelAndInfo "Name" "Enter your name"
             |> Enhance.WithSubmitAndResetButtons
             |> Enhance.WithFormContainer)

    let input label err = 
        Controls.Input ""
        |> Validator.IsNotEmpty err
        |> Enhance.WithValidationIcon
        |> Enhance.WithTextLabel label

    let inputInt label err = 
        Controls.Input ""
        |> Validator.IsInt err
        |> Formlet.Map int
        |> Enhance.WithValidationIcon
        |> Enhance.WithTextLabel label

    let Snippet2 : Formlet<string * int> =
        Formlet.Yield (fun name age -> name, age)
        <*> input "Name" "Please enter your name"
        <*> inputInt "Age" "Please enter a valid age"
        |> Enhance.WithSubmitAndResetButtons
        |> Enhance.WithFormContainer

    let Snippet3 =
        Formlet.Yield (fun name age -> name, age)
        <*> input "Name" "Please enter your name"
        <*> inputInt "Age" "Please enter a valid age"
        |> Enhance.WithLegend "Person"
        |> Enhance.WithTextLabel "Person"
        |> Enhance.Many
        |> Enhance.WithLegend "People"
        |> Enhance.WithSubmitAndResetButtons
        |> Enhance.WithFormContainer

    let Snippet4a =
        Formlet.Do {
            let! name = input "Name" "Please enter your name"
            let! age = inputInt "Age" "Please enter a valid age"
            return name, age
        }
        |> Enhance.WithSubmitAndResetButtons
        |> Enhance.WithFormContainer

    let Snippet4b =
        Formlet.Do {
            let! name = input "Name" "Please enter your name"
                        |> Enhance.WithSubmitAndResetButtons
                        |> Enhance.WithFormContainer
            let! age =  inputInt "Age" "Please enter a valid age"
                        |> Enhance.WithSubmitAndResetButtons
                        |> Enhance.WithFormContainer
            return name, age
        }
        |> Formlet.Flowlet

    let Main =
        Div([
            RunSnippet "Snippet1"  Snippet1
            RunSnippet "Snippet1a" Snippet1a
            RunSnippet "Snippet1b" Snippet1b
            RunSnippet "Snippet1c" Snippet1c
            RunSnippet "Snippet1d" Snippet1d
            RunSnippet "Snippet1e" Snippet1e
            RunSnippet "Snippet2" Snippet2
            RunSnippet "Snippet3" Snippet3
            RunSnippet "Snippet4a" Snippet4a
            RunSnippet "Snippet4b" Snippet4b
        ]).AppendTo "main"

