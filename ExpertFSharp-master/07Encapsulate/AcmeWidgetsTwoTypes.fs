namespace Acme.Widgets
    type Wheel = Square | Round | Triangle
    type Widget = {id : int; wheels : Wheel list; size : string}