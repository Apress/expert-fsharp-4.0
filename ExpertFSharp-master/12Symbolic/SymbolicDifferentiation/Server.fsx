#I "../packages/FsLexYacc.Runtime/lib/net40"
#I "../packages/Suave/lib/net40"
#r "FsLexYacc.Runtime.dll"
#r "Suave.dll"
#load "Expr.fs" "ExprParser.fs" "ExprLexer.fs" "ExprUtils.fs"

open Symbolic.Expressions
open Symbolic.Expressions.Utils
open Microsoft.FSharp.Text.Lexing

open Suave                 
open Suave.Http
open Suave.Http.Applicatives
open Suave.Http.Successful
open Suave.Web

let parse text = 
    let lex = LexBuffer<char>.FromString text
    ExprParser.expr ExprLexer.main lex 

let toJson x = 
    OK (sprintf """{ "result": "%A" }""" x)
    >>= Writers.setMimeType "application/json"

let webServerSpec () =
  choose
    [ path "/" >>= OK "Welcome to the analyzer" 
      pathScan "/simp/%s" (parse >> simp >> toJson)
      pathScan "/diff/%s" (parse >> diff "x" >> toJson)
      pathScan "/diffsimp/%s" (parse >> diff "x" >> simp >> toJson)
      pathScan "/parse/%s" (parse >> toJson) ] 

startWebServer defaultConfig (webServerSpec())

//--> Added 'C:\...\SymbolicDifferentiation\../packages/FsLexYacc.Runtime/lib/net40' to library include path
//--> Added 'C:\...\SymbolicDifferentiation\../packages/Suave/lib/net40' to library include path
//--> Referenced 'C:\...\SymbolicDifferentiation\../packages/FsLexYacc.Runtime/lib/net40\FsLexYacc.Runtime.dll'
//--> Referenced 'C:\...\SymbolicDifferentiation\../packages/Suave/lib/net40\Suave.dll'
//
//[Loading C:\...\SymbolicDifferentiation\Expr.fs
// Loading C:\...\SymbolicDifferentiation\ExprParser.fs
// Loading C:\...\SymbolicDifferentiation\ExprLexer.fs
// Loading C:\...\SymbolicDifferentiation\ExprUtils.fs]
//
//namespace FSI_0002.Symbolic.Expressions
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
//namespace FSI_0002.Symbolic.Expressions
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
//  val _fsyacc_dataOfToken : t:token -> System.Object
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
//    lexer:(Text.Lexing.LexBuffer<'a> -> token) ->
//      lexbuf:Text.Lexing.LexBuffer<'a> -> startState:int -> obj
//  val expr :
//    lexer:(Text.Lexing.LexBuffer<'a> -> token) ->
//      lexbuf:Text.Lexing.LexBuffer<'a> -> Expr
//
//
//namespace FSI_0002.Symbolic.Expressions
//  val lexeme : arg00:Text.Lexing.LexBuffer<char> -> string
//  val special : lexbuf:'a -> _arg1:string -> ExprParser.token
//  val id : lexbuf:'a -> _arg1:string -> ExprParser.token
//  val trans : uint16 [] array
//  val actions : uint16 []
//  val _fslex_tables : Text.Lexing.UnicodeTables
//  val _fslex_dummy : unit -> 'a
//  val main : lexbuf:Text.Lexing.LexBuffer<char> -> ExprParser.token
//  val _fslex_main :
//    _fslex_state:int -> lexbuf:Text.Lexing.LexBuffer<char> -> ExprParser.token
//
//
//namespace FSI_0002.Symbolic.Expressions
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
//[I] 2015-11-13T19:37:09.0906871Z: listener started in 16.095 ms with binding 127.0.0.1:8083 [Suave.Tcp.tcpIpServer]

//C:\...> curl --write-out \n --url http://localhost:8083/simp/x+0
//{ "result": "Var "x"" }
//C:\...> curl --write-out \n --url http://localhost:8083/diff/x*x
//{ "result": "Add [Prod (Num 1M,Var "x"); Prod (Var "x",Num 1M)]" }
//C:\...> curl --write-out \n --url http://localhost:8083/diff/x+x+x+x
//{ "result": "Add [Add [Add [Num 1M; Num 1M]; Num 1M]; Num 1M]" }
//C:\...> curl --write-out \n --url http://localhost:8083/diffsimp/x+x+x+x
//{ "result": "Num 4M" }