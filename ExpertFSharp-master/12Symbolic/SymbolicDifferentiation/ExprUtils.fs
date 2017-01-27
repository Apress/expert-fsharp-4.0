module Symbolic.Expressions.Utils

open Symbolic.Expressions

/// A helper function to map/select across a list while threading state
/// through the computation
let collectFold f l s =
    let l,s2 = (s, l) ||>  List.mapFold (fun z x -> f x z)
    List.concat l,s2

/// Collect constants
let rec collect e = 
    match e with
    | Prod (e1, e2) ->
        match collect e1, collect e2 with
        | Num n1, Num n2       -> Num (n1 * n2)
        | Num n1, Prod (Num n2, e)
        | Prod (Num n2, e), Num n1 -> Prod (Num (n1 * n2), e)
        | Num n, e | e, Num n      -> Prod (Num n, e)
        | Prod (Num n1, e1), Prod (Num n2, e2) ->
            Prod (Num (n1 * n2), Prod (e1, e2))
        | e1', e2'                 -> Prod (e1', e2')
    | Num _ | Var _ as e   -> e
    | Neg e                -> Neg (collect e)
    | Add exprs            -> Add (List.map collect exprs)
    | Sub (e1, exprs)      -> Sub (collect e1, List.map collect exprs)
    | Frac (e1, e2)        -> Frac (collect e1, collect e2)
    | Pow (e1, n)        -> Pow (collect e1, n)
    | Sin e                -> Sin (collect e)
    | Cos e                -> Cos (collect e)
    | Exp _ as e           -> e

/// Push negations through an expression
let rec negate e = 
    match e with
    | Num n           -> Num (-n)
    | Var v as exp      -> Neg exp
    | Neg e             -> e
    | Add exprs         -> Add (List.map negate exprs)
    | Sub _             -> failwith "unexpected Sub"
    | Prod (e1, e2)     -> Prod (negate e1, e2)
    | Frac (e1, e2)     -> Frac (negate e1, e2)
    | exp               -> Neg exp

let filterNums (e:Expr) n =
    if e.IsNumber
    then [], n + e.NumOf
    else [e], n

let summands e = 
    match e with 
    | Add es -> es 
    | e -> [e]

/// Simplify an expression
let rec simp e = 
    match e with
    | Num n -> Num n
    | Var v -> Var v
    | Neg e -> negate (simp e)
    | Add exprs ->
        let exprs2, n =
            (exprs, 0M) ||> collectFold (simp >> summands >> collectFold filterNums) 
        match exprs2 with
        | [] -> Num n
        | [e] when n = 0M -> e
        | _ when n = 0M -> Add exprs2
        | _ -> Add (exprs2 @ [Num n])
    | Sub (e1, exprs) -> simp (Add (e1 :: List.map Neg exprs))
    | Prod (e1, e2) ->
        match simp e1, simp e2 with
        | Num 0M, _ | _, Num 0M -> Num 0M
        | Num 1M, e | e, Num 1M -> e
        | Num n1, Num n2 -> Num (n1 * n2)
        | e1, e2 -> Prod (e1, e2)
    | Frac (e1, e2) ->
        match simp e1, simp e2 with
        | Num 0M, _ -> Num 0M
        | e1, Num 1M -> e1
        | Num n, Frac (Num n2, e) -> Prod (Frac (Num n, Num n2), e)
        | Num n, Frac (e, Num n2) -> Frac (Prod (Num n, Num n2), e)
        | e1, e2 -> Frac (e1, e2)
    | Pow (e, 1M) -> simp e
    | Pow (e, n) -> Pow (simp e, n)
    | Sin e -> Sin (simp e)
    | Cos e -> Cos (simp e)
    | Exp e -> Exp (simp e)

let simplify e = e |> simp |> simp |> collect

let rec diff v e =
    match e with 
    | Num _ -> Num 0M
    | Var v2 when v2=v -> Num 1M
    | Var _ -> Num 0M
    | Neg e -> diff v (Prod ((Num -1M), e))
    | Add exprs -> Add (List.map (diff v) exprs)
    | Sub (e1, exprs)  -> Sub (diff v e1, List.map (diff v) exprs)
    | Prod (e1, e2) -> Add [Prod (diff v e1, e2); Prod (e1, diff v e2)]
    | Frac (e1, e2) -> Frac (Sub (Prod (diff v e1, e2), [Prod (e1, diff v e2)]),Pow (e2, 2M))
    | Pow (e1, n) -> Prod (Prod (Num n, Pow (e1, n - 1M)), diff v e1)
    | Sin e -> Prod (Cos e, diff v e)
    | Cos e -> Neg (Prod (Sin e, diff v e))
    | Exp (Var v2) as e when v2=v  -> e
    | Exp (Var v2) -> Num 0M
    | Exp e -> Prod (Exp e, diff v e) 