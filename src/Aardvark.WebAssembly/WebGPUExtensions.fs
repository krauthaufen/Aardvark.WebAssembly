namespace WebGPU

open System.Threading.Tasks
open WebAssembly
open Aardvark.WebAssembly
open WebAssembly.Core
open Aardvark.Base

type WebGPUAdapter(o : JSObject) =
    inherit JsObj(o)
    member x.RequestDevice() : Async<Device> =
        async {
            let! t = o.Invoke("requestDevice") |> unbox<Task<obj>> |> Async.AwaitTask
            return new Device(convert<DeviceHandle> t)
        }

type GPU(o : JSObject) =
    inherit JsObj(o)

    member x.RequestAdapter() : Async<WebGPUAdapter> =
        let t = o.Invoke("requestAdapter") |> unbox<Task<obj>>
        async {
            let! o = o.Invoke("requestAdapter") |> unbox<Task<obj>> |> Async.AwaitTask
            return convert<WebGPUAdapter> o
        }

type Navigator(o : JSObject) =
    inherit JsObj(o)
    static let instance = Runtime.GetGlobalObject "navigator" |> unbox<JSObject>
    static member GPU : GPU = instance.GetObjectProperty "gpu" |> convert

type GPUValidationError(r : JSObject) =
    inherit JsObj(r)
    member x.Message = r.GetObjectProperty "message" |> convert<string>

type GPUUncapturedErrorEvent(r : JSObject) =
    inherit Event(r)
    member x.Error = r.GetObjectProperty "error" |> convert<GPUValidationError>

type GPUSwapChainDescriptor =
    {
        Device  : Device
        Format  : TextureFormat
        Usage   : TextureUsage
    }

type GPUPresentContext(r : JSObject) =
    inherit JsObj(r)

    member x.GetSwapChainPreferredFormat(adapter : WebGPUAdapter) =
        async {
            let task = 
                try x.Reference.Invoke("getSwapChainPreferredFormat", adapter.Reference) //|> convert<System.Threading.Tasks.Task<obj>>
                with e ->
                    Console.Warn("error" + e.ToString())
                    TextureFormat.BGRA8Unorm.GetValue()

            match task with
            | :? string as str ->
                return str |> TextureFormat.Parse
            | :? Task<obj> as task ->
                let! o = Async.AwaitTask task
                return o |> convert<string> |> TextureFormat.Parse
            | o ->
                Console.Warn("bad result: ", o)
                return TextureFormat.BGRA8Unorm
        }

    member x.ConfigureSwapChain (desc : GPUSwapChainDescriptor) =
        try
            use r = new JSObject()
            r.SetObjectProperty("device", desc.Device.Handle.Reference)
            r.SetObjectProperty("format", desc.Format.GetValue())
            r.SetObjectProperty("usage", int desc.Usage)
            let r = x.Reference.Invoke("configureSwapChain", js r) |> convert<SwapChainHandle>
            new SwapChain(desc.Device, r)

        with e ->
            printfn "%A" e
            Unchecked.defaultof<_>


type TextureRange =
    abstract Texture : Texture
    abstract Aspect : TextureAspect
    abstract BaseLevel : int
    abstract Levels : int
    abstract BaseLayer : int
    abstract Layers : int

type TextureLevel =
    inherit TextureRange
    inherit SubTextureLevel
    abstract Level : int
    abstract LevelSize : V3i

and SubTextureLevel =
    abstract TextureLevel : TextureLevel
    abstract Offset : V3i
    abstract Size : V3i

type TextureLayer =
    inherit TextureRange
    abstract Layer : int

type TextureImage =
    inherit TextureLayer
    inherit TextureLevel
    inherit SubTextureImage

and SubTextureImage =
    inherit SubTextureLevel
    abstract TextureImage : TextureImage

