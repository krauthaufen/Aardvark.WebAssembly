namespace rec WebGPU
open System
open System.Threading
open WebAssembly
open WebAssembly.Core
open Aardvark.WebAssembly
#nowarn "9"
#nowarn "49"

[<AllowNullLiteral>]
type BindGroupHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type BindGroupLayoutHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type CommandBufferHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type PipelineLayoutHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type QuerySetHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type RenderBundleHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type SamplerHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type ShaderModuleHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type SurfaceHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type TextureViewHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type BufferHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type ComputePipelineHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type InstanceHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type RenderPipelineHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type SwapChainHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type ComputePassEncoderHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type FenceHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type RenderBundleEncoderHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type RenderPassEncoderHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type TextureHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type CommandEncoderHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type QueueHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type DeviceHandle(r : JSObject) = 
    inherit JsObj(r)
type WGPUDeviceLostCallback = delegate of string * int -> unit
type WGPUBufferMapCallback = delegate of obj * int -> unit
type WGPUErrorCallback = delegate of obj * string * int -> unit
type WGPUFenceOnCompletionCallback = delegate of obj * int -> unit
type WGPUCreateReadyComputePipelineCallback = delegate of obj * ComputePipelineHandle * string * int -> unit
type WGPUCreateReadyRenderPipelineCallback = delegate of obj * RenderPipelineHandle * string * int -> unit
type DeviceLostCallback = delegate of string * nativeint -> unit
type BufferMapCallback = delegate of BufferMapAsyncStatus * nativeint -> unit
type ErrorCallback = delegate of ErrorType * string * nativeint -> unit
type FenceOnCompletionCallback = delegate of FenceCompletionStatus * nativeint -> unit
type CreateReadyComputePipelineCallback = delegate of CreateReadyPipelineStatus * ComputePipeline * string * nativeint -> unit
type CreateReadyRenderPipelineCallback = delegate of CreateReadyPipelineStatus * RenderPipeline * string * nativeint -> unit


[<RequireQualifiedAccess>]
type AdapterType = 
| DiscreteGPU
| IntegratedGPU
| CPU
| Unknown
| Custom of string
    member internal x.GetValue() =
        match x with
        | AdapterType.DiscreteGPU -> "discrete-gpu" :> obj
        | AdapterType.IntegratedGPU -> "integrated-gpu" :> obj
        | AdapterType.CPU -> "cpu" :> obj
        | AdapterType.Unknown -> "unknown" :> obj
        | AdapterType.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "discrete-gpu" -> AdapterType.DiscreteGPU
            | "integrated-gpu" -> AdapterType.IntegratedGPU
            | "cpu" -> AdapterType.CPU
            | "unknown" -> AdapterType.Unknown
            | str -> AdapterType.Custom str
[<RequireQualifiedAccess>]
type AddressMode = 
| Repeat
| MirrorRepeat
| ClampToEdge
| Custom of string
    member internal x.GetValue() =
        match x with
        | AddressMode.Repeat -> "repeat" :> obj
        | AddressMode.MirrorRepeat -> "mirror-repeat" :> obj
        | AddressMode.ClampToEdge -> "clamp-to-edge" :> obj
        | AddressMode.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "repeat" -> AddressMode.Repeat
            | "mirror-repeat" -> AddressMode.MirrorRepeat
            | "clamp-to-edge" -> AddressMode.ClampToEdge
            | str -> AddressMode.Custom str
[<RequireQualifiedAccess>]
type BackendType = 
| Null
| D3D11
| D3D12
| Metal
| Vulkan
| OpenGL
| OpenGLES
| Custom of string
    member internal x.GetValue() =
        match x with
        | BackendType.Null -> "null" :> obj
        | BackendType.D3D11 -> "d3d11" :> obj
        | BackendType.D3D12 -> "d3d12" :> obj
        | BackendType.Metal -> "metal" :> obj
        | BackendType.Vulkan -> "vulkan" :> obj
        | BackendType.OpenGL -> "opengl" :> obj
        | BackendType.OpenGLES -> "opengles" :> obj
        | BackendType.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "null" -> BackendType.Null
            | "d3d11" -> BackendType.D3D11
            | "d3d12" -> BackendType.D3D12
            | "metal" -> BackendType.Metal
            | "vulkan" -> BackendType.Vulkan
            | "opengl" -> BackendType.OpenGL
            | "opengles" -> BackendType.OpenGLES
            | str -> BackendType.Custom str
[<RequireQualifiedAccess>]
type BindingType = 
| UniformBuffer
| StorageBuffer
| ReadonlyStorageBuffer
| Sampler
| ComparisonSampler
| SampledTexture
| MultisampledTexture
| ReadonlyStorageTexture
| WriteonlyStorageTexture
| Custom of string
    member internal x.GetValue() =
        match x with
        | BindingType.UniformBuffer -> "uniform-buffer" :> obj
        | BindingType.StorageBuffer -> "storage-buffer" :> obj
        | BindingType.ReadonlyStorageBuffer -> "readonly-storage-buffer" :> obj
        | BindingType.Sampler -> "sampler" :> obj
        | BindingType.ComparisonSampler -> "comparison-sampler" :> obj
        | BindingType.SampledTexture -> "sampled-texture" :> obj
        | BindingType.MultisampledTexture -> "multisampled-texture" :> obj
        | BindingType.ReadonlyStorageTexture -> "readonly-storage-texture" :> obj
        | BindingType.WriteonlyStorageTexture -> "writeonly-storage-texture" :> obj
        | BindingType.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "uniform-buffer" -> BindingType.UniformBuffer
            | "storage-buffer" -> BindingType.StorageBuffer
            | "readonly-storage-buffer" -> BindingType.ReadonlyStorageBuffer
            | "sampler" -> BindingType.Sampler
            | "comparison-sampler" -> BindingType.ComparisonSampler
            | "sampled-texture" -> BindingType.SampledTexture
            | "multisampled-texture" -> BindingType.MultisampledTexture
            | "readonly-storage-texture" -> BindingType.ReadonlyStorageTexture
            | "writeonly-storage-texture" -> BindingType.WriteonlyStorageTexture
            | str -> BindingType.Custom str
[<RequireQualifiedAccess>]
type BlendFactor = 
| Zero
| One
| SrcColor
| OneMinusSrcColor
| SrcAlpha
| OneMinusSrcAlpha
| DstColor
| OneMinusDstColor
| DstAlpha
| OneMinusDstAlpha
| SrcAlphaSaturated
| BlendColor
| OneMinusBlendColor
| Custom of string
    member internal x.GetValue() =
        match x with
        | BlendFactor.Zero -> "zero" :> obj
        | BlendFactor.One -> "one" :> obj
        | BlendFactor.SrcColor -> "src-color" :> obj
        | BlendFactor.OneMinusSrcColor -> "one-minus-src-color" :> obj
        | BlendFactor.SrcAlpha -> "src-alpha" :> obj
        | BlendFactor.OneMinusSrcAlpha -> "one-minus-src-alpha" :> obj
        | BlendFactor.DstColor -> "dst-color" :> obj
        | BlendFactor.OneMinusDstColor -> "one-minus-dst-color" :> obj
        | BlendFactor.DstAlpha -> "dst-alpha" :> obj
        | BlendFactor.OneMinusDstAlpha -> "one-minus-dst-alpha" :> obj
        | BlendFactor.SrcAlphaSaturated -> "src-alpha-saturated" :> obj
        | BlendFactor.BlendColor -> "blend-color" :> obj
        | BlendFactor.OneMinusBlendColor -> "one-minus-blend-color" :> obj
        | BlendFactor.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "zero" -> BlendFactor.Zero
            | "one" -> BlendFactor.One
            | "src-color" -> BlendFactor.SrcColor
            | "one-minus-src-color" -> BlendFactor.OneMinusSrcColor
            | "src-alpha" -> BlendFactor.SrcAlpha
            | "one-minus-src-alpha" -> BlendFactor.OneMinusSrcAlpha
            | "dst-color" -> BlendFactor.DstColor
            | "one-minus-dst-color" -> BlendFactor.OneMinusDstColor
            | "dst-alpha" -> BlendFactor.DstAlpha
            | "one-minus-dst-alpha" -> BlendFactor.OneMinusDstAlpha
            | "src-alpha-saturated" -> BlendFactor.SrcAlphaSaturated
            | "blend-color" -> BlendFactor.BlendColor
            | "one-minus-blend-color" -> BlendFactor.OneMinusBlendColor
            | str -> BlendFactor.Custom str
[<RequireQualifiedAccess>]
type BlendOperation = 
| Add
| Subtract
| ReverseSubtract
| Min
| Max
| Custom of string
    member internal x.GetValue() =
        match x with
        | BlendOperation.Add -> "add" :> obj
        | BlendOperation.Subtract -> "subtract" :> obj
        | BlendOperation.ReverseSubtract -> "reverse-subtract" :> obj
        | BlendOperation.Min -> "min" :> obj
        | BlendOperation.Max -> "max" :> obj
        | BlendOperation.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "add" -> BlendOperation.Add
            | "subtract" -> BlendOperation.Subtract
            | "reverse-subtract" -> BlendOperation.ReverseSubtract
            | "min" -> BlendOperation.Min
            | "max" -> BlendOperation.Max
            | str -> BlendOperation.Custom str
[<RequireQualifiedAccess>]
type BufferMapAsyncStatus = 
| Success
| Error
| Unknown
| DeviceLost
| DestroyedBeforeCallback
| UnmappedBeforeCallback
| Custom of string
    member internal x.GetValue() =
        match x with
        | BufferMapAsyncStatus.Success -> "success" :> obj
        | BufferMapAsyncStatus.Error -> "error" :> obj
        | BufferMapAsyncStatus.Unknown -> "unknown" :> obj
        | BufferMapAsyncStatus.DeviceLost -> "device-lost" :> obj
        | BufferMapAsyncStatus.DestroyedBeforeCallback -> "destroyed-before-callback" :> obj
        | BufferMapAsyncStatus.UnmappedBeforeCallback -> "unmapped-before-callback" :> obj
        | BufferMapAsyncStatus.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "success" -> BufferMapAsyncStatus.Success
            | "error" -> BufferMapAsyncStatus.Error
            | "unknown" -> BufferMapAsyncStatus.Unknown
            | "device-lost" -> BufferMapAsyncStatus.DeviceLost
            | "destroyed-before-callback" -> BufferMapAsyncStatus.DestroyedBeforeCallback
            | "unmapped-before-callback" -> BufferMapAsyncStatus.UnmappedBeforeCallback
            | str -> BufferMapAsyncStatus.Custom str
[<Flags>]
type BufferUsage = 
| None = 0x00000000
| MapRead = 0x00000001
| MapWrite = 0x00000002
| CopySrc = 0x00000004
| CopyDst = 0x00000008
| Index = 0x00000010
| Vertex = 0x00000020
| Uniform = 0x00000040
| Storage = 0x00000080
| Indirect = 0x00000100
| QueryResolve = 0x00000200
[<Flags>]
type ColorWriteMask = 
| None = 0x00000000
| Red = 0x00000001
| Green = 0x00000002
| Blue = 0x00000004
| Alpha = 0x00000008
| All = 0x0000000F
[<RequireQualifiedAccess>]
type CompareFunction = 
| Never
| Less
| LessEqual
| Greater
| GreaterEqual
| Equal
| NotEqual
| Always
| Custom of string
| Undefined
    member internal x.GetValue() =
        match x with
        | CompareFunction.Never -> "never" :> obj
        | CompareFunction.Less -> "less" :> obj
        | CompareFunction.LessEqual -> "less-equal" :> obj
        | CompareFunction.Greater -> "greater" :> obj
        | CompareFunction.GreaterEqual -> "greater-equal" :> obj
        | CompareFunction.Equal -> "equal" :> obj
        | CompareFunction.NotEqual -> "not-equal" :> obj
        | CompareFunction.Always -> "always" :> obj
        | CompareFunction.Custom n -> n :> obj
        | CompareFunction.Undefined -> null
    static member Parse(obj : obj) =
        if isNull obj then CompareFunction.Undefined
        else
            match (string obj).Trim().ToLower() with
            | "never" -> CompareFunction.Never
            | "less" -> CompareFunction.Less
            | "less-equal" -> CompareFunction.LessEqual
            | "greater" -> CompareFunction.Greater
            | "greater-equal" -> CompareFunction.GreaterEqual
            | "equal" -> CompareFunction.Equal
            | "not-equal" -> CompareFunction.NotEqual
            | "always" -> CompareFunction.Always
            | str -> CompareFunction.Custom str
[<RequireQualifiedAccess>]
type CreateReadyPipelineStatus = 
| Success
| Error
| DeviceLost
| Unknown
| Custom of string
    member internal x.GetValue() =
        match x with
        | CreateReadyPipelineStatus.Success -> "success" :> obj
        | CreateReadyPipelineStatus.Error -> "error" :> obj
        | CreateReadyPipelineStatus.DeviceLost -> "device-lost" :> obj
        | CreateReadyPipelineStatus.Unknown -> "unknown" :> obj
        | CreateReadyPipelineStatus.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "success" -> CreateReadyPipelineStatus.Success
            | "error" -> CreateReadyPipelineStatus.Error
            | "device-lost" -> CreateReadyPipelineStatus.DeviceLost
            | "unknown" -> CreateReadyPipelineStatus.Unknown
            | str -> CreateReadyPipelineStatus.Custom str
[<RequireQualifiedAccess>]
type CullMode = 
| None
| Front
| Back
| Custom of string
    member internal x.GetValue() =
        match x with
        | CullMode.None -> "none" :> obj
        | CullMode.Front -> "front" :> obj
        | CullMode.Back -> "back" :> obj
        | CullMode.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "none" -> CullMode.None
            | "front" -> CullMode.Front
            | "back" -> CullMode.Back
            | str -> CullMode.Custom str
[<RequireQualifiedAccess>]
type DepthLoadOp = 
| Load
| Depth of float
    member internal x.GetValue() =
        match x with
        | DepthLoadOp.Load -> "load" :> obj
        | DepthLoadOp.Depth c -> c :> obj
[<RequireQualifiedAccess>]
type ErrorFilter = 
| None
| Validation
| OutOfMemory
| Custom of string
    member internal x.GetValue() =
        match x with
        | ErrorFilter.None -> "none" :> obj
        | ErrorFilter.Validation -> "validation" :> obj
        | ErrorFilter.OutOfMemory -> "out-of-memory" :> obj
        | ErrorFilter.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "none" -> ErrorFilter.None
            | "validation" -> ErrorFilter.Validation
            | "out-of-memory" -> ErrorFilter.OutOfMemory
            | str -> ErrorFilter.Custom str
[<RequireQualifiedAccess>]
type ErrorType = 
| NoError
| Validation
| OutOfMemory
| Unknown
| DeviceLost
| Custom of string
    member internal x.GetValue() =
        match x with
        | ErrorType.NoError -> "no-error" :> obj
        | ErrorType.Validation -> "validation" :> obj
        | ErrorType.OutOfMemory -> "out-of-memory" :> obj
        | ErrorType.Unknown -> "unknown" :> obj
        | ErrorType.DeviceLost -> "device-lost" :> obj
        | ErrorType.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "no-error" -> ErrorType.NoError
            | "validation" -> ErrorType.Validation
            | "out-of-memory" -> ErrorType.OutOfMemory
            | "unknown" -> ErrorType.Unknown
            | "device-lost" -> ErrorType.DeviceLost
            | str -> ErrorType.Custom str
[<RequireQualifiedAccess>]
type FenceCompletionStatus = 
| Success
| Error
| Unknown
| DeviceLost
| Custom of string
    member internal x.GetValue() =
        match x with
        | FenceCompletionStatus.Success -> "success" :> obj
        | FenceCompletionStatus.Error -> "error" :> obj
        | FenceCompletionStatus.Unknown -> "unknown" :> obj
        | FenceCompletionStatus.DeviceLost -> "device-lost" :> obj
        | FenceCompletionStatus.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "success" -> FenceCompletionStatus.Success
            | "error" -> FenceCompletionStatus.Error
            | "unknown" -> FenceCompletionStatus.Unknown
            | "device-lost" -> FenceCompletionStatus.DeviceLost
            | str -> FenceCompletionStatus.Custom str
[<RequireQualifiedAccess>]
type FilterMode = 
| Nearest
| Linear
| Custom of string
    member internal x.GetValue() =
        match x with
        | FilterMode.Nearest -> "nearest" :> obj
        | FilterMode.Linear -> "linear" :> obj
        | FilterMode.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "nearest" -> FilterMode.Nearest
            | "linear" -> FilterMode.Linear
            | str -> FilterMode.Custom str
[<RequireQualifiedAccess>]
type FrontFace = 
| CCW
| CW
| Custom of string
    member internal x.GetValue() =
        match x with
        | FrontFace.CCW -> "ccw" :> obj
        | FrontFace.CW -> "cw" :> obj
        | FrontFace.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "ccw" -> FrontFace.CCW
            | "cw" -> FrontFace.CW
            | str -> FrontFace.Custom str
[<RequireQualifiedAccess>]
type IndexFormat = 
| Uint16
| Uint32
| Custom of string
| Undefined
    member internal x.GetValue() =
        match x with
        | IndexFormat.Uint16 -> "uint16" :> obj
        | IndexFormat.Uint32 -> "uint32" :> obj
        | IndexFormat.Custom n -> n :> obj
        | IndexFormat.Undefined -> null
    static member Parse(obj : obj) =
        if isNull obj then IndexFormat.Undefined
        else
            match (string obj).Trim().ToLower() with
            | "uint16" -> IndexFormat.Uint16
            | "uint32" -> IndexFormat.Uint32
            | str -> IndexFormat.Custom str
[<RequireQualifiedAccess>]
type InputStepMode = 
| Vertex
| Instance
| Custom of string
    member internal x.GetValue() =
        match x with
        | InputStepMode.Vertex -> "vertex" :> obj
        | InputStepMode.Instance -> "instance" :> obj
        | InputStepMode.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "vertex" -> InputStepMode.Vertex
            | "instance" -> InputStepMode.Instance
            | str -> InputStepMode.Custom str
[<RequireQualifiedAccess>]
type LoadOp = 
| Load
| Color of Color
    member internal x.GetValue() =
        match x with
        | LoadOp.Load -> "load" :> obj
        | LoadOp.Color c -> (createObj ["r", c.R :> obj; "g", c.G :> obj; "b", c.B :> obj; "a", c.A :> obj]).Reference :> obj
[<Flags>]
type MapMode = 
| None = 0x00000000
| Read = 0x00000001
| Write = 0x00000002
[<RequireQualifiedAccess>]
type PipelineStatisticName = 
| VertexShaderInvocations
| ClipperInvocations
| ClipperPrimitivesOut
| FragmentShaderInvocations
| ComputeShaderInvocations
| Custom of string
    member internal x.GetValue() =
        match x with
        | PipelineStatisticName.VertexShaderInvocations -> "vertex-shader-invocations" :> obj
        | PipelineStatisticName.ClipperInvocations -> "clipper-invocations" :> obj
        | PipelineStatisticName.ClipperPrimitivesOut -> "clipper-primitives-out" :> obj
        | PipelineStatisticName.FragmentShaderInvocations -> "fragment-shader-invocations" :> obj
        | PipelineStatisticName.ComputeShaderInvocations -> "compute-shader-invocations" :> obj
        | PipelineStatisticName.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "vertex-shader-invocations" -> PipelineStatisticName.VertexShaderInvocations
            | "clipper-invocations" -> PipelineStatisticName.ClipperInvocations
            | "clipper-primitives-out" -> PipelineStatisticName.ClipperPrimitivesOut
            | "fragment-shader-invocations" -> PipelineStatisticName.FragmentShaderInvocations
            | "compute-shader-invocations" -> PipelineStatisticName.ComputeShaderInvocations
            | str -> PipelineStatisticName.Custom str
[<RequireQualifiedAccess>]
type PresentMode = 
| Immediate
| Mailbox
| Fifo
| Custom of string
    member internal x.GetValue() =
        match x with
        | PresentMode.Immediate -> "immediate" :> obj
        | PresentMode.Mailbox -> "mailbox" :> obj
        | PresentMode.Fifo -> "fifo" :> obj
        | PresentMode.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "immediate" -> PresentMode.Immediate
            | "mailbox" -> PresentMode.Mailbox
            | "fifo" -> PresentMode.Fifo
            | str -> PresentMode.Custom str
[<RequireQualifiedAccess>]
type PrimitiveTopology = 
| PointList
| LineList
| LineStrip
| TriangleList
| TriangleStrip
| Custom of string
    member internal x.GetValue() =
        match x with
        | PrimitiveTopology.PointList -> "point-list" :> obj
        | PrimitiveTopology.LineList -> "line-list" :> obj
        | PrimitiveTopology.LineStrip -> "line-strip" :> obj
        | PrimitiveTopology.TriangleList -> "triangle-list" :> obj
        | PrimitiveTopology.TriangleStrip -> "triangle-strip" :> obj
        | PrimitiveTopology.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "point-list" -> PrimitiveTopology.PointList
            | "line-list" -> PrimitiveTopology.LineList
            | "line-strip" -> PrimitiveTopology.LineStrip
            | "triangle-list" -> PrimitiveTopology.TriangleList
            | "triangle-strip" -> PrimitiveTopology.TriangleStrip
            | str -> PrimitiveTopology.Custom str
[<RequireQualifiedAccess>]
type QueryType = 
| Occlusion
| PipelineStatistics
| Timestamp
| Custom of string
    member internal x.GetValue() =
        match x with
        | QueryType.Occlusion -> "occlusion" :> obj
        | QueryType.PipelineStatistics -> "pipeline-statistics" :> obj
        | QueryType.Timestamp -> "timestamp" :> obj
        | QueryType.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "occlusion" -> QueryType.Occlusion
            | "pipeline-statistics" -> QueryType.PipelineStatistics
            | "timestamp" -> QueryType.Timestamp
            | str -> QueryType.Custom str
[<RequireQualifiedAccess>]
type SType = 
| Invalid
| SurfaceDescriptorFromMetalLayer
| SurfaceDescriptorFromWindowsHWND
| SurfaceDescriptorFromXlib
| SurfaceDescriptorFromCanvasHTMLSelector
| ShaderModuleSPIRVDescriptor
| ShaderModuleWGSLDescriptor
| SamplerDescriptorDummyAnisotropicFiltering
| RenderPipelineDescriptorDummyExtension
| Custom of string
    member internal x.GetValue() =
        match x with
        | SType.Invalid -> "invalid" :> obj
        | SType.SurfaceDescriptorFromMetalLayer -> "surface-descriptor-from-metal-layer" :> obj
        | SType.SurfaceDescriptorFromWindowsHWND -> "surface-descriptor-from-windows-hwnd" :> obj
        | SType.SurfaceDescriptorFromXlib -> "surface-descriptor-from-xlib" :> obj
        | SType.SurfaceDescriptorFromCanvasHTMLSelector -> "surface-descriptor-from-canvas-html-selector" :> obj
        | SType.ShaderModuleSPIRVDescriptor -> "shader-module-spirv-descriptor" :> obj
        | SType.ShaderModuleWGSLDescriptor -> "shader-module-wgsl-descriptor" :> obj
        | SType.SamplerDescriptorDummyAnisotropicFiltering -> "sampler-descriptor-dummy-anisotropic-filtering" :> obj
        | SType.RenderPipelineDescriptorDummyExtension -> "render-pipeline-descriptor-dummy-extension" :> obj
        | SType.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "invalid" -> SType.Invalid
            | "surface-descriptor-from-metal-layer" -> SType.SurfaceDescriptorFromMetalLayer
            | "surface-descriptor-from-windows-hwnd" -> SType.SurfaceDescriptorFromWindowsHWND
            | "surface-descriptor-from-xlib" -> SType.SurfaceDescriptorFromXlib
            | "surface-descriptor-from-canvas-html-selector" -> SType.SurfaceDescriptorFromCanvasHTMLSelector
            | "shader-module-spirv-descriptor" -> SType.ShaderModuleSPIRVDescriptor
            | "shader-module-wgsl-descriptor" -> SType.ShaderModuleWGSLDescriptor
            | "sampler-descriptor-dummy-anisotropic-filtering" -> SType.SamplerDescriptorDummyAnisotropicFiltering
            | "render-pipeline-descriptor-dummy-extension" -> SType.RenderPipelineDescriptorDummyExtension
            | str -> SType.Custom str
[<Flags>]
type ShaderStage = 
| None = 0x00000000
| Vertex = 0x00000001
| Fragment = 0x00000002
| Compute = 0x00000004
[<RequireQualifiedAccess>]
type StencilLoadOp = 
| Load
| Stencil of int
    member internal x.GetValue() =
        match x with
        | StencilLoadOp.Load -> "load" :> obj
        | StencilLoadOp.Stencil c -> c :> obj
[<RequireQualifiedAccess>]
type StencilOperation = 
| Keep
| Zero
| Replace
| Invert
| IncrementClamp
| DecrementClamp
| IncrementWrap
| DecrementWrap
| Custom of string
    member internal x.GetValue() =
        match x with
        | StencilOperation.Keep -> "keep" :> obj
        | StencilOperation.Zero -> "zero" :> obj
        | StencilOperation.Replace -> "replace" :> obj
        | StencilOperation.Invert -> "invert" :> obj
        | StencilOperation.IncrementClamp -> "increment-clamp" :> obj
        | StencilOperation.DecrementClamp -> "decrement-clamp" :> obj
        | StencilOperation.IncrementWrap -> "increment-wrap" :> obj
        | StencilOperation.DecrementWrap -> "decrement-wrap" :> obj
        | StencilOperation.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "keep" -> StencilOperation.Keep
            | "zero" -> StencilOperation.Zero
            | "replace" -> StencilOperation.Replace
            | "invert" -> StencilOperation.Invert
            | "increment-clamp" -> StencilOperation.IncrementClamp
            | "decrement-clamp" -> StencilOperation.DecrementClamp
            | "increment-wrap" -> StencilOperation.IncrementWrap
            | "decrement-wrap" -> StencilOperation.DecrementWrap
            | str -> StencilOperation.Custom str
[<RequireQualifiedAccess>]
type StoreOp = 
| Store
| Clear
| Custom of string
    member internal x.GetValue() =
        match x with
        | StoreOp.Store -> "store" :> obj
        | StoreOp.Clear -> "clear" :> obj
        | StoreOp.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "store" -> StoreOp.Store
            | "clear" -> StoreOp.Clear
            | str -> StoreOp.Custom str
[<RequireQualifiedAccess>]
type TextureAspect = 
| All
| StencilOnly
| DepthOnly
| Custom of string
    member internal x.GetValue() =
        match x with
        | TextureAspect.All -> "all" :> obj
        | TextureAspect.StencilOnly -> "stencil-only" :> obj
        | TextureAspect.DepthOnly -> "depth-only" :> obj
        | TextureAspect.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "all" -> TextureAspect.All
            | "stencil-only" -> TextureAspect.StencilOnly
            | "depth-only" -> TextureAspect.DepthOnly
            | str -> TextureAspect.Custom str
[<RequireQualifiedAccess>]
type TextureComponentType = 
| Float
| Sint
| Uint
| DepthComparison
| Custom of string
    member internal x.GetValue() =
        match x with
        | TextureComponentType.Float -> "float" :> obj
        | TextureComponentType.Sint -> "sint" :> obj
        | TextureComponentType.Uint -> "uint" :> obj
        | TextureComponentType.DepthComparison -> "depth-comparison" :> obj
        | TextureComponentType.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "float" -> TextureComponentType.Float
            | "sint" -> TextureComponentType.Sint
            | "uint" -> TextureComponentType.Uint
            | "depth-comparison" -> TextureComponentType.DepthComparison
            | str -> TextureComponentType.Custom str
[<RequireQualifiedAccess>]
type TextureDimension = 
| D1D
| D2D
| D3D
| Custom of string
    member internal x.GetValue() =
        match x with
        | TextureDimension.D1D -> "1d" :> obj
        | TextureDimension.D2D -> "2d" :> obj
        | TextureDimension.D3D -> "3d" :> obj
        | TextureDimension.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "1d" -> TextureDimension.D1D
            | "2d" -> TextureDimension.D2D
            | "3d" -> TextureDimension.D3D
            | str -> TextureDimension.Custom str
[<RequireQualifiedAccess>]
type TextureFormat = 
| R8Unorm
| R8Snorm
| R8Uint
| R8Sint
| R16Uint
| R16Sint
| R16Float
| RG8Unorm
| RG8Snorm
| RG8Uint
| RG8Sint
| R32Float
| R32Uint
| R32Sint
| RG16Uint
| RG16Sint
| RG16Float
| RGBA8Unorm
| RGBA8UnormSrgb
| RGBA8Snorm
| RGBA8Uint
| RGBA8Sint
| BGRA8Unorm
| BGRA8UnormSrgb
| RGB10A2Unorm
| RG11B10Ufloat
| RGB9E5Ufloat
| RG32Float
| RG32Uint
| RG32Sint
| RGBA16Uint
| RGBA16Sint
| RGBA16Float
| RGBA32Float
| RGBA32Uint
| RGBA32Sint
| Depth32Float
| Depth24Plus
| Depth24PlusStencil8
| BC1RGBAUnorm
| BC1RGBAUnormSrgb
| BC2RGBAUnorm
| BC2RGBAUnormSrgb
| BC3RGBAUnorm
| BC3RGBAUnormSrgb
| BC4RUnorm
| BC4RSnorm
| BC5RGUnorm
| BC5RGSnorm
| BC6HRGBUfloat
| BC6HRGBFloat
| BC7RGBAUnorm
| BC7RGBAUnormSrgb
| Custom of string
| Undefined
    member internal x.GetValue() =
        match x with
        | TextureFormat.R8Unorm -> "r8unorm" :> obj
        | TextureFormat.R8Snorm -> "r8snorm" :> obj
        | TextureFormat.R8Uint -> "r8uint" :> obj
        | TextureFormat.R8Sint -> "r8sint" :> obj
        | TextureFormat.R16Uint -> "r16uint" :> obj
        | TextureFormat.R16Sint -> "r16sint" :> obj
        | TextureFormat.R16Float -> "r16float" :> obj
        | TextureFormat.RG8Unorm -> "rg8unorm" :> obj
        | TextureFormat.RG8Snorm -> "rg8snorm" :> obj
        | TextureFormat.RG8Uint -> "rg8uint" :> obj
        | TextureFormat.RG8Sint -> "rg8sint" :> obj
        | TextureFormat.R32Float -> "r32float" :> obj
        | TextureFormat.R32Uint -> "r32uint" :> obj
        | TextureFormat.R32Sint -> "r32sint" :> obj
        | TextureFormat.RG16Uint -> "rg16uint" :> obj
        | TextureFormat.RG16Sint -> "rg16sint" :> obj
        | TextureFormat.RG16Float -> "rg16float" :> obj
        | TextureFormat.RGBA8Unorm -> "rgba8unorm" :> obj
        | TextureFormat.RGBA8UnormSrgb -> "rgba8unorm-srgb" :> obj
        | TextureFormat.RGBA8Snorm -> "rgba8snorm" :> obj
        | TextureFormat.RGBA8Uint -> "rgba8uint" :> obj
        | TextureFormat.RGBA8Sint -> "rgba8sint" :> obj
        | TextureFormat.BGRA8Unorm -> "bgra8unorm" :> obj
        | TextureFormat.BGRA8UnormSrgb -> "bgra8unorm-srgb" :> obj
        | TextureFormat.RGB10A2Unorm -> "rgb10a2unorm" :> obj
        | TextureFormat.RG11B10Ufloat -> "rg11b10ufloat" :> obj
        | TextureFormat.RGB9E5Ufloat -> "rgb9e5ufloat" :> obj
        | TextureFormat.RG32Float -> "rg32float" :> obj
        | TextureFormat.RG32Uint -> "rg32uint" :> obj
        | TextureFormat.RG32Sint -> "rg32sint" :> obj
        | TextureFormat.RGBA16Uint -> "rgba16uint" :> obj
        | TextureFormat.RGBA16Sint -> "rgba16sint" :> obj
        | TextureFormat.RGBA16Float -> "rgba16float" :> obj
        | TextureFormat.RGBA32Float -> "rgba32float" :> obj
        | TextureFormat.RGBA32Uint -> "rgba32uint" :> obj
        | TextureFormat.RGBA32Sint -> "rgba32sint" :> obj
        | TextureFormat.Depth32Float -> "depth32float" :> obj
        | TextureFormat.Depth24Plus -> "depth24plus" :> obj
        | TextureFormat.Depth24PlusStencil8 -> "depth24plus-stencil8" :> obj
        | TextureFormat.BC1RGBAUnorm -> "bc1rgba-unorm" :> obj
        | TextureFormat.BC1RGBAUnormSrgb -> "bc1rgba-unorm-srgb" :> obj
        | TextureFormat.BC2RGBAUnorm -> "bc2rgba-unorm" :> obj
        | TextureFormat.BC2RGBAUnormSrgb -> "bc2rgba-unorm-srgb" :> obj
        | TextureFormat.BC3RGBAUnorm -> "bc3rgba-unorm" :> obj
        | TextureFormat.BC3RGBAUnormSrgb -> "bc3rgba-unorm-srgb" :> obj
        | TextureFormat.BC4RUnorm -> "bc4r-unorm" :> obj
        | TextureFormat.BC4RSnorm -> "bc4r-snorm" :> obj
        | TextureFormat.BC5RGUnorm -> "bc5rg-unorm" :> obj
        | TextureFormat.BC5RGSnorm -> "bc5rg-snorm" :> obj
        | TextureFormat.BC6HRGBUfloat -> "bc6h-rgb-ufloat" :> obj
        | TextureFormat.BC6HRGBFloat -> "bc6h-rgb-float" :> obj
        | TextureFormat.BC7RGBAUnorm -> "bc7rgba-unorm" :> obj
        | TextureFormat.BC7RGBAUnormSrgb -> "bc7rgba-unorm-srgb" :> obj
        | TextureFormat.Custom n -> n :> obj
        | TextureFormat.Undefined -> null
    static member Parse(obj : obj) =
        if isNull obj then TextureFormat.Undefined
        else
            match (string obj).Trim().ToLower() with
            | "r8unorm" -> TextureFormat.R8Unorm
            | "r8snorm" -> TextureFormat.R8Snorm
            | "r8uint" -> TextureFormat.R8Uint
            | "r8sint" -> TextureFormat.R8Sint
            | "r16uint" -> TextureFormat.R16Uint
            | "r16sint" -> TextureFormat.R16Sint
            | "r16float" -> TextureFormat.R16Float
            | "rg8unorm" -> TextureFormat.RG8Unorm
            | "rg8snorm" -> TextureFormat.RG8Snorm
            | "rg8uint" -> TextureFormat.RG8Uint
            | "rg8sint" -> TextureFormat.RG8Sint
            | "r32float" -> TextureFormat.R32Float
            | "r32uint" -> TextureFormat.R32Uint
            | "r32sint" -> TextureFormat.R32Sint
            | "rg16uint" -> TextureFormat.RG16Uint
            | "rg16sint" -> TextureFormat.RG16Sint
            | "rg16float" -> TextureFormat.RG16Float
            | "rgba8unorm" -> TextureFormat.RGBA8Unorm
            | "rgba8unorm-srgb" -> TextureFormat.RGBA8UnormSrgb
            | "rgba8snorm" -> TextureFormat.RGBA8Snorm
            | "rgba8uint" -> TextureFormat.RGBA8Uint
            | "rgba8sint" -> TextureFormat.RGBA8Sint
            | "bgra8unorm" -> TextureFormat.BGRA8Unorm
            | "bgra8unorm-srgb" -> TextureFormat.BGRA8UnormSrgb
            | "rgb10a2unorm" -> TextureFormat.RGB10A2Unorm
            | "rg11b10ufloat" -> TextureFormat.RG11B10Ufloat
            | "rgb9e5ufloat" -> TextureFormat.RGB9E5Ufloat
            | "rg32float" -> TextureFormat.RG32Float
            | "rg32uint" -> TextureFormat.RG32Uint
            | "rg32sint" -> TextureFormat.RG32Sint
            | "rgba16uint" -> TextureFormat.RGBA16Uint
            | "rgba16sint" -> TextureFormat.RGBA16Sint
            | "rgba16float" -> TextureFormat.RGBA16Float
            | "rgba32float" -> TextureFormat.RGBA32Float
            | "rgba32uint" -> TextureFormat.RGBA32Uint
            | "rgba32sint" -> TextureFormat.RGBA32Sint
            | "depth32float" -> TextureFormat.Depth32Float
            | "depth24plus" -> TextureFormat.Depth24Plus
            | "depth24plus-stencil8" -> TextureFormat.Depth24PlusStencil8
            | "bc1rgba-unorm" -> TextureFormat.BC1RGBAUnorm
            | "bc1rgba-unorm-srgb" -> TextureFormat.BC1RGBAUnormSrgb
            | "bc2rgba-unorm" -> TextureFormat.BC2RGBAUnorm
            | "bc2rgba-unorm-srgb" -> TextureFormat.BC2RGBAUnormSrgb
            | "bc3rgba-unorm" -> TextureFormat.BC3RGBAUnorm
            | "bc3rgba-unorm-srgb" -> TextureFormat.BC3RGBAUnormSrgb
            | "bc4r-unorm" -> TextureFormat.BC4RUnorm
            | "bc4r-snorm" -> TextureFormat.BC4RSnorm
            | "bc5rg-unorm" -> TextureFormat.BC5RGUnorm
            | "bc5rg-snorm" -> TextureFormat.BC5RGSnorm
            | "bc6h-rgb-ufloat" -> TextureFormat.BC6HRGBUfloat
            | "bc6h-rgb-float" -> TextureFormat.BC6HRGBFloat
            | "bc7rgba-unorm" -> TextureFormat.BC7RGBAUnorm
            | "bc7rgba-unorm-srgb" -> TextureFormat.BC7RGBAUnormSrgb
            | str -> TextureFormat.Custom str
