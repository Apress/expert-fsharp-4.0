namespace Acme.Collections
    type SparseVector =
        new : unit -> SparseVector
        member Add : int * float -> unit
        member Item : int -> float with get


