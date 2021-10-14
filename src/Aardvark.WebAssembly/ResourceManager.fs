namespace Aardvark.WebAssembly

open FSharp.Data.Adaptive
open System.Runtime.CompilerServices
open Aardvark.Base
open Aardvark.Rendering
open WebAssembly.Core
open WebGPU
open System.Runtime.InteropServices
open Microsoft.FSharp.NativeInterop

#nowarn "9"

[<AutoOpen>]
module private Caches =

    type private ConstantTester<'a> private() =
        static let isConstant =
            let aval = typeof<'a>.GetInterface typedefof<IAdaptiveValue>.FullName
            if isNull aval then fun _ -> false
            else fun (v : 'a) ->
                let v = v :> obj :?> IAdaptiveValue
                v.IsConstant

        static member IsConstant(value : 'a) =
            isConstant value


    type Cache<'a, 'b when 'a : not struct and 'b : not struct>() =
        let table = new ConditionalWeakTable<'a, 'b>()
        let constants = Dict<'a, 'b>()

        member x.GetOrCreate(input : 'a, creator : 'a -> 'b) =  
            if ConstantTester.IsConstant input then
                constants.GetOrCreate(input, System.Func<_,_>(creator))
            else
                let result = 
                    lock table (fun () ->
                        match table.TryGetValue(input) with
                        | (true, res) -> ValueSome res
                        | _ -> ValueNone
                    )
                match result with
                | ValueSome res -> res
                | ValueNone ->
                    let res = creator input
                    lock table (fun () ->
                        match table.TryGetValue input with
                        | (true, otherResult) ->
                            // another thread created result
                            otherResult
                        | _ ->
                            table.Add(input, res)
                            res

                    )

        member x.Remove(input : 'a) =
            if ConstantTester.IsConstant input then
                constants.Remove input |> ignore
            else
                lock table (fun () ->
                    table.Remove input |> ignore
                )
                
    type CacheV<'a, 'b, 'c when 'a : not struct>() =
        let table = new ConditionalWeakTable<'a, Dict<'b, 'c>>()
        let constants = Dict<struct('a * 'b), 'c>()

        member x.GetOrCreate(a : 'a, b : 'b, creator : 'a -> 'b -> 'c) =  
            if ConstantTester.IsConstant a then
                constants.GetOrCreate(struct(a,b), fun struct(a,b) -> creator a b)
            else
                let result = 
                    lock table (fun () ->
                        match table.TryGetValue a with
                        | (true, tb) -> 
                            match tb.TryGetValue b with
                            | (true, res) -> ValueSome res
                            | _ -> ValueNone
                        | _ -> 
                            ValueNone
                    )
                match result with
                | ValueSome res -> res
                | ValueNone ->
                    let res = creator a b
                    lock table (fun () ->
                        match table.TryGetValue a with
                        | (true, tb) ->
                            match tb.TryGetValue b with
                            | (true, otherResult) ->
                                // another thread created result
                                otherResult
                            | _ ->
                                tb.Add(b, res)
                                res
                            
                        | _ ->
                            let tb = new Dict<'b, 'c>()
                            table.Add(a, tb)
                            tb.Add(b, res)
                            res

                    )

        member x.Remove(a : 'a, b : 'b) =
            if ConstantTester.IsConstant a then
                constants.Remove struct(a,b) |> ignore
            else
                lock table (fun () ->
                    match table.TryGetValue a with
                    | (true, tb) ->
                        tb.Remove b |> ignore
                        if tb.Count = 0 then table.Remove a |> ignore
                    | _ ->
                        ()
                )

    type Cache<'a, 'b, 'c when 'a : not struct and 'b : not struct and 'c : not struct>() =
        let table = new ConditionalWeakTable<'a, ConditionalWeakTable<'b, 'c>>()

        member x.GetOrCreate(a : 'a, b : 'b, creator : 'a -> 'b -> 'c) =  
            let result = 
                lock table (fun () ->
                    match table.TryGetValue a with
                    | (true, tb) -> 
                        match tb.TryGetValue b with
                        | (true, res) -> ValueSome res
                        | _ -> ValueNone
                    | _ -> 
                        ValueNone
                )
            match result with
            | ValueSome res -> res
            | ValueNone ->
                let res = creator a b
                lock table (fun () ->
                    match table.TryGetValue a with
                    | (true, tb) ->
                        match tb.TryGetValue b with
                        | (true, otherResult) ->
                            // another thread created result
                            otherResult
                        | _ ->
                            tb.Add(b, res)
                            res
                            
                    | _ ->
                        let tb = new ConditionalWeakTable<'b, 'c>()
                        table.Add(a, tb)
                        tb.Add(b, res)
                        res

                )

        member x.Remove(a : 'a, b : 'b) =
            lock table (fun () ->
                match table.TryGetValue a with
                | (true, tb) ->
                    tb.Remove b |> ignore
                | _ ->
                    ()
            ) 

    type Cache<'a, 'b, 'c, 'd when 'a : not struct and 'b : not struct and 'c : not struct and 'd : not struct>() =
        let table = new ConditionalWeakTable<'a, ConditionalWeakTable<'b, ConditionalWeakTable<'c, 'd>>>()

        member x.GetOrCreate(a : 'a, b : 'b, c : 'c, creator : 'a -> 'b -> 'c -> 'd) =  
            let result = 
                lock table (fun () ->
                    match table.TryGetValue a with
                    | (true, tb) -> 
                        match tb.TryGetValue b with
                        | (true, tc) ->     
                            match tc.TryGetValue c with
                            | (true, res) -> ValueSome res
                            | _ -> ValueNone
                        | _ -> ValueNone
                    | _ -> 
                        ValueNone
                )
            match result with
            | ValueSome res -> res
            | ValueNone ->
                let res = creator a b c
                match table.TryGetValue a with
                | (true, tb) ->
                    match tb.TryGetValue b with
                    | (true, tc) ->
                        tc.Add(c, res)
                    | _ ->
                        let tc = ConditionalWeakTable()
                        tc.Add(c, res)
                        tb.Add(b, tc)
                            
                | _ ->
                    let tb = new ConditionalWeakTable<'b, _>()
                    let tc = new ConditionalWeakTable<'c, 'd>()
                    tc.Add(c, res)
                    tb.Add(b, tc)
                    table.Add(a, tb)

                res

        member x.Remove(a : 'a, b : 'b, c : 'c) =
            lock table (fun () ->
                match table.TryGetValue a with
                | (true, tb) ->
                    match tb.TryGetValue b with
                    | (true, tc) ->
                        tc.Remove c |> ignore
                    | _ ->
                        ()
                | _ ->
                    ()
            ) 


[<AutoOpen>]
module Conversions =

    module BlendOperation =
        let toWebGPU =
            LookupTable.lookupTable [
                Aardvark.Rendering.BlendOperation.Add, WebGPU.BlendOperation.Add
                Aardvark.Rendering.BlendOperation.Maximum, WebGPU.BlendOperation.Max
                Aardvark.Rendering.BlendOperation.Minimum, WebGPU.BlendOperation.Min
                Aardvark.Rendering.BlendOperation.Subtract, WebGPU.BlendOperation.Subtract
                Aardvark.Rendering.BlendOperation.ReverseSubtract, WebGPU.BlendOperation.ReverseSubtract
            ]

    module BlendFactor =
        let toWebGPU =
            LookupTable.lookupTable [
                Aardvark.Rendering.BlendFactor.Zero, WebGPU.BlendFactor.Zero
                Aardvark.Rendering.BlendFactor.One, WebGPU.BlendFactor.One
                Aardvark.Rendering.BlendFactor.SourceAlpha, WebGPU.BlendFactor.SrcAlpha
                Aardvark.Rendering.BlendFactor.InvSourceAlpha, WebGPU.BlendFactor.OneMinusSrcAlpha
                Aardvark.Rendering.BlendFactor.DestinationAlpha, WebGPU.BlendFactor.DstAlpha
                Aardvark.Rendering.BlendFactor.InvDestinationAlpha, WebGPU.BlendFactor.OneMinusDstAlpha
                Aardvark.Rendering.BlendFactor.SourceColor, WebGPU.BlendFactor.Src
                Aardvark.Rendering.BlendFactor.InvSourceColor, WebGPU.BlendFactor.OneMinusSrc
                Aardvark.Rendering.BlendFactor.DestinationColor, WebGPU.BlendFactor.Dst
                Aardvark.Rendering.BlendFactor.InvDestinationColor, WebGPU.BlendFactor.OneMinusDst

                Aardvark.Rendering.BlendFactor.ConstantColor, WebGPU.BlendFactor.Constant
                Aardvark.Rendering.BlendFactor.InvConstantColor, WebGPU.BlendFactor.OneMinusConstant
                Aardvark.Rendering.BlendFactor.ConstantAlpha, WebGPU.BlendFactor.Constant
                Aardvark.Rendering.BlendFactor.InvConstantAlpha, WebGPU.BlendFactor.OneMinusConstant

                Aardvark.Rendering.BlendFactor.SourceAlphaSaturate, WebGPU.BlendFactor.SrcAlphaSaturated

            ]

    module RenderbufferFormat =
        let toWebGPU =
            LookupTable.lookupTable [
                RenderbufferFormat.DepthComponent, WebGPU.TextureFormat.Depth32Float
                RenderbufferFormat.Rgb10, WebGPU.TextureFormat.RGB10A2Unorm
                RenderbufferFormat.Rgba8, WebGPU.TextureFormat.RGBA8Unorm
                RenderbufferFormat.Rgb10A2, WebGPU.TextureFormat.RGB10A2Unorm
                RenderbufferFormat.Rgba16, WebGPU.TextureFormat.RGBA16Uint
                RenderbufferFormat.DepthComponent16, WebGPU.TextureFormat.Depth16Unorm
                RenderbufferFormat.DepthComponent24, WebGPU.TextureFormat.Depth24Plus
                RenderbufferFormat.DepthComponent32, WebGPU.TextureFormat.Depth32Float
                RenderbufferFormat.R8, WebGPU.TextureFormat.R8Unorm
                RenderbufferFormat.R16, WebGPU.TextureFormat.R16Uint
                RenderbufferFormat.Rg8, WebGPU.TextureFormat.RG8Unorm
                RenderbufferFormat.Rg16, WebGPU.TextureFormat.RG16Uint
                RenderbufferFormat.R16f, WebGPU.TextureFormat.R16Float
                RenderbufferFormat.R32f, WebGPU.TextureFormat.R32Float
                RenderbufferFormat.Rg16f, WebGPU.TextureFormat.RG16Float
                RenderbufferFormat.Rg32f, WebGPU.TextureFormat.RG32Float
                RenderbufferFormat.R8i, WebGPU.TextureFormat.R8Sint
                RenderbufferFormat.R8ui, WebGPU.TextureFormat.R8Uint
                RenderbufferFormat.R16i, WebGPU.TextureFormat.R16Sint
                RenderbufferFormat.R16ui, WebGPU.TextureFormat.R16Uint
                RenderbufferFormat.R32i, WebGPU.TextureFormat.R32Sint
                RenderbufferFormat.R32ui, WebGPU.TextureFormat.R32Uint
                RenderbufferFormat.Rg8i, WebGPU.TextureFormat.RG8Sint
                RenderbufferFormat.Rg8ui, WebGPU.TextureFormat.RG8Uint
                RenderbufferFormat.Rg16i, WebGPU.TextureFormat.RG16Sint
                RenderbufferFormat.Rg16ui, WebGPU.TextureFormat.RG16Uint
                RenderbufferFormat.Rg32i, WebGPU.TextureFormat.RG32Sint
                RenderbufferFormat.Rg32ui, WebGPU.TextureFormat.RG32Uint
                RenderbufferFormat.DepthStencil, WebGPU.TextureFormat.Depth24PlusStencil8
                RenderbufferFormat.Rgba32f, WebGPU.TextureFormat.RGBA32Float
                RenderbufferFormat.Rgba16f, WebGPU.TextureFormat.RGBA16Float
                RenderbufferFormat.Depth24Stencil8, WebGPU.TextureFormat.Depth24PlusStencil8
                RenderbufferFormat.R11fG11fB10f, WebGPU.TextureFormat.RG11B10Ufloat
                RenderbufferFormat.Rgb9E5, WebGPU.TextureFormat.RGB9E5Ufloat
                RenderbufferFormat.Srgb8Alpha8, WebGPU.TextureFormat.RGBA8UnormSrgb
                RenderbufferFormat.DepthComponent32f, WebGPU.TextureFormat.Depth32Float
                RenderbufferFormat.Rgba32ui, WebGPU.TextureFormat.RGBA32Uint
                RenderbufferFormat.Rgba16ui, WebGPU.TextureFormat.RGBA16Uint
                RenderbufferFormat.Rgba8ui, WebGPU.TextureFormat.RGBA8Uint
                RenderbufferFormat.Rgba32i, WebGPU.TextureFormat.RGBA32Sint
                RenderbufferFormat.Rgba16i, WebGPU.TextureFormat.RGBA16Sint
                RenderbufferFormat.Rgba8i, WebGPU.TextureFormat.RGBA8Sint
                RenderbufferFormat.Rgb10A2ui, WebGPU.TextureFormat.RGB10A2Unorm
            ]

    module DepthTest =
        let toWebGPU =
            LookupTable.lookupTable [
                DepthTest.LessOrEqual, CompareFunction.LessEqual
                DepthTest.Less, CompareFunction.Less
                DepthTest.GreaterOrEqual, CompareFunction.GreaterEqual
                DepthTest.Greater, CompareFunction.Greater
                DepthTest.Equal, CompareFunction.Equal
                DepthTest.NotEqual, CompareFunction.NotEqual
                DepthTest.Always, CompareFunction.Always
                DepthTest.Never, CompareFunction.Never
                DepthTest.None, CompareFunction.Always
            ]

    module ComparisonFunction =
        let toWebGPU =
            LookupTable.lookupTable [
                ComparisonFunction.LessOrEqual, CompareFunction.LessEqual
                ComparisonFunction.Less, CompareFunction.Less
                ComparisonFunction.GreaterOrEqual, CompareFunction.GreaterEqual
                ComparisonFunction.Greater, CompareFunction.Greater
                ComparisonFunction.Equal, CompareFunction.Equal
                ComparisonFunction.NotEqual, CompareFunction.NotEqual
                ComparisonFunction.Always, CompareFunction.Always
                ComparisonFunction.Never, CompareFunction.Never
            ]
            
    module StencilOperation =
        let toWebGPU =
            LookupTable.lookupTable [
                Aardvark.Rendering.StencilOperation.Decrement, StencilOperation.DecrementClamp
                Aardvark.Rendering.StencilOperation.DecrementWrap, StencilOperation.DecrementWrap
                Aardvark.Rendering.StencilOperation.Increment, StencilOperation.IncrementClamp
                Aardvark.Rendering.StencilOperation.IncrementWrap, StencilOperation.IncrementWrap
                Aardvark.Rendering.StencilOperation.Keep, StencilOperation.Keep
                Aardvark.Rendering.StencilOperation.Replace, StencilOperation.Replace
                Aardvark.Rendering.StencilOperation.Invert, StencilOperation.Invert
                Aardvark.Rendering.StencilOperation.Zero, StencilOperation.Zero
            ]

type ShaderProgram(signature : IFramebufferSignature, shaders : Map<ShaderStage, ShaderModule>, iface : FShade.GLSL.GLSLProgramInterface, layout : PipelineLayout) =
    member x.Shaders = shaders
    member x.Interface = iface
    member x.Layout = layout
    member x.Signature = signature

type AttributeDescriptor =
    {
        stepMode        : VertexStepMode
        isSingleValue   : bool
        elementType     : System.Type
        stride          : int
    }

type ResourceManager(device : Device) =

    let bufferCache = Cache<aval<IBuffer>, ares<Buffer>>()
    let shaderCache = Cache<IFramebufferSignature, FShade.Effect, System.Threading.Tasks.Task<ShaderProgram>>()
    let vertexStateCache = CacheV<ShaderProgram, list<VertexBufferLayout>, VertexState>()
    let primitiveStateCache = CacheV<aval<Aardvark.Rendering.CullMode>, IndexedGeometryMode, aval<PrimitiveState>>()
    
    let pipelineCache = Dict<ShaderProgram * VertexState * aval<PrimitiveState> * aval<FragmentState> * MultisampleState, ares<RenderPipeline>>()

    member x.CreateBuffer(usage : BufferUsage, input : aval<IBuffer>) =
        bufferCache.GetOrCreate(input, fun input ->

            let create (b : IBuffer) =
                match b with
                | :? INativeBuffer as b ->
                    let size = b.SizeInBytes

                    let temp =
                        device.CreateBuffer {
                            Label = null
                            Usage = BufferUsage.MapWrite ||| BufferUsage.CopySrc
                            Size = uint64 size
                            MappedAtCreation = true
                        }

                    let handle = 
                        device.CreateBuffer {
                            Label = null
                            Usage = BufferUsage.CopyDst ||| usage
                            Size = uint64 size
                            MappedAtCreation = false
                        }

                    let data = temp.GetMappedRange(0un, unativeint size)
                    b.Use(fun ptr ->
                        let ptr = NativePtr.ofNativeInt<byte> ptr |> NativePtr.toVoidPtr
                        let src = Uint8Array.op_Implicit(System.Span<byte>(ptr, size))
                        let dst = new Uint8Array(data)
                        dst.Set src
                    )

                    temp.Unmap()

                    let token = ResourceToken.Current
                    token.Encoder.CopyBufferToBuffer(temp, 0UL, handle, 0UL, uint64 size) 
                    token.WhenDone temp.Destroy 
                    handle

                | _ ->
                    failwithf "[WebGPU] bad buffer: %A" b

            let destroy (b : Buffer) =
                b.Destroy()
                bufferCache.Remove input

            input |> ARes.mapVal create destroy
        )
        
    member x.CreateBuffer(input : aval<IBuffer>) =
        let usage = BufferUsage.Vertex ||| BufferUsage.CopySrc ||| BufferUsage.Index ||| BufferUsage.Indirect ||| BufferUsage.Storage
        x.CreateBuffer(usage, input)

    member x.CreateShaderProgram(effect : FShade.Effect, signature : IFramebufferSignature) =
        shaderCache.GetOrCreate(signature, effect, fun signature effect ->
            async {
                try
                    let cfg =
                        {
                            FShade.depthRange = Range1d(-1.0, 1.0)
                            FShade.flipHandedness = false
                            FShade.lastStage = FShade.ShaderStage.Fragment
                            FShade.EffectConfig.outputs = signature.ColorAttachments |> Seq.map (fun (KeyValue(slot, (name, fmt))) -> string name, (typeof<V4d>, slot)) |> Map.ofSeq
                        }
                    let glsl =  
                        effect
                        |> FShade.Effect.toModule cfg
                        |> FShade.Imperative.ModuleCompiler.compile FShade.Backends.glslVulkan
                        |> FShade.GLSL.Assembler.assemble FShade.Backends.glslVulkan
        
                    let stages = glsl.iface.shaders |> MapExt.keys

                    let mutable res = Map.empty
                    for stage in stages do
                        let gpuStage =
                            match stage with
                            | FShade.ShaderStage.Vertex -> ShaderStage.Vertex
                            | FShade.ShaderStage.Fragment -> ShaderStage.Fragment
                            | _ -> failwithf "bad stage: %A" stage

                        let! sh = GLSLang.CreateShader(gpuStage, [string stage], glsl.code)
                        let m = device.CreateSpirVShaderModule sh.SpirV
                        let! messages = m.GetMessages()
                        for m in messages do
                            Log.error "[WebGPU] shader modile error: %s" m
                        res <- Map.add gpuStage m res
            
                    let mutable bindings = MapExt.empty

                    for KeyValue(_, v) in glsl.iface.uniformBuffers do
                        let entry =
                            {
                                Binding = v.ubBinding
                                Visibility = ShaderStage.Vertex ||| ShaderStage.Fragment
                                Buffer = 
                                    Some {
                                        HasDynamicOffset = false
                                        MinBindingSize = uint64 v.ubSize
                                        Type = BufferBindingType.Uniform
                                    }
                                Sampler = None
                                Texture = None
                                StorageTexture = None
                            }

                        bindings <- 
                            bindings |> MapExt.alter v.ubSet (function
                                | Some o -> Some (MapExt.add v.ubBinding entry o)
                                | None -> Some (MapExt.singleton v.ubBinding entry)
                            )


                        ()

                    for KeyValue(_, v) in glsl.iface.samplers do
                        let samplerBindingType =
                            if v.samplerType.isShadow then SamplerBindingType.Comparison
                            else
                                match v.samplerType.valueType with
                                | FShade.GLSL.GLSLType.Float _ 
                                | FShade.GLSL.GLSLType.Vec(_, FShade.GLSL.GLSLType.Float _) ->
                                    SamplerBindingType.Filtering
                                | _ ->
                                    SamplerBindingType.NonFiltering

                        let viewDimension =
                            if v.samplerType.isArray then
                                match v.samplerType.dimension with
                                | FShade.SamplerDimension.Sampler2d -> TextureViewDimension.D2DArray
                                | FShade.SamplerDimension.SamplerCube -> TextureViewDimension.CubeArray
                                | typ -> failwithf "[WebGPU] bad array-sampler: %A" typ
                            else
                                match v.samplerType.dimension with
                                | FShade.SamplerDimension.Sampler1d -> TextureViewDimension.D1D
                                | FShade.SamplerDimension.Sampler2d -> TextureViewDimension.D2D
                                | FShade.SamplerDimension.Sampler3d -> TextureViewDimension.D3D
                                | FShade.SamplerDimension.SamplerCube -> TextureViewDimension.Cube
                                | typ -> failwithf "[WebGPU] bad sampler: %A" typ
                            
                        let sampleType =
                            // TODO: check what this means!
                            if v.samplerType.isShadow then
                                TextureSampleType.Depth
                            else
                                match v.samplerType.valueType with
                                | FShade.GLSL.GLSLType.Float _ 
                                | FShade.GLSL.GLSLType.Vec(_, FShade.GLSL.GLSLType.Float _) ->
                                    TextureSampleType.Float
                            
                                | FShade.GLSL.GLSLType.Int(true, _) 
                                | FShade.GLSL.GLSLType.Vec(_, FShade.GLSL.GLSLType.Int(true, _)) ->
                                    TextureSampleType.Sint

                                | _ ->
                                    TextureSampleType.Uint
                        let entry =
                            {
                                Binding = v.samplerBinding
                                Visibility = ShaderStage.Vertex ||| ShaderStage.Fragment
                                Buffer = None
                                Sampler =
                                    Some {
                                        Type = samplerBindingType
                                    }
                                Texture =
                                    Some {
                                        SampleType = sampleType
                                        ViewDimension = viewDimension
                                        Multisampled = v.samplerType.isMS
                                    }
                                StorageTexture = None
                            }
                        
                        bindings <- 
                            bindings |> MapExt.alter v.samplerSet (function
                                | Some o -> Some (MapExt.add v.samplerBinding entry o)
                                | None -> Some (MapExt.singleton v.samplerBinding entry)
                            )

                    for KeyValue(_, v) in glsl.iface.storageBuffers do
                        let entry =
                            {
                                Binding = v.ssbBinding
                                Visibility = ShaderStage.Vertex ||| ShaderStage.Fragment
                                Buffer = 
                                    Some {
                                        HasDynamicOffset = false
                                        MinBindingSize = 0UL
                                        Type = BufferBindingType.Storage
                                    }
                                Sampler = None
                                Texture = None
                                StorageTexture = None
                            }
                        bindings <- 
                            bindings |> MapExt.alter v.ssbSet (function
                                | Some o -> Some (MapExt.add v.ssbBinding entry o)
                                | None -> Some (MapExt.singleton v.ssbBinding entry)
                            )

                    for KeyValue(_, v) in glsl.iface.images do  
                    
                        let viewDimension =
                            if v.imageType.isArray then
                                match v.imageType.dimension with
                                | FShade.SamplerDimension.Sampler2d -> TextureViewDimension.D2DArray
                                | FShade.SamplerDimension.SamplerCube -> TextureViewDimension.CubeArray
                                | typ -> failwithf "[WebGPU] bad array-sampler: %A" typ
                            else
                                match v.imageType.dimension with
                                | FShade.SamplerDimension.Sampler1d -> TextureViewDimension.D1D
                                | FShade.SamplerDimension.Sampler2d -> TextureViewDimension.D2D
                                | FShade.SamplerDimension.Sampler3d -> TextureViewDimension.D3D
                                | FShade.SamplerDimension.SamplerCube -> TextureViewDimension.Cube
                                | typ -> failwithf "[WebGPU] bad sampler: %A" typ
                            
                        let format =
                            match v.imageType.format with
                            | Some FShade.ImageFormat.Depth24Stencil8 -> TextureFormat.Depth24PlusStencil8
                            // TODO: more formats
                            | fmt -> failwithf "[WebGPU] bad storage-image format: %A" fmt

                        let entry =
                            {
                                Binding = v.imageBinding
                                Visibility = ShaderStage.Vertex ||| ShaderStage.Fragment
                                Buffer = None
                                Sampler = None
                                Texture = None
                                StorageTexture = 
                                    Some {
                                        Access = StorageTextureAccess.WriteOnly // TODO: read-write??? https://www.w3.org/TR/webgpu/#enumdef-gpustoragetextureaccess
                                        Format = format
                                        ViewDimension = viewDimension
                                    }
                            }
                        bindings <- 
                            bindings |> MapExt.alter v.imageSet (function
                                | Some o -> Some (MapExt.add v.imageBinding entry o)
                                | None -> Some (MapExt.singleton v.imageBinding entry)
                            )


                    let sets =  
                        bindings 
                        |> MapExt.toArray
                        |> Array.mapi (fun idx (slot, bindings) ->
                            if idx <> slot then Log.error "[WebGPU] cannot use sparse bindgroups"

                            device.CreateBindGroupLayout {
                                Label = null
                                Entries =
                                    bindings 
                                    |> MapExt.toArray
                                    |> Array.mapi (fun idx (slot, binding) ->
                                        if idx <> slot then Log.error "[WebGPU] cannot use sparse bindgroups"
                                        binding
                                    )
                            }
                        )

                    let layout =
                        device.CreatePipelineLayout {
                            Label = null
                            BindGroupLayouts = sets
                        }

                    return ShaderProgram(signature, res, glsl.iface, layout)
                with e ->
                    Log.error "[WebGPU] cannot create shader: %A" e
                    return raise e
            } |> Async.StartAsTask
        )
        |> Async.AwaitTask

    member x.CreateVertexState(program : ShaderProgram, tryGetAttribute : string -> option<AttributeDescriptor>) =
        
        let vertexBuffers =
            program.Interface.inputs |> List.choose (fun input ->   
                let attributeProps = tryGetAttribute input.paramSemantic
                //let sem = Symbol.Create input.paramSemantic
                //    match state.VertexAttributes.TryGetAttribute sem with
                //    | Some res -> Some(VertexStepMode.Vertex, res.IsSingleValue, res.ElementType, res.Stride)
                //    | None -> 
                //        match state.InstanceAttributes.TryGetAttribute sem with
                //        | Some res -> Some(VertexStepMode.Instance, res.IsSingleValue, res.ElementType, res.Stride)
                //        | None -> 
                //            None


                match attributeProps with
                | Some props ->
                    let inputType = props.elementType
                    let formats =
                        if inputType = typeof<float32> then [|VertexFormat.Float32, 0|]
                        elif inputType = typeof<V2f> then [|VertexFormat.Float32x2, 0|]
                        elif inputType = typeof<V3f> then [|VertexFormat.Float32x3, 0|]
                        elif inputType = typeof<V4f> then [|VertexFormat.Float32x4, 0|]

                        elif inputType = typeof<int32> then [|VertexFormat.Sint32, 0|]
                        elif inputType = typeof<V2i> then [|VertexFormat.Sint32x2, 0|]
                        elif inputType = typeof<V3i> then [|VertexFormat.Sint32x3, 0|]
                        elif inputType = typeof<V4i> then [|VertexFormat.Sint32x4, 0|]
                            
                        elif inputType = typeof<uint32> then [|VertexFormat.Uint32, 0|]
                        elif inputType = typeof<C3ui> then [|VertexFormat.Uint32x3, 0|]
                        elif inputType = typeof<C4ui> then [|VertexFormat.Uint32x4, 0|]
                            
                        elif inputType = typeof<C4b> then [|VertexFormat.Unorm8x4, 0|]
                        elif inputType = typeof<C4us> then [|VertexFormat.Unorm16x4, 0|]

                        else failwithf "[WebGPU] bad input-type: %A" inputType
                   

                    Some {
                        ArrayStride = uint64 props.stride
                        StepMode = props.stepMode
                        Attributes =
                            formats |> Array.mapi (fun i (fmt, off) ->
                                { 
                                    VertexAttribute.Offset = uint64 off
                                    VertexAttribute.ShaderLocation = input.paramLocation + i
                                    VertexAttribute.Format = fmt
                                }
                            )
                    }
                | None ->
                    None
            )

        vertexStateCache.GetOrCreate(program, vertexBuffers, fun program vertexBuffers ->
            {
                Module = program.Shaders.[ShaderStage.Vertex]
                EntryPoint = "main"
                Constants = [||]
                Buffers = List.toArray vertexBuffers
            }
        )
        
    member x.CreateVertexState(program : ShaderProgram, vatt : IAttributeProvider, iatt : IAttributeProvider) =
        let tryFind (sem : string) =
            let sem = Symbol.Create sem
            match vatt.TryGetAttribute sem with
            | Some res -> 
                Some { stepMode = VertexStepMode.Vertex; isSingleValue = res.IsSingleValue; elementType = res.ElementType; stride = res.Stride }
            | None -> 
                match iatt.TryGetAttribute sem with
                | Some res -> 
                    Some { stepMode = VertexStepMode.Instance; isSingleValue = res.IsSingleValue; elementType = res.ElementType; stride = res.Stride }
                | None -> 
                    None
        x.CreateVertexState(program, tryFind)

    member x.CreatePrimitiveState(mode : IndexedGeometryMode, cullMode : aval<Aardvark.Rendering.CullMode>) =
        primitiveStateCache.GetOrCreate(cullMode, mode, fun cullMode mode ->
            let topology = 
                match mode with
                | IndexedGeometryMode.PointList -> PrimitiveTopology.PointList
                | IndexedGeometryMode.LineList -> PrimitiveTopology.LineList
                | IndexedGeometryMode.LineStrip -> PrimitiveTopology.LineStrip
                | IndexedGeometryMode.TriangleList -> PrimitiveTopology.TriangleList
                | IndexedGeometryMode.TriangleStrip -> PrimitiveTopology.TriangleStrip
                | mode -> failwithf "[WebGPU] bad IndexedGeometryMode: %A" mode

            cullMode |> AVal.map (fun cullMode ->
                let mode =
                    match cullMode with
                    | Aardvark.Rendering.CullMode.None -> WebGPU.CullMode.None
                    | Aardvark.Rendering.CullMode.Front -> WebGPU.CullMode.Front
                    | Aardvark.Rendering.CullMode.Back -> WebGPU.CullMode.Back
                    | m -> failwithf "[WebGPU] bad CullMode: %A" m

                {
                    CullMode = mode
                    Topology = topology
                    StripIndexFormat = IndexFormat.Undefined
                    FrontFace = FrontFace.CCW
                }
            )

        )

    member x.CreatePipeline(signature : IFramebufferSignature, state : Aardvark.Rendering.RenderObject) =
        async { 
            let effect = 
                match state.Surface with
                | Aardvark.Rendering.Surface.FShadeSimple e -> e
                | surface -> failwithf "[WebGPU] bad surface: %A" surface

            let! program = x.CreateShaderProgram(effect, signature)

            let vertexState = x.CreateVertexState(program, state.VertexAttributes, state.InstanceAttributes)
            let primitive = x.CreatePrimitiveState(state.Mode, state.RasterizerState.CullMode)

            let depthStencil =
                match signature.DepthAttachment with
                | Some att ->
                    AVal.custom (fun token ->
                    
                        let writeEnabled = state.DepthState.WriteMask.GetValue token
                        let depthMode = state.DepthState.Test.GetValue token
                        let stencilFront = state.StencilState.ModeFront.GetValue token
                        let stencilBack = state.StencilState.ModeBack.GetValue token
                        let stencilMask = state.StencilState.WriteMaskFront.GetValue token
                        let depthBias = state.DepthState.Bias.GetValue token

                        let compare = DepthTest.toWebGPU depthMode
                        let stencilStateFront =
                            {
                                Compare = ComparisonFunction.toWebGPU stencilFront.Comparison
                                FailOp = StencilOperation.toWebGPU stencilFront.Fail
                                PassOp = StencilOperation.toWebGPU stencilFront.Pass
                                DepthFailOp = StencilOperation.toWebGPU stencilFront.DepthFail
                            }
                        
                        let stencilStateBack =
                            {
                                Compare = ComparisonFunction.toWebGPU stencilBack.Comparison
                                FailOp = StencilOperation.toWebGPU stencilBack.Fail
                                PassOp = StencilOperation.toWebGPU stencilBack.Pass
                                DepthFailOp = StencilOperation.toWebGPU stencilBack.DepthFail
                            }

                        Some {
                            Format = RenderbufferFormat.toWebGPU att.format
                            DepthWriteEnabled = writeEnabled
                            DepthCompare = compare
                            StencilFront = stencilStateFront
                            StencilBack = stencilStateBack
                            StencilReadMask = int stencilFront.CompareMask.Value
                            StencilWriteMask = int stencilMask.Value
                            DepthBias = int depthBias.Constant // TODO: scaling???
                            DepthBiasSlopeScale = float32 depthBias.SlopeScale
                            DepthBiasClamp = float32 depthBias.Clamp
                        }
                    )
                | None ->
                    AVal.constant None

            let fragment =
                AVal.custom (fun token ->
                    
                    let blendModes = state.BlendState.AttachmentMode.GetValue token
                    let blendMode = state.BlendState.Mode.GetValue token

                    let masks = state.BlendState.AttachmentWriteMask.GetValue token
                    let mask = state.BlendState.ColorWriteMask.GetValue token

                    {
                        Constants = [||]
                        EntryPoint = "main"
                        Module = program.Shaders.[ShaderStage.Fragment]
                        Targets =
                            signature.ColorAttachments 
                            |> Map.toArray
                            |> Array.mapi (fun i (slot, (sem, att)) ->
                                if i <> slot then Log.error "[WebGPU] cannot render to sparse outputs: %A" signature.ColorAttachments

                                let mask =
                                    let m =
                                        match Map.tryFind sem masks with
                                        | Some m -> m
                                        | None -> mask

                                    if m = ColorMask.All then 
                                        ColorWriteMask.All
                                    else
                                        let mutable m = ColorWriteMask.None
                                        if m.HasFlag ColorMask.Red then m <- m ||| ColorWriteMask.Red
                                        if m.HasFlag ColorMask.Green then m <- m ||| ColorWriteMask.Green
                                        if m.HasFlag ColorMask.Blue then m <- m ||| ColorWriteMask.Blue
                                        if m.HasFlag ColorMask.Alpha then m <- m ||| ColorWriteMask.Alpha
                                        m

                                let blendState =
                                    let mode = 
                                        match Map.tryFind sem blendModes with
                                        | Some m -> m
                                        | None -> blendMode
                                    if mode.Enabled then
                                        Some {
                                            Color =
                                                {
                                                    Operation = BlendOperation.toWebGPU mode.ColorOperation
                                                    SrcFactor = BlendFactor.toWebGPU mode.SourceColorFactor
                                                    DstFactor = BlendFactor.toWebGPU mode.DestinationColorFactor
                                                }
                                            Alpha =
                                                {
                                                    Operation = BlendOperation.toWebGPU mode.AlphaOperation
                                                    SrcFactor = BlendFactor.toWebGPU mode.SourceAlphaFactor
                                                    DstFactor = BlendFactor.toWebGPU mode.DestinationAlphaFactor
                                                }
                                        }
                                    else
                                        None
                                        

                                {
                                    Format = RenderbufferFormat.toWebGPU att.format
                                    Blend = blendState
                                    WriteMask = mask
                                }
                            )
                    }
                )

            let multisample =
                let samples =
                    if Map.isEmpty signature.ColorAttachments then  
                        match signature.DepthAttachment with
                        | Some a -> a.samples
                        | None -> 1
                    else
                        let (KeyValue(_, (_,att))) = signature.ColorAttachments |> Seq.head
                        att.samples
                
                // TODO: respect state.RasterizerState.Multisample!
                {
                    Count = samples
                    Mask = 0xFFFFFFFF
                    AlphaToCoverageEnabled = false
                }

            return
                pipelineCache.GetOrCreate((program, vertexState, primitive, fragment, multisample), fun (program, vertexState, primitiveState, fragmentState, multisample) ->
                    let dyn = 
                        AVal.custom (fun t ->
                            depthStencil.GetValue t,
                            fragment.GetValue t,
                            primitive.GetValue t
                        )
                    
                    let create (depthState : option<DepthStencilState>, fragment : FragmentState, prim : PrimitiveState) =
                        device.CreateRenderPipeline {
                            Label = null
                            Layout = program.Layout
                            Vertex = vertexState
                            Primitive = prim
                            DepthStencil = depthState
                            Multisample = multisample
                            Fragment = Some fragment
                        }
                        
                    let destroy (p : RenderPipeline) =
                        ()


                    dyn |> ARes.mapVal create destroy

                )

        }



