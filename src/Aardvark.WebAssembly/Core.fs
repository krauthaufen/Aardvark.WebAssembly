namespace rec Aardvark.WebAssembly

open System
open WebAssembly

type JsValueAttribute(name : string) =
    inherit Attribute()
    member x.Name = name

[<AutoOpen>]
module internal Interop =
    open System.Reflection
    open System.Reflection.Emit

    let js (o : obj) =
        match o with
        | null -> null
        | :? JsObj as o -> o.Reference :> obj
        | :? nativeint as o -> int o :> obj
        | :? unativeint as o -> uint32 o :> obj
        | _ -> o

    let net (o : obj) =
        match o with
        | null -> null
        | :? JSObject as o -> JsObj o :> obj
        | _ -> o
    
    let createObj (props : seq<string * obj>) =
        let o = new JSObject()
        for (k, v) in props do
            o.SetObjectProperty(k, js v)
        JsObj o

    type private Converter<'a> private() =
        static let create : obj -> 'a =
            let rt = typeof<'a>

            if rt.IsArray then
                //let et = rt.GetElementType()

                //let ct = typedefof<Converter<_>>.MakeGenericType [| et |]
                //let m = ct.GetMethod("Convert", BindingFlags.NonPublic ||| BindingFlags.Public ||| BindingFlags.Static, Type.DefaultBinder, [| typeof<obj> |], null)
                
                //fun (o : obj) ->
                //    match o with
                //    | :? JSObject as o ->
                //        match o.GetObjectProperty "length" with
                //        | :? int as l
                //            let arr = System.Array.CreateInstance(et, l)

                //            for i in 0 .. arr.Length - 1 do

                //        | _ ->
                //            failwith ""


                failwith "arrays not implemented"
            elif typeof<JsObj>.IsAssignableFrom rt then
                let ctor = typeof<'a>.GetConstructor(BindingFlags.NonPublic ||| BindingFlags.Public ||| BindingFlags.Static ||| BindingFlags.Instance, Type.DefaultBinder, [| typeof<JSObject> |], null)
                if isNull ctor then
                    failwithf "cannot create object of type %A" rt

                let m = DynamicMethod("create" + rt.Name, rt, [| typeof<JSObject> |], true)
                let il = m.GetILGenerator()
            
                il.Emit(OpCodes.Ldarg_0)
                il.Emit(OpCodes.Newobj, ctor)
                il.Emit(OpCodes.Ret)


                let d = m.CreateDelegate(typeof<Func<JSObject, 'a>>) |> unbox<Func<JSObject, 'a>>
                fun (o : obj) ->
                    match o with
                    | null -> Unchecked.defaultof<'a>
                    | :? JsObj as o -> d.Invoke(o.Reference)
                    | :? JSObject as o -> d.Invoke(o)
                    | o -> failwithf "cannot create object from %A" o
                
            elif rt = typeof<uint8> then
                (fun (v : obj) -> System.Convert.ToByte(v)) |> unbox<obj -> 'a>
                
            elif rt = typeof<int8> then
                (fun (v : obj) -> System.Convert.ToSByte(v)) |> unbox<obj -> 'a>
                
            elif rt = typeof<uint16> then
                (fun (v : obj) -> System.Convert.ToUInt16(v)) |> unbox<obj -> 'a>
                
            elif rt = typeof<int16> then
                (fun (v : obj) -> System.Convert.ToInt16(v)) |> unbox<obj -> 'a>
                
            elif rt = typeof<uint32> then
                (fun (v : obj) -> System.Convert.ToUInt32(v)) |> unbox<obj -> 'a>
                
            elif rt = typeof<int32> then
                (fun (v : obj) -> System.Convert.ToInt32(v)) |> unbox<obj -> 'a>
                
            elif rt = typeof<uint64> then
                (fun (v : obj) -> System.Convert.ToUInt64(v)) |> unbox<obj -> 'a>
                
            elif rt = typeof<int64> then
                (fun (v : obj) -> System.Convert.ToInt64(v)) |> unbox<obj -> 'a>
                
            elif rt = typeof<float32> then
                (fun (v : obj) -> System.Convert.ToSingle(v)) |> unbox<obj -> 'a>

            elif rt = typeof<float> then
                (fun (v : obj) -> System.Convert.ToDouble(v)) |> unbox<obj -> 'a>
                
            elif rt = typeof<decimal> then
                (fun (v : obj) -> System.Convert.ToDecimal(v)) |> unbox<obj -> 'a>
                
            elif rt = typeof<bool> then
                (fun (v : obj) -> System.Convert.ToBoolean(v)) |> unbox<obj -> 'a>
                
            elif rt = typeof<char> then
                (fun (v : obj) -> System.Convert.ToChar(v)) |> unbox<obj -> 'a>
                
            elif rt = typeof<string> then
                (fun (v : obj) -> System.Convert.ToString(v)) |> unbox<obj -> 'a>

            else
                unbox

        static member Converter(o : obj) : 'a = create o


    let convert<'a> (o : obj) =
        Converter<'a>.Converter o

    let newObj (typ : string) (args : obj[]) =
        let ctor = Runtime.GetGlobalObject(typ)
        match ctor with
        | :? JSObject as ctor ->
            Runtime.NewJSObject(ctor, Array.map js args) |> JsObj
        | _ ->
            failwithf "could not get constructor %A" typ
            
    let newArray (length : int) =
        let ctor = Runtime.GetGlobalObject("Array")
        match ctor with
        | :? JSObject as ctor ->
            Runtime.NewJSObject(ctor, [| length :> obj |]) |> JsArray
        | _ ->
            failwithf "could not get constructor Array"

[<AllowNullLiteral>]
type JsObj(r : JSObject) =
    member x.Reference = r
    
    member x.Call(meth : string, a0 : 'a) =
        r.Invoke(meth, js a0) |> net
        
    member x.Call(meth : string, a0 : 'a, a1 : 'b) =
        r.Invoke(meth, js a0, js a1) |> net

    member x.Call(meth : string, a0 : 'a, a1 : 'b, a2 : 'c) =
        r.Invoke(meth, js a0, js a1, js a2) |> net

    member x.Call(meth : string, a0 : 'a, a1 : 'b, a2 : 'c, a3 : 'd) =
        r.Invoke(meth, js a0, js a1, js a2, js a3) |> net

    member x.Item
        with get(name : string) = r.GetObjectProperty(name) |> net
        and set (name : string) (value : obj) = r.SetObjectProperty(name, js value)

    member x.Cast<'a when 'a :> JsObj>() =
        convert<'a> x

    new() = JsObj(new JSObject())

    new(props : seq<string * obj>) =
        let o = createObj props
        JsObj(o.Reference)

    new(typ : string, args : obj[]) =
        let o = newObj typ args
        JsObj(o.Reference)

    override x.ToString() =
        use p = r.GetObjectProperty("__proto__") |> unbox<JSObject>
        use c = p.GetObjectProperty("constructor") |> unbox<JSObject>
        let n = c.GetObjectProperty("name") |> unbox<string>
        n

[<AllowNullLiteral>]
type JsArray(r : JSObject) =
    inherit JsObj(r)

    member x.Length = r.GetObjectProperty "length" |> convert<int>
    member x.Push(o : obj) = r.Invoke("push", [| js o |]) |> ignore

    member x.Item
        with get(i : int) = r.GetObjectProperty(string i) |> net
        and set(i : int) (value : obj) = r.SetObjectProperty(string i, js value)


type Iterator<'a>(getIterator : unit -> JSObject, extract : obj -> 'a) =
    let mutable iterator : JSObject = null
    let mutable current : JSObject = null

    member x.MoveNext() = 
        if isNull iterator then
            iterator <- getIterator()

        if not (isNull current) then
            current.Dispose()

        let o = iterator.Invoke("next") |> unbox<JSObject>
        let d = o.GetObjectProperty("done") |> convert<bool>
        if d then
            current <- null
            false
        else
            current <- o
            true

    member x.Current : 'a =
        extract (current.GetObjectProperty "value")
            
    interface System.Collections.IEnumerator with
        member x.Reset() = 
            if not (isNull iterator) then
                iterator.Dispose()
                iterator <- null
            if not (isNull current) then
                current.Dispose()
                current <- null

        member x.MoveNext() = x.MoveNext()
        member x.Current = x.Current :> obj

    interface System.Collections.Generic.IEnumerator<'a> with
        member x.Dispose() = 
            if not (isNull iterator) then
                iterator.Dispose()
                iterator <- null
            if not (isNull current) then
                current.Dispose()
                current <- null
        member x.Current = x.Current

    new(getIterator : unit -> JSObject) = new Iterator<'a>(getIterator, convert)