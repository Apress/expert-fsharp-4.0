namespace global
open System
type APoint(angle, radius) = 
    member x.Angle = angle
    member x.Radius = radius
    member x.Stretch(l) = APoint(angle = x.Angle, radius = x.Radius * l)
    member x.Warp(f) = APoint(angle = f(x.Angle), radius = x.Radius)
    static member Circle(n) = 
        [for i in 1..n -> APoint(angle = 2.0 * Math.PI / float(n), radius = 1.0)]
    new() = APoint(angle = 0.0, radius = 0.0)

