module Acme.Widgets.WidgetWheels 

type Wheel = Square | Triangle | Round

let wheelCornerCount = 
    dict [(Wheel.Square, 4)
          (Wheel.Triangle, 3)
          (Wheel.Round, 0)] 