#r "System.Collections.Immutable.dll"
#r @"../../packages/build/Newtonsoft.Json/lib/netstandard2.0/Newtonsoft.Json.dll"

open System
open Newtonsoft.Json
open Newtonsoft.Json.Linq
open System.IO

type Parameter =
    {
        name        : string
        typ         : string
        annotation  : option<string>
        def         : option<string>
    }

type Method =
    {
        name    : string
        ret     : option<string>
        args    : list<Parameter>
    }

type Entry =
    | Struct of name : string * extensible : bool * members : list<Parameter>
    | Enum of name : string * flags : bool * values : list<string * int>
    | Object of name : string * methods : list<Method>
    | Native of name : string
    | Callback of name : string * args : list<Parameter>

    member x.Name =
        match x with
        | Struct(name,_,_) -> name
        | Enum(name,_,_) -> name
        | Object(name,_) -> name
        | Native(name) -> name
        | Callback(name,_) -> name

[<AutoOpen>]
module Helpers =
    
    let prop (v : JToken) (name : string) : option<'a> =
        if isNull v then None
        else
            match v with
            | :? JObject as o -> 
                let p = o.Property name
                if isNull p then None
                else 
                    if typeof<JToken>.IsAssignableFrom typeof<'a> then
                        match p.Value :> obj with
                        | :? 'a as a -> Some a
                        | _ -> None
                    else
                        p.Value.ToObject<'a>() |> Some
            | _ ->
                None

    let cleanName (name : string) =
        let c0 = name.[0] 
        let name =
            if c0 >= '0' && c0 <= '9' then "d " + name
            else name

        name.Split([| ' '; '\t' |], StringSplitOptions.RemoveEmptyEntries)
        |> Seq.map (fun str ->
            if str.Length > 0 then 
                str.Substring(0, 1).ToUpper() + str.Substring(1)
            else
                str
        )
        |> String.concat ""
        
    let jsName (name : string) =
        let arr = name.Split([| ' '; '\t' |], StringSplitOptions.RemoveEmptyEntries)
        let b = System.Text.StringBuilder()

        if arr.Length > 0 then
            let mutable last = arr.[0].ToLower()
            b.Append last |> ignore

            for i in 1 .. arr.Length - 1 do
                let v = arr.[i].ToLower()
                let lastIsDigit = 
                    if last.Length > 0 then
                        let c = last.[last.Length - 1]
                        if c >= '0' && c <= '9' then true
                        else false
                    else
                        false
                if lastIsDigit then
                    b.Append(v) |> ignore
                else
                    b.Append "-" |> ignore
                    b.Append v |> ignore
                last <- v


            b.ToString()
        else
            ""
        
    let texFormatName (name : string) =
        name.Split([| ' '; '\t' |], StringSplitOptions.RemoveEmptyEntries)
        |> Seq.map (fun s -> s.ToLower())
        |> String.concat ""

    let otherChars (a : string) (b : string) =
        let a = cleanName a
        let b = cleanName b
        let mutable cnt = 0 
        while cnt < a.Length && cnt < b.Length && a.[cnt] = b.[cnt] do
            cnt <- cnt + 1

        let res = min a.Length b.Length - cnt
        res

module Method =
    let tryParse (v : JToken) =
        match prop v "name" with
        | Some (name : string) ->
            let ret : option<string> = prop v "returns"
            let args =
                match prop v "args" with
                | Some (arr : JArray) ->
                    arr |> Seq.choose (fun a ->
                        match prop a "name", prop a "type" with
                        | Some (name : string), Some(typ : string) ->
                            let defaultValue = 
                                match name.ToLower().Trim() with
                                | "userdata" | "user data" -> Some "0" 
                                | "label" -> Some "null" 
                                | _ -> prop a "default"
                            Some { name = name; typ = typ; annotation = prop a "annotation"; def = defaultValue }
                        | _ ->
                            None
                    )
                | None ->
                    Seq.empty

            Some { name = name; ret = ret; args = Seq.toList args }
        | None ->
            None
            
module Callback =
    let tryParse (v : JToken) =
        match prop v "args" with
        | Some (arr : JArray) ->
            arr |> Seq.toList |> List.choose (fun a ->
                match prop a "name", prop a "type" with
                | Some (name : string), Some(typ : string) ->
                    Some { name = name; typ = typ; annotation = prop a "annotation"; def = prop a "default" }
                | _ ->
                    None
            ) |> Some
        | None ->
            None

module Entry =

    let prop (v : JToken) (name : string) : option<'a> =
        if isNull v then None
        else
            match v with
            | :? JObject as o -> 
                let p = o.Property name
                if isNull p then None
                else p.Value.ToObject<'a>() |> Some
            | _ ->
                None

    let tryParse (o : JProperty) =
        match o.Value with
        | :? JObject as value -> 
            let c = value.Property("category")
            if isNull c then
                None
            else
                let cat = c.Value.ToString().Trim()
                match cat with
                | "object" ->
                    let methods =
                        let p = value.Property("methods")
                        if isNull p then []
                        else
                            match p.Value with
                            | :? JArray as methods ->
                                methods |> Seq.choose Method.tryParse |> Seq.toList
                            | _ ->
                                []

                    
                    let methods =
                        { name = "reference"; ret = None; args = [] } ::
                        { name = "release"; ret = None; args = [] } ::
                        methods

                    Object(o.Name, methods) |> Some

                | "structure" ->
                    let ext = 
                        match prop value "extensible" with
                        | Some (v : string) -> 
                            match v.ToLower().Trim() with
                            | "in" | "out" -> true
                            | _ -> false
                        | None -> false

                    let fields =
                        let p = value.Property("members")
                        if isNull p then []
                        else
                            match p.Value with
                            | :? JArray as members ->
                                members |> Seq.toList |> List.choose (fun m ->  
                                    match prop m "type", prop m "name" with
                                    | Some (typ : string), Some (name : string) ->
                                        let defaultValue = 
                                            match name.ToLower().Trim() with
                                            | "userdata" | "user data" -> Some "0" 
                                            | "label" -> Some "null" 
                                            | _ -> prop m "default"
                                        Some { name = name; typ = typ; annotation = prop m "annotation"; def = defaultValue }
                                    | _ ->
                                        None
                                )
                            | _->
                                []

                    Struct(o.Name, ext, fields) |> Some

                | "enum" | "bitmask" ->
                    let isFlag = cat = "bitmask"

                    let values =
                        let p = value.Property("values")
                        if isNull p then []
                        else
                            match p.Value with
                            | :? JArray as members ->
                                members |> Seq.toList |> List.choose (fun m ->  
                                    match prop m "value", prop m "name" with
                                    | Some (value : int), Some (name : string) ->
                                        Some (name, value)
                                    | _ ->
                                        None
                                )
                            | _->
                                []
                        

                    Enum(o.Name, isFlag, values) |> Some

                | "native" ->   
                    Native( o.Name.Trim()) |> Some

                | "callback" ->
                    match Callback.tryParse value with
                    | Some cb -> Callback(o.Name, cb) |> Some
                    | None -> printfn "bad %s: %s" cat o.Name; None

                | cat ->
                    printfn "bad category: %A" cat
                    None
        | _ ->
            None

let indent (str : string) =
    str.Split([|"\r\n"|], StringSplitOptions.None) |> Array.map (sprintf "    %s") |> String.concat "\r\n"