[<AutoOpen>]
module WebGPUExtensions =
    let private subscribeQueueIdle =
        new Function(
            "queue", "action",
            String.concat "\r\n" [
                "queue.onSubmittedWorkDone().then(() => { action(); }).catch(() => { console.error('queue error'); });"
            ]
        )
    type Queue with
        member x.WaitIdle() =
            x.Handle.Call("onSubmittedWorkDone") |> convert<System.Threading.Tasks.Task> |> Async.AwaitTask

        member x.WhenIdle(action : unit -> unit) =
            subscribeQueueIdle.Call(null, js x.Handle, System.Action action) |> ignore

    type SwapChain with
        member x.GetCurrentTexture() : Texture =
            let o = x.Handle.Reference.Invoke("getCurrentTexture") |> convert<TextureHandle>
            new Texture(x.Device, o, Unchecked.defaultof<_>)

    type HTMLCanvasElement with
        member x.GetGPUPresentContext() : GPUPresentContext =
            let o = x.Reference.Invoke("getContext", "gpupresent")
            Console.Log(o)
            GPUPresentContext(convert o)

    type GLSLangShader(spirv : Uint32Array, code : string) =
        member x.Code = code
        member x.SpirV = spirv

    type GLSLang private() =
        static let rx = System.Text.RegularExpressions.Regex @"\#version[ \t]+([^\r\n]*)"

        static let instance = 
            async {
                let win = Runtime.GetGlobalObject "window" |> unbox<JSObject>
                let! glsl = win.GetObjectProperty "glslang" |> convert<System.Threading.Tasks.Task<obj>> |> Async.AwaitTask

                return unbox<JSObject> glsl
            } |> Async.StartAsTask
            
        static member CreateShader(stage : ShaderStage, defines : list<string>, code : string) =
            async {
                let defines =
                    defines
                    |> List.map (sprintf "#define %s")
                    |> String.concat "\n"

                let realCode = 
                    rx.Replace(code, System.Text.RegularExpressions.MatchEvaluator(fun m -> 
                        let version = m.Groups.[1].Value
                        String.concat "\n" [
                            sprintf "#version %s" version
                            defines
                        ]
                    ))


                let stringStage=
                    match stage with
                    | ShaderStage.Vertex -> "vertex"
                    | ShaderStage.Fragment -> "fragment"
                    | _ -> "compute"

                Console.BeginCollapsed(sprintf "compile %s-shader" stringStage)
                Console.Log(realCode)
                Console.End()

                let! instance = Async.AwaitTask instance
                let res = instance.Invoke("compileGLSL", realCode, stringStage)
                return GLSLangShader(unbox<Uint32Array> res, realCode)
            }




    type Device with
    
        member x.CreateSpirVShaderModule (spirv : Uint32Array) =
            use d = new JSObject()
            d.SetObjectProperty("code", spirv)
            let handle = x.Handle.Reference.Invoke("createShaderModule", d) |> convert<ShaderModuleHandle>
            new ShaderModule(x, handle, Unchecked.defaultof<_>)

        member x.CreateGLSLShaderModule (stage : ShaderStage, defines : list<string>, code : string) =
            async {
                let! shader = GLSLang.CreateShader(stage, defines, code)

                return x.CreateSpirVShaderModule(shader.SpirV)
            }
        member x.OnUncapturedError
            with set (action : GPUUncapturedErrorEvent -> unit) =
                x.Handle.Reference.SetObjectProperty("onuncapturederror", System.Action<obj>(fun o -> action(convert o)))

    type ShaderModule with
        member x.GetMessages() =
            async {
                let! messages = x.Handle.Call("compilationInfo") |> convert<System.Threading.Tasks.Task<obj>> |> Async.AwaitTask
                let arr = (unbox<WebAssembly.JSObject> messages).GetObjectProperty("messages") |> convert<JsArray>
                return Array.init arr.Length (fun i ->
                    arr.[i] |> convert<string>
                )
            }   

    //type Queue with 
    //    member x.OnSubmittedWorkDone() : Async<unit> =
    //        let f = x. { Label = null; InitialValue = 0UL }
    //        x.Signal(f, 1UL)
    //        f.Handle.Reference.Invoke("onCompletion", 1) |> convert<System.Threading.Tasks.Task> |> Async.AwaitTask

    type Buffer with
        member x.Map(mode : MapMode, offset : unativeint, size : unativeint) =
            async {
                let! __ = x.Handle.Call("mapAsync", int mode, float offset, float size) |> convert<System.Threading.Tasks.Task> |> Async.AwaitTask
                return x.GetMappedRange(offset, size)
            }


    //type TextureRange(tex : Texture, aspect : TextureAspect, baseLevel : int, levels : int, baseLayer : int, layers : int) =
    //    member x.Texture = tex
    //    member x.Aspect = aspect
    //    member x.BaseLevel = baseLevel
    //    member x.Levels = levels
    //    member x.BaseLayer = baseLayer
    //    member x.Layers = layers

    type TextureDescriptor with
        member x.Layers =
            match x.Dimension with
            | TextureDimension.D3D -> 1
            | _ -> x.Size.DepthOrArrayLayers


    let private getTextureSize (level : int) (t : Texture) =
        let s0 = 
            let e = t.Descriptor.Size
            V3i(e.Width, e.Height, e.DepthOrArrayLayers)

        let d = 1 <<< level

        match t.Descriptor.Dimension with
        | TextureDimension.D1D ->
            V3i(max 1 (s0.X / d), 1, 1)

        | TextureDimension.D2D ->
            V3i(max V2i.II (s0.XY / d), 1)

        | TextureDimension.D3D ->
            max V3i.III (s0 / d)

        | dim ->
            failwithf "bad TextureDimension: %A" dim 

    type Texture with
        member x.GetSlice(aspect : TextureAspect, minLevel : option<int>, maxLevel : option<int>, minLayer : option<int>, maxLayer : option<int>) =
            let minLevel = defaultArg minLevel 0
            let maxLevel = defaultArg maxLevel (x.Descriptor.MipLevelCount - 1)
            let minLayer = defaultArg minLayer 0
            let maxLayer = defaultArg maxLayer (x.Descriptor.Layers - 1)

            let levels = 1+maxLevel-minLevel
            let layers = 1+maxLayer-minLayer
            {
                new TextureRange with
                    member __.Texture = x
                    member __.Aspect = aspect
                    member __.BaseLevel = minLevel
                    member __.Levels = levels
                    member __.BaseLayer = minLayer
                    member __.Layers = layers
            }

        member x.GetSlice(aspect : TextureAspect, level : int, minLayer : option<int>, maxLayer : option<int>) =
            let minLayer = defaultArg minLayer 0
            let maxLayer = defaultArg maxLayer (x.Descriptor.Layers - 1)

            let layers = 1+maxLayer-minLayer
            let s = getTextureSize level x
            {
                new TextureLevel with
                    member __.Texture = x
                    member __.Aspect = aspect
                    member __.BaseLevel = level
                    member __.Levels = 1
                    member __.Level = level
                    member __.BaseLayer = minLayer
                    member __.Layers = layers
                    member __.LevelSize = s
                    member x.TextureLevel = x
                    member __.Offset = V3i.Zero
                    member __.Size = s
            }

        member x.GetSlice(aspect : TextureAspect, minLevel : option<int>, maxLevel : option<int>, layer : int) =
            let minLevel = defaultArg minLevel 0
            let maxLevel = defaultArg maxLevel (x.Descriptor.MipLevelCount - 1)
            let levels = 1+maxLevel-minLevel
            {
                new TextureLayer with
                    member __.Texture = x
                    member __.Aspect = aspect
                    member __.BaseLevel = minLevel
                    member __.Levels = levels
                    member __.BaseLayer = layer
                    member __.Layers = 1
                    member __.Layer = layer
            }

        member x.Item 
            with get(aspect : TextureAspect, level : int, layer : int) =
                let s = getTextureSize level x
                {
                    new TextureImage with
                        member __.Texture = x
                        member __.Aspect = aspect
                        member __.BaseLevel = level
                        member __.Levels = 1
                        member __.BaseLayer = layer
                        member __.Level = level
                        member __.Layers = 1
                        member __.Layer = layer
                        member __.LevelSize = s
                        member x.TextureLevel = x :> _
                        member x.TextureImage = x
                        member __.Offset = V3i.Zero
                        member __.Size = s
                }
        
        member x.Item
            with get(aspect : TextureAspect) =
                let levels = x.Descriptor.MipLevelCount
                let layers = x.Descriptor.Layers
                { new TextureRange with
                    member __.Aspect = aspect
                    member __.Texture = x
                    member __.BaseLevel = 0
                    member __.BaseLayer = 0
                    member __.Levels = levels
                    member __.Layers = layers
                }
                
        member x.Item
            with get(aspect : TextureAspect, level : int) =
                let layers = x.Descriptor.Layers
                let s = getTextureSize level x
                { new TextureLevel with
                    member __.Aspect = aspect
                    member __.Texture = x
                    member __.BaseLevel = level
                    member __.BaseLayer = 0
                    member __.Levels = 1
                    member __.Layers = layers
                    member __.Level = level
                    member __.LevelSize = s
                    member x.TextureLevel = x
                    member __.Offset = V3i.Zero
                    member __.Size = s
                }

    type TextureRange with

        member x.CreateView(dim : TextureViewDimension) =
            x.Texture.CreateView {
                Label = null
                Aspect = x.Aspect
                ArrayLayerCount = x.Layers
                BaseArrayLayer = x.BaseLayer
                BaseMipLevel = x.BaseLevel
                MipLevelCount = x.Levels
                Format = x.Texture.Descriptor.Format
                Dimension = dim
            }

        member x.CreateView() =
            let dim =
                match x.Texture.Descriptor.Dimension with
                | TextureDimension.D1D ->
                    TextureViewDimension.D1D
                | TextureDimension.D2D ->
                    if x.Layers <= 1 then TextureViewDimension.D2D
                    else TextureViewDimension.D2DArray
                | TextureDimension.D3D ->
                    TextureViewDimension.D3D
                | dim ->
                    failwithf "bad TextureDimension: %A" dim
            x.CreateView dim

        member x.GetSlice(minLevel : option<int>, maxLevel : option<int>, minLayer : option<int>, maxLayer : option<int>) =
            let minLevel = defaultArg minLevel 0
            let maxLevel = defaultArg maxLevel (x.Levels - 1)
            let minLayer = defaultArg minLayer 0
            let maxLayer = defaultArg maxLayer (x.Layers - 1)

            let levels = 1+maxLevel-minLevel
            let layers = 1+maxLayer-minLayer

            let tex = x.Texture
            let baseLevel = x.BaseLevel + minLevel
            let baseLayer = x.BaseLayer + minLayer
            let aspect = x.Aspect
            {
                new TextureRange with
                    member __.Texture = tex
                    member __.Aspect = aspect
                    member __.BaseLevel = baseLevel
                    member __.Levels = levels
                    member __.BaseLayer = baseLayer
                    member __.Layers = layers
            }
            
        member x.GetSlice(level : int, minLayer : option<int>, maxLayer : option<int>) =
            let minLayer = defaultArg minLayer 0
            let maxLayer = defaultArg maxLayer (x.Layers - 1)

            let layers = 1+maxLayer-minLayer

            let tex = x.Texture
            let baseLevel = x.BaseLevel + level
            let baseLayer = x.BaseLayer + minLayer
            let aspect = x.Aspect
            let s = getTextureSize baseLevel x.Texture
            {
                new TextureLevel with
                    member __.Texture = tex
                    member __.Aspect = aspect
                    member __.BaseLevel = baseLevel
                    member __.Levels = 1
                    member __.Level = baseLevel
                    member __.BaseLayer = baseLayer
                    member __.Layers = layers
                    member __.LevelSize = s
                    member x.TextureLevel = x
                    member __.Offset = V3i.Zero
                    member __.Size = s
            }
  
        member x.GetSlice(minLevel : option<int>, maxLevel : option<int>, layer : int) =
            let minLevel = defaultArg minLevel 0
            let maxLevel = defaultArg maxLevel (x.Levels - 1)
            let levels = 1+maxLevel-minLevel

            let tex = x.Texture
            let baseLevel = x.BaseLevel + minLevel
            let baseLayer = x.BaseLayer + layer
            let aspect = x.Aspect
            {
                new TextureLayer with
                    member __.Texture = tex
                    member __.Aspect = aspect
                    member __.BaseLevel = baseLevel
                    member __.Levels = levels
                    member __.BaseLayer = baseLayer
                    member __.Layers = 1
                    member __.Layer = baseLayer
            }
            
        member x.Item 
            with get(aspect : TextureAspect, level : int, layer : int) =
                let tex = x.Texture
                let baseLevel = x.BaseLevel + level
                let baseLayer = x.BaseLayer + layer
                let s = getTextureSize baseLevel x.Texture
                {
                    new TextureImage with
                        member __.Texture = tex
                        member __.Aspect = aspect
                        member __.BaseLevel = baseLevel
                        member __.Levels = 1
                        member __.BaseLayer = baseLayer
                        member __.Level = baseLevel
                        member __.Layers = 1
                        member __.Layer = baseLayer
                        member __.LevelSize = s
                        member x.TextureLevel = x :> _
                        member x.TextureImage = x
                        member __.Offset = V3i.Zero
                        member __.Size = s
                }
            
    type TextureLevel with
      
        member x.GetSlice(minLayer : option<int>, maxLayer : option<int>) =
            let minLayer = defaultArg minLayer 0
            let maxLayer = defaultArg maxLayer (x.Layers - 1)

            let layers = 1+maxLayer-minLayer

            let tex = x.Texture
            let baseLevel = x.Level
            let baseLayer = x.BaseLayer + minLayer
            let aspect = x.Aspect
            let s = x.LevelSize
            {
                new TextureLevel with
                    member __.Texture = tex
                    member __.Aspect = aspect
                    member __.BaseLevel = baseLevel
                    member __.Levels = 1
                    member __.Level = baseLevel
                    member __.BaseLayer = baseLayer
                    member __.Layers = layers
                    member __.LevelSize = s
                    member x.TextureLevel = x
                    member __.Offset = V3i.Zero
                    member __.Size = s
            }
  
        member x.Item
            with get(layer : int) =
                let tex = x.Texture
                let baseLevel = x.Level
                let baseLayer = x.BaseLayer + layer
                let aspect = x.Aspect
                let s = x.LevelSize
                {
                    new TextureImage with
                        member __.Texture = tex
                        member __.Aspect = aspect
                        member __.BaseLevel = baseLevel
                        member __.Levels = 1
                        member __.Level = baseLevel
                        member __.BaseLayer = baseLayer
                        member __.Layers = 1
                        member __.Layer = baseLayer
                        member __.LevelSize = s
                        member x.TextureLevel = x :> _
                        member x.TextureImage = x
                        member __.Offset = V3i.Zero
                        member __.Size = s
                }

        member x.Sub(offset : V3i, size : V3i) =
            { new SubTextureLevel with
                member __.TextureLevel = x
                member __.Offset = offset
                member __.Size = size
            }
        member x.Sub(offset : V2i, size : V2i) =
            let o = V3i(offset, 0)
            let s = V3i(size, x.LevelSize.Z)
            { new SubTextureLevel with
                member __.TextureLevel = x
                member __.Offset = o
                member __.Size = s
            }
                
        member x.Sub(offset : int, size : int) =
            let o = V3i(offset, 0, 0)
            let s = V3i(size, x.LevelSize.Y, x.LevelSize.Z)
            { new SubTextureLevel with
                member __.TextureLevel = x
                member __.Offset = o
                member __.Size = s
            }
                

    type TextureLayer with
        member x.GetSlice(minLevel : option<int>, maxLevel : option<int>) =
            let minLevel = defaultArg minLevel 0
            let maxLevel = defaultArg maxLevel (x.Levels - 1)
            let levels = 1+maxLevel-minLevel

            let tex = x.Texture
            let baseLevel = x.BaseLevel + minLevel
            let baseLayer = x.BaseLayer + x.Layer
            let aspect = x.Aspect
            {
                new TextureLayer with
                    member __.Texture = tex
                    member __.Aspect = aspect
                    member __.BaseLevel = baseLevel
                    member __.Levels = levels
                    member __.BaseLayer = baseLayer
                    member __.Layers = 1
                    member __.Layer = baseLayer
            }
            
        member x.Item
            with get(level : int) =
                let tex = x.Texture
                let baseLevel = x.BaseLevel + level
                let baseLayer = x.Layer
                let aspect = x.Aspect
                let s = getTextureSize baseLevel tex
                {
                    new TextureImage with
                        member __.Texture = tex
                        member __.Aspect = aspect
                        member __.BaseLevel = baseLevel
                        member __.Levels = 1
                        member __.Level = baseLevel
                        member __.BaseLayer = baseLayer
                        member __.Layers = 1
                        member __.Layer = baseLayer
                        member __.LevelSize = s
                        member x.TextureLevel = x :> _
                        member x.TextureImage = x
                        member __.Offset = V3i.Zero
                        member __.Size = s
                }
               
               
    type TextureImage  with

        member x.Sub(offset : V3i, size : V3i) =
            { new SubTextureImage with
                member __.TextureLevel = x :> TextureLevel
                member __.TextureImage = x
                member __.Offset = offset
                member __.Size = size
            }
            
        member x.Sub(offset : V2i, size : V2i) =
            let o = V3i(offset, 0)
            let s = V3i(size, x.LevelSize.Z)
            { new SubTextureImage with
                member __.TextureLevel = x :> TextureLevel
                member __.TextureImage = x
                member __.Offset = o
                member __.Size = s
            }
            
            
        member x.Sub(offset : int, size : int) =
            let o = V3i(offset, 0, 0)
            let s = V3i(size, x.LevelSize.Y, x.LevelSize.Z)
            { new SubTextureImage with
                member __.TextureLevel = x :> TextureLevel
                member __.TextureImage = x
                member __.Offset = o
                member __.Size = s
            }