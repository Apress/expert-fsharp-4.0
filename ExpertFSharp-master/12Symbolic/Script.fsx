type Var = string

type Prop =
    | And of Prop * Prop
    | Var of Var
    | Not of Prop
    | Exists of Var * Prop
    | False

let True          = Not False
let Or(p, q)      = Not(And(Not(p), Not(q)))
let Iff(p, q)     = Or(And(p, q), And(Not(p), Not(q)))
let Implies(p, q) = Or(Not(p), q)
let Forall(v, p)  = Not(Exists(v, Not(p)))

let (&&&) p q = And(p, q)
let (|||) p q = Or(p, q)
let (~~~) p   = Not p
let (<=>) p q = Iff(p, q)
let (===) p q = (p <=> q)
let (==>) p q = Implies(p, q)
let (^^^) p q = Not (p <=> q)

let var (nm: Var) = Var nm

let fresh =
    let mutable count = 0
    fun nm -> count <- count + 1; (sprintf "_%s%d" nm count : Var)
//type Var = string
//type Prop =
//  | And of Prop * Prop
//  | Var of Var
//  | Not of Prop
//  | Exists of Var * Prop
//  | False
//val True : Prop = Not False
//val Or : p:Prop * q:Prop -> Prop
//val Iff : p:Prop * q:Prop -> Prop
//val Implies : p:Prop * q:Prop -> Prop
//val Forall : v:Var * p:Prop -> Prop
//val ( &&& ) : p:Prop -> q:Prop -> Prop
//val ( ||| ) : p:Prop -> q:Prop -> Prop
//val ( ~~~ ) : p:Prop -> Prop
//val ( <=> ) : p:Prop -> q:Prop -> Prop
//val ( === ) : p:Prop -> q:Prop -> Prop
//val ( ==> ) : p:Prop -> q:Prop -> Prop
//val ( ^^^ ) : p:Prop -> q:Prop -> Prop
//val var : nm:Var -> Prop
//val fresh : (string -> Var)

let rec eval (env: Map<Var,bool>) inp =
    match inp with
    | Exists(v, p) -> eval (env.Add(v, false)) p || eval (env.Add(v, true)) p
    | And(p1, p2)  -> eval env p1 && eval env p2
    | Var v        -> if env.ContainsKey(v) then env.[v]
                      else failwithf "env didn't contain a value for %A" v
    | Not p        -> not (eval env p)
    | False        -> false

let rec support f =
    match f with
    | And(x, y)    -> Set.union (support x) (support y)
    | Exists(v, p) -> (support p).Remove(v)
    | Var p        -> Set.singleton p
    | Not x        -> support x
    | False        -> Set.empty

let rec cases supp =
    seq {
        match supp with
        | [] ->  yield Map.empty
        | v :: rest ->
            yield! rest |> cases |> Seq.map (Map.add v false)
            yield! rest |> cases |> Seq.map (Map.add v true)
    }

let truthTable x =
    x |> support |> Set.toList |> cases
    |> Seq.map (fun env -> env, eval env x)

let satisfiable x =
    x 
    |> truthTable 
    |> Seq.exists (fun (env, res) -> res)

let satisfiableWithExample x =
    x 
    |> truthTable 
    |> Seq.tryFind (fun (env, res) -> res)
    |> Option.map fst

let tautology x =
    x 
    |> truthTable 
    |> Seq.forall (fun (env, res) -> res)

let tautologyWithCounterExample x =
    x 
    |> truthTable 
    |> Seq.tryFind (fun (env, res) -> not res)
    |> Option.map fst

let printCounterExample x = 
    match x with
    | None     -> printfn "tautology verified OK"
    | Some env -> printfn "tautology failed on %A" (Seq.toList env)
//val eval : env:Map<Var,bool> -> inp:Prop -> bool
//val support : f:Prop -> Set<Var>
//val cases : supp:'a list -> seq<Map<'a,bool>> when 'a : comparison
//val truthTable : x:Prop -> seq<Map<Var,bool> * bool>
//val satisfiable : x:Prop -> bool
//val satisfiableWithExample : x:Prop -> Map<Var,bool> option
//val tautology : x:Prop -> bool
//val tautologyWithCounterExample : x:Prop -> Map<Var,bool> option
//val printCounterExample : x:#seq<'b> option -> unit

