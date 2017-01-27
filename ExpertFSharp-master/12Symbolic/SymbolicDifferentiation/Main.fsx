#I "../packages/FsLexYacc.Runtime/lib/net40"
#r "FsLexYacc.Runtime.dll"
#load "Expr.fs" "ExprParser.fs" "ExprLexer.fs" "ExprUtils.fs"

open System
open Symbolic.Expressions
open Microsoft.FSharp.Text.Lexing

let ProcessOneLine text = 
    let lex = LexBuffer<char>.FromString text
    let e1 = ExprParser.expr ExprLexer.main lex
    printfn "After parsing: %A" e1

    let e2 = Utils.simplify e1
    printfn "After simplifying: %A" e2

    let e3 = Utils.diff "x" e2
    printfn "After differentiating: %A" e3

    let e4 = Utils.simplify e3
    printfn "After simplifying: %A" e4

let main () = 
    while true do 
        let text = Console.ReadLine()
        try 
            ProcessOneLine text
        with e -> printfn "Error: %A" e

main()

//--> Added 'C:\...\SymbolicDifferentiation\../packages/FsLexYacc.Runtime/lib/net40' to library include path
//--> Referenced 'C:\...\SymbolicDifferentiation\../packages/FsLexYacc.Runtime/lib/net40\FsLexYacc.Runtime.dll'
//
//[Loading C:\...\SymbolicDifferentiation\Expr.fs
// Loading C:\...\SymbolicDifferentiation\ExprParser.fs
// Loading C:\...\SymbolicDifferentiation\ExprLexer.fs
// Loading C:\...\SymbolicDifferentiation\ExprUtils.fs]
//
//namespace FSI_0004.Symbolic.Expressions
//  type Expr =
//    | Num of decimal
//    | Var of string
//    | Neg of Expr
//    | Add of Expr list
//    | Sub of Expr * Expr list
//    | Prod of Expr * Expr
//    | Frac of Expr * Expr
//    | Pow of Expr * decimal
//    | Sin of Expr
//    | Cos of Expr
//    | Exp of Expr
//    with
//      member IsNegative : bool
//      member IsNumber : bool
//      member Negate : Expr
//      member NumOf : decimal
//      static member StarNeeded : e1:Expr -> e2:Expr -> bool
//    end
//
//
//namespace FSI_0004.Symbolic.Expressions
//  type token =
//    | EOF
//    | LPAREN
//    | RPAREN
//    | PLUS
//    | MINUS
//    | TIMES
//    | DIV
//    | HAT
//    | SIN
//    | COS
//    | E
//    | ID of string
//    | FLOAT of float
//    | INT of int
//  type tokenId =
//    | TOKEN_EOF
//    | TOKEN_LPAREN
//    | TOKEN_RPAREN
//    | TOKEN_PLUS
//    | TOKEN_MINUS
//    | TOKEN_TIMES
//    | TOKEN_DIV
//    | TOKEN_HAT
//    | TOKEN_SIN
//    | TOKEN_COS
//    | TOKEN_E
//    | TOKEN_ID
//    | TOKEN_FLOAT
//    | TOKEN_INT
//    | TOKEN_end_of_input
//    | TOKEN_error
//  type nonTerminalId =
//    | NONTERM__startexpr
//    | NONTERM_expr
//    | NONTERM_number
//    | NONTERM_exp
//    | NONTERM_term
//  val tagOfToken : t:token -> int
//  val tokenTagToTokenId : tokenIdx:int -> tokenId
//  val prodIdxToNonTerminal : prodIdx:int -> nonTerminalId
//  val _fsyacc_endOfInputTag : int
//  val _fsyacc_tagOfErrorTerminal : int
//  val token_to_string : t:token -> string
//  val _fsyacc_dataOfToken : t:token -> Object
//  val _fsyacc_gotos : uint16 []
//  val _fsyacc_sparseGotoTableRowOffsets : uint16 []
//  val _fsyacc_stateToProdIdxsTableElements : uint16 []
//  val _fsyacc_stateToProdIdxsTableRowOffsets : uint16 []
//  val _fsyacc_action_rows : int
//  val _fsyacc_actionTableElements : uint16 []
//  val _fsyacc_actionTableRowOffsets : uint16 []
//  val _fsyacc_reductionSymbolCounts : uint16 []
//  val _fsyacc_productionToNonTerminalTable : uint16 []
//  val _fsyacc_immediateActions : uint16 []
//  val _fsyacc_reductions : unit -> (Text.Parsing.IParseState -> obj) []
//  val tables : unit -> Text.Parsing.Tables<token>
//  val engine :
//    lexer:(LexBuffer<'a> -> token) ->
//      lexbuf:LexBuffer<'a> -> startState:int -> obj
//  val expr : lexer:(LexBuffer<'a> -> token) -> lexbuf:LexBuffer<'a> -> Expr
//
//
//namespace FSI_0004.Symbolic.Expressions
//  val lexeme : arg00:LexBuffer<char> -> string
//  val special : lexbuf:'a -> _arg1:string -> ExprParser.token
//  val id : lexbuf:'a -> _arg1:string -> ExprParser.token
//  val trans : uint16 [] array
//  val actions : uint16 []
//  val _fslex_tables : UnicodeTables
//  val _fslex_dummy : unit -> 'a
//  val main : lexbuf:LexBuffer<char> -> ExprParser.token
//  val _fslex_main :
//    _fslex_state:int -> lexbuf:LexBuffer<char> -> ExprParser.token
//
//
//namespace FSI_0004.Symbolic.Expressions
//  val collectFold :
//    f:('a -> 'b -> 'c list * 'b) -> l:'a list -> s:'b -> 'c list * 'b
//  val collect : e:Expr -> Expr
//  val negate : e:Expr -> Expr
//  val filterNums : e:Expr -> n:decimal -> Expr list * decimal
//  val summands : e:Expr -> Expr list
//  val simp : e:Expr -> Expr
//  val simplify : e:Expr -> Expr
//  val diff : v:string -> e:Expr -> Expr
//
//
//val ProcessOneLine : text:string -> unit
//val main : unit -> unit