[<Flags>]
type TextureUsage = 
| None = 0x00000000
| CopySrc = 0x00000001
| CopyDst = 0x00000002
| Sampled = 0x00000004
| Storage = 0x00000008
| OutputAttachment = 0x00000010
| Present = 0x00000020
[<RequireQualifiedAccess>]
type TextureViewDimension = 
| D1D
| D2D
| D2DArray
| Cube
| CubeArray
| D3D
| Custom of string
| Undefined
    member internal x.GetValue() =
        match x with
        | TextureViewDimension.D1D -> "1d" :> obj
        | TextureViewDimension.D2D -> "2d" :> obj
        | TextureViewDimension.D2DArray -> "2d-array" :> obj
        | TextureViewDimension.Cube -> "cube" :> obj
        | TextureViewDimension.CubeArray -> "cube-array" :> obj
        | TextureViewDimension.D3D -> "3d" :> obj
        | TextureViewDimension.Custom n -> n :> obj
        | TextureViewDimension.Undefined -> null
    static member Parse(obj : obj) =
        if isNull obj then TextureViewDimension.Undefined
        else
            match (string obj).Trim().ToLower() with
            | "1d" -> TextureViewDimension.D1D
            | "2d" -> TextureViewDimension.D2D
            | "2d-array" -> TextureViewDimension.D2DArray
            | "cube" -> TextureViewDimension.Cube
            | "cube-array" -> TextureViewDimension.CubeArray
            | "3d" -> TextureViewDimension.D3D
            | str -> TextureViewDimension.Custom str
[<RequireQualifiedAccess>]
type VertexFormat = 
| UChar2
| UChar4
| Char2
| Char4
| UChar2Norm
| UChar4Norm
| Char2Norm
| Char4Norm
| UShort2
| UShort4
| Short2
| Short4
| UShort2Norm
| UShort4Norm
| Short2Norm
| Short4Norm
| Half2
| Half4
| Float
| Float2
| Float3
| Float4
| UInt
| UInt2
| UInt3
| UInt4
| Int
| Int2
| Int3
| Int4
| Custom of string
    member internal x.GetValue() =
        match x with
        | VertexFormat.UChar2 -> "uchar2" :> obj
        | VertexFormat.UChar4 -> "uchar4" :> obj
        | VertexFormat.Char2 -> "char2" :> obj
        | VertexFormat.Char4 -> "char4" :> obj
        | VertexFormat.UChar2Norm -> "uchar2norm" :> obj
        | VertexFormat.UChar4Norm -> "uchar4norm" :> obj
        | VertexFormat.Char2Norm -> "char2norm" :> obj
        | VertexFormat.Char4Norm -> "char4norm" :> obj
        | VertexFormat.UShort2 -> "ushort2" :> obj
        | VertexFormat.UShort4 -> "ushort4" :> obj
        | VertexFormat.Short2 -> "short2" :> obj
        | VertexFormat.Short4 -> "short4" :> obj
        | VertexFormat.UShort2Norm -> "ushort2norm" :> obj
        | VertexFormat.UShort4Norm -> "ushort4norm" :> obj
        | VertexFormat.Short2Norm -> "short2norm" :> obj
        | VertexFormat.Short4Norm -> "short4norm" :> obj
        | VertexFormat.Half2 -> "half2" :> obj
        | VertexFormat.Half4 -> "half4" :> obj
        | VertexFormat.Float -> "float" :> obj
        | VertexFormat.Float2 -> "float2" :> obj
        | VertexFormat.Float3 -> "float3" :> obj
        | VertexFormat.Float4 -> "float4" :> obj
        | VertexFormat.UInt -> "uint" :> obj
        | VertexFormat.UInt2 -> "uint2" :> obj
        | VertexFormat.UInt3 -> "uint3" :> obj
        | VertexFormat.UInt4 -> "uint4" :> obj
        | VertexFormat.Int -> "int" :> obj
        | VertexFormat.Int2 -> "int2" :> obj
        | VertexFormat.Int3 -> "int3" :> obj
        | VertexFormat.Int4 -> "int4" :> obj
        | VertexFormat.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "uchar2" -> VertexFormat.UChar2
            | "uchar4" -> VertexFormat.UChar4
            | "char2" -> VertexFormat.Char2
            | "char4" -> VertexFormat.Char4
            | "uchar2norm" -> VertexFormat.UChar2Norm
            | "uchar4norm" -> VertexFormat.UChar4Norm
            | "char2norm" -> VertexFormat.Char2Norm
            | "char4norm" -> VertexFormat.Char4Norm
            | "ushort2" -> VertexFormat.UShort2
            | "ushort4" -> VertexFormat.UShort4
            | "short2" -> VertexFormat.Short2
            | "short4" -> VertexFormat.Short4
            | "ushort2norm" -> VertexFormat.UShort2Norm
            | "ushort4norm" -> VertexFormat.UShort4Norm
            | "short2norm" -> VertexFormat.Short2Norm
            | "short4norm" -> VertexFormat.Short4Norm
            | "half2" -> VertexFormat.Half2
            | "half4" -> VertexFormat.Half4
            | "float" -> VertexFormat.Float
            | "float2" -> VertexFormat.Float2
            | "float3" -> VertexFormat.Float3
            | "float4" -> VertexFormat.Float4
            | "uint" -> VertexFormat.UInt
            | "uint2" -> VertexFormat.UInt2
            | "uint3" -> VertexFormat.UInt3
            | "uint4" -> VertexFormat.UInt4
            | "int" -> VertexFormat.Int
            | "int2" -> VertexFormat.Int2
            | "int3" -> VertexFormat.Int3
            | "int4" -> VertexFormat.Int4
            | str -> VertexFormat.Custom str