let stringOfBit b = if b then "T" else "F"

let stringOfEnv env =
    Map.fold (fun acc k v -> sprintf "%s=%s;" k (stringOfBit v) + acc) "" env

let stringOfLine (env, res) =
    sprintf "%20s %s" (stringOfEnv env) (stringOfBit res)

let stringOfTruthTable tt =
    "\n" + (tt |> Seq.toList |> List.map stringOfLine |> String.concat "\n")
fsi.AddPrinter(fun tt -> tt |> Seq.truncate 20 |> stringOfTruthTable)
//val stringOfBit : b:bool -> string
//val stringOfEnv : env:Map<string,bool> -> string
//val stringOfLine : env:Map<string,bool> * res:bool -> string
//val stringOfTruthTable : tt:seq<Map<string,bool> * bool> -> string
//val it : unit = ()

satisfiable (var "x")
//val it : bool = true

satisfiableWithExample (var "x")
//val it : Map<Var,bool> option = Some (map [("x", true)])

truthTable (var "x" &&&  var "y")
//val it : seq<Map<Var,bool> * bool> =
//  
//            y=F;x=F; F
//            y=T;x=F; F
//            y=F;x=T; F
//            y=T;x=T; T

tautology (var "x" ||| ~~~(var "x"))
//val it : bool = true

let sumBit x y = (x ^^^ y)
let carryBit x y = (x &&& y)
let halfAdder x y sum carry =
    (sum === sumBit x y)  &&&
    (carry === carryBit x y)

let fullAdder x y z sum carry =
    let xy = (sumBit x y)
    (sum === sumBit xy z) &&&
    (carry === (carryBit x y ||| carryBit xy z))

let twoBitAdder (x1, x2) (y1, y2) (sum1, sum2) carryInner carry =
    halfAdder x1 y1 sum1 carryInner &&&
    fullAdder x2 y2 carryInner sum2 carry
//val sumBit : x:Prop -> y:Prop -> Prop
//val carryBit : x:Prop -> y:Prop -> Prop
//val halfAdder : x:Prop -> y:Prop -> sum:Prop -> carry:Prop -> Prop
//val fullAdder : x:Prop -> y:Prop -> z:Prop -> sum:Prop -> carry:Prop -> Prop
//val twoBitAdder :
//  x1:Prop * x2:Prop ->
//    y1:Prop * y2:Prop ->
//      sum1:Prop * sum2:Prop -> carryInner:Prop -> carry:Prop -> Prop

type bit = Prop
type bitvec = bit[]

let Lo : bit = False
let Hi : bit = True
let vec n nm : bitvec = Array.init n (fun i -> var (sprintf "%s%d" nm i))
let bitEq (b1: bit) (b2: bit) = (b1 <=> b2)
let AndL l = Seq.reduce (fun x y -> And(x, y)) l
let vecEq (v1: bitvec) (v2: bitvec) = AndL (Array.map2 bitEq v1 v2)
//type bit = Prop
//type bitvec = bit []
//val Lo : bit = False
//val Hi : bit = Not False
//val vec : n:int -> nm:string -> bitvec
//val bitEq : b1:bit -> b2:bit -> Prop
//val AndL : l:seq<Prop> -> Prop
//val vecEq : v1:bitvec -> v2:bitvec -> Prop

let fourBitAdder (x: bitvec) (y: bitvec) (sum: bitvec) (carry: bitvec) =
    halfAdder  x.[0] y.[0]           sum.[0] carry.[0] &&&
    fullAdder  x.[1] y.[1] carry.[0] sum.[1] carry.[1] &&&
    fullAdder  x.[2] y.[2] carry.[1] sum.[2] carry.[2] &&&
    fullAdder  x.[3] y.[3] carry.[2] sum.[3] carry.[3]