//C:\...\12Symbolic>fsi SymbolicDifferentiation\Main.fsx
//x+0
//After parsing: Add [Var "x"; Num 0M]
//After simplifying: Var "x"
//After differentiating: Num 1M
//After simplifying: Num 1M
//x+x
//After parsing: Add [Var "x"; Var "x"]
//After simplifying: Add [Var "x"; Var "x"]
//After differentiating: Add [Num 1M; Num 1M]
//After simplifying: Num 2M
//x+x+x+x
//After parsing: Add [Add [Add [Var "x"; Var "x"]; Var "x"]; Var "x"]
//After simplifying: Add [Var "x"; Var "x"; Var "x"; Var "x"]
//After differentiating: Add [Num 1M; Num 1M; Num 1M; Num 1M]
//After simplifying: Num 4M
//cos(sin(1 / (x^2 + 1)))
//After parsing: Cos (Sin (Frac (Num 1M,Add [Prod (Num 1M,Pow (Var "x",2M)); Num 1M])))
//After simplifying: Cos (Sin (Frac (Num 1M,Add [Pow (Var "x",2M); Num 1M])))
//After differentiating: Neg
//  (Prod
//     (Sin (Sin (Frac (Num 1M,Add [Pow (Var "x",2M); Num 1M]))),
//      Prod
//        (Cos (Frac (Num 1M,Add [Pow (Var "x",2M); Num 1M])),
//         Frac
//           (Sub
//              (Prod (Num 0M,Add [Pow (Var "x",2M); Num 1M]),
//               [Prod
//                  (Num 1M,
//                   Add [Prod (Prod (Num 2M,Pow (Var "x",1M)),Num 1M); Num 0M])])
//,
//            Pow (Add [Pow (Var "x",2M); Num 1M],2M)))))
//After simplifying: Prod
//  (Neg (Sin (Sin (Frac (Num 1M,Add [Pow (Var "x",2M); Num 1M])))),
//   Prod
//     (Cos (Frac (Num 1M,Add [Pow (Var "x",2M); Num 1M])),
//      Frac (Prod (Num -2M,Var "x"),Pow (Add [Pow (Var "x",2M); Num 1M],2M))))