module DawnRaw =
    [<AllowNullLiteral>]
    type WGPUColor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUColor(new JSObject())
        member x.R
            with get() : float = h.GetObjectProperty("r") |> convert<float>
            and set (v : float) = h.SetObjectProperty("r", js v)
        member x.G
            with get() : float = h.GetObjectProperty("g") |> convert<float>
            and set (v : float) = h.SetObjectProperty("g", js v)
        member x.B
            with get() : float = h.GetObjectProperty("b") |> convert<float>
            and set (v : float) = h.SetObjectProperty("b", js v)
        member x.A
            with get() : float = h.GetObjectProperty("a") |> convert<float>
            and set (v : float) = h.SetObjectProperty("a", js v)
    [<AllowNullLiteral>]
    type WGPUCommandBufferDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUCommandBufferDescriptor(new JSObject())
        member x.Label
            with get() : string = h.GetObjectProperty("label") |> convert<string>
            and set (v : string) = h.SetObjectProperty("label", js v)
    [<AllowNullLiteral>]
    type WGPUCommandEncoderDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUCommandEncoderDescriptor(new JSObject())
        member x.Label
            with get() : string = h.GetObjectProperty("label") |> convert<string>
            and set (v : string) = h.SetObjectProperty("label", js v)
    [<AllowNullLiteral>]
    type WGPUComputePassDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUComputePassDescriptor(new JSObject())
        member x.Label
            with get() : string = h.GetObjectProperty("label") |> convert<string>
            and set (v : string) = h.SetObjectProperty("label", js v)
    [<AllowNullLiteral>]
    type WGPUDeviceProperties(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUDeviceProperties(new JSObject())
        member x.TextureCompressionBC
            with get() : bool = h.GetObjectProperty("textureCompressionBC") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("textureCompressionBC", js v)
        member x.ShaderFloat16
            with get() : bool = h.GetObjectProperty("shaderFloat16") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("shaderFloat16", js v)
        member x.PipelineStatisticsQuery
            with get() : bool = h.GetObjectProperty("pipelineStatisticsQuery") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("pipelineStatisticsQuery", js v)
        member x.TimestampQuery
            with get() : bool = h.GetObjectProperty("timestampQuery") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("timestampQuery", js v)
    [<AllowNullLiteral>]
    type WGPUExtent3D(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUExtent3D(new JSObject())
        member x.Width
            with get() : int = h.GetObjectProperty("width") |> convert<int>
            and set (v : int) = h.SetObjectProperty("width", js v)
        member x.Height
            with get() : int = h.GetObjectProperty("height") |> convert<int>
            and set (v : int) = h.SetObjectProperty("height", js v)
        member x.Depth
            with get() : int = h.GetObjectProperty("depth") |> convert<int>
            and set (v : int) = h.SetObjectProperty("depth", js v)
    [<AllowNullLiteral>]
    type WGPUFenceDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUFenceDescriptor(new JSObject())
        member x.Label
            with get() : string = h.GetObjectProperty("label") |> convert<string>
            and set (v : string) = h.SetObjectProperty("label", js v)
        member x.InitialValue
            with get() : int = h.GetObjectProperty("initialValue") |> convert<int>
            and set (v : int) = h.SetObjectProperty("initialValue", js v)
    [<AllowNullLiteral>]
    type WGPUInstanceDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUInstanceDescriptor(new JSObject())
    [<AllowNullLiteral>]
    type WGPUOrigin3D(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUOrigin3D(new JSObject())
        member x.X
            with get() : int = h.GetObjectProperty("x") |> convert<int>
            and set (v : int) = h.SetObjectProperty("x", js v)
        member x.Y
            with get() : int = h.GetObjectProperty("y") |> convert<int>
            and set (v : int) = h.SetObjectProperty("y", js v)
        member x.Z
            with get() : int = h.GetObjectProperty("z") |> convert<int>
            and set (v : int) = h.SetObjectProperty("z", js v)
    [<AllowNullLiteral>]
    type WGPURenderBundleDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPURenderBundleDescriptor(new JSObject())
        member x.Label
            with get() : string = h.GetObjectProperty("label") |> convert<string>
            and set (v : string) = h.SetObjectProperty("label", js v)
    [<AllowNullLiteral>]
    type WGPUSamplerDescriptorDummyAnisotropicFiltering(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUSamplerDescriptorDummyAnisotropicFiltering(new JSObject())
        member x.MaxAnisotropy
            with get() : float32 = h.GetObjectProperty("maxAnisotropy") |> convert<float32>
            and set (v : float32) = h.SetObjectProperty("maxAnisotropy", js v)
    [<AllowNullLiteral>]
    type WGPUShaderModuleSPIRVDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUShaderModuleSPIRVDescriptor(new JSObject())
        member x.Code
            with get() : Uint32Array = h.GetObjectProperty("code") |> convert<Uint32Array>
            and set (v : Uint32Array) = h.SetObjectProperty("code", js v)
    [<AllowNullLiteral>]
    type WGPUShaderModuleWGSLDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUShaderModuleWGSLDescriptor(new JSObject())
        member x.Source
            with get() : string = h.GetObjectProperty("source") |> convert<string>
            and set (v : string) = h.SetObjectProperty("source", js v)
    [<AllowNullLiteral>]
    type WGPUShaderModuleDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUShaderModuleDescriptor(new JSObject())
        member x.Label
            with get() : string = h.GetObjectProperty("label") |> convert<string>
            and set (v : string) = h.SetObjectProperty("label", js v)
    [<AllowNullLiteral>]
    type WGPUSurfaceDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUSurfaceDescriptor(new JSObject())
        member x.Label
            with get() : string = h.GetObjectProperty("label") |> convert<string>
            and set (v : string) = h.SetObjectProperty("label", js v)
    [<AllowNullLiteral>]
    type WGPUSurfaceDescriptorFromCanvasHTMLSelector(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUSurfaceDescriptorFromCanvasHTMLSelector(new JSObject())
        member x.Selector
            with get() : string = h.GetObjectProperty("selector") |> convert<string>
            and set (v : string) = h.SetObjectProperty("selector", js v)
    [<AllowNullLiteral>]
    type WGPUSurfaceDescriptorFromMetalLayer(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUSurfaceDescriptorFromMetalLayer(new JSObject())
        member x.Layer
            with get() : int = h.GetObjectProperty("layer") |> convert<int>
            and set (v : int) = h.SetObjectProperty("layer", js v)
    [<AllowNullLiteral>]
    type WGPUSurfaceDescriptorFromWindowsHWND(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUSurfaceDescriptorFromWindowsHWND(new JSObject())
        member x.Hinstance
            with get() : int = h.GetObjectProperty("hinstance") |> convert<int>
            and set (v : int) = h.SetObjectProperty("hinstance", js v)
        member x.Hwnd
            with get() : int = h.GetObjectProperty("hwnd") |> convert<int>
            and set (v : int) = h.SetObjectProperty("hwnd", js v)
    [<AllowNullLiteral>]
    type WGPUSurfaceDescriptorFromXlib(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUSurfaceDescriptorFromXlib(new JSObject())
        member x.Display
            with get() : int = h.GetObjectProperty("display") |> convert<int>
            and set (v : int) = h.SetObjectProperty("display", js v)
        member x.Window
            with get() : int = h.GetObjectProperty("window") |> convert<int>
            and set (v : int) = h.SetObjectProperty("window", js v)
    [<AllowNullLiteral>]
    type WGPUTextureDataLayout(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUTextureDataLayout(new JSObject())
        member x.Offset
            with get() : int = h.GetObjectProperty("offset") |> convert<int>
            and set (v : int) = h.SetObjectProperty("offset", js v)
        member x.BytesPerRow
            with get() : int = h.GetObjectProperty("bytesPerRow") |> convert<int>
            and set (v : int) = h.SetObjectProperty("bytesPerRow", js v)
        member x.RowsPerImage
            with get() : int = h.GetObjectProperty("rowsPerImage") |> convert<int>
            and set (v : int) = h.SetObjectProperty("rowsPerImage", js v)
    [<AllowNullLiteral>]
    type WGPUAdapterProperties(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUAdapterProperties(new JSObject())
        member x.DeviceID
            with get() : int = h.GetObjectProperty("deviceID") |> convert<int>
            and set (v : int) = h.SetObjectProperty("deviceID", js v)
        member x.VendorID
            with get() : int = h.GetObjectProperty("vendorID") |> convert<int>
            and set (v : int) = h.SetObjectProperty("vendorID", js v)
        member x.Name
            with get() : string = h.GetObjectProperty("name") |> convert<string>
            and set (v : string) = h.SetObjectProperty("name", js v)
        member x.DriverDescription
            with get() : string = h.GetObjectProperty("driverDescription") |> convert<string>
            and set (v : string) = h.SetObjectProperty("driverDescription", js v)
        member x.AdapterType
            with get() : obj = h.GetObjectProperty("adapterType") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("adapterType", v)
        member x.BackendType
            with get() : obj = h.GetObjectProperty("backendType") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("backendType", v)
    [<AllowNullLiteral>]
    type WGPUBindGroupLayoutEntry(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUBindGroupLayoutEntry(new JSObject())
        member x.Binding
            with get() : int = h.GetObjectProperty("binding") |> convert<int>
            and set (v : int) = h.SetObjectProperty("binding", js v)
        member x.Visibility
            with get() : int = h.GetObjectProperty("visibility") |> convert<int>
            and set (v : int) = h.SetObjectProperty("visibility", v)
        member x.Type
            with get() : obj = h.GetObjectProperty("type") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("type", v)
        member x.HasDynamicOffset
            with get() : bool = h.GetObjectProperty("hasDynamicOffset") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("hasDynamicOffset", js v)
        member x.MinBufferBindingSize
            with get() : int = h.GetObjectProperty("minBufferBindingSize") |> convert<int>
            and set (v : int) = h.SetObjectProperty("minBufferBindingSize", js v)
        member x.Multisampled
            with get() : bool = h.GetObjectProperty("multisampled") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("multisampled", js v)
        member x.ViewDimension
            with get() : obj = h.GetObjectProperty("viewDimension") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("viewDimension", v)
        member x.TextureComponentType
            with get() : obj = h.GetObjectProperty("textureComponentType") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("textureComponentType", v)
        member x.StorageTextureFormat
            with get() : obj = h.GetObjectProperty("storageTextureFormat") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("storageTextureFormat", v)
    [<AllowNullLiteral>]
    type WGPUBlendDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUBlendDescriptor(new JSObject())
        member x.Operation
            with get() : obj = h.GetObjectProperty("operation") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("operation", v)
        member x.SrcFactor
            with get() : obj = h.GetObjectProperty("srcFactor") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("srcFactor", v)
        member x.DstFactor
            with get() : obj = h.GetObjectProperty("dstFactor") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("dstFactor", v)
    [<AllowNullLiteral>]
    type WGPUBufferDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUBufferDescriptor(new JSObject())
        member x.Label
            with get() : string = h.GetObjectProperty("label") |> convert<string>
            and set (v : string) = h.SetObjectProperty("label", js v)
        member x.Usage
            with get() : int = h.GetObjectProperty("usage") |> convert<int>
            and set (v : int) = h.SetObjectProperty("usage", v)
        member x.Size
            with get() : int = h.GetObjectProperty("size") |> convert<int>
            and set (v : int) = h.SetObjectProperty("size", js v)
        member x.MappedAtCreation
            with get() : bool = h.GetObjectProperty("mappedAtCreation") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("mappedAtCreation", js v)
    [<AllowNullLiteral>]
    type WGPUPipelineLayoutDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUPipelineLayoutDescriptor(new JSObject())
        member x.Label
            with get() : string = h.GetObjectProperty("label") |> convert<string>
            and set (v : string) = h.SetObjectProperty("label", js v)
        member x.BindGroupLayouts
            with get() : JSObject = h.GetObjectProperty("bindGroupLayouts") |> convert<JSObject>
            and set (v : JSObject) = h.SetObjectProperty("bindGroupLayouts", js v)
    [<AllowNullLiteral>]
    type WGPUProgrammableStageDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUProgrammableStageDescriptor(new JSObject())
        member x.Module
            with get() : ShaderModuleHandle = h.GetObjectProperty("module") |> convert<ShaderModuleHandle>
            and set (v : ShaderModuleHandle) = h.SetObjectProperty("module", js v)
        member x.EntryPoint
            with get() : string = h.GetObjectProperty("entryPoint") |> convert<string>
            and set (v : string) = h.SetObjectProperty("entryPoint", js v)
    [<AllowNullLiteral>]
    type WGPUQuerySetDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUQuerySetDescriptor(new JSObject())
        member x.Label
            with get() : string = h.GetObjectProperty("label") |> convert<string>
            and set (v : string) = h.SetObjectProperty("label", js v)
        member x.Type
            with get() : obj = h.GetObjectProperty("type") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("type", v)
        member x.Count
            with get() : int = h.GetObjectProperty("count") |> convert<int>
            and set (v : int) = h.SetObjectProperty("count", js v)
        member x.PipelineStatistics
            with get() : obj = h.GetObjectProperty("pipelineStatistics") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("pipelineStatistics", js v)
        member x.PipelineStatisticsCount
            with get() : int = h.GetObjectProperty("pipelineStatisticsCount") |> convert<int>
            and set (v : int) = h.SetObjectProperty("pipelineStatisticsCount", js v)
    [<AllowNullLiteral>]
    type WGPURasterizationStateDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPURasterizationStateDescriptor(new JSObject())
        member x.FrontFace
            with get() : obj = h.GetObjectProperty("frontFace") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("frontFace", v)
        member x.CullMode
            with get() : obj = h.GetObjectProperty("cullMode") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("cullMode", v)
        member x.DepthBias
            with get() : int = h.GetObjectProperty("depthBias") |> convert<int>
            and set (v : int) = h.SetObjectProperty("depthBias", js v)
        member x.DepthBiasSlopeScale
            with get() : float32 = h.GetObjectProperty("depthBiasSlopeScale") |> convert<float32>
            and set (v : float32) = h.SetObjectProperty("depthBiasSlopeScale", js v)
        member x.DepthBiasClamp
            with get() : float32 = h.GetObjectProperty("depthBiasClamp") |> convert<float32>
            and set (v : float32) = h.SetObjectProperty("depthBiasClamp", js v)
    [<AllowNullLiteral>]
    type WGPURenderBundleEncoderDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPURenderBundleEncoderDescriptor(new JSObject())
        member x.Label
            with get() : string = h.GetObjectProperty("label") |> convert<string>
            and set (v : string) = h.SetObjectProperty("label", js v)
        member x.ColorFormats
            with get() : JSObject = h.GetObjectProperty("colorFormats") |> convert<JSObject>
            and set (v : JSObject) = h.SetObjectProperty("colorFormats", js v)
        member x.DepthStencilFormat
            with get() : obj = h.GetObjectProperty("depthStencilFormat") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("depthStencilFormat", v)
        member x.SampleCount
            with get() : int = h.GetObjectProperty("sampleCount") |> convert<int>
            and set (v : int) = h.SetObjectProperty("sampleCount", js v)
    [<AllowNullLiteral>]
    type WGPURenderPassColorAttachmentDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPURenderPassColorAttachmentDescriptor(new JSObject())
        member x.Attachment
            with get() : TextureViewHandle = h.GetObjectProperty("attachment") |> convert<TextureViewHandle>
            and set (v : TextureViewHandle) = h.SetObjectProperty("attachment", js v)
        member x.ResolveTarget
            with get() : TextureViewHandle = h.GetObjectProperty("resolveTarget") |> convert<TextureViewHandle>
            and set (v : TextureViewHandle) = h.SetObjectProperty("resolveTarget", js v)
        member x.LoadValue
            with get() : obj = h.GetObjectProperty("loadValue") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("loadValue", v)
        member x.StoreOp
            with get() : obj = h.GetObjectProperty("storeOp") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("storeOp", v)
    [<AllowNullLiteral>]
    type WGPURenderPassDepthStencilAttachmentDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPURenderPassDepthStencilAttachmentDescriptor(new JSObject())
        member x.Attachment
            with get() : TextureViewHandle = h.GetObjectProperty("attachment") |> convert<TextureViewHandle>
            and set (v : TextureViewHandle) = h.SetObjectProperty("attachment", js v)
        member x.DepthLoadValue
            with get() : obj = h.GetObjectProperty("depthLoadValue") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("depthLoadValue", v)
        member x.DepthStoreOp
            with get() : obj = h.GetObjectProperty("depthStoreOp") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("depthStoreOp", v)
        member x.DepthReadOnly
            with get() : bool = h.GetObjectProperty("depthReadOnly") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("depthReadOnly", js v)
        member x.StencilLoadValue
            with get() : obj = h.GetObjectProperty("stencilLoadValue") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("stencilLoadValue", v)
        member x.StencilStoreOp
            with get() : obj = h.GetObjectProperty("stencilStoreOp") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("stencilStoreOp", v)
        member x.StencilReadOnly
            with get() : bool = h.GetObjectProperty("stencilReadOnly") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("stencilReadOnly", js v)
    [<AllowNullLiteral>]
    type WGPUSamplerDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUSamplerDescriptor(new JSObject())
        member x.Label
            with get() : string = h.GetObjectProperty("label") |> convert<string>
            and set (v : string) = h.SetObjectProperty("label", js v)
        member x.AddressModeU
            with get() : obj = h.GetObjectProperty("addressModeU") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("addressModeU", v)
        member x.AddressModeV
            with get() : obj = h.GetObjectProperty("addressModeV") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("addressModeV", v)
        member x.AddressModeW
            with get() : obj = h.GetObjectProperty("addressModeW") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("addressModeW", v)
        member x.MagFilter
            with get() : obj = h.GetObjectProperty("magFilter") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("magFilter", v)
        member x.MinFilter
            with get() : obj = h.GetObjectProperty("minFilter") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("minFilter", v)
        member x.MipmapFilter
            with get() : obj = h.GetObjectProperty("mipmapFilter") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("mipmapFilter", v)
        member x.LodMinClamp
            with get() : float32 = h.GetObjectProperty("lodMinClamp") |> convert<float32>
            and set (v : float32) = h.SetObjectProperty("lodMinClamp", js v)
        member x.LodMaxClamp
            with get() : float32 = h.GetObjectProperty("lodMaxClamp") |> convert<float32>
            and set (v : float32) = h.SetObjectProperty("lodMaxClamp", js v)
        member x.Compare
            with get() : obj = h.GetObjectProperty("compare") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("compare", v)
    [<AllowNullLiteral>]
    type WGPUStencilStateFaceDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUStencilStateFaceDescriptor(new JSObject())
        member x.Compare
            with get() : obj = h.GetObjectProperty("compare") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("compare", v)
        member x.FailOp
            with get() : obj = h.GetObjectProperty("failOp") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("failOp", v)
        member x.DepthFailOp
            with get() : obj = h.GetObjectProperty("depthFailOp") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("depthFailOp", v)
        member x.PassOp
            with get() : obj = h.GetObjectProperty("passOp") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("passOp", v)
    [<AllowNullLiteral>]
    type WGPUSwapChainDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUSwapChainDescriptor(new JSObject())
        member x.Label
            with get() : string = h.GetObjectProperty("label") |> convert<string>
            and set (v : string) = h.SetObjectProperty("label", js v)
        member x.Usage
            with get() : int = h.GetObjectProperty("usage") |> convert<int>
            and set (v : int) = h.SetObjectProperty("usage", v)
        member x.Format
            with get() : obj = h.GetObjectProperty("format") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("format", v)
        member x.Width
            with get() : int = h.GetObjectProperty("width") |> convert<int>
            and set (v : int) = h.SetObjectProperty("width", js v)
        member x.Height
            with get() : int = h.GetObjectProperty("height") |> convert<int>
            and set (v : int) = h.SetObjectProperty("height", js v)
        member x.PresentMode
            with get() : obj = h.GetObjectProperty("presentMode") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("presentMode", v)
        member x.Implementation
            with get() : int = h.GetObjectProperty("implementation") |> convert<int>
            and set (v : int) = h.SetObjectProperty("implementation", js v)
    [<AllowNullLiteral>]
    type WGPUTextureDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUTextureDescriptor(new JSObject())
        member x.Label
            with get() : string = h.GetObjectProperty("label") |> convert<string>
            and set (v : string) = h.SetObjectProperty("label", js v)
        member x.Usage
            with get() : int = h.GetObjectProperty("usage") |> convert<int>
            and set (v : int) = h.SetObjectProperty("usage", v)
        member x.Dimension
            with get() : obj = h.GetObjectProperty("dimension") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("dimension", v)
        member x.Size
            with get() : DawnRaw.WGPUExtent3D = h.GetObjectProperty("size") |> convert<DawnRaw.WGPUExtent3D>
            and set (v : DawnRaw.WGPUExtent3D) = h.SetObjectProperty("size", js v)
        member x.Format
            with get() : obj = h.GetObjectProperty("format") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("format", v)
        member x.MipLevelCount
            with get() : int = h.GetObjectProperty("mipLevelCount") |> convert<int>
            and set (v : int) = h.SetObjectProperty("mipLevelCount", js v)
        member x.SampleCount
            with get() : int = h.GetObjectProperty("sampleCount") |> convert<int>
            and set (v : int) = h.SetObjectProperty("sampleCount", js v)
    [<AllowNullLiteral>]
    type WGPUTextureViewDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUTextureViewDescriptor(new JSObject())
        member x.Label
            with get() : string = h.GetObjectProperty("label") |> convert<string>
            and set (v : string) = h.SetObjectProperty("label", js v)
        member x.Format
            with get() : obj = h.GetObjectProperty("format") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("format", v)
        member x.Dimension
            with get() : obj = h.GetObjectProperty("dimension") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("dimension", v)
        member x.BaseMipLevel
            with get() : int = h.GetObjectProperty("baseMipLevel") |> convert<int>
            and set (v : int) = h.SetObjectProperty("baseMipLevel", js v)
        member x.MipLevelCount
            with get() : int = h.GetObjectProperty("mipLevelCount") |> convert<int>
            and set (v : int) = h.SetObjectProperty("mipLevelCount", js v)
        member x.BaseArrayLayer
            with get() : int = h.GetObjectProperty("baseArrayLayer") |> convert<int>
            and set (v : int) = h.SetObjectProperty("baseArrayLayer", js v)
        member x.ArrayLayerCount
            with get() : int = h.GetObjectProperty("arrayLayerCount") |> convert<int>
            and set (v : int) = h.SetObjectProperty("arrayLayerCount", js v)
        member x.Aspect
            with get() : obj = h.GetObjectProperty("aspect") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("aspect", v)
    [<AllowNullLiteral>]
    type WGPUVertexAttributeDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUVertexAttributeDescriptor(new JSObject())
        member x.Format
            with get() : obj = h.GetObjectProperty("format") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("format", v)
        member x.Offset
            with get() : int = h.GetObjectProperty("offset") |> convert<int>
            and set (v : int) = h.SetObjectProperty("offset", js v)
        member x.ShaderLocation
            with get() : int = h.GetObjectProperty("shaderLocation") |> convert<int>
            and set (v : int) = h.SetObjectProperty("shaderLocation", js v)
    [<AllowNullLiteral>]
    type WGPUBindGroupEntry(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUBindGroupEntry(new JSObject())
        member x.Binding
            with get() : int = h.GetObjectProperty("binding") |> convert<int>
            and set (v : int) = h.SetObjectProperty("binding", js v)
        member x.Buffer
            with get() : BufferHandle = h.GetObjectProperty("buffer") |> convert<BufferHandle>
            and set (v : BufferHandle) = h.SetObjectProperty("buffer", js v)
        member x.Offset
            with get() : int = h.GetObjectProperty("offset") |> convert<int>
            and set (v : int) = h.SetObjectProperty("offset", js v)
        member x.Size
            with get() : int = h.GetObjectProperty("size") |> convert<int>
            and set (v : int) = h.SetObjectProperty("size", js v)
        member x.Sampler
            with get() : SamplerHandle = h.GetObjectProperty("sampler") |> convert<SamplerHandle>
            and set (v : SamplerHandle) = h.SetObjectProperty("sampler", js v)
        member x.TextureView
            with get() : TextureViewHandle = h.GetObjectProperty("textureView") |> convert<TextureViewHandle>
            and set (v : TextureViewHandle) = h.SetObjectProperty("textureView", js v)
    [<AllowNullLiteral>]
    type WGPUBindGroupLayoutDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUBindGroupLayoutDescriptor(new JSObject())
        member x.Label
            with get() : string = h.GetObjectProperty("label") |> convert<string>
            and set (v : string) = h.SetObjectProperty("label", js v)
        member x.Entries
            with get() : JSObject = h.GetObjectProperty("entries") |> convert<JSObject>
            and set (v : JSObject) = h.SetObjectProperty("entries", js v)
    [<AllowNullLiteral>]
    type WGPUBufferCopyView(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUBufferCopyView(new JSObject())
        member x.Layout
            with get() : DawnRaw.WGPUTextureDataLayout = h.GetObjectProperty("layout") |> convert<DawnRaw.WGPUTextureDataLayout>
            and set (v : DawnRaw.WGPUTextureDataLayout) = h.SetObjectProperty("layout", js v)
        member x.Buffer
            with get() : BufferHandle = h.GetObjectProperty("buffer") |> convert<BufferHandle>
            and set (v : BufferHandle) = h.SetObjectProperty("buffer", js v)
    [<AllowNullLiteral>]
    type WGPUColorStateDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUColorStateDescriptor(new JSObject())
        member x.Format
            with get() : obj = h.GetObjectProperty("format") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("format", v)
        member x.AlphaBlend
            with get() : DawnRaw.WGPUBlendDescriptor = h.GetObjectProperty("alphaBlend") |> convert<DawnRaw.WGPUBlendDescriptor>
            and set (v : DawnRaw.WGPUBlendDescriptor) = h.SetObjectProperty("alphaBlend", js v)
        member x.ColorBlend
            with get() : DawnRaw.WGPUBlendDescriptor = h.GetObjectProperty("colorBlend") |> convert<DawnRaw.WGPUBlendDescriptor>
            and set (v : DawnRaw.WGPUBlendDescriptor) = h.SetObjectProperty("colorBlend", js v)
        member x.WriteMask
            with get() : int = h.GetObjectProperty("writeMask") |> convert<int>
            and set (v : int) = h.SetObjectProperty("writeMask", v)
    [<AllowNullLiteral>]
    type WGPUComputePipelineDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUComputePipelineDescriptor(new JSObject())
        member x.Label
            with get() : string = h.GetObjectProperty("label") |> convert<string>
            and set (v : string) = h.SetObjectProperty("label", js v)
        member x.Layout
            with get() : PipelineLayoutHandle = h.GetObjectProperty("layout") |> convert<PipelineLayoutHandle>
            and set (v : PipelineLayoutHandle) = h.SetObjectProperty("layout", js v)
        member x.ComputeStage
            with get() : DawnRaw.WGPUProgrammableStageDescriptor = h.GetObjectProperty("computeStage") |> convert<DawnRaw.WGPUProgrammableStageDescriptor>
            and set (v : DawnRaw.WGPUProgrammableStageDescriptor) = h.SetObjectProperty("computeStage", js v)
    [<AllowNullLiteral>]
    type WGPUDepthStencilStateDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUDepthStencilStateDescriptor(new JSObject())
        member x.Format
            with get() : obj = h.GetObjectProperty("format") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("format", v)
        member x.DepthWriteEnabled
            with get() : bool = h.GetObjectProperty("depthWriteEnabled") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("depthWriteEnabled", js v)
        member x.DepthCompare
            with get() : obj = h.GetObjectProperty("depthCompare") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("depthCompare", v)
        member x.StencilFront
            with get() : DawnRaw.WGPUStencilStateFaceDescriptor = h.GetObjectProperty("stencilFront") |> convert<DawnRaw.WGPUStencilStateFaceDescriptor>
            and set (v : DawnRaw.WGPUStencilStateFaceDescriptor) = h.SetObjectProperty("stencilFront", js v)
        member x.StencilBack
            with get() : DawnRaw.WGPUStencilStateFaceDescriptor = h.GetObjectProperty("stencilBack") |> convert<DawnRaw.WGPUStencilStateFaceDescriptor>
            and set (v : DawnRaw.WGPUStencilStateFaceDescriptor) = h.SetObjectProperty("stencilBack", js v)
        member x.StencilReadMask
            with get() : int = h.GetObjectProperty("stencilReadMask") |> convert<int>
            and set (v : int) = h.SetObjectProperty("stencilReadMask", js v)
        member x.StencilWriteMask
            with get() : int = h.GetObjectProperty("stencilWriteMask") |> convert<int>
            and set (v : int) = h.SetObjectProperty("stencilWriteMask", js v)
    [<AllowNullLiteral>]
    type WGPURenderPassDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPURenderPassDescriptor(new JSObject())
        member x.Label
            with get() : string = h.GetObjectProperty("label") |> convert<string>
            and set (v : string) = h.SetObjectProperty("label", js v)
        member x.ColorAttachments
            with get() : JSObject = h.GetObjectProperty("colorAttachments") |> convert<JSObject>
            and set (v : JSObject) = h.SetObjectProperty("colorAttachments", js v)
        member x.DepthStencilAttachment
            with get() : DawnRaw.WGPURenderPassDepthStencilAttachmentDescriptor = h.GetObjectProperty("depthStencilAttachment") |> convert<DawnRaw.WGPURenderPassDepthStencilAttachmentDescriptor>
            and set (v : DawnRaw.WGPURenderPassDepthStencilAttachmentDescriptor) = h.SetObjectProperty("depthStencilAttachment", js v)
        member x.OcclusionQuerySet
            with get() : QuerySetHandle = h.GetObjectProperty("occlusionQuerySet") |> convert<QuerySetHandle>
            and set (v : QuerySetHandle) = h.SetObjectProperty("occlusionQuerySet", js v)
    [<AllowNullLiteral>]
    type WGPURenderPipelineDescriptorDummyExtension(h : JSObject) =
        inherit JsObj(h)
        new() = WGPURenderPipelineDescriptorDummyExtension(new JSObject())
        member x.DummyStage
            with get() : DawnRaw.WGPUProgrammableStageDescriptor = h.GetObjectProperty("dummyStage") |> convert<DawnRaw.WGPUProgrammableStageDescriptor>
            and set (v : DawnRaw.WGPUProgrammableStageDescriptor) = h.SetObjectProperty("dummyStage", js v)
    [<AllowNullLiteral>]
    type WGPUVertexBufferLayoutDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUVertexBufferLayoutDescriptor(new JSObject())
        member x.ArrayStride
            with get() : int = h.GetObjectProperty("arrayStride") |> convert<int>
            and set (v : int) = h.SetObjectProperty("arrayStride", js v)
        member x.StepMode
            with get() : obj = h.GetObjectProperty("stepMode") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("stepMode", v)
        member x.Attributes
            with get() : JSObject = h.GetObjectProperty("attributes") |> convert<JSObject>
            and set (v : JSObject) = h.SetObjectProperty("attributes", js v)
    [<AllowNullLiteral>]
    type WGPUBindGroupDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUBindGroupDescriptor(new JSObject())
        member x.Label
            with get() : string = h.GetObjectProperty("label") |> convert<string>
            and set (v : string) = h.SetObjectProperty("label", js v)
        member x.Layout
            with get() : BindGroupLayoutHandle = h.GetObjectProperty("layout") |> convert<BindGroupLayoutHandle>
            and set (v : BindGroupLayoutHandle) = h.SetObjectProperty("layout", js v)
        member x.Entries
            with get() : JSObject = h.GetObjectProperty("entries") |> convert<JSObject>
            and set (v : JSObject) = h.SetObjectProperty("entries", js v)
    [<AllowNullLiteral>]
    type WGPUTextureCopyView(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUTextureCopyView(new JSObject())
        member x.Texture
            with get() : TextureHandle = h.GetObjectProperty("texture") |> convert<TextureHandle>
            and set (v : TextureHandle) = h.SetObjectProperty("texture", js v)
        member x.MipLevel
            with get() : int = h.GetObjectProperty("mipLevel") |> convert<int>
            and set (v : int) = h.SetObjectProperty("mipLevel", js v)
        member x.Origin
            with get() : DawnRaw.WGPUOrigin3D = h.GetObjectProperty("origin") |> convert<DawnRaw.WGPUOrigin3D>
            and set (v : DawnRaw.WGPUOrigin3D) = h.SetObjectProperty("origin", js v)
        member x.Aspect
            with get() : obj = h.GetObjectProperty("aspect") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("aspect", v)
    [<AllowNullLiteral>]
    type WGPUVertexStateDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUVertexStateDescriptor(new JSObject())
        member x.IndexFormat
            with get() : obj = h.GetObjectProperty("indexFormat") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("indexFormat", v)
        member x.VertexBuffers
            with get() : JSObject = h.GetObjectProperty("vertexBuffers") |> convert<JSObject>
            and set (v : JSObject) = h.SetObjectProperty("vertexBuffers", js v)
    [<AllowNullLiteral>]
    type WGPURenderPipelineDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPURenderPipelineDescriptor(new JSObject())
        member x.Label
            with get() : string = h.GetObjectProperty("label") |> convert<string>
            and set (v : string) = h.SetObjectProperty("label", js v)
        member x.Layout
            with get() : PipelineLayoutHandle = h.GetObjectProperty("layout") |> convert<PipelineLayoutHandle>
            and set (v : PipelineLayoutHandle) = h.SetObjectProperty("layout", js v)
        member x.VertexStage
            with get() : DawnRaw.WGPUProgrammableStageDescriptor = h.GetObjectProperty("vertexStage") |> convert<DawnRaw.WGPUProgrammableStageDescriptor>
            and set (v : DawnRaw.WGPUProgrammableStageDescriptor) = h.SetObjectProperty("vertexStage", js v)
        member x.FragmentStage
            with get() : DawnRaw.WGPUProgrammableStageDescriptor = h.GetObjectProperty("fragmentStage") |> convert<DawnRaw.WGPUProgrammableStageDescriptor>
            and set (v : DawnRaw.WGPUProgrammableStageDescriptor) = h.SetObjectProperty("fragmentStage", js v)
        member x.VertexState
            with get() : DawnRaw.WGPUVertexStateDescriptor = h.GetObjectProperty("vertexState") |> convert<DawnRaw.WGPUVertexStateDescriptor>
            and set (v : DawnRaw.WGPUVertexStateDescriptor) = h.SetObjectProperty("vertexState", js v)
        member x.PrimitiveTopology
            with get() : obj = h.GetObjectProperty("primitiveTopology") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("primitiveTopology", v)
        member x.RasterizationState
            with get() : DawnRaw.WGPURasterizationStateDescriptor = h.GetObjectProperty("rasterizationState") |> convert<DawnRaw.WGPURasterizationStateDescriptor>
            and set (v : DawnRaw.WGPURasterizationStateDescriptor) = h.SetObjectProperty("rasterizationState", js v)
        member x.SampleCount
            with get() : int = h.GetObjectProperty("sampleCount") |> convert<int>
            and set (v : int) = h.SetObjectProperty("sampleCount", js v)
        member x.DepthStencilState
            with get() : DawnRaw.WGPUDepthStencilStateDescriptor = h.GetObjectProperty("depthStencilState") |> convert<DawnRaw.WGPUDepthStencilStateDescriptor>
            and set (v : DawnRaw.WGPUDepthStencilStateDescriptor) = h.SetObjectProperty("depthStencilState", js v)
        member x.ColorStates
            with get() : JSObject = h.GetObjectProperty("colorStates") |> convert<JSObject>
            and set (v : JSObject) = h.SetObjectProperty("colorStates", js v)
        member x.SampleMask
            with get() : int = h.GetObjectProperty("sampleMask") |> convert<int>
            and set (v : int) = h.SetObjectProperty("sampleMask", js v)
        member x.AlphaToCoverageEnabled
            with get() : bool = h.GetObjectProperty("alphaToCoverageEnabled") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("alphaToCoverageEnabled", js v)


type Color =
    {
        R : float
        G : float
        B : float
        A : float
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUColor -> 'a) : 'a = 
        let x = x
        let _R = (x.R)
        let _G = (x.G)
        let _B = (x.B)
        let _A = (x.A)
        let native = DawnRaw.WGPUColor()
        native.R <- _R
        native.G <- _G
        native.B <- _B
        native.A <- _A
        callback native
type CommandBufferDescriptor =
    {
        Label : string
    }
    static member Default : CommandBufferDescriptor =
        {
            Label = null
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUCommandBufferDescriptor -> 'a) : 'a = 
        let x = x
        let _Label = x.Label
        let native = DawnRaw.WGPUCommandBufferDescriptor()
        native.Label <- _Label
        callback native
type CommandEncoderDescriptor =
    {
        Label : string
    }
    static member Default : CommandEncoderDescriptor =
        {
            Label = null
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUCommandEncoderDescriptor -> 'a) : 'a = 
        let x = x
        let _Label = x.Label
        let native = DawnRaw.WGPUCommandEncoderDescriptor()
        native.Label <- _Label
        callback native
type ComputePassDescriptor =
    {
        Label : string
    }
    static member Default : ComputePassDescriptor =
        {
            Label = null
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUComputePassDescriptor -> 'a) : 'a = 
        let x = x
        let _Label = x.Label
        let native = DawnRaw.WGPUComputePassDescriptor()
        native.Label <- _Label
        callback native
type DeviceProperties =
    {
        TextureCompressionBC : bool
        ShaderFloat16 : bool
        PipelineStatisticsQuery : bool
        TimestampQuery : bool
    }
    static member Default : DeviceProperties =
        {
            TextureCompressionBC = false
            ShaderFloat16 = false
            PipelineStatisticsQuery = false
            TimestampQuery = false
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUDeviceProperties -> 'a) : 'a = 
        let x = x
        let _TextureCompressionBC = x.TextureCompressionBC
        let _ShaderFloat16 = x.ShaderFloat16
        let _PipelineStatisticsQuery = x.PipelineStatisticsQuery
        let _TimestampQuery = x.TimestampQuery
        let native = DawnRaw.WGPUDeviceProperties()
        native.TextureCompressionBC <- _TextureCompressionBC
        native.ShaderFloat16 <- _ShaderFloat16
        native.PipelineStatisticsQuery <- _PipelineStatisticsQuery
        native.TimestampQuery <- _TimestampQuery
        callback native
type Extent3D =
    {
        Width : int
        Height : int
        Depth : int
    }
    static member Default : Extent3D =
        {
            Width = 1
            Height = 1
            Depth = 1
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUExtent3D -> 'a) : 'a = 
        let x = x
        let _Width = int (x.Width)
        let _Height = int (x.Height)
        let _Depth = int (x.Depth)
        let native = DawnRaw.WGPUExtent3D()
        native.Width <- _Width
        native.Height <- _Height
        native.Depth <- _Depth
        callback native
type FenceDescriptor =
    {
        Label : string
        InitialValue : uint64
    }
    static member Default : FenceDescriptor =
        {
            Label = null
            InitialValue = 0UL
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUFenceDescriptor -> 'a) : 'a = 
        let x = x
        let _Label = x.Label
        let _InitialValue = int (x.InitialValue)
        let native = DawnRaw.WGPUFenceDescriptor()
        native.Label <- _Label
        native.InitialValue <- _InitialValue
        callback native
type InstanceDescriptor(h : JSObject) = 
    inherit JsObj(h)
    member inline internal x.Pin<'a>(callback : DawnRaw.WGPUInstanceDescriptor -> 'a) : 'a = 
        let native = DawnRaw.WGPUInstanceDescriptor()
        callback native
type Origin3D =
    {
        X : int
        Y : int
        Z : int
    }
    static member Default : Origin3D =
        {
            X = 0
            Y = 0
            Z = 0
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUOrigin3D -> 'a) : 'a = 
        let x = x
        let _X = int (x.X)
        let _Y = int (x.Y)
        let _Z = int (x.Z)
        let native = DawnRaw.WGPUOrigin3D()
        native.X <- _X
        native.Y <- _Y
        native.Z <- _Z
        callback native
type RenderBundleDescriptor =
    {
        Label : string
    }
    static member Default : RenderBundleDescriptor =
        {
            Label = null
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPURenderBundleDescriptor -> 'a) : 'a = 
        let x = x
        let _Label = x.Label
        let native = DawnRaw.WGPURenderBundleDescriptor()
        native.Label <- _Label
        callback native
type SamplerDescriptorDummyAnisotropicFiltering =
    {
        MaxAnisotropy : float32
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUSamplerDescriptorDummyAnisotropicFiltering -> 'a) : 'a = 
        let x = x
        let _MaxAnisotropy = (x.MaxAnisotropy)
        let native = DawnRaw.WGPUSamplerDescriptorDummyAnisotropicFiltering()
        native.MaxAnisotropy <- _MaxAnisotropy
        callback native
type ShaderModuleSPIRVDescriptor =
    {
        Code : uint32[]
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUShaderModuleSPIRVDescriptor -> 'a) : 'a = 
        let x = x
        use _Code = Uint32Array.op_Implicit(Span(x.Code))
        let _CodeCount = x.Code.Length
        let native = DawnRaw.WGPUShaderModuleSPIRVDescriptor()
        native.Code <- _Code
        callback native
type ShaderModuleWGSLDescriptor =
    {
        Source : string
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUShaderModuleWGSLDescriptor -> 'a) : 'a = 
        let x = x
        let _Source = x.Source
        let native = DawnRaw.WGPUShaderModuleWGSLDescriptor()
        native.Source <- _Source
        callback native
type ShaderModuleDescriptor =
    {
        Label : string
    }
    static member Default : ShaderModuleDescriptor =
        {
            Label = null
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUShaderModuleDescriptor -> 'a) : 'a = 
        let x = x
        let _Label = x.Label
        let native = DawnRaw.WGPUShaderModuleDescriptor()
        native.Label <- _Label
        callback native
type SurfaceDescriptor =
    {
        Label : string
    }
    static member Default : SurfaceDescriptor =
        {
            Label = null
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUSurfaceDescriptor -> 'a) : 'a = 
        let x = x
        let _Label = x.Label
        let native = DawnRaw.WGPUSurfaceDescriptor()
        native.Label <- _Label
        callback native
type SurfaceDescriptorFromCanvasHTMLSelector =
    {
        Selector : string
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUSurfaceDescriptorFromCanvasHTMLSelector -> 'a) : 'a = 
        let x = x
        let _Selector = x.Selector
        let native = DawnRaw.WGPUSurfaceDescriptorFromCanvasHTMLSelector()
        native.Selector <- _Selector
        callback native
type SurfaceDescriptorFromMetalLayer =
    {
        Layer : nativeint
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUSurfaceDescriptorFromMetalLayer -> 'a) : 'a = 
        let x = x
        let _Layer = int (x.Layer)
        let native = DawnRaw.WGPUSurfaceDescriptorFromMetalLayer()
        native.Layer <- _Layer
        callback native
type SurfaceDescriptorFromWindowsHWND =
    {
        Hinstance : nativeint
        Hwnd : nativeint
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUSurfaceDescriptorFromWindowsHWND -> 'a) : 'a = 
        let x = x
        let _Hinstance = int (x.Hinstance)
        let _Hwnd = int (x.Hwnd)
        let native = DawnRaw.WGPUSurfaceDescriptorFromWindowsHWND()
        native.Hinstance <- _Hinstance
        native.Hwnd <- _Hwnd
        callback native
type SurfaceDescriptorFromXlib =
    {
        Display : nativeint
        Window : int
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUSurfaceDescriptorFromXlib -> 'a) : 'a = 
        let x = x
        let _Display = int (x.Display)
        let _Window = int (x.Window)
        let native = DawnRaw.WGPUSurfaceDescriptorFromXlib()
        native.Display <- _Display
        native.Window <- _Window
        callback native
type TextureDataLayout =
    {
        Offset : uint64
        BytesPerRow : int
        RowsPerImage : int
    }
    static member Default(BytesPerRow: int) : TextureDataLayout =
        {
            Offset = 0UL
            BytesPerRow = BytesPerRow
            RowsPerImage = 0
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUTextureDataLayout -> 'a) : 'a = 
        let x = x
        let _Offset = int (x.Offset)
        let _BytesPerRow = int (x.BytesPerRow)
        let _RowsPerImage = int (x.RowsPerImage)
        let native = DawnRaw.WGPUTextureDataLayout()
        native.Offset <- _Offset
        native.BytesPerRow <- _BytesPerRow
        native.RowsPerImage <- _RowsPerImage
        callback native
type AdapterProperties =
    {
        DeviceID : int
        VendorID : int
        Name : string
        DriverDescription : string
        AdapterType : AdapterType
        BackendType : BackendType
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUAdapterProperties -> 'a) : 'a = 
        let x = x
        let _DeviceID = int (x.DeviceID)
        let _VendorID = int (x.VendorID)
        let _Name = x.Name
        let _DriverDescription = x.DriverDescription
        let _AdapterType = x.AdapterType.GetValue()
        let _BackendType = x.BackendType.GetValue()
        let native = DawnRaw.WGPUAdapterProperties()
        native.DeviceID <- _DeviceID
        native.VendorID <- _VendorID
        native.Name <- _Name
        native.DriverDescription <- _DriverDescription
        native.AdapterType <- _AdapterType
        native.BackendType <- _BackendType
        callback native
type BindGroupLayoutEntry =
    {
        Binding : int
        Visibility : ShaderStage
        Type : BindingType
        HasDynamicOffset : bool
        MinBufferBindingSize : uint64
        Multisampled : bool
        ViewDimension : TextureViewDimension
        TextureComponentType : TextureComponentType
        StorageTextureFormat : TextureFormat
    }
    static member Default(Binding: int, Visibility: ShaderStage, Type: BindingType) : BindGroupLayoutEntry =
        {
            Binding = Binding
            Visibility = Visibility
            Type = Type
            HasDynamicOffset = false
            MinBufferBindingSize = 0UL
            Multisampled = false
            ViewDimension = TextureViewDimension.Undefined
            TextureComponentType = TextureComponentType.Float
            StorageTextureFormat = TextureFormat.Undefined
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUBindGroupLayoutEntry -> 'a) : 'a = 
        let x = x
        let _Binding = int (x.Binding)
        let _Visibility = int (x.Visibility)
        let _Type = x.Type.GetValue()
        let _HasDynamicOffset = x.HasDynamicOffset
        let _MinBufferBindingSize = int (x.MinBufferBindingSize)
        let _Multisampled = x.Multisampled
        let _ViewDimension = x.ViewDimension.GetValue()
        let _TextureComponentType = x.TextureComponentType.GetValue()
        let _StorageTextureFormat = x.StorageTextureFormat.GetValue()
        let native = DawnRaw.WGPUBindGroupLayoutEntry()
        native.Binding <- _Binding
        native.Visibility <- _Visibility
        native.Type <- _Type
        native.HasDynamicOffset <- _HasDynamicOffset
        native.MinBufferBindingSize <- _MinBufferBindingSize
        native.Multisampled <- _Multisampled
        native.ViewDimension <- _ViewDimension
        native.TextureComponentType <- _TextureComponentType
        native.StorageTextureFormat <- _StorageTextureFormat
        callback native
type BlendDescriptor =
    {
        Operation : BlendOperation
        SrcFactor : BlendFactor
        DstFactor : BlendFactor
    }
    static member Default : BlendDescriptor =
        {
            Operation = BlendOperation.Add
            SrcFactor = BlendFactor.One
            DstFactor = BlendFactor.Zero
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUBlendDescriptor -> 'a) : 'a = 
        let x = x
        let _Operation = x.Operation.GetValue()
        let _SrcFactor = x.SrcFactor.GetValue()
        let _DstFactor = x.DstFactor.GetValue()
        let native = DawnRaw.WGPUBlendDescriptor()
        native.Operation <- _Operation
        native.SrcFactor <- _SrcFactor
        native.DstFactor <- _DstFactor
        callback native
type BufferDescriptor =
    {
        Label : string
        Usage : BufferUsage
        Size : uint64
        MappedAtCreation : bool
    }
    static member Default(Usage: BufferUsage, Size: uint64) : BufferDescriptor =
        {
            Label = null
            Usage = Usage
            Size = Size
            MappedAtCreation = false
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUBufferDescriptor -> 'a) : 'a = 
        let x = x
        let _Label = x.Label
        let _Usage = int (x.Usage)
        let _Size = int (x.Size)
        let _MappedAtCreation = x.MappedAtCreation
        let native = DawnRaw.WGPUBufferDescriptor()
        native.Label <- _Label
        native.Usage <- _Usage
        native.Size <- _Size
        native.MappedAtCreation <- _MappedAtCreation
        callback native
type PipelineLayoutDescriptor =
    {
        Label : string
        BindGroupLayouts : array<BindGroupLayout>
    }
    static member Default(BindGroupLayouts: array<BindGroupLayout>) : PipelineLayoutDescriptor =
        {
            Label = null
            BindGroupLayouts = BindGroupLayouts
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUPipelineLayoutDescriptor -> 'a) : 'a = 
        let x = x
        let _Label = x.Label
        let _BindGroupLayoutsCount = x.BindGroupLayouts.Length
        let _BindGroupLayoutsArray = newArray _BindGroupLayoutsCount
        for i in 0 .. _BindGroupLayoutsCount-1 do
            if isNull x.BindGroupLayouts.[i] then _BindGroupLayoutsArray.[i] <- null
            else _BindGroupLayoutsArray.[i] <- x.BindGroupLayouts.[i].Handle
        let _BindGroupLayouts = _BindGroupLayoutsArray.Reference
        let native = DawnRaw.WGPUPipelineLayoutDescriptor()
        native.Label <- _Label
        native.BindGroupLayouts <- _BindGroupLayouts
        callback native
type ProgrammableStageDescriptor =
    {
        Module : ShaderModule
        EntryPoint : string
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUProgrammableStageDescriptor -> 'a) : 'a = 
        let x = x
        let _Module = (if isNull x.Module then null else x.Module.Handle)
        let _EntryPoint = x.EntryPoint
        let native = DawnRaw.WGPUProgrammableStageDescriptor()
        native.Module <- _Module
        native.EntryPoint <- _EntryPoint
        callback native
type QuerySetDescriptor =
    {
        Label : string
        Type : QueryType
        Count : int
        PipelineStatistics : option<PipelineStatisticName>
        PipelineStatisticsCount : int
    }
    static member Default(Type: QueryType, Count: int, PipelineStatistics: option<PipelineStatisticName>) : QuerySetDescriptor =
        {
            Label = null
            Type = Type
            Count = Count
            PipelineStatistics = PipelineStatistics
            PipelineStatisticsCount = 0
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUQuerySetDescriptor -> 'a) : 'a = 
        let x = x
        let _Label = x.Label
        let _Type = x.Type.GetValue()
        let _Count = int (x.Count)
        let inline _PipelineStatisticsCont _PipelineStatistics =
            let _PipelineStatisticsCount = int (x.PipelineStatisticsCount)
            let native = DawnRaw.WGPUQuerySetDescriptor()
            native.Label <- _Label
            native.Type <- _Type
            native.Count <- _Count
            native.PipelineStatistics <- _PipelineStatistics
            native.PipelineStatisticsCount <- _PipelineStatisticsCount
            callback native
        match x.PipelineStatistics with
        | Some o ->
            _PipelineStatisticsCont(o.GetValue())
        | _ ->
            _PipelineStatisticsCont null
type RasterizationStateDescriptor =
    {
        FrontFace : FrontFace
        CullMode : CullMode
        DepthBias : int32
        DepthBiasSlopeScale : float32
        DepthBiasClamp : float32
    }
    static member Default : RasterizationStateDescriptor =
        {
            FrontFace = FrontFace.CCW
            CullMode = CullMode.None
            DepthBias = 0
            DepthBiasSlopeScale = 0.000000f
            DepthBiasClamp = 0.000000f
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPURasterizationStateDescriptor -> 'a) : 'a = 
        let x = x
        let _FrontFace = x.FrontFace.GetValue()
        let _CullMode = x.CullMode.GetValue()
        let _DepthBias = int (x.DepthBias)
        let _DepthBiasSlopeScale = (x.DepthBiasSlopeScale)
        let _DepthBiasClamp = (x.DepthBiasClamp)
        let native = DawnRaw.WGPURasterizationStateDescriptor()
        native.FrontFace <- _FrontFace
        native.CullMode <- _CullMode
        native.DepthBias <- _DepthBias
        native.DepthBiasSlopeScale <- _DepthBiasSlopeScale
        native.DepthBiasClamp <- _DepthBiasClamp
        callback native
type RenderBundleEncoderDescriptor =
    {
        Label : string
        ColorFormats : array<TextureFormat>
        DepthStencilFormat : TextureFormat
        SampleCount : int
    }
    static member Default(ColorFormats: array<TextureFormat>) : RenderBundleEncoderDescriptor =
        {
            Label = null
            ColorFormats = ColorFormats
            DepthStencilFormat = TextureFormat.Undefined
            SampleCount = 1
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPURenderBundleEncoderDescriptor -> 'a) : 'a = 
        let x = x
        let _Label = x.Label
        let _ColorFormatsCount = x.ColorFormats.Length
        let _ColorFormatsArray = newArray (_ColorFormatsCount)
        for i in 0 .. _ColorFormatsCount-1 do
            _ColorFormatsArray.[i] <- x.ColorFormats.[i].GetValue()
        let _ColorFormats = _ColorFormatsArray.Reference
        let _DepthStencilFormat = x.DepthStencilFormat.GetValue()
        let _SampleCount = int (x.SampleCount)
        let native = DawnRaw.WGPURenderBundleEncoderDescriptor()
        native.Label <- _Label
        native.ColorFormats <- _ColorFormats
        native.DepthStencilFormat <- _DepthStencilFormat
        native.SampleCount <- _SampleCount
        callback native
type RenderPassColorAttachmentDescriptor =
    {
        Attachment : TextureView
        ResolveTarget : TextureView
        LoadValue : LoadOp
        StoreOp : StoreOp
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPURenderPassColorAttachmentDescriptor -> 'a) : 'a = 
        let x = x
        let _Attachment = (if isNull x.Attachment then null else x.Attachment.Handle)
        let _ResolveTarget = (if isNull x.ResolveTarget then null else x.ResolveTarget.Handle)
        let _LoadValue = x.LoadValue.GetValue()
        let _StoreOp = x.StoreOp.GetValue()
        let native = DawnRaw.WGPURenderPassColorAttachmentDescriptor()
        native.Attachment <- _Attachment
        native.ResolveTarget <- _ResolveTarget
        native.LoadValue <- _LoadValue
        native.StoreOp <- _StoreOp
        callback native
type RenderPassDepthStencilAttachmentDescriptor =
    {
        Attachment : TextureView
        DepthLoadValue : DepthLoadOp
        DepthStoreOp : StoreOp
        DepthReadOnly : bool
        StencilLoadValue : StencilLoadOp
        StencilStoreOp : StoreOp
        StencilReadOnly : bool
    }
    static member Default(Attachment: TextureView, DepthLoadValue: DepthLoadOp, DepthStoreOp: StoreOp, StencilLoadValue: StencilLoadOp, StencilStoreOp: StoreOp) : RenderPassDepthStencilAttachmentDescriptor =
        {
            Attachment = Attachment
            DepthLoadValue = DepthLoadValue
            DepthStoreOp = DepthStoreOp
            DepthReadOnly = false
            StencilLoadValue = StencilLoadValue
            StencilStoreOp = StencilStoreOp
            StencilReadOnly = false
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPURenderPassDepthStencilAttachmentDescriptor -> 'a) : 'a = 
        let x = x
        let _Attachment = (if isNull x.Attachment then null else x.Attachment.Handle)
        let _DepthLoadValue = x.DepthLoadValue.GetValue()
        let _DepthStoreOp = x.DepthStoreOp.GetValue()
        let _DepthReadOnly = x.DepthReadOnly
        let _StencilLoadValue = x.StencilLoadValue.GetValue()
        let _StencilStoreOp = x.StencilStoreOp.GetValue()
        let _StencilReadOnly = x.StencilReadOnly
        let native = DawnRaw.WGPURenderPassDepthStencilAttachmentDescriptor()
        native.Attachment <- _Attachment
        native.DepthLoadValue <- _DepthLoadValue
        native.DepthStoreOp <- _DepthStoreOp
        native.DepthReadOnly <- _DepthReadOnly
        native.StencilLoadValue <- _StencilLoadValue
        native.StencilStoreOp <- _StencilStoreOp
        native.StencilReadOnly <- _StencilReadOnly
        callback native
type SamplerDescriptor =
    {
        Label : string
        AddressModeU : AddressMode
        AddressModeV : AddressMode
        AddressModeW : AddressMode
        MagFilter : FilterMode
        MinFilter : FilterMode
        MipmapFilter : FilterMode
        LodMinClamp : float32
        LodMaxClamp : float32
        Compare : CompareFunction
    }
    static member Default : SamplerDescriptor =
        {
            Label = null
            AddressModeU = AddressMode.ClampToEdge
            AddressModeV = AddressMode.ClampToEdge
            AddressModeW = AddressMode.ClampToEdge
            MagFilter = FilterMode.Nearest
            MinFilter = FilterMode.Nearest
            MipmapFilter = FilterMode.Nearest
            LodMinClamp = 0.000000f
            LodMaxClamp = 1000.000000f
            Compare = CompareFunction.Undefined
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUSamplerDescriptor -> 'a) : 'a = 
        let x = x
        let _Label = x.Label
        let _AddressModeU = x.AddressModeU.GetValue()
        let _AddressModeV = x.AddressModeV.GetValue()
        let _AddressModeW = x.AddressModeW.GetValue()
        let _MagFilter = x.MagFilter.GetValue()
        let _MinFilter = x.MinFilter.GetValue()
        let _MipmapFilter = x.MipmapFilter.GetValue()
        let _LodMinClamp = (x.LodMinClamp)
        let _LodMaxClamp = (x.LodMaxClamp)
        let _Compare = x.Compare.GetValue()
        let native = DawnRaw.WGPUSamplerDescriptor()
        native.Label <- _Label
        native.AddressModeU <- _AddressModeU
        native.AddressModeV <- _AddressModeV
        native.AddressModeW <- _AddressModeW
        native.MagFilter <- _MagFilter
        native.MinFilter <- _MinFilter
        native.MipmapFilter <- _MipmapFilter
        native.LodMinClamp <- _LodMinClamp
        native.LodMaxClamp <- _LodMaxClamp
        native.Compare <- _Compare
        callback native
type StencilStateFaceDescriptor =
    {
        Compare : CompareFunction
        FailOp : StencilOperation
        DepthFailOp : StencilOperation
        PassOp : StencilOperation
    }
    static member Default : StencilStateFaceDescriptor =
        {
            Compare = CompareFunction.Always
            FailOp = StencilOperation.Keep
            DepthFailOp = StencilOperation.Keep
            PassOp = StencilOperation.Keep
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUStencilStateFaceDescriptor -> 'a) : 'a = 
        let x = x
        let _Compare = x.Compare.GetValue()
        let _FailOp = x.FailOp.GetValue()
        let _DepthFailOp = x.DepthFailOp.GetValue()
        let _PassOp = x.PassOp.GetValue()
        let native = DawnRaw.WGPUStencilStateFaceDescriptor()
        native.Compare <- _Compare
        native.FailOp <- _FailOp
        native.DepthFailOp <- _DepthFailOp
        native.PassOp <- _PassOp
        callback native
type SwapChainDescriptor =
    {
        Label : string
        Usage : TextureUsage
        Format : TextureFormat
        Width : int
        Height : int
        PresentMode : PresentMode
        Implementation : uint64
    }
    static member Default(Usage: TextureUsage, Format: TextureFormat, Width: int, Height: int, PresentMode: PresentMode) : SwapChainDescriptor =
        {
            Label = null
            Usage = Usage
            Format = Format
            Width = Width
            Height = Height
            PresentMode = PresentMode
            Implementation = 0UL
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUSwapChainDescriptor -> 'a) : 'a = 
        let x = x
        let _Label = x.Label
        let _Usage = int (x.Usage)
        let _Format = x.Format.GetValue()
        let _Width = int (x.Width)
        let _Height = int (x.Height)
        let _PresentMode = x.PresentMode.GetValue()
        let _Implementation = int (x.Implementation)
        let native = DawnRaw.WGPUSwapChainDescriptor()
        native.Label <- _Label
        native.Usage <- _Usage
        native.Format <- _Format
        native.Width <- _Width
        native.Height <- _Height
        native.PresentMode <- _PresentMode
        native.Implementation <- _Implementation
        callback native
type TextureDescriptor =
    {
        Label : string
        Usage : TextureUsage
        Dimension : TextureDimension
        Size : Extent3D
        Format : TextureFormat
        MipLevelCount : int
        SampleCount : int
    }
    static member Default(Usage: TextureUsage, Format: TextureFormat) : TextureDescriptor =
        {
            Label = null
            Usage = Usage
            Dimension = TextureDimension.D2D
            Size = Extent3D.Default
            Format = Format
            MipLevelCount = 1
            SampleCount = 1
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUTextureDescriptor -> 'a) : 'a = 
        let x = x
        let _Label = x.Label
        let _Usage = int (x.Usage)
        let _Dimension = x.Dimension.GetValue()
        let _Width = int (x.Size.Width)
        let _Height = int (x.Size.Height)
        let _Depth = int (x.Size.Depth)
        let _Size = new DawnRaw.WGPUExtent3D()
        _Size.Width <- _Width
        _Size.Height <- _Height
        _Size.Depth <- _Depth
        let _Size = _Size
        let _Format = x.Format.GetValue()
        let _MipLevelCount = int (x.MipLevelCount)
        let _SampleCount = int (x.SampleCount)
        let native = DawnRaw.WGPUTextureDescriptor()
        native.Label <- _Label
        native.Usage <- _Usage
        native.Dimension <- _Dimension
        native.Size <- _Size
        native.Format <- _Format
        native.MipLevelCount <- _MipLevelCount
        native.SampleCount <- _SampleCount
        callback native
type TextureViewDescriptor =
    {
        Label : string
        Format : TextureFormat
        Dimension : TextureViewDimension
        BaseMipLevel : int
        MipLevelCount : int
        BaseArrayLayer : int
        ArrayLayerCount : int
        Aspect : TextureAspect
    }
    static member Default : TextureViewDescriptor =
        {
            Label = null
            Format = TextureFormat.Undefined
            Dimension = TextureViewDimension.Undefined
            BaseMipLevel = 0
            MipLevelCount = 0
            BaseArrayLayer = 0
            ArrayLayerCount = 0
            Aspect = TextureAspect.All
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUTextureViewDescriptor -> 'a) : 'a = 
        let x = x
        let _Label = x.Label
        let _Format = x.Format.GetValue()
        let _Dimension = x.Dimension.GetValue()
        let _BaseMipLevel = int (x.BaseMipLevel)
        let _MipLevelCount = int (x.MipLevelCount)
        let _BaseArrayLayer = int (x.BaseArrayLayer)
        let _ArrayLayerCount = int (x.ArrayLayerCount)
        let _Aspect = x.Aspect.GetValue()
        let native = DawnRaw.WGPUTextureViewDescriptor()
        native.Label <- _Label
        native.Format <- _Format
        native.Dimension <- _Dimension
        native.BaseMipLevel <- _BaseMipLevel
        native.MipLevelCount <- _MipLevelCount
        native.BaseArrayLayer <- _BaseArrayLayer
        native.ArrayLayerCount <- _ArrayLayerCount
        native.Aspect <- _Aspect
        callback native
type VertexAttributeDescriptor =
    {
        Format : VertexFormat
        Offset : uint64
        ShaderLocation : int
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUVertexAttributeDescriptor -> 'a) : 'a = 
        let x = x
        let _Format = x.Format.GetValue()
        let _Offset = int (x.Offset)
        let _ShaderLocation = int (x.ShaderLocation)
        let native = DawnRaw.WGPUVertexAttributeDescriptor()
        native.Format <- _Format
        native.Offset <- _Offset
        native.ShaderLocation <- _ShaderLocation
        callback native
type BindGroupEntry =
    {
        Binding : int
        Buffer : Buffer
        Offset : uint64
        Size : uint64
        Sampler : Sampler
        TextureView : TextureView
    }
    static member Default(Binding: int, Buffer: Buffer, Size: uint64, Sampler: Sampler, TextureView: TextureView) : BindGroupEntry =
        {
            Binding = Binding
            Buffer = Buffer
            Offset = 0UL
            Size = Size
            Sampler = Sampler
            TextureView = TextureView
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUBindGroupEntry -> 'a) : 'a = 
        let x = x
        let _Binding = int (x.Binding)
        let _Buffer = (if isNull x.Buffer then null else x.Buffer.Handle)
        let _Offset = int (x.Offset)
        let _Size = int (x.Size)
        let _Sampler = (if isNull x.Sampler then null else x.Sampler.Handle)
        let _TextureView = (if isNull x.TextureView then null else x.TextureView.Handle)
        let native = DawnRaw.WGPUBindGroupEntry()
        native.Binding <- _Binding
        native.Buffer <- _Buffer
        native.Offset <- _Offset
        native.Size <- _Size
        native.Sampler <- _Sampler
        native.TextureView <- _TextureView
        callback native
type BindGroupLayoutDescriptor =
    {
        Label : string
        Entries : array<BindGroupLayoutEntry>
    }
    static member Default(Entries: array<BindGroupLayoutEntry>) : BindGroupLayoutDescriptor =
        {
            Label = null
            Entries = Entries
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUBindGroupLayoutDescriptor -> 'a) : 'a = 
        let x = x
        let _Label = x.Label
        let _EntriesCount = if isNull x.Entries then 0 else x.Entries.Length
        let rec _EntriesCont (_Entriesinputs : array<BindGroupLayoutEntry>) (_Entriesoutputs : JsArray) (_Entriesi : int) =
            if _Entriesi >= _EntriesCount then
                let _Entries = _Entriesoutputs.Reference
                let native = DawnRaw.WGPUBindGroupLayoutDescriptor()
                native.Label <- _Label
                native.Entries <- _Entries
                callback native
            else
                let _Binding = int (_Entriesinputs.[_Entriesi].Binding)
                let _Visibility = int (_Entriesinputs.[_Entriesi].Visibility)
                let _Type = _Entriesinputs.[_Entriesi].Type.GetValue()
                let _HasDynamicOffset = _Entriesinputs.[_Entriesi].HasDynamicOffset
                let _MinBufferBindingSize = int (_Entriesinputs.[_Entriesi].MinBufferBindingSize)
                let _Multisampled = _Entriesinputs.[_Entriesi].Multisampled
                let _ViewDimension = _Entriesinputs.[_Entriesi].ViewDimension.GetValue()
                let _TextureComponentType = _Entriesinputs.[_Entriesi].TextureComponentType.GetValue()
                let _StorageTextureFormat = _Entriesinputs.[_Entriesi].StorageTextureFormat.GetValue()
                let _n = new DawnRaw.WGPUBindGroupLayoutEntry()
                _n.Binding <- _Binding
                _n.Visibility <- _Visibility
                _n.Type <- _Type
                _n.HasDynamicOffset <- _HasDynamicOffset
                _n.MinBufferBindingSize <- _MinBufferBindingSize
                _n.Multisampled <- _Multisampled
                _n.ViewDimension <- _ViewDimension
                _n.TextureComponentType <- _TextureComponentType
                _n.StorageTextureFormat <- _StorageTextureFormat
                let _n = _n
                _Entriesoutputs.[_Entriesi] <- _n
                _EntriesCont _Entriesinputs _Entriesoutputs (_Entriesi + 1)
        _EntriesCont x.Entries (if _EntriesCount > 0 then newArray _EntriesCount else null) 0
type BufferCopyView =
    {
        Layout : TextureDataLayout
        Buffer : Buffer
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUBufferCopyView -> 'a) : 'a = 
        let x = x
        let _Offset = int (x.Layout.Offset)
        let _BytesPerRow = int (x.Layout.BytesPerRow)
        let _RowsPerImage = int (x.Layout.RowsPerImage)
        let _Layout = new DawnRaw.WGPUTextureDataLayout()
        _Layout.Offset <- _Offset
        _Layout.BytesPerRow <- _BytesPerRow
        _Layout.RowsPerImage <- _RowsPerImage
        let _Layout = _Layout
        let _Buffer = (if isNull x.Buffer then null else x.Buffer.Handle)
        let native = DawnRaw.WGPUBufferCopyView()
        native.Layout <- _Layout
        native.Buffer <- _Buffer
        callback native
type ColorStateDescriptor =
    {
        Format : TextureFormat
        AlphaBlend : BlendDescriptor
        ColorBlend : BlendDescriptor
        WriteMask : ColorWriteMask
    }
    static member Default(Format: TextureFormat) : ColorStateDescriptor =
        {
            Format = Format
            AlphaBlend = BlendDescriptor.Default
            ColorBlend = BlendDescriptor.Default
            WriteMask = ColorWriteMask.All
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUColorStateDescriptor -> 'a) : 'a = 
        let x = x
        let _Format = x.Format.GetValue()
        let _Operation = x.AlphaBlend.Operation.GetValue()
        let _SrcFactor = x.AlphaBlend.SrcFactor.GetValue()
        let _DstFactor = x.AlphaBlend.DstFactor.GetValue()
        let _AlphaBlend = new DawnRaw.WGPUBlendDescriptor()
        _AlphaBlend.Operation <- _Operation
        _AlphaBlend.SrcFactor <- _SrcFactor
        _AlphaBlend.DstFactor <- _DstFactor
        let _AlphaBlend = _AlphaBlend
        let _Operation = x.ColorBlend.Operation.GetValue()
        let _SrcFactor = x.ColorBlend.SrcFactor.GetValue()
        let _DstFactor = x.ColorBlend.DstFactor.GetValue()
        let _ColorBlend = new DawnRaw.WGPUBlendDescriptor()
        _ColorBlend.Operation <- _Operation
        _ColorBlend.SrcFactor <- _SrcFactor
        _ColorBlend.DstFactor <- _DstFactor
        let _ColorBlend = _ColorBlend
        let _WriteMask = int (x.WriteMask)
        let native = DawnRaw.WGPUColorStateDescriptor()
        native.Format <- _Format
        native.AlphaBlend <- _AlphaBlend
        native.ColorBlend <- _ColorBlend
        native.WriteMask <- _WriteMask
        callback native
type ComputePipelineDescriptor =
    {
        Label : string
        Layout : PipelineLayout
        ComputeStage : ProgrammableStageDescriptor
    }
    static member Default(Layout: PipelineLayout, ComputeStage: ProgrammableStageDescriptor) : ComputePipelineDescriptor =
        {
            Label = null
            Layout = Layout
            ComputeStage = ComputeStage
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUComputePipelineDescriptor -> 'a) : 'a = 
        let x = x
        let _Label = x.Label
        let _Layout = (if isNull x.Layout then null else x.Layout.Handle)
        let _Module = (if isNull x.ComputeStage.Module then null else x.ComputeStage.Module.Handle)
        let _EntryPoint = x.ComputeStage.EntryPoint
        let _ComputeStage = new DawnRaw.WGPUProgrammableStageDescriptor()
        _ComputeStage.Module <- _Module
        _ComputeStage.EntryPoint <- _EntryPoint
        let _ComputeStage = _ComputeStage
        let native = DawnRaw.WGPUComputePipelineDescriptor()
        native.Label <- _Label
        native.Layout <- _Layout
        native.ComputeStage <- _ComputeStage
        callback native
type DepthStencilStateDescriptor =
    {
        Format : TextureFormat
        DepthWriteEnabled : bool
        DepthCompare : CompareFunction
        StencilFront : StencilStateFaceDescriptor
        StencilBack : StencilStateFaceDescriptor
        StencilReadMask : int
        StencilWriteMask : int
    }
    static member Default(Format: TextureFormat) : DepthStencilStateDescriptor =
        {
            Format = Format
            DepthWriteEnabled = false
            DepthCompare = CompareFunction.Always
            StencilFront = StencilStateFaceDescriptor.Default
            StencilBack = StencilStateFaceDescriptor.Default
            StencilReadMask = -1
            StencilWriteMask = -1
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUDepthStencilStateDescriptor -> 'a) : 'a = 
        let x = x
        let _Format = x.Format.GetValue()
        let _DepthWriteEnabled = x.DepthWriteEnabled
        let _DepthCompare = x.DepthCompare.GetValue()
        let _Compare = x.StencilFront.Compare.GetValue()
        let _FailOp = x.StencilFront.FailOp.GetValue()
        let _DepthFailOp = x.StencilFront.DepthFailOp.GetValue()
        let _PassOp = x.StencilFront.PassOp.GetValue()
        let _StencilFront = new DawnRaw.WGPUStencilStateFaceDescriptor()
        _StencilFront.Compare <- _Compare
        _StencilFront.FailOp <- _FailOp
        _StencilFront.DepthFailOp <- _DepthFailOp
        _StencilFront.PassOp <- _PassOp
        let _StencilFront = _StencilFront
        let _Compare = x.StencilBack.Compare.GetValue()
        let _FailOp = x.StencilBack.FailOp.GetValue()
        let _DepthFailOp = x.StencilBack.DepthFailOp.GetValue()
        let _PassOp = x.StencilBack.PassOp.GetValue()
        let _StencilBack = new DawnRaw.WGPUStencilStateFaceDescriptor()
        _StencilBack.Compare <- _Compare
        _StencilBack.FailOp <- _FailOp
        _StencilBack.DepthFailOp <- _DepthFailOp
        _StencilBack.PassOp <- _PassOp
        let _StencilBack = _StencilBack
        let _StencilReadMask = int (x.StencilReadMask)
        let _StencilWriteMask = int (x.StencilWriteMask)
        let native = DawnRaw.WGPUDepthStencilStateDescriptor()
        native.Format <- _Format
        native.DepthWriteEnabled <- _DepthWriteEnabled
        native.DepthCompare <- _DepthCompare
        native.StencilFront <- _StencilFront
        native.StencilBack <- _StencilBack
        native.StencilReadMask <- _StencilReadMask
        native.StencilWriteMask <- _StencilWriteMask
        callback native
type RenderPassDescriptor =
    {
        Label : string
        ColorAttachments : array<RenderPassColorAttachmentDescriptor>
        DepthStencilAttachment : option<RenderPassDepthStencilAttachmentDescriptor>
        OcclusionQuerySet : QuerySet
    }
    static member Default(ColorAttachments: array<RenderPassColorAttachmentDescriptor>, DepthStencilAttachment: option<RenderPassDepthStencilAttachmentDescriptor>, OcclusionQuerySet: QuerySet) : RenderPassDescriptor =
        {
            Label = null
            ColorAttachments = ColorAttachments
            DepthStencilAttachment = DepthStencilAttachment
            OcclusionQuerySet = OcclusionQuerySet
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPURenderPassDescriptor -> 'a) : 'a = 
        let x = x
        let _Label = x.Label
        let _ColorAttachmentsCount = if isNull x.ColorAttachments then 0 else x.ColorAttachments.Length
        let rec _ColorAttachmentsCont (_ColorAttachmentsinputs : array<RenderPassColorAttachmentDescriptor>) (_ColorAttachmentsoutputs : JsArray) (_ColorAttachmentsi : int) =
            if _ColorAttachmentsi >= _ColorAttachmentsCount then
                let _ColorAttachments = _ColorAttachmentsoutputs.Reference
                let inline _DepthStencilAttachmentCont _DepthStencilAttachment = 
                    let _OcclusionQuerySet = (if isNull x.OcclusionQuerySet then null else x.OcclusionQuerySet.Handle)
                    let native = DawnRaw.WGPURenderPassDescriptor()
                    native.Label <- _Label
                    native.ColorAttachments <- _ColorAttachments
                    native.DepthStencilAttachment <- _DepthStencilAttachment
                    native.OcclusionQuerySet <- _OcclusionQuerySet
                    callback native
                match x.DepthStencilAttachment with
                | Some v ->
                    let _Attachment = (if isNull v.Attachment then null else v.Attachment.Handle)
                    let _DepthLoadValue = v.DepthLoadValue.GetValue()
                    let _DepthStoreOp = v.DepthStoreOp.GetValue()
                    let _DepthReadOnly = v.DepthReadOnly
                    let _StencilLoadValue = v.StencilLoadValue.GetValue()
                    let _StencilStoreOp = v.StencilStoreOp.GetValue()
                    let _StencilReadOnly = v.StencilReadOnly
                    let _n = new DawnRaw.WGPURenderPassDepthStencilAttachmentDescriptor()
                    _n.Attachment <- _Attachment
                    _n.DepthLoadValue <- _DepthLoadValue
                    _n.DepthStoreOp <- _DepthStoreOp
                    _n.DepthReadOnly <- _DepthReadOnly
                    _n.StencilLoadValue <- _StencilLoadValue
                    _n.StencilStoreOp <- _StencilStoreOp
                    _n.StencilReadOnly <- _StencilReadOnly
                    let _n = _n
                    _DepthStencilAttachmentCont _n
                | None -> _DepthStencilAttachmentCont null
            else
                let _Attachment = (if isNull _ColorAttachmentsinputs.[_ColorAttachmentsi].Attachment then null else _ColorAttachmentsinputs.[_ColorAttachmentsi].Attachment.Handle)
                let _ResolveTarget = (if isNull _ColorAttachmentsinputs.[_ColorAttachmentsi].ResolveTarget then null else _ColorAttachmentsinputs.[_ColorAttachmentsi].ResolveTarget.Handle)
                let _LoadValue = _ColorAttachmentsinputs.[_ColorAttachmentsi].LoadValue.GetValue()
                let _StoreOp = _ColorAttachmentsinputs.[_ColorAttachmentsi].StoreOp.GetValue()
                let _n = new DawnRaw.WGPURenderPassColorAttachmentDescriptor()
                _n.Attachment <- _Attachment
                _n.ResolveTarget <- _ResolveTarget
                _n.LoadValue <- _LoadValue
                _n.StoreOp <- _StoreOp
                let _n = _n
                _ColorAttachmentsoutputs.[_ColorAttachmentsi] <- _n
                _ColorAttachmentsCont _ColorAttachmentsinputs _ColorAttachmentsoutputs (_ColorAttachmentsi + 1)
        _ColorAttachmentsCont x.ColorAttachments (if _ColorAttachmentsCount > 0 then newArray _ColorAttachmentsCount else null) 0
type RenderPipelineDescriptorDummyExtension =
    {
        DummyStage : ProgrammableStageDescriptor
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPURenderPipelineDescriptorDummyExtension -> 'a) : 'a = 
        let x = x
        let _Module = (if isNull x.DummyStage.Module then null else x.DummyStage.Module.Handle)
        let _EntryPoint = x.DummyStage.EntryPoint
        let _DummyStage = new DawnRaw.WGPUProgrammableStageDescriptor()
        _DummyStage.Module <- _Module
        _DummyStage.EntryPoint <- _EntryPoint
        let _DummyStage = _DummyStage
        let native = DawnRaw.WGPURenderPipelineDescriptorDummyExtension()
        native.DummyStage <- _DummyStage
        callback native
type VertexBufferLayoutDescriptor =
    {
        ArrayStride : uint64
        StepMode : InputStepMode
        Attributes : array<VertexAttributeDescriptor>
    }
    static member Default(ArrayStride: uint64, Attributes: array<VertexAttributeDescriptor>) : VertexBufferLayoutDescriptor =
        {
            ArrayStride = ArrayStride
            StepMode = InputStepMode.Vertex
            Attributes = Attributes
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUVertexBufferLayoutDescriptor -> 'a) : 'a = 
        let x = x
        let _ArrayStride = int (x.ArrayStride)
        let _StepMode = x.StepMode.GetValue()
        let _AttributesCount = if isNull x.Attributes then 0 else x.Attributes.Length
        let rec _AttributesCont (_Attributesinputs : array<VertexAttributeDescriptor>) (_Attributesoutputs : JsArray) (_Attributesi : int) =
            if _Attributesi >= _AttributesCount then
                let _Attributes = _Attributesoutputs.Reference
                let native = DawnRaw.WGPUVertexBufferLayoutDescriptor()
                native.ArrayStride <- _ArrayStride
                native.StepMode <- _StepMode
                native.Attributes <- _Attributes
                callback native
            else
                let _Format = _Attributesinputs.[_Attributesi].Format.GetValue()
                let _Offset = int (_Attributesinputs.[_Attributesi].Offset)
                let _ShaderLocation = int (_Attributesinputs.[_Attributesi].ShaderLocation)
                let _n = new DawnRaw.WGPUVertexAttributeDescriptor()
                _n.Format <- _Format
                _n.Offset <- _Offset
                _n.ShaderLocation <- _ShaderLocation
                let _n = _n
                _Attributesoutputs.[_Attributesi] <- _n
                _AttributesCont _Attributesinputs _Attributesoutputs (_Attributesi + 1)
        _AttributesCont x.Attributes (if _AttributesCount > 0 then newArray _AttributesCount else null) 0
type BindGroupDescriptor =
    {
        Label : string
        Layout : BindGroupLayout
        Entries : array<BindGroupEntry>
    }
    static member Default(Layout: BindGroupLayout, Entries: array<BindGroupEntry>) : BindGroupDescriptor =
        {
            Label = null
            Layout = Layout
            Entries = Entries
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUBindGroupDescriptor -> 'a) : 'a = 
        let x = x
        let _Label = x.Label
        let _Layout = (if isNull x.Layout then null else x.Layout.Handle)
        let _EntriesCount = if isNull x.Entries then 0 else x.Entries.Length
        let rec _EntriesCont (_Entriesinputs : array<BindGroupEntry>) (_Entriesoutputs : JsArray) (_Entriesi : int) =
            if _Entriesi >= _EntriesCount then
                let _Entries = _Entriesoutputs.Reference
                let native = DawnRaw.WGPUBindGroupDescriptor()
                native.Label <- _Label
                native.Layout <- _Layout
                native.Entries <- _Entries
                callback native
            else
                let _Binding = int (_Entriesinputs.[_Entriesi].Binding)
                let _Buffer = (if isNull _Entriesinputs.[_Entriesi].Buffer then null else _Entriesinputs.[_Entriesi].Buffer.Handle)
                let _Offset = int (_Entriesinputs.[_Entriesi].Offset)
                let _Size = int (_Entriesinputs.[_Entriesi].Size)
                let _Sampler = (if isNull _Entriesinputs.[_Entriesi].Sampler then null else _Entriesinputs.[_Entriesi].Sampler.Handle)
                let _TextureView = (if isNull _Entriesinputs.[_Entriesi].TextureView then null else _Entriesinputs.[_Entriesi].TextureView.Handle)
                let _n = new DawnRaw.WGPUBindGroupEntry()
                _n.Binding <- _Binding
                _n.Buffer <- _Buffer
                _n.Offset <- _Offset
                _n.Size <- _Size
                _n.Sampler <- _Sampler
                _n.TextureView <- _TextureView
                let _n = _n
                _Entriesoutputs.[_Entriesi] <- _n
                _EntriesCont _Entriesinputs _Entriesoutputs (_Entriesi + 1)
        _EntriesCont x.Entries (if _EntriesCount > 0 then newArray _EntriesCount else null) 0
type TextureCopyView =
    {
        Texture : Texture
        MipLevel : int
        Origin : Origin3D
        Aspect : TextureAspect
    }
    static member Default(Texture: Texture) : TextureCopyView =
        {
            Texture = Texture
            MipLevel = 0
            Origin = Origin3D.Default
            Aspect = TextureAspect.All
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUTextureCopyView -> 'a) : 'a = 
        let x = x
        let _Texture = (if isNull x.Texture then null else x.Texture.Handle)
        let _MipLevel = int (x.MipLevel)
        let _X = int (x.Origin.X)
        let _Y = int (x.Origin.Y)
        let _Z = int (x.Origin.Z)
        let _Origin = new DawnRaw.WGPUOrigin3D()
        _Origin.X <- _X
        _Origin.Y <- _Y
        _Origin.Z <- _Z
        let _Origin = _Origin
        let _Aspect = x.Aspect.GetValue()
        let native = DawnRaw.WGPUTextureCopyView()
        native.Texture <- _Texture
        native.MipLevel <- _MipLevel
        native.Origin <- _Origin
        native.Aspect <- _Aspect
        callback native
type VertexStateDescriptor =
    {
        IndexFormat : IndexFormat
        VertexBuffers : array<VertexBufferLayoutDescriptor>
    }
    static member Default(VertexBuffers: array<VertexBufferLayoutDescriptor>) : VertexStateDescriptor =
        {
            IndexFormat = IndexFormat.Undefined
            VertexBuffers = VertexBuffers
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUVertexStateDescriptor -> 'a) : 'a = 
        let x = x
        let _IndexFormat = x.IndexFormat.GetValue()
        let _VertexBuffersCount = if isNull x.VertexBuffers then 0 else x.VertexBuffers.Length
        let rec _VertexBuffersCont (_VertexBuffersinputs : array<VertexBufferLayoutDescriptor>) (_VertexBuffersoutputs : JsArray) (_VertexBuffersi : int) =
            if _VertexBuffersi >= _VertexBuffersCount then
                let _VertexBuffers = _VertexBuffersoutputs.Reference
                let native = DawnRaw.WGPUVertexStateDescriptor()
                native.IndexFormat <- _IndexFormat
                native.VertexBuffers <- _VertexBuffers
                callback native
            else
                let _ArrayStride = int (_VertexBuffersinputs.[_VertexBuffersi].ArrayStride)
                let _StepMode = _VertexBuffersinputs.[_VertexBuffersi].StepMode.GetValue()
                let _AttributesCount = if isNull _VertexBuffersinputs.[_VertexBuffersi].Attributes then 0 else _VertexBuffersinputs.[_VertexBuffersi].Attributes.Length
                let rec _AttributesCont (_Attributesinputs : array<VertexAttributeDescriptor>) (_Attributesoutputs : JsArray) (_Attributesi : int) =
                    if _Attributesi >= _AttributesCount then
                        let _Attributes = _Attributesoutputs.Reference
                        let _n = new DawnRaw.WGPUVertexBufferLayoutDescriptor()
                        _n.ArrayStride <- _ArrayStride
                        _n.StepMode <- _StepMode
                        _n.Attributes <- _Attributes
                        let _n = _n
                        _VertexBuffersoutputs.[_VertexBuffersi] <- _n
                        _VertexBuffersCont _VertexBuffersinputs _VertexBuffersoutputs (_VertexBuffersi + 1)
                    else
                        let _Format = _Attributesinputs.[_Attributesi].Format.GetValue()
                        let _Offset = int (_Attributesinputs.[_Attributesi].Offset)
                        let _ShaderLocation = int (_Attributesinputs.[_Attributesi].ShaderLocation)
                        let _n = new DawnRaw.WGPUVertexAttributeDescriptor()
                        _n.Format <- _Format
                        _n.Offset <- _Offset
                        _n.ShaderLocation <- _ShaderLocation
                        let _n = _n
                        _Attributesoutputs.[_Attributesi] <- _n
                        _AttributesCont _Attributesinputs _Attributesoutputs (_Attributesi + 1)
                _AttributesCont _VertexBuffersinputs.[_VertexBuffersi].Attributes (if _AttributesCount > 0 then newArray _AttributesCount else null) 0
        _VertexBuffersCont x.VertexBuffers (if _VertexBuffersCount > 0 then newArray _VertexBuffersCount else null) 0
type RenderPipelineDescriptor =
    {
        Label : string
        Layout : PipelineLayout
        VertexStage : ProgrammableStageDescriptor
        FragmentStage : option<ProgrammableStageDescriptor>
        VertexState : option<VertexStateDescriptor>
        PrimitiveTopology : PrimitiveTopology
        RasterizationState : option<RasterizationStateDescriptor>
        SampleCount : int
        DepthStencilState : option<DepthStencilStateDescriptor>
        ColorStates : array<ColorStateDescriptor>
        SampleMask : int
        AlphaToCoverageEnabled : bool
    }
    static member Default(Layout: PipelineLayout, VertexStage: ProgrammableStageDescriptor, FragmentStage: option<ProgrammableStageDescriptor>, VertexState: option<VertexStateDescriptor>, PrimitiveTopology: PrimitiveTopology, RasterizationState: option<RasterizationStateDescriptor>, DepthStencilState: option<DepthStencilStateDescriptor>, ColorStates: array<ColorStateDescriptor>) : RenderPipelineDescriptor =
        {
            Label = null
            Layout = Layout
            VertexStage = VertexStage
            FragmentStage = FragmentStage
            VertexState = VertexState
            PrimitiveTopology = PrimitiveTopology
            RasterizationState = RasterizationState
            SampleCount = 1
            DepthStencilState = DepthStencilState
            ColorStates = ColorStates
            SampleMask = -1
            AlphaToCoverageEnabled = false
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPURenderPipelineDescriptor -> 'a) : 'a = 
        let x = x
        let _Label = x.Label
        let _Layout = (if isNull x.Layout then null else x.Layout.Handle)
        let _Module = (if isNull x.VertexStage.Module then null else x.VertexStage.Module.Handle)
        let _EntryPoint = x.VertexStage.EntryPoint
        let _VertexStage = new DawnRaw.WGPUProgrammableStageDescriptor()
        _VertexStage.Module <- _Module
        _VertexStage.EntryPoint <- _EntryPoint
        let _VertexStage = _VertexStage
        let inline _FragmentStageCont _FragmentStage = 
            let inline _VertexStateCont _VertexState = 
                let _PrimitiveTopology = x.PrimitiveTopology.GetValue()
                let inline _RasterizationStateCont _RasterizationState = 
                    let _SampleCount = int (x.SampleCount)
                    let inline _DepthStencilStateCont _DepthStencilState = 
                        let _ColorStatesCount = if isNull x.ColorStates then 0 else x.ColorStates.Length
                        let rec _ColorStatesCont (_ColorStatesinputs : array<ColorStateDescriptor>) (_ColorStatesoutputs : JsArray) (_ColorStatesi : int) =
                            if _ColorStatesi >= _ColorStatesCount then
                                let _ColorStates = _ColorStatesoutputs.Reference
                                let _SampleMask = int (x.SampleMask)
                                let _AlphaToCoverageEnabled = x.AlphaToCoverageEnabled
                                let native = DawnRaw.WGPURenderPipelineDescriptor()
                                native.Label <- _Label
                                native.Layout <- _Layout
                                native.VertexStage <- _VertexStage
                                native.FragmentStage <- _FragmentStage
                                native.VertexState <- _VertexState
                                native.PrimitiveTopology <- _PrimitiveTopology
                                native.RasterizationState <- _RasterizationState
                                native.SampleCount <- _SampleCount
                                native.DepthStencilState <- _DepthStencilState
                                native.ColorStates <- _ColorStates
                                native.SampleMask <- _SampleMask
                                native.AlphaToCoverageEnabled <- _AlphaToCoverageEnabled
                                callback native
                            else
                                let _Format = _ColorStatesinputs.[_ColorStatesi].Format.GetValue()
                                let _Operation = _ColorStatesinputs.[_ColorStatesi].AlphaBlend.Operation.GetValue()
                                let _SrcFactor = _ColorStatesinputs.[_ColorStatesi].AlphaBlend.SrcFactor.GetValue()
                                let _DstFactor = _ColorStatesinputs.[_ColorStatesi].AlphaBlend.DstFactor.GetValue()
                                let _AlphaBlend = new DawnRaw.WGPUBlendDescriptor()
                                _AlphaBlend.Operation <- _Operation
                                _AlphaBlend.SrcFactor <- _SrcFactor
                                _AlphaBlend.DstFactor <- _DstFactor
                                let _AlphaBlend = _AlphaBlend
                                let _Operation = _ColorStatesinputs.[_ColorStatesi].ColorBlend.Operation.GetValue()
                                let _SrcFactor = _ColorStatesinputs.[_ColorStatesi].ColorBlend.SrcFactor.GetValue()
                                let _DstFactor = _ColorStatesinputs.[_ColorStatesi].ColorBlend.DstFactor.GetValue()
                                let _ColorBlend = new DawnRaw.WGPUBlendDescriptor()
                                _ColorBlend.Operation <- _Operation
                                _ColorBlend.SrcFactor <- _SrcFactor
                                _ColorBlend.DstFactor <- _DstFactor
                                let _ColorBlend = _ColorBlend
                                let _WriteMask = int (_ColorStatesinputs.[_ColorStatesi].WriteMask)
                                let _n = new DawnRaw.WGPUColorStateDescriptor()
                                _n.Format <- _Format
                                _n.AlphaBlend <- _AlphaBlend
                                _n.ColorBlend <- _ColorBlend
                                _n.WriteMask <- _WriteMask
                                let _n = _n
                                _ColorStatesoutputs.[_ColorStatesi] <- _n
                                _ColorStatesCont _ColorStatesinputs _ColorStatesoutputs (_ColorStatesi + 1)
                        _ColorStatesCont x.ColorStates (if _ColorStatesCount > 0 then newArray _ColorStatesCount else null) 0
                    match x.DepthStencilState with
                    | Some v ->
                        let _Format = v.Format.GetValue()
                        let _DepthWriteEnabled = v.DepthWriteEnabled
                        let _DepthCompare = v.DepthCompare.GetValue()
                        let _Compare = v.StencilFront.Compare.GetValue()
                        let _FailOp = v.StencilFront.FailOp.GetValue()
                        let _DepthFailOp = v.StencilFront.DepthFailOp.GetValue()
                        let _PassOp = v.StencilFront.PassOp.GetValue()
                        let _StencilFront = new DawnRaw.WGPUStencilStateFaceDescriptor()
                        _StencilFront.Compare <- _Compare
                        _StencilFront.FailOp <- _FailOp
                        _StencilFront.DepthFailOp <- _DepthFailOp
                        _StencilFront.PassOp <- _PassOp
                        let _StencilFront = _StencilFront
                        let _Compare = v.StencilBack.Compare.GetValue()
                        let _FailOp = v.StencilBack.FailOp.GetValue()
                        let _DepthFailOp = v.StencilBack.DepthFailOp.GetValue()
                        let _PassOp = v.StencilBack.PassOp.GetValue()
                        let _StencilBack = new DawnRaw.WGPUStencilStateFaceDescriptor()
                        _StencilBack.Compare <- _Compare
                        _StencilBack.FailOp <- _FailOp
                        _StencilBack.DepthFailOp <- _DepthFailOp
                        _StencilBack.PassOp <- _PassOp
                        let _StencilBack = _StencilBack
                        let _StencilReadMask = int (v.StencilReadMask)
                        let _StencilWriteMask = int (v.StencilWriteMask)
                        let _n = new DawnRaw.WGPUDepthStencilStateDescriptor()
                        _n.Format <- _Format
                        _n.DepthWriteEnabled <- _DepthWriteEnabled
                        _n.DepthCompare <- _DepthCompare
                        _n.StencilFront <- _StencilFront
                        _n.StencilBack <- _StencilBack
                        _n.StencilReadMask <- _StencilReadMask
                        _n.StencilWriteMask <- _StencilWriteMask
                        let _n = _n
                        _DepthStencilStateCont _n
                    | None -> _DepthStencilStateCont null
                match x.RasterizationState with
                | Some v ->
                    let _FrontFace = v.FrontFace.GetValue()
                    let _CullMode = v.CullMode.GetValue()
                    let _DepthBias = int (v.DepthBias)
                    let _DepthBiasSlopeScale = (v.DepthBiasSlopeScale)
                    let _DepthBiasClamp = (v.DepthBiasClamp)
                    let _n = new DawnRaw.WGPURasterizationStateDescriptor()
                    _n.FrontFace <- _FrontFace
                    _n.CullMode <- _CullMode
                    _n.DepthBias <- _DepthBias
                    _n.DepthBiasSlopeScale <- _DepthBiasSlopeScale
                    _n.DepthBiasClamp <- _DepthBiasClamp
                    let _n = _n
                    _RasterizationStateCont _n
                | None -> _RasterizationStateCont null
            match x.VertexState with
            | Some v ->
                let _IndexFormat = v.IndexFormat.GetValue()
                let _VertexBuffersCount = if isNull v.VertexBuffers then 0 else v.VertexBuffers.Length
                let rec _VertexBuffersCont (_VertexBuffersinputs : array<VertexBufferLayoutDescriptor>) (_VertexBuffersoutputs : JsArray) (_VertexBuffersi : int) =
                    if _VertexBuffersi >= _VertexBuffersCount then
                        let _VertexBuffers = _VertexBuffersoutputs.Reference
                        let _n = new DawnRaw.WGPUVertexStateDescriptor()
                        _n.IndexFormat <- _IndexFormat
                        _n.VertexBuffers <- _VertexBuffers
                        let _n = _n
                        _VertexStateCont _n
                    else
                        let _ArrayStride = int (_VertexBuffersinputs.[_VertexBuffersi].ArrayStride)
                        let _StepMode = _VertexBuffersinputs.[_VertexBuffersi].StepMode.GetValue()
                        let _AttributesCount = if isNull _VertexBuffersinputs.[_VertexBuffersi].Attributes then 0 else _VertexBuffersinputs.[_VertexBuffersi].Attributes.Length
                        let rec _AttributesCont (_Attributesinputs : array<VertexAttributeDescriptor>) (_Attributesoutputs : JsArray) (_Attributesi : int) =
                            if _Attributesi >= _AttributesCount then
                                let _Attributes = _Attributesoutputs.Reference
                                let _n = new DawnRaw.WGPUVertexBufferLayoutDescriptor()
                                _n.ArrayStride <- _ArrayStride
                                _n.StepMode <- _StepMode
                                _n.Attributes <- _Attributes
                                let _n = _n
                                _VertexBuffersoutputs.[_VertexBuffersi] <- _n
                                _VertexBuffersCont _VertexBuffersinputs _VertexBuffersoutputs (_VertexBuffersi + 1)
                            else
                                let _Format = _Attributesinputs.[_Attributesi].Format.GetValue()
                                let _Offset = int (_Attributesinputs.[_Attributesi].Offset)
                                let _ShaderLocation = int (_Attributesinputs.[_Attributesi].ShaderLocation)
                                let _n = new DawnRaw.WGPUVertexAttributeDescriptor()
                                _n.Format <- _Format
                                _n.Offset <- _Offset
                                _n.ShaderLocation <- _ShaderLocation
                                let _n = _n
                                _Attributesoutputs.[_Attributesi] <- _n
                                _AttributesCont _Attributesinputs _Attributesoutputs (_Attributesi + 1)
                        _AttributesCont _VertexBuffersinputs.[_VertexBuffersi].Attributes (if _AttributesCount > 0 then newArray _AttributesCount else null) 0
                _VertexBuffersCont v.VertexBuffers (if _VertexBuffersCount > 0 then newArray _VertexBuffersCount else null) 0
            | None -> _VertexStateCont null
        match x.FragmentStage with
        | Some v ->
            let _Module = (if isNull v.Module then null else v.Module.Handle)
            let _EntryPoint = v.EntryPoint
            let _n = new DawnRaw.WGPUProgrammableStageDescriptor()
            _n.Module <- _Module
            _n.EntryPoint <- _EntryPoint
            let _n = _n
            _FragmentStageCont _n
        | None -> _FragmentStageCont null


[<AllowNullLiteral>]
type BindGroup(device : Device, handle : BindGroupHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle = handle
    member x.IsDisposed = isDisposed
    member private x.Dispose(disposing : bool) =
        if not isDisposed then 
            let r = Interlocked.Decrement(&refCount.contents)
            isDisposed <- true
    member x.Dispose() = x.Dispose(true)
    member x.Clone() = 
        let mutable o = refCount.contents
        if o = 0 then raise <| System.ObjectDisposedException("BindGroup")
        let mutable n = Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        while o <> n do
            o <- n
            if o = 0 then raise <| System.ObjectDisposedException("BindGroup")
            n <- Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        new BindGroup(device, handle, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : BindGroupHandle) = new BindGroup(device, handle, ref 1)
[<AllowNullLiteral>]
type BindGroupLayout(device : Device, handle : BindGroupLayoutHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle = handle
    member x.IsDisposed = isDisposed
    member private x.Dispose(disposing : bool) =
        if not isDisposed then 
            let r = Interlocked.Decrement(&refCount.contents)
            isDisposed <- true
    member x.Dispose() = x.Dispose(true)
    member x.Clone() = 
        let mutable o = refCount.contents
        if o = 0 then raise <| System.ObjectDisposedException("BindGroupLayout")
        let mutable n = Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        while o <> n do
            o <- n
            if o = 0 then raise <| System.ObjectDisposedException("BindGroupLayout")
            n <- Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        new BindGroupLayout(device, handle, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : BindGroupLayoutHandle) = new BindGroupLayout(device, handle, ref 1)
[<AllowNullLiteral>]
type CommandBuffer(device : Device, handle : CommandBufferHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle = handle
    member x.IsDisposed = isDisposed
    member private x.Dispose(disposing : bool) =
        if not isDisposed then 
            let r = Interlocked.Decrement(&refCount.contents)
            isDisposed <- true
    member x.Dispose() = x.Dispose(true)
    member x.Clone() = 
        let mutable o = refCount.contents
        if o = 0 then raise <| System.ObjectDisposedException("CommandBuffer")
        let mutable n = Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        while o <> n do
            o <- n
            if o = 0 then raise <| System.ObjectDisposedException("CommandBuffer")
            n <- Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        new CommandBuffer(device, handle, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : CommandBufferHandle) = new CommandBuffer(device, handle, ref 1)
[<AllowNullLiteral>]
type PipelineLayout(device : Device, handle : PipelineLayoutHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle = handle
    member x.IsDisposed = isDisposed
    member private x.Dispose(disposing : bool) =
        if not isDisposed then 
            let r = Interlocked.Decrement(&refCount.contents)
            isDisposed <- true
    member x.Dispose() = x.Dispose(true)
    member x.Clone() = 
        let mutable o = refCount.contents
        if o = 0 then raise <| System.ObjectDisposedException("PipelineLayout")
        let mutable n = Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        while o <> n do
            o <- n
            if o = 0 then raise <| System.ObjectDisposedException("PipelineLayout")
            n <- Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        new PipelineLayout(device, handle, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : PipelineLayoutHandle) = new PipelineLayout(device, handle, ref 1)
[<AllowNullLiteral>]
type QuerySet(device : Device, handle : QuerySetHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle = handle
    member x.IsDisposed = isDisposed
    member private x.Dispose(disposing : bool) =
        if not isDisposed then 
            let r = Interlocked.Decrement(&refCount.contents)
            isDisposed <- true
    member x.Dispose() = x.Dispose(true)
    member x.Clone() = 
        let mutable o = refCount.contents
        if o = 0 then raise <| System.ObjectDisposedException("QuerySet")
        let mutable n = Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        while o <> n do
            o <- n
            if o = 0 then raise <| System.ObjectDisposedException("QuerySet")
            n <- Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        new QuerySet(device, handle, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : QuerySetHandle) = new QuerySet(device, handle, ref 1)
    member x.Destroy() : unit = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "destroy") |> ignore
        try
            x.Handle.Reference.Invoke("destroy") |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
[<AllowNullLiteral>]
type RenderBundle(device : Device, handle : RenderBundleHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle = handle
    member x.IsDisposed = isDisposed
    member private x.Dispose(disposing : bool) =
        if not isDisposed then 
            let r = Interlocked.Decrement(&refCount.contents)
            isDisposed <- true
    member x.Dispose() = x.Dispose(true)
    member x.Clone() = 
        let mutable o = refCount.contents
        if o = 0 then raise <| System.ObjectDisposedException("RenderBundle")
        let mutable n = Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        while o <> n do
            o <- n
            if o = 0 then raise <| System.ObjectDisposedException("RenderBundle")
            n <- Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        new RenderBundle(device, handle, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : RenderBundleHandle) = new RenderBundle(device, handle, ref 1)
[<AllowNullLiteral>]
type Sampler(device : Device, handle : SamplerHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle = handle
    member x.IsDisposed = isDisposed
    member private x.Dispose(disposing : bool) =
        if not isDisposed then 
            let r = Interlocked.Decrement(&refCount.contents)
            isDisposed <- true
    member x.Dispose() = x.Dispose(true)
    member x.Clone() = 
        let mutable o = refCount.contents
        if o = 0 then raise <| System.ObjectDisposedException("Sampler")
        let mutable n = Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        while o <> n do
            o <- n
            if o = 0 then raise <| System.ObjectDisposedException("Sampler")
            n <- Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        new Sampler(device, handle, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : SamplerHandle) = new Sampler(device, handle, ref 1)
[<AllowNullLiteral>]
type ShaderModule(device : Device, handle : ShaderModuleHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle = handle
    member x.IsDisposed = isDisposed
    member private x.Dispose(disposing : bool) =
        if not isDisposed then 
            let r = Interlocked.Decrement(&refCount.contents)
            isDisposed <- true
    member x.Dispose() = x.Dispose(true)
    member x.Clone() = 
        let mutable o = refCount.contents
        if o = 0 then raise <| System.ObjectDisposedException("ShaderModule")
        let mutable n = Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        while o <> n do
            o <- n
            if o = 0 then raise <| System.ObjectDisposedException("ShaderModule")
            n <- Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        new ShaderModule(device, handle, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : ShaderModuleHandle) = new ShaderModule(device, handle, ref 1)
[<AllowNullLiteral>]
type Surface(device : Device, handle : SurfaceHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle = handle
    member x.IsDisposed = isDisposed
    member private x.Dispose(disposing : bool) =
        if not isDisposed then 
            let r = Interlocked.Decrement(&refCount.contents)
            isDisposed <- true
    member x.Dispose() = x.Dispose(true)
    member x.Clone() = 
        let mutable o = refCount.contents
        if o = 0 then raise <| System.ObjectDisposedException("Surface")
        let mutable n = Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        while o <> n do
            o <- n
            if o = 0 then raise <| System.ObjectDisposedException("Surface")
            n <- Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        new Surface(device, handle, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : SurfaceHandle) = new Surface(device, handle, ref 1)
[<AllowNullLiteral>]
type TextureView(device : Device, handle : TextureViewHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle = handle
    member x.IsDisposed = isDisposed
    member private x.Dispose(disposing : bool) =
        if not isDisposed then 
            let r = Interlocked.Decrement(&refCount.contents)
            isDisposed <- true
    member x.Dispose() = x.Dispose(true)
    member x.Clone() = 
        let mutable o = refCount.contents
        if o = 0 then raise <| System.ObjectDisposedException("TextureView")
        let mutable n = Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        while o <> n do
            o <- n
            if o = 0 then raise <| System.ObjectDisposedException("TextureView")
            n <- Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        new TextureView(device, handle, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : TextureViewHandle) = new TextureView(device, handle, ref 1)
[<AllowNullLiteral>]
type Buffer(device : Device, handle : BufferHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle = handle
    member x.IsDisposed = isDisposed
    member private x.Dispose(disposing : bool) =
        if not isDisposed then 
            let r = Interlocked.Decrement(&refCount.contents)
            isDisposed <- true
    member x.Dispose() = x.Dispose(true)
    member x.Clone() = 
        let mutable o = refCount.contents
        if o = 0 then raise <| System.ObjectDisposedException("Buffer")
        let mutable n = Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        while o <> n do
            o <- n
            if o = 0 then raise <| System.ObjectDisposedException("Buffer")
            n <- Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        new Buffer(device, handle, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : BufferHandle) = new Buffer(device, handle, ref 1)
    member x.MapAsync(Mode : MapMode, Offset : unativeint, Size : unativeint) : System.Threading.Tasks.Task = 
        let _Mode = int (Mode)
        let _Offset = int (Offset)
        let _Size = int (Size)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "mapAsync", js _Mode, js _Offset, js _Size) |> ignore
        try
            x.Handle.Reference.Invoke("mapAsync", js _Mode, js _Offset, js _Size) |> convert<System.Threading.Tasks.Task>
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.GetMappedRange() : ArrayBuffer = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "getMappedRange") |> ignore
        try
            x.Handle.Reference.Invoke("getMappedRange") |> unbox<ArrayBuffer>
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.GetMappedRange(Offset : unativeint) : ArrayBuffer = 
        let _Offset = int (Offset)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "getMappedRange", js _Offset) |> ignore
        try
            x.Handle.Reference.Invoke("getMappedRange", js _Offset) |> unbox<ArrayBuffer>
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.GetMappedRange(Offset : unativeint, Size : unativeint) : ArrayBuffer = 
        let _Offset = int (Offset)
        let _Size = int (Size)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "getMappedRange", js _Offset, js _Size) |> ignore
        try
            x.Handle.Reference.Invoke("getMappedRange", js _Offset, js _Size) |> unbox<ArrayBuffer>
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.GetConstMappedRange() : ArrayBuffer = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "getConstMappedRange") |> ignore
        try
            x.Handle.Reference.Invoke("getConstMappedRange") |> unbox<ArrayBuffer>
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.GetConstMappedRange(Offset : unativeint) : ArrayBuffer = 
        let _Offset = int (Offset)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "getConstMappedRange", js _Offset) |> ignore
        try
            x.Handle.Reference.Invoke("getConstMappedRange", js _Offset) |> unbox<ArrayBuffer>
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.GetConstMappedRange(Offset : unativeint, Size : unativeint) : ArrayBuffer = 
        let _Offset = int (Offset)
        let _Size = int (Size)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "getConstMappedRange", js _Offset, js _Size) |> ignore
        try
            x.Handle.Reference.Invoke("getConstMappedRange", js _Offset, js _Size) |> unbox<ArrayBuffer>
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.Unmap() : unit = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "unmap") |> ignore
        try
            x.Handle.Reference.Invoke("unmap") |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.Destroy() : unit = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "destroy") |> ignore
        try
            x.Handle.Reference.Invoke("destroy") |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
[<AllowNullLiteral>]
type ComputePipeline(device : Device, handle : ComputePipelineHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle = handle
    member x.IsDisposed = isDisposed
    member private x.Dispose(disposing : bool) =
        if not isDisposed then 
            let r = Interlocked.Decrement(&refCount.contents)
            isDisposed <- true
    member x.Dispose() = x.Dispose(true)
    member x.Clone() = 
        let mutable o = refCount.contents
        if o = 0 then raise <| System.ObjectDisposedException("ComputePipeline")
        let mutable n = Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        while o <> n do
            o <- n
            if o = 0 then raise <| System.ObjectDisposedException("ComputePipeline")
            n <- Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        new ComputePipeline(device, handle, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : ComputePipelineHandle) = new ComputePipeline(device, handle, ref 1)
    member x.GetBindGroupLayout(GroupIndex : int) : BindGroupLayout = 
        let _GroupIndex = int (GroupIndex)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "getBindGroupLayout", js _GroupIndex) |> ignore
        try
            new BindGroupLayout(x.Device, convert(x.Handle.Reference.Invoke("getBindGroupLayout", js _GroupIndex)))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
[<AllowNullLiteral>]
type Instance(device : Device, handle : InstanceHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle = handle
    member x.IsDisposed = isDisposed
    member private x.Dispose(disposing : bool) =
        if not isDisposed then 
            let r = Interlocked.Decrement(&refCount.contents)
            isDisposed <- true
    member x.Dispose() = x.Dispose(true)
    member x.Clone() = 
        let mutable o = refCount.contents
        if o = 0 then raise <| System.ObjectDisposedException("Instance")
        let mutable n = Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        while o <> n do
            o <- n
            if o = 0 then raise <| System.ObjectDisposedException("Instance")
            n <- Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        new Instance(device, handle, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : InstanceHandle) = new Instance(device, handle, ref 1)
    member x.CreateSurface() : Surface = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "createSurface") |> ignore
        try
            new Surface(x.Device, convert(x.Handle.Reference.Invoke("createSurface")))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.CreateSurface(Descriptor : SurfaceDescriptor) : Surface = 
        let _Label = Descriptor.Label
        let _Descriptor = new DawnRaw.WGPUSurfaceDescriptor()
        _Descriptor.Label <- _Label
        let _Descriptor = _Descriptor
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "createSurface", js _Descriptor) |> ignore
        try
            new Surface(x.Device, convert(x.Handle.Reference.Invoke("createSurface", js _Descriptor)))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
[<AllowNullLiteral>]
type RenderPipeline(device : Device, handle : RenderPipelineHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle = handle
    member x.IsDisposed = isDisposed
    member private x.Dispose(disposing : bool) =
        if not isDisposed then 
            let r = Interlocked.Decrement(&refCount.contents)
            isDisposed <- true
    member x.Dispose() = x.Dispose(true)
    member x.Clone() = 
        let mutable o = refCount.contents
        if o = 0 then raise <| System.ObjectDisposedException("RenderPipeline")
        let mutable n = Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        while o <> n do
            o <- n
            if o = 0 then raise <| System.ObjectDisposedException("RenderPipeline")
            n <- Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        new RenderPipeline(device, handle, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : RenderPipelineHandle) = new RenderPipeline(device, handle, ref 1)
    member x.GetBindGroupLayout(GroupIndex : int) : BindGroupLayout = 
        let _GroupIndex = int (GroupIndex)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "getBindGroupLayout", js _GroupIndex) |> ignore
        try
            new BindGroupLayout(x.Device, convert(x.Handle.Reference.Invoke("getBindGroupLayout", js _GroupIndex)))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
[<AllowNullLiteral>]
type SwapChain(device : Device, handle : SwapChainHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle = handle
    member x.IsDisposed = isDisposed
    member private x.Dispose(disposing : bool) =
        if not isDisposed then 
            let r = Interlocked.Decrement(&refCount.contents)
            isDisposed <- true
    member x.Dispose() = x.Dispose(true)
    member x.Clone() = 
        let mutable o = refCount.contents
        if o = 0 then raise <| System.ObjectDisposedException("SwapChain")
        let mutable n = Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        while o <> n do
            o <- n
            if o = 0 then raise <| System.ObjectDisposedException("SwapChain")
            n <- Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        new SwapChain(device, handle, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : SwapChainHandle) = new SwapChain(device, handle, ref 1)
    member x.Configure(Format : TextureFormat, AllowedUsage : TextureUsage, Width : int, Height : int) : unit = 
        let _Format = Format.GetValue()
        let _AllowedUsage = int (AllowedUsage)
        let _Width = int (Width)
        let _Height = int (Height)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "configure", js _Format, js _AllowedUsage, js _Width, js _Height) |> ignore
        try
            x.Handle.Reference.Invoke("configure", js _Format, js _AllowedUsage, js _Width, js _Height) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.GetCurrentTextureView() : TextureView = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "getCurrentTextureView") |> ignore
        try
            new TextureView(x.Device, convert(x.Handle.Reference.Invoke("getCurrentTextureView")))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.Present() : unit = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "present") |> ignore
        try
            x.Handle.Reference.Invoke("present") |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
[<AllowNullLiteral>]
type ComputePassEncoder(device : Device, handle : ComputePassEncoderHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle = handle
    member x.IsDisposed = isDisposed
    member private x.Dispose(disposing : bool) =
        if not isDisposed then 
            let r = Interlocked.Decrement(&refCount.contents)
            isDisposed <- true
    member x.Dispose() = x.Dispose(true)
    member x.Clone() = 
        let mutable o = refCount.contents
        if o = 0 then raise <| System.ObjectDisposedException("ComputePassEncoder")
        let mutable n = Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        while o <> n do
            o <- n
            if o = 0 then raise <| System.ObjectDisposedException("ComputePassEncoder")
            n <- Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        new ComputePassEncoder(device, handle, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : ComputePassEncoderHandle) = new ComputePassEncoder(device, handle, ref 1)
    member x.InsertDebugMarker(MarkerLabel : string) : unit = 
        let _MarkerLabel = MarkerLabel
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "insertDebugMarker", js _MarkerLabel) |> ignore
        try
            x.Handle.Reference.Invoke("insertDebugMarker", js _MarkerLabel) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.PopDebugGroup() : unit = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "popDebugGroup") |> ignore
        try
            x.Handle.Reference.Invoke("popDebugGroup") |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.PushDebugGroup(GroupLabel : string) : unit = 
        let _GroupLabel = GroupLabel
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "pushDebugGroup", js _GroupLabel) |> ignore
        try
            x.Handle.Reference.Invoke("pushDebugGroup", js _GroupLabel) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.SetPipeline(Pipeline : ComputePipeline) : unit = 
        let _Pipeline = (if isNull Pipeline then null else Pipeline.Handle)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "setPipeline", js _Pipeline) |> ignore
        try
            x.Handle.Reference.Invoke("setPipeline", js _Pipeline) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.SetBindGroup(GroupIndex : int, Group : BindGroup, DynamicOffsets : uint32[]) : unit = 
        let _GroupIndex = int (GroupIndex)
        let _Group = (if isNull Group then null else Group.Handle)
        use _DynamicOffsets = Uint32Array.op_Implicit(Span(DynamicOffsets))
        let _DynamicOffsetsCount = DynamicOffsets.Length
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "setBindGroup", js _GroupIndex, js _Group, js _DynamicOffsets) |> ignore
        try
            x.Handle.Reference.Invoke("setBindGroup", js _GroupIndex, js _Group, js _DynamicOffsets) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.WriteTimestamp(QuerySet : QuerySet, QueryIndex : int) : unit = 
        let _QuerySet = (if isNull QuerySet then null else QuerySet.Handle)
        let _QueryIndex = int (QueryIndex)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "writeTimestamp", js _QuerySet, js _QueryIndex) |> ignore
        try
            x.Handle.Reference.Invoke("writeTimestamp", js _QuerySet, js _QueryIndex) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.Dispatch(X : int) : unit = 
        let _X = int (X)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "dispatch", js _X) |> ignore
        try
            x.Handle.Reference.Invoke("dispatch", js _X) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.Dispatch(X : int, Y : int) : unit = 
        let _X = int (X)
        let _Y = int (Y)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "dispatch", js _X, js _Y) |> ignore
        try
            x.Handle.Reference.Invoke("dispatch", js _X, js _Y) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.Dispatch(X : int, Y : int, Z : int) : unit = 
        let _X = int (X)
        let _Y = int (Y)
        let _Z = int (Z)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "dispatch", js _X, js _Y, js _Z) |> ignore
        try
            x.Handle.Reference.Invoke("dispatch", js _X, js _Y, js _Z) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.DispatchIndirect(IndirectBuffer : Buffer, IndirectOffset : uint64) : unit = 
        let _IndirectBuffer = (if isNull IndirectBuffer then null else IndirectBuffer.Handle)
        let _IndirectOffset = int (IndirectOffset)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "dispatchIndirect", js _IndirectBuffer, js _IndirectOffset) |> ignore
        try
            x.Handle.Reference.Invoke("dispatchIndirect", js _IndirectBuffer, js _IndirectOffset) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.EndPass() : unit = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "endPass") |> ignore
        try
            x.Handle.Reference.Invoke("endPass") |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
[<AllowNullLiteral>]
type Fence(device : Device, handle : FenceHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle = handle
    member x.IsDisposed = isDisposed
    member private x.Dispose(disposing : bool) =
        if not isDisposed then 
            let r = Interlocked.Decrement(&refCount.contents)
            isDisposed <- true
    member x.Dispose() = x.Dispose(true)
    member x.Clone() = 
        let mutable o = refCount.contents
        if o = 0 then raise <| System.ObjectDisposedException("Fence")
        let mutable n = Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        while o <> n do
            o <- n
            if o = 0 then raise <| System.ObjectDisposedException("Fence")
            n <- Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        new Fence(device, handle, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : FenceHandle) = new Fence(device, handle, ref 1)
    member x.GetCompletedValue() : uint64 = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "getCompletedValue") |> ignore
        try
            x.Handle.Reference.Invoke("getCompletedValue") |> convert
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.OnCompletion(Value : uint64, Callback : FenceOnCompletionCallback) : unit = 
        let _Value = int (Value)
        let mutable _CallbackGC = Unchecked.defaultof<System.Runtime.InteropServices.GCHandle>
        let _CallbackFunction (Status : obj) (Userdata : int) = 
            let _Status = Status
            let _Userdata = Userdata
            if _CallbackGC.IsAllocated then _CallbackGC.Free()
            Callback.Invoke(FenceCompletionStatus.Parse(_Status), nativeint _Userdata)
        let _CallbackDel = WGPUFenceOnCompletionCallback(_CallbackFunction)
        _CallbackGC <- System.Runtime.InteropServices.GCHandle.Alloc(_CallbackDel)
        let _Callback = _CallbackDel
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "onCompletion", js _Value, js _Callback) |> ignore
        try
            x.Handle.Reference.Invoke("onCompletion", js _Value, js _Callback) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.OnCompletion(Value : uint64, Callback : FenceOnCompletionCallback, Userdata : nativeint) : unit = 
        let _Value = int (Value)
        let mutable _CallbackGC = Unchecked.defaultof<System.Runtime.InteropServices.GCHandle>
        let _CallbackFunction (Status : obj) (Userdata : int) = 
            let _Status = Status
            let _Userdata = Userdata
            if _CallbackGC.IsAllocated then _CallbackGC.Free()
            Callback.Invoke(FenceCompletionStatus.Parse(_Status), nativeint _Userdata)
        let _CallbackDel = WGPUFenceOnCompletionCallback(_CallbackFunction)
        _CallbackGC <- System.Runtime.InteropServices.GCHandle.Alloc(_CallbackDel)
        let _Callback = _CallbackDel
        let _Userdata = int (Userdata)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "onCompletion", js _Value, js _Callback, js _Userdata) |> ignore
        try
            x.Handle.Reference.Invoke("onCompletion", js _Value, js _Callback, js _Userdata) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
[<AllowNullLiteral>]
type RenderBundleEncoder(device : Device, handle : RenderBundleEncoderHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle = handle
    member x.IsDisposed = isDisposed
    member private x.Dispose(disposing : bool) =
        if not isDisposed then 
            let r = Interlocked.Decrement(&refCount.contents)
            isDisposed <- true
    member x.Dispose() = x.Dispose(true)
    member x.Clone() = 
        let mutable o = refCount.contents
        if o = 0 then raise <| System.ObjectDisposedException("RenderBundleEncoder")
        let mutable n = Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        while o <> n do
            o <- n
            if o = 0 then raise <| System.ObjectDisposedException("RenderBundleEncoder")
            n <- Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        new RenderBundleEncoder(device, handle, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : RenderBundleEncoderHandle) = new RenderBundleEncoder(device, handle, ref 1)
    member x.SetPipeline(Pipeline : RenderPipeline) : unit = 
        let _Pipeline = (if isNull Pipeline then null else Pipeline.Handle)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "setPipeline", js _Pipeline) |> ignore
        try
            x.Handle.Reference.Invoke("setPipeline", js _Pipeline) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.SetBindGroup(GroupIndex : int, Group : BindGroup, DynamicOffsets : uint32[]) : unit = 
        let _GroupIndex = int (GroupIndex)
        let _Group = (if isNull Group then null else Group.Handle)
        use _DynamicOffsets = Uint32Array.op_Implicit(Span(DynamicOffsets))
        let _DynamicOffsetsCount = DynamicOffsets.Length
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "setBindGroup", js _GroupIndex, js _Group, js _DynamicOffsets) |> ignore
        try
            x.Handle.Reference.Invoke("setBindGroup", js _GroupIndex, js _Group, js _DynamicOffsets) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.Draw(VertexCount : int) : unit = 
        let _VertexCount = int (VertexCount)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "draw", js _VertexCount) |> ignore
        try
            x.Handle.Reference.Invoke("draw", js _VertexCount) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.Draw(VertexCount : int, InstanceCount : int) : unit = 
        let _VertexCount = int (VertexCount)
        let _InstanceCount = int (InstanceCount)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "draw", js _VertexCount, js _InstanceCount) |> ignore
        try
            x.Handle.Reference.Invoke("draw", js _VertexCount, js _InstanceCount) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.Draw(VertexCount : int, InstanceCount : int, FirstVertex : int) : unit = 
        let _VertexCount = int (VertexCount)
        let _InstanceCount = int (InstanceCount)
        let _FirstVertex = int (FirstVertex)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "draw", js _VertexCount, js _InstanceCount, js _FirstVertex) |> ignore
        try
            x.Handle.Reference.Invoke("draw", js _VertexCount, js _InstanceCount, js _FirstVertex) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.Draw(VertexCount : int, InstanceCount : int, FirstVertex : int, FirstInstance : int) : unit = 
        let _VertexCount = int (VertexCount)
        let _InstanceCount = int (InstanceCount)
        let _FirstVertex = int (FirstVertex)
        let _FirstInstance = int (FirstInstance)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "draw", js _VertexCount, js _InstanceCount, js _FirstVertex, js _FirstInstance) |> ignore
        try
            x.Handle.Reference.Invoke("draw", js _VertexCount, js _InstanceCount, js _FirstVertex, js _FirstInstance) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.DrawIndexed(IndexCount : int) : unit = 
        let _IndexCount = int (IndexCount)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "drawIndexed", js _IndexCount) |> ignore
        try
            x.Handle.Reference.Invoke("drawIndexed", js _IndexCount) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.DrawIndexed(IndexCount : int, InstanceCount : int) : unit = 
        let _IndexCount = int (IndexCount)
        let _InstanceCount = int (InstanceCount)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "drawIndexed", js _IndexCount, js _InstanceCount) |> ignore
        try
            x.Handle.Reference.Invoke("drawIndexed", js _IndexCount, js _InstanceCount) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.DrawIndexed(IndexCount : int, InstanceCount : int, FirstIndex : int) : unit = 
        let _IndexCount = int (IndexCount)
        let _InstanceCount = int (InstanceCount)
        let _FirstIndex = int (FirstIndex)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "drawIndexed", js _IndexCount, js _InstanceCount, js _FirstIndex) |> ignore
        try
            x.Handle.Reference.Invoke("drawIndexed", js _IndexCount, js _InstanceCount, js _FirstIndex) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.DrawIndexed(IndexCount : int, InstanceCount : int, FirstIndex : int, BaseVertex : int32) : unit = 
        let _IndexCount = int (IndexCount)
        let _InstanceCount = int (InstanceCount)
        let _FirstIndex = int (FirstIndex)
        let _BaseVertex = int (BaseVertex)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "drawIndexed", js _IndexCount, js _InstanceCount, js _FirstIndex, js _BaseVertex) |> ignore
        try
            x.Handle.Reference.Invoke("drawIndexed", js _IndexCount, js _InstanceCount, js _FirstIndex, js _BaseVertex) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.DrawIndexed(IndexCount : int, InstanceCount : int, FirstIndex : int, BaseVertex : int32, FirstInstance : int) : unit = 
        let _IndexCount = int (IndexCount)
        let _InstanceCount = int (InstanceCount)
        let _FirstIndex = int (FirstIndex)
        let _BaseVertex = int (BaseVertex)
        let _FirstInstance = int (FirstInstance)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "drawIndexed", js _IndexCount, js _InstanceCount, js _FirstIndex, js _BaseVertex, js _FirstInstance) |> ignore
        try
            x.Handle.Reference.Invoke("drawIndexed", js _IndexCount, js _InstanceCount, js _FirstIndex, js _BaseVertex, js _FirstInstance) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.DrawIndirect(IndirectBuffer : Buffer, IndirectOffset : uint64) : unit = 
        let _IndirectBuffer = (if isNull IndirectBuffer then null else IndirectBuffer.Handle)
        let _IndirectOffset = int (IndirectOffset)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "drawIndirect", js _IndirectBuffer, js _IndirectOffset) |> ignore
        try
            x.Handle.Reference.Invoke("drawIndirect", js _IndirectBuffer, js _IndirectOffset) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.DrawIndexedIndirect(IndirectBuffer : Buffer, IndirectOffset : uint64) : unit = 
        let _IndirectBuffer = (if isNull IndirectBuffer then null else IndirectBuffer.Handle)
        let _IndirectOffset = int (IndirectOffset)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "drawIndexedIndirect", js _IndirectBuffer, js _IndirectOffset) |> ignore
        try
            x.Handle.Reference.Invoke("drawIndexedIndirect", js _IndirectBuffer, js _IndirectOffset) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.InsertDebugMarker(MarkerLabel : string) : unit = 
        let _MarkerLabel = MarkerLabel
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "insertDebugMarker", js _MarkerLabel) |> ignore
        try
            x.Handle.Reference.Invoke("insertDebugMarker", js _MarkerLabel) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.PopDebugGroup() : unit = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "popDebugGroup") |> ignore
        try
            x.Handle.Reference.Invoke("popDebugGroup") |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.PushDebugGroup(GroupLabel : string) : unit = 
        let _GroupLabel = GroupLabel
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "pushDebugGroup", js _GroupLabel) |> ignore
        try
            x.Handle.Reference.Invoke("pushDebugGroup", js _GroupLabel) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.SetVertexBuffer(Slot : int, Buffer : Buffer) : unit = 
        let _Slot = int (Slot)
        let _Buffer = (if isNull Buffer then null else Buffer.Handle)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "setVertexBuffer", js _Slot, js _Buffer) |> ignore
        try
            x.Handle.Reference.Invoke("setVertexBuffer", js _Slot, js _Buffer) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.SetVertexBuffer(Slot : int, Buffer : Buffer, Offset : uint64) : unit = 
        let _Slot = int (Slot)
        let _Buffer = (if isNull Buffer then null else Buffer.Handle)
        let _Offset = int (Offset)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "setVertexBuffer", js _Slot, js _Buffer, js _Offset) |> ignore
        try
            x.Handle.Reference.Invoke("setVertexBuffer", js _Slot, js _Buffer, js _Offset) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.SetVertexBuffer(Slot : int, Buffer : Buffer, Offset : uint64, Size : uint64) : unit = 
        let _Slot = int (Slot)
        let _Buffer = (if isNull Buffer then null else Buffer.Handle)
        let _Offset = int (Offset)
        let _Size = int (Size)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "setVertexBuffer", js _Slot, js _Buffer, js _Offset, js _Size) |> ignore
        try
            x.Handle.Reference.Invoke("setVertexBuffer", js _Slot, js _Buffer, js _Offset, js _Size) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.SetIndexBuffer(Buffer : Buffer, Format : IndexFormat) : unit = 
        let _Buffer = (if isNull Buffer then null else Buffer.Handle)
        let _Format = Format.GetValue()
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "setIndexBuffer", js _Buffer, js _Format) |> ignore
        try
            x.Handle.Reference.Invoke("setIndexBuffer", js _Buffer, js _Format) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.SetIndexBuffer(Buffer : Buffer, Format : IndexFormat, Offset : uint64) : unit = 
        let _Buffer = (if isNull Buffer then null else Buffer.Handle)
        let _Format = Format.GetValue()
        let _Offset = int (Offset)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "setIndexBuffer", js _Buffer, js _Format, js _Offset) |> ignore
        try
            x.Handle.Reference.Invoke("setIndexBuffer", js _Buffer, js _Format, js _Offset) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.SetIndexBuffer(Buffer : Buffer, Format : IndexFormat, Offset : uint64, Size : uint64) : unit = 
        let _Buffer = (if isNull Buffer then null else Buffer.Handle)
        let _Format = Format.GetValue()
        let _Offset = int (Offset)
        let _Size = int (Size)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "setIndexBuffer", js _Buffer, js _Format, js _Offset, js _Size) |> ignore
        try
            x.Handle.Reference.Invoke("setIndexBuffer", js _Buffer, js _Format, js _Offset, js _Size) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.Finish() : RenderBundle = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "finish") |> ignore
        try
            new RenderBundle(x.Device, convert(x.Handle.Reference.Invoke("finish")))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.Finish(Descriptor : RenderBundleDescriptor) : RenderBundle = 
        let _Label = Descriptor.Label
        let _Descriptor = new DawnRaw.WGPURenderBundleDescriptor()
        _Descriptor.Label <- _Label
        let _Descriptor = _Descriptor
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "finish", js _Descriptor) |> ignore
        try
            new RenderBundle(x.Device, convert(x.Handle.Reference.Invoke("finish", js _Descriptor)))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
[<AllowNullLiteral>]
type RenderPassEncoder(device : Device, handle : RenderPassEncoderHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle = handle
    member x.IsDisposed = isDisposed
    member private x.Dispose(disposing : bool) =
        if not isDisposed then 
            let r = Interlocked.Decrement(&refCount.contents)
            isDisposed <- true
    member x.Dispose() = x.Dispose(true)
    member x.Clone() = 
        let mutable o = refCount.contents
        if o = 0 then raise <| System.ObjectDisposedException("RenderPassEncoder")
        let mutable n = Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        while o <> n do
            o <- n
            if o = 0 then raise <| System.ObjectDisposedException("RenderPassEncoder")
            n <- Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        new RenderPassEncoder(device, handle, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : RenderPassEncoderHandle) = new RenderPassEncoder(device, handle, ref 1)
    member x.SetPipeline(Pipeline : RenderPipeline) : unit = 
        let _Pipeline = (if isNull Pipeline then null else Pipeline.Handle)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "setPipeline", js _Pipeline) |> ignore
        try
            x.Handle.Reference.Invoke("setPipeline", js _Pipeline) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.SetBindGroup(GroupIndex : int, Group : BindGroup, DynamicOffsets : uint32[]) : unit = 
        let _GroupIndex = int (GroupIndex)
        let _Group = (if isNull Group then null else Group.Handle)
        use _DynamicOffsets = Uint32Array.op_Implicit(Span(DynamicOffsets))
        let _DynamicOffsetsCount = DynamicOffsets.Length
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "setBindGroup", js _GroupIndex, js _Group, js _DynamicOffsets) |> ignore
        try
            x.Handle.Reference.Invoke("setBindGroup", js _GroupIndex, js _Group, js _DynamicOffsets) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.Draw(VertexCount : int) : unit = 
        let _VertexCount = int (VertexCount)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "draw", js _VertexCount) |> ignore
        try
            x.Handle.Reference.Invoke("draw", js _VertexCount) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.Draw(VertexCount : int, InstanceCount : int) : unit = 
        let _VertexCount = int (VertexCount)
        let _InstanceCount = int (InstanceCount)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "draw", js _VertexCount, js _InstanceCount) |> ignore
        try
            x.Handle.Reference.Invoke("draw", js _VertexCount, js _InstanceCount) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.Draw(VertexCount : int, InstanceCount : int, FirstVertex : int) : unit = 
        let _VertexCount = int (VertexCount)
        let _InstanceCount = int (InstanceCount)
        let _FirstVertex = int (FirstVertex)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "draw", js _VertexCount, js _InstanceCount, js _FirstVertex) |> ignore
        try
            x.Handle.Reference.Invoke("draw", js _VertexCount, js _InstanceCount, js _FirstVertex) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.Draw(VertexCount : int, InstanceCount : int, FirstVertex : int, FirstInstance : int) : unit = 
        let _VertexCount = int (VertexCount)
        let _InstanceCount = int (InstanceCount)
        let _FirstVertex = int (FirstVertex)
        let _FirstInstance = int (FirstInstance)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "draw", js _VertexCount, js _InstanceCount, js _FirstVertex, js _FirstInstance) |> ignore
        try
            x.Handle.Reference.Invoke("draw", js _VertexCount, js _InstanceCount, js _FirstVertex, js _FirstInstance) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.DrawIndexed(IndexCount : int) : unit = 
        let _IndexCount = int (IndexCount)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "drawIndexed", js _IndexCount) |> ignore
        try
            x.Handle.Reference.Invoke("drawIndexed", js _IndexCount) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.DrawIndexed(IndexCount : int, InstanceCount : int) : unit = 
        let _IndexCount = int (IndexCount)
        let _InstanceCount = int (InstanceCount)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "drawIndexed", js _IndexCount, js _InstanceCount) |> ignore
        try
            x.Handle.Reference.Invoke("drawIndexed", js _IndexCount, js _InstanceCount) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.DrawIndexed(IndexCount : int, InstanceCount : int, FirstIndex : int) : unit = 
        let _IndexCount = int (IndexCount)
        let _InstanceCount = int (InstanceCount)
        let _FirstIndex = int (FirstIndex)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "drawIndexed", js _IndexCount, js _InstanceCount, js _FirstIndex) |> ignore
        try
            x.Handle.Reference.Invoke("drawIndexed", js _IndexCount, js _InstanceCount, js _FirstIndex) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.DrawIndexed(IndexCount : int, InstanceCount : int, FirstIndex : int, BaseVertex : int32) : unit = 
        let _IndexCount = int (IndexCount)
        let _InstanceCount = int (InstanceCount)
        let _FirstIndex = int (FirstIndex)
        let _BaseVertex = int (BaseVertex)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "drawIndexed", js _IndexCount, js _InstanceCount, js _FirstIndex, js _BaseVertex) |> ignore
        try
            x.Handle.Reference.Invoke("drawIndexed", js _IndexCount, js _InstanceCount, js _FirstIndex, js _BaseVertex) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.DrawIndexed(IndexCount : int, InstanceCount : int, FirstIndex : int, BaseVertex : int32, FirstInstance : int) : unit = 
        let _IndexCount = int (IndexCount)
        let _InstanceCount = int (InstanceCount)
        let _FirstIndex = int (FirstIndex)
        let _BaseVertex = int (BaseVertex)
        let _FirstInstance = int (FirstInstance)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "drawIndexed", js _IndexCount, js _InstanceCount, js _FirstIndex, js _BaseVertex, js _FirstInstance) |> ignore
        try
            x.Handle.Reference.Invoke("drawIndexed", js _IndexCount, js _InstanceCount, js _FirstIndex, js _BaseVertex, js _FirstInstance) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.DrawIndirect(IndirectBuffer : Buffer, IndirectOffset : uint64) : unit = 
        let _IndirectBuffer = (if isNull IndirectBuffer then null else IndirectBuffer.Handle)
        let _IndirectOffset = int (IndirectOffset)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "drawIndirect", js _IndirectBuffer, js _IndirectOffset) |> ignore
        try
            x.Handle.Reference.Invoke("drawIndirect", js _IndirectBuffer, js _IndirectOffset) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.DrawIndexedIndirect(IndirectBuffer : Buffer, IndirectOffset : uint64) : unit = 
        let _IndirectBuffer = (if isNull IndirectBuffer then null else IndirectBuffer.Handle)
        let _IndirectOffset = int (IndirectOffset)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "drawIndexedIndirect", js _IndirectBuffer, js _IndirectOffset) |> ignore
        try
            x.Handle.Reference.Invoke("drawIndexedIndirect", js _IndirectBuffer, js _IndirectOffset) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.ExecuteBundles(Bundles : array<RenderBundle>) : unit = 
        let _BundlesCount = Bundles.Length
        let _BundlesArray = newArray _BundlesCount
        for i in 0 .. _BundlesCount-1 do
            if isNull Bundles.[i] then _BundlesArray.[i] <- null
            else _BundlesArray.[i] <- Bundles.[i].Handle
        let _Bundles = _BundlesArray.Reference
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "executeBundles", js _Bundles) |> ignore
        try
            x.Handle.Reference.Invoke("executeBundles", js _Bundles) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.InsertDebugMarker(MarkerLabel : string) : unit = 
        let _MarkerLabel = MarkerLabel
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "insertDebugMarker", js _MarkerLabel) |> ignore
        try
            x.Handle.Reference.Invoke("insertDebugMarker", js _MarkerLabel) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.PopDebugGroup() : unit = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "popDebugGroup") |> ignore
        try
            x.Handle.Reference.Invoke("popDebugGroup") |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.PushDebugGroup(GroupLabel : string) : unit = 
        let _GroupLabel = GroupLabel
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "pushDebugGroup", js _GroupLabel) |> ignore
        try
            x.Handle.Reference.Invoke("pushDebugGroup", js _GroupLabel) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.SetStencilReference(Reference : int) : unit = 
        let _Reference = int (Reference)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "setStencilReference", js _Reference) |> ignore
        try
            x.Handle.Reference.Invoke("setStencilReference", js _Reference) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.SetBlendColor(Color : Color) : unit = 
        let _R = (Color.R)
        let _G = (Color.G)
        let _B = (Color.B)
        let _A = (Color.A)
        let _Color = new DawnRaw.WGPUColor()
        _Color.R <- _R
        _Color.G <- _G
        _Color.B <- _B
        _Color.A <- _A
        let _Color = _Color
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "setBlendColor", js _Color) |> ignore
        try
            x.Handle.Reference.Invoke("setBlendColor", js _Color) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.SetViewport(X : float32, Y : float32, Width : float32, Height : float32, MinDepth : float32, MaxDepth : float32) : unit = 
        let _X = (X)
        let _Y = (Y)
        let _Width = (Width)
        let _Height = (Height)
        let _MinDepth = (MinDepth)
        let _MaxDepth = (MaxDepth)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "setViewport", js _X, js _Y, js _Width, js _Height, js _MinDepth, js _MaxDepth) |> ignore
        try
            x.Handle.Reference.Invoke("setViewport", js _X, js _Y, js _Width, js _Height, js _MinDepth, js _MaxDepth) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.SetScissorRect(X : int, Y : int, Width : int, Height : int) : unit = 
        let _X = int (X)
        let _Y = int (Y)
        let _Width = int (Width)
        let _Height = int (Height)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "setScissorRect", js _X, js _Y, js _Width, js _Height) |> ignore
        try
            x.Handle.Reference.Invoke("setScissorRect", js _X, js _Y, js _Width, js _Height) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.SetVertexBuffer(Slot : int, Buffer : Buffer) : unit = 
        let _Slot = int (Slot)
        let _Buffer = (if isNull Buffer then null else Buffer.Handle)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "setVertexBuffer", js _Slot, js _Buffer) |> ignore
        try
            x.Handle.Reference.Invoke("setVertexBuffer", js _Slot, js _Buffer) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.SetVertexBuffer(Slot : int, Buffer : Buffer, Offset : uint64) : unit = 
        let _Slot = int (Slot)
        let _Buffer = (if isNull Buffer then null else Buffer.Handle)
        let _Offset = int (Offset)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "setVertexBuffer", js _Slot, js _Buffer, js _Offset) |> ignore
        try
            x.Handle.Reference.Invoke("setVertexBuffer", js _Slot, js _Buffer, js _Offset) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.SetVertexBuffer(Slot : int, Buffer : Buffer, Offset : uint64, Size : uint64) : unit = 
        let _Slot = int (Slot)
        let _Buffer = (if isNull Buffer then null else Buffer.Handle)
        let _Offset = int (Offset)
        let _Size = int (Size)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "setVertexBuffer", js _Slot, js _Buffer, js _Offset, js _Size) |> ignore
        try
            x.Handle.Reference.Invoke("setVertexBuffer", js _Slot, js _Buffer, js _Offset, js _Size) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.SetIndexBuffer(Buffer : Buffer, Format : IndexFormat) : unit = 
        let _Buffer = (if isNull Buffer then null else Buffer.Handle)
        let _Format = Format.GetValue()
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "setIndexBuffer", js _Buffer, js _Format) |> ignore
        try
            x.Handle.Reference.Invoke("setIndexBuffer", js _Buffer, js _Format) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.SetIndexBuffer(Buffer : Buffer, Format : IndexFormat, Offset : uint64) : unit = 
        let _Buffer = (if isNull Buffer then null else Buffer.Handle)
        let _Format = Format.GetValue()
        let _Offset = int (Offset)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "setIndexBuffer", js _Buffer, js _Format, js _Offset) |> ignore
        try
            x.Handle.Reference.Invoke("setIndexBuffer", js _Buffer, js _Format, js _Offset) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.SetIndexBuffer(Buffer : Buffer, Format : IndexFormat, Offset : uint64, Size : uint64) : unit = 
        let _Buffer = (if isNull Buffer then null else Buffer.Handle)
        let _Format = Format.GetValue()
        let _Offset = int (Offset)
        let _Size = int (Size)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "setIndexBuffer", js _Buffer, js _Format, js _Offset, js _Size) |> ignore
        try
            x.Handle.Reference.Invoke("setIndexBuffer", js _Buffer, js _Format, js _Offset, js _Size) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.WriteTimestamp(QuerySet : QuerySet, QueryIndex : int) : unit = 
        let _QuerySet = (if isNull QuerySet then null else QuerySet.Handle)
        let _QueryIndex = int (QueryIndex)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "writeTimestamp", js _QuerySet, js _QueryIndex) |> ignore
        try
            x.Handle.Reference.Invoke("writeTimestamp", js _QuerySet, js _QueryIndex) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.EndPass() : unit = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "endPass") |> ignore
        try
            x.Handle.Reference.Invoke("endPass") |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