//val fourBitAdder : x:bitvec -> y:bitvec -> sum:bitvec -> carry:bitvec -> Prop

let Blocks l = AndL l

let nBitCarryRippleAdder (n: int) (x: bitvec) (y: bitvec)
                         (sum: bitvec) (carry: bitvec) =
    Blocks [ for i in 0 .. n-1 ->
                if i = 0
                then halfAdder x.[i] y.[i] sum.[i] carry.[i]
                else fullAdder x.[i] y.[i] carry.[i-1] sum.[i] carry.[i]  ]

let rippleAdder (n: int) (x: bitvec) (y: bitvec)
                (sum: bitvec) (carry: bitvec)  =
    Blocks [ for i in 0 .. n-1 ->
                fullAdder x.[i] y.[i] carry.[i] sum.[i] carry.[i+1] ]
//val Blocks : l:seq<Prop> -> Prop
//val nBitCarryRippleAdder :
//  n:int -> x:bitvec -> y:bitvec -> sum:bitvec -> carry:bitvec -> Prop
//val rippleAdder :
//  n:int -> x:bitvec -> y:bitvec -> sum:bitvec -> carry:bitvec -> Prop

halfAdder (var "x") (var "y") (var "sum") (var "carry")
//val it : Prop =
//  And
//    (Not
//       (And
//          (Not
//             (And
//                (Var "sum",
//                 Not
//                   (Not
//                      (And
//                         (Not (And (Var "x",Var "y")),
//                          Not (And (Not (Var "x"),Not (Var "y")))))))),
//           Not
//             (And
//                (Not (Var "sum"),
//                 Not
//                   (Not
//                      (Not
//                         (And
//                            (Not (And (Var "x",Var "y")),
//                             Not (And (Not (Var "x"),Not (Var "y"))))))))))),
//     Not
//       (And
//          (Not (And (Var "carry",And (Var "x",Var "y"))),
//           Not (And (Not (Var "carry"),Not (And (Var "x",Var "y"))))))))

let twoBitAdderWithHiding (x1, x2) (y1, y2) (sum1, sum2) carry =
    let carryInnerVar = fresh "carry"
    let carryInner = var(carryInnerVar)
    Exists(carryInnerVar, halfAdder x1 y1 sum1 carryInner &&&
                          fullAdder x2 y2 carryInner sum2 carry)
//val twoBitAdderWithHiding :
//  x1:Prop * x2:Prop ->
//    y1:Prop * y2:Prop -> sum1:Prop * sum2:Prop -> carry:Prop -> Prop

tautology (fullAdder Lo Lo Lo Lo Lo)
//val it : bool = true

satisfiable (fullAdder Lo Lo Lo Hi Lo)
//val it : bool = false

tautology (halfAdder (var "x") (var "x") Lo (var "x"))
//val it : bool = true
tautology
    (nBitCarryRippleAdder 2 (vec 2 "x") (vec 2 "y") (vec 2 "sum") (vec 3 "carry") ===
    nBitCarryRippleAdder 2 (vec 2 "y") (vec 2 "x") (vec 2 "sum") (vec 3 "carry"))
//val it : bool = true

open System.Collections.Generic

let memoize f =
    let tab = new Dictionary<_, _>()
    fun x ->
        if tab.ContainsKey(x) then tab.[x]
        else let res = f x in tab.[x] <- res; res

