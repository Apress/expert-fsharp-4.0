module EtoUtils

open Eto
open Eto.Forms
open Eto.Drawing

type TCell =
| El of Control
| StretchedEl of Control
| EmptyElement
| TableEl of Table

and TRow = 
| Row of TCell list
| StretchedRow of TCell list
| Spacing of Size
| Pad of Padding

and Table = Tbl of TRow list

let rec mklayout (Tbl t) =
  let ret = new TableLayout()

  t |> List.iter (fun r ->
    let mktd (tds:TCell list) =
      let row = new TableRow()
      tds |> List.iter (fun td ->
        match td with
        | El c -> row.Cells.Add(new TableCell(c, false))
        | StretchedEl c -> row.Cells.Add(new TableCell(c, true))
        | EmptyElement -> row.Cells.Add(new TableCell(null, true))
        | TableEl t -> row.Cells.Add(new TableCell(mklayout t, true))
      )
      row
    match r with
    | Row tds -> let r = mktd tds in ret.Rows.Add(r)
    | StretchedRow tds -> let r = mktd tds in r.ScaleHeight <- true; ret.Rows.Add(r)
    | Spacing sz -> ret.Spacing <- sz
    | Pad pad -> ret.Padding <- pad
  )
  ret

type Menu =
| Item of MenuItem
| MenuItem of string
| RadioMenuItem of string*string
| CheckMenuItem of string
| SubMenu of string*Menu list
| WithAction of Menu*(MenuItem -> unit)
| WithCheck of Menu*bool
| WithShortcut of Menu*Keys


let private radioGroup = new System.Collections.Generic.Dictionary<string, RadioMenuItem>()

let rec mkmenu = function
    | Item m -> m
    | MenuItem lbl -> 
        let m = new ButtonMenuItem(Text=lbl)
        m :> MenuItem
    | RadioMenuItem (group, lbl) ->
        let m = if radioGroup.ContainsKey(group) then 
                    new RadioMenuItem(radioGroup.[group], Text=lbl) 
                else
                    let g = new RadioMenuItem(Text=lbl)
                    radioGroup.[group] <- g
                    g
        m :> MenuItem
    | CheckMenuItem lbl ->
        let m = new CheckMenuItem(Text=lbl)
        m :> MenuItem
    | SubMenu (lbl, lst) ->
        let m = new ButtonMenuItem(Text=lbl)
        lst |> List.iter (fun el -> m.Items.Add(mkmenu el))
        m :> MenuItem
    | WithAction (m, cb) ->
        let ret = mkmenu m
        ret.Click.Add(fun _ -> cb(ret))
        ret
    | WithCheck (m, def) ->
        let ret = mkmenu m
        match ret with
        | :? RadioMenuItem as r -> r.Checked <- def
        | :? CheckMenuItem as c -> c.Checked <- def
        | _ -> () 
        ret
    | WithShortcut (m, k) ->
        let ret = mkmenu m
        ret.Shortcut <- k
        ret

let action cb m  =  WithAction (m, cb)
let check m = WithCheck(m, true)
let shortcut v m = WithShortcut(m, v)