[<AllowNullLiteral>]
type Texture(device : Device, handle : TextureHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle = handle
    member x.IsDisposed = isDisposed
    member private x.Dispose(disposing : bool) =
        if not isDisposed then 
            let r = Interlocked.Decrement(&refCount.contents)
            isDisposed <- true
    member x.Dispose() = x.Dispose(true)
    member x.Clone() = 
        let mutable o = refCount.contents
        if o = 0 then raise <| System.ObjectDisposedException("Texture")
        let mutable n = Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        while o <> n do
            o <- n
            if o = 0 then raise <| System.ObjectDisposedException("Texture")
            n <- Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        new Texture(device, handle, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : TextureHandle) = new Texture(device, handle, ref 1)
    member x.CreateView() : TextureView = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "createView") |> ignore
        try
            new TextureView(x.Device, convert(x.Handle.Reference.Invoke("createView")))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.CreateView(Descriptor : TextureViewDescriptor) : TextureView = 
        let _Label = Descriptor.Label
        let _Format = Descriptor.Format.GetValue()
        let _Dimension = Descriptor.Dimension.GetValue()
        let _BaseMipLevel = int (Descriptor.BaseMipLevel)
        let _MipLevelCount = int (Descriptor.MipLevelCount)
        let _BaseArrayLayer = int (Descriptor.BaseArrayLayer)
        let _ArrayLayerCount = int (Descriptor.ArrayLayerCount)
        let _Aspect = Descriptor.Aspect.GetValue()
        let _Descriptor = new DawnRaw.WGPUTextureViewDescriptor()
        _Descriptor.Label <- _Label
        _Descriptor.Format <- _Format
        _Descriptor.Dimension <- _Dimension
        _Descriptor.BaseMipLevel <- _BaseMipLevel
        _Descriptor.MipLevelCount <- _MipLevelCount
        _Descriptor.BaseArrayLayer <- _BaseArrayLayer
        _Descriptor.ArrayLayerCount <- _ArrayLayerCount
        _Descriptor.Aspect <- _Aspect
        let _Descriptor = _Descriptor
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "createView", js _Descriptor) |> ignore
        try
            new TextureView(x.Device, convert(x.Handle.Reference.Invoke("createView", js _Descriptor)))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.Destroy() : unit = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "destroy") |> ignore
        try
            x.Handle.Reference.Invoke("destroy") |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