module rec Ast =
    type Field =
        {
            nam             : string
            fieldType       : Lazy<TypeDef>
            defaultValue    : option<string>
        }

        member x.uniqueName =
            let typeName = userName x.fieldType.Value
            sprintf "%s_%s" x.nam typeName

        member x.Defaultable =
            match x.defaultValue with
            | Some _ -> true
            | None ->
                match x.fieldType.Value with
                | Struct(_,_,fields) | ByRef(Struct(_,_,fields)) ->
                    fields |> List.forall (fun f -> f.Defaultable)
                | _ ->
                    false

    type Method =
        {
            name            : string
            parameters      : list<Field>
            returnType      : Lazy<TypeDef>
        }

    type TypeDef =
        | Unit
        | Object of name : string * methods : list<Method>
        | Struct of name : string * extensible : bool * fields : list<Field>
        | PersistentCallback of name : string * args : list<Field>
        | CompletionCallback of name : string * args : list<Field>
        | Enum of name : string * flags : bool * values : list<string * int * string>
        | Option of TypeDef
        | Array of TypeDef
        | Ptr of TypeDef
        | ByRef of TypeDef
        | NativeInt of signed : bool
        | Int of signed : bool * bits : int 
        | Float of bits : int 
        | Bool
        | String
        | Task of TypeDef

        member x.Flatten =  
            match x with
            | Option t | Array t | Ptr t | ByRef t | Task t -> t
            | _ -> x


        member x.UsedTypes =
            match x with
            | Task t ->
                Seq.singleton t
            | Object(_, methods) -> 
                methods 
                |> Seq.collect (fun m -> m.returnType.Value :: (m.parameters |> List.map (fun p -> p.fieldType.Value)))
                |> Seq.map (fun t -> t.Flatten)
            | Struct(_, _, fields) ->
                fields 
                |> Seq.map (fun p -> p.fieldType.Value)
                |> Seq.map (fun t -> t.Flatten)
            | PersistentCallback(_, args) | CompletionCallback(_, args) ->
                args 
                |> Seq.map (fun p -> p.fieldType.Value)
                |> Seq.map (fun t -> t.Flatten)
            | Option t | Array t | Ptr t | ByRef t ->
                Seq.append (Seq.singleton t) t.UsedTypes
                |> Seq.map (fun t -> t.Flatten)
            | _ ->
                Seq.empty

    let rec nativeName (t : TypeDef) =
        match t with
        | Task Unit -> "System.Threading.Tasks.Task"
        | Task t -> sprintf "System.Threading.Tasks.Task<%s>" (nativeName t)
        | Int _ | NativeInt _ -> "int"
        ////| Int _ | Float _ | NativeInt _ -> "float"
        //| Int(false, 8) -> "uint8"
        //| Int(false, 16) -> "uint16"
        //| Int(false, 32) -> "int"
        //| Int(false, 64) -> "uint64"
        //| Int(true, 8) -> "int8"
        //| Int(true, 16) -> "int16"
        //| Int(true, 32) -> "int32"
        //| Int(true, 64) -> "int64"
        //| Int(s, b) -> failwithf "bad int: %A %A" s b
        | Float(32) -> "float32"
        | Float(64) -> "float"
        //| NativeInt true -> "nativeint"
        //| NativeInt false -> "unativeint"
        | Float _ -> failwith "bad int"
        | String -> "string"
        | Bool -> "bool"
        | Array Unit | Option Unit | Ptr Unit | ByRef Unit ->
            "ArrayBuffer"

        | Array (Int(false, 8)) -> "Uint8Array"
        | Array (Int(false, 16)) -> "Uint16Array"
        | Array (Int(false, 32)) -> "Uint32Array"
        | Array (Int(true, 8)) -> "Int8Array"
        | Array (Int(true, 16)) -> "Int16Array"
        | Array (Int(true, 32)) -> "Int32Array"
        | Array (Float 32) -> "Float32Array"
        | Array (Float 64) -> "Float64Array"

        | Option t ->
            nativeName t

        | Array t | Ptr t | ByRef t ->
            "JSObject"

        | PersistentCallback(name,args) | CompletionCallback(name, args) ->
            sprintf "WGPU%s" name
            //args |> List.map (fun f -> nativeName f.fieldType.Value) |> String.concat ", " |> sprintf "System.Action<%s>"
            //"nativeint"
        //| Function _ ->
        //    failwithf "functions cannot be native"
        | Enum(_, true,_) ->
            "int"
        | Enum(_, false, _) ->
            "obj"
        | Struct(name,_,_) ->
            "DawnRaw.WGPU" + name
        | Object(name,_) ->
            //"JSObject"
            name + "Handle"
        | Unit ->
            "unit"
                
    //let rec externName (t : TypeDef) =
    //    match t with
    //    | Int(false, 8) -> "uint8"
    //    | Int(false, 16) -> "uint16"
    //    | Int(false, 32) -> "int"
    //    | Int(false, 64) -> "uint64"
    //    | Int(true, 8) -> "int8"
    //    | Int(true, 16) -> "int16"
    //    | Int(true, 32) -> "int32"
    //    | Int(true, 64) -> "int64"
    //    | Int(s, b) -> failwithf "bad int: %A %A" s b
    //    | Float(32) -> "float32"
    //    | Float(64) -> "float"
    //    | NativeInt true -> "nativeint"
    //    | NativeInt false -> "unativeint"
    //    | Float _ -> failwith "bad int"
    //    | String -> "string"
    //    | Bool -> "int"

    //    | Array t | Option t | Ptr t | ByRef t ->
    //        sprintf "%s*" (externName t)
    //    | PersistentCallback _ | CompletionCallback _ ->
    //        "nativeint"
    //    //| Function _ ->
    //    //    failwithf "functions cannot be native"
    //    | Enum(name,_,_) ->
    //        name
    //    | Struct(name,_,_) ->
    //        "WGPU" + name
    //    | Object(name,_) ->
    //        name + "Handle"
    //    | Unit ->
    //        "void"

    let rec frontendName (t : TypeDef) =
        match t with
        | Task Unit -> "System.Threading.Tasks.Task"
        | Task t -> sprintf "System.Threading.Tasks.Task<%s>" (frontendName t)
        | Int(false, 8) -> "uint8"
        | Int(false, 16) -> "uint16"
        | Int(false, 32) -> "int"
        | Int(false, 64) -> "uint64"
        | Int(true, 8) -> "int8"
        | Int(true, 16) -> "int16"
        | Int(true, 32) -> "int32"
        | Int(true, 64) -> "int64"
        | Int(s, b) -> failwithf "bad int: %A %A" s b
        | Float(32) -> "float32"
        | Float(64) -> "float"
        | NativeInt true -> "nativeint"
        | NativeInt false -> "unativeint"
        | Float _ -> failwith "bad float"
        | String -> "string"
        | Bool -> "bool"
        | Array (Int(false, 32)) -> "uint32[]"
        | Array (Int(false, 16)) -> "uint16[]"
        | Array (Int(false, 8)) -> "byte[]"
        | Array Unit -> "ArrayBuffer"
        | Array t ->
            sprintf "array<%s>" (frontendName t)
        | Option t ->
            sprintf "option<%s>" (frontendName t)
        | Ptr t ->
            sprintf "nativeptr<%s>" (frontendName t)
        | ByRef t ->
            frontendName t
        | PersistentCallback(name, _) | CompletionCallback(name,_) ->
            name
        | Enum(name,_,_) ->
            name
        | Struct(name,_,_) ->
            name
        | Object(name,_) ->
            name
        | Unit ->
            "unit"
    
    let rec userName (t : TypeDef) =
        match t with
        | Task Unit -> "Task"
        | Task t -> sprintf "Task_%s" (userName t)
        | Int(false, 8) -> "uint8"
        | Int(false, 16) -> "uint16"
        | Int(false, 32) -> "int"
        | Int(false, 64) -> "uint64"
        | Int(true, 8) -> "int8"
        | Int(true, 16) -> "int16"
        | Int(true, 32) -> "int32"
        | Int(true, 64) -> "int64"
        | Int(s, b) -> failwithf "bad int: %A %A" s b
        | Float(32) -> "float32"
        | Float(64) -> "float"
        | NativeInt true -> "nativeint"
        | NativeInt false -> "unativeint"
        | Float _ -> failwith "bad float"
        | String -> "string"
        | Bool -> "bool"
        | Array (Int(false, 32)) -> "uint32Arr"
        | Array (Int(false, 16)) -> "uint16Arr"
        | Array (Int(false, 8)) -> "byteArr"
        | Array Unit -> "ArrayBuffer"
        | Array t ->
            sprintf "%sArr" (userName t)
        | Option t ->
            sprintf "%sOpt" (userName t)
        | Ptr t ->
            sprintf "%sPtr" (userName t)
        | ByRef t ->
            userName t
        | PersistentCallback(name, _) | CompletionCallback(name,_) ->
            name
        | Enum(name,_,_) ->
            name
        | Struct(name,_,_) ->
            name
        | Object(name,_) ->
            name
        | Unit ->
            "unit"
    
    module private TypeDef =
        let cache = System.Collections.Concurrent.ConcurrentDictionary<string, Lazy<TypeDef>>()

        let cleanName (name : string) =
            let c0 = name.[0] 
            let name =
                if c0 >= '0' && c0 <= '9' then "d " + name
                else name

            name.Split([| ' '; '\t' |], StringSplitOptions.RemoveEmptyEntries)
            |> Seq.map (fun str ->
                if str.Length > 0 then 
                    str.Substring(0, 1).ToUpper() + str.Substring(1)
                else
                    str
            )
            |> String.concat ""
            
        let otherChars (a : string) (b : string) =
            let a = cleanName a
            let b = cleanName b
            let mutable cnt = 0 
            while cnt < a.Length && cnt < b.Length && a.[cnt] = b.[cnt] do
                cnt <- cnt + 1

            let res = min a.Length b.Length - cnt

            res



        let rec parameterType (context : Map<string, Entry>) (p : Parameter) =
            let baseType = 
                match Map.tryFind p.typ context with
                | Some entry -> ofEntry context entry
                | None -> failwithf "bad parameter type: %A" p.typ
            if Option.isNone p.annotation then 
                baseType
            else 
                lazy (  
                    match baseType.Value with
                    | Int(false, 8) -> String
                    | Unit -> NativeInt true
                    | t -> Option t
                )

        and private unsafeValueString (typ : TypeDef) (value : string) =
            
            match typ with
            
            | Int(signed, bits) ->

                let inline opt (a : bool, value : 'a) = if a then Some value else None

                let parsers =
                    [
                        System.Numerics.BigInteger.TryParse >> opt
                        
                        fun v -> 
                            if v.StartsWith "0x" then
                                System.Numerics.BigInteger.TryParse(v.Substring 2, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture) |> opt
                            else
                                None
                    ]

                let result = parsers |> List.tryPick (fun f -> f value)

                match result with
                | Some value ->
                    if signed then
                        match bits with
                        | 8 -> sprintf "%dy" (int8 value)
                        | 16 -> sprintf "%ds" (int16 value)
                        | 32 -> sprintf "%d" (int32 value)
                        | 64 -> sprintf "%dL" (int64 value)
                        | _ -> failwithf "bad interger type: %A" typ
                    else
                        match bits with
                        | 8 -> sprintf "%duy" (uint8 value)
                        | 16 -> sprintf "%dus" (uint16 value)
                        | 32 -> sprintf "%d" (int value)
                        | 64 -> sprintf "%dUL" (uint64 value)
                        | _ -> failwithf "bad interger type: %A" typ
                | _ ->
                    failwithf "bad integer value: %A" value
            | NativeInt signed ->
                match System.Int64.TryParse value with
                | (true, value) ->
                    if signed then sprintf "%dn" value
                    else sprintf "%dun" (uint64 value)
                | _ ->
                    failwithf "bad integer value: %A" value
            | Float bits ->
                let value = if value.EndsWith "f" then value.Substring(0, value.Length - 1) else value

                match System.Double.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture) with
                | (true, value) ->
                    match bits with
                    | 32 -> sprintf "%ff" value
                    | 64 -> sprintf "%f" value
                    | _ -> failwithf "bad float type: %A" typ
                | _ ->
                    failwithf "bad float value: %A" value
            | Bool ->
                match System.Boolean.TryParse value with
                | (true, value) ->
                    if value then "true" else "false"
                | _ ->
                    failwithf "bad boolean value: %A" value

            | Enum(_, _, values) ->
                let value = cleanName value
                let existing = values |> List.exists (fun (n,_,_) -> n = value)
                if existing then
                    sprintf "%s.%s" (frontendName typ) value
                else
                    failwithf "unknown enum-value: %s.%s" (frontendName typ) value

            | Object _ ->
                match value with
                | "undefined" | "null" | "" ->
                    "null"
                | _ ->
                    failwithf "bad object value: %A" value

            | Option _ ->
                match value with
                | "undefined" | "null" | "" ->
                    "None"
                | _ ->
                    failwithf "bad object value: %A" value

            | String ->
                match value with
                | "null" -> "null"
                | _ -> sprintf "\"%s\"" value

            | Unit ->
                "()"

            | ByRef _ | Array _ | Ptr _ | Struct _ | PersistentCallback _ | CompletionCallback _ | Task _  ->
                failwithf "cannot print default value for type: %A" typ
                
        and private valueString (typ : TypeDef) (value : string) =
            try unsafeValueString typ value |> Some
            with _ -> None

        and private ofParameters (persistent : bool) (meth : bool) (context : Map<string, Entry>) (pars : list<Parameter>) =
            match pars with
            | [] ->
                []
            | p0 :: pars ->
                let n0 = cleanName p0.name
                match (parameterType context p0).Value with
                | Int(false, 32) ->
                    match pars with
                    | p1 :: pars when Option.isSome p1.annotation && otherChars p0.name p1.name < 4 ->
                        let n1 = cleanName p1.name
                        let typ = 
                            match (parameterType context p1).Value with
                            | Array t -> lazy (Array t)
                            | Option t -> lazy (Array t)
                            | Ptr t -> lazy (Array t)
                            | t -> lazy t

                        let def = p1.def |> Option.bind (valueString typ.Value)

                        { nam = n1; fieldType = typ; defaultValue = def } :: ofParameters persistent meth context pars
                    | _ ->
                        let def = p0.def |> Option.bind (valueString (Int(false, 32)))

                        { nam = n0; fieldType = lazy Int(false, 32); defaultValue = def } :: ofParameters persistent meth context pars
                | Option elementType when meth -> //(n0.EndsWith "Descriptor" || n0 = "CopySize" || (frontendName elementType).EndsWith "CopyView") ->
                    { nam = n0; fieldType = lazy (ByRef elementType); defaultValue = None } :: ofParameters persistent meth context pars
                | PersistentCallback(name, args) ->
                    if persistent then { nam = n0; fieldType = lazy (PersistentCallback(name, args)); defaultValue = None } :: ofParameters persistent meth context pars
                    else { nam = n0; fieldType = lazy (CompletionCallback(name, args)); defaultValue = None } :: ofParameters persistent meth context pars
                | t0 ->
                    let def = p0.def |> Option.bind (valueString t0)
                    { nam = n0; fieldType = lazy t0; defaultValue = def } :: ofParameters persistent meth context pars

        let rec ofEntry (context : Map<string, Entry>) (entry : Entry) = 
            cache.GetOrAdd(entry.Name, fun _ ->
                lazy 
                (
                    match entry with
                    | Entry.Native "uint32_t" -> Int(false, 32)
                    | Entry.Native "int32_t" -> Int(true, 32)
                    | Entry.Native "int16_t" -> Int(true, 16)
                    | Entry.Native "uint16_t" -> Int(false, 16)
                    | Entry.Native "uint8_t" -> Int(false, 8)
                    | Entry.Native "uint64_t" -> Int(false, 64)
                    | Entry.Native "bool" -> Bool
                    | Entry.Native "float" -> Float 32
                    | Entry.Native "double" -> Float 64
                    | Entry.Native "char" -> Int(false, 8)
                    | Entry.Native "size_t" -> NativeInt false
                    | Entry.Native "void" -> Unit
                    | Entry.Native "Promise" -> Task Unit
                    | Entry.Native "void *" | Entry.Native "const void *" | Entry.Native "void const *"-> Array Unit
                    | Entry.Native o ->  failwithf "bad native type: %A" o

                    | Entry.Object("adapter", _) ->
                        Object("Adapter", [])

                    | Entry.Object(name, meths) ->  
                        let objectName = cleanName name

                        let methods =
                            meths |> List.map (fun m ->
                                let returnType =
                                    match m.ret with
                                    | Some retName ->
                                        match Map.tryFind retName context with
                                        | Some retEntry ->
                                            ofEntry context retEntry
                                        | None ->
                                            failwithf "bad return type: %A" retName
                                    | None ->
                                        lazy Unit

                                let parameters = ofParameters (m.name.EndsWith "callback") true context m.args
                                //System.Console.WriteLine(sprintf "%s: %A" m.name (m.name.EndsWith "callback"))
                                {
                                    name = cleanName m.name
                                    parameters = parameters
                                    returnType = returnType
                                }
                            )

                        Object(objectName, methods)

                    | Entry.Enum(name, flags, values) ->
                        let name = cleanName name
                        //if name = "TextureFormat" || name = "TextureDimension" then
                        //    let values = values |> List.map (fun (n, v) -> cleanName n, v, texFormatName n)
                        //    Enum(name, flags, values)
                        //else
                        let values = values |> List.map (fun (n, v) -> cleanName n, v, jsName n)
                        Enum(name, flags, values)
                    | Entry.Struct(name, ext, fields) ->
                        let fields = ofParameters false false context fields
                        Struct(cleanName name, ext, fields)

                    | Entry.Callback(name, args) ->
                        let args = ofParameters false false context args
                        PersistentCallback(cleanName name, args)

                )
            )

        and ofType (context : Map<string, Entry>) (typ : string) (annotation : option<string>) =
            let baseType = 
                match Map.tryFind typ context with
                | Some e -> ofEntry context e
                | None -> failwithf "undefined type: %A" typ
            let isPtr = Option.isSome annotation
            lazy (
                let baseType = baseType.Value
                match baseType with
                | Unit when isPtr -> NativeInt true
                | Int(false, 8) when isPtr -> String
                | _ ->
                    if isPtr then Array baseType
                    else baseType

            )
          
    let typeDefs (context : Map<string, Entry>) =
        context |> Map.toList |> List.choose (fun (_, e) ->
            match e with
            | Native _ -> None
            | _ -> (TypeDef.ofEntry context e).Value |> Some
        )

    let run2() =
        let b = System.Text.StringBuilder()
        let printfn fmt = Printf.kprintf (fun str -> b.AppendLine str |> ignore) fmt

        let text = File.ReadAllText (Path.Combine(__SOURCE_DIRECTORY__, "dawn.json"))
        let root = JObject.Parse text


        let all = root.Properties() |> Seq.choose Entry.tryParse |> Seq.map (fun e -> e.Name, e) |> Map.ofSeq

        let indent (str : string) =
            str.Split([|"\r\n"|], StringSplitOptions.None) |> Array.map (fun l -> "    " + l) |> String.concat "\r\n"

                
        let rec readValue (access : Field -> string) (field : Field) (inner : string) =

            let typ = field.fieldType.Value
            match typ with
            | Struct(n, _, fs) ->
                String.concat "\r\n" [
                    yield sprintf "let _%s = " field.uniqueName
                    yield sprintf "    {"
                    for f in fs do
                        let line = 
                            try
                                let conv = sprintf "            let _%sVal = %s.%s" f.uniqueName field.nam f.nam
                                let r = readValue (fun f -> sprintf "_%sVal" f.uniqueName) f (sprintf "_%s" f.uniqueName) |> indent |> indent |> indent
                                sprintf "        %s =\r\n%s\r\n%s" f.nam conv r
                            with _ ->
                                sprintf "        %s = failwith \"asdsadsad\"" f.nam
                        yield line
                    yield sprintf "    }"
                    yield sprintf "let _%s = Unchecked.defaultof<%s>" field.uniqueName (frontendName typ)
                    yield inner
                ]

            | Array t ->
                let f =  { nam = "item"; fieldType = lazy t; defaultValue = None }
                String.concat "\r\n" [
                    sprintf "let _%s =" field.uniqueName
                    sprintf "    let len = (%s).GetObjectProperty(\"length\") |> convert<int>" (access field)
                    sprintf "    Array.init len (fun i ->"
                    sprintf "        let item = (%s).GetObjectProperty(string i) |> convert<%s>" (access field) (nativeName t)
                    readValue (fun _ -> "item") f (sprintf "_%s" f.uniqueName) |> indent |> indent
                    sprintf "    )"
                    inner
                ]

            | Option t ->
                String.concat "\r\n" [
                    sprintf "let _%s = " field.uniqueName
                    sprintf "    if isNull(%s) then None" (access field)
                    sprintf "    else "
                    let fld = { field with fieldType = lazy t }
                    readValue access fld (sprintf "Some _%s" fld.uniqueName) |> indent |> indent
                    inner
                ]
                
            | Enum _ ->
                String.concat "\r\n" [
                    sprintf "let _%s = %s |> System.Convert.ToInt32 |> unbox<%s>" field.uniqueName (access field) (frontendName field.fieldType.Value)
                    inner
                ]
                
            | Int(s, b) ->
                let conv = 
                    if s then sprintf "ToInt%d" b
                    else sprintf "ToUInt%d" b
                String.concat "\r\n" [
                    sprintf "let _%s = %s |> System.Convert.%s" field.uniqueName (access field) conv
                    inner
                ]
                

            | _ -> 
                String.concat "\r\n" [
                    sprintf "let _%s = %s" field.uniqueName (access field)
                    inner
                ]
            
        let rec pinField (device : string) (access : Field -> string) (field : Field) (inner : string) =
            //let name = field.name
            let typ = field.fieldType.Value
            match typ with
            | Task _ ->
                String.concat "\r\n" [
                    sprintf "let _%s = %s" field.uniqueName (access field)
                    inner
                ]
                
            | String ->
                String.concat "\r\n" [
                    sprintf "let _%s = %s" field.uniqueName (access field)
                    inner
                ]
                
            | Enum(_,false,_) ->
                String.concat "\r\n" [
                    sprintf "let _%s = %s.GetValue()" field.uniqueName (access field)
                    inner
                ]

            | Enum(_,true,_) ->
                String.concat "\r\n" [
                    sprintf "let _%s = int (%s)" field.uniqueName (access field)
                    inner
                ]
                
            | Bool ->
                String.concat "\r\n" [
                    sprintf "let _%s = %s" field.uniqueName (access field)
                    inner
                ]
            | Enum _ | Ptr _ ->
                String.concat "\r\n" [
                    sprintf "let _%s = int(%s)" field.uniqueName (access field)
                    inner
                ]
            | Int _ | NativeInt _ ->
                String.concat "\r\n" [
                    sprintf "let _%s = int (%s)" field.uniqueName (access field)
                    inner
                ]


            | Float _ | Enum _ | Ptr _ ->
                String.concat "\r\n" [
                    sprintf "let _%s = (%s)" field.uniqueName (access field)
                    inner
                ]
                
            | ByRef (Struct (_, ext, fields) as element) ->
                let nativeName = nativeName element
                pinStruct device nativeName (access field) field.uniqueName ext fields [
                    inner
                ]
                //String.concat "\r\n" [
                //    sprintf "%s.Pin (fun _%sValue ->" (access name) name
                //    sprintf "    let _%s = NativePtr.stackalloc 1" name
                //    sprintf "    NativePtr.write _%s _%sValue" name name
                //    //sprintf "    use _%s = fixed [| _%sValue |]" name name
                //    indent inner
                //    sprintf ")"
                //]
            | ByRef (Enum _ | Float _ | Int _ | NativeInt _ | Ptr _) ->
                String.concat "\r\n" [
                    sprintf "let _%s = %s" field.uniqueName (access field)
                    //sprintf "use _%s = fixed [| %s |]" name (access name)
                    inner
                ]
            | ByRef typ ->
                failwithf "unexpected: %A" typ
                
            | Array (Object _ as element) ->
                String.concat "\r\n" [
                    sprintf "let _%sCount = %s.Length" field.uniqueName (access field)
                    sprintf "let _%sArray = newArray _%sCount" field.uniqueName field.uniqueName
                    sprintf "for i in 0 .. _%sCount-1 do" field.uniqueName
                    sprintf "    if isNull %s.[i] then _%sArray.[i] <- null" (access field) field.uniqueName
                    sprintf "    else _%sArray.[i] <- %s.[i].Handle" field.uniqueName (access field)
                    sprintf "let _%s = _%sArray.Reference" field.uniqueName field.uniqueName
                    inner
                ]
                 
            | Array (Enum(_, true, _)) ->
                String.concat "\r\n" [
                    sprintf "let _%sCount = %s.Length" field.uniqueName (access field)
                    sprintf "let _%sArray = new Uint32Array(_%sCount)" field.uniqueName field.uniqueName
                    sprintf "for i in 0 .. _%sCount-1 do" field.uniqueName
                    sprintf "    _%sArray.[i] <- uint32 %s.[i]" field.uniqueName (access field)
                    sprintf "let _%s = _%sArray :> JSObject" field.uniqueName field.uniqueName
                    inner
                ]
                
            | Array (Enum(_, false, _)) ->
                String.concat "\r\n" [
                    sprintf "let _%sCount = %s.Length" field.uniqueName (access field)
                    sprintf "let _%sArray = newArray (_%sCount)" field.uniqueName field.uniqueName
                    sprintf "for i in 0 .. _%sCount-1 do" field.uniqueName
                    sprintf "    _%sArray.[i] <- %s.[i].GetValue()" field.uniqueName (access field)
                    sprintf "let _%s = _%sArray.Reference" field.uniqueName field.uniqueName
                    inner
                ]

            | Array (Struct(_,ext, fields) as element) ->
                let frontendElement = frontendName element
                let nativeElement = nativeName element

                String.concat "\r\n" [
                    sprintf "let _%sCount = if isNull %s then 0 else %s.Length" field.uniqueName (access field) (access field)
                    sprintf "let rec _%sCont (_%sinputs : array<%s>) (_%soutputs : JsArray) (_%si : int) =" field.uniqueName field.uniqueName frontendElement field.uniqueName field.uniqueName
                    sprintf "    if _%si >= _%sCount then" field.uniqueName field.uniqueName
                    sprintf "        let _%s = _%soutputs.Reference" field.uniqueName field.uniqueName
                    sprintf "%s" (indent (indent inner))
                    sprintf "    else"
                    indent (indent (pinStruct device nativeElement (sprintf "_%sinputs.[_%si]" field.uniqueName field.uniqueName) "n" ext fields [
                        sprintf "_%soutputs.[_%si] <- js _n" field.uniqueName field.uniqueName
                        sprintf "_%sCont _%sinputs _%soutputs (_%si + 1)" field.uniqueName field.uniqueName field.uniqueName field.uniqueName
                    ]))
                    //sprintf "        inputs.[i].Pin(fun n -> outputs.[i] <- n; _%sCont inputs outputs (i + 1))" name
                    sprintf "_%sCont %s (if _%sCount > 0 then newArray _%sCount else null) 0" field.uniqueName (access field) field.uniqueName field.uniqueName

                ]

            | Array _ ->
                let typeName = nativeName typ

                String.concat "\r\n" [
                    sprintf "let _%s = if isNull %s then null else %s.op_Implicit(Span(%s))" field.uniqueName (access field) typeName (access field)
                    sprintf "let _%sCount = if isNull %s then 0 else %s.Length" field.uniqueName (access field) (access field)
                    inner
                ]

            | Option (Object _ as element) ->
                let elementName = nativeName element
                String.concat "\r\n" [
                    sprintf "let inline _%sCont _%s =" field.uniqueName field.uniqueName
                    indent inner

                    sprintf "match %s with" (access field)
                    sprintf "| Some o ->"
                    sprintf "    let _%s = o.Handle" field.uniqueName
                    sprintf "    _%sCont _%s" field.uniqueName field.uniqueName
                    sprintf "| _ ->"
                    sprintf "    _%sCont null" field.uniqueName
                ]
                
            | Option (Struct(_, ext, fields) as element) ->
                let nativeName = nativeName element
                String.concat "\r\n" [
                    sprintf "let inline _%sCont _%s = " field.uniqueName field.uniqueName
                    indent inner
                    sprintf "match %s with" (access field)
                    sprintf "| Some v ->"
                    indent (
                        pinStruct device nativeName "v" "n" ext fields [
                            sprintf "_%sCont _n" field.uniqueName
                            
                        ]
                    )
                    //sprintf "    v.Pin(fun n -> "
                    //sprintf "    )" 
                    sprintf "| None -> _%sCont null" field.uniqueName
                ]
                
                
            | Option (Enum _) ->
                String.concat "\r\n" [
                    sprintf "let inline _%sCont _%s =" field.uniqueName field.uniqueName
                    indent inner

                    sprintf "match %s with" (access field)
                    sprintf "| Some o ->"
                    sprintf "    _%sCont(o.GetValue())" field.uniqueName
                    sprintf "| _ ->"
                    sprintf "    _%sCont null" field.uniqueName
                ]
                
            | Option _ ->
                String.concat "\r\n" [
                    sprintf "let inline _%sCont _%s =" field.uniqueName field.uniqueName
                    indent inner

                    sprintf "match %s with" (access field)
                    sprintf "| Some o ->"
                    sprintf "    _%sCont o" field.uniqueName
                    sprintf "| _ ->"
                    sprintf "    _%sCont null" field.uniqueName
                ]

            | PersistentCallback(_, args) ->
                let argDef = args |> List.map (fun a -> sprintf "(%s : %s)" a.nam (nativeName a.fieldType.Value)) |> String.concat " "
                
                //let device = if name = "Device" then "x" else "x.Device"
                let rec readArgs (a : list<Field>) =
                    match a with
                    | [] ->
                        let all = 
                            args |> List.map (fun f -> 
                                match f.fieldType.Value with
                                | Enum(_, true, _) ->
                                    sprintf "unbox<%s>(_%s)" (frontendName f.fieldType.Value) f.uniqueName
                                | Enum(_, false, _) ->
                                    sprintf "%s.Parse(_%s)" (frontendName f.fieldType.Value) f.uniqueName
                                | Object _ -> 
                                    sprintf "new %s(%s, _%s)" (frontendName f.fieldType.Value) device f.uniqueName
                                | NativeInt _ ->
                                    sprintf "nativeint _%s" f.uniqueName
                                | _ -> 
                                    sprintf "_%s" f.uniqueName
                            ) |> String.concat ", "
                        sprintf "%s.Invoke(%s)" field.nam all
                    | a0 :: rest ->
                        readValue (fun f -> f.nam) a0 (readArgs rest)



                String.concat "\r\n" [
                    sprintf "let _%sFunction %s = " field.uniqueName argDef
                    indent (readArgs args)
                    sprintf "let _%sDel = WGPU%s(_%sFunction)" field.uniqueName (frontendName typ) field.uniqueName
                    sprintf "let _%sGC = System.Runtime.InteropServices.GCHandle.Alloc(_%sDel)" field.uniqueName field.uniqueName
                    sprintf "let _%s = _%sDel" field.uniqueName field.uniqueName
                    inner
                ]

            | CompletionCallback(_, args) ->
                let argDef = args |> List.map (fun a -> sprintf "(%s : %s)" a.nam (nativeName a.fieldType.Value)) |> String.concat " "

                let rec readArgs (a : list<Field>) =
                    match a with
                    | [] ->
                        let all = 
                            args |> List.map (fun f -> 
                                match f.fieldType.Value with
                                | Enum(_, true, _) ->
                                    sprintf "unbox<%s>(_%s)" (frontendName f.fieldType.Value) f.uniqueName
                                | Enum(_, false, _) ->
                                    sprintf "%s.Parse(_%s)" (frontendName f.fieldType.Value) f.uniqueName
                                | Object _ -> 
                                    sprintf "new %s(%s, _%s)" (frontendName f.fieldType.Value) device f.uniqueName
                                | NativeInt _ ->
                                    sprintf "nativeint _%s" f.uniqueName
                                | _ -> 
                                    sprintf "_%s" f.uniqueName
                            ) |> String.concat ", "
                        String.concat "\r\n"[
                            sprintf "if _%sGC.IsAllocated then _%sGC.Free()" field.uniqueName field.uniqueName
                            sprintf "%s.Invoke(%s)" field.nam all
                        ]
                    | a0 :: rest ->
                        readValue (fun f -> f.nam) a0 (readArgs rest)



                String.concat "\r\n" [
                    sprintf "let mutable _%sGC = Unchecked.defaultof<System.Runtime.InteropServices.GCHandle>" field.uniqueName
                    sprintf "let _%sFunction %s = " field.uniqueName argDef
                    indent (readArgs args)
                    sprintf "let _%sDel = WGPU%s(_%sFunction)" field.uniqueName (frontendName typ) field.uniqueName
                    sprintf "_%sGC <- System.Runtime.InteropServices.GCHandle.Alloc(_%sDel)" field.uniqueName field.uniqueName
                    sprintf "let _%s = _%sDel" field.uniqueName field.uniqueName
                    inner
                ]

            | Object(_, _meths) ->
                let nativeName = nativeName typ
                String.concat "\r\n" [
                    sprintf "let _%s = (if isNull %s then null else %s.Handle)" field.uniqueName (access field) (access field)
                    inner
                ]
            | Struct(_, ext, fields)  ->
                pinStruct device (nativeName typ) (access field) field.uniqueName ext fields [
                    inner
                ]
                //String.concat "\r\n" [
                //    sprintf "%s.Pin (fun _%s ->" (access name) name
                //    indent inner
                //    sprintf ")"
                //]
            | Unit ->
                inner

        and pinStruct (device : string) (nativeName : string) (access : string) (varName : string) (ext : bool) (fields : list<Field>) (inner : list<string>) =
            let rec pinCode (f : list<Field>) =
                match f with
                | [] ->
                    String.concat "\r\n" [
                        yield sprintf "let _%s = new %s()" varName nativeName
                        for f in fields do
                            //match f.fieldType.Value with
                            //| Array _ -> yield sprintf "_%s.%sCount <- _%sCount" varName f.name f.name
                            //| _ -> ()
                            yield sprintf "_%s.%s <- _%s" varName f.nam f.uniqueName
                        yield sprintf "let _%s = _%s" varName varName
                        yield! inner
                    ]
                | f0 :: rest ->
                    let rest = pinCode rest
                    pinField device (fun f -> sprintf "%s.%s" access f.nam) f0 rest
                
            pinCode fields

        let defs = typeDefs all

        let equalType (a : TypeDef) (b : TypeDef) =
            frontendName a = frontendName b

        let graph =
            defs |> List.map (fun d ->
                let used = d.UsedTypes |> Seq.filter (fun t -> defs |> List.exists (fun ti -> equalType ti t)) |> Seq.toList |> List.distinct

                d, used
            )

        let rec tops (graph : list<TypeDef * list<TypeDef>>) =
            let noDeps, deps = graph |> List.partition (fun (_,ds) -> List.isEmpty ds)
            let noDeps = List.map fst noDeps
            if List.isEmpty deps then
                noDeps
            elif List.isEmpty noDeps then
                failwithf "cycle in [%s]" (graph |> List.map (fun (a,_) -> frontendName a) |> String.concat "; ")
            else
                let filtered = deps |> List.map (fun (a, bs) -> a, bs |> List.filter (fun b -> not (List.exists (equalType b) noDeps)))
                noDeps @ tops filtered

   
        let defs = tops graph
            
        for d in defs do
            match d with
            | Object(_, meths) ->
                for m in meths do
                    match m.returnType.Value with
                    | Object(name, _) ->
                        
                        let rec objects (t : TypeDef) =
                            match t with
                            | Task _ -> []
                            | Object(name,_) -> [name]
                            | Struct(_, _, fields) ->
                                fields |> List.collect (fun f -> objects f.fieldType.Value)
                            | Array t | ByRef t | Ptr t | Option t ->
                                objects t
                            | Unit | String | Enum _ | Bool | Float _ | Int _ | NativeInt _ -> []
                            | CompletionCallback _ | PersistentCallback _ -> []

                        let referenced = m.parameters |> List.collect (fun f -> objects f.fieldType.Value)

                        System.Console.WriteLine(sprintf "CREATOR: %s -> %s" m.name name)
                        for r in referenced do 
                            System.Console.WriteLine("  {0}", r)
                    | _ ->
                        ()
                ()
            | _ ->
                ()



        printfn "namespace rec WebGPU"
        printfn "open System"
        printfn "open System.Threading"
        printfn "open WebAssembly"
        printfn "open WebAssembly.Core"
        printfn "open Aardvark.WebAssembly"
        printfn "#nowarn \"9\""
        printfn "#nowarn \"49\""
        printfn ""


        
        // all handles
        for e in defs do
            match e with
            | Object _ ->
                let nativeName = nativeName e
                printfn "[<AllowNullLiteral>]"
                printfn "type %s(r : JSObject) = " nativeName
                printfn "    inherit JsObj(r)"

            | _ ->
                ()

        // all callbacks  
        for e in defs do
            match e with
            | PersistentCallback(_, args) | CompletionCallback(_, args) ->
                let argTypes = args |> List.map (fun a -> nativeName a.fieldType.Value) |> String.concat " * "
                let selfName = frontendName e
                printfn "type WGPU%s = delegate of %s -> unit" selfName argTypes
            | _ ->
                ()
                
        for e in defs do
            match e with
            | PersistentCallback(_, args) | CompletionCallback(_, args) ->
                let argTypes = args |> List.map (fun a -> frontendName a.fieldType.Value) |> String.concat " * "
                let selfName = frontendName e
                printfn "type %s = delegate of %s -> unit" selfName argTypes
            | _ ->
                ()

        printfn ""
        printfn ""

        // all enums
        for e in defs do
            match e with
            | Enum(name, false, values) ->
                if name = "LoadOp" then
                    printfn "[<RequireQualifiedAccess>]"
                    printfn "type %s = " name
                    for (name, _value, _real) in values do
                        printfn "| %s" name
                    printfn "| Color of Color"

                    printfn "    member internal x.GetValue() ="
                    printfn "        match x with"
                    for (c, _, r) in values do
                        printfn "        | %s.%s -> \"%s\" :> obj" name c r
                    printfn "        | %s.Color c -> (createObj [\"r\", c.R :> obj; \"g\", c.G :> obj; \"b\", c.B :> obj; \"a\", c.A :> obj]).Reference :> obj" name
                    
                elif name = "DepthLoadOp" then
                    printfn "[<RequireQualifiedAccess>]"
                    printfn "type %s = " name
                    for (name, _value, _real) in values do
                        printfn "| %s" name
                    printfn "| Depth of float"

                    printfn "    member internal x.GetValue() ="
                    printfn "        match x with"
                    for (c, _, r) in values do
                        printfn "        | %s.%s -> \"%s\" :> obj" name c r
                    printfn "        | %s.Depth c -> c :> obj" name
                    
                elif name = "StencilLoadOp" then
                    printfn "[<RequireQualifiedAccess>]"
                    printfn "type %s = " name
                    for (name, _value, _real) in values do
                        printfn "| %s" name
                    printfn "| Stencil of int"

                    printfn "    member internal x.GetValue() ="
                    printfn "        match x with"
                    for (c, _, r) in values do
                        printfn "        | %s.%s -> \"%s\" :> obj" name c r
                    printfn "        | %s.Stencil c -> c :> obj" name

                elif name = "BindingResource" then
                    printfn "[<RequireQualifiedAccess>]"
                    printfn "type BindingResource ="
                    printfn "| Sampler of Sampler"
                    printfn "| TextureView of TextureView"
                    printfn "| Buffer of Buffer * uint64 * uint64"
                    printfn "    member internal x.GetValue() = "
                    printfn "        match x with"
                    printfn "        | BindingResource.Sampler s -> s.Handle.Reference :> obj"
                    printfn "        | BindingResource.TextureView s -> s.Handle.Reference :> obj"
                    printfn "        | BindingResource.Buffer(b, off, size) ->"
                    printfn "            let o = new JSObject()"
                    printfn "            o.SetObjectProperty(\"buffer\", b.Handle.Reference)"
                    printfn "            o.SetObjectProperty(\"offset\", int off)"
                    printfn "            o.SetObjectProperty(\"size\", int size)"
                    printfn "            o :> obj"
                    ()

                else
                    
                    printfn "[<RequireQualifiedAccess>]"
                    printfn "type %s = " name

                    let undefinedCase = values |> List.exists (fun (n,_,_) -> n = "Undefined")
                    let values = values |> List.filter (fun (n,_,_) -> n <> "Undefined")


                    for (name, _value, _real) in values do
                        printfn "| %s" name
                    printfn "| Custom of string"
                    if undefinedCase then
                        printfn "| Undefined"

                    printfn "    member internal x.GetValue() ="
                    printfn "        match x with"
                    for (c, _, r) in values do
                        printfn "        | %s.%s -> \"%s\" :> obj" name c r
                    printfn "        | %s.Custom n -> n :> obj" name
                    if undefinedCase then printfn "        | %s.Undefined -> null" name

                    printfn "    static member Parse(obj : obj) ="
                    if undefinedCase then
                        printfn "        if isNull obj then %s.Undefined" name
                        printfn "        else"
                    printfn "            match (string obj).Trim().ToLower() with"
                    for (c, _, r) in values do
                        printfn "            | \"%s\" -> %s.%s" r name c
                    printfn "            | str -> %s.Custom str" name


            | Enum(name, true, values) ->
                printfn "[<Flags>]"
                printfn "type %s = " name
                for (name, value, _real) in values do
                    printfn "| %s = 0x%08X" name value

            | _ ->
                ()



                
        printfn ""
        printfn ""

        printfn "module DawnRaw ="
        // all structs
        for e in defs do
            match e with
            | Struct(name, ext, fields) ->
                printfn "    [<AllowNullLiteral>]"
                printfn "    type WGPU%s(h : JSObject) =" name
                printfn "        inherit JsObj(h)"
                printfn "        new() = WGPU%s(new JSObject())" name
                //if ext then printfn "            val mutable public Next : nativeint"
                for f in fields do
                    let jsName =
                        let n = f.nam
                        match n with
                        | "LoadOp" -> "loadValue"
                        | _ -> 
                            if n.Length > 0 then n.Substring(0, 1).ToLower() + n.Substring(1)
                            else n
                    let typ = f.fieldType.Value
                    let typeName = nativeName typ
                    match typ with
                    | Enum(_, true, _) ->

                        printfn "        member x.%s" f.nam
                        printfn "            with get() : %s = h.GetObjectProperty(\"%s\") |> convert<int>" typeName jsName
                        printfn "            and set (v : %s) = h.SetObjectProperty(\"%s\", v)" typeName jsName
            
                    | Enum(n, false, _) ->
                        //if n = "LoadOp" then
                        printfn "        member x.%s" f.nam
                        printfn "            with get() : obj = h.GetObjectProperty(\"%s\") |> convert<obj>" jsName
                        printfn "            and set (v : obj) = h.SetObjectProperty(\"%s\", v)" jsName
            
                        //else
                        //    printfn "        member x.%s" f.name
                        //    printfn "            with get() : %s = h.GetObjectProperty(\"%s\") |> convert<string>" typeName jsName
                        //    printfn "            and set (v : %s) = h.SetObjectProperty(\"%s\", v)" typeName jsName
            
                    | TypeDef.Struct _ | TypeDef.Object _ | TypeDef.Option _ ->
                        printfn "        member x.%s" f.nam
                        printfn "            with get() : %s = h.GetObjectProperty(\"%s\") |> convert<%s>" typeName jsName typeName
                        printfn "            and set (v : %s) = if not (isNull v) then h.SetObjectProperty(\"%s\", js v)" typeName jsName

                    |  _ ->
                        printfn "        member x.%s" f.nam
                        printfn "            with get() : %s = h.GetObjectProperty(\"%s\") |> convert<%s>" typeName jsName typeName
                        printfn "            and set (v : %s) = h.SetObjectProperty(\"%s\", js v)" typeName jsName

            
            
            | _ ->
                ()

        //// all functions
        //for e in defs do
        //    match e with
        //    | Object(name, meths) ->
        //        for meth in meths do
        //            let functionName = sprintf "wgpu%s%s" name meth.name

        //            let args =
        //                { name = "self"; fieldType = lazy e; defaultValue = None } :: meth.parameters 
        //                |> List.collect (fun f ->  
        //                    let arg = sprintf "%s %s" (externName f.fieldType.Value) f.name
        //                    match f.fieldType.Value with
        //                    | Array _ -> 
        //                        [ sprintf "%s %sCount" (externName (Int(false, 32))) f.name; arg]
        //                    | _ -> 
        //                        [ arg ]
        //                )
        //                |> String.concat ", "

        //            printfn "    [<DllImport(\"dawn\"); SuppressUnmanagedCodeSecurity>]"
        //            printfn "    extern %s %s(%s)" (externName meth.returnType.Value) functionName args
        //    | _ ->
        //        ()

        printfn ""
        printfn ""


        // frontend structs
        for e in defs do
            match e with
            | Struct(name, ext, []) ->
                printfn "type %s(h : JSObject) = " name
                printfn "    inherit JsObj(h)"
                printfn "    member inline internal x.Pin<'a>(callback : %s -> 'a) : 'a = " (nativeName e)
                printfn "        let native = %s()" (nativeName e)
                printfn "        callback native"
                ()
            | Struct(name, ext, fields) ->
                printfn "type %s =" name
                printfn "    {"
                for f in fields do
                    let typ = f.fieldType.Value
                    let typeName = frontendName typ
                    printfn "        %s : %s" f.nam typeName
                printfn "    }"

                
                let defFields, otherFields = fields |> List.partition (fun f -> f.Defaultable)
                if not (List.isEmpty defFields) then
                    let args = 
                        match otherFields with
                        | [] -> 
                            ""
                        | other ->
                            other |> List.map (fun f -> sprintf "%s: %s" f.nam (frontendName f.fieldType.Value)) |> String.concat ", " |> sprintf "(%s)"

                    let argSet = otherFields |> List.map (fun f -> f.nam) |> Set.ofList

                    printfn "    static member Default%s : %s =" args name
                    printfn "        {"
                    for f in fields do
                        let fieldType = f.fieldType.Value |> frontendName
                        match f.defaultValue with
                        | Some v ->
                            printfn "            %s = %s" f.nam v
                        | None ->
                            if Set.contains f.nam argSet then
                                printfn "            %s = %s" f.nam f.nam
                            else
                                printfn "            %s = %s.Default" f.nam fieldType
                    printfn "        }"

                printfn ""

                printfn "    member inline internal x.Pin<'a>(device : Device, callback : %s -> 'a) : 'a = " (nativeName e)
                printfn "        let x = x"
                let rec pinCode (f : list<Field>) =
                    match f with
                    | [] ->
                        String.concat "\r\n" [
                            yield sprintf "let native = %s()" (nativeName e)
                            for f in fields do
                                yield sprintf "native.%s <- _%s" f.nam f.uniqueName
                            yield sprintf "callback native"
                        ]
                    | f0 :: rest ->
                        let rest = pinCode rest
                        pinField "device" (fun f -> sprintf "x.%s" f.nam) f0 rest
                
                let code = pinCode fields
                printfn "%s" (indent (indent code))


            | _ ->
                ()

        printfn ""
        printfn ""
        
        for e in defs do
            match e with
            | Object(name, meths) ->
                let meNative = nativeName e
                let device = if name = "Device" then "x" else "x.Device"
                let ctorArgs = 
                    String.concat ", " [
                        if name <> "Device" then yield "device : Device"
                        yield sprintf "handle : %s" meNative
                    ]

                let ctorArgsWithRefCount = 
                    String.concat ", " [
                        if name <> "Device" then yield "device : Device"
                        yield sprintf "handle : %s" meNative
                        yield sprintf "refCount : ref<int>"
                    ]

                    
                let ctorArgsUseWithRefCount = 
                    String.concat ", " [
                        if name <> "Device" then yield "device"
                        yield sprintf "handle"
                        yield sprintf "refCount"
                    ]

                //let destroy, meths =
                //    meths |> List.partition (fun m -> m.name = "Destroy")

                //let destroy = List.tryHead destroy

                printfn "[<AllowNullLiteral>]"
                printfn "type %s(%s) = " name ctorArgsWithRefCount
                printfn "    let mutable isDisposed = false"
                if name <> "Device" then
                    printfn "    member x.Device = device"

                printfn "    member x.ReferenceCount = !refCount"
                printfn "    member x.Handle : %sHandle = handle" name
                printfn "    member x.IsDisposed = isDisposed"

                printfn "    member private x.Dispose(disposing : bool) ="
                printfn "        if not isDisposed then "
                printfn "            let r = Interlocked.Decrement(&refCount.contents)"
                printfn "            isDisposed <- true"
                //printfn "            if disposing then System.GC.SuppressFinalize x"
                //match destroy with
                //| Some destroy -> 
                //    printfn "            if r = 0 then DawnRaw.wgpu%sDestroy(handle)" name
                //| None ->
                //    ()
                //printfn "            handle.Dispose()"

                printfn "    member x.Dispose() = x.Dispose(true)"
                //printfn "    override x.Finalize() = x.Dispose(false)"
                printfn "    member x.Clone() = "
                printfn "        let mutable o = refCount.contents"
                printfn "        if o = 0 then raise <| System.ObjectDisposedException(\"%s\")" name
                printfn "        let mutable n = Interlocked.CompareExchange(&refCount.contents, o + 1, o)"
                printfn "        while o <> n do"
                printfn "            o <- n"
                printfn "            if o = 0 then raise <| System.ObjectDisposedException(\"%s\")" name
                printfn "            n <- Interlocked.CompareExchange(&refCount.contents, o + 1, o)"
                //printfn "        DawnRaw.wgpu%sReference(handle)" name
                printfn "        new %s(%s)" name ctorArgsUseWithRefCount
                printfn "    interface System.IDisposable with"
                printfn "        member x.Dispose() = x.Dispose()"
                
                let ctorArgUse =
                    String.concat ", " [
                        if name <> "Device" then yield "device"
                        yield sprintf "handle"
                        yield "ref 1"
                    ]
                printfn "    new(%s) = new %s(%s)" ctorArgs name ctorArgUse

                for meth in meths do
                    if meth.name = "GetDefaultQueue" then
                        let ret = meth.returnType.Value |> frontendName
                        printfn "    member x.GetDefaultQueue() : %s = " ret
                        printfn "        let handle = x.Handle.Reference.GetObjectProperty(\"defaultQueue\") |> convert<%sHandle>" ret
                        printfn "        new %s(x, handle)" ret
                    elif meth.name <> "Reference" && meth.name <> "Release" then
                        let overloads (meth : Method) =
                            let rec traverse (a : list<Field>) =
                                match a with
                                | [] -> [[]]
                                | f :: rest ->
                                    if f.Defaultable then
                                        let take = traverse rest |> List.map (fun a -> Choice1Of2 { f with defaultValue = None } :: a)
                                        let restArgs = 
                                            (f :: rest) |> List.map (fun f -> 
                                                match f.defaultValue with
                                                | Some v -> Choice2Of2 (f.nam, v, f.fieldType.Value)
                                                | None -> Choice2Of2 (f.nam, sprintf "%s.Default" (frontendName f.fieldType.Value), f.fieldType.Value)
                                            )
                                        restArgs :: take
                                    else
                                        traverse rest |> List.map (fun fs ->
                                            Choice1Of2 f :: fs
                                        )

                            traverse meth.parameters |> List.map (fun args ->
                                let pars = args |> List.choose (function Choice1Of2 f -> Some f | _ -> None)
                                { meth with parameters = pars }, args
                            )

                        for meth, args in overloads meth do

                            let ret = meth.returnType.Value |> frontendName
                            let argDecl = 
                                meth.parameters |> List.map (fun p ->
                                    sprintf "%s : %s" p.nam (frontendName p.fieldType.Value)
                                ) |> String.concat ", "
                            printfn "    member x.%s(%s) : %s = " meth.name argDecl ret

                            //for p in meth.parameters do
                            //    if p.Defaultable then
                            //        match p.defaultValue with
                            //        | Some value -> printfn "        let %s = defaultArg %s %s" p.name p.name value
                            //        | None -> printfn "        let %s = defaultArg %s %s.Default" p.name p.name (frontendName p.fieldType.Value)


                            //let nativeFunctionName = sprintf "wgpu%s%s" name meth.name

                            let realName = 
                                let n = meth.name
                                if n.Length > 0 then n.Substring(0, 1).ToLower() + n.Substring(1)
                                else n

                            let rec pinCode (args : list<string>) (f : list<Choice<Field, string * string * TypeDef>>) =
                                match f with
                                | [] ->
                                    let retName = frontendName meth.returnType.Value
                                    let wrap = 
                                        match meth.returnType.Value with
                                        | Object _ -> 
                                            if retName = "Device" then sprintf "new %s(convert(%s))" retName
                                            else sprintf "new %s(%s, convert(%s))" retName device
             
                                        | Array Unit ->
                                            sprintf "%s |> unbox<ArrayBuffer>"

                                        | NativeInt _ ->
                                            sprintf "%s |> convert<float> |> nativeint"
                                            

                                        | Bool | Float _ | Int _  ->
                                            sprintf "%s |> convert"

                                        | Unit ->
                                            sprintf "%s |> ignore"

                                        | Task Unit ->
                                            sprintf "%s |> convert<System.Threading.Tasks.Task>"

                                        | Enum(name, _, _) ->
                                            fun s -> sprintf "%s |> System.Convert.ToInt32 |> unbox<%s>" s name

                                        | t ->
                                            failwithf "bad return type: %A" t


                                    let print = 
                                        let args = ((sprintf "\"%s\"" realName :: List.rev args) |> String.concat ", ")
                                        String.concat "\r\n" [
                                            sprintf "let window = Runtime.GetGlobalObject(\"window\") |> unbox<JSObject>"
                                            sprintf "let console = window.GetObjectProperty(\"console\") |> unbox<JSObject>"

                                            sprintf "console.Invoke(\"debug\", %s) |> ignore" args
                                        ]

                                    String.concat "\r\n" [
                                        sprintf "x.Handle.Reference.Invoke(%s)" ((sprintf "\"%s\"" realName :: List.rev args) |> String.concat ", ") |> wrap
                                        
                                        //print
                                        //sprintf "try"
                                        //sprintf "x.Handle.Reference.Invoke(%s)" ((sprintf "\"%s\"" realName :: List.rev args) |> String.concat ", ") |> wrap |> sprintf "    %s"
                                        //sprintf "with e ->"
                                        //sprintf "    console.Invoke(\"error\", string e) |> ignore"
                                        //sprintf "    Unchecked.defaultof<_>"
                                        //sprintf "DawnRaw.%s(%s)" nativeFunctionName ("x.Handle" :: (List.rev args) |> String.concat ", ") |> wrap
                                    ]

                        

                                | f0 :: rest ->
                                    match f0 with
                                    | Choice1Of2 f0 -> 
                                        let rest = pinCode (sprintf "js _%s" f0.uniqueName :: args) rest
                                        pinField device (fun f -> f.nam) f0 rest
                                    | Choice2Of2 ((name, value, typ)) ->
                                        pinCode args rest
                                        //pinField device (fun _ -> value) { name = name; fieldType = lazy typ; defaultValue = None } rest
                
                            let code = pinCode [] args
                            printfn "%s" (indent (indent code))


            | _ ->
                ()

        let output = Path.Combine(__SOURCE_DIRECTORY__, "WebGPU.fs")
        File.WriteAllText(output, b.ToString())

Ast.run2()