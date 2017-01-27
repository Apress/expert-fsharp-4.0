module MTypesNProperties

open ProviderImplementation.ProvidedTypes
open FSharp.Core.CompilerServices
open System.Reflection

let createType (i, numProps) = 
    let t = ProvidedTypeDefinition("Type"+string i, Some typeof<obj>)
    t.AddMembersDelayed(fun () -> 
        [ for j in 1 .. numProps -> 
            let propName = "StaticProp"+string j
            ProvidedProperty(propName, typeof<string>, 
                IsStatic = true,
                GetterCode = (fun _ -> <@@ "Hello world: " + string j @@>)) ])
    t

[<TypeProvider>]
type ExampleProviderImpl (config : TypeProviderConfig) as this =
    inherit TypeProviderForNamespaces ()

    let ns = "MyCode"
    let asm = Assembly.GetExecutingAssembly()


    let myType = ProvidedTypeDefinition(asm, ns, "MyTypeProvider", Some typeof<obj>)
    let sparams = 
        [ ProvidedStaticParameter("NumTypes", typeof<int>)
          ProvidedStaticParameter("NumProperties", typeof<int>) ]

    do myType.DefineStaticParameters(sparams, (fun typeName args -> 
        let numTypes = unbox<int> args.[0]
        let numProps = unbox<int> args.[1]
        let t = ProvidedTypeDefinition(asm, ns, typeName, Some typeof<obj>)
        t.AddMembersDelayed(fun () -> [ for i in 1 .. numTypes -> createType(i,numProps) ])
        t))

    do
        this.AddNamespace(ns, [myType])

[<assembly:TypeProviderAssembly>]
do ()