type BddIndex = int
type Bdd = Bdd of BddIndex
type BddNode = Node of Var * BddIndex * BddIndex
type BddBuilder(order: Var -> Var -> int) =

    // The core data structures that preserve uniqueness
    let nodeToIndex = new Dictionary<BddNode, BddIndex>()
    let indexToNode = new Dictionary<BddIndex, BddNode>()

    // Keep track of the next index
    let mutable nextIdx = 2
    let trueIdx = 1
    let falseIdx = -1
    let trueNode = Node("", trueIdx, trueIdx)
    let falseNode = Node("", falseIdx, falseIdx)

    // Map indexes to nodes. Negative indexes go to their negation.
    // The special indexes -1 and 1 go to special true/false nodes.
    let idxToNode(idx) =
        if idx = trueIdx then trueNode
        elif idx = falseIdx then falseNode
        elif idx > 0 then indexToNode.[idx]
        else
            let (Node(v, l, r)) = indexToNode.[-idx]
            Node(v, -l, -r)
    // Map nodes to indexes. Add an entry to the table if needed.
    let nodeToUniqueIdx(node) =
        if nodeToIndex.ContainsKey(node) then nodeToIndex.[node]
        else
            let idx = nextIdx
            nodeToIndex.[node] <- idx
            indexToNode.[idx] <- node
            nextIdx <- nextIdx + 1
            idx

    // Get the canonical index for a node. Preserve the invariant that the
    // left-hand node of a conditional is always a positive node
    let mkNode(v: Var, l: BddIndex, r: BddIndex) =
        if l = r then l
        elif l >= 0 then nodeToUniqueIdx(Node(v, l, r) )
        else -nodeToUniqueIdx(Node(v, -l, -r))

    // Construct the BDD for a conjunction "m1 AND m2"
    let rec mkAnd(m1, m2) =
        if m1 = falseIdx || m2 = falseIdx then falseIdx
        elif m1 = trueIdx then m2
        elif m2 = trueIdx then m1
        else
            let (Node(x, l1, r1)) = idxToNode(m1)
            let (Node(y, l2, r2)) = idxToNode(m2)
            let v, (la, lb), (ra, rb) =
                match order x y with
                | c when c = 0 -> x, (l1, l2), (r1, r2)
                | c when c < 0 -> x, (l1, m2), (r1, m2)
                | c -> y, (m1, l2), (m1, r2)
            mkNode(v, mkAnd(la, lb), mkAnd(ra, rb))

    // Memoize this function
    let mkAnd = memoize mkAnd

    // Publish the construction functions that make BDDs from existing BDDs
    member g.False = Bdd falseIdx
    member g.And(Bdd m1, Bdd m2) = Bdd(mkAnd(m1, m2))
    member g.Not(Bdd m) = Bdd(-m)
    member g.Var(nm) = Bdd(mkNode(nm, trueIdx, falseIdx))
    member g.NodeCount = nextIdx
//val memoize : f:('a -> 'b) -> ('a -> 'b) when 'a : equality
//type BddIndex = int
//type Bdd = | Bdd of BddIndex
//type BddNode = | Node of Var * BddIndex * BddIndex
//type BddBuilder =
//  class
//    new : order:(Var -> Var -> int) -> BddBuilder
//    member And : Bdd * Bdd -> Bdd
//    member Not : Bdd -> Bdd
//    member Var : nm:Var -> Bdd
//    member False : Bdd
//    member NodeCount : int
//  end