[<AllowNullLiteral>]
type CommandEncoder(device : Device, handle : CommandEncoderHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle = handle
    member x.IsDisposed = isDisposed
    member private x.Dispose(disposing : bool) =
        if not isDisposed then 
            let r = Interlocked.Decrement(&refCount.contents)
            isDisposed <- true
    member x.Dispose() = x.Dispose(true)
    member x.Clone() = 
        let mutable o = refCount.contents
        if o = 0 then raise <| System.ObjectDisposedException("CommandEncoder")
        let mutable n = Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        while o <> n do
            o <- n
            if o = 0 then raise <| System.ObjectDisposedException("CommandEncoder")
            n <- Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        new CommandEncoder(device, handle, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : CommandEncoderHandle) = new CommandEncoder(device, handle, ref 1)
    member x.Finish() : CommandBuffer = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "finish") |> ignore
        try
            new CommandBuffer(x.Device, convert(x.Handle.Reference.Invoke("finish")))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.Finish(Descriptor : CommandBufferDescriptor) : CommandBuffer = 
        let _Label = Descriptor.Label
        let _Descriptor = new DawnRaw.WGPUCommandBufferDescriptor()
        _Descriptor.Label <- _Label
        let _Descriptor = _Descriptor
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "finish", js _Descriptor) |> ignore
        try
            new CommandBuffer(x.Device, convert(x.Handle.Reference.Invoke("finish", js _Descriptor)))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.BeginComputePass() : ComputePassEncoder = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "beginComputePass") |> ignore
        try
            new ComputePassEncoder(x.Device, convert(x.Handle.Reference.Invoke("beginComputePass")))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.BeginComputePass(Descriptor : ComputePassDescriptor) : ComputePassEncoder = 
        let _Label = Descriptor.Label
        let _Descriptor = new DawnRaw.WGPUComputePassDescriptor()
        _Descriptor.Label <- _Label
        let _Descriptor = _Descriptor
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "beginComputePass", js _Descriptor) |> ignore
        try
            new ComputePassEncoder(x.Device, convert(x.Handle.Reference.Invoke("beginComputePass", js _Descriptor)))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.BeginRenderPass(Descriptor : RenderPassDescriptor) : RenderPassEncoder = 
        let _Label = Descriptor.Label
        let _ColorAttachmentsCount = if isNull Descriptor.ColorAttachments then 0 else Descriptor.ColorAttachments.Length
        let rec _ColorAttachmentsCont (_ColorAttachmentsinputs : array<RenderPassColorAttachmentDescriptor>) (_ColorAttachmentsoutputs : JsArray) (_ColorAttachmentsi : int) =
            if _ColorAttachmentsi >= _ColorAttachmentsCount then
                let _ColorAttachments = _ColorAttachmentsoutputs.Reference
                let inline _DepthStencilAttachmentCont _DepthStencilAttachment = 
                    let _OcclusionQuerySet = (if isNull Descriptor.OcclusionQuerySet then null else Descriptor.OcclusionQuerySet.Handle)
                    let _Descriptor = new DawnRaw.WGPURenderPassDescriptor()
                    _Descriptor.Label <- _Label
                    _Descriptor.ColorAttachments <- _ColorAttachments
                    _Descriptor.DepthStencilAttachment <- _DepthStencilAttachment
                    _Descriptor.OcclusionQuerySet <- _OcclusionQuerySet
                    let _Descriptor = _Descriptor
                    let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
                    let console = window.GetObjectProperty("console") |> unbox<JSObject>
                    console.Invoke("debug", "beginRenderPass", js _Descriptor) |> ignore
                    try
                        new RenderPassEncoder(x.Device, convert(x.Handle.Reference.Invoke("beginRenderPass", js _Descriptor)))
                    with e ->
                        console.Invoke("error", string e) |> ignore
                        Unchecked.defaultof<_>
                match Descriptor.DepthStencilAttachment with
                | Some v ->
                    let _Attachment = (if isNull v.Attachment then null else v.Attachment.Handle)
                    let _DepthLoadValue = v.DepthLoadValue.GetValue()
                    let _DepthStoreOp = v.DepthStoreOp.GetValue()
                    let _DepthReadOnly = v.DepthReadOnly
                    let _StencilLoadValue = v.StencilLoadValue.GetValue()
                    let _StencilStoreOp = v.StencilStoreOp.GetValue()
                    let _StencilReadOnly = v.StencilReadOnly
                    let _n = new DawnRaw.WGPURenderPassDepthStencilAttachmentDescriptor()
                    _n.Attachment <- _Attachment
                    _n.DepthLoadValue <- _DepthLoadValue
                    _n.DepthStoreOp <- _DepthStoreOp
                    _n.DepthReadOnly <- _DepthReadOnly
                    _n.StencilLoadValue <- _StencilLoadValue
                    _n.StencilStoreOp <- _StencilStoreOp
                    _n.StencilReadOnly <- _StencilReadOnly
                    let _n = _n
                    _DepthStencilAttachmentCont _n
                | None -> _DepthStencilAttachmentCont null
            else
                let _Attachment = (if isNull _ColorAttachmentsinputs.[_ColorAttachmentsi].Attachment then null else _ColorAttachmentsinputs.[_ColorAttachmentsi].Attachment.Handle)
                let _ResolveTarget = (if isNull _ColorAttachmentsinputs.[_ColorAttachmentsi].ResolveTarget then null else _ColorAttachmentsinputs.[_ColorAttachmentsi].ResolveTarget.Handle)
                let _LoadValue = _ColorAttachmentsinputs.[_ColorAttachmentsi].LoadValue.GetValue()
                let _StoreOp = _ColorAttachmentsinputs.[_ColorAttachmentsi].StoreOp.GetValue()
                let _n = new DawnRaw.WGPURenderPassColorAttachmentDescriptor()
                _n.Attachment <- _Attachment
                _n.ResolveTarget <- _ResolveTarget
                _n.LoadValue <- _LoadValue
                _n.StoreOp <- _StoreOp
                let _n = _n
                _ColorAttachmentsoutputs.[_ColorAttachmentsi] <- _n
                _ColorAttachmentsCont _ColorAttachmentsinputs _ColorAttachmentsoutputs (_ColorAttachmentsi + 1)
        _ColorAttachmentsCont Descriptor.ColorAttachments (if _ColorAttachmentsCount > 0 then newArray _ColorAttachmentsCount else null) 0
    member x.CopyBufferToBuffer(Source : Buffer, SourceOffset : uint64, Destination : Buffer, DestinationOffset : uint64, Size : uint64) : unit = 
        let _Source = (if isNull Source then null else Source.Handle)
        let _SourceOffset = int (SourceOffset)
        let _Destination = (if isNull Destination then null else Destination.Handle)
        let _DestinationOffset = int (DestinationOffset)
        let _Size = int (Size)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "copyBufferToBuffer", js _Source, js _SourceOffset, js _Destination, js _DestinationOffset, js _Size) |> ignore
        try
            x.Handle.Reference.Invoke("copyBufferToBuffer", js _Source, js _SourceOffset, js _Destination, js _DestinationOffset, js _Size) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.CopyBufferToTexture(Source : BufferCopyView, Destination : TextureCopyView) : unit = 
        let _Offset = int (Source.Layout.Offset)
        let _BytesPerRow = int (Source.Layout.BytesPerRow)
        let _RowsPerImage = int (Source.Layout.RowsPerImage)
        let _Layout = new DawnRaw.WGPUTextureDataLayout()
        _Layout.Offset <- _Offset
        _Layout.BytesPerRow <- _BytesPerRow
        _Layout.RowsPerImage <- _RowsPerImage
        let _Layout = _Layout
        let _Buffer = (if isNull Source.Buffer then null else Source.Buffer.Handle)
        let _Source = new DawnRaw.WGPUBufferCopyView()
        _Source.Layout <- _Layout
        _Source.Buffer <- _Buffer
        let _Source = _Source
        let _Texture = (if isNull Destination.Texture then null else Destination.Texture.Handle)
        let _MipLevel = int (Destination.MipLevel)
        let _X = int (Destination.Origin.X)
        let _Y = int (Destination.Origin.Y)
        let _Z = int (Destination.Origin.Z)
        let _Origin = new DawnRaw.WGPUOrigin3D()
        _Origin.X <- _X
        _Origin.Y <- _Y
        _Origin.Z <- _Z
        let _Origin = _Origin
        let _Aspect = Destination.Aspect.GetValue()
        let _Destination = new DawnRaw.WGPUTextureCopyView()
        _Destination.Texture <- _Texture
        _Destination.MipLevel <- _MipLevel
        _Destination.Origin <- _Origin
        _Destination.Aspect <- _Aspect
        let _Destination = _Destination
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "copyBufferToTexture", js _Source, js _Destination) |> ignore
        try
            x.Handle.Reference.Invoke("copyBufferToTexture", js _Source, js _Destination) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.CopyBufferToTexture(Source : BufferCopyView, Destination : TextureCopyView, CopySize : Extent3D) : unit = 
        let _Offset = int (Source.Layout.Offset)
        let _BytesPerRow = int (Source.Layout.BytesPerRow)
        let _RowsPerImage = int (Source.Layout.RowsPerImage)
        let _Layout = new DawnRaw.WGPUTextureDataLayout()
        _Layout.Offset <- _Offset
        _Layout.BytesPerRow <- _BytesPerRow
        _Layout.RowsPerImage <- _RowsPerImage
        let _Layout = _Layout
        let _Buffer = (if isNull Source.Buffer then null else Source.Buffer.Handle)
        let _Source = new DawnRaw.WGPUBufferCopyView()
        _Source.Layout <- _Layout
        _Source.Buffer <- _Buffer
        let _Source = _Source
        let _Texture = (if isNull Destination.Texture then null else Destination.Texture.Handle)
        let _MipLevel = int (Destination.MipLevel)
        let _X = int (Destination.Origin.X)
        let _Y = int (Destination.Origin.Y)
        let _Z = int (Destination.Origin.Z)
        let _Origin = new DawnRaw.WGPUOrigin3D()
        _Origin.X <- _X
        _Origin.Y <- _Y
        _Origin.Z <- _Z
        let _Origin = _Origin
        let _Aspect = Destination.Aspect.GetValue()
        let _Destination = new DawnRaw.WGPUTextureCopyView()
        _Destination.Texture <- _Texture
        _Destination.MipLevel <- _MipLevel
        _Destination.Origin <- _Origin
        _Destination.Aspect <- _Aspect
        let _Destination = _Destination
        let _Width = int (CopySize.Width)
        let _Height = int (CopySize.Height)
        let _Depth = int (CopySize.Depth)
        let _CopySize = new DawnRaw.WGPUExtent3D()
        _CopySize.Width <- _Width
        _CopySize.Height <- _Height
        _CopySize.Depth <- _Depth
        let _CopySize = _CopySize
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "copyBufferToTexture", js _Source, js _Destination, js _CopySize) |> ignore
        try
            x.Handle.Reference.Invoke("copyBufferToTexture", js _Source, js _Destination, js _CopySize) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.CopyTextureToBuffer(Source : TextureCopyView, Destination : BufferCopyView) : unit = 
        let _Texture = (if isNull Source.Texture then null else Source.Texture.Handle)
        let _MipLevel = int (Source.MipLevel)
        let _X = int (Source.Origin.X)
        let _Y = int (Source.Origin.Y)
        let _Z = int (Source.Origin.Z)
        let _Origin = new DawnRaw.WGPUOrigin3D()
        _Origin.X <- _X
        _Origin.Y <- _Y
        _Origin.Z <- _Z
        let _Origin = _Origin
        let _Aspect = Source.Aspect.GetValue()
        let _Source = new DawnRaw.WGPUTextureCopyView()
        _Source.Texture <- _Texture
        _Source.MipLevel <- _MipLevel
        _Source.Origin <- _Origin
        _Source.Aspect <- _Aspect
        let _Source = _Source
        let _Offset = int (Destination.Layout.Offset)
        let _BytesPerRow = int (Destination.Layout.BytesPerRow)
        let _RowsPerImage = int (Destination.Layout.RowsPerImage)
        let _Layout = new DawnRaw.WGPUTextureDataLayout()
        _Layout.Offset <- _Offset
        _Layout.BytesPerRow <- _BytesPerRow
        _Layout.RowsPerImage <- _RowsPerImage
        let _Layout = _Layout
        let _Buffer = (if isNull Destination.Buffer then null else Destination.Buffer.Handle)
        let _Destination = new DawnRaw.WGPUBufferCopyView()
        _Destination.Layout <- _Layout
        _Destination.Buffer <- _Buffer
        let _Destination = _Destination
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "copyTextureToBuffer", js _Source, js _Destination) |> ignore
        try
            x.Handle.Reference.Invoke("copyTextureToBuffer", js _Source, js _Destination) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.CopyTextureToBuffer(Source : TextureCopyView, Destination : BufferCopyView, CopySize : Extent3D) : unit = 
        let _Texture = (if isNull Source.Texture then null else Source.Texture.Handle)
        let _MipLevel = int (Source.MipLevel)
        let _X = int (Source.Origin.X)
        let _Y = int (Source.Origin.Y)
        let _Z = int (Source.Origin.Z)
        let _Origin = new DawnRaw.WGPUOrigin3D()
        _Origin.X <- _X
        _Origin.Y <- _Y
        _Origin.Z <- _Z
        let _Origin = _Origin
        let _Aspect = Source.Aspect.GetValue()
        let _Source = new DawnRaw.WGPUTextureCopyView()
        _Source.Texture <- _Texture
        _Source.MipLevel <- _MipLevel
        _Source.Origin <- _Origin
        _Source.Aspect <- _Aspect
        let _Source = _Source
        let _Offset = int (Destination.Layout.Offset)
        let _BytesPerRow = int (Destination.Layout.BytesPerRow)
        let _RowsPerImage = int (Destination.Layout.RowsPerImage)
        let _Layout = new DawnRaw.WGPUTextureDataLayout()
        _Layout.Offset <- _Offset
        _Layout.BytesPerRow <- _BytesPerRow
        _Layout.RowsPerImage <- _RowsPerImage
        let _Layout = _Layout
        let _Buffer = (if isNull Destination.Buffer then null else Destination.Buffer.Handle)
        let _Destination = new DawnRaw.WGPUBufferCopyView()
        _Destination.Layout <- _Layout
        _Destination.Buffer <- _Buffer
        let _Destination = _Destination
        let _Width = int (CopySize.Width)
        let _Height = int (CopySize.Height)
        let _Depth = int (CopySize.Depth)
        let _CopySize = new DawnRaw.WGPUExtent3D()
        _CopySize.Width <- _Width
        _CopySize.Height <- _Height
        _CopySize.Depth <- _Depth
        let _CopySize = _CopySize
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "copyTextureToBuffer", js _Source, js _Destination, js _CopySize) |> ignore
        try
            x.Handle.Reference.Invoke("copyTextureToBuffer", js _Source, js _Destination, js _CopySize) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.CopyTextureToTexture(Source : TextureCopyView, Destination : TextureCopyView) : unit = 
        let _Texture = (if isNull Source.Texture then null else Source.Texture.Handle)
        let _MipLevel = int (Source.MipLevel)
        let _X = int (Source.Origin.X)
        let _Y = int (Source.Origin.Y)
        let _Z = int (Source.Origin.Z)
        let _Origin = new DawnRaw.WGPUOrigin3D()
        _Origin.X <- _X
        _Origin.Y <- _Y
        _Origin.Z <- _Z
        let _Origin = _Origin
        let _Aspect = Source.Aspect.GetValue()
        let _Source = new DawnRaw.WGPUTextureCopyView()
        _Source.Texture <- _Texture
        _Source.MipLevel <- _MipLevel
        _Source.Origin <- _Origin
        _Source.Aspect <- _Aspect
        let _Source = _Source
        let _Texture = (if isNull Destination.Texture then null else Destination.Texture.Handle)
        let _MipLevel = int (Destination.MipLevel)
        let _X = int (Destination.Origin.X)
        let _Y = int (Destination.Origin.Y)
        let _Z = int (Destination.Origin.Z)
        let _Origin = new DawnRaw.WGPUOrigin3D()
        _Origin.X <- _X
        _Origin.Y <- _Y
        _Origin.Z <- _Z
        let _Origin = _Origin
        let _Aspect = Destination.Aspect.GetValue()
        let _Destination = new DawnRaw.WGPUTextureCopyView()
        _Destination.Texture <- _Texture
        _Destination.MipLevel <- _MipLevel
        _Destination.Origin <- _Origin
        _Destination.Aspect <- _Aspect
        let _Destination = _Destination
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "copyTextureToTexture", js _Source, js _Destination) |> ignore
        try
            x.Handle.Reference.Invoke("copyTextureToTexture", js _Source, js _Destination) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.CopyTextureToTexture(Source : TextureCopyView, Destination : TextureCopyView, CopySize : Extent3D) : unit = 
        let _Texture = (if isNull Source.Texture then null else Source.Texture.Handle)
        let _MipLevel = int (Source.MipLevel)
        let _X = int (Source.Origin.X)
        let _Y = int (Source.Origin.Y)
        let _Z = int (Source.Origin.Z)
        let _Origin = new DawnRaw.WGPUOrigin3D()
        _Origin.X <- _X
        _Origin.Y <- _Y
        _Origin.Z <- _Z
        let _Origin = _Origin
        let _Aspect = Source.Aspect.GetValue()
        let _Source = new DawnRaw.WGPUTextureCopyView()
        _Source.Texture <- _Texture
        _Source.MipLevel <- _MipLevel
        _Source.Origin <- _Origin
        _Source.Aspect <- _Aspect
        let _Source = _Source
        let _Texture = (if isNull Destination.Texture then null else Destination.Texture.Handle)
        let _MipLevel = int (Destination.MipLevel)
        let _X = int (Destination.Origin.X)
        let _Y = int (Destination.Origin.Y)
        let _Z = int (Destination.Origin.Z)
        let _Origin = new DawnRaw.WGPUOrigin3D()
        _Origin.X <- _X
        _Origin.Y <- _Y
        _Origin.Z <- _Z
        let _Origin = _Origin
        let _Aspect = Destination.Aspect.GetValue()
        let _Destination = new DawnRaw.WGPUTextureCopyView()
        _Destination.Texture <- _Texture
        _Destination.MipLevel <- _MipLevel
        _Destination.Origin <- _Origin
        _Destination.Aspect <- _Aspect
        let _Destination = _Destination
        let _Width = int (CopySize.Width)
        let _Height = int (CopySize.Height)
        let _Depth = int (CopySize.Depth)
        let _CopySize = new DawnRaw.WGPUExtent3D()
        _CopySize.Width <- _Width
        _CopySize.Height <- _Height
        _CopySize.Depth <- _Depth
        let _CopySize = _CopySize
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "copyTextureToTexture", js _Source, js _Destination, js _CopySize) |> ignore
        try
            x.Handle.Reference.Invoke("copyTextureToTexture", js _Source, js _Destination, js _CopySize) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.InsertDebugMarker(MarkerLabel : string) : unit = 
        let _MarkerLabel = MarkerLabel
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "insertDebugMarker", js _MarkerLabel) |> ignore
        try
            x.Handle.Reference.Invoke("insertDebugMarker", js _MarkerLabel) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.PopDebugGroup() : unit = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "popDebugGroup") |> ignore
        try
            x.Handle.Reference.Invoke("popDebugGroup") |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.PushDebugGroup(GroupLabel : string) : unit = 
        let _GroupLabel = GroupLabel
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "pushDebugGroup", js _GroupLabel) |> ignore
        try
            x.Handle.Reference.Invoke("pushDebugGroup", js _GroupLabel) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.ResolveQuerySet(QuerySet : QuerySet, FirstQuery : int, QueryCount : int, Destination : Buffer, DestinationOffset : uint64) : unit = 
        let _QuerySet = (if isNull QuerySet then null else QuerySet.Handle)
        let _FirstQuery = int (FirstQuery)
        let _QueryCount = int (QueryCount)
        let _Destination = (if isNull Destination then null else Destination.Handle)
        let _DestinationOffset = int (DestinationOffset)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "resolveQuerySet", js _QuerySet, js _FirstQuery, js _QueryCount, js _Destination, js _DestinationOffset) |> ignore
        try
            x.Handle.Reference.Invoke("resolveQuerySet", js _QuerySet, js _FirstQuery, js _QueryCount, js _Destination, js _DestinationOffset) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.WriteTimestamp(QuerySet : QuerySet, QueryIndex : int) : unit = 
        let _QuerySet = (if isNull QuerySet then null else QuerySet.Handle)
        let _QueryIndex = int (QueryIndex)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "writeTimestamp", js _QuerySet, js _QueryIndex) |> ignore
        try
            x.Handle.Reference.Invoke("writeTimestamp", js _QuerySet, js _QueryIndex) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
