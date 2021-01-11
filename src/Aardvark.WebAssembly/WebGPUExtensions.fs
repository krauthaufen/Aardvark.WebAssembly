namespace WebGPU

open System.Threading.Tasks
open WebAssembly
open Aardvark.WebAssembly
open WebAssembly.Core

type Adapter(o : JSObject) =
    inherit JsObj(o)
    member x.RequestDevice() : Async<Device> =
        async {
            let! t = o.Invoke("requestDevice") |> unbox<Task<obj>> |> Async.AwaitTask
            return new Device(convert<DeviceHandle> t)
        }

type GPU(o : JSObject) =
    inherit JsObj(o)

    member x.RequestAdapter() : Async<Adapter> =
        let t = o.Invoke("requestAdapter") |> unbox<Task<obj>>
        async {
            let! o = o.Invoke("requestAdapter") |> unbox<Task<obj>> |> Async.AwaitTask
            return convert<Adapter> o
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

    member x.GetSwapChainPreferredFormat(adapter : Adapter) =
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


[<AutoOpen>]
module WebGPUExtensions =

    type SwapChain with
        member x.GetCurrentTexture() : Texture =
            let o = x.Handle.Reference.Invoke("getCurrentTexture") |> convert<TextureHandle>
            new Texture(x.Device, o)

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
            new ShaderModule(x, handle)

        member x.CreateGLSLShaderModule (stage : ShaderStage, defines : list<string>, code : string) =
            async {
                let! shader = GLSLang.CreateShader(stage, defines, code)

                return x.CreateSpirVShaderModule(shader.SpirV)
            }
        member x.OnUncapturedError
            with set (action : GPUUncapturedErrorEvent -> unit) =
                x.Handle.Reference.SetObjectProperty("onuncapturederror", System.Action<obj>(fun o -> action(convert o)))

    type Queue with 
        member x.OnSubmittedWorkDone() : Async<unit> =
            let f = x.CreateFence { Label = null; InitialValue = 0UL }
            x.Signal(f, 1UL)
            f.Handle.Reference.Invoke("onCompletion", 1) |> convert<System.Threading.Tasks.Task> |> Async.AwaitTask

    type Buffer with
        member x.Map(mode : MapMode, offset : unativeint, size : unativeint) =
            async {
                do! x.MapAsync(mode, offset, size) |> Async.AwaitTask
                return x.GetMappedRange(offset, size)
            }