type BddBuilder(order: Var -> Var -> int) =

    // The core data structures that preserve uniqueness
    let nodeToIndex = new Dictionary<BddNode, BddIndex>()
    let indexToNode = new Dictionary<BddIndex, BddNode>()

    // Keep track of the next index
    let mutable nextIdx = 2
    let trueIdx = 1
    let falseIdx = -1
    let trueNode = Node("", trueIdx, trueIdx)
    let falseNode = Node("", falseIdx, falseIdx)

    // Map indexes to nodes. Negative indexes go to their negation.
    // The special indexes -1 and 1 go to special true/false nodes.
    let idxToNode(idx) =
        if idx = trueIdx then trueNode
        elif idx = falseIdx then falseNode
        elif idx > 0 then indexToNode.[idx]
        else
            let (Node(v, l, r)) = indexToNode.[-idx]
            Node(v, -l, -r)
    // Map nodes to indexes. Add an entry to the table if needed.
    let nodeToUniqueIdx(node) =
        if nodeToIndex.ContainsKey(node) then nodeToIndex.[node]
        else
            let idx = nextIdx
            nodeToIndex.[node] <- idx
            indexToNode.[idx] <- node
            nextIdx <- nextIdx + 1
            idx

    // Get the canonical index for a node. Preserve the invariant that the
    // left-hand node of a conditional is always a positive node
    let mkNode(v: Var, l: BddIndex, r: BddIndex) =
        if l = r then l
        elif l >= 0 then nodeToUniqueIdx(Node(v, l, r) )
        else -nodeToUniqueIdx(Node(v, -l, -r))

    // Construct the BDD for a conjunction "m1 AND m2"
    let rec mkAnd(m1, m2) =
        if m1 = falseIdx || m2 = falseIdx then falseIdx
        elif m1 = trueIdx then m2
        elif m2 = trueIdx then m1
        else
            let (Node(x, l1, r1)) = idxToNode(m1)
            let (Node(y, l2, r2)) = idxToNode(m2)
            let v, (la, lb), (ra, rb) =
                match order x y with
                | c when c = 0 -> x, (l1, l2), (r1, r2)
                | c when c < 0 -> x, (l1, m2), (r1, m2)
                | c -> y, (m1, l2), (m1, r2)
            mkNode(v, mkAnd(la, lb), mkAnd(ra, rb))

    // Memoize this function
    let mkAnd = memoize mkAnd

    // Publish the construction functions that make BDDs from existing BDDs
    member g.False = Bdd falseIdx
    member g.And(Bdd m1, Bdd m2) = Bdd(mkAnd(m1, m2))
    member g.Not(Bdd m) = Bdd(-m)
    member g.Var(nm) = Bdd(mkNode(nm, trueIdx, falseIdx))
    member g.NodeCount = nextIdx

    member g.ToString(Bdd idx) =
        let rec fmt dep idx =
            if dep > 3 then "..." else
            let (Node(p, l, r)) = idxToNode(idx)
            if p = "" then if l = trueIdx then "T" else "F"
            else sprintf "(%s => %s | %s)" p (fmt (dep+1) l) (fmt (dep+1) r)
        fmt 1 idx

    member g.Build(f) =
        match f with
        | And(x, y) -> g.And(g.Build x, g.Build y)
        | Var(p) -> g.Var(p)
        | Not(x) -> g.Not(g.Build x)
        | False -> g.False
        | Exists(v, p) -> failwith "Exists node"

    member g.Equiv(p1, p2) = g.Build(p1) = g.Build(p2)
//type BddBuilder =
//  class
//    new : order:(Var -> Var -> int) -> BddBuilder
//    member And : Bdd * Bdd -> Bdd
//    member Build : f:Prop -> Bdd
//    member Equiv : p1:Prop * p2:Prop -> bool
//    member Not : Bdd -> Bdd
//    member ToString : Bdd -> string
//    member Var : nm:Var -> Bdd
//    member False : Bdd
//    member NodeCount : int
//  end

let bddBuilder = BddBuilder(compare)
fsi.AddPrinter(fun bdd -> bddBuilder.ToString(bdd))
//val bddBuilder: BddBuilder
//val it: unit = ()

bddBuilder.Build(var "x")
//val it : Bdd = (x => T | F)

bddBuilder.Build(var "x" &&& var "x")
//val it : Bdd = (x => T | F)

bddBuilder.Build(var "x") = bddBuilder.Build(var "x" &&& var "x")
//val it : bool = true

(var "x") = (var "x" &&& var "x")
//val it : bool = false

bddBuilder.Build(var "x" &&& var "y")
//val it : Bdd = (x => (y => T | F) | F)

bddBuilder.Equiv (var "x", var "x" &&& var "x")
//val it : bool = true

bddBuilder.Equiv(
    nBitCarryRippleAdder 8 (vec 8 "x") (vec 8 "y") (vec 8 "sum") (vec 9 "carry"),
    nBitCarryRippleAdder 8 (vec 8 "y") (vec 8 "x") (vec 8 "sum") (vec 9 "carry"))
//val it : bool = true

let mux a b c = ((~~~a ==> b) &&& (a ==> c))

let carrySelectAdder
       totalSize maxBlockSize
       (x: bitvec) (y: bitvec)
       (sumLo: bitvec) (sumHi: bitvec)
       (carryLo: bitvec) (carryHi: bitvec)
       (sum: bitvec) (carry: bitvec) =
  Blocks
    [ for i in 0..maxBlockSize..totalSize-1 ->
        let sz = min (totalSize-i) maxBlockSize
        let j = i+sz-1
        let carryLo = Array.append [| False |] carryLo.[i+1..j+1]
        let adderLo = rippleAdder sz x.[i..j] y.[i..j] sumLo.[i..j] carryLo
        let carryHi = Array.append [| True  |] carryHi.[i+1..j+1]
        let adderHi = rippleAdder sz x.[i..j] y.[i..j] sumHi.[i..j] carryHi
        let carrySelect = (carry.[j+1] === mux carry.[i] carryLo.[sz] carryHi.[sz])
        let sumSelect =
            Blocks
                [ for k in i..j ->
                    sum.[k] === mux carry.[i] sumLo.[k] sumHi.[k] ]
        adderLo &&& adderHi &&& carrySelect &&& sumSelect ]

let checkAdders n k =
    let x = vec n "x"
    let y = vec n "y"
    let sumA    = vec n "sumA"
    let sumB    = vec n "sumB"
    let sumLo   = vec n "sumLo"
    let sumHi   = vec n "sumHi"
    let carryA  = vec (n+1) "carryA"
    let carryB  = vec (n+1) "carryB"
    let carryLo = vec (n+1) "carryLo"
    let carryHi = vec (n+1) "carryHi"
    let adder1 = carrySelectAdder n k x y sumLo sumHi carryLo carryHi sumA carryA
    let adder2 = rippleAdder n x y sumB carryB
    (adder1 &&& adder2 &&& (carryA.[0] === carryB.[0]) ==>
         (vecEq sumA sumB &&& bitEq carryA.[n] carryB.[n]))
//val mux : a:Prop -> b:Prop -> c:Prop -> Prop
//val carrySelectAdder :
//  totalSize:int ->
//    maxBlockSize:int ->
//      x:bitvec ->
//        y:bitvec ->
//          sumLo:bitvec ->
//            sumHi:bitvec ->
//              carryLo:bitvec ->
//                carryHi:bitvec -> sum:bitvec -> carry:bitvec -> Prop
//val checkAdders : n:int -> k:int -> Prop

bddBuilder.Equiv(checkAdders 5 2, True)
//val it : bool = true

let approxCompareOn f x y =
    let c = compare (f x) (f y)
    if c <> 0 then c else compare x y

let bddBuilder2 = BddBuilder(approxCompareOn hash)
bddBuilder2.Equiv(checkAdders 7 2, True)
//val approxCompareOn :
//  f:('a -> 'b) -> x:'a -> y:'a -> int when 'a : comparison and 'b : comparison
//val bddBuilder2 : BddBuilder
//val it : bool = true

open System

type Expr =
    | Var
    | Num of int
    | Sum of Expr * Expr
    | Prod of Expr * Expr

let rec deriv expr =
    match expr with
    | Var           -> Num 1
    | Num _         -> Num 0
    | Sum (e1, e2)  -> Sum (deriv e1, deriv e2)
    | Prod (e1, e2) -> Sum (Prod (e1, deriv e2), Prod (e2, deriv e1))
let e1 = Sum (Num 1, Prod (Num 2, Var))
deriv e1
//type Expr =
//  | Var
//  | Num of int
//  | Sum of Expr * Expr
//  | Prod of Expr * Expr
//val deriv : expr:Expr -> Expr
//val e1 : Expr = Sum (Num 1,Prod (Num 2,Var))
//val it : Expr = Sum (Num 0,Sum (Prod (Num 2,Num 1),Prod (Var,Num 0)))

let precSum = 10
let precProd = 20

let rec stringOfExpr prec expr =
    match expr with
    | Var   -> "x"
    | Num i -> i.ToString()
    | Sum (e1, e2) ->
        let sum = stringOfExpr precSum e1 + "+" + stringOfExpr precSum e2
        if prec > precSum then
            "(" + sum + ")"
        else
            sum
    | Prod (e1, e2) ->
        stringOfExpr precProd e1 + "*" + stringOfExpr precProd e2