[<AllowNullLiteral>]
type Queue(device : Device, handle : QueueHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle = handle
    member x.IsDisposed = isDisposed
    member private x.Dispose(disposing : bool) =
        if not isDisposed then 
            let r = Interlocked.Decrement(&refCount.contents)
            isDisposed <- true
    member x.Dispose() = x.Dispose(true)
    member x.Clone() = 
        let mutable o = refCount.contents
        if o = 0 then raise <| System.ObjectDisposedException("Queue")
        let mutable n = Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        while o <> n do
            o <- n
            if o = 0 then raise <| System.ObjectDisposedException("Queue")
            n <- Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        new Queue(device, handle, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : QueueHandle) = new Queue(device, handle, ref 1)
    member x.Submit(Commands : array<CommandBuffer>) : unit = 
        let _CommandsCount = Commands.Length
        let _CommandsArray = newArray _CommandsCount
        for i in 0 .. _CommandsCount-1 do
            if isNull Commands.[i] then _CommandsArray.[i] <- null
            else _CommandsArray.[i] <- Commands.[i].Handle
        let _Commands = _CommandsArray.Reference
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "submit", js _Commands) |> ignore
        try
            x.Handle.Reference.Invoke("submit", js _Commands) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.Signal(Fence : Fence, SignalValue : uint64) : unit = 
        let _Fence = (if isNull Fence then null else Fence.Handle)
        let _SignalValue = int (SignalValue)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "signal", js _Fence, js _SignalValue) |> ignore
        try
            x.Handle.Reference.Invoke("signal", js _Fence, js _SignalValue) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.CreateFence() : Fence = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "createFence") |> ignore
        try
            new Fence(x.Device, convert(x.Handle.Reference.Invoke("createFence")))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.CreateFence(Descriptor : FenceDescriptor) : Fence = 
        let _Label = Descriptor.Label
        let _InitialValue = int (Descriptor.InitialValue)
        let _Descriptor = new DawnRaw.WGPUFenceDescriptor()
        _Descriptor.Label <- _Label
        _Descriptor.InitialValue <- _InitialValue
        let _Descriptor = _Descriptor
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "createFence", js _Descriptor) |> ignore
        try
            new Fence(x.Device, convert(x.Handle.Reference.Invoke("createFence", js _Descriptor)))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.WriteBuffer(Buffer : Buffer, BufferOffset : uint64, Data : nativeint, Size : unativeint) : unit = 
        let _Buffer = (if isNull Buffer then null else Buffer.Handle)
        let _BufferOffset = int (BufferOffset)
        let _Data = int (Data)
        let _Size = int (Size)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "writeBuffer", js _Buffer, js _BufferOffset, js _Data, js _Size) |> ignore
        try
            x.Handle.Reference.Invoke("writeBuffer", js _Buffer, js _BufferOffset, js _Data, js _Size) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.WriteTexture(Destination : TextureCopyView, Data : nativeint, DataSize : unativeint, DataLayout : TextureDataLayout) : unit = 
        let _Texture = (if isNull Destination.Texture then null else Destination.Texture.Handle)
        let _MipLevel = int (Destination.MipLevel)
        let _X = int (Destination.Origin.X)
        let _Y = int (Destination.Origin.Y)
        let _Z = int (Destination.Origin.Z)
        let _Origin = new DawnRaw.WGPUOrigin3D()
        _Origin.X <- _X
        _Origin.Y <- _Y
        _Origin.Z <- _Z
        let _Origin = _Origin
        let _Aspect = Destination.Aspect.GetValue()
        let _Destination = new DawnRaw.WGPUTextureCopyView()
        _Destination.Texture <- _Texture
        _Destination.MipLevel <- _MipLevel
        _Destination.Origin <- _Origin
        _Destination.Aspect <- _Aspect
        let _Destination = _Destination
        let _Data = int (Data)
        let _DataSize = int (DataSize)
        let _Offset = int (DataLayout.Offset)
        let _BytesPerRow = int (DataLayout.BytesPerRow)
        let _RowsPerImage = int (DataLayout.RowsPerImage)
        let _DataLayout = new DawnRaw.WGPUTextureDataLayout()
        _DataLayout.Offset <- _Offset
        _DataLayout.BytesPerRow <- _BytesPerRow
        _DataLayout.RowsPerImage <- _RowsPerImage
        let _DataLayout = _DataLayout
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "writeTexture", js _Destination, js _Data, js _DataSize, js _DataLayout) |> ignore
        try
            x.Handle.Reference.Invoke("writeTexture", js _Destination, js _Data, js _DataSize, js _DataLayout) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.WriteTexture(Destination : TextureCopyView, Data : nativeint, DataSize : unativeint, DataLayout : TextureDataLayout, WriteSize : Extent3D) : unit = 
        let _Texture = (if isNull Destination.Texture then null else Destination.Texture.Handle)
        let _MipLevel = int (Destination.MipLevel)
        let _X = int (Destination.Origin.X)
        let _Y = int (Destination.Origin.Y)
        let _Z = int (Destination.Origin.Z)
        let _Origin = new DawnRaw.WGPUOrigin3D()
        _Origin.X <- _X
        _Origin.Y <- _Y
        _Origin.Z <- _Z
        let _Origin = _Origin
        let _Aspect = Destination.Aspect.GetValue()
        let _Destination = new DawnRaw.WGPUTextureCopyView()
        _Destination.Texture <- _Texture
        _Destination.MipLevel <- _MipLevel
        _Destination.Origin <- _Origin
        _Destination.Aspect <- _Aspect
        let _Destination = _Destination
        let _Data = int (Data)
        let _DataSize = int (DataSize)
        let _Offset = int (DataLayout.Offset)
        let _BytesPerRow = int (DataLayout.BytesPerRow)
        let _RowsPerImage = int (DataLayout.RowsPerImage)
        let _DataLayout = new DawnRaw.WGPUTextureDataLayout()
        _DataLayout.Offset <- _Offset
        _DataLayout.BytesPerRow <- _BytesPerRow
        _DataLayout.RowsPerImage <- _RowsPerImage
        let _DataLayout = _DataLayout
        let _Width = int (WriteSize.Width)
        let _Height = int (WriteSize.Height)
        let _Depth = int (WriteSize.Depth)
        let _WriteSize = new DawnRaw.WGPUExtent3D()
        _WriteSize.Width <- _Width
        _WriteSize.Height <- _Height
        _WriteSize.Depth <- _Depth
        let _WriteSize = _WriteSize
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "writeTexture", js _Destination, js _Data, js _DataSize, js _DataLayout, js _WriteSize) |> ignore
        try
            x.Handle.Reference.Invoke("writeTexture", js _Destination, js _Data, js _DataSize, js _DataLayout, js _WriteSize) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
