module Clock
type TickTock =
  | Tick
  | Tock
val ticker : x:TickTock -> TickTock