fsi.AddPrinter (fun expr -> stringOfExpr 0 expr)
let e3 = Prod (Var, Prod (Var, Num 2))
deriv e3
//val precSum : int = 10
//val precProd : int = 20
//val stringOfExpr : prec:int -> expr:Expr -> string
//val e3 : Expr = x*x*2
//val it : Expr = x*(x*0+2*1)+x*2*1

let simpSum (a, b) = 
    match a, b with 
    | Num n, Num m -> Num (n+m)      // constants!
    | Num 0, e | e, Num 0 -> e       // 0+e = e+0 = e
    | e1, e2 -> Sum(e1, e2)

let simpProd (a, b) = 
    match a, b with 
    | Num n, Num m -> Num (n*m)      // constants!
    | Num 0, e | e, Num 0 -> Num 0   // 0*e=0
    | Num 1, e | e, Num 1 -> e       // 1*e = e*1 = e
    | e1, e2 -> Prod(e1, e2)

let rec simpDeriv e = 
    match e with 
    | Var           -> Num 1
    | Num _         -> Num 0
    | Sum (e1, e2)  -> simpSum (simpDeriv e1, simpDeriv e2)
    | Prod (e1, e2) -> simpSum (simpProd (e1, simpDeriv e2),
                                simpProd (e2, simpDeriv e1))

simpDeriv e3
//val simpSum : a:Expr * b:Expr -> Expr
//val simpProd : a:Expr * b:Expr -> Expr
//val simpDeriv : e:Expr -> Expr
//val it : Expr = x*2+x*2

namespace Symbolic.Expressions

type Expr =
    | Num of decimal
    | Var of string
    | Neg of Expr
    | Add of Expr list
    | Sub of Expr * Expr list
    | Prod of Expr * Expr
    | Frac of Expr * Expr
    | Pow of Expr * decimal
    | Sin of Expr
    | Cos of Expr
    | Exp of Expr

    static member StarNeeded e1 e2 =
        match e1, e2 with
        | Num _, Neg _ | _, Num _ -> true
        | _ -> false

    member self.IsNumber =
        match self with
        | Num _ -> true | _ -> false

    member self.NumOf =
        match self with
        | Num num -> num | _ -> failwith "NumOf: Not a Num"

    member self.IsNegative =
        match self with
        | Num num | Prod (Num num, _) -> num < 0M
        | Neg e -> true | _ -> false

    member self.Negate =
        match self with
        | Num num -> Num (-num)
        | Neg e -> e
        | exp -> Neg exp
//type Expr =
//  | Num of decimal
//  | Var of string
//  | Neg of Expr
//  | Add of Expr list
//  | Sub of Expr * Expr list
//  | Prod of Expr * Expr
//  | Frac of Expr * Expr
//  | Pow of Expr * decimal
//  | Sin of Expr
//  | Cos of Expr
//  | Exp of Expr
//  with
//    member IsNegative : bool
//    member IsNumber : bool
//    member Negate : Expr
//    member NumOf : decimal
//    static member StarNeeded : e1:Expr -> e2:Expr -> bool
//  end

//c:\dev\apress\f-3.0code\12Symbolic\SymbolicDifferentiation>.\packages\FSPowerPack.Community.2.1.3.1\Tools\fsyacc ExprParser.fsy --module ExprParser
//building tables
//computing first function...time: 00:00:00.0843255
//building kernels...time: 00:00:00.0812604
//building kernel table...time: 00:00:00.0360036
//computing lookahead relations................................................time: 00:00:00.0695658
//building lookahead table...time: 00:00:00.0213183
//building action table...time: 00:00:00.0245422
//building goto table...time: 00:00:00.0073612
//returning tables.
//45 states
//5 nonterminals
//17 terminals
//22 productions
//#rows in action table: 45
//
//c:\dev\apress\f-3.0code\12Symbolic\SymbolicDifferentiation>.\packages\FSPowerPack.Community.2.1.3.1\Tools\fslex ExprLexer.fsl --unicode
//compiling to dfas (can take a while...)
//18 states
//writing output
