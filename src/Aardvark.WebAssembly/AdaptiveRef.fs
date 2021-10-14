namespace Aardvark.WebAssembly

open FSharp.Data.Adaptive

type IAdaptiveRef =
    inherit IAdaptiveObject
    abstract member Ref : JsObj
    abstract member Update : AdaptiveToken -> unit
        
type IAdaptiveRef<'a> =
    inherit IAdaptiveRef
    abstract member Value : 'a

type aref<'a> = IAdaptiveRef<'a>

module ARef = 
    type private ConstantRef<'a>(value : 'a) =
        inherit ConstantObject()

        interface aref<'a> with
            member x.Value = value
            member x.Ref = createObj [| "value", js value |]
            member x.Update _ = ()

    type private AdaptiveRef<'a, 'b>(value : aval<'a>, mapping : 'a -> 'b) =
        inherit AdaptiveObject()

        let mutable valid = false
        let ref = createObj [| "value", null |]

        interface aref<'b> with
            member x.Value = 
                if valid then ref.["value"] |> convert<'b>
                else AVal.force value |> mapping

            member x.Ref = ref
            member x.Update token =
                x.EvaluateIfNeeded token () (fun t ->
                    let v = value.GetValue t |> mapping
                    ref.["value"] <- js v
                    valid <- true
                )

    let constant (value : 'a) =
        ConstantRef value :> aref<_>
            
    let ofAVal (value : aval<'a>) =
        AdaptiveRef(value, id) :> aref<_>

    let mapVal (mapping : 'a -> 'b) (value : aval<'a>) =
        AdaptiveRef(value, mapping) :> aref<_>