[<AllowNullLiteral>]
type Device(handle : DeviceHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.ReferenceCount = !refCount
    member x.Handle = handle
    member x.IsDisposed = isDisposed
    member private x.Dispose(disposing : bool) =
        if not isDisposed then 
            let r = Interlocked.Decrement(&refCount.contents)
            isDisposed <- true
    member x.Dispose() = x.Dispose(true)
    member x.Clone() = 
        let mutable o = refCount.contents
        if o = 0 then raise <| System.ObjectDisposedException("Device")
        let mutable n = Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        while o <> n do
            o <- n
            if o = 0 then raise <| System.ObjectDisposedException("Device")
            n <- Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        new Device(handle, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(handle : DeviceHandle) = new Device(handle, ref 1)
    member x.CreateBindGroup(Descriptor : BindGroupDescriptor) : BindGroup = 
        let _Label = Descriptor.Label
        let _Layout = (if isNull Descriptor.Layout then null else Descriptor.Layout.Handle)
        let _EntriesCount = if isNull Descriptor.Entries then 0 else Descriptor.Entries.Length
        let rec _EntriesCont (_Entriesinputs : array<BindGroupEntry>) (_Entriesoutputs : JsArray) (_Entriesi : int) =
            if _Entriesi >= _EntriesCount then
                let _Entries = _Entriesoutputs.Reference
                let _Descriptor = new DawnRaw.WGPUBindGroupDescriptor()
                _Descriptor.Label <- _Label
                _Descriptor.Layout <- _Layout
                _Descriptor.Entries <- _Entries
                let _Descriptor = _Descriptor
                let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
                let console = window.GetObjectProperty("console") |> unbox<JSObject>
                console.Invoke("debug", "createBindGroup", js _Descriptor) |> ignore
                try
                    new BindGroup(x, convert(x.Handle.Reference.Invoke("createBindGroup", js _Descriptor)))
                with e ->
                    console.Invoke("error", string e) |> ignore
                    Unchecked.defaultof<_>
            else
                let _Binding = int (_Entriesinputs.[_Entriesi].Binding)
                let _Buffer = (if isNull _Entriesinputs.[_Entriesi].Buffer then null else _Entriesinputs.[_Entriesi].Buffer.Handle)
                let _Offset = int (_Entriesinputs.[_Entriesi].Offset)
                let _Size = int (_Entriesinputs.[_Entriesi].Size)
                let _Sampler = (if isNull _Entriesinputs.[_Entriesi].Sampler then null else _Entriesinputs.[_Entriesi].Sampler.Handle)
                let _TextureView = (if isNull _Entriesinputs.[_Entriesi].TextureView then null else _Entriesinputs.[_Entriesi].TextureView.Handle)
                let _n = new DawnRaw.WGPUBindGroupEntry()
                _n.Binding <- _Binding
                _n.Buffer <- _Buffer
                _n.Offset <- _Offset
                _n.Size <- _Size
                _n.Sampler <- _Sampler
                _n.TextureView <- _TextureView
                let _n = _n
                _Entriesoutputs.[_Entriesi] <- _n
                _EntriesCont _Entriesinputs _Entriesoutputs (_Entriesi + 1)
        _EntriesCont Descriptor.Entries (if _EntriesCount > 0 then newArray _EntriesCount else null) 0
    member x.CreateBindGroupLayout(Descriptor : BindGroupLayoutDescriptor) : BindGroupLayout = 
        let _Label = Descriptor.Label
        let _EntriesCount = if isNull Descriptor.Entries then 0 else Descriptor.Entries.Length
        let rec _EntriesCont (_Entriesinputs : array<BindGroupLayoutEntry>) (_Entriesoutputs : JsArray) (_Entriesi : int) =
            if _Entriesi >= _EntriesCount then
                let _Entries = _Entriesoutputs.Reference
                let _Descriptor = new DawnRaw.WGPUBindGroupLayoutDescriptor()
                _Descriptor.Label <- _Label
                _Descriptor.Entries <- _Entries
                let _Descriptor = _Descriptor
                let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
                let console = window.GetObjectProperty("console") |> unbox<JSObject>
                console.Invoke("debug", "createBindGroupLayout", js _Descriptor) |> ignore
                try
                    new BindGroupLayout(x, convert(x.Handle.Reference.Invoke("createBindGroupLayout", js _Descriptor)))
                with e ->
                    console.Invoke("error", string e) |> ignore
                    Unchecked.defaultof<_>
            else
                let _Binding = int (_Entriesinputs.[_Entriesi].Binding)
                let _Visibility = int (_Entriesinputs.[_Entriesi].Visibility)
                let _Type = _Entriesinputs.[_Entriesi].Type.GetValue()
                let _HasDynamicOffset = _Entriesinputs.[_Entriesi].HasDynamicOffset
                let _MinBufferBindingSize = int (_Entriesinputs.[_Entriesi].MinBufferBindingSize)
                let _Multisampled = _Entriesinputs.[_Entriesi].Multisampled
                let _ViewDimension = _Entriesinputs.[_Entriesi].ViewDimension.GetValue()
                let _TextureComponentType = _Entriesinputs.[_Entriesi].TextureComponentType.GetValue()
                let _StorageTextureFormat = _Entriesinputs.[_Entriesi].StorageTextureFormat.GetValue()
                let _n = new DawnRaw.WGPUBindGroupLayoutEntry()
                _n.Binding <- _Binding
                _n.Visibility <- _Visibility
                _n.Type <- _Type
                _n.HasDynamicOffset <- _HasDynamicOffset
                _n.MinBufferBindingSize <- _MinBufferBindingSize
                _n.Multisampled <- _Multisampled
                _n.ViewDimension <- _ViewDimension
                _n.TextureComponentType <- _TextureComponentType
                _n.StorageTextureFormat <- _StorageTextureFormat
                let _n = _n
                _Entriesoutputs.[_Entriesi] <- _n
                _EntriesCont _Entriesinputs _Entriesoutputs (_Entriesi + 1)
        _EntriesCont Descriptor.Entries (if _EntriesCount > 0 then newArray _EntriesCount else null) 0
    member x.CreateBuffer(Descriptor : BufferDescriptor) : Buffer = 
        let _Label = Descriptor.Label
        let _Usage = int (Descriptor.Usage)
        let _Size = int (Descriptor.Size)
        let _MappedAtCreation = Descriptor.MappedAtCreation
        let _Descriptor = new DawnRaw.WGPUBufferDescriptor()
        _Descriptor.Label <- _Label
        _Descriptor.Usage <- _Usage
        _Descriptor.Size <- _Size
        _Descriptor.MappedAtCreation <- _MappedAtCreation
        let _Descriptor = _Descriptor
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "createBuffer", js _Descriptor) |> ignore
        try
            new Buffer(x, convert(x.Handle.Reference.Invoke("createBuffer", js _Descriptor)))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.CreateErrorBuffer() : Buffer = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "createErrorBuffer") |> ignore
        try
            new Buffer(x, convert(x.Handle.Reference.Invoke("createErrorBuffer")))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.CreateCommandEncoder() : CommandEncoder = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "createCommandEncoder") |> ignore
        try
            new CommandEncoder(x, convert(x.Handle.Reference.Invoke("createCommandEncoder")))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.CreateCommandEncoder(Descriptor : CommandEncoderDescriptor) : CommandEncoder = 
        let _Label = Descriptor.Label
        let _Descriptor = new DawnRaw.WGPUCommandEncoderDescriptor()
        _Descriptor.Label <- _Label
        let _Descriptor = _Descriptor
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "createCommandEncoder", js _Descriptor) |> ignore
        try
            new CommandEncoder(x, convert(x.Handle.Reference.Invoke("createCommandEncoder", js _Descriptor)))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.CreateComputePipeline(Descriptor : ComputePipelineDescriptor) : ComputePipeline = 
        let _Label = Descriptor.Label
        let _Layout = (if isNull Descriptor.Layout then null else Descriptor.Layout.Handle)
        let _Module = (if isNull Descriptor.ComputeStage.Module then null else Descriptor.ComputeStage.Module.Handle)
        let _EntryPoint = Descriptor.ComputeStage.EntryPoint
        let _ComputeStage = new DawnRaw.WGPUProgrammableStageDescriptor()
        _ComputeStage.Module <- _Module
        _ComputeStage.EntryPoint <- _EntryPoint
        let _ComputeStage = _ComputeStage
        let _Descriptor = new DawnRaw.WGPUComputePipelineDescriptor()
        _Descriptor.Label <- _Label
        _Descriptor.Layout <- _Layout
        _Descriptor.ComputeStage <- _ComputeStage
        let _Descriptor = _Descriptor
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "createComputePipeline", js _Descriptor) |> ignore
        try
            new ComputePipeline(x, convert(x.Handle.Reference.Invoke("createComputePipeline", js _Descriptor)))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.CreateReadyComputePipeline(Descriptor : ComputePipelineDescriptor, Callback : CreateReadyComputePipelineCallback) : unit = 
        let _Label = Descriptor.Label
        let _Layout = (if isNull Descriptor.Layout then null else Descriptor.Layout.Handle)
        let _Module = (if isNull Descriptor.ComputeStage.Module then null else Descriptor.ComputeStage.Module.Handle)
        let _EntryPoint = Descriptor.ComputeStage.EntryPoint
        let _ComputeStage = new DawnRaw.WGPUProgrammableStageDescriptor()
        _ComputeStage.Module <- _Module
        _ComputeStage.EntryPoint <- _EntryPoint
        let _ComputeStage = _ComputeStage
        let _Descriptor = new DawnRaw.WGPUComputePipelineDescriptor()
        _Descriptor.Label <- _Label
        _Descriptor.Layout <- _Layout
        _Descriptor.ComputeStage <- _ComputeStage
        let _Descriptor = _Descriptor
        let mutable _CallbackGC = Unchecked.defaultof<System.Runtime.InteropServices.GCHandle>
        let _CallbackFunction (Status : obj) (Pipeline : ComputePipelineHandle) (Message : string) (Userdata : int) = 
            let _Status = Status
            let _Pipeline = Pipeline
            let _Message = Message
            let _Userdata = Userdata
            if _CallbackGC.IsAllocated then _CallbackGC.Free()
            Callback.Invoke(CreateReadyPipelineStatus.Parse(_Status), new ComputePipeline(x, _Pipeline), _Message, nativeint _Userdata)
        let _CallbackDel = WGPUCreateReadyComputePipelineCallback(_CallbackFunction)
        _CallbackGC <- System.Runtime.InteropServices.GCHandle.Alloc(_CallbackDel)
        let _Callback = _CallbackDel
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "createReadyComputePipeline", js _Descriptor, js _Callback) |> ignore
        try
            x.Handle.Reference.Invoke("createReadyComputePipeline", js _Descriptor, js _Callback) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.CreateReadyComputePipeline(Descriptor : ComputePipelineDescriptor, Callback : CreateReadyComputePipelineCallback, Userdata : nativeint) : unit = 
        let _Label = Descriptor.Label
        let _Layout = (if isNull Descriptor.Layout then null else Descriptor.Layout.Handle)
        let _Module = (if isNull Descriptor.ComputeStage.Module then null else Descriptor.ComputeStage.Module.Handle)
        let _EntryPoint = Descriptor.ComputeStage.EntryPoint
        let _ComputeStage = new DawnRaw.WGPUProgrammableStageDescriptor()
        _ComputeStage.Module <- _Module
        _ComputeStage.EntryPoint <- _EntryPoint
        let _ComputeStage = _ComputeStage
        let _Descriptor = new DawnRaw.WGPUComputePipelineDescriptor()
        _Descriptor.Label <- _Label
        _Descriptor.Layout <- _Layout
        _Descriptor.ComputeStage <- _ComputeStage
        let _Descriptor = _Descriptor
        let mutable _CallbackGC = Unchecked.defaultof<System.Runtime.InteropServices.GCHandle>
        let _CallbackFunction (Status : obj) (Pipeline : ComputePipelineHandle) (Message : string) (Userdata : int) = 
            let _Status = Status
            let _Pipeline = Pipeline
            let _Message = Message
            let _Userdata = Userdata
            if _CallbackGC.IsAllocated then _CallbackGC.Free()
            Callback.Invoke(CreateReadyPipelineStatus.Parse(_Status), new ComputePipeline(x, _Pipeline), _Message, nativeint _Userdata)
        let _CallbackDel = WGPUCreateReadyComputePipelineCallback(_CallbackFunction)
        _CallbackGC <- System.Runtime.InteropServices.GCHandle.Alloc(_CallbackDel)
        let _Callback = _CallbackDel
        let _Userdata = int (Userdata)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "createReadyComputePipeline", js _Descriptor, js _Callback, js _Userdata) |> ignore
        try
            x.Handle.Reference.Invoke("createReadyComputePipeline", js _Descriptor, js _Callback, js _Userdata) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.CreatePipelineLayout(Descriptor : PipelineLayoutDescriptor) : PipelineLayout = 
        let _Label = Descriptor.Label
        let _BindGroupLayoutsCount = Descriptor.BindGroupLayouts.Length
        let _BindGroupLayoutsArray = newArray _BindGroupLayoutsCount
        for i in 0 .. _BindGroupLayoutsCount-1 do
            if isNull Descriptor.BindGroupLayouts.[i] then _BindGroupLayoutsArray.[i] <- null
            else _BindGroupLayoutsArray.[i] <- Descriptor.BindGroupLayouts.[i].Handle
        let _BindGroupLayouts = _BindGroupLayoutsArray.Reference
        let _Descriptor = new DawnRaw.WGPUPipelineLayoutDescriptor()
        _Descriptor.Label <- _Label
        _Descriptor.BindGroupLayouts <- _BindGroupLayouts
        let _Descriptor = _Descriptor
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "createPipelineLayout", js _Descriptor) |> ignore
        try
            new PipelineLayout(x, convert(x.Handle.Reference.Invoke("createPipelineLayout", js _Descriptor)))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.CreateQuerySet(Descriptor : QuerySetDescriptor) : QuerySet = 
        let _Label = Descriptor.Label
        let _Type = Descriptor.Type.GetValue()
        let _Count = int (Descriptor.Count)
        let inline _PipelineStatisticsCont _PipelineStatistics =
            let _PipelineStatisticsCount = int (Descriptor.PipelineStatisticsCount)
            let _Descriptor = new DawnRaw.WGPUQuerySetDescriptor()
            _Descriptor.Label <- _Label
            _Descriptor.Type <- _Type
            _Descriptor.Count <- _Count
            _Descriptor.PipelineStatistics <- _PipelineStatistics
            _Descriptor.PipelineStatisticsCount <- _PipelineStatisticsCount
            let _Descriptor = _Descriptor
            let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
            let console = window.GetObjectProperty("console") |> unbox<JSObject>
            console.Invoke("debug", "createQuerySet", js _Descriptor) |> ignore
            try
                new QuerySet(x, convert(x.Handle.Reference.Invoke("createQuerySet", js _Descriptor)))
            with e ->
                console.Invoke("error", string e) |> ignore
                Unchecked.defaultof<_>
        match Descriptor.PipelineStatistics with
        | Some o ->
            _PipelineStatisticsCont(o.GetValue())
        | _ ->
            _PipelineStatisticsCont null
    member x.CreateReadyRenderPipeline(Descriptor : RenderPipelineDescriptor, Callback : CreateReadyRenderPipelineCallback) : unit = 
        let _Label = Descriptor.Label
        let _Layout = (if isNull Descriptor.Layout then null else Descriptor.Layout.Handle)
        let _Module = (if isNull Descriptor.VertexStage.Module then null else Descriptor.VertexStage.Module.Handle)
        let _EntryPoint = Descriptor.VertexStage.EntryPoint
        let _VertexStage = new DawnRaw.WGPUProgrammableStageDescriptor()
        _VertexStage.Module <- _Module
        _VertexStage.EntryPoint <- _EntryPoint
        let _VertexStage = _VertexStage
        let inline _FragmentStageCont _FragmentStage = 
            let inline _VertexStateCont _VertexState = 
                let _PrimitiveTopology = Descriptor.PrimitiveTopology.GetValue()
                let inline _RasterizationStateCont _RasterizationState = 
                    let _SampleCount = int (Descriptor.SampleCount)
                    let inline _DepthStencilStateCont _DepthStencilState = 
                        let _ColorStatesCount = if isNull Descriptor.ColorStates then 0 else Descriptor.ColorStates.Length
                        let rec _ColorStatesCont (_ColorStatesinputs : array<ColorStateDescriptor>) (_ColorStatesoutputs : JsArray) (_ColorStatesi : int) =
                            if _ColorStatesi >= _ColorStatesCount then
                                let _ColorStates = _ColorStatesoutputs.Reference
                                let _SampleMask = int (Descriptor.SampleMask)
                                let _AlphaToCoverageEnabled = Descriptor.AlphaToCoverageEnabled
                                let _Descriptor = new DawnRaw.WGPURenderPipelineDescriptor()
                                _Descriptor.Label <- _Label
                                _Descriptor.Layout <- _Layout
                                _Descriptor.VertexStage <- _VertexStage
                                _Descriptor.FragmentStage <- _FragmentStage
                                _Descriptor.VertexState <- _VertexState
                                _Descriptor.PrimitiveTopology <- _PrimitiveTopology
                                _Descriptor.RasterizationState <- _RasterizationState
                                _Descriptor.SampleCount <- _SampleCount
                                _Descriptor.DepthStencilState <- _DepthStencilState
                                _Descriptor.ColorStates <- _ColorStates
                                _Descriptor.SampleMask <- _SampleMask
                                _Descriptor.AlphaToCoverageEnabled <- _AlphaToCoverageEnabled
                                let _Descriptor = _Descriptor
                                let mutable _CallbackGC = Unchecked.defaultof<System.Runtime.InteropServices.GCHandle>
                                let _CallbackFunction (Status : obj) (Pipeline : RenderPipelineHandle) (Message : string) (Userdata : int) = 
                                    let _Status = Status
                                    let _Pipeline = Pipeline
                                    let _Message = Message
                                    let _Userdata = Userdata
                                    if _CallbackGC.IsAllocated then _CallbackGC.Free()
                                    Callback.Invoke(CreateReadyPipelineStatus.Parse(_Status), new RenderPipeline(x, _Pipeline), _Message, nativeint _Userdata)
                                let _CallbackDel = WGPUCreateReadyRenderPipelineCallback(_CallbackFunction)
                                _CallbackGC <- System.Runtime.InteropServices.GCHandle.Alloc(_CallbackDel)
                                let _Callback = _CallbackDel
                                let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
                                let console = window.GetObjectProperty("console") |> unbox<JSObject>
                                console.Invoke("debug", "createReadyRenderPipeline", js _Descriptor, js _Callback) |> ignore
                                try
                                    x.Handle.Reference.Invoke("createReadyRenderPipeline", js _Descriptor, js _Callback) |> ignore
                                with e ->
                                    console.Invoke("error", string e) |> ignore
                                    Unchecked.defaultof<_>
                            else
                                let _Format = _ColorStatesinputs.[_ColorStatesi].Format.GetValue()
                                let _Operation = _ColorStatesinputs.[_ColorStatesi].AlphaBlend.Operation.GetValue()
                                let _SrcFactor = _ColorStatesinputs.[_ColorStatesi].AlphaBlend.SrcFactor.GetValue()
                                let _DstFactor = _ColorStatesinputs.[_ColorStatesi].AlphaBlend.DstFactor.GetValue()
                                let _AlphaBlend = new DawnRaw.WGPUBlendDescriptor()
                                _AlphaBlend.Operation <- _Operation
                                _AlphaBlend.SrcFactor <- _SrcFactor
                                _AlphaBlend.DstFactor <- _DstFactor
                                let _AlphaBlend = _AlphaBlend
                                let _Operation = _ColorStatesinputs.[_ColorStatesi].ColorBlend.Operation.GetValue()
                                let _SrcFactor = _ColorStatesinputs.[_ColorStatesi].ColorBlend.SrcFactor.GetValue()
                                let _DstFactor = _ColorStatesinputs.[_ColorStatesi].ColorBlend.DstFactor.GetValue()
                                let _ColorBlend = new DawnRaw.WGPUBlendDescriptor()
                                _ColorBlend.Operation <- _Operation
                                _ColorBlend.SrcFactor <- _SrcFactor
                                _ColorBlend.DstFactor <- _DstFactor
                                let _ColorBlend = _ColorBlend
                                let _WriteMask = int (_ColorStatesinputs.[_ColorStatesi].WriteMask)
                                let _n = new DawnRaw.WGPUColorStateDescriptor()
                                _n.Format <- _Format
                                _n.AlphaBlend <- _AlphaBlend
                                _n.ColorBlend <- _ColorBlend
                                _n.WriteMask <- _WriteMask
                                let _n = _n
                                _ColorStatesoutputs.[_ColorStatesi] <- _n
                                _ColorStatesCont _ColorStatesinputs _ColorStatesoutputs (_ColorStatesi + 1)
                        _ColorStatesCont Descriptor.ColorStates (if _ColorStatesCount > 0 then newArray _ColorStatesCount else null) 0
                    match Descriptor.DepthStencilState with
                    | Some v ->
                        let _Format = v.Format.GetValue()
                        let _DepthWriteEnabled = v.DepthWriteEnabled
                        let _DepthCompare = v.DepthCompare.GetValue()
                        let _Compare = v.StencilFront.Compare.GetValue()
                        let _FailOp = v.StencilFront.FailOp.GetValue()
                        let _DepthFailOp = v.StencilFront.DepthFailOp.GetValue()
                        let _PassOp = v.StencilFront.PassOp.GetValue()
                        let _StencilFront = new DawnRaw.WGPUStencilStateFaceDescriptor()
                        _StencilFront.Compare <- _Compare
                        _StencilFront.FailOp <- _FailOp
                        _StencilFront.DepthFailOp <- _DepthFailOp
                        _StencilFront.PassOp <- _PassOp
                        let _StencilFront = _StencilFront
                        let _Compare = v.StencilBack.Compare.GetValue()
                        let _FailOp = v.StencilBack.FailOp.GetValue()
                        let _DepthFailOp = v.StencilBack.DepthFailOp.GetValue()
                        let _PassOp = v.StencilBack.PassOp.GetValue()
                        let _StencilBack = new DawnRaw.WGPUStencilStateFaceDescriptor()
                        _StencilBack.Compare <- _Compare
                        _StencilBack.FailOp <- _FailOp
                        _StencilBack.DepthFailOp <- _DepthFailOp
                        _StencilBack.PassOp <- _PassOp
                        let _StencilBack = _StencilBack
                        let _StencilReadMask = int (v.StencilReadMask)
                        let _StencilWriteMask = int (v.StencilWriteMask)
                        let _n = new DawnRaw.WGPUDepthStencilStateDescriptor()
                        _n.Format <- _Format
                        _n.DepthWriteEnabled <- _DepthWriteEnabled
                        _n.DepthCompare <- _DepthCompare
                        _n.StencilFront <- _StencilFront
                        _n.StencilBack <- _StencilBack
                        _n.StencilReadMask <- _StencilReadMask
                        _n.StencilWriteMask <- _StencilWriteMask
                        let _n = _n
                        _DepthStencilStateCont _n
                    | None -> _DepthStencilStateCont null
                match Descriptor.RasterizationState with
                | Some v ->
                    let _FrontFace = v.FrontFace.GetValue()
                    let _CullMode = v.CullMode.GetValue()
                    let _DepthBias = int (v.DepthBias)
                    let _DepthBiasSlopeScale = (v.DepthBiasSlopeScale)
                    let _DepthBiasClamp = (v.DepthBiasClamp)
                    let _n = new DawnRaw.WGPURasterizationStateDescriptor()
                    _n.FrontFace <- _FrontFace
                    _n.CullMode <- _CullMode
                    _n.DepthBias <- _DepthBias
                    _n.DepthBiasSlopeScale <- _DepthBiasSlopeScale
                    _n.DepthBiasClamp <- _DepthBiasClamp
                    let _n = _n
                    _RasterizationStateCont _n
                | None -> _RasterizationStateCont null
            match Descriptor.VertexState with
            | Some v ->
                let _IndexFormat = v.IndexFormat.GetValue()
                let _VertexBuffersCount = if isNull v.VertexBuffers then 0 else v.VertexBuffers.Length
                let rec _VertexBuffersCont (_VertexBuffersinputs : array<VertexBufferLayoutDescriptor>) (_VertexBuffersoutputs : JsArray) (_VertexBuffersi : int) =
                    if _VertexBuffersi >= _VertexBuffersCount then
                        let _VertexBuffers = _VertexBuffersoutputs.Reference
                        let _n = new DawnRaw.WGPUVertexStateDescriptor()
                        _n.IndexFormat <- _IndexFormat
                        _n.VertexBuffers <- _VertexBuffers
                        let _n = _n
                        _VertexStateCont _n
                    else
                        let _ArrayStride = int (_VertexBuffersinputs.[_VertexBuffersi].ArrayStride)
                        let _StepMode = _VertexBuffersinputs.[_VertexBuffersi].StepMode.GetValue()
                        let _AttributesCount = if isNull _VertexBuffersinputs.[_VertexBuffersi].Attributes then 0 else _VertexBuffersinputs.[_VertexBuffersi].Attributes.Length
                        let rec _AttributesCont (_Attributesinputs : array<VertexAttributeDescriptor>) (_Attributesoutputs : JsArray) (_Attributesi : int) =
                            if _Attributesi >= _AttributesCount then
                                let _Attributes = _Attributesoutputs.Reference
                                let _n = new DawnRaw.WGPUVertexBufferLayoutDescriptor()
                                _n.ArrayStride <- _ArrayStride
                                _n.StepMode <- _StepMode
                                _n.Attributes <- _Attributes
                                let _n = _n
                                _VertexBuffersoutputs.[_VertexBuffersi] <- _n
                                _VertexBuffersCont _VertexBuffersinputs _VertexBuffersoutputs (_VertexBuffersi + 1)
                            else
                                let _Format = _Attributesinputs.[_Attributesi].Format.GetValue()
                                let _Offset = int (_Attributesinputs.[_Attributesi].Offset)
                                let _ShaderLocation = int (_Attributesinputs.[_Attributesi].ShaderLocation)
                                let _n = new DawnRaw.WGPUVertexAttributeDescriptor()
                                _n.Format <- _Format
                                _n.Offset <- _Offset
                                _n.ShaderLocation <- _ShaderLocation
                                let _n = _n
                                _Attributesoutputs.[_Attributesi] <- _n
                                _AttributesCont _Attributesinputs _Attributesoutputs (_Attributesi + 1)
                        _AttributesCont _VertexBuffersinputs.[_VertexBuffersi].Attributes (if _AttributesCount > 0 then newArray _AttributesCount else null) 0
                _VertexBuffersCont v.VertexBuffers (if _VertexBuffersCount > 0 then newArray _VertexBuffersCount else null) 0
            | None -> _VertexStateCont null
        match Descriptor.FragmentStage with
        | Some v ->
            let _Module = (if isNull v.Module then null else v.Module.Handle)
            let _EntryPoint = v.EntryPoint
            let _n = new DawnRaw.WGPUProgrammableStageDescriptor()
            _n.Module <- _Module
            _n.EntryPoint <- _EntryPoint
            let _n = _n
            _FragmentStageCont _n
        | None -> _FragmentStageCont null
    member x.CreateReadyRenderPipeline(Descriptor : RenderPipelineDescriptor, Callback : CreateReadyRenderPipelineCallback, Userdata : nativeint) : unit = 
        let _Label = Descriptor.Label
        let _Layout = (if isNull Descriptor.Layout then null else Descriptor.Layout.Handle)
        let _Module = (if isNull Descriptor.VertexStage.Module then null else Descriptor.VertexStage.Module.Handle)
        let _EntryPoint = Descriptor.VertexStage.EntryPoint
        let _VertexStage = new DawnRaw.WGPUProgrammableStageDescriptor()
        _VertexStage.Module <- _Module
        _VertexStage.EntryPoint <- _EntryPoint
        let _VertexStage = _VertexStage
        let inline _FragmentStageCont _FragmentStage = 
            let inline _VertexStateCont _VertexState = 
                let _PrimitiveTopology = Descriptor.PrimitiveTopology.GetValue()
                let inline _RasterizationStateCont _RasterizationState = 
                    let _SampleCount = int (Descriptor.SampleCount)
                    let inline _DepthStencilStateCont _DepthStencilState = 
                        let _ColorStatesCount = if isNull Descriptor.ColorStates then 0 else Descriptor.ColorStates.Length
                        let rec _ColorStatesCont (_ColorStatesinputs : array<ColorStateDescriptor>) (_ColorStatesoutputs : JsArray) (_ColorStatesi : int) =
                            if _ColorStatesi >= _ColorStatesCount then
                                let _ColorStates = _ColorStatesoutputs.Reference
                                let _SampleMask = int (Descriptor.SampleMask)
                                let _AlphaToCoverageEnabled = Descriptor.AlphaToCoverageEnabled
                                let _Descriptor = new DawnRaw.WGPURenderPipelineDescriptor()
                                _Descriptor.Label <- _Label
                                _Descriptor.Layout <- _Layout
                                _Descriptor.VertexStage <- _VertexStage
                                _Descriptor.FragmentStage <- _FragmentStage
                                _Descriptor.VertexState <- _VertexState
                                _Descriptor.PrimitiveTopology <- _PrimitiveTopology
                                _Descriptor.RasterizationState <- _RasterizationState
                                _Descriptor.SampleCount <- _SampleCount
                                _Descriptor.DepthStencilState <- _DepthStencilState
                                _Descriptor.ColorStates <- _ColorStates
                                _Descriptor.SampleMask <- _SampleMask
                                _Descriptor.AlphaToCoverageEnabled <- _AlphaToCoverageEnabled
                                let _Descriptor = _Descriptor
                                let mutable _CallbackGC = Unchecked.defaultof<System.Runtime.InteropServices.GCHandle>
                                let _CallbackFunction (Status : obj) (Pipeline : RenderPipelineHandle) (Message : string) (Userdata : int) = 
                                    let _Status = Status
                                    let _Pipeline = Pipeline
                                    let _Message = Message
                                    let _Userdata = Userdata
                                    if _CallbackGC.IsAllocated then _CallbackGC.Free()
                                    Callback.Invoke(CreateReadyPipelineStatus.Parse(_Status), new RenderPipeline(x, _Pipeline), _Message, nativeint _Userdata)
                                let _CallbackDel = WGPUCreateReadyRenderPipelineCallback(_CallbackFunction)
                                _CallbackGC <- System.Runtime.InteropServices.GCHandle.Alloc(_CallbackDel)
                                let _Callback = _CallbackDel
                                let _Userdata = int (Userdata)
                                let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
                                let console = window.GetObjectProperty("console") |> unbox<JSObject>
                                console.Invoke("debug", "createReadyRenderPipeline", js _Descriptor, js _Callback, js _Userdata) |> ignore
                                try
                                    x.Handle.Reference.Invoke("createReadyRenderPipeline", js _Descriptor, js _Callback, js _Userdata) |> ignore
                                with e ->
                                    console.Invoke("error", string e) |> ignore
                                    Unchecked.defaultof<_>
                            else
                                let _Format = _ColorStatesinputs.[_ColorStatesi].Format.GetValue()
                                let _Operation = _ColorStatesinputs.[_ColorStatesi].AlphaBlend.Operation.GetValue()
                                let _SrcFactor = _ColorStatesinputs.[_ColorStatesi].AlphaBlend.SrcFactor.GetValue()
                                let _DstFactor = _ColorStatesinputs.[_ColorStatesi].AlphaBlend.DstFactor.GetValue()
                                let _AlphaBlend = new DawnRaw.WGPUBlendDescriptor()
                                _AlphaBlend.Operation <- _Operation
                                _AlphaBlend.SrcFactor <- _SrcFactor
                                _AlphaBlend.DstFactor <- _DstFactor
                                let _AlphaBlend = _AlphaBlend
                                let _Operation = _ColorStatesinputs.[_ColorStatesi].ColorBlend.Operation.GetValue()
                                let _SrcFactor = _ColorStatesinputs.[_ColorStatesi].ColorBlend.SrcFactor.GetValue()
                                let _DstFactor = _ColorStatesinputs.[_ColorStatesi].ColorBlend.DstFactor.GetValue()
                                let _ColorBlend = new DawnRaw.WGPUBlendDescriptor()
                                _ColorBlend.Operation <- _Operation
                                _ColorBlend.SrcFactor <- _SrcFactor
                                _ColorBlend.DstFactor <- _DstFactor
                                let _ColorBlend = _ColorBlend
                                let _WriteMask = int (_ColorStatesinputs.[_ColorStatesi].WriteMask)
                                let _n = new DawnRaw.WGPUColorStateDescriptor()
                                _n.Format <- _Format
                                _n.AlphaBlend <- _AlphaBlend
                                _n.ColorBlend <- _ColorBlend
                                _n.WriteMask <- _WriteMask
                                let _n = _n
                                _ColorStatesoutputs.[_ColorStatesi] <- _n
                                _ColorStatesCont _ColorStatesinputs _ColorStatesoutputs (_ColorStatesi + 1)
                        _ColorStatesCont Descriptor.ColorStates (if _ColorStatesCount > 0 then newArray _ColorStatesCount else null) 0
                    match Descriptor.DepthStencilState with
                    | Some v ->
                        let _Format = v.Format.GetValue()
                        let _DepthWriteEnabled = v.DepthWriteEnabled
                        let _DepthCompare = v.DepthCompare.GetValue()
                        let _Compare = v.StencilFront.Compare.GetValue()
                        let _FailOp = v.StencilFront.FailOp.GetValue()
                        let _DepthFailOp = v.StencilFront.DepthFailOp.GetValue()
                        let _PassOp = v.StencilFront.PassOp.GetValue()
                        let _StencilFront = new DawnRaw.WGPUStencilStateFaceDescriptor()
                        _StencilFront.Compare <- _Compare
                        _StencilFront.FailOp <- _FailOp
                        _StencilFront.DepthFailOp <- _DepthFailOp
                        _StencilFront.PassOp <- _PassOp
                        let _StencilFront = _StencilFront
                        let _Compare = v.StencilBack.Compare.GetValue()
                        let _FailOp = v.StencilBack.FailOp.GetValue()
                        let _DepthFailOp = v.StencilBack.DepthFailOp.GetValue()
                        let _PassOp = v.StencilBack.PassOp.GetValue()
                        let _StencilBack = new DawnRaw.WGPUStencilStateFaceDescriptor()
                        _StencilBack.Compare <- _Compare
                        _StencilBack.FailOp <- _FailOp
                        _StencilBack.DepthFailOp <- _DepthFailOp
                        _StencilBack.PassOp <- _PassOp
                        let _StencilBack = _StencilBack
                        let _StencilReadMask = int (v.StencilReadMask)
                        let _StencilWriteMask = int (v.StencilWriteMask)
                        let _n = new DawnRaw.WGPUDepthStencilStateDescriptor()
                        _n.Format <- _Format
                        _n.DepthWriteEnabled <- _DepthWriteEnabled
                        _n.DepthCompare <- _DepthCompare
                        _n.StencilFront <- _StencilFront
                        _n.StencilBack <- _StencilBack
                        _n.StencilReadMask <- _StencilReadMask
                        _n.StencilWriteMask <- _StencilWriteMask
                        let _n = _n
                        _DepthStencilStateCont _n
                    | None -> _DepthStencilStateCont null
                match Descriptor.RasterizationState with
                | Some v ->
                    let _FrontFace = v.FrontFace.GetValue()
                    let _CullMode = v.CullMode.GetValue()
                    let _DepthBias = int (v.DepthBias)
                    let _DepthBiasSlopeScale = (v.DepthBiasSlopeScale)
                    let _DepthBiasClamp = (v.DepthBiasClamp)
                    let _n = new DawnRaw.WGPURasterizationStateDescriptor()
                    _n.FrontFace <- _FrontFace
                    _n.CullMode <- _CullMode
                    _n.DepthBias <- _DepthBias
                    _n.DepthBiasSlopeScale <- _DepthBiasSlopeScale
                    _n.DepthBiasClamp <- _DepthBiasClamp
                    let _n = _n
                    _RasterizationStateCont _n
                | None -> _RasterizationStateCont null
            match Descriptor.VertexState with
            | Some v ->
                let _IndexFormat = v.IndexFormat.GetValue()
                let _VertexBuffersCount = if isNull v.VertexBuffers then 0 else v.VertexBuffers.Length
                let rec _VertexBuffersCont (_VertexBuffersinputs : array<VertexBufferLayoutDescriptor>) (_VertexBuffersoutputs : JsArray) (_VertexBuffersi : int) =
                    if _VertexBuffersi >= _VertexBuffersCount then
                        let _VertexBuffers = _VertexBuffersoutputs.Reference
                        let _n = new DawnRaw.WGPUVertexStateDescriptor()
                        _n.IndexFormat <- _IndexFormat
                        _n.VertexBuffers <- _VertexBuffers
                        let _n = _n
                        _VertexStateCont _n
                    else
                        let _ArrayStride = int (_VertexBuffersinputs.[_VertexBuffersi].ArrayStride)
                        let _StepMode = _VertexBuffersinputs.[_VertexBuffersi].StepMode.GetValue()
                        let _AttributesCount = if isNull _VertexBuffersinputs.[_VertexBuffersi].Attributes then 0 else _VertexBuffersinputs.[_VertexBuffersi].Attributes.Length
                        let rec _AttributesCont (_Attributesinputs : array<VertexAttributeDescriptor>) (_Attributesoutputs : JsArray) (_Attributesi : int) =
                            if _Attributesi >= _AttributesCount then
                                let _Attributes = _Attributesoutputs.Reference
                                let _n = new DawnRaw.WGPUVertexBufferLayoutDescriptor()
                                _n.ArrayStride <- _ArrayStride
                                _n.StepMode <- _StepMode
                                _n.Attributes <- _Attributes
                                let _n = _n
                                _VertexBuffersoutputs.[_VertexBuffersi] <- _n
                                _VertexBuffersCont _VertexBuffersinputs _VertexBuffersoutputs (_VertexBuffersi + 1)
                            else
                                let _Format = _Attributesinputs.[_Attributesi].Format.GetValue()
                                let _Offset = int (_Attributesinputs.[_Attributesi].Offset)
                                let _ShaderLocation = int (_Attributesinputs.[_Attributesi].ShaderLocation)
                                let _n = new DawnRaw.WGPUVertexAttributeDescriptor()
                                _n.Format <- _Format
                                _n.Offset <- _Offset
                                _n.ShaderLocation <- _ShaderLocation
                                let _n = _n
                                _Attributesoutputs.[_Attributesi] <- _n
                                _AttributesCont _Attributesinputs _Attributesoutputs (_Attributesi + 1)
                        _AttributesCont _VertexBuffersinputs.[_VertexBuffersi].Attributes (if _AttributesCount > 0 then newArray _AttributesCount else null) 0
                _VertexBuffersCont v.VertexBuffers (if _VertexBuffersCount > 0 then newArray _VertexBuffersCount else null) 0
            | None -> _VertexStateCont null
        match Descriptor.FragmentStage with
        | Some v ->
            let _Module = (if isNull v.Module then null else v.Module.Handle)
            let _EntryPoint = v.EntryPoint
            let _n = new DawnRaw.WGPUProgrammableStageDescriptor()
            _n.Module <- _Module
            _n.EntryPoint <- _EntryPoint
            let _n = _n
            _FragmentStageCont _n
        | None -> _FragmentStageCont null
    member x.CreateRenderBundleEncoder(Descriptor : RenderBundleEncoderDescriptor) : RenderBundleEncoder = 
        let _Label = Descriptor.Label
        let _ColorFormatsCount = Descriptor.ColorFormats.Length
        let _ColorFormatsArray = newArray (_ColorFormatsCount)
        for i in 0 .. _ColorFormatsCount-1 do
            _ColorFormatsArray.[i] <- Descriptor.ColorFormats.[i].GetValue()
        let _ColorFormats = _ColorFormatsArray.Reference
        let _DepthStencilFormat = Descriptor.DepthStencilFormat.GetValue()
        let _SampleCount = int (Descriptor.SampleCount)
        let _Descriptor = new DawnRaw.WGPURenderBundleEncoderDescriptor()
        _Descriptor.Label <- _Label
        _Descriptor.ColorFormats <- _ColorFormats
        _Descriptor.DepthStencilFormat <- _DepthStencilFormat
        _Descriptor.SampleCount <- _SampleCount
        let _Descriptor = _Descriptor
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "createRenderBundleEncoder", js _Descriptor) |> ignore
        try
            new RenderBundleEncoder(x, convert(x.Handle.Reference.Invoke("createRenderBundleEncoder", js _Descriptor)))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.CreateRenderPipeline(Descriptor : RenderPipelineDescriptor) : RenderPipeline = 
        let _Label = Descriptor.Label
        let _Layout = (if isNull Descriptor.Layout then null else Descriptor.Layout.Handle)
        let _Module = (if isNull Descriptor.VertexStage.Module then null else Descriptor.VertexStage.Module.Handle)
        let _EntryPoint = Descriptor.VertexStage.EntryPoint
        let _VertexStage = new DawnRaw.WGPUProgrammableStageDescriptor()
        _VertexStage.Module <- _Module
        _VertexStage.EntryPoint <- _EntryPoint
        let _VertexStage = _VertexStage
        let inline _FragmentStageCont _FragmentStage = 
            let inline _VertexStateCont _VertexState = 
                let _PrimitiveTopology = Descriptor.PrimitiveTopology.GetValue()
                let inline _RasterizationStateCont _RasterizationState = 
                    let _SampleCount = int (Descriptor.SampleCount)
                    let inline _DepthStencilStateCont _DepthStencilState = 
                        let _ColorStatesCount = if isNull Descriptor.ColorStates then 0 else Descriptor.ColorStates.Length
                        let rec _ColorStatesCont (_ColorStatesinputs : array<ColorStateDescriptor>) (_ColorStatesoutputs : JsArray) (_ColorStatesi : int) =
                            if _ColorStatesi >= _ColorStatesCount then
                                let _ColorStates = _ColorStatesoutputs.Reference
                                let _SampleMask = int (Descriptor.SampleMask)
                                let _AlphaToCoverageEnabled = Descriptor.AlphaToCoverageEnabled
                                let _Descriptor = new DawnRaw.WGPURenderPipelineDescriptor()
                                _Descriptor.Label <- _Label
                                _Descriptor.Layout <- _Layout
                                _Descriptor.VertexStage <- _VertexStage
                                _Descriptor.FragmentStage <- _FragmentStage
                                _Descriptor.VertexState <- _VertexState
                                _Descriptor.PrimitiveTopology <- _PrimitiveTopology
                                _Descriptor.RasterizationState <- _RasterizationState
                                _Descriptor.SampleCount <- _SampleCount
                                _Descriptor.DepthStencilState <- _DepthStencilState
                                _Descriptor.ColorStates <- _ColorStates
                                _Descriptor.SampleMask <- _SampleMask
                                _Descriptor.AlphaToCoverageEnabled <- _AlphaToCoverageEnabled
                                let _Descriptor = _Descriptor
                                let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
                                let console = window.GetObjectProperty("console") |> unbox<JSObject>
                                console.Invoke("debug", "createRenderPipeline", js _Descriptor) |> ignore
                                try
                                    new RenderPipeline(x, convert(x.Handle.Reference.Invoke("createRenderPipeline", js _Descriptor)))
                                with e ->
                                    console.Invoke("error", string e) |> ignore
                                    Unchecked.defaultof<_>
                            else
                                let _Format = _ColorStatesinputs.[_ColorStatesi].Format.GetValue()
                                let _Operation = _ColorStatesinputs.[_ColorStatesi].AlphaBlend.Operation.GetValue()
                                let _SrcFactor = _ColorStatesinputs.[_ColorStatesi].AlphaBlend.SrcFactor.GetValue()
                                let _DstFactor = _ColorStatesinputs.[_ColorStatesi].AlphaBlend.DstFactor.GetValue()
                                let _AlphaBlend = new DawnRaw.WGPUBlendDescriptor()
                                _AlphaBlend.Operation <- _Operation
                                _AlphaBlend.SrcFactor <- _SrcFactor
                                _AlphaBlend.DstFactor <- _DstFactor
                                let _AlphaBlend = _AlphaBlend
                                let _Operation = _ColorStatesinputs.[_ColorStatesi].ColorBlend.Operation.GetValue()
                                let _SrcFactor = _ColorStatesinputs.[_ColorStatesi].ColorBlend.SrcFactor.GetValue()
                                let _DstFactor = _ColorStatesinputs.[_ColorStatesi].ColorBlend.DstFactor.GetValue()
                                let _ColorBlend = new DawnRaw.WGPUBlendDescriptor()
                                _ColorBlend.Operation <- _Operation
                                _ColorBlend.SrcFactor <- _SrcFactor
                                _ColorBlend.DstFactor <- _DstFactor
                                let _ColorBlend = _ColorBlend
                                let _WriteMask = int (_ColorStatesinputs.[_ColorStatesi].WriteMask)
                                let _n = new DawnRaw.WGPUColorStateDescriptor()
                                _n.Format <- _Format
                                _n.AlphaBlend <- _AlphaBlend
                                _n.ColorBlend <- _ColorBlend
                                _n.WriteMask <- _WriteMask
                                let _n = _n
                                _ColorStatesoutputs.[_ColorStatesi] <- _n
                                _ColorStatesCont _ColorStatesinputs _ColorStatesoutputs (_ColorStatesi + 1)
                        _ColorStatesCont Descriptor.ColorStates (if _ColorStatesCount > 0 then newArray _ColorStatesCount else null) 0
                    match Descriptor.DepthStencilState with
                    | Some v ->
                        let _Format = v.Format.GetValue()
                        let _DepthWriteEnabled = v.DepthWriteEnabled
                        let _DepthCompare = v.DepthCompare.GetValue()
                        let _Compare = v.StencilFront.Compare.GetValue()
                        let _FailOp = v.StencilFront.FailOp.GetValue()
                        let _DepthFailOp = v.StencilFront.DepthFailOp.GetValue()
                        let _PassOp = v.StencilFront.PassOp.GetValue()
                        let _StencilFront = new DawnRaw.WGPUStencilStateFaceDescriptor()
                        _StencilFront.Compare <- _Compare
                        _StencilFront.FailOp <- _FailOp
                        _StencilFront.DepthFailOp <- _DepthFailOp
                        _StencilFront.PassOp <- _PassOp
                        let _StencilFront = _StencilFront
                        let _Compare = v.StencilBack.Compare.GetValue()
                        let _FailOp = v.StencilBack.FailOp.GetValue()
                        let _DepthFailOp = v.StencilBack.DepthFailOp.GetValue()
                        let _PassOp = v.StencilBack.PassOp.GetValue()
                        let _StencilBack = new DawnRaw.WGPUStencilStateFaceDescriptor()
                        _StencilBack.Compare <- _Compare
                        _StencilBack.FailOp <- _FailOp
                        _StencilBack.DepthFailOp <- _DepthFailOp
                        _StencilBack.PassOp <- _PassOp
                        let _StencilBack = _StencilBack
                        let _StencilReadMask = int (v.StencilReadMask)
                        let _StencilWriteMask = int (v.StencilWriteMask)
                        let _n = new DawnRaw.WGPUDepthStencilStateDescriptor()
                        _n.Format <- _Format
                        _n.DepthWriteEnabled <- _DepthWriteEnabled
                        _n.DepthCompare <- _DepthCompare
                        _n.StencilFront <- _StencilFront
                        _n.StencilBack <- _StencilBack
                        _n.StencilReadMask <- _StencilReadMask
                        _n.StencilWriteMask <- _StencilWriteMask
                        let _n = _n
                        _DepthStencilStateCont _n
                    | None -> _DepthStencilStateCont null
                match Descriptor.RasterizationState with
                | Some v ->
                    let _FrontFace = v.FrontFace.GetValue()
                    let _CullMode = v.CullMode.GetValue()
                    let _DepthBias = int (v.DepthBias)
                    let _DepthBiasSlopeScale = (v.DepthBiasSlopeScale)
                    let _DepthBiasClamp = (v.DepthBiasClamp)
                    let _n = new DawnRaw.WGPURasterizationStateDescriptor()
                    _n.FrontFace <- _FrontFace
                    _n.CullMode <- _CullMode
                    _n.DepthBias <- _DepthBias
                    _n.DepthBiasSlopeScale <- _DepthBiasSlopeScale
                    _n.DepthBiasClamp <- _DepthBiasClamp
                    let _n = _n
                    _RasterizationStateCont _n
                | None -> _RasterizationStateCont null
            match Descriptor.VertexState with
            | Some v ->
                let _IndexFormat = v.IndexFormat.GetValue()
                let _VertexBuffersCount = if isNull v.VertexBuffers then 0 else v.VertexBuffers.Length
                let rec _VertexBuffersCont (_VertexBuffersinputs : array<VertexBufferLayoutDescriptor>) (_VertexBuffersoutputs : JsArray) (_VertexBuffersi : int) =
                    if _VertexBuffersi >= _VertexBuffersCount then
                        let _VertexBuffers = _VertexBuffersoutputs.Reference
                        let _n = new DawnRaw.WGPUVertexStateDescriptor()
                        _n.IndexFormat <- _IndexFormat
                        _n.VertexBuffers <- _VertexBuffers
                        let _n = _n
                        _VertexStateCont _n
                    else
                        let _ArrayStride = int (_VertexBuffersinputs.[_VertexBuffersi].ArrayStride)
                        let _StepMode = _VertexBuffersinputs.[_VertexBuffersi].StepMode.GetValue()
                        let _AttributesCount = if isNull _VertexBuffersinputs.[_VertexBuffersi].Attributes then 0 else _VertexBuffersinputs.[_VertexBuffersi].Attributes.Length
                        let rec _AttributesCont (_Attributesinputs : array<VertexAttributeDescriptor>) (_Attributesoutputs : JsArray) (_Attributesi : int) =
                            if _Attributesi >= _AttributesCount then
                                let _Attributes = _Attributesoutputs.Reference
                                let _n = new DawnRaw.WGPUVertexBufferLayoutDescriptor()
                                _n.ArrayStride <- _ArrayStride
                                _n.StepMode <- _StepMode
                                _n.Attributes <- _Attributes
                                let _n = _n
                                _VertexBuffersoutputs.[_VertexBuffersi] <- _n
                                _VertexBuffersCont _VertexBuffersinputs _VertexBuffersoutputs (_VertexBuffersi + 1)
                            else
                                let _Format = _Attributesinputs.[_Attributesi].Format.GetValue()
                                let _Offset = int (_Attributesinputs.[_Attributesi].Offset)
                                let _ShaderLocation = int (_Attributesinputs.[_Attributesi].ShaderLocation)
                                let _n = new DawnRaw.WGPUVertexAttributeDescriptor()
                                _n.Format <- _Format
                                _n.Offset <- _Offset
                                _n.ShaderLocation <- _ShaderLocation
                                let _n = _n
                                _Attributesoutputs.[_Attributesi] <- _n
                                _AttributesCont _Attributesinputs _Attributesoutputs (_Attributesi + 1)
                        _AttributesCont _VertexBuffersinputs.[_VertexBuffersi].Attributes (if _AttributesCount > 0 then newArray _AttributesCount else null) 0
                _VertexBuffersCont v.VertexBuffers (if _VertexBuffersCount > 0 then newArray _VertexBuffersCount else null) 0
            | None -> _VertexStateCont null
        match Descriptor.FragmentStage with
        | Some v ->
            let _Module = (if isNull v.Module then null else v.Module.Handle)
            let _EntryPoint = v.EntryPoint
            let _n = new DawnRaw.WGPUProgrammableStageDescriptor()
            _n.Module <- _Module
            _n.EntryPoint <- _EntryPoint
            let _n = _n
            _FragmentStageCont _n
        | None -> _FragmentStageCont null
    member x.CreateSampler() : Sampler = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "createSampler") |> ignore
        try
            new Sampler(x, convert(x.Handle.Reference.Invoke("createSampler")))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.CreateSampler(Descriptor : SamplerDescriptor) : Sampler = 
        let _Label = Descriptor.Label
        let _AddressModeU = Descriptor.AddressModeU.GetValue()
        let _AddressModeV = Descriptor.AddressModeV.GetValue()
        let _AddressModeW = Descriptor.AddressModeW.GetValue()
        let _MagFilter = Descriptor.MagFilter.GetValue()
        let _MinFilter = Descriptor.MinFilter.GetValue()
        let _MipmapFilter = Descriptor.MipmapFilter.GetValue()
        let _LodMinClamp = (Descriptor.LodMinClamp)
        let _LodMaxClamp = (Descriptor.LodMaxClamp)
        let _Compare = Descriptor.Compare.GetValue()
        let _Descriptor = new DawnRaw.WGPUSamplerDescriptor()
        _Descriptor.Label <- _Label
        _Descriptor.AddressModeU <- _AddressModeU
        _Descriptor.AddressModeV <- _AddressModeV
        _Descriptor.AddressModeW <- _AddressModeW
        _Descriptor.MagFilter <- _MagFilter
        _Descriptor.MinFilter <- _MinFilter
        _Descriptor.MipmapFilter <- _MipmapFilter
        _Descriptor.LodMinClamp <- _LodMinClamp
        _Descriptor.LodMaxClamp <- _LodMaxClamp
        _Descriptor.Compare <- _Compare
        let _Descriptor = _Descriptor
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "createSampler", js _Descriptor) |> ignore
        try
            new Sampler(x, convert(x.Handle.Reference.Invoke("createSampler", js _Descriptor)))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.CreateShaderModule() : ShaderModule = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "createShaderModule") |> ignore
        try
            new ShaderModule(x, convert(x.Handle.Reference.Invoke("createShaderModule")))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.CreateShaderModule(Descriptor : ShaderModuleDescriptor) : ShaderModule = 
        let _Label = Descriptor.Label
        let _Descriptor = new DawnRaw.WGPUShaderModuleDescriptor()
        _Descriptor.Label <- _Label
        let _Descriptor = _Descriptor
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "createShaderModule", js _Descriptor) |> ignore
        try
            new ShaderModule(x, convert(x.Handle.Reference.Invoke("createShaderModule", js _Descriptor)))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.CreateSwapChain(Surface : Surface, Descriptor : SwapChainDescriptor) : SwapChain = 
        let _Surface = (if isNull Surface then null else Surface.Handle)
        let _Label = Descriptor.Label
        let _Usage = int (Descriptor.Usage)
        let _Format = Descriptor.Format.GetValue()
        let _Width = int (Descriptor.Width)
        let _Height = int (Descriptor.Height)
        let _PresentMode = Descriptor.PresentMode.GetValue()
        let _Implementation = int (Descriptor.Implementation)
        let _Descriptor = new DawnRaw.WGPUSwapChainDescriptor()
        _Descriptor.Label <- _Label
        _Descriptor.Usage <- _Usage
        _Descriptor.Format <- _Format
        _Descriptor.Width <- _Width
        _Descriptor.Height <- _Height
        _Descriptor.PresentMode <- _PresentMode
        _Descriptor.Implementation <- _Implementation
        let _Descriptor = _Descriptor
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "createSwapChain", js _Surface, js _Descriptor) |> ignore
        try
            new SwapChain(x, convert(x.Handle.Reference.Invoke("createSwapChain", js _Surface, js _Descriptor)))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.CreateTexture(Descriptor : TextureDescriptor) : Texture = 
        let _Label = Descriptor.Label
        let _Usage = int (Descriptor.Usage)
        let _Dimension = Descriptor.Dimension.GetValue()
        let _Width = int (Descriptor.Size.Width)
        let _Height = int (Descriptor.Size.Height)
        let _Depth = int (Descriptor.Size.Depth)
        let _Size = new DawnRaw.WGPUExtent3D()
        _Size.Width <- _Width
        _Size.Height <- _Height
        _Size.Depth <- _Depth
        let _Size = _Size
        let _Format = Descriptor.Format.GetValue()
        let _MipLevelCount = int (Descriptor.MipLevelCount)
        let _SampleCount = int (Descriptor.SampleCount)
        let _Descriptor = new DawnRaw.WGPUTextureDescriptor()
        _Descriptor.Label <- _Label
        _Descriptor.Usage <- _Usage
        _Descriptor.Dimension <- _Dimension
        _Descriptor.Size <- _Size
        _Descriptor.Format <- _Format
        _Descriptor.MipLevelCount <- _MipLevelCount
        _Descriptor.SampleCount <- _SampleCount
        let _Descriptor = _Descriptor
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "createTexture", js _Descriptor) |> ignore
        try
            new Texture(x, convert(x.Handle.Reference.Invoke("createTexture", js _Descriptor)))
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.GetDefaultQueue() : Queue = 
        let handle = x.Handle.Reference.GetObjectProperty("defaultQueue") |> convert<QueueHandle>
        new Queue(x, handle)
    member x.InjectError(Type : ErrorType, Message : string) : unit = 
        let _Type = Type.GetValue()
        let _Message = Message
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "injectError", js _Type, js _Message) |> ignore
        try
            x.Handle.Reference.Invoke("injectError", js _Type, js _Message) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.LoseForTesting() : unit = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "loseForTesting") |> ignore
        try
            x.Handle.Reference.Invoke("loseForTesting") |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.Tick() : unit = 
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "tick") |> ignore
        try
            x.Handle.Reference.Invoke("tick") |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.SetUncapturedErrorCallback(Callback : ErrorCallback) : unit = 
        let _CallbackFunction (Type : obj) (Message : string) (Userdata : int) = 
            let _Type = Type
            let _Message = Message
            let _Userdata = Userdata
            Callback.Invoke(ErrorType.Parse(_Type), _Message, nativeint _Userdata)
        let _CallbackDel = WGPUErrorCallback(_CallbackFunction)
        let _CallbackGC = System.Runtime.InteropServices.GCHandle.Alloc(_CallbackDel)
        let _Callback = _CallbackDel
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "setUncapturedErrorCallback", js _Callback) |> ignore
        try
            x.Handle.Reference.Invoke("setUncapturedErrorCallback", js _Callback) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.SetUncapturedErrorCallback(Callback : ErrorCallback, Userdata : nativeint) : unit = 
        let _CallbackFunction (Type : obj) (Message : string) (Userdata : int) = 
            let _Type = Type
            let _Message = Message
            let _Userdata = Userdata
            Callback.Invoke(ErrorType.Parse(_Type), _Message, nativeint _Userdata)
        let _CallbackDel = WGPUErrorCallback(_CallbackFunction)
        let _CallbackGC = System.Runtime.InteropServices.GCHandle.Alloc(_CallbackDel)
        let _Callback = _CallbackDel
        let _Userdata = int (Userdata)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "setUncapturedErrorCallback", js _Callback, js _Userdata) |> ignore
        try
            x.Handle.Reference.Invoke("setUncapturedErrorCallback", js _Callback, js _Userdata) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.SetDeviceLostCallback(Callback : DeviceLostCallback) : unit = 
        let _CallbackFunction (Message : string) (Userdata : int) = 
            let _Message = Message
            let _Userdata = Userdata
            Callback.Invoke(_Message, nativeint _Userdata)
        let _CallbackDel = WGPUDeviceLostCallback(_CallbackFunction)
        let _CallbackGC = System.Runtime.InteropServices.GCHandle.Alloc(_CallbackDel)
        let _Callback = _CallbackDel
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "setDeviceLostCallback", js _Callback) |> ignore
        try
            x.Handle.Reference.Invoke("setDeviceLostCallback", js _Callback) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.SetDeviceLostCallback(Callback : DeviceLostCallback, Userdata : nativeint) : unit = 
        let _CallbackFunction (Message : string) (Userdata : int) = 
            let _Message = Message
            let _Userdata = Userdata
            Callback.Invoke(_Message, nativeint _Userdata)
        let _CallbackDel = WGPUDeviceLostCallback(_CallbackFunction)
        let _CallbackGC = System.Runtime.InteropServices.GCHandle.Alloc(_CallbackDel)
        let _Callback = _CallbackDel
        let _Userdata = int (Userdata)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "setDeviceLostCallback", js _Callback, js _Userdata) |> ignore
        try
            x.Handle.Reference.Invoke("setDeviceLostCallback", js _Callback, js _Userdata) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.PushErrorScope(Filter : ErrorFilter) : unit = 
        let _Filter = Filter.GetValue()
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "pushErrorScope", js _Filter) |> ignore
        try
            x.Handle.Reference.Invoke("pushErrorScope", js _Filter) |> ignore
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.PopErrorScope(Callback : ErrorCallback) : bool = 
        let mutable _CallbackGC = Unchecked.defaultof<System.Runtime.InteropServices.GCHandle>
        let _CallbackFunction (Type : obj) (Message : string) (Userdata : int) = 
            let _Type = Type
            let _Message = Message
            let _Userdata = Userdata
            if _CallbackGC.IsAllocated then _CallbackGC.Free()
            Callback.Invoke(ErrorType.Parse(_Type), _Message, nativeint _Userdata)
        let _CallbackDel = WGPUErrorCallback(_CallbackFunction)
        _CallbackGC <- System.Runtime.InteropServices.GCHandle.Alloc(_CallbackDel)
        let _Callback = _CallbackDel
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "popErrorScope", js _Callback) |> ignore
        try
            x.Handle.Reference.Invoke("popErrorScope", js _Callback) |> convert
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
    member x.PopErrorScope(Callback : ErrorCallback, Userdata : nativeint) : bool = 
        let mutable _CallbackGC = Unchecked.defaultof<System.Runtime.InteropServices.GCHandle>
        let _CallbackFunction (Type : obj) (Message : string) (Userdata : int) = 
            let _Type = Type
            let _Message = Message
            let _Userdata = Userdata
            if _CallbackGC.IsAllocated then _CallbackGC.Free()
            Callback.Invoke(ErrorType.Parse(_Type), _Message, nativeint _Userdata)
        let _CallbackDel = WGPUErrorCallback(_CallbackFunction)
        _CallbackGC <- System.Runtime.InteropServices.GCHandle.Alloc(_CallbackDel)
        let _Callback = _CallbackDel
        let _Userdata = int (Userdata)
        let window = Runtime.GetGlobalObject("window") |> unbox<JSObject>
        let console = window.GetObjectProperty("console") |> unbox<JSObject>
        console.Invoke("debug", "popErrorScope", js _Callback, js _Userdata) |> ignore
        try
            x.Handle.Reference.Invoke("popErrorScope", js _Callback, js _Userdata) |> convert
        with e ->
            console.Invoke("error", string e) |> ignore
            Unchecked.defaultof<_>
