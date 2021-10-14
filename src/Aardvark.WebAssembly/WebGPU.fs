namespace rec WebGPU
open System
open System.Threading
open WebAssembly
open WebAssembly.Core
open Aardvark.WebAssembly
#nowarn "9"
#nowarn "49"

[<AllowNullLiteral>]
type AdapterHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type BindGroupLayoutHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type TextureViewHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type CommandBufferHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type RenderBundleHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type SurfaceHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type SwapChainHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type BufferHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type ExternalTextureHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type PipelineLayoutHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type QuerySetHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type SamplerHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type TextureHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type InstanceHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type QueueHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type ShaderModuleHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type BindGroupHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type ComputePipelineHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type RenderPipelineHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type ComputePassEncoderHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type RenderBundleEncoderHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type RenderPassEncoderHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type CommandEncoderHandle(r : JSObject) = 
    inherit JsObj(r)
[<AllowNullLiteral>]
type DeviceHandle(r : JSObject) = 
    inherit JsObj(r)
type WGPUBufferMapCallback = delegate of obj * float -> unit
type WGPUDeviceLostCallback = delegate of obj * string * float -> unit
type WGPUErrorCallback = delegate of obj * string * float -> unit
type WGPULoggingCallback = delegate of obj * string * float -> unit
type WGPUQueueWorkDoneCallback = delegate of obj * float -> unit
type WGPURequestAdapterCallback = delegate of obj * AdapterHandle * string * float -> unit
type WGPUCompilationInfoCallback = delegate of obj * DawnRaw.WGPUCompilationInfo * float -> unit
type WGPUCreateComputePipelineAsyncCallback = delegate of obj * ComputePipelineHandle * string * float -> unit
type WGPUCreateRenderPipelineAsyncCallback = delegate of obj * RenderPipelineHandle * string * float -> unit
type WGPURequestDeviceCallback = delegate of obj * DeviceHandle * string * float -> unit
type BufferMapCallback = delegate of BufferMapAsyncStatus * nativeint -> unit
type DeviceLostCallback = delegate of DeviceLostReason * string * nativeint -> unit
type ErrorCallback = delegate of ErrorType * string * nativeint -> unit
type LoggingCallback = delegate of LoggingType * string * nativeint -> unit
type QueueWorkDoneCallback = delegate of QueueWorkDoneStatus * nativeint -> unit
type RequestAdapterCallback = delegate of RequestAdapterStatus * Adapter * string * nativeint -> unit
type CompilationInfoCallback = delegate of CompilationInfoRequestStatus * option<CompilationInfo> * nativeint -> unit
type CreateComputePipelineAsyncCallback = delegate of CreatePipelineAsyncStatus * ComputePipeline * string * nativeint -> unit
type CreateRenderPipelineAsyncCallback = delegate of CreatePipelineAsyncStatus * RenderPipeline * string * nativeint -> unit
type RequestDeviceCallback = delegate of RequestDeviceStatus * Device * string * nativeint -> unit


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
type AlphaOp = 
| DontChange
| Premultiply
| Unpremultiply
| Custom of string
    member internal x.GetValue() =
        match x with
        | AlphaOp.DontChange -> "dont-change" :> obj
        | AlphaOp.Premultiply -> "premultiply" :> obj
        | AlphaOp.Unpremultiply -> "unpremultiply" :> obj
        | AlphaOp.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "dont-change" -> AlphaOp.DontChange
            | "premultiply" -> AlphaOp.Premultiply
            | "unpremultiply" -> AlphaOp.Unpremultiply
            | str -> AlphaOp.Custom str
[<RequireQualifiedAccess>]
type BackendType = 
| Null
| WebGPU
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
        | BackendType.WebGPU -> "webgpu" :> obj
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
            | "webgpu" -> BackendType.WebGPU
            | "d3d11" -> BackendType.D3D11
            | "d3d12" -> BackendType.D3D12
            | "metal" -> BackendType.Metal
            | "vulkan" -> BackendType.Vulkan
            | "opengl" -> BackendType.OpenGL
            | "opengles" -> BackendType.OpenGLES
            | str -> BackendType.Custom str
[<RequireQualifiedAccess>]
type BlendFactor = 
| Zero
| One
| Src
| OneMinusSrc
| SrcAlpha
| OneMinusSrcAlpha
| Dst
| OneMinusDst
| DstAlpha
| OneMinusDstAlpha
| SrcAlphaSaturated
| Constant
| OneMinusConstant
| Custom of string
    member internal x.GetValue() =
        match x with
        | BlendFactor.Zero -> "zero" :> obj
        | BlendFactor.One -> "one" :> obj
        | BlendFactor.Src -> "src" :> obj
        | BlendFactor.OneMinusSrc -> "one-minus-src" :> obj
        | BlendFactor.SrcAlpha -> "src-alpha" :> obj
        | BlendFactor.OneMinusSrcAlpha -> "one-minus-src-alpha" :> obj
        | BlendFactor.Dst -> "dst" :> obj
        | BlendFactor.OneMinusDst -> "one-minus-dst" :> obj
        | BlendFactor.DstAlpha -> "dst-alpha" :> obj
        | BlendFactor.OneMinusDstAlpha -> "one-minus-dst-alpha" :> obj
        | BlendFactor.SrcAlphaSaturated -> "src-alpha-saturated" :> obj
        | BlendFactor.Constant -> "constant" :> obj
        | BlendFactor.OneMinusConstant -> "one-minus-constant" :> obj
        | BlendFactor.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "zero" -> BlendFactor.Zero
            | "one" -> BlendFactor.One
            | "src" -> BlendFactor.Src
            | "one-minus-src" -> BlendFactor.OneMinusSrc
            | "src-alpha" -> BlendFactor.SrcAlpha
            | "one-minus-src-alpha" -> BlendFactor.OneMinusSrcAlpha
            | "dst" -> BlendFactor.Dst
            | "one-minus-dst" -> BlendFactor.OneMinusDst
            | "dst-alpha" -> BlendFactor.DstAlpha
            | "one-minus-dst-alpha" -> BlendFactor.OneMinusDstAlpha
            | "src-alpha-saturated" -> BlendFactor.SrcAlphaSaturated
            | "constant" -> BlendFactor.Constant
            | "one-minus-constant" -> BlendFactor.OneMinusConstant
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
type BufferBindingType = 
| Uniform
| Storage
| ReadOnlyStorage
| Custom of string
| Undefined
    member internal x.GetValue() =
        match x with
        | BufferBindingType.Uniform -> "uniform" :> obj
        | BufferBindingType.Storage -> "storage" :> obj
        | BufferBindingType.ReadOnlyStorage -> "read-only-storage" :> obj
        | BufferBindingType.Custom n -> n :> obj
        | BufferBindingType.Undefined -> null
    static member Parse(obj : obj) =
        if isNull obj then BufferBindingType.Undefined
        else
            match (string obj).Trim().ToLower() with
            | "uniform" -> BufferBindingType.Uniform
            | "storage" -> BufferBindingType.Storage
            | "read-only-storage" -> BufferBindingType.ReadOnlyStorage
            | str -> BufferBindingType.Custom str
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
type CompilationInfoRequestStatus = 
| Success
| Error
| DeviceLost
| Unknown
| Custom of string
    member internal x.GetValue() =
        match x with
        | CompilationInfoRequestStatus.Success -> "success" :> obj
        | CompilationInfoRequestStatus.Error -> "error" :> obj
        | CompilationInfoRequestStatus.DeviceLost -> "device-lost" :> obj
        | CompilationInfoRequestStatus.Unknown -> "unknown" :> obj
        | CompilationInfoRequestStatus.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "success" -> CompilationInfoRequestStatus.Success
            | "error" -> CompilationInfoRequestStatus.Error
            | "device-lost" -> CompilationInfoRequestStatus.DeviceLost
            | "unknown" -> CompilationInfoRequestStatus.Unknown
            | str -> CompilationInfoRequestStatus.Custom str
[<RequireQualifiedAccess>]
type CompilationMessageType = 
| Error
| Warning
| Info
| Custom of string
    member internal x.GetValue() =
        match x with
        | CompilationMessageType.Error -> "error" :> obj
        | CompilationMessageType.Warning -> "warning" :> obj
        | CompilationMessageType.Info -> "info" :> obj
        | CompilationMessageType.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "error" -> CompilationMessageType.Error
            | "warning" -> CompilationMessageType.Warning
            | "info" -> CompilationMessageType.Info
            | str -> CompilationMessageType.Custom str
[<RequireQualifiedAccess>]
type CreatePipelineAsyncStatus = 
| Success
| Error
| DeviceLost
| DeviceDestroyed
| Unknown
| Custom of string
    member internal x.GetValue() =
        match x with
        | CreatePipelineAsyncStatus.Success -> "success" :> obj
        | CreatePipelineAsyncStatus.Error -> "error" :> obj
        | CreatePipelineAsyncStatus.DeviceLost -> "device-lost" :> obj
        | CreatePipelineAsyncStatus.DeviceDestroyed -> "device-destroyed" :> obj
        | CreatePipelineAsyncStatus.Unknown -> "unknown" :> obj
        | CreatePipelineAsyncStatus.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "success" -> CreatePipelineAsyncStatus.Success
            | "error" -> CreatePipelineAsyncStatus.Error
            | "device-lost" -> CreatePipelineAsyncStatus.DeviceLost
            | "device-destroyed" -> CreatePipelineAsyncStatus.DeviceDestroyed
            | "unknown" -> CreatePipelineAsyncStatus.Unknown
            | str -> CreatePipelineAsyncStatus.Custom str
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
type DeviceLostReason = 
| Destroyed
| Custom of string
| Undefined
    member internal x.GetValue() =
        match x with
        | DeviceLostReason.Destroyed -> "destroyed" :> obj
        | DeviceLostReason.Custom n -> n :> obj
        | DeviceLostReason.Undefined -> null
    static member Parse(obj : obj) =
        if isNull obj then DeviceLostReason.Undefined
        else
            match (string obj).Trim().ToLower() with
            | "destroyed" -> DeviceLostReason.Destroyed
            | str -> DeviceLostReason.Custom str
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
type FeatureName = 
| DepthClamping
| Depth24UnormStencil8
| Depth32FloatStencil8
| TimestampQuery
| PipelineStatisticsQuery
| TextureCompressionBC
| TextureCompressionETC2
| TextureCompressionASTC
| Custom of string
| Undefined
    member internal x.GetValue() =
        match x with
        | FeatureName.DepthClamping -> "depth-clamping" :> obj
        | FeatureName.Depth24UnormStencil8 -> "depth24unorm-stencil8" :> obj
        | FeatureName.Depth32FloatStencil8 -> "depth32float-stencil8" :> obj
        | FeatureName.TimestampQuery -> "timestamp-query" :> obj
        | FeatureName.PipelineStatisticsQuery -> "pipeline-statistics-query" :> obj
        | FeatureName.TextureCompressionBC -> "texture-compression-bc" :> obj
        | FeatureName.TextureCompressionETC2 -> "texture-compression-etc2" :> obj
        | FeatureName.TextureCompressionASTC -> "texture-compression-astc" :> obj
        | FeatureName.Custom n -> n :> obj
        | FeatureName.Undefined -> null
    static member Parse(obj : obj) =
        if isNull obj then FeatureName.Undefined
        else
            match (string obj).Trim().ToLower() with
            | "depth-clamping" -> FeatureName.DepthClamping
            | "depth24unorm-stencil8" -> FeatureName.Depth24UnormStencil8
            | "depth32float-stencil8" -> FeatureName.Depth32FloatStencil8
            | "timestamp-query" -> FeatureName.TimestampQuery
            | "pipeline-statistics-query" -> FeatureName.PipelineStatisticsQuery
            | "texture-compression-bc" -> FeatureName.TextureCompressionBC
            | "texture-compression-etc2" -> FeatureName.TextureCompressionETC2
            | "texture-compression-astc" -> FeatureName.TextureCompressionASTC
            | str -> FeatureName.Custom str
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
type LoadOp = 
| Clear
| Load
| Color of Color
    member internal x.GetValue() =
        match x with
        | LoadOp.Clear -> "clear" :> obj
        | LoadOp.Load -> "load" :> obj
        | LoadOp.Color c -> (createObj ["r", c.R :> obj; "g", c.G :> obj; "b", c.B :> obj; "a", c.A :> obj]).Reference :> obj
[<RequireQualifiedAccess>]
type LoggingType = 
| Verbose
| Info
| Warning
| Error
| Custom of string
    member internal x.GetValue() =
        match x with
        | LoggingType.Verbose -> "verbose" :> obj
        | LoggingType.Info -> "info" :> obj
        | LoggingType.Warning -> "warning" :> obj
        | LoggingType.Error -> "error" :> obj
        | LoggingType.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "verbose" -> LoggingType.Verbose
            | "info" -> LoggingType.Info
            | "warning" -> LoggingType.Warning
            | "error" -> LoggingType.Error
            | str -> LoggingType.Custom str
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
type PowerPreference = 
| LowPower
| HighPerformance
| Custom of string
    member internal x.GetValue() =
        match x with
        | PowerPreference.LowPower -> "low-power" :> obj
        | PowerPreference.HighPerformance -> "high-performance" :> obj
        | PowerPreference.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "low-power" -> PowerPreference.LowPower
            | "high-performance" -> PowerPreference.HighPerformance
            | str -> PowerPreference.Custom str
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
type QueueWorkDoneStatus = 
| Success
| Error
| Unknown
| DeviceLost
| Custom of string
    member internal x.GetValue() =
        match x with
        | QueueWorkDoneStatus.Success -> "success" :> obj
        | QueueWorkDoneStatus.Error -> "error" :> obj
        | QueueWorkDoneStatus.Unknown -> "unknown" :> obj
        | QueueWorkDoneStatus.DeviceLost -> "device-lost" :> obj
        | QueueWorkDoneStatus.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "success" -> QueueWorkDoneStatus.Success
            | "error" -> QueueWorkDoneStatus.Error
            | "unknown" -> QueueWorkDoneStatus.Unknown
            | "device-lost" -> QueueWorkDoneStatus.DeviceLost
            | str -> QueueWorkDoneStatus.Custom str
[<RequireQualifiedAccess>]
type RequestAdapterStatus = 
| Success
| Unavailable
| Error
| Unknown
| Custom of string
    member internal x.GetValue() =
        match x with
        | RequestAdapterStatus.Success -> "success" :> obj
        | RequestAdapterStatus.Unavailable -> "unavailable" :> obj
        | RequestAdapterStatus.Error -> "error" :> obj
        | RequestAdapterStatus.Unknown -> "unknown" :> obj
        | RequestAdapterStatus.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "success" -> RequestAdapterStatus.Success
            | "unavailable" -> RequestAdapterStatus.Unavailable
            | "error" -> RequestAdapterStatus.Error
            | "unknown" -> RequestAdapterStatus.Unknown
            | str -> RequestAdapterStatus.Custom str
[<RequireQualifiedAccess>]
type RequestDeviceStatus = 
| Success
| Error
| Unknown
| Custom of string
    member internal x.GetValue() =
        match x with
        | RequestDeviceStatus.Success -> "success" :> obj
        | RequestDeviceStatus.Error -> "error" :> obj
        | RequestDeviceStatus.Unknown -> "unknown" :> obj
        | RequestDeviceStatus.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "success" -> RequestDeviceStatus.Success
            | "error" -> RequestDeviceStatus.Error
            | "unknown" -> RequestDeviceStatus.Unknown
            | str -> RequestDeviceStatus.Custom str
[<RequireQualifiedAccess>]
type SType = 
| Invalid
| SurfaceDescriptorFromMetalLayer
| SurfaceDescriptorFromWindowsHWND
| SurfaceDescriptorFromXlib
| SurfaceDescriptorFromCanvasHTMLSelector
| ShaderModuleSPIRVDescriptor
| ShaderModuleWGSLDescriptor
| PrimitiveDepthClampingState
| SurfaceDescriptorFromWindowsCoreWindow
| ExternalTextureBindingEntry
| ExternalTextureBindingLayout
| SurfaceDescriptorFromWindowsSwapChainPanel
| DawnTextureInternalUsageDescriptor
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
        | SType.PrimitiveDepthClampingState -> "primitive-depth-clamping-state" :> obj
        | SType.SurfaceDescriptorFromWindowsCoreWindow -> "surface-descriptor-from-windows-core-window" :> obj
        | SType.ExternalTextureBindingEntry -> "external-texture-binding-entry" :> obj
        | SType.ExternalTextureBindingLayout -> "external-texture-binding-layout" :> obj
        | SType.SurfaceDescriptorFromWindowsSwapChainPanel -> "surface-descriptor-from-windows-swap-chain-panel" :> obj
        | SType.DawnTextureInternalUsageDescriptor -> "dawn-texture-internal-usage-descriptor" :> obj
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
            | "primitive-depth-clamping-state" -> SType.PrimitiveDepthClampingState
            | "surface-descriptor-from-windows-core-window" -> SType.SurfaceDescriptorFromWindowsCoreWindow
            | "external-texture-binding-entry" -> SType.ExternalTextureBindingEntry
            | "external-texture-binding-layout" -> SType.ExternalTextureBindingLayout
            | "surface-descriptor-from-windows-swap-chain-panel" -> SType.SurfaceDescriptorFromWindowsSwapChainPanel
            | "dawn-texture-internal-usage-descriptor" -> SType.DawnTextureInternalUsageDescriptor
            | str -> SType.Custom str
[<RequireQualifiedAccess>]
type SamplerBindingType = 
| Filtering
| NonFiltering
| Comparison
| Custom of string
| Undefined
    member internal x.GetValue() =
        match x with
        | SamplerBindingType.Filtering -> "filtering" :> obj
        | SamplerBindingType.NonFiltering -> "non-filtering" :> obj
        | SamplerBindingType.Comparison -> "comparison" :> obj
        | SamplerBindingType.Custom n -> n :> obj
        | SamplerBindingType.Undefined -> null
    static member Parse(obj : obj) =
        if isNull obj then SamplerBindingType.Undefined
        else
            match (string obj).Trim().ToLower() with
            | "filtering" -> SamplerBindingType.Filtering
            | "non-filtering" -> SamplerBindingType.NonFiltering
            | "comparison" -> SamplerBindingType.Comparison
            | str -> SamplerBindingType.Custom str
[<Flags>]
type ShaderStage = 
| None = 0x00000000
| Vertex = 0x00000001
| Fragment = 0x00000002
| Compute = 0x00000004
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
type StorageTextureAccess = 
| WriteOnly
| Custom of string
| Undefined
    member internal x.GetValue() =
        match x with
        | StorageTextureAccess.WriteOnly -> "write-only" :> obj
        | StorageTextureAccess.Custom n -> n :> obj
        | StorageTextureAccess.Undefined -> null
    static member Parse(obj : obj) =
        if isNull obj then StorageTextureAccess.Undefined
        else
            match (string obj).Trim().ToLower() with
            | "write-only" -> StorageTextureAccess.WriteOnly
            | str -> StorageTextureAccess.Custom str
[<RequireQualifiedAccess>]
type StoreOp = 
| Store
| Discard
| Custom of string
    member internal x.GetValue() =
        match x with
        | StoreOp.Store -> "store" :> obj
        | StoreOp.Discard -> "discard" :> obj
        | StoreOp.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "store" -> StoreOp.Store
            | "discard" -> StoreOp.Discard
            | str -> StoreOp.Custom str
[<RequireQualifiedAccess>]
type TextureAspect = 
| All
| StencilOnly
| DepthOnly
| Plane0Only
| Plane1Only
| Custom of string
    member internal x.GetValue() =
        match x with
        | TextureAspect.All -> "all" :> obj
        | TextureAspect.StencilOnly -> "stencil-only" :> obj
        | TextureAspect.DepthOnly -> "depth-only" :> obj
        | TextureAspect.Plane0Only -> "plane-0only" :> obj
        | TextureAspect.Plane1Only -> "plane-1only" :> obj
        | TextureAspect.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "all" -> TextureAspect.All
            | "stencil-only" -> TextureAspect.StencilOnly
            | "depth-only" -> TextureAspect.DepthOnly
            | "plane-0only" -> TextureAspect.Plane0Only
            | "plane-1only" -> TextureAspect.Plane1Only
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
| Stencil8
| Depth16Unorm
| Depth24Plus
| Depth24PlusStencil8
| Depth32Float
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
| ETC2RGB8Unorm
| ETC2RGB8UnormSrgb
| ETC2RGB8A1Unorm
| ETC2RGB8A1UnormSrgb
| ETC2RGBA8Unorm
| ETC2RGBA8UnormSrgb
| EACR11Unorm
| EACR11Snorm
| EACRG11Unorm
| EACRG11Snorm
| ASTC4x4Unorm
| ASTC4x4UnormSrgb
| ASTC5x4Unorm
| ASTC5x4UnormSrgb
| ASTC5x5Unorm
| ASTC5x5UnormSrgb
| ASTC6x5Unorm
| ASTC6x5UnormSrgb
| ASTC6x6Unorm
| ASTC6x6UnormSrgb
| ASTC8x5Unorm
| ASTC8x5UnormSrgb
| ASTC8x6Unorm
| ASTC8x6UnormSrgb
| ASTC8x8Unorm
| ASTC8x8UnormSrgb
| ASTC10x5Unorm
| ASTC10x5UnormSrgb
| ASTC10x6Unorm
| ASTC10x6UnormSrgb
| ASTC10x8Unorm
| ASTC10x8UnormSrgb
| ASTC10x10Unorm
| ASTC10x10UnormSrgb
| ASTC12x10Unorm
| ASTC12x10UnormSrgb
| ASTC12x12Unorm
| ASTC12x12UnormSrgb
| R8BG8Biplanar420Unorm
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
        | TextureFormat.Stencil8 -> "stencil8" :> obj
        | TextureFormat.Depth16Unorm -> "depth16unorm" :> obj
        | TextureFormat.Depth24Plus -> "depth24plus" :> obj
        | TextureFormat.Depth24PlusStencil8 -> "depth24plus-stencil8" :> obj
        | TextureFormat.Depth32Float -> "depth32float" :> obj
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
        | TextureFormat.ETC2RGB8Unorm -> "etc2rgb8unorm" :> obj
        | TextureFormat.ETC2RGB8UnormSrgb -> "etc2rgb8unorm-srgb" :> obj
        | TextureFormat.ETC2RGB8A1Unorm -> "etc2rgb8a1unorm" :> obj
        | TextureFormat.ETC2RGB8A1UnormSrgb -> "etc2rgb8a1unorm-srgb" :> obj
        | TextureFormat.ETC2RGBA8Unorm -> "etc2rgba8unorm" :> obj
        | TextureFormat.ETC2RGBA8UnormSrgb -> "etc2rgba8unorm-srgb" :> obj
        | TextureFormat.EACR11Unorm -> "eac-r11unorm" :> obj
        | TextureFormat.EACR11Snorm -> "eac-r11snorm" :> obj
        | TextureFormat.EACRG11Unorm -> "eac-rg11unorm" :> obj
        | TextureFormat.EACRG11Snorm -> "eac-rg11snorm" :> obj
        | TextureFormat.ASTC4x4Unorm -> "astc-4x4unorm" :> obj
        | TextureFormat.ASTC4x4UnormSrgb -> "astc-4x4unorm-srgb" :> obj
        | TextureFormat.ASTC5x4Unorm -> "astc-5x4unorm" :> obj
        | TextureFormat.ASTC5x4UnormSrgb -> "astc-5x4unorm-srgb" :> obj
        | TextureFormat.ASTC5x5Unorm -> "astc-5x5unorm" :> obj
        | TextureFormat.ASTC5x5UnormSrgb -> "astc-5x5unorm-srgb" :> obj
        | TextureFormat.ASTC6x5Unorm -> "astc-6x5unorm" :> obj
        | TextureFormat.ASTC6x5UnormSrgb -> "astc-6x5unorm-srgb" :> obj
        | TextureFormat.ASTC6x6Unorm -> "astc-6x6unorm" :> obj
        | TextureFormat.ASTC6x6UnormSrgb -> "astc-6x6unorm-srgb" :> obj
        | TextureFormat.ASTC8x5Unorm -> "astc-8x5unorm" :> obj
        | TextureFormat.ASTC8x5UnormSrgb -> "astc-8x5unorm-srgb" :> obj
        | TextureFormat.ASTC8x6Unorm -> "astc-8x6unorm" :> obj
        | TextureFormat.ASTC8x6UnormSrgb -> "astc-8x6unorm-srgb" :> obj
        | TextureFormat.ASTC8x8Unorm -> "astc-8x8unorm" :> obj
        | TextureFormat.ASTC8x8UnormSrgb -> "astc-8x8unorm-srgb" :> obj
        | TextureFormat.ASTC10x5Unorm -> "astc-10x5unorm" :> obj
        | TextureFormat.ASTC10x5UnormSrgb -> "astc-10x5unorm-srgb" :> obj
        | TextureFormat.ASTC10x6Unorm -> "astc-10x6unorm" :> obj
        | TextureFormat.ASTC10x6UnormSrgb -> "astc-10x6unorm-srgb" :> obj
        | TextureFormat.ASTC10x8Unorm -> "astc-10x8unorm" :> obj
        | TextureFormat.ASTC10x8UnormSrgb -> "astc-10x8unorm-srgb" :> obj
        | TextureFormat.ASTC10x10Unorm -> "astc-10x10unorm" :> obj
        | TextureFormat.ASTC10x10UnormSrgb -> "astc-10x10unorm-srgb" :> obj
        | TextureFormat.ASTC12x10Unorm -> "astc-12x10unorm" :> obj
        | TextureFormat.ASTC12x10UnormSrgb -> "astc-12x10unorm-srgb" :> obj
        | TextureFormat.ASTC12x12Unorm -> "astc-12x12unorm" :> obj
        | TextureFormat.ASTC12x12UnormSrgb -> "astc-12x12unorm-srgb" :> obj
        | TextureFormat.R8BG8Biplanar420Unorm -> "r8bg8biplanar-420unorm" :> obj
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
            | "stencil8" -> TextureFormat.Stencil8
            | "depth16unorm" -> TextureFormat.Depth16Unorm
            | "depth24plus" -> TextureFormat.Depth24Plus
            | "depth24plus-stencil8" -> TextureFormat.Depth24PlusStencil8
            | "depth32float" -> TextureFormat.Depth32Float
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
            | "etc2rgb8unorm" -> TextureFormat.ETC2RGB8Unorm
            | "etc2rgb8unorm-srgb" -> TextureFormat.ETC2RGB8UnormSrgb
            | "etc2rgb8a1unorm" -> TextureFormat.ETC2RGB8A1Unorm
            | "etc2rgb8a1unorm-srgb" -> TextureFormat.ETC2RGB8A1UnormSrgb
            | "etc2rgba8unorm" -> TextureFormat.ETC2RGBA8Unorm
            | "etc2rgba8unorm-srgb" -> TextureFormat.ETC2RGBA8UnormSrgb
            | "eac-r11unorm" -> TextureFormat.EACR11Unorm
            | "eac-r11snorm" -> TextureFormat.EACR11Snorm
            | "eac-rg11unorm" -> TextureFormat.EACRG11Unorm
            | "eac-rg11snorm" -> TextureFormat.EACRG11Snorm
            | "astc-4x4unorm" -> TextureFormat.ASTC4x4Unorm
            | "astc-4x4unorm-srgb" -> TextureFormat.ASTC4x4UnormSrgb
            | "astc-5x4unorm" -> TextureFormat.ASTC5x4Unorm
            | "astc-5x4unorm-srgb" -> TextureFormat.ASTC5x4UnormSrgb
            | "astc-5x5unorm" -> TextureFormat.ASTC5x5Unorm
            | "astc-5x5unorm-srgb" -> TextureFormat.ASTC5x5UnormSrgb
            | "astc-6x5unorm" -> TextureFormat.ASTC6x5Unorm
            | "astc-6x5unorm-srgb" -> TextureFormat.ASTC6x5UnormSrgb
            | "astc-6x6unorm" -> TextureFormat.ASTC6x6Unorm
            | "astc-6x6unorm-srgb" -> TextureFormat.ASTC6x6UnormSrgb
            | "astc-8x5unorm" -> TextureFormat.ASTC8x5Unorm
            | "astc-8x5unorm-srgb" -> TextureFormat.ASTC8x5UnormSrgb
            | "astc-8x6unorm" -> TextureFormat.ASTC8x6Unorm
            | "astc-8x6unorm-srgb" -> TextureFormat.ASTC8x6UnormSrgb
            | "astc-8x8unorm" -> TextureFormat.ASTC8x8Unorm
            | "astc-8x8unorm-srgb" -> TextureFormat.ASTC8x8UnormSrgb
            | "astc-10x5unorm" -> TextureFormat.ASTC10x5Unorm
            | "astc-10x5unorm-srgb" -> TextureFormat.ASTC10x5UnormSrgb
            | "astc-10x6unorm" -> TextureFormat.ASTC10x6Unorm
            | "astc-10x6unorm-srgb" -> TextureFormat.ASTC10x6UnormSrgb
            | "astc-10x8unorm" -> TextureFormat.ASTC10x8Unorm
            | "astc-10x8unorm-srgb" -> TextureFormat.ASTC10x8UnormSrgb
            | "astc-10x10unorm" -> TextureFormat.ASTC10x10Unorm
            | "astc-10x10unorm-srgb" -> TextureFormat.ASTC10x10UnormSrgb
            | "astc-12x10unorm" -> TextureFormat.ASTC12x10Unorm
            | "astc-12x10unorm-srgb" -> TextureFormat.ASTC12x10UnormSrgb
            | "astc-12x12unorm" -> TextureFormat.ASTC12x12Unorm
            | "astc-12x12unorm-srgb" -> TextureFormat.ASTC12x12UnormSrgb
            | "r8bg8biplanar-420unorm" -> TextureFormat.R8BG8Biplanar420Unorm
            | str -> TextureFormat.Custom str
[<RequireQualifiedAccess>]
type TextureSampleType = 
| Float
| UnfilterableFloat
| Depth
| Sint
| Uint
| Custom of string
| Undefined
    member internal x.GetValue() =
        match x with
        | TextureSampleType.Float -> "float" :> obj
        | TextureSampleType.UnfilterableFloat -> "unfilterable-float" :> obj
        | TextureSampleType.Depth -> "depth" :> obj
        | TextureSampleType.Sint -> "sint" :> obj
        | TextureSampleType.Uint -> "uint" :> obj
        | TextureSampleType.Custom n -> n :> obj
        | TextureSampleType.Undefined -> null
    static member Parse(obj : obj) =
        if isNull obj then TextureSampleType.Undefined
        else
            match (string obj).Trim().ToLower() with
            | "float" -> TextureSampleType.Float
            | "unfilterable-float" -> TextureSampleType.UnfilterableFloat
            | "depth" -> TextureSampleType.Depth
            | "sint" -> TextureSampleType.Sint
            | "uint" -> TextureSampleType.Uint
            | str -> TextureSampleType.Custom str
[<Flags>]
type TextureUsage = 
| None = 0x00000000
| CopySrc = 0x00000001
| CopyDst = 0x00000002
| TextureBinding = 0x00000004
| StorageBinding = 0x00000008
| RenderAttachment = 0x00000010
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
| Uint8x2
| Uint8x4
| Sint8x2
| Sint8x4
| Unorm8x2
| Unorm8x4
| Snorm8x2
| Snorm8x4
| Uint16x2
| Uint16x4
| Sint16x2
| Sint16x4
| Unorm16x2
| Unorm16x4
| Snorm16x2
| Snorm16x4
| Float16x2
| Float16x4
| Float32
| Float32x2
| Float32x3
| Float32x4
| Uint32
| Uint32x2
| Uint32x3
| Uint32x4
| Sint32
| Sint32x2
| Sint32x3
| Sint32x4
| Custom of string
| Undefined
    member internal x.GetValue() =
        match x with
        | VertexFormat.Uint8x2 -> "uint8x2" :> obj
        | VertexFormat.Uint8x4 -> "uint8x4" :> obj
        | VertexFormat.Sint8x2 -> "sint8x2" :> obj
        | VertexFormat.Sint8x4 -> "sint8x4" :> obj
        | VertexFormat.Unorm8x2 -> "unorm8x2" :> obj
        | VertexFormat.Unorm8x4 -> "unorm8x4" :> obj
        | VertexFormat.Snorm8x2 -> "snorm8x2" :> obj
        | VertexFormat.Snorm8x4 -> "snorm8x4" :> obj
        | VertexFormat.Uint16x2 -> "uint16x2" :> obj
        | VertexFormat.Uint16x4 -> "uint16x4" :> obj
        | VertexFormat.Sint16x2 -> "sint16x2" :> obj
        | VertexFormat.Sint16x4 -> "sint16x4" :> obj
        | VertexFormat.Unorm16x2 -> "unorm16x2" :> obj
        | VertexFormat.Unorm16x4 -> "unorm16x4" :> obj
        | VertexFormat.Snorm16x2 -> "snorm16x2" :> obj
        | VertexFormat.Snorm16x4 -> "snorm16x4" :> obj
        | VertexFormat.Float16x2 -> "float16x2" :> obj
        | VertexFormat.Float16x4 -> "float16x4" :> obj
        | VertexFormat.Float32 -> "float32" :> obj
        | VertexFormat.Float32x2 -> "float32x2" :> obj
        | VertexFormat.Float32x3 -> "float32x3" :> obj
        | VertexFormat.Float32x4 -> "float32x4" :> obj
        | VertexFormat.Uint32 -> "uint32" :> obj
        | VertexFormat.Uint32x2 -> "uint32x2" :> obj
        | VertexFormat.Uint32x3 -> "uint32x3" :> obj
        | VertexFormat.Uint32x4 -> "uint32x4" :> obj
        | VertexFormat.Sint32 -> "sint32" :> obj
        | VertexFormat.Sint32x2 -> "sint32x2" :> obj
        | VertexFormat.Sint32x3 -> "sint32x3" :> obj
        | VertexFormat.Sint32x4 -> "sint32x4" :> obj
        | VertexFormat.Custom n -> n :> obj
        | VertexFormat.Undefined -> null
    static member Parse(obj : obj) =
        if isNull obj then VertexFormat.Undefined
        else
            match (string obj).Trim().ToLower() with
            | "uint8x2" -> VertexFormat.Uint8x2
            | "uint8x4" -> VertexFormat.Uint8x4
            | "sint8x2" -> VertexFormat.Sint8x2
            | "sint8x4" -> VertexFormat.Sint8x4
            | "unorm8x2" -> VertexFormat.Unorm8x2
            | "unorm8x4" -> VertexFormat.Unorm8x4
            | "snorm8x2" -> VertexFormat.Snorm8x2
            | "snorm8x4" -> VertexFormat.Snorm8x4
            | "uint16x2" -> VertexFormat.Uint16x2
            | "uint16x4" -> VertexFormat.Uint16x4
            | "sint16x2" -> VertexFormat.Sint16x2
            | "sint16x4" -> VertexFormat.Sint16x4
            | "unorm16x2" -> VertexFormat.Unorm16x2
            | "unorm16x4" -> VertexFormat.Unorm16x4
            | "snorm16x2" -> VertexFormat.Snorm16x2
            | "snorm16x4" -> VertexFormat.Snorm16x4
            | "float16x2" -> VertexFormat.Float16x2
            | "float16x4" -> VertexFormat.Float16x4
            | "float32" -> VertexFormat.Float32
            | "float32x2" -> VertexFormat.Float32x2
            | "float32x3" -> VertexFormat.Float32x3
            | "float32x4" -> VertexFormat.Float32x4
            | "uint32" -> VertexFormat.Uint32
            | "uint32x2" -> VertexFormat.Uint32x2
            | "uint32x3" -> VertexFormat.Uint32x3
            | "uint32x4" -> VertexFormat.Uint32x4
            | "sint32" -> VertexFormat.Sint32
            | "sint32x2" -> VertexFormat.Sint32x2
            | "sint32x3" -> VertexFormat.Sint32x3
            | "sint32x4" -> VertexFormat.Sint32x4
            | str -> VertexFormat.Custom str
[<RequireQualifiedAccess>]
type VertexStepMode = 
| Vertex
| Instance
| Custom of string
    member internal x.GetValue() =
        match x with
        | VertexStepMode.Vertex -> "vertex" :> obj
        | VertexStepMode.Instance -> "instance" :> obj
        | VertexStepMode.Custom n -> n :> obj
    static member Parse(obj : obj) =
            match (string obj).Trim().ToLower() with
            | "vertex" -> VertexStepMode.Vertex
            | "instance" -> VertexStepMode.Instance
            | str -> VertexStepMode.Custom str


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
    type WGPUConstantEntry(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUConstantEntry(new JSObject())
        member x.Key
            with get() : string = h.GetObjectProperty("key") |> convert<string>
            and set (v : string) = h.SetObjectProperty("key", js v)
        member x.Value
            with get() : float = h.GetObjectProperty("value") |> convert<float>
            and set (v : float) = h.SetObjectProperty("value", js v)
    [<AllowNullLiteral>]
    type WGPUExtent3D(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUExtent3D(new JSObject())
        member x.Width
            with get() : uint32 = h.GetObjectProperty("width") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("width", js v)
        member x.Height
            with get() : uint32 = h.GetObjectProperty("height") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("height", js v)
        member x.DepthOrArrayLayers
            with get() : uint32 = h.GetObjectProperty("depthOrArrayLayers") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("depthOrArrayLayers", js v)
    [<AllowNullLiteral>]
    type WGPUExternalTextureBindingLayout(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUExternalTextureBindingLayout(new JSObject())
    [<AllowNullLiteral>]
    type WGPUInstanceDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUInstanceDescriptor(new JSObject())
    [<AllowNullLiteral>]
    type WGPULimits(h : JSObject) =
        inherit JsObj(h)
        new() = WGPULimits(new JSObject())
        member x.MaxTextureDimension1D
            with get() : uint32 = h.GetObjectProperty("maxTextureDimension1D") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("maxTextureDimension1D", js v)
        member x.MaxTextureDimension2D
            with get() : uint32 = h.GetObjectProperty("maxTextureDimension2D") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("maxTextureDimension2D", js v)
        member x.MaxTextureDimension3D
            with get() : uint32 = h.GetObjectProperty("maxTextureDimension3D") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("maxTextureDimension3D", js v)
        member x.MaxTextureArrayLayers
            with get() : uint32 = h.GetObjectProperty("maxTextureArrayLayers") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("maxTextureArrayLayers", js v)
        member x.MaxBindGroups
            with get() : uint32 = h.GetObjectProperty("maxBindGroups") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("maxBindGroups", js v)
        member x.MaxDynamicUniformBuffersPerPipelineLayout
            with get() : uint32 = h.GetObjectProperty("maxDynamicUniformBuffersPerPipelineLayout") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("maxDynamicUniformBuffersPerPipelineLayout", js v)
        member x.MaxDynamicStorageBuffersPerPipelineLayout
            with get() : uint32 = h.GetObjectProperty("maxDynamicStorageBuffersPerPipelineLayout") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("maxDynamicStorageBuffersPerPipelineLayout", js v)
        member x.MaxSampledTexturesPerShaderStage
            with get() : uint32 = h.GetObjectProperty("maxSampledTexturesPerShaderStage") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("maxSampledTexturesPerShaderStage", js v)
        member x.MaxSamplersPerShaderStage
            with get() : uint32 = h.GetObjectProperty("maxSamplersPerShaderStage") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("maxSamplersPerShaderStage", js v)
        member x.MaxStorageBuffersPerShaderStage
            with get() : uint32 = h.GetObjectProperty("maxStorageBuffersPerShaderStage") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("maxStorageBuffersPerShaderStage", js v)
        member x.MaxStorageTexturesPerShaderStage
            with get() : uint32 = h.GetObjectProperty("maxStorageTexturesPerShaderStage") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("maxStorageTexturesPerShaderStage", js v)
        member x.MaxUniformBuffersPerShaderStage
            with get() : uint32 = h.GetObjectProperty("maxUniformBuffersPerShaderStage") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("maxUniformBuffersPerShaderStage", js v)
        member x.MaxUniformBufferBindingSize
            with get() : float = h.GetObjectProperty("maxUniformBufferBindingSize") |> convert<float>
            and set (v : float) = h.SetObjectProperty("maxUniformBufferBindingSize", js v)
        member x.MaxStorageBufferBindingSize
            with get() : float = h.GetObjectProperty("maxStorageBufferBindingSize") |> convert<float>
            and set (v : float) = h.SetObjectProperty("maxStorageBufferBindingSize", js v)
        member x.MinUniformBufferOffsetAlignment
            with get() : uint32 = h.GetObjectProperty("minUniformBufferOffsetAlignment") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("minUniformBufferOffsetAlignment", js v)
        member x.MinStorageBufferOffsetAlignment
            with get() : uint32 = h.GetObjectProperty("minStorageBufferOffsetAlignment") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("minStorageBufferOffsetAlignment", js v)
        member x.MaxVertexBuffers
            with get() : uint32 = h.GetObjectProperty("maxVertexBuffers") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("maxVertexBuffers", js v)
        member x.MaxVertexAttributes
            with get() : uint32 = h.GetObjectProperty("maxVertexAttributes") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("maxVertexAttributes", js v)
        member x.MaxVertexBufferArrayStride
            with get() : uint32 = h.GetObjectProperty("maxVertexBufferArrayStride") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("maxVertexBufferArrayStride", js v)
        member x.MaxInterStageShaderComponents
            with get() : uint32 = h.GetObjectProperty("maxInterStageShaderComponents") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("maxInterStageShaderComponents", js v)
        member x.MaxComputeWorkgroupStorageSize
            with get() : uint32 = h.GetObjectProperty("maxComputeWorkgroupStorageSize") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("maxComputeWorkgroupStorageSize", js v)
        member x.MaxComputeInvocationsPerWorkgroup
            with get() : uint32 = h.GetObjectProperty("maxComputeInvocationsPerWorkgroup") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("maxComputeInvocationsPerWorkgroup", js v)
        member x.MaxComputeWorkgroupSizeX
            with get() : uint32 = h.GetObjectProperty("maxComputeWorkgroupSizeX") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("maxComputeWorkgroupSizeX", js v)
        member x.MaxComputeWorkgroupSizeY
            with get() : uint32 = h.GetObjectProperty("maxComputeWorkgroupSizeY") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("maxComputeWorkgroupSizeY", js v)
        member x.MaxComputeWorkgroupSizeZ
            with get() : uint32 = h.GetObjectProperty("maxComputeWorkgroupSizeZ") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("maxComputeWorkgroupSizeZ", js v)
        member x.MaxComputeWorkgroupsPerDimension
            with get() : uint32 = h.GetObjectProperty("maxComputeWorkgroupsPerDimension") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("maxComputeWorkgroupsPerDimension", js v)
    [<AllowNullLiteral>]
    type WGPUMultisampleState(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUMultisampleState(new JSObject())
        member x.Count
            with get() : uint32 = h.GetObjectProperty("count") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("count", js v)
        member x.Mask
            with get() : uint32 = h.GetObjectProperty("mask") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("mask", js v)
        member x.AlphaToCoverageEnabled
            with get() : bool = h.GetObjectProperty("alphaToCoverageEnabled") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("alphaToCoverageEnabled", js v)
    [<AllowNullLiteral>]
    type WGPUOrigin3D(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUOrigin3D(new JSObject())
        member x.X
            with get() : uint32 = h.GetObjectProperty("x") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("x", js v)
        member x.Y
            with get() : uint32 = h.GetObjectProperty("y") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("y", js v)
        member x.Z
            with get() : uint32 = h.GetObjectProperty("z") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("z", js v)
    [<AllowNullLiteral>]
    type WGPUPrimitiveDepthClampingState(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUPrimitiveDepthClampingState(new JSObject())
        member x.ClampDepth
            with get() : bool = h.GetObjectProperty("clampDepth") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("clampDepth", js v)
    [<AllowNullLiteral>]
    type WGPURenderBundleDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPURenderBundleDescriptor(new JSObject())
        member x.Label
            with get() : string = h.GetObjectProperty("label") |> convert<string>
            and set (v : string) = h.SetObjectProperty("label", js v)
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
            with get() : float = h.GetObjectProperty("layer") |> convert<float>
            and set (v : float) = h.SetObjectProperty("layer", js v)
    [<AllowNullLiteral>]
    type WGPUSurfaceDescriptorFromWindowsHWND(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUSurfaceDescriptorFromWindowsHWND(new JSObject())
        member x.Hinstance
            with get() : float = h.GetObjectProperty("hinstance") |> convert<float>
            and set (v : float) = h.SetObjectProperty("hinstance", js v)
        member x.Hwnd
            with get() : float = h.GetObjectProperty("hwnd") |> convert<float>
            and set (v : float) = h.SetObjectProperty("hwnd", js v)
    [<AllowNullLiteral>]
    type WGPUSurfaceDescriptorFromWindowsCoreWindow(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUSurfaceDescriptorFromWindowsCoreWindow(new JSObject())
        member x.CoreWindow
            with get() : float = h.GetObjectProperty("coreWindow") |> convert<float>
            and set (v : float) = h.SetObjectProperty("coreWindow", js v)
    [<AllowNullLiteral>]
    type WGPUSurfaceDescriptorFromWindowsSwapChainPanel(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUSurfaceDescriptorFromWindowsSwapChainPanel(new JSObject())
        member x.SwapChainPanel
            with get() : float = h.GetObjectProperty("swapChainPanel") |> convert<float>
            and set (v : float) = h.SetObjectProperty("swapChainPanel", js v)
    [<AllowNullLiteral>]
    type WGPUSurfaceDescriptorFromXlib(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUSurfaceDescriptorFromXlib(new JSObject())
        member x.Display
            with get() : float = h.GetObjectProperty("display") |> convert<float>
            and set (v : float) = h.SetObjectProperty("display", js v)
        member x.Window
            with get() : uint32 = h.GetObjectProperty("window") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("window", js v)
    [<AllowNullLiteral>]
    type WGPUTextureDataLayout(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUTextureDataLayout(new JSObject())
        member x.Offset
            with get() : float = h.GetObjectProperty("offset") |> convert<float>
            and set (v : float) = h.SetObjectProperty("offset", js v)
        member x.BytesPerRow
            with get() : uint32 = h.GetObjectProperty("bytesPerRow") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("bytesPerRow", js v)
        member x.RowsPerImage
            with get() : uint32 = h.GetObjectProperty("rowsPerImage") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("rowsPerImage", js v)
    [<AllowNullLiteral>]
    type WGPUAdapterProperties(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUAdapterProperties(new JSObject())
        member x.VendorID
            with get() : uint32 = h.GetObjectProperty("vendorID") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("vendorID", js v)
        member x.DeviceID
            with get() : uint32 = h.GetObjectProperty("deviceID") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("deviceID", js v)
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
    type WGPUBlendComponent(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUBlendComponent(new JSObject())
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
    type WGPUBufferBindingLayout(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUBufferBindingLayout(new JSObject())
        member x.Type
            with get() : obj = h.GetObjectProperty("type") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("type", v)
        member x.HasDynamicOffset
            with get() : bool = h.GetObjectProperty("hasDynamicOffset") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("hasDynamicOffset", js v)
        member x.MinBindingSize
            with get() : float = h.GetObjectProperty("minBindingSize") |> convert<float>
            and set (v : float) = h.SetObjectProperty("minBindingSize", js v)
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
            with get() : float = h.GetObjectProperty("size") |> convert<float>
            and set (v : float) = h.SetObjectProperty("size", js v)
        member x.MappedAtCreation
            with get() : bool = h.GetObjectProperty("mappedAtCreation") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("mappedAtCreation", js v)
    [<AllowNullLiteral>]
    type WGPUCompilationMessage(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUCompilationMessage(new JSObject())
        member x.Message
            with get() : string = h.GetObjectProperty("message") |> convert<string>
            and set (v : string) = h.SetObjectProperty("message", js v)
        member x.Type
            with get() : obj = h.GetObjectProperty("type") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("type", v)
        member x.LineNum
            with get() : float = h.GetObjectProperty("lineNum") |> convert<float>
            and set (v : float) = h.SetObjectProperty("lineNum", js v)
        member x.LinePos
            with get() : float = h.GetObjectProperty("linePos") |> convert<float>
            and set (v : float) = h.SetObjectProperty("linePos", js v)
        member x.Offset
            with get() : float = h.GetObjectProperty("offset") |> convert<float>
            and set (v : float) = h.SetObjectProperty("offset", js v)
        member x.Length
            with get() : float = h.GetObjectProperty("length") |> convert<float>
            and set (v : float) = h.SetObjectProperty("length", js v)
    [<AllowNullLiteral>]
    type WGPUCopyTextureForBrowserOptions(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUCopyTextureForBrowserOptions(new JSObject())
        member x.FlipY
            with get() : bool = h.GetObjectProperty("flipY") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("flipY", js v)
        member x.AlphaOp
            with get() : obj = h.GetObjectProperty("alphaOp") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("alphaOp", v)
    [<AllowNullLiteral>]
    type WGPUDawnTextureInternalUsageDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUDawnTextureInternalUsageDescriptor(new JSObject())
        member x.InternalUsage
            with get() : int = h.GetObjectProperty("internalUsage") |> convert<int>
            and set (v : int) = h.SetObjectProperty("internalUsage", v)
    [<AllowNullLiteral>]
    type WGPUExternalTextureDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUExternalTextureDescriptor(new JSObject())
        member x.Label
            with get() : string = h.GetObjectProperty("label") |> convert<string>
            and set (v : string) = h.SetObjectProperty("label", js v)
        member x.Plane0
            with get() : TextureViewHandle = h.GetObjectProperty("plane0") |> convert<TextureViewHandle>
            and set (v : TextureViewHandle) = if not (isNull v) then h.SetObjectProperty("plane0", js v)
        member x.Format
            with get() : obj = h.GetObjectProperty("format") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("format", v)
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
    type WGPUPrimitiveState(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUPrimitiveState(new JSObject())
        member x.Topology
            with get() : obj = h.GetObjectProperty("topology") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("topology", v)
        member x.StripIndexFormat
            with get() : obj = h.GetObjectProperty("stripIndexFormat") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("stripIndexFormat", v)
        member x.FrontFace
            with get() : obj = h.GetObjectProperty("frontFace") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("frontFace", v)
        member x.CullMode
            with get() : obj = h.GetObjectProperty("cullMode") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("cullMode", v)
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
            with get() : uint32 = h.GetObjectProperty("count") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("count", js v)
        member x.PipelineStatistics
            with get() : obj = h.GetObjectProperty("pipelineStatistics") |> convert<obj>
            and set (v : obj) = if not (isNull v) then h.SetObjectProperty("pipelineStatistics", js v)
        member x.PipelineStatisticsCount
            with get() : uint32 = h.GetObjectProperty("pipelineStatisticsCount") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("pipelineStatisticsCount", js v)
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
            with get() : uint32 = h.GetObjectProperty("sampleCount") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("sampleCount", js v)
    [<AllowNullLiteral>]
    type WGPURenderPassColorAttachment(h : JSObject) =
        inherit JsObj(h)
        new() = WGPURenderPassColorAttachment(new JSObject())
        member x.View
            with get() : TextureViewHandle = h.GetObjectProperty("view") |> convert<TextureViewHandle>
            and set (v : TextureViewHandle) = if not (isNull v) then h.SetObjectProperty("view", js v)
        member x.ResolveTarget
            with get() : TextureViewHandle = h.GetObjectProperty("resolveTarget") |> convert<TextureViewHandle>
            and set (v : TextureViewHandle) = if not (isNull v) then h.SetObjectProperty("resolveTarget", js v)
        member x.LoadOp
            with get() : obj = h.GetObjectProperty("loadValue") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("loadValue", v)
        member x.StoreOp
            with get() : obj = h.GetObjectProperty("storeOp") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("storeOp", v)
    [<AllowNullLiteral>]
    type WGPURenderPassDepthStencilAttachment(h : JSObject) =
        inherit JsObj(h)
        new() = WGPURenderPassDepthStencilAttachment(new JSObject())
        member x.View
            with get() : TextureViewHandle = h.GetObjectProperty("view") |> convert<TextureViewHandle>
            and set (v : TextureViewHandle) = if not (isNull v) then h.SetObjectProperty("view", js v)
        member x.DepthLoadOp
            with get() : obj = h.GetObjectProperty("depthLoadOp") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("depthLoadOp", v)
        member x.DepthStoreOp
            with get() : obj = h.GetObjectProperty("depthStoreOp") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("depthStoreOp", v)
        member x.ClearDepth
            with get() : float32 = h.GetObjectProperty("clearDepth") |> convert<float32>
            and set (v : float32) = h.SetObjectProperty("clearDepth", js v)
        member x.DepthReadOnly
            with get() : bool = h.GetObjectProperty("depthReadOnly") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("depthReadOnly", js v)
        member x.StencilLoadOp
            with get() : obj = h.GetObjectProperty("stencilLoadOp") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("stencilLoadOp", v)
        member x.StencilStoreOp
            with get() : obj = h.GetObjectProperty("stencilStoreOp") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("stencilStoreOp", v)
        member x.ClearStencil
            with get() : uint32 = h.GetObjectProperty("clearStencil") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("clearStencil", js v)
        member x.StencilReadOnly
            with get() : bool = h.GetObjectProperty("stencilReadOnly") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("stencilReadOnly", js v)
    [<AllowNullLiteral>]
    type WGPURequiredLimits(h : JSObject) =
        inherit JsObj(h)
        new() = WGPURequiredLimits(new JSObject())
        member x.Limits
            with get() : DawnRaw.WGPULimits = h.GetObjectProperty("limits") |> convert<DawnRaw.WGPULimits>
            and set (v : DawnRaw.WGPULimits) = if not (isNull v) then h.SetObjectProperty("limits", js v)
    [<AllowNullLiteral>]
    type WGPUSamplerBindingLayout(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUSamplerBindingLayout(new JSObject())
        member x.Type
            with get() : obj = h.GetObjectProperty("type") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("type", v)
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
        member x.MaxAnisotropy
            with get() : uint16 = h.GetObjectProperty("maxAnisotropy") |> convert<uint16>
            and set (v : uint16) = h.SetObjectProperty("maxAnisotropy", js v)
    [<AllowNullLiteral>]
    type WGPUStencilFaceState(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUStencilFaceState(new JSObject())
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
    type WGPUStorageTextureBindingLayout(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUStorageTextureBindingLayout(new JSObject())
        member x.Access
            with get() : obj = h.GetObjectProperty("access") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("access", v)
        member x.Format
            with get() : obj = h.GetObjectProperty("format") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("format", v)
        member x.ViewDimension
            with get() : obj = h.GetObjectProperty("viewDimension") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("viewDimension", v)
    [<AllowNullLiteral>]
    type WGPUSupportedLimits(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUSupportedLimits(new JSObject())
        member x.Limits
            with get() : DawnRaw.WGPULimits = h.GetObjectProperty("limits") |> convert<DawnRaw.WGPULimits>
            and set (v : DawnRaw.WGPULimits) = if not (isNull v) then h.SetObjectProperty("limits", js v)
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
            with get() : uint32 = h.GetObjectProperty("width") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("width", js v)
        member x.Height
            with get() : uint32 = h.GetObjectProperty("height") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("height", js v)
        member x.PresentMode
            with get() : obj = h.GetObjectProperty("presentMode") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("presentMode", v)
        member x.Implementation
            with get() : float = h.GetObjectProperty("implementation") |> convert<float>
            and set (v : float) = h.SetObjectProperty("implementation", js v)
    [<AllowNullLiteral>]
    type WGPUTextureBindingLayout(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUTextureBindingLayout(new JSObject())
        member x.SampleType
            with get() : obj = h.GetObjectProperty("sampleType") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("sampleType", v)
        member x.ViewDimension
            with get() : obj = h.GetObjectProperty("viewDimension") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("viewDimension", v)
        member x.Multisampled
            with get() : bool = h.GetObjectProperty("multisampled") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("multisampled", js v)
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
            and set (v : DawnRaw.WGPUExtent3D) = if not (isNull v) then h.SetObjectProperty("size", js v)
        member x.Format
            with get() : obj = h.GetObjectProperty("format") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("format", v)
        member x.MipLevelCount
            with get() : uint32 = h.GetObjectProperty("mipLevelCount") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("mipLevelCount", js v)
        member x.SampleCount
            with get() : uint32 = h.GetObjectProperty("sampleCount") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("sampleCount", js v)
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
            with get() : uint32 = h.GetObjectProperty("baseMipLevel") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("baseMipLevel", js v)
        member x.MipLevelCount
            with get() : uint32 = h.GetObjectProperty("mipLevelCount") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("mipLevelCount", js v)
        member x.BaseArrayLayer
            with get() : uint32 = h.GetObjectProperty("baseArrayLayer") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("baseArrayLayer", js v)
        member x.ArrayLayerCount
            with get() : uint32 = h.GetObjectProperty("arrayLayerCount") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("arrayLayerCount", js v)
        member x.Aspect
            with get() : obj = h.GetObjectProperty("aspect") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("aspect", v)
    [<AllowNullLiteral>]
    type WGPUVertexAttribute(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUVertexAttribute(new JSObject())
        member x.Format
            with get() : obj = h.GetObjectProperty("format") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("format", v)
        member x.Offset
            with get() : float = h.GetObjectProperty("offset") |> convert<float>
            and set (v : float) = h.SetObjectProperty("offset", js v)
        member x.ShaderLocation
            with get() : uint32 = h.GetObjectProperty("shaderLocation") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("shaderLocation", js v)
    [<AllowNullLiteral>]
    type WGPUBindGroupLayoutEntry(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUBindGroupLayoutEntry(new JSObject())
        member x.Binding
            with get() : uint32 = h.GetObjectProperty("binding") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("binding", js v)
        member x.Visibility
            with get() : int = h.GetObjectProperty("visibility") |> convert<int>
            and set (v : int) = h.SetObjectProperty("visibility", v)
        member x.Buffer
            with get() : DawnRaw.WGPUBufferBindingLayout = h.GetObjectProperty("buffer") |> convert<DawnRaw.WGPUBufferBindingLayout>
            and set (v : DawnRaw.WGPUBufferBindingLayout) = if not (isNull v) then h.SetObjectProperty("buffer", js v)
        member x.Sampler
            with get() : DawnRaw.WGPUSamplerBindingLayout = h.GetObjectProperty("sampler") |> convert<DawnRaw.WGPUSamplerBindingLayout>
            and set (v : DawnRaw.WGPUSamplerBindingLayout) = if not (isNull v) then h.SetObjectProperty("sampler", js v)
        member x.Texture
            with get() : DawnRaw.WGPUTextureBindingLayout = h.GetObjectProperty("texture") |> convert<DawnRaw.WGPUTextureBindingLayout>
            and set (v : DawnRaw.WGPUTextureBindingLayout) = if not (isNull v) then h.SetObjectProperty("texture", js v)
        member x.StorageTexture
            with get() : DawnRaw.WGPUStorageTextureBindingLayout = h.GetObjectProperty("storageTexture") |> convert<DawnRaw.WGPUStorageTextureBindingLayout>
            and set (v : DawnRaw.WGPUStorageTextureBindingLayout) = if not (isNull v) then h.SetObjectProperty("storageTexture", js v)
    [<AllowNullLiteral>]
    type WGPUBlendState(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUBlendState(new JSObject())
        member x.Color
            with get() : DawnRaw.WGPUBlendComponent = h.GetObjectProperty("color") |> convert<DawnRaw.WGPUBlendComponent>
            and set (v : DawnRaw.WGPUBlendComponent) = if not (isNull v) then h.SetObjectProperty("color", js v)
        member x.Alpha
            with get() : DawnRaw.WGPUBlendComponent = h.GetObjectProperty("alpha") |> convert<DawnRaw.WGPUBlendComponent>
            and set (v : DawnRaw.WGPUBlendComponent) = if not (isNull v) then h.SetObjectProperty("alpha", js v)
    [<AllowNullLiteral>]
    type WGPUCompilationInfo(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUCompilationInfo(new JSObject())
        member x.Messages
            with get() : JSObject = h.GetObjectProperty("messages") |> convert<JSObject>
            and set (v : JSObject) = h.SetObjectProperty("messages", js v)
    [<AllowNullLiteral>]
    type WGPUDepthStencilState(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUDepthStencilState(new JSObject())
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
            with get() : DawnRaw.WGPUStencilFaceState = h.GetObjectProperty("stencilFront") |> convert<DawnRaw.WGPUStencilFaceState>
            and set (v : DawnRaw.WGPUStencilFaceState) = if not (isNull v) then h.SetObjectProperty("stencilFront", js v)
        member x.StencilBack
            with get() : DawnRaw.WGPUStencilFaceState = h.GetObjectProperty("stencilBack") |> convert<DawnRaw.WGPUStencilFaceState>
            and set (v : DawnRaw.WGPUStencilFaceState) = if not (isNull v) then h.SetObjectProperty("stencilBack", js v)
        member x.StencilReadMask
            with get() : uint32 = h.GetObjectProperty("stencilReadMask") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("stencilReadMask", js v)
        member x.StencilWriteMask
            with get() : uint32 = h.GetObjectProperty("stencilWriteMask") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("stencilWriteMask", js v)
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
    type WGPUDeviceDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUDeviceDescriptor(new JSObject())
        member x.RequiredFeatures
            with get() : JSObject = h.GetObjectProperty("requiredFeatures") |> convert<JSObject>
            and set (v : JSObject) = h.SetObjectProperty("requiredFeatures", js v)
        member x.RequiredLimits
            with get() : DawnRaw.WGPURequiredLimits = h.GetObjectProperty("requiredLimits") |> convert<DawnRaw.WGPURequiredLimits>
            and set (v : DawnRaw.WGPURequiredLimits) = if not (isNull v) then h.SetObjectProperty("requiredLimits", js v)
    [<AllowNullLiteral>]
    type WGPUDeviceProperties(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUDeviceProperties(new JSObject())
        member x.DeviceID
            with get() : uint32 = h.GetObjectProperty("deviceID") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("deviceID", js v)
        member x.VendorID
            with get() : uint32 = h.GetObjectProperty("vendorID") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("vendorID", js v)
        member x.TextureCompressionBC
            with get() : bool = h.GetObjectProperty("textureCompressionBC") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("textureCompressionBC", js v)
        member x.TextureCompressionETC2
            with get() : bool = h.GetObjectProperty("textureCompressionETC2") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("textureCompressionETC2", js v)
        member x.TextureCompressionASTC
            with get() : bool = h.GetObjectProperty("textureCompressionASTC") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("textureCompressionASTC", js v)
        member x.ShaderFloat16
            with get() : bool = h.GetObjectProperty("shaderFloat16") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("shaderFloat16", js v)
        member x.PipelineStatisticsQuery
            with get() : bool = h.GetObjectProperty("pipelineStatisticsQuery") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("pipelineStatisticsQuery", js v)
        member x.TimestampQuery
            with get() : bool = h.GetObjectProperty("timestampQuery") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("timestampQuery", js v)
        member x.MultiPlanarFormats
            with get() : bool = h.GetObjectProperty("multiPlanarFormats") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("multiPlanarFormats", js v)
        member x.DepthClamping
            with get() : bool = h.GetObjectProperty("depthClamping") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("depthClamping", js v)
        member x.InvalidExtension
            with get() : bool = h.GetObjectProperty("invalidExtension") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("invalidExtension", js v)
        member x.InvalidFeature
            with get() : bool = h.GetObjectProperty("invalidFeature") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("invalidFeature", js v)
        member x.DawnInternalUsages
            with get() : bool = h.GetObjectProperty("dawnInternalUsages") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("dawnInternalUsages", js v)
        member x.Limits
            with get() : DawnRaw.WGPUSupportedLimits = h.GetObjectProperty("limits") |> convert<DawnRaw.WGPUSupportedLimits>
            and set (v : DawnRaw.WGPUSupportedLimits) = if not (isNull v) then h.SetObjectProperty("limits", js v)
    [<AllowNullLiteral>]
    type WGPURequestAdapterOptions(h : JSObject) =
        inherit JsObj(h)
        new() = WGPURequestAdapterOptions(new JSObject())
        member x.CompatibleSurface
            with get() : SurfaceHandle = h.GetObjectProperty("compatibleSurface") |> convert<SurfaceHandle>
            and set (v : SurfaceHandle) = if not (isNull v) then h.SetObjectProperty("compatibleSurface", js v)
        member x.PowerPreference
            with get() : obj = h.GetObjectProperty("powerPreference") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("powerPreference", v)
        member x.ForceFallbackAdapter
            with get() : bool = h.GetObjectProperty("forceFallbackAdapter") |> convert<bool>
            and set (v : bool) = h.SetObjectProperty("forceFallbackAdapter", js v)
    [<AllowNullLiteral>]
    type WGPUVertexBufferLayout(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUVertexBufferLayout(new JSObject())
        member x.ArrayStride
            with get() : float = h.GetObjectProperty("arrayStride") |> convert<float>
            and set (v : float) = h.SetObjectProperty("arrayStride", js v)
        member x.StepMode
            with get() : obj = h.GetObjectProperty("stepMode") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("stepMode", v)
        member x.Attributes
            with get() : JSObject = h.GetObjectProperty("attributes") |> convert<JSObject>
            and set (v : JSObject) = h.SetObjectProperty("attributes", js v)
    [<AllowNullLiteral>]
    type WGPUBindGroupEntry(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUBindGroupEntry(new JSObject())
        member x.Binding
            with get() : uint32 = h.GetObjectProperty("binding") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("binding", js v)
        member x.Buffer
            with get() : BufferHandle = h.GetObjectProperty("buffer") |> convert<BufferHandle>
            and set (v : BufferHandle) = if not (isNull v) then h.SetObjectProperty("buffer", js v)
        member x.Offset
            with get() : float = h.GetObjectProperty("offset") |> convert<float>
            and set (v : float) = h.SetObjectProperty("offset", js v)
        member x.Size
            with get() : float = h.GetObjectProperty("size") |> convert<float>
            and set (v : float) = h.SetObjectProperty("size", js v)
        member x.Sampler
            with get() : SamplerHandle = h.GetObjectProperty("sampler") |> convert<SamplerHandle>
            and set (v : SamplerHandle) = if not (isNull v) then h.SetObjectProperty("sampler", js v)
        member x.TextureView
            with get() : TextureViewHandle = h.GetObjectProperty("textureView") |> convert<TextureViewHandle>
            and set (v : TextureViewHandle) = if not (isNull v) then h.SetObjectProperty("textureView", js v)
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
    type WGPUColorTargetState(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUColorTargetState(new JSObject())
        member x.Format
            with get() : obj = h.GetObjectProperty("format") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("format", v)
        member x.Blend
            with get() : DawnRaw.WGPUBlendState = h.GetObjectProperty("blend") |> convert<DawnRaw.WGPUBlendState>
            and set (v : DawnRaw.WGPUBlendState) = if not (isNull v) then h.SetObjectProperty("blend", js v)
        member x.WriteMask
            with get() : int = h.GetObjectProperty("writeMask") |> convert<int>
            and set (v : int) = h.SetObjectProperty("writeMask", v)
    [<AllowNullLiteral>]
    type WGPUExternalTextureBindingEntry(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUExternalTextureBindingEntry(new JSObject())
        member x.ExternalTexture
            with get() : ExternalTextureHandle = h.GetObjectProperty("externalTexture") |> convert<ExternalTextureHandle>
            and set (v : ExternalTextureHandle) = if not (isNull v) then h.SetObjectProperty("externalTexture", js v)
    [<AllowNullLiteral>]
    type WGPUImageCopyBuffer(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUImageCopyBuffer(new JSObject())
        member x.Offset
            with get() : float = h.GetObjectProperty("offset") |> convert<float>
            and set (v : float) = h.SetObjectProperty("offset", js v)
        member x.BytesPerRow
            with get() : uint32 = h.GetObjectProperty("bytesPerRow") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("bytesPerRow", js v)
        member x.RowsPerImage
            with get() : uint32 = h.GetObjectProperty("rowsPerImage") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("rowsPerImage", js v)
        member x.Buffer
            with get() : BufferHandle = h.GetObjectProperty("buffer") |> convert<BufferHandle>
            and set (v : BufferHandle) = if not (isNull v) then h.SetObjectProperty("buffer", js v)
    [<AllowNullLiteral>]
    type WGPUImageCopyTexture(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUImageCopyTexture(new JSObject())
        member x.Texture
            with get() : TextureHandle = h.GetObjectProperty("texture") |> convert<TextureHandle>
            and set (v : TextureHandle) = if not (isNull v) then h.SetObjectProperty("texture", js v)
        member x.MipLevel
            with get() : uint32 = h.GetObjectProperty("mipLevel") |> convert<uint32>
            and set (v : uint32) = h.SetObjectProperty("mipLevel", js v)
        member x.Origin
            with get() : DawnRaw.WGPUOrigin3D = h.GetObjectProperty("origin") |> convert<DawnRaw.WGPUOrigin3D>
            and set (v : DawnRaw.WGPUOrigin3D) = if not (isNull v) then h.SetObjectProperty("origin", js v)
        member x.Aspect
            with get() : obj = h.GetObjectProperty("aspect") |> convert<obj>
            and set (v : obj) = h.SetObjectProperty("aspect", v)
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
            with get() : DawnRaw.WGPURenderPassDepthStencilAttachment = h.GetObjectProperty("depthStencilAttachment") |> convert<DawnRaw.WGPURenderPassDepthStencilAttachment>
            and set (v : DawnRaw.WGPURenderPassDepthStencilAttachment) = if not (isNull v) then h.SetObjectProperty("depthStencilAttachment", js v)
        member x.OcclusionQuerySet
            with get() : QuerySetHandle = h.GetObjectProperty("occlusionQuerySet") |> convert<QuerySetHandle>
            and set (v : QuerySetHandle) = if not (isNull v) then h.SetObjectProperty("occlusionQuerySet", js v)
    [<AllowNullLiteral>]
    type WGPUBindGroupDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUBindGroupDescriptor(new JSObject())
        member x.Label
            with get() : string = h.GetObjectProperty("label") |> convert<string>
            and set (v : string) = h.SetObjectProperty("label", js v)
        member x.Layout
            with get() : BindGroupLayoutHandle = h.GetObjectProperty("layout") |> convert<BindGroupLayoutHandle>
            and set (v : BindGroupLayoutHandle) = if not (isNull v) then h.SetObjectProperty("layout", js v)
        member x.Entries
            with get() : JSObject = h.GetObjectProperty("entries") |> convert<JSObject>
            and set (v : JSObject) = h.SetObjectProperty("entries", js v)
    [<AllowNullLiteral>]
    type WGPUFragmentState(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUFragmentState(new JSObject())
        member x.Module
            with get() : ShaderModuleHandle = h.GetObjectProperty("module") |> convert<ShaderModuleHandle>
            and set (v : ShaderModuleHandle) = if not (isNull v) then h.SetObjectProperty("module", js v)
        member x.EntryPoint
            with get() : string = h.GetObjectProperty("entryPoint") |> convert<string>
            and set (v : string) = h.SetObjectProperty("entryPoint", js v)
        member x.Constants
            with get() : JSObject = h.GetObjectProperty("constants") |> convert<JSObject>
            and set (v : JSObject) = h.SetObjectProperty("constants", js v)
        member x.Targets
            with get() : JSObject = h.GetObjectProperty("targets") |> convert<JSObject>
            and set (v : JSObject) = h.SetObjectProperty("targets", js v)
    [<AllowNullLiteral>]
    type WGPUProgrammableStageDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUProgrammableStageDescriptor(new JSObject())
        member x.Module
            with get() : ShaderModuleHandle = h.GetObjectProperty("module") |> convert<ShaderModuleHandle>
            and set (v : ShaderModuleHandle) = if not (isNull v) then h.SetObjectProperty("module", js v)
        member x.EntryPoint
            with get() : string = h.GetObjectProperty("entryPoint") |> convert<string>
            and set (v : string) = h.SetObjectProperty("entryPoint", js v)
        member x.Constants
            with get() : JSObject = h.GetObjectProperty("constants") |> convert<JSObject>
            and set (v : JSObject) = h.SetObjectProperty("constants", js v)
    [<AllowNullLiteral>]
    type WGPUVertexState(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUVertexState(new JSObject())
        member x.Module
            with get() : ShaderModuleHandle = h.GetObjectProperty("module") |> convert<ShaderModuleHandle>
            and set (v : ShaderModuleHandle) = if not (isNull v) then h.SetObjectProperty("module", js v)
        member x.EntryPoint
            with get() : string = h.GetObjectProperty("entryPoint") |> convert<string>
            and set (v : string) = h.SetObjectProperty("entryPoint", js v)
        member x.Constants
            with get() : JSObject = h.GetObjectProperty("constants") |> convert<JSObject>
            and set (v : JSObject) = h.SetObjectProperty("constants", js v)
        member x.Buffers
            with get() : JSObject = h.GetObjectProperty("buffers") |> convert<JSObject>
            and set (v : JSObject) = h.SetObjectProperty("buffers", js v)
    [<AllowNullLiteral>]
    type WGPUComputePipelineDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPUComputePipelineDescriptor(new JSObject())
        member x.Label
            with get() : string = h.GetObjectProperty("label") |> convert<string>
            and set (v : string) = h.SetObjectProperty("label", js v)
        member x.Layout
            with get() : PipelineLayoutHandle = h.GetObjectProperty("layout") |> convert<PipelineLayoutHandle>
            and set (v : PipelineLayoutHandle) = if not (isNull v) then h.SetObjectProperty("layout", js v)
        member x.Compute
            with get() : DawnRaw.WGPUProgrammableStageDescriptor = h.GetObjectProperty("compute") |> convert<DawnRaw.WGPUProgrammableStageDescriptor>
            and set (v : DawnRaw.WGPUProgrammableStageDescriptor) = if not (isNull v) then h.SetObjectProperty("compute", js v)
    [<AllowNullLiteral>]
    type WGPURenderPipelineDescriptor(h : JSObject) =
        inherit JsObj(h)
        new() = WGPURenderPipelineDescriptor(new JSObject())
        member x.Label
            with get() : string = h.GetObjectProperty("label") |> convert<string>
            and set (v : string) = h.SetObjectProperty("label", js v)
        member x.Layout
            with get() : PipelineLayoutHandle = h.GetObjectProperty("layout") |> convert<PipelineLayoutHandle>
            and set (v : PipelineLayoutHandle) = if not (isNull v) then h.SetObjectProperty("layout", js v)
        member x.Vertex
            with get() : DawnRaw.WGPUVertexState = h.GetObjectProperty("vertex") |> convert<DawnRaw.WGPUVertexState>
            and set (v : DawnRaw.WGPUVertexState) = if not (isNull v) then h.SetObjectProperty("vertex", js v)
        member x.Primitive
            with get() : DawnRaw.WGPUPrimitiveState = h.GetObjectProperty("primitive") |> convert<DawnRaw.WGPUPrimitiveState>
            and set (v : DawnRaw.WGPUPrimitiveState) = if not (isNull v) then h.SetObjectProperty("primitive", js v)
        member x.DepthStencil
            with get() : DawnRaw.WGPUDepthStencilState = h.GetObjectProperty("depthStencil") |> convert<DawnRaw.WGPUDepthStencilState>
            and set (v : DawnRaw.WGPUDepthStencilState) = if not (isNull v) then h.SetObjectProperty("depthStencil", js v)
        member x.Multisample
            with get() : DawnRaw.WGPUMultisampleState = h.GetObjectProperty("multisample") |> convert<DawnRaw.WGPUMultisampleState>
            and set (v : DawnRaw.WGPUMultisampleState) = if not (isNull v) then h.SetObjectProperty("multisample", js v)
        member x.Fragment
            with get() : DawnRaw.WGPUFragmentState = h.GetObjectProperty("fragment") |> convert<DawnRaw.WGPUFragmentState>
            and set (v : DawnRaw.WGPUFragmentState) = if not (isNull v) then h.SetObjectProperty("fragment", js v)


type Color =
    {
        R : float
        G : float
        B : float
        A : float
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUColor -> 'a) : 'a = 
        let x = x
        let _R_float = (x.R)
        let _G_float = (x.G)
        let _B_float = (x.B)
        let _A_float = (x.A)
        let native = DawnRaw.WGPUColor()
        native.R <- _R_float
        native.G <- _G_float
        native.B <- _B_float
        native.A <- _A_float
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
        let _Label_string = x.Label
        let native = DawnRaw.WGPUCommandBufferDescriptor()
        native.Label <- _Label_string
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
        let _Label_string = x.Label
        let native = DawnRaw.WGPUCommandEncoderDescriptor()
        native.Label <- _Label_string
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
        let _Label_string = x.Label
        let native = DawnRaw.WGPUComputePassDescriptor()
        native.Label <- _Label_string
        callback native
type ConstantEntry =
    {
        Key : string
        Value : float
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUConstantEntry -> 'a) : 'a = 
        let x = x
        let _Key_string = x.Key
        let _Value_float = (x.Value)
        let native = DawnRaw.WGPUConstantEntry()
        native.Key <- _Key_string
        native.Value <- _Value_float
        callback native
type Extent3D =
    {
        Width : int
        Height : int
        DepthOrArrayLayers : int
    }
    static member Default(Width: int) : Extent3D =
        {
            Width = Width
            Height = 1
            DepthOrArrayLayers = 1
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUExtent3D -> 'a) : 'a = 
        let x = x
        let _Width_int = uint32 (x.Width)
        let _Height_int = uint32 (x.Height)
        let _DepthOrArrayLayers_int = uint32 (x.DepthOrArrayLayers)
        let native = DawnRaw.WGPUExtent3D()
        native.Width <- _Width_int
        native.Height <- _Height_int
        native.DepthOrArrayLayers <- _DepthOrArrayLayers_int
        callback native
type ExternalTextureBindingLayout(h : JSObject) = 
    inherit JsObj(h)
    member inline internal x.Pin<'a>(callback : DawnRaw.WGPUExternalTextureBindingLayout -> 'a) : 'a = 
        let native = DawnRaw.WGPUExternalTextureBindingLayout()
        callback native
type InstanceDescriptor(h : JSObject) = 
    inherit JsObj(h)
    member inline internal x.Pin<'a>(callback : DawnRaw.WGPUInstanceDescriptor -> 'a) : 'a = 
        let native = DawnRaw.WGPUInstanceDescriptor()
        callback native
type Limits =
    {
        MaxTextureDimension1D : int
        MaxTextureDimension2D : int
        MaxTextureDimension3D : int
        MaxTextureArrayLayers : int
        MaxBindGroups : int
        MaxDynamicUniformBuffersPerPipelineLayout : int
        MaxDynamicStorageBuffersPerPipelineLayout : int
        MaxSampledTexturesPerShaderStage : int
        MaxSamplersPerShaderStage : int
        MaxStorageBuffersPerShaderStage : int
        MaxStorageTexturesPerShaderStage : int
        MaxUniformBuffersPerShaderStage : int
        MaxUniformBufferBindingSize : uint64
        MaxStorageBufferBindingSize : uint64
        MinUniformBufferOffsetAlignment : int
        MinStorageBufferOffsetAlignment : int
        MaxVertexBuffers : int
        MaxVertexAttributes : int
        MaxVertexBufferArrayStride : int
        MaxInterStageShaderComponents : int
        MaxComputeWorkgroupStorageSize : int
        MaxComputeInvocationsPerWorkgroup : int
        MaxComputeWorkgroupSizeX : int
        MaxComputeWorkgroupSizeY : int
        MaxComputeWorkgroupSizeZ : int
        MaxComputeWorkgroupsPerDimension : int
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPULimits -> 'a) : 'a = 
        let x = x
        let _MaxTextureDimension1D_int = uint32 (x.MaxTextureDimension1D)
        let _MaxTextureDimension2D_int = uint32 (x.MaxTextureDimension2D)
        let _MaxTextureDimension3D_int = uint32 (x.MaxTextureDimension3D)
        let _MaxTextureArrayLayers_int = uint32 (x.MaxTextureArrayLayers)
        let _MaxBindGroups_int = uint32 (x.MaxBindGroups)
        let _MaxDynamicUniformBuffersPerPipelineLayout_int = uint32 (x.MaxDynamicUniformBuffersPerPipelineLayout)
        let _MaxDynamicStorageBuffersPerPipelineLayout_int = uint32 (x.MaxDynamicStorageBuffersPerPipelineLayout)
        let _MaxSampledTexturesPerShaderStage_int = uint32 (x.MaxSampledTexturesPerShaderStage)
        let _MaxSamplersPerShaderStage_int = uint32 (x.MaxSamplersPerShaderStage)
        let _MaxStorageBuffersPerShaderStage_int = uint32 (x.MaxStorageBuffersPerShaderStage)
        let _MaxStorageTexturesPerShaderStage_int = uint32 (x.MaxStorageTexturesPerShaderStage)
        let _MaxUniformBuffersPerShaderStage_int = uint32 (x.MaxUniformBuffersPerShaderStage)
        let _MaxUniformBufferBindingSize_uint64 = float (x.MaxUniformBufferBindingSize)
        let _MaxStorageBufferBindingSize_uint64 = float (x.MaxStorageBufferBindingSize)
        let _MinUniformBufferOffsetAlignment_int = uint32 (x.MinUniformBufferOffsetAlignment)
        let _MinStorageBufferOffsetAlignment_int = uint32 (x.MinStorageBufferOffsetAlignment)
        let _MaxVertexBuffers_int = uint32 (x.MaxVertexBuffers)
        let _MaxVertexAttributes_int = uint32 (x.MaxVertexAttributes)
        let _MaxVertexBufferArrayStride_int = uint32 (x.MaxVertexBufferArrayStride)
        let _MaxInterStageShaderComponents_int = uint32 (x.MaxInterStageShaderComponents)
        let _MaxComputeWorkgroupStorageSize_int = uint32 (x.MaxComputeWorkgroupStorageSize)
        let _MaxComputeInvocationsPerWorkgroup_int = uint32 (x.MaxComputeInvocationsPerWorkgroup)
        let _MaxComputeWorkgroupSizeX_int = uint32 (x.MaxComputeWorkgroupSizeX)
        let _MaxComputeWorkgroupSizeY_int = uint32 (x.MaxComputeWorkgroupSizeY)
        let _MaxComputeWorkgroupSizeZ_int = uint32 (x.MaxComputeWorkgroupSizeZ)
        let _MaxComputeWorkgroupsPerDimension_int = uint32 (x.MaxComputeWorkgroupsPerDimension)
        let native = DawnRaw.WGPULimits()
        native.MaxTextureDimension1D <- _MaxTextureDimension1D_int
        native.MaxTextureDimension2D <- _MaxTextureDimension2D_int
        native.MaxTextureDimension3D <- _MaxTextureDimension3D_int
        native.MaxTextureArrayLayers <- _MaxTextureArrayLayers_int
        native.MaxBindGroups <- _MaxBindGroups_int
        native.MaxDynamicUniformBuffersPerPipelineLayout <- _MaxDynamicUniformBuffersPerPipelineLayout_int
        native.MaxDynamicStorageBuffersPerPipelineLayout <- _MaxDynamicStorageBuffersPerPipelineLayout_int
        native.MaxSampledTexturesPerShaderStage <- _MaxSampledTexturesPerShaderStage_int
        native.MaxSamplersPerShaderStage <- _MaxSamplersPerShaderStage_int
        native.MaxStorageBuffersPerShaderStage <- _MaxStorageBuffersPerShaderStage_int
        native.MaxStorageTexturesPerShaderStage <- _MaxStorageTexturesPerShaderStage_int
        native.MaxUniformBuffersPerShaderStage <- _MaxUniformBuffersPerShaderStage_int
        native.MaxUniformBufferBindingSize <- _MaxUniformBufferBindingSize_uint64
        native.MaxStorageBufferBindingSize <- _MaxStorageBufferBindingSize_uint64
        native.MinUniformBufferOffsetAlignment <- _MinUniformBufferOffsetAlignment_int
        native.MinStorageBufferOffsetAlignment <- _MinStorageBufferOffsetAlignment_int
        native.MaxVertexBuffers <- _MaxVertexBuffers_int
        native.MaxVertexAttributes <- _MaxVertexAttributes_int
        native.MaxVertexBufferArrayStride <- _MaxVertexBufferArrayStride_int
        native.MaxInterStageShaderComponents <- _MaxInterStageShaderComponents_int
        native.MaxComputeWorkgroupStorageSize <- _MaxComputeWorkgroupStorageSize_int
        native.MaxComputeInvocationsPerWorkgroup <- _MaxComputeInvocationsPerWorkgroup_int
        native.MaxComputeWorkgroupSizeX <- _MaxComputeWorkgroupSizeX_int
        native.MaxComputeWorkgroupSizeY <- _MaxComputeWorkgroupSizeY_int
        native.MaxComputeWorkgroupSizeZ <- _MaxComputeWorkgroupSizeZ_int
        native.MaxComputeWorkgroupsPerDimension <- _MaxComputeWorkgroupsPerDimension_int
        callback native
type MultisampleState =
    {
        Count : int
        Mask : int
        AlphaToCoverageEnabled : bool
    }
    static member Default : MultisampleState =
        {
            Count = 1
            Mask = -1
            AlphaToCoverageEnabled = false
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUMultisampleState -> 'a) : 'a = 
        let x = x
        let _Count_int = uint32 (x.Count)
        let _Mask_int = uint32 (x.Mask)
        let _AlphaToCoverageEnabled_bool = x.AlphaToCoverageEnabled
        let native = DawnRaw.WGPUMultisampleState()
        native.Count <- _Count_int
        native.Mask <- _Mask_int
        native.AlphaToCoverageEnabled <- _AlphaToCoverageEnabled_bool
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
        let _X_int = uint32 (x.X)
        let _Y_int = uint32 (x.Y)
        let _Z_int = uint32 (x.Z)
        let native = DawnRaw.WGPUOrigin3D()
        native.X <- _X_int
        native.Y <- _Y_int
        native.Z <- _Z_int
        callback native
type PrimitiveDepthClampingState =
    {
        ClampDepth : bool
    }
    static member Default : PrimitiveDepthClampingState =
        {
            ClampDepth = false
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUPrimitiveDepthClampingState -> 'a) : 'a = 
        let x = x
        let _ClampDepth_bool = x.ClampDepth
        let native = DawnRaw.WGPUPrimitiveDepthClampingState()
        native.ClampDepth <- _ClampDepth_bool
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
        let _Label_string = x.Label
        let native = DawnRaw.WGPURenderBundleDescriptor()
        native.Label <- _Label_string
        callback native
type ShaderModuleSPIRVDescriptor =
    {
        Code : uint32[]
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUShaderModuleSPIRVDescriptor -> 'a) : 'a = 
        let x = x
        let _Code_uint32Arr = if isNull x.Code then null else Uint32Array.op_Implicit(Span(x.Code))
        let _Code_uint32ArrCount = if isNull x.Code then 0 else x.Code.Length
        let native = DawnRaw.WGPUShaderModuleSPIRVDescriptor()
        native.Code <- _Code_uint32Arr
        callback native
type ShaderModuleWGSLDescriptor =
    {
        Source : string
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUShaderModuleWGSLDescriptor -> 'a) : 'a = 
        let x = x
        let _Source_string = x.Source
        let native = DawnRaw.WGPUShaderModuleWGSLDescriptor()
        native.Source <- _Source_string
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
        let _Label_string = x.Label
        let native = DawnRaw.WGPUShaderModuleDescriptor()
        native.Label <- _Label_string
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
        let _Label_string = x.Label
        let native = DawnRaw.WGPUSurfaceDescriptor()
        native.Label <- _Label_string
        callback native
type SurfaceDescriptorFromCanvasHTMLSelector =
    {
        Selector : string
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUSurfaceDescriptorFromCanvasHTMLSelector -> 'a) : 'a = 
        let x = x
        let _Selector_string = x.Selector
        let native = DawnRaw.WGPUSurfaceDescriptorFromCanvasHTMLSelector()
        native.Selector <- _Selector_string
        callback native
type SurfaceDescriptorFromMetalLayer =
    {
        Layer : nativeint
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUSurfaceDescriptorFromMetalLayer -> 'a) : 'a = 
        let x = x
        let _Layer_nativeint = float (x.Layer)
        let native = DawnRaw.WGPUSurfaceDescriptorFromMetalLayer()
        native.Layer <- _Layer_nativeint
        callback native
type SurfaceDescriptorFromWindowsHWND =
    {
        Hinstance : nativeint
        Hwnd : nativeint
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUSurfaceDescriptorFromWindowsHWND -> 'a) : 'a = 
        let x = x
        let _Hinstance_nativeint = float (x.Hinstance)
        let _Hwnd_nativeint = float (x.Hwnd)
        let native = DawnRaw.WGPUSurfaceDescriptorFromWindowsHWND()
        native.Hinstance <- _Hinstance_nativeint
        native.Hwnd <- _Hwnd_nativeint
        callback native
type SurfaceDescriptorFromWindowsCoreWindow =
    {
        CoreWindow : nativeint
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUSurfaceDescriptorFromWindowsCoreWindow -> 'a) : 'a = 
        let x = x
        let _CoreWindow_nativeint = float (x.CoreWindow)
        let native = DawnRaw.WGPUSurfaceDescriptorFromWindowsCoreWindow()
        native.CoreWindow <- _CoreWindow_nativeint
        callback native
type SurfaceDescriptorFromWindowsSwapChainPanel =
    {
        SwapChainPanel : nativeint
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUSurfaceDescriptorFromWindowsSwapChainPanel -> 'a) : 'a = 
        let x = x
        let _SwapChainPanel_nativeint = float (x.SwapChainPanel)
        let native = DawnRaw.WGPUSurfaceDescriptorFromWindowsSwapChainPanel()
        native.SwapChainPanel <- _SwapChainPanel_nativeint
        callback native
type SurfaceDescriptorFromXlib =
    {
        Display : nativeint
        Window : int
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUSurfaceDescriptorFromXlib -> 'a) : 'a = 
        let x = x
        let _Display_nativeint = float (x.Display)
        let _Window_int = uint32 (x.Window)
        let native = DawnRaw.WGPUSurfaceDescriptorFromXlib()
        native.Display <- _Display_nativeint
        native.Window <- _Window_int
        callback native
type TextureDataLayout =
    {
        Offset : uint64
        BytesPerRow : int
        RowsPerImage : int
    }
    static member Default(BytesPerRow: int, RowsPerImage: int) : TextureDataLayout =
        {
            Offset = 0UL
            BytesPerRow = BytesPerRow
            RowsPerImage = RowsPerImage
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUTextureDataLayout -> 'a) : 'a = 
        let x = x
        let _Offset_uint64 = float (x.Offset)
        let _BytesPerRow_int = uint32 (x.BytesPerRow)
        let _RowsPerImage_int = uint32 (x.RowsPerImage)
        let native = DawnRaw.WGPUTextureDataLayout()
        native.Offset <- _Offset_uint64
        native.BytesPerRow <- _BytesPerRow_int
        native.RowsPerImage <- _RowsPerImage_int
        callback native
type AdapterProperties =
    {
        VendorID : int
        DeviceID : int
        Name : string
        DriverDescription : string
        AdapterType : AdapterType
        BackendType : BackendType
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUAdapterProperties -> 'a) : 'a = 
        let x = x
        let _VendorID_int = uint32 (x.VendorID)
        let _DeviceID_int = uint32 (x.DeviceID)
        let _Name_string = x.Name
        let _DriverDescription_string = x.DriverDescription
        let _AdapterType_AdapterType = x.AdapterType.GetValue()
        let _BackendType_BackendType = x.BackendType.GetValue()
        let native = DawnRaw.WGPUAdapterProperties()
        native.VendorID <- _VendorID_int
        native.DeviceID <- _DeviceID_int
        native.Name <- _Name_string
        native.DriverDescription <- _DriverDescription_string
        native.AdapterType <- _AdapterType_AdapterType
        native.BackendType <- _BackendType_BackendType
        callback native
type BlendComponent =
    {
        Operation : BlendOperation
        SrcFactor : BlendFactor
        DstFactor : BlendFactor
    }
    static member Default : BlendComponent =
        {
            Operation = BlendOperation.Add
            SrcFactor = BlendFactor.One
            DstFactor = BlendFactor.Zero
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUBlendComponent -> 'a) : 'a = 
        let x = x
        let _Operation_BlendOperation = x.Operation.GetValue()
        let _SrcFactor_BlendFactor = x.SrcFactor.GetValue()
        let _DstFactor_BlendFactor = x.DstFactor.GetValue()
        let native = DawnRaw.WGPUBlendComponent()
        native.Operation <- _Operation_BlendOperation
        native.SrcFactor <- _SrcFactor_BlendFactor
        native.DstFactor <- _DstFactor_BlendFactor
        callback native
type BufferBindingLayout =
    {
        Type : BufferBindingType
        HasDynamicOffset : bool
        MinBindingSize : uint64
    }
    static member Default : BufferBindingLayout =
        {
            Type = BufferBindingType.Undefined
            HasDynamicOffset = false
            MinBindingSize = 0UL
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUBufferBindingLayout -> 'a) : 'a = 
        let x = x
        let _Type_BufferBindingType = x.Type.GetValue()
        let _HasDynamicOffset_bool = x.HasDynamicOffset
        let _MinBindingSize_uint64 = float (x.MinBindingSize)
        let native = DawnRaw.WGPUBufferBindingLayout()
        native.Type <- _Type_BufferBindingType
        native.HasDynamicOffset <- _HasDynamicOffset_bool
        native.MinBindingSize <- _MinBindingSize_uint64
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
        let _Label_string = x.Label
        let _Usage_BufferUsage = int (x.Usage)
        let _Size_uint64 = float (x.Size)
        let _MappedAtCreation_bool = x.MappedAtCreation
        let native = DawnRaw.WGPUBufferDescriptor()
        native.Label <- _Label_string
        native.Usage <- _Usage_BufferUsage
        native.Size <- _Size_uint64
        native.MappedAtCreation <- _MappedAtCreation_bool
        callback native
type CompilationMessage =
    {
        Message : string
        Type : CompilationMessageType
        LineNum : uint64
        LinePos : uint64
        Offset : uint64
        Length : uint64
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUCompilationMessage -> 'a) : 'a = 
        let x = x
        let _Message_string = x.Message
        let _Type_CompilationMessageType = x.Type.GetValue()
        let _LineNum_uint64 = float (x.LineNum)
        let _LinePos_uint64 = float (x.LinePos)
        let _Offset_uint64 = float (x.Offset)
        let _Length_uint64 = float (x.Length)
        let native = DawnRaw.WGPUCompilationMessage()
        native.Message <- _Message_string
        native.Type <- _Type_CompilationMessageType
        native.LineNum <- _LineNum_uint64
        native.LinePos <- _LinePos_uint64
        native.Offset <- _Offset_uint64
        native.Length <- _Length_uint64
        callback native
type CopyTextureForBrowserOptions =
    {
        FlipY : bool
        AlphaOp : AlphaOp
    }
    static member Default : CopyTextureForBrowserOptions =
        {
            FlipY = false
            AlphaOp = AlphaOp.DontChange
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUCopyTextureForBrowserOptions -> 'a) : 'a = 
        let x = x
        let _FlipY_bool = x.FlipY
        let _AlphaOp_AlphaOp = x.AlphaOp.GetValue()
        let native = DawnRaw.WGPUCopyTextureForBrowserOptions()
        native.FlipY <- _FlipY_bool
        native.AlphaOp <- _AlphaOp_AlphaOp
        callback native
type DawnTextureInternalUsageDescriptor =
    {
        InternalUsage : TextureUsage
    }
    static member Default : DawnTextureInternalUsageDescriptor =
        {
            InternalUsage = TextureUsage.None
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUDawnTextureInternalUsageDescriptor -> 'a) : 'a = 
        let x = x
        let _InternalUsage_TextureUsage = int (x.InternalUsage)
        let native = DawnRaw.WGPUDawnTextureInternalUsageDescriptor()
        native.InternalUsage <- _InternalUsage_TextureUsage
        callback native
type ExternalTextureDescriptor =
    {
        Label : string
        Plane0 : TextureView
        Format : TextureFormat
    }
    static member Default(Plane0: TextureView, Format: TextureFormat) : ExternalTextureDescriptor =
        {
            Label = null
            Plane0 = Plane0
            Format = Format
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUExternalTextureDescriptor -> 'a) : 'a = 
        let x = x
        let _Label_string = x.Label
        let _Plane0_TextureView = (if isNull x.Plane0 then null else x.Plane0.Handle)
        let _Format_TextureFormat = x.Format.GetValue()
        let native = DawnRaw.WGPUExternalTextureDescriptor()
        native.Label <- _Label_string
        native.Plane0 <- _Plane0_TextureView
        native.Format <- _Format_TextureFormat
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
        let _Label_string = x.Label
        let _BindGroupLayouts_BindGroupLayoutArrCount = x.BindGroupLayouts.Length
        let _BindGroupLayouts_BindGroupLayoutArrArray = newArray _BindGroupLayouts_BindGroupLayoutArrCount
        for i in 0 .. _BindGroupLayouts_BindGroupLayoutArrCount-1 do
            if isNull x.BindGroupLayouts.[i] then _BindGroupLayouts_BindGroupLayoutArrArray.[i] <- null
            else _BindGroupLayouts_BindGroupLayoutArrArray.[i] <- x.BindGroupLayouts.[i].Handle
        let _BindGroupLayouts_BindGroupLayoutArr = _BindGroupLayouts_BindGroupLayoutArrArray.Reference
        let native = DawnRaw.WGPUPipelineLayoutDescriptor()
        native.Label <- _Label_string
        native.BindGroupLayouts <- _BindGroupLayouts_BindGroupLayoutArr
        callback native
type PrimitiveState =
    {
        Topology : PrimitiveTopology
        StripIndexFormat : IndexFormat
        FrontFace : FrontFace
        CullMode : CullMode
    }
    static member Default : PrimitiveState =
        {
            Topology = PrimitiveTopology.TriangleList
            StripIndexFormat = IndexFormat.Undefined
            FrontFace = FrontFace.CCW
            CullMode = CullMode.None
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUPrimitiveState -> 'a) : 'a = 
        let x = x
        let _Topology_PrimitiveTopology = x.Topology.GetValue()
        let _StripIndexFormat_IndexFormat = x.StripIndexFormat.GetValue()
        let _FrontFace_FrontFace = x.FrontFace.GetValue()
        let _CullMode_CullMode = x.CullMode.GetValue()
        let native = DawnRaw.WGPUPrimitiveState()
        native.Topology <- _Topology_PrimitiveTopology
        native.StripIndexFormat <- _StripIndexFormat_IndexFormat
        native.FrontFace <- _FrontFace_FrontFace
        native.CullMode <- _CullMode_CullMode
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
        let _Label_string = x.Label
        let _Type_QueryType = x.Type.GetValue()
        let _Count_int = uint32 (x.Count)
        let inline _PipelineStatistics_PipelineStatisticNameOptCont _PipelineStatistics_PipelineStatisticNameOpt =
            let _PipelineStatisticsCount_int = uint32 (x.PipelineStatisticsCount)
            let native = DawnRaw.WGPUQuerySetDescriptor()
            native.Label <- _Label_string
            native.Type <- _Type_QueryType
            native.Count <- _Count_int
            native.PipelineStatistics <- _PipelineStatistics_PipelineStatisticNameOpt
            native.PipelineStatisticsCount <- _PipelineStatisticsCount_int
            callback native
        match x.PipelineStatistics with
        | Some o ->
            _PipelineStatistics_PipelineStatisticNameOptCont(o.GetValue())
        | _ ->
            _PipelineStatistics_PipelineStatisticNameOptCont null
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
        let _Label_string = x.Label
        let _ColorFormats_TextureFormatArrCount = x.ColorFormats.Length
        let _ColorFormats_TextureFormatArrArray = newArray (_ColorFormats_TextureFormatArrCount)
        for i in 0 .. _ColorFormats_TextureFormatArrCount-1 do
            _ColorFormats_TextureFormatArrArray.[i] <- x.ColorFormats.[i].GetValue()
        let _ColorFormats_TextureFormatArr = _ColorFormats_TextureFormatArrArray.Reference
        let _DepthStencilFormat_TextureFormat = x.DepthStencilFormat.GetValue()
        let _SampleCount_int = uint32 (x.SampleCount)
        let native = DawnRaw.WGPURenderBundleEncoderDescriptor()
        native.Label <- _Label_string
        native.ColorFormats <- _ColorFormats_TextureFormatArr
        native.DepthStencilFormat <- _DepthStencilFormat_TextureFormat
        native.SampleCount <- _SampleCount_int
        callback native
type RenderPassColorAttachment =
    {
        View : TextureView
        ResolveTarget : TextureView
        LoadOp : LoadOp
        StoreOp : StoreOp
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPURenderPassColorAttachment -> 'a) : 'a = 
        let x = x
        let _View_TextureView = (if isNull x.View then null else x.View.Handle)
        let _ResolveTarget_TextureView = (if isNull x.ResolveTarget then null else x.ResolveTarget.Handle)
        let _LoadOp_LoadOp = x.LoadOp.GetValue()
        let _StoreOp_StoreOp = x.StoreOp.GetValue()
        let native = DawnRaw.WGPURenderPassColorAttachment()
        native.View <- _View_TextureView
        native.ResolveTarget <- _ResolveTarget_TextureView
        native.LoadOp <- _LoadOp_LoadOp
        native.StoreOp <- _StoreOp_StoreOp
        callback native
type RenderPassDepthStencilAttachment =
    {
        View : TextureView
        DepthLoadOp : LoadOp
        DepthStoreOp : StoreOp
        ClearDepth : float32
        DepthReadOnly : bool
        StencilLoadOp : LoadOp
        StencilStoreOp : StoreOp
        ClearStencil : int
        StencilReadOnly : bool
    }
    static member Default(View: TextureView, DepthLoadOp: LoadOp, DepthStoreOp: StoreOp, ClearDepth: float32, StencilLoadOp: LoadOp, StencilStoreOp: StoreOp) : RenderPassDepthStencilAttachment =
        {
            View = View
            DepthLoadOp = DepthLoadOp
            DepthStoreOp = DepthStoreOp
            ClearDepth = ClearDepth
            DepthReadOnly = false
            StencilLoadOp = StencilLoadOp
            StencilStoreOp = StencilStoreOp
            ClearStencil = 0
            StencilReadOnly = false
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPURenderPassDepthStencilAttachment -> 'a) : 'a = 
        let x = x
        let _View_TextureView = (if isNull x.View then null else x.View.Handle)
        let _DepthLoadOp_LoadOp = x.DepthLoadOp.GetValue()
        let _DepthStoreOp_StoreOp = x.DepthStoreOp.GetValue()
        let _ClearDepth_float32 = (x.ClearDepth)
        let _DepthReadOnly_bool = x.DepthReadOnly
        let _StencilLoadOp_LoadOp = x.StencilLoadOp.GetValue()
        let _StencilStoreOp_StoreOp = x.StencilStoreOp.GetValue()
        let _ClearStencil_int = uint32 (x.ClearStencil)
        let _StencilReadOnly_bool = x.StencilReadOnly
        let native = DawnRaw.WGPURenderPassDepthStencilAttachment()
        native.View <- _View_TextureView
        native.DepthLoadOp <- _DepthLoadOp_LoadOp
        native.DepthStoreOp <- _DepthStoreOp_StoreOp
        native.ClearDepth <- _ClearDepth_float32
        native.DepthReadOnly <- _DepthReadOnly_bool
        native.StencilLoadOp <- _StencilLoadOp_LoadOp
        native.StencilStoreOp <- _StencilStoreOp_StoreOp
        native.ClearStencil <- _ClearStencil_int
        native.StencilReadOnly <- _StencilReadOnly_bool
        callback native
type RequiredLimits =
    {
        Limits : Limits
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPURequiredLimits -> 'a) : 'a = 
        let x = x
        let _MaxTextureDimension1D_int = uint32 (x.Limits.MaxTextureDimension1D)
        let _MaxTextureDimension2D_int = uint32 (x.Limits.MaxTextureDimension2D)
        let _MaxTextureDimension3D_int = uint32 (x.Limits.MaxTextureDimension3D)
        let _MaxTextureArrayLayers_int = uint32 (x.Limits.MaxTextureArrayLayers)
        let _MaxBindGroups_int = uint32 (x.Limits.MaxBindGroups)
        let _MaxDynamicUniformBuffersPerPipelineLayout_int = uint32 (x.Limits.MaxDynamicUniformBuffersPerPipelineLayout)
        let _MaxDynamicStorageBuffersPerPipelineLayout_int = uint32 (x.Limits.MaxDynamicStorageBuffersPerPipelineLayout)
        let _MaxSampledTexturesPerShaderStage_int = uint32 (x.Limits.MaxSampledTexturesPerShaderStage)
        let _MaxSamplersPerShaderStage_int = uint32 (x.Limits.MaxSamplersPerShaderStage)
        let _MaxStorageBuffersPerShaderStage_int = uint32 (x.Limits.MaxStorageBuffersPerShaderStage)
        let _MaxStorageTexturesPerShaderStage_int = uint32 (x.Limits.MaxStorageTexturesPerShaderStage)
        let _MaxUniformBuffersPerShaderStage_int = uint32 (x.Limits.MaxUniformBuffersPerShaderStage)
        let _MaxUniformBufferBindingSize_uint64 = float (x.Limits.MaxUniformBufferBindingSize)
        let _MaxStorageBufferBindingSize_uint64 = float (x.Limits.MaxStorageBufferBindingSize)
        let _MinUniformBufferOffsetAlignment_int = uint32 (x.Limits.MinUniformBufferOffsetAlignment)
        let _MinStorageBufferOffsetAlignment_int = uint32 (x.Limits.MinStorageBufferOffsetAlignment)
        let _MaxVertexBuffers_int = uint32 (x.Limits.MaxVertexBuffers)
        let _MaxVertexAttributes_int = uint32 (x.Limits.MaxVertexAttributes)
        let _MaxVertexBufferArrayStride_int = uint32 (x.Limits.MaxVertexBufferArrayStride)
        let _MaxInterStageShaderComponents_int = uint32 (x.Limits.MaxInterStageShaderComponents)
        let _MaxComputeWorkgroupStorageSize_int = uint32 (x.Limits.MaxComputeWorkgroupStorageSize)
        let _MaxComputeInvocationsPerWorkgroup_int = uint32 (x.Limits.MaxComputeInvocationsPerWorkgroup)
        let _MaxComputeWorkgroupSizeX_int = uint32 (x.Limits.MaxComputeWorkgroupSizeX)
        let _MaxComputeWorkgroupSizeY_int = uint32 (x.Limits.MaxComputeWorkgroupSizeY)
        let _MaxComputeWorkgroupSizeZ_int = uint32 (x.Limits.MaxComputeWorkgroupSizeZ)
        let _MaxComputeWorkgroupsPerDimension_int = uint32 (x.Limits.MaxComputeWorkgroupsPerDimension)
        let _Limits_Limits = new DawnRaw.WGPULimits()
        _Limits_Limits.MaxTextureDimension1D <- _MaxTextureDimension1D_int
        _Limits_Limits.MaxTextureDimension2D <- _MaxTextureDimension2D_int
        _Limits_Limits.MaxTextureDimension3D <- _MaxTextureDimension3D_int
        _Limits_Limits.MaxTextureArrayLayers <- _MaxTextureArrayLayers_int
        _Limits_Limits.MaxBindGroups <- _MaxBindGroups_int
        _Limits_Limits.MaxDynamicUniformBuffersPerPipelineLayout <- _MaxDynamicUniformBuffersPerPipelineLayout_int
        _Limits_Limits.MaxDynamicStorageBuffersPerPipelineLayout <- _MaxDynamicStorageBuffersPerPipelineLayout_int
        _Limits_Limits.MaxSampledTexturesPerShaderStage <- _MaxSampledTexturesPerShaderStage_int
        _Limits_Limits.MaxSamplersPerShaderStage <- _MaxSamplersPerShaderStage_int
        _Limits_Limits.MaxStorageBuffersPerShaderStage <- _MaxStorageBuffersPerShaderStage_int
        _Limits_Limits.MaxStorageTexturesPerShaderStage <- _MaxStorageTexturesPerShaderStage_int
        _Limits_Limits.MaxUniformBuffersPerShaderStage <- _MaxUniformBuffersPerShaderStage_int
        _Limits_Limits.MaxUniformBufferBindingSize <- _MaxUniformBufferBindingSize_uint64
        _Limits_Limits.MaxStorageBufferBindingSize <- _MaxStorageBufferBindingSize_uint64
        _Limits_Limits.MinUniformBufferOffsetAlignment <- _MinUniformBufferOffsetAlignment_int
        _Limits_Limits.MinStorageBufferOffsetAlignment <- _MinStorageBufferOffsetAlignment_int
        _Limits_Limits.MaxVertexBuffers <- _MaxVertexBuffers_int
        _Limits_Limits.MaxVertexAttributes <- _MaxVertexAttributes_int
        _Limits_Limits.MaxVertexBufferArrayStride <- _MaxVertexBufferArrayStride_int
        _Limits_Limits.MaxInterStageShaderComponents <- _MaxInterStageShaderComponents_int
        _Limits_Limits.MaxComputeWorkgroupStorageSize <- _MaxComputeWorkgroupStorageSize_int
        _Limits_Limits.MaxComputeInvocationsPerWorkgroup <- _MaxComputeInvocationsPerWorkgroup_int
        _Limits_Limits.MaxComputeWorkgroupSizeX <- _MaxComputeWorkgroupSizeX_int
        _Limits_Limits.MaxComputeWorkgroupSizeY <- _MaxComputeWorkgroupSizeY_int
        _Limits_Limits.MaxComputeWorkgroupSizeZ <- _MaxComputeWorkgroupSizeZ_int
        _Limits_Limits.MaxComputeWorkgroupsPerDimension <- _MaxComputeWorkgroupsPerDimension_int
        let _Limits_Limits = _Limits_Limits
        let native = DawnRaw.WGPURequiredLimits()
        native.Limits <- _Limits_Limits
        callback native
type SamplerBindingLayout =
    {
        Type : SamplerBindingType
    }
    static member Default : SamplerBindingLayout =
        {
            Type = SamplerBindingType.Undefined
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUSamplerBindingLayout -> 'a) : 'a = 
        let x = x
        let _Type_SamplerBindingType = x.Type.GetValue()
        let native = DawnRaw.WGPUSamplerBindingLayout()
        native.Type <- _Type_SamplerBindingType
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
        MaxAnisotropy : uint16
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
            MaxAnisotropy = 1us
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUSamplerDescriptor -> 'a) : 'a = 
        let x = x
        let _Label_string = x.Label
        let _AddressModeU_AddressMode = x.AddressModeU.GetValue()
        let _AddressModeV_AddressMode = x.AddressModeV.GetValue()
        let _AddressModeW_AddressMode = x.AddressModeW.GetValue()
        let _MagFilter_FilterMode = x.MagFilter.GetValue()
        let _MinFilter_FilterMode = x.MinFilter.GetValue()
        let _MipmapFilter_FilterMode = x.MipmapFilter.GetValue()
        let _LodMinClamp_float32 = (x.LodMinClamp)
        let _LodMaxClamp_float32 = (x.LodMaxClamp)
        let _Compare_CompareFunction = x.Compare.GetValue()
        let _MaxAnisotropy_uint16 = uint16 (x.MaxAnisotropy)
        let native = DawnRaw.WGPUSamplerDescriptor()
        native.Label <- _Label_string
        native.AddressModeU <- _AddressModeU_AddressMode
        native.AddressModeV <- _AddressModeV_AddressMode
        native.AddressModeW <- _AddressModeW_AddressMode
        native.MagFilter <- _MagFilter_FilterMode
        native.MinFilter <- _MinFilter_FilterMode
        native.MipmapFilter <- _MipmapFilter_FilterMode
        native.LodMinClamp <- _LodMinClamp_float32
        native.LodMaxClamp <- _LodMaxClamp_float32
        native.Compare <- _Compare_CompareFunction
        native.MaxAnisotropy <- _MaxAnisotropy_uint16
        callback native
type StencilFaceState =
    {
        Compare : CompareFunction
        FailOp : StencilOperation
        DepthFailOp : StencilOperation
        PassOp : StencilOperation
    }
    static member Default : StencilFaceState =
        {
            Compare = CompareFunction.Always
            FailOp = StencilOperation.Keep
            DepthFailOp = StencilOperation.Keep
            PassOp = StencilOperation.Keep
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUStencilFaceState -> 'a) : 'a = 
        let x = x
        let _Compare_CompareFunction = x.Compare.GetValue()
        let _FailOp_StencilOperation = x.FailOp.GetValue()
        let _DepthFailOp_StencilOperation = x.DepthFailOp.GetValue()
        let _PassOp_StencilOperation = x.PassOp.GetValue()
        let native = DawnRaw.WGPUStencilFaceState()
        native.Compare <- _Compare_CompareFunction
        native.FailOp <- _FailOp_StencilOperation
        native.DepthFailOp <- _DepthFailOp_StencilOperation
        native.PassOp <- _PassOp_StencilOperation
        callback native
type StorageTextureBindingLayout =
    {
        Access : StorageTextureAccess
        Format : TextureFormat
        ViewDimension : TextureViewDimension
    }
    static member Default : StorageTextureBindingLayout =
        {
            Access = StorageTextureAccess.Undefined
            Format = TextureFormat.Undefined
            ViewDimension = TextureViewDimension.Undefined
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUStorageTextureBindingLayout -> 'a) : 'a = 
        let x = x
        let _Access_StorageTextureAccess = x.Access.GetValue()
        let _Format_TextureFormat = x.Format.GetValue()
        let _ViewDimension_TextureViewDimension = x.ViewDimension.GetValue()
        let native = DawnRaw.WGPUStorageTextureBindingLayout()
        native.Access <- _Access_StorageTextureAccess
        native.Format <- _Format_TextureFormat
        native.ViewDimension <- _ViewDimension_TextureViewDimension
        callback native
type SupportedLimits =
    {
        Limits : Limits
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUSupportedLimits -> 'a) : 'a = 
        let x = x
        let _MaxTextureDimension1D_int = uint32 (x.Limits.MaxTextureDimension1D)
        let _MaxTextureDimension2D_int = uint32 (x.Limits.MaxTextureDimension2D)
        let _MaxTextureDimension3D_int = uint32 (x.Limits.MaxTextureDimension3D)
        let _MaxTextureArrayLayers_int = uint32 (x.Limits.MaxTextureArrayLayers)
        let _MaxBindGroups_int = uint32 (x.Limits.MaxBindGroups)
        let _MaxDynamicUniformBuffersPerPipelineLayout_int = uint32 (x.Limits.MaxDynamicUniformBuffersPerPipelineLayout)
        let _MaxDynamicStorageBuffersPerPipelineLayout_int = uint32 (x.Limits.MaxDynamicStorageBuffersPerPipelineLayout)
        let _MaxSampledTexturesPerShaderStage_int = uint32 (x.Limits.MaxSampledTexturesPerShaderStage)
        let _MaxSamplersPerShaderStage_int = uint32 (x.Limits.MaxSamplersPerShaderStage)
        let _MaxStorageBuffersPerShaderStage_int = uint32 (x.Limits.MaxStorageBuffersPerShaderStage)
        let _MaxStorageTexturesPerShaderStage_int = uint32 (x.Limits.MaxStorageTexturesPerShaderStage)
        let _MaxUniformBuffersPerShaderStage_int = uint32 (x.Limits.MaxUniformBuffersPerShaderStage)
        let _MaxUniformBufferBindingSize_uint64 = float (x.Limits.MaxUniformBufferBindingSize)
        let _MaxStorageBufferBindingSize_uint64 = float (x.Limits.MaxStorageBufferBindingSize)
        let _MinUniformBufferOffsetAlignment_int = uint32 (x.Limits.MinUniformBufferOffsetAlignment)
        let _MinStorageBufferOffsetAlignment_int = uint32 (x.Limits.MinStorageBufferOffsetAlignment)
        let _MaxVertexBuffers_int = uint32 (x.Limits.MaxVertexBuffers)
        let _MaxVertexAttributes_int = uint32 (x.Limits.MaxVertexAttributes)
        let _MaxVertexBufferArrayStride_int = uint32 (x.Limits.MaxVertexBufferArrayStride)
        let _MaxInterStageShaderComponents_int = uint32 (x.Limits.MaxInterStageShaderComponents)
        let _MaxComputeWorkgroupStorageSize_int = uint32 (x.Limits.MaxComputeWorkgroupStorageSize)
        let _MaxComputeInvocationsPerWorkgroup_int = uint32 (x.Limits.MaxComputeInvocationsPerWorkgroup)
        let _MaxComputeWorkgroupSizeX_int = uint32 (x.Limits.MaxComputeWorkgroupSizeX)
        let _MaxComputeWorkgroupSizeY_int = uint32 (x.Limits.MaxComputeWorkgroupSizeY)
        let _MaxComputeWorkgroupSizeZ_int = uint32 (x.Limits.MaxComputeWorkgroupSizeZ)
        let _MaxComputeWorkgroupsPerDimension_int = uint32 (x.Limits.MaxComputeWorkgroupsPerDimension)
        let _Limits_Limits = new DawnRaw.WGPULimits()
        _Limits_Limits.MaxTextureDimension1D <- _MaxTextureDimension1D_int
        _Limits_Limits.MaxTextureDimension2D <- _MaxTextureDimension2D_int
        _Limits_Limits.MaxTextureDimension3D <- _MaxTextureDimension3D_int
        _Limits_Limits.MaxTextureArrayLayers <- _MaxTextureArrayLayers_int
        _Limits_Limits.MaxBindGroups <- _MaxBindGroups_int
        _Limits_Limits.MaxDynamicUniformBuffersPerPipelineLayout <- _MaxDynamicUniformBuffersPerPipelineLayout_int
        _Limits_Limits.MaxDynamicStorageBuffersPerPipelineLayout <- _MaxDynamicStorageBuffersPerPipelineLayout_int
        _Limits_Limits.MaxSampledTexturesPerShaderStage <- _MaxSampledTexturesPerShaderStage_int
        _Limits_Limits.MaxSamplersPerShaderStage <- _MaxSamplersPerShaderStage_int
        _Limits_Limits.MaxStorageBuffersPerShaderStage <- _MaxStorageBuffersPerShaderStage_int
        _Limits_Limits.MaxStorageTexturesPerShaderStage <- _MaxStorageTexturesPerShaderStage_int
        _Limits_Limits.MaxUniformBuffersPerShaderStage <- _MaxUniformBuffersPerShaderStage_int
        _Limits_Limits.MaxUniformBufferBindingSize <- _MaxUniformBufferBindingSize_uint64
        _Limits_Limits.MaxStorageBufferBindingSize <- _MaxStorageBufferBindingSize_uint64
        _Limits_Limits.MinUniformBufferOffsetAlignment <- _MinUniformBufferOffsetAlignment_int
        _Limits_Limits.MinStorageBufferOffsetAlignment <- _MinStorageBufferOffsetAlignment_int
        _Limits_Limits.MaxVertexBuffers <- _MaxVertexBuffers_int
        _Limits_Limits.MaxVertexAttributes <- _MaxVertexAttributes_int
        _Limits_Limits.MaxVertexBufferArrayStride <- _MaxVertexBufferArrayStride_int
        _Limits_Limits.MaxInterStageShaderComponents <- _MaxInterStageShaderComponents_int
        _Limits_Limits.MaxComputeWorkgroupStorageSize <- _MaxComputeWorkgroupStorageSize_int
        _Limits_Limits.MaxComputeInvocationsPerWorkgroup <- _MaxComputeInvocationsPerWorkgroup_int
        _Limits_Limits.MaxComputeWorkgroupSizeX <- _MaxComputeWorkgroupSizeX_int
        _Limits_Limits.MaxComputeWorkgroupSizeY <- _MaxComputeWorkgroupSizeY_int
        _Limits_Limits.MaxComputeWorkgroupSizeZ <- _MaxComputeWorkgroupSizeZ_int
        _Limits_Limits.MaxComputeWorkgroupsPerDimension <- _MaxComputeWorkgroupsPerDimension_int
        let _Limits_Limits = _Limits_Limits
        let native = DawnRaw.WGPUSupportedLimits()
        native.Limits <- _Limits_Limits
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
        let _Label_string = x.Label
        let _Usage_TextureUsage = int (x.Usage)
        let _Format_TextureFormat = x.Format.GetValue()
        let _Width_int = uint32 (x.Width)
        let _Height_int = uint32 (x.Height)
        let _PresentMode_PresentMode = x.PresentMode.GetValue()
        let _Implementation_uint64 = float (x.Implementation)
        let native = DawnRaw.WGPUSwapChainDescriptor()
        native.Label <- _Label_string
        native.Usage <- _Usage_TextureUsage
        native.Format <- _Format_TextureFormat
        native.Width <- _Width_int
        native.Height <- _Height_int
        native.PresentMode <- _PresentMode_PresentMode
        native.Implementation <- _Implementation_uint64
        callback native
type TextureBindingLayout =
    {
        SampleType : TextureSampleType
        ViewDimension : TextureViewDimension
        Multisampled : bool
    }
    static member Default : TextureBindingLayout =
        {
            SampleType = TextureSampleType.Undefined
            ViewDimension = TextureViewDimension.Undefined
            Multisampled = false
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUTextureBindingLayout -> 'a) : 'a = 
        let x = x
        let _SampleType_TextureSampleType = x.SampleType.GetValue()
        let _ViewDimension_TextureViewDimension = x.ViewDimension.GetValue()
        let _Multisampled_bool = x.Multisampled
        let native = DawnRaw.WGPUTextureBindingLayout()
        native.SampleType <- _SampleType_TextureSampleType
        native.ViewDimension <- _ViewDimension_TextureViewDimension
        native.Multisampled <- _Multisampled_bool
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
    static member Default(Usage: TextureUsage, Size: Extent3D, Format: TextureFormat) : TextureDescriptor =
        {
            Label = null
            Usage = Usage
            Dimension = TextureDimension.D2D
            Size = Size
            Format = Format
            MipLevelCount = 1
            SampleCount = 1
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUTextureDescriptor -> 'a) : 'a = 
        let x = x
        let _Label_string = x.Label
        let _Usage_TextureUsage = int (x.Usage)
        let _Dimension_TextureDimension = x.Dimension.GetValue()
        let _Width_int = uint32 (x.Size.Width)
        let _Height_int = uint32 (x.Size.Height)
        let _DepthOrArrayLayers_int = uint32 (x.Size.DepthOrArrayLayers)
        let _Size_Extent3D = new DawnRaw.WGPUExtent3D()
        _Size_Extent3D.Width <- _Width_int
        _Size_Extent3D.Height <- _Height_int
        _Size_Extent3D.DepthOrArrayLayers <- _DepthOrArrayLayers_int
        let _Size_Extent3D = _Size_Extent3D
        let _Format_TextureFormat = x.Format.GetValue()
        let _MipLevelCount_int = uint32 (x.MipLevelCount)
        let _SampleCount_int = uint32 (x.SampleCount)
        let native = DawnRaw.WGPUTextureDescriptor()
        native.Label <- _Label_string
        native.Usage <- _Usage_TextureUsage
        native.Dimension <- _Dimension_TextureDimension
        native.Size <- _Size_Extent3D
        native.Format <- _Format_TextureFormat
        native.MipLevelCount <- _MipLevelCount_int
        native.SampleCount <- _SampleCount_int
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
    static member Default(MipLevelCount: int, ArrayLayerCount: int) : TextureViewDescriptor =
        {
            Label = null
            Format = TextureFormat.Undefined
            Dimension = TextureViewDimension.Undefined
            BaseMipLevel = 0
            MipLevelCount = MipLevelCount
            BaseArrayLayer = 0
            ArrayLayerCount = ArrayLayerCount
            Aspect = TextureAspect.All
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUTextureViewDescriptor -> 'a) : 'a = 
        let x = x
        let _Label_string = x.Label
        let _Format_TextureFormat = x.Format.GetValue()
        let _Dimension_TextureViewDimension = x.Dimension.GetValue()
        let _BaseMipLevel_int = uint32 (x.BaseMipLevel)
        let _MipLevelCount_int = uint32 (x.MipLevelCount)
        let _BaseArrayLayer_int = uint32 (x.BaseArrayLayer)
        let _ArrayLayerCount_int = uint32 (x.ArrayLayerCount)
        let _Aspect_TextureAspect = x.Aspect.GetValue()
        let native = DawnRaw.WGPUTextureViewDescriptor()
        native.Label <- _Label_string
        native.Format <- _Format_TextureFormat
        native.Dimension <- _Dimension_TextureViewDimension
        native.BaseMipLevel <- _BaseMipLevel_int
        native.MipLevelCount <- _MipLevelCount_int
        native.BaseArrayLayer <- _BaseArrayLayer_int
        native.ArrayLayerCount <- _ArrayLayerCount_int
        native.Aspect <- _Aspect_TextureAspect
        callback native
type VertexAttribute =
    {
        Format : VertexFormat
        Offset : uint64
        ShaderLocation : int
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUVertexAttribute -> 'a) : 'a = 
        let x = x
        let _Format_VertexFormat = x.Format.GetValue()
        let _Offset_uint64 = float (x.Offset)
        let _ShaderLocation_int = uint32 (x.ShaderLocation)
        let native = DawnRaw.WGPUVertexAttribute()
        native.Format <- _Format_VertexFormat
        native.Offset <- _Offset_uint64
        native.ShaderLocation <- _ShaderLocation_int
        callback native
type BindGroupLayoutEntry =
    {
        Binding : int
        Visibility : ShaderStage
        Buffer : option<BufferBindingLayout>
        Sampler : option<SamplerBindingLayout>
        Texture : option<TextureBindingLayout>
        StorageTexture : option<StorageTextureBindingLayout>
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUBindGroupLayoutEntry -> 'a) : 'a = 
        let x = x
        let _Binding_int = uint32 (x.Binding)
        let _Visibility_ShaderStage = int (x.Visibility)
        let inline _Buffer_BufferBindingLayoutOptCont _Buffer_BufferBindingLayoutOpt = 
            let inline _Sampler_SamplerBindingLayoutOptCont _Sampler_SamplerBindingLayoutOpt = 
                let inline _Texture_TextureBindingLayoutOptCont _Texture_TextureBindingLayoutOpt = 
                    let inline _StorageTexture_StorageTextureBindingLayoutOptCont _StorageTexture_StorageTextureBindingLayoutOpt = 
                        let native = DawnRaw.WGPUBindGroupLayoutEntry()
                        native.Binding <- _Binding_int
                        native.Visibility <- _Visibility_ShaderStage
                        native.Buffer <- _Buffer_BufferBindingLayoutOpt
                        native.Sampler <- _Sampler_SamplerBindingLayoutOpt
                        native.Texture <- _Texture_TextureBindingLayoutOpt
                        native.StorageTexture <- _StorageTexture_StorageTextureBindingLayoutOpt
                        callback native
                    match x.StorageTexture with
                    | Some v ->
                        let _Access_StorageTextureAccess = v.Access.GetValue()
                        let _Format_TextureFormat = v.Format.GetValue()
                        let _ViewDimension_TextureViewDimension = v.ViewDimension.GetValue()
                        let _n = new DawnRaw.WGPUStorageTextureBindingLayout()
                        _n.Access <- _Access_StorageTextureAccess
                        _n.Format <- _Format_TextureFormat
                        _n.ViewDimension <- _ViewDimension_TextureViewDimension
                        let _n = _n
                        _StorageTexture_StorageTextureBindingLayoutOptCont _n
                    | None -> _StorageTexture_StorageTextureBindingLayoutOptCont null
                match x.Texture with
                | Some v ->
                    let _SampleType_TextureSampleType = v.SampleType.GetValue()
                    let _ViewDimension_TextureViewDimension = v.ViewDimension.GetValue()
                    let _Multisampled_bool = v.Multisampled
                    let _n = new DawnRaw.WGPUTextureBindingLayout()
                    _n.SampleType <- _SampleType_TextureSampleType
                    _n.ViewDimension <- _ViewDimension_TextureViewDimension
                    _n.Multisampled <- _Multisampled_bool
                    let _n = _n
                    _Texture_TextureBindingLayoutOptCont _n
                | None -> _Texture_TextureBindingLayoutOptCont null
            match x.Sampler with
            | Some v ->
                let _Type_SamplerBindingType = v.Type.GetValue()
                let _n = new DawnRaw.WGPUSamplerBindingLayout()
                _n.Type <- _Type_SamplerBindingType
                let _n = _n
                _Sampler_SamplerBindingLayoutOptCont _n
            | None -> _Sampler_SamplerBindingLayoutOptCont null
        match x.Buffer with
        | Some v ->
            let _Type_BufferBindingType = v.Type.GetValue()
            let _HasDynamicOffset_bool = v.HasDynamicOffset
            let _MinBindingSize_uint64 = float (v.MinBindingSize)
            let _n = new DawnRaw.WGPUBufferBindingLayout()
            _n.Type <- _Type_BufferBindingType
            _n.HasDynamicOffset <- _HasDynamicOffset_bool
            _n.MinBindingSize <- _MinBindingSize_uint64
            let _n = _n
            _Buffer_BufferBindingLayoutOptCont _n
        | None -> _Buffer_BufferBindingLayoutOptCont null
type BlendState =
    {
        Color : BlendComponent
        Alpha : BlendComponent
    }
    static member Default : BlendState =
        {
            Color = BlendComponent.Default
            Alpha = BlendComponent.Default
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUBlendState -> 'a) : 'a = 
        let x = x
        let _Operation_BlendOperation = x.Color.Operation.GetValue()
        let _SrcFactor_BlendFactor = x.Color.SrcFactor.GetValue()
        let _DstFactor_BlendFactor = x.Color.DstFactor.GetValue()
        let _Color_BlendComponent = new DawnRaw.WGPUBlendComponent()
        _Color_BlendComponent.Operation <- _Operation_BlendOperation
        _Color_BlendComponent.SrcFactor <- _SrcFactor_BlendFactor
        _Color_BlendComponent.DstFactor <- _DstFactor_BlendFactor
        let _Color_BlendComponent = _Color_BlendComponent
        let _Operation_BlendOperation = x.Alpha.Operation.GetValue()
        let _SrcFactor_BlendFactor = x.Alpha.SrcFactor.GetValue()
        let _DstFactor_BlendFactor = x.Alpha.DstFactor.GetValue()
        let _Alpha_BlendComponent = new DawnRaw.WGPUBlendComponent()
        _Alpha_BlendComponent.Operation <- _Operation_BlendOperation
        _Alpha_BlendComponent.SrcFactor <- _SrcFactor_BlendFactor
        _Alpha_BlendComponent.DstFactor <- _DstFactor_BlendFactor
        let _Alpha_BlendComponent = _Alpha_BlendComponent
        let native = DawnRaw.WGPUBlendState()
        native.Color <- _Color_BlendComponent
        native.Alpha <- _Alpha_BlendComponent
        callback native
type CompilationInfo =
    {
        Messages : array<CompilationMessage>
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUCompilationInfo -> 'a) : 'a = 
        let x = x
        let _Messages_CompilationMessageArrCount = if isNull x.Messages then 0 else x.Messages.Length
        let rec _Messages_CompilationMessageArrCont (_Messages_CompilationMessageArrinputs : array<CompilationMessage>) (_Messages_CompilationMessageArroutputs : JsArray) (_Messages_CompilationMessageArri : int) =
            if _Messages_CompilationMessageArri >= _Messages_CompilationMessageArrCount then
                let _Messages_CompilationMessageArr = _Messages_CompilationMessageArroutputs.Reference
                let native = DawnRaw.WGPUCompilationInfo()
                native.Messages <- _Messages_CompilationMessageArr
                callback native
            else
                let _Message_string = _Messages_CompilationMessageArrinputs.[_Messages_CompilationMessageArri].Message
                let _Type_CompilationMessageType = _Messages_CompilationMessageArrinputs.[_Messages_CompilationMessageArri].Type.GetValue()
                let _LineNum_uint64 = float (_Messages_CompilationMessageArrinputs.[_Messages_CompilationMessageArri].LineNum)
                let _LinePos_uint64 = float (_Messages_CompilationMessageArrinputs.[_Messages_CompilationMessageArri].LinePos)
                let _Offset_uint64 = float (_Messages_CompilationMessageArrinputs.[_Messages_CompilationMessageArri].Offset)
                let _Length_uint64 = float (_Messages_CompilationMessageArrinputs.[_Messages_CompilationMessageArri].Length)
                let _n = new DawnRaw.WGPUCompilationMessage()
                _n.Message <- _Message_string
                _n.Type <- _Type_CompilationMessageType
                _n.LineNum <- _LineNum_uint64
                _n.LinePos <- _LinePos_uint64
                _n.Offset <- _Offset_uint64
                _n.Length <- _Length_uint64
                let _n = _n
                _Messages_CompilationMessageArroutputs.[_Messages_CompilationMessageArri] <- js _n
                _Messages_CompilationMessageArrCont _Messages_CompilationMessageArrinputs _Messages_CompilationMessageArroutputs (_Messages_CompilationMessageArri + 1)
        _Messages_CompilationMessageArrCont x.Messages (if _Messages_CompilationMessageArrCount > 0 then newArray _Messages_CompilationMessageArrCount else null) 0
type DepthStencilState =
    {
        Format : TextureFormat
        DepthWriteEnabled : bool
        DepthCompare : CompareFunction
        StencilFront : StencilFaceState
        StencilBack : StencilFaceState
        StencilReadMask : int
        StencilWriteMask : int
        DepthBias : int32
        DepthBiasSlopeScale : float32
        DepthBiasClamp : float32
    }
    static member Default(Format: TextureFormat) : DepthStencilState =
        {
            Format = Format
            DepthWriteEnabled = false
            DepthCompare = CompareFunction.Always
            StencilFront = StencilFaceState.Default
            StencilBack = StencilFaceState.Default
            StencilReadMask = -1
            StencilWriteMask = -1
            DepthBias = 0
            DepthBiasSlopeScale = 0.000000f
            DepthBiasClamp = 0.000000f
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUDepthStencilState -> 'a) : 'a = 
        let x = x
        let _Format_TextureFormat = x.Format.GetValue()
        let _DepthWriteEnabled_bool = x.DepthWriteEnabled
        let _DepthCompare_CompareFunction = x.DepthCompare.GetValue()
        let _Compare_CompareFunction = x.StencilFront.Compare.GetValue()
        let _FailOp_StencilOperation = x.StencilFront.FailOp.GetValue()
        let _DepthFailOp_StencilOperation = x.StencilFront.DepthFailOp.GetValue()
        let _PassOp_StencilOperation = x.StencilFront.PassOp.GetValue()
        let _StencilFront_StencilFaceState = new DawnRaw.WGPUStencilFaceState()
        _StencilFront_StencilFaceState.Compare <- _Compare_CompareFunction
        _StencilFront_StencilFaceState.FailOp <- _FailOp_StencilOperation
        _StencilFront_StencilFaceState.DepthFailOp <- _DepthFailOp_StencilOperation
        _StencilFront_StencilFaceState.PassOp <- _PassOp_StencilOperation
        let _StencilFront_StencilFaceState = _StencilFront_StencilFaceState
        let _Compare_CompareFunction = x.StencilBack.Compare.GetValue()
        let _FailOp_StencilOperation = x.StencilBack.FailOp.GetValue()
        let _DepthFailOp_StencilOperation = x.StencilBack.DepthFailOp.GetValue()
        let _PassOp_StencilOperation = x.StencilBack.PassOp.GetValue()
        let _StencilBack_StencilFaceState = new DawnRaw.WGPUStencilFaceState()
        _StencilBack_StencilFaceState.Compare <- _Compare_CompareFunction
        _StencilBack_StencilFaceState.FailOp <- _FailOp_StencilOperation
        _StencilBack_StencilFaceState.DepthFailOp <- _DepthFailOp_StencilOperation
        _StencilBack_StencilFaceState.PassOp <- _PassOp_StencilOperation
        let _StencilBack_StencilFaceState = _StencilBack_StencilFaceState
        let _StencilReadMask_int = uint32 (x.StencilReadMask)
        let _StencilWriteMask_int = uint32 (x.StencilWriteMask)
        let _DepthBias_int32 = int32 (x.DepthBias)
        let _DepthBiasSlopeScale_float32 = (x.DepthBiasSlopeScale)
        let _DepthBiasClamp_float32 = (x.DepthBiasClamp)
        let native = DawnRaw.WGPUDepthStencilState()
        native.Format <- _Format_TextureFormat
        native.DepthWriteEnabled <- _DepthWriteEnabled_bool
        native.DepthCompare <- _DepthCompare_CompareFunction
        native.StencilFront <- _StencilFront_StencilFaceState
        native.StencilBack <- _StencilBack_StencilFaceState
        native.StencilReadMask <- _StencilReadMask_int
        native.StencilWriteMask <- _StencilWriteMask_int
        native.DepthBias <- _DepthBias_int32
        native.DepthBiasSlopeScale <- _DepthBiasSlopeScale_float32
        native.DepthBiasClamp <- _DepthBiasClamp_float32
        callback native
type DeviceDescriptor =
    {
        RequiredFeatures : array<FeatureName>
        RequiredLimits : option<RequiredLimits>
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUDeviceDescriptor -> 'a) : 'a = 
        let x = x
        let _RequiredFeatures_FeatureNameArrCount = x.RequiredFeatures.Length
        let _RequiredFeatures_FeatureNameArrArray = newArray (_RequiredFeatures_FeatureNameArrCount)
        for i in 0 .. _RequiredFeatures_FeatureNameArrCount-1 do
            _RequiredFeatures_FeatureNameArrArray.[i] <- x.RequiredFeatures.[i].GetValue()
        let _RequiredFeatures_FeatureNameArr = _RequiredFeatures_FeatureNameArrArray.Reference
        let inline _RequiredLimits_RequiredLimitsOptCont _RequiredLimits_RequiredLimitsOpt = 
            let native = DawnRaw.WGPUDeviceDescriptor()
            native.RequiredFeatures <- _RequiredFeatures_FeatureNameArr
            native.RequiredLimits <- _RequiredLimits_RequiredLimitsOpt
            callback native
        match x.RequiredLimits with
        | Some v ->
            let _MaxTextureDimension1D_int = uint32 (v.Limits.MaxTextureDimension1D)
            let _MaxTextureDimension2D_int = uint32 (v.Limits.MaxTextureDimension2D)
            let _MaxTextureDimension3D_int = uint32 (v.Limits.MaxTextureDimension3D)
            let _MaxTextureArrayLayers_int = uint32 (v.Limits.MaxTextureArrayLayers)
            let _MaxBindGroups_int = uint32 (v.Limits.MaxBindGroups)
            let _MaxDynamicUniformBuffersPerPipelineLayout_int = uint32 (v.Limits.MaxDynamicUniformBuffersPerPipelineLayout)
            let _MaxDynamicStorageBuffersPerPipelineLayout_int = uint32 (v.Limits.MaxDynamicStorageBuffersPerPipelineLayout)
            let _MaxSampledTexturesPerShaderStage_int = uint32 (v.Limits.MaxSampledTexturesPerShaderStage)
            let _MaxSamplersPerShaderStage_int = uint32 (v.Limits.MaxSamplersPerShaderStage)
            let _MaxStorageBuffersPerShaderStage_int = uint32 (v.Limits.MaxStorageBuffersPerShaderStage)
            let _MaxStorageTexturesPerShaderStage_int = uint32 (v.Limits.MaxStorageTexturesPerShaderStage)
            let _MaxUniformBuffersPerShaderStage_int = uint32 (v.Limits.MaxUniformBuffersPerShaderStage)
            let _MaxUniformBufferBindingSize_uint64 = float (v.Limits.MaxUniformBufferBindingSize)
            let _MaxStorageBufferBindingSize_uint64 = float (v.Limits.MaxStorageBufferBindingSize)
            let _MinUniformBufferOffsetAlignment_int = uint32 (v.Limits.MinUniformBufferOffsetAlignment)
            let _MinStorageBufferOffsetAlignment_int = uint32 (v.Limits.MinStorageBufferOffsetAlignment)
            let _MaxVertexBuffers_int = uint32 (v.Limits.MaxVertexBuffers)
            let _MaxVertexAttributes_int = uint32 (v.Limits.MaxVertexAttributes)
            let _MaxVertexBufferArrayStride_int = uint32 (v.Limits.MaxVertexBufferArrayStride)
            let _MaxInterStageShaderComponents_int = uint32 (v.Limits.MaxInterStageShaderComponents)
            let _MaxComputeWorkgroupStorageSize_int = uint32 (v.Limits.MaxComputeWorkgroupStorageSize)
            let _MaxComputeInvocationsPerWorkgroup_int = uint32 (v.Limits.MaxComputeInvocationsPerWorkgroup)
            let _MaxComputeWorkgroupSizeX_int = uint32 (v.Limits.MaxComputeWorkgroupSizeX)
            let _MaxComputeWorkgroupSizeY_int = uint32 (v.Limits.MaxComputeWorkgroupSizeY)
            let _MaxComputeWorkgroupSizeZ_int = uint32 (v.Limits.MaxComputeWorkgroupSizeZ)
            let _MaxComputeWorkgroupsPerDimension_int = uint32 (v.Limits.MaxComputeWorkgroupsPerDimension)
            let _Limits_Limits = new DawnRaw.WGPULimits()
            _Limits_Limits.MaxTextureDimension1D <- _MaxTextureDimension1D_int
            _Limits_Limits.MaxTextureDimension2D <- _MaxTextureDimension2D_int
            _Limits_Limits.MaxTextureDimension3D <- _MaxTextureDimension3D_int
            _Limits_Limits.MaxTextureArrayLayers <- _MaxTextureArrayLayers_int
            _Limits_Limits.MaxBindGroups <- _MaxBindGroups_int
            _Limits_Limits.MaxDynamicUniformBuffersPerPipelineLayout <- _MaxDynamicUniformBuffersPerPipelineLayout_int
            _Limits_Limits.MaxDynamicStorageBuffersPerPipelineLayout <- _MaxDynamicStorageBuffersPerPipelineLayout_int
            _Limits_Limits.MaxSampledTexturesPerShaderStage <- _MaxSampledTexturesPerShaderStage_int
            _Limits_Limits.MaxSamplersPerShaderStage <- _MaxSamplersPerShaderStage_int
            _Limits_Limits.MaxStorageBuffersPerShaderStage <- _MaxStorageBuffersPerShaderStage_int
            _Limits_Limits.MaxStorageTexturesPerShaderStage <- _MaxStorageTexturesPerShaderStage_int
            _Limits_Limits.MaxUniformBuffersPerShaderStage <- _MaxUniformBuffersPerShaderStage_int
            _Limits_Limits.MaxUniformBufferBindingSize <- _MaxUniformBufferBindingSize_uint64
            _Limits_Limits.MaxStorageBufferBindingSize <- _MaxStorageBufferBindingSize_uint64
            _Limits_Limits.MinUniformBufferOffsetAlignment <- _MinUniformBufferOffsetAlignment_int
            _Limits_Limits.MinStorageBufferOffsetAlignment <- _MinStorageBufferOffsetAlignment_int
            _Limits_Limits.MaxVertexBuffers <- _MaxVertexBuffers_int
            _Limits_Limits.MaxVertexAttributes <- _MaxVertexAttributes_int
            _Limits_Limits.MaxVertexBufferArrayStride <- _MaxVertexBufferArrayStride_int
            _Limits_Limits.MaxInterStageShaderComponents <- _MaxInterStageShaderComponents_int
            _Limits_Limits.MaxComputeWorkgroupStorageSize <- _MaxComputeWorkgroupStorageSize_int
            _Limits_Limits.MaxComputeInvocationsPerWorkgroup <- _MaxComputeInvocationsPerWorkgroup_int
            _Limits_Limits.MaxComputeWorkgroupSizeX <- _MaxComputeWorkgroupSizeX_int
            _Limits_Limits.MaxComputeWorkgroupSizeY <- _MaxComputeWorkgroupSizeY_int
            _Limits_Limits.MaxComputeWorkgroupSizeZ <- _MaxComputeWorkgroupSizeZ_int
            _Limits_Limits.MaxComputeWorkgroupsPerDimension <- _MaxComputeWorkgroupsPerDimension_int
            let _Limits_Limits = _Limits_Limits
            let _n = new DawnRaw.WGPURequiredLimits()
            _n.Limits <- _Limits_Limits
            let _n = _n
            _RequiredLimits_RequiredLimitsOptCont _n
        | None -> _RequiredLimits_RequiredLimitsOptCont null
type DeviceProperties =
    {
        DeviceID : int
        VendorID : int
        TextureCompressionBC : bool
        TextureCompressionETC2 : bool
        TextureCompressionASTC : bool
        ShaderFloat16 : bool
        PipelineStatisticsQuery : bool
        TimestampQuery : bool
        MultiPlanarFormats : bool
        DepthClamping : bool
        InvalidExtension : bool
        InvalidFeature : bool
        DawnInternalUsages : bool
        Limits : SupportedLimits
    }
    static member Default(DeviceID: int, VendorID: int, Limits: SupportedLimits) : DeviceProperties =
        {
            DeviceID = DeviceID
            VendorID = VendorID
            TextureCompressionBC = false
            TextureCompressionETC2 = false
            TextureCompressionASTC = false
            ShaderFloat16 = false
            PipelineStatisticsQuery = false
            TimestampQuery = false
            MultiPlanarFormats = false
            DepthClamping = false
            InvalidExtension = false
            InvalidFeature = false
            DawnInternalUsages = false
            Limits = Limits
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUDeviceProperties -> 'a) : 'a = 
        let x = x
        let _DeviceID_int = uint32 (x.DeviceID)
        let _VendorID_int = uint32 (x.VendorID)
        let _TextureCompressionBC_bool = x.TextureCompressionBC
        let _TextureCompressionETC2_bool = x.TextureCompressionETC2
        let _TextureCompressionASTC_bool = x.TextureCompressionASTC
        let _ShaderFloat16_bool = x.ShaderFloat16
        let _PipelineStatisticsQuery_bool = x.PipelineStatisticsQuery
        let _TimestampQuery_bool = x.TimestampQuery
        let _MultiPlanarFormats_bool = x.MultiPlanarFormats
        let _DepthClamping_bool = x.DepthClamping
        let _InvalidExtension_bool = x.InvalidExtension
        let _InvalidFeature_bool = x.InvalidFeature
        let _DawnInternalUsages_bool = x.DawnInternalUsages
        let _MaxTextureDimension1D_int = uint32 (x.Limits.Limits.MaxTextureDimension1D)
        let _MaxTextureDimension2D_int = uint32 (x.Limits.Limits.MaxTextureDimension2D)
        let _MaxTextureDimension3D_int = uint32 (x.Limits.Limits.MaxTextureDimension3D)
        let _MaxTextureArrayLayers_int = uint32 (x.Limits.Limits.MaxTextureArrayLayers)
        let _MaxBindGroups_int = uint32 (x.Limits.Limits.MaxBindGroups)
        let _MaxDynamicUniformBuffersPerPipelineLayout_int = uint32 (x.Limits.Limits.MaxDynamicUniformBuffersPerPipelineLayout)
        let _MaxDynamicStorageBuffersPerPipelineLayout_int = uint32 (x.Limits.Limits.MaxDynamicStorageBuffersPerPipelineLayout)
        let _MaxSampledTexturesPerShaderStage_int = uint32 (x.Limits.Limits.MaxSampledTexturesPerShaderStage)
        let _MaxSamplersPerShaderStage_int = uint32 (x.Limits.Limits.MaxSamplersPerShaderStage)
        let _MaxStorageBuffersPerShaderStage_int = uint32 (x.Limits.Limits.MaxStorageBuffersPerShaderStage)
        let _MaxStorageTexturesPerShaderStage_int = uint32 (x.Limits.Limits.MaxStorageTexturesPerShaderStage)
        let _MaxUniformBuffersPerShaderStage_int = uint32 (x.Limits.Limits.MaxUniformBuffersPerShaderStage)
        let _MaxUniformBufferBindingSize_uint64 = float (x.Limits.Limits.MaxUniformBufferBindingSize)
        let _MaxStorageBufferBindingSize_uint64 = float (x.Limits.Limits.MaxStorageBufferBindingSize)
        let _MinUniformBufferOffsetAlignment_int = uint32 (x.Limits.Limits.MinUniformBufferOffsetAlignment)
        let _MinStorageBufferOffsetAlignment_int = uint32 (x.Limits.Limits.MinStorageBufferOffsetAlignment)
        let _MaxVertexBuffers_int = uint32 (x.Limits.Limits.MaxVertexBuffers)
        let _MaxVertexAttributes_int = uint32 (x.Limits.Limits.MaxVertexAttributes)
        let _MaxVertexBufferArrayStride_int = uint32 (x.Limits.Limits.MaxVertexBufferArrayStride)
        let _MaxInterStageShaderComponents_int = uint32 (x.Limits.Limits.MaxInterStageShaderComponents)
        let _MaxComputeWorkgroupStorageSize_int = uint32 (x.Limits.Limits.MaxComputeWorkgroupStorageSize)
        let _MaxComputeInvocationsPerWorkgroup_int = uint32 (x.Limits.Limits.MaxComputeInvocationsPerWorkgroup)
        let _MaxComputeWorkgroupSizeX_int = uint32 (x.Limits.Limits.MaxComputeWorkgroupSizeX)
        let _MaxComputeWorkgroupSizeY_int = uint32 (x.Limits.Limits.MaxComputeWorkgroupSizeY)
        let _MaxComputeWorkgroupSizeZ_int = uint32 (x.Limits.Limits.MaxComputeWorkgroupSizeZ)
        let _MaxComputeWorkgroupsPerDimension_int = uint32 (x.Limits.Limits.MaxComputeWorkgroupsPerDimension)
        let _Limits_Limits = new DawnRaw.WGPULimits()
        _Limits_Limits.MaxTextureDimension1D <- _MaxTextureDimension1D_int
        _Limits_Limits.MaxTextureDimension2D <- _MaxTextureDimension2D_int
        _Limits_Limits.MaxTextureDimension3D <- _MaxTextureDimension3D_int
        _Limits_Limits.MaxTextureArrayLayers <- _MaxTextureArrayLayers_int
        _Limits_Limits.MaxBindGroups <- _MaxBindGroups_int
        _Limits_Limits.MaxDynamicUniformBuffersPerPipelineLayout <- _MaxDynamicUniformBuffersPerPipelineLayout_int
        _Limits_Limits.MaxDynamicStorageBuffersPerPipelineLayout <- _MaxDynamicStorageBuffersPerPipelineLayout_int
        _Limits_Limits.MaxSampledTexturesPerShaderStage <- _MaxSampledTexturesPerShaderStage_int
        _Limits_Limits.MaxSamplersPerShaderStage <- _MaxSamplersPerShaderStage_int
        _Limits_Limits.MaxStorageBuffersPerShaderStage <- _MaxStorageBuffersPerShaderStage_int
        _Limits_Limits.MaxStorageTexturesPerShaderStage <- _MaxStorageTexturesPerShaderStage_int
        _Limits_Limits.MaxUniformBuffersPerShaderStage <- _MaxUniformBuffersPerShaderStage_int
        _Limits_Limits.MaxUniformBufferBindingSize <- _MaxUniformBufferBindingSize_uint64
        _Limits_Limits.MaxStorageBufferBindingSize <- _MaxStorageBufferBindingSize_uint64
        _Limits_Limits.MinUniformBufferOffsetAlignment <- _MinUniformBufferOffsetAlignment_int
        _Limits_Limits.MinStorageBufferOffsetAlignment <- _MinStorageBufferOffsetAlignment_int
        _Limits_Limits.MaxVertexBuffers <- _MaxVertexBuffers_int
        _Limits_Limits.MaxVertexAttributes <- _MaxVertexAttributes_int
        _Limits_Limits.MaxVertexBufferArrayStride <- _MaxVertexBufferArrayStride_int
        _Limits_Limits.MaxInterStageShaderComponents <- _MaxInterStageShaderComponents_int
        _Limits_Limits.MaxComputeWorkgroupStorageSize <- _MaxComputeWorkgroupStorageSize_int
        _Limits_Limits.MaxComputeInvocationsPerWorkgroup <- _MaxComputeInvocationsPerWorkgroup_int
        _Limits_Limits.MaxComputeWorkgroupSizeX <- _MaxComputeWorkgroupSizeX_int
        _Limits_Limits.MaxComputeWorkgroupSizeY <- _MaxComputeWorkgroupSizeY_int
        _Limits_Limits.MaxComputeWorkgroupSizeZ <- _MaxComputeWorkgroupSizeZ_int
        _Limits_Limits.MaxComputeWorkgroupsPerDimension <- _MaxComputeWorkgroupsPerDimension_int
        let _Limits_Limits = _Limits_Limits
        let _Limits_SupportedLimits = new DawnRaw.WGPUSupportedLimits()
        _Limits_SupportedLimits.Limits <- _Limits_Limits
        let _Limits_SupportedLimits = _Limits_SupportedLimits
        let native = DawnRaw.WGPUDeviceProperties()
        native.DeviceID <- _DeviceID_int
        native.VendorID <- _VendorID_int
        native.TextureCompressionBC <- _TextureCompressionBC_bool
        native.TextureCompressionETC2 <- _TextureCompressionETC2_bool
        native.TextureCompressionASTC <- _TextureCompressionASTC_bool
        native.ShaderFloat16 <- _ShaderFloat16_bool
        native.PipelineStatisticsQuery <- _PipelineStatisticsQuery_bool
        native.TimestampQuery <- _TimestampQuery_bool
        native.MultiPlanarFormats <- _MultiPlanarFormats_bool
        native.DepthClamping <- _DepthClamping_bool
        native.InvalidExtension <- _InvalidExtension_bool
        native.InvalidFeature <- _InvalidFeature_bool
        native.DawnInternalUsages <- _DawnInternalUsages_bool
        native.Limits <- _Limits_SupportedLimits
        callback native
type RequestAdapterOptions =
    {
        CompatibleSurface : Surface
        PowerPreference : PowerPreference
        ForceFallbackAdapter : bool
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPURequestAdapterOptions -> 'a) : 'a = 
        let x = x
        let _CompatibleSurface_Surface = (if isNull x.CompatibleSurface then null else x.CompatibleSurface.Handle)
        let _PowerPreference_PowerPreference = x.PowerPreference.GetValue()
        let _ForceFallbackAdapter_bool = x.ForceFallbackAdapter
        let native = DawnRaw.WGPURequestAdapterOptions()
        native.CompatibleSurface <- _CompatibleSurface_Surface
        native.PowerPreference <- _PowerPreference_PowerPreference
        native.ForceFallbackAdapter <- _ForceFallbackAdapter_bool
        callback native
type VertexBufferLayout =
    {
        ArrayStride : uint64
        StepMode : VertexStepMode
        Attributes : array<VertexAttribute>
    }
    static member Default(ArrayStride: uint64, Attributes: array<VertexAttribute>) : VertexBufferLayout =
        {
            ArrayStride = ArrayStride
            StepMode = VertexStepMode.Vertex
            Attributes = Attributes
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUVertexBufferLayout -> 'a) : 'a = 
        let x = x
        let _ArrayStride_uint64 = float (x.ArrayStride)
        let _StepMode_VertexStepMode = x.StepMode.GetValue()
        let _Attributes_VertexAttributeArrCount = if isNull x.Attributes then 0 else x.Attributes.Length
        let rec _Attributes_VertexAttributeArrCont (_Attributes_VertexAttributeArrinputs : array<VertexAttribute>) (_Attributes_VertexAttributeArroutputs : JsArray) (_Attributes_VertexAttributeArri : int) =
            if _Attributes_VertexAttributeArri >= _Attributes_VertexAttributeArrCount then
                let _Attributes_VertexAttributeArr = _Attributes_VertexAttributeArroutputs.Reference
                let native = DawnRaw.WGPUVertexBufferLayout()
                native.ArrayStride <- _ArrayStride_uint64
                native.StepMode <- _StepMode_VertexStepMode
                native.Attributes <- _Attributes_VertexAttributeArr
                callback native
            else
                let _Format_VertexFormat = _Attributes_VertexAttributeArrinputs.[_Attributes_VertexAttributeArri].Format.GetValue()
                let _Offset_uint64 = float (_Attributes_VertexAttributeArrinputs.[_Attributes_VertexAttributeArri].Offset)
                let _ShaderLocation_int = uint32 (_Attributes_VertexAttributeArrinputs.[_Attributes_VertexAttributeArri].ShaderLocation)
                let _n = new DawnRaw.WGPUVertexAttribute()
                _n.Format <- _Format_VertexFormat
                _n.Offset <- _Offset_uint64
                _n.ShaderLocation <- _ShaderLocation_int
                let _n = _n
                _Attributes_VertexAttributeArroutputs.[_Attributes_VertexAttributeArri] <- js _n
                _Attributes_VertexAttributeArrCont _Attributes_VertexAttributeArrinputs _Attributes_VertexAttributeArroutputs (_Attributes_VertexAttributeArri + 1)
        _Attributes_VertexAttributeArrCont x.Attributes (if _Attributes_VertexAttributeArrCount > 0 then newArray _Attributes_VertexAttributeArrCount else null) 0
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
        let _Binding_int = uint32 (x.Binding)
        let _Buffer_Buffer = (if isNull x.Buffer then null else x.Buffer.Handle)
        let _Offset_uint64 = float (x.Offset)
        let _Size_uint64 = float (x.Size)
        let _Sampler_Sampler = (if isNull x.Sampler then null else x.Sampler.Handle)
        let _TextureView_TextureView = (if isNull x.TextureView then null else x.TextureView.Handle)
        let native = DawnRaw.WGPUBindGroupEntry()
        native.Binding <- _Binding_int
        native.Buffer <- _Buffer_Buffer
        native.Offset <- _Offset_uint64
        native.Size <- _Size_uint64
        native.Sampler <- _Sampler_Sampler
        native.TextureView <- _TextureView_TextureView
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
        let _Label_string = x.Label
        let _Entries_BindGroupLayoutEntryArrCount = if isNull x.Entries then 0 else x.Entries.Length
        let rec _Entries_BindGroupLayoutEntryArrCont (_Entries_BindGroupLayoutEntryArrinputs : array<BindGroupLayoutEntry>) (_Entries_BindGroupLayoutEntryArroutputs : JsArray) (_Entries_BindGroupLayoutEntryArri : int) =
            if _Entries_BindGroupLayoutEntryArri >= _Entries_BindGroupLayoutEntryArrCount then
                let _Entries_BindGroupLayoutEntryArr = _Entries_BindGroupLayoutEntryArroutputs.Reference
                let native = DawnRaw.WGPUBindGroupLayoutDescriptor()
                native.Label <- _Label_string
                native.Entries <- _Entries_BindGroupLayoutEntryArr
                callback native
            else
                let _Binding_int = uint32 (_Entries_BindGroupLayoutEntryArrinputs.[_Entries_BindGroupLayoutEntryArri].Binding)
                let _Visibility_ShaderStage = int (_Entries_BindGroupLayoutEntryArrinputs.[_Entries_BindGroupLayoutEntryArri].Visibility)
                let inline _Buffer_BufferBindingLayoutOptCont _Buffer_BufferBindingLayoutOpt = 
                    let inline _Sampler_SamplerBindingLayoutOptCont _Sampler_SamplerBindingLayoutOpt = 
                        let inline _Texture_TextureBindingLayoutOptCont _Texture_TextureBindingLayoutOpt = 
                            let inline _StorageTexture_StorageTextureBindingLayoutOptCont _StorageTexture_StorageTextureBindingLayoutOpt = 
                                let _n = new DawnRaw.WGPUBindGroupLayoutEntry()
                                _n.Binding <- _Binding_int
                                _n.Visibility <- _Visibility_ShaderStage
                                _n.Buffer <- _Buffer_BufferBindingLayoutOpt
                                _n.Sampler <- _Sampler_SamplerBindingLayoutOpt
                                _n.Texture <- _Texture_TextureBindingLayoutOpt
                                _n.StorageTexture <- _StorageTexture_StorageTextureBindingLayoutOpt
                                let _n = _n
                                _Entries_BindGroupLayoutEntryArroutputs.[_Entries_BindGroupLayoutEntryArri] <- js _n
                                _Entries_BindGroupLayoutEntryArrCont _Entries_BindGroupLayoutEntryArrinputs _Entries_BindGroupLayoutEntryArroutputs (_Entries_BindGroupLayoutEntryArri + 1)
                            match _Entries_BindGroupLayoutEntryArrinputs.[_Entries_BindGroupLayoutEntryArri].StorageTexture with
                            | Some v ->
                                let _Access_StorageTextureAccess = v.Access.GetValue()
                                let _Format_TextureFormat = v.Format.GetValue()
                                let _ViewDimension_TextureViewDimension = v.ViewDimension.GetValue()
                                let _n = new DawnRaw.WGPUStorageTextureBindingLayout()
                                _n.Access <- _Access_StorageTextureAccess
                                _n.Format <- _Format_TextureFormat
                                _n.ViewDimension <- _ViewDimension_TextureViewDimension
                                let _n = _n
                                _StorageTexture_StorageTextureBindingLayoutOptCont _n
                            | None -> _StorageTexture_StorageTextureBindingLayoutOptCont null
                        match _Entries_BindGroupLayoutEntryArrinputs.[_Entries_BindGroupLayoutEntryArri].Texture with
                        | Some v ->
                            let _SampleType_TextureSampleType = v.SampleType.GetValue()
                            let _ViewDimension_TextureViewDimension = v.ViewDimension.GetValue()
                            let _Multisampled_bool = v.Multisampled
                            let _n = new DawnRaw.WGPUTextureBindingLayout()
                            _n.SampleType <- _SampleType_TextureSampleType
                            _n.ViewDimension <- _ViewDimension_TextureViewDimension
                            _n.Multisampled <- _Multisampled_bool
                            let _n = _n
                            _Texture_TextureBindingLayoutOptCont _n
                        | None -> _Texture_TextureBindingLayoutOptCont null
                    match _Entries_BindGroupLayoutEntryArrinputs.[_Entries_BindGroupLayoutEntryArri].Sampler with
                    | Some v ->
                        let _Type_SamplerBindingType = v.Type.GetValue()
                        let _n = new DawnRaw.WGPUSamplerBindingLayout()
                        _n.Type <- _Type_SamplerBindingType
                        let _n = _n
                        _Sampler_SamplerBindingLayoutOptCont _n
                    | None -> _Sampler_SamplerBindingLayoutOptCont null
                match _Entries_BindGroupLayoutEntryArrinputs.[_Entries_BindGroupLayoutEntryArri].Buffer with
                | Some v ->
                    let _Type_BufferBindingType = v.Type.GetValue()
                    let _HasDynamicOffset_bool = v.HasDynamicOffset
                    let _MinBindingSize_uint64 = float (v.MinBindingSize)
                    let _n = new DawnRaw.WGPUBufferBindingLayout()
                    _n.Type <- _Type_BufferBindingType
                    _n.HasDynamicOffset <- _HasDynamicOffset_bool
                    _n.MinBindingSize <- _MinBindingSize_uint64
                    let _n = _n
                    _Buffer_BufferBindingLayoutOptCont _n
                | None -> _Buffer_BufferBindingLayoutOptCont null
        _Entries_BindGroupLayoutEntryArrCont x.Entries (if _Entries_BindGroupLayoutEntryArrCount > 0 then newArray _Entries_BindGroupLayoutEntryArrCount else null) 0
type ColorTargetState =
    {
        Format : TextureFormat
        Blend : option<BlendState>
        WriteMask : ColorWriteMask
    }
    static member Default(Format: TextureFormat, Blend: option<BlendState>) : ColorTargetState =
        {
            Format = Format
            Blend = Blend
            WriteMask = ColorWriteMask.All
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUColorTargetState -> 'a) : 'a = 
        let x = x
        let _Format_TextureFormat = x.Format.GetValue()
        let inline _Blend_BlendStateOptCont _Blend_BlendStateOpt = 
            let _WriteMask_ColorWriteMask = int (x.WriteMask)
            let native = DawnRaw.WGPUColorTargetState()
            native.Format <- _Format_TextureFormat
            native.Blend <- _Blend_BlendStateOpt
            native.WriteMask <- _WriteMask_ColorWriteMask
            callback native
        match x.Blend with
        | Some v ->
            let _Operation_BlendOperation = v.Color.Operation.GetValue()
            let _SrcFactor_BlendFactor = v.Color.SrcFactor.GetValue()
            let _DstFactor_BlendFactor = v.Color.DstFactor.GetValue()
            let _Color_BlendComponent = new DawnRaw.WGPUBlendComponent()
            _Color_BlendComponent.Operation <- _Operation_BlendOperation
            _Color_BlendComponent.SrcFactor <- _SrcFactor_BlendFactor
            _Color_BlendComponent.DstFactor <- _DstFactor_BlendFactor
            let _Color_BlendComponent = _Color_BlendComponent
            let _Operation_BlendOperation = v.Alpha.Operation.GetValue()
            let _SrcFactor_BlendFactor = v.Alpha.SrcFactor.GetValue()
            let _DstFactor_BlendFactor = v.Alpha.DstFactor.GetValue()
            let _Alpha_BlendComponent = new DawnRaw.WGPUBlendComponent()
            _Alpha_BlendComponent.Operation <- _Operation_BlendOperation
            _Alpha_BlendComponent.SrcFactor <- _SrcFactor_BlendFactor
            _Alpha_BlendComponent.DstFactor <- _DstFactor_BlendFactor
            let _Alpha_BlendComponent = _Alpha_BlendComponent
            let _n = new DawnRaw.WGPUBlendState()
            _n.Color <- _Color_BlendComponent
            _n.Alpha <- _Alpha_BlendComponent
            let _n = _n
            _Blend_BlendStateOptCont _n
        | None -> _Blend_BlendStateOptCont null
type ExternalTextureBindingEntry =
    {
        ExternalTexture : ExternalTexture
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUExternalTextureBindingEntry -> 'a) : 'a = 
        let x = x
        let _ExternalTexture_ExternalTexture = (if isNull x.ExternalTexture then null else x.ExternalTexture.Handle)
        let native = DawnRaw.WGPUExternalTextureBindingEntry()
        native.ExternalTexture <- _ExternalTexture_ExternalTexture
        callback native
type ImageCopyBuffer =
    {
        Offset : uint64
        BytesPerRow : int
        RowsPerImage : int
        Buffer : Buffer
    }
    static member Default(BytesPerRow: int, RowsPerImage: int, Buffer: Buffer) : ImageCopyBuffer =
        {
            Offset = 0UL
            BytesPerRow = BytesPerRow
            RowsPerImage = RowsPerImage
            Buffer = Buffer
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUImageCopyBuffer -> 'a) : 'a = 
        let x = x
        let _Offset_uint64 = float (x.Offset)
        let _BytesPerRow_int = uint32 (x.BytesPerRow)
        let _RowsPerImage_int = uint32 (x.RowsPerImage)
        let _Buffer_Buffer = (if isNull x.Buffer then null else x.Buffer.Handle)
        let native = DawnRaw.WGPUImageCopyBuffer()
        native.Offset <- _Offset_uint64
        native.BytesPerRow <- _BytesPerRow_int
        native.RowsPerImage <- _RowsPerImage_int
        native.Buffer <- _Buffer_Buffer
        callback native
type ImageCopyTexture =
    {
        Texture : Texture
        MipLevel : int
        Origin : Origin3D
        Aspect : TextureAspect
    }
    static member Default(Texture: Texture) : ImageCopyTexture =
        {
            Texture = Texture
            MipLevel = 0
            Origin = Origin3D.Default
            Aspect = TextureAspect.All
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUImageCopyTexture -> 'a) : 'a = 
        let x = x
        let _Texture_Texture = (if isNull x.Texture then null else x.Texture.Handle)
        let _MipLevel_int = uint32 (x.MipLevel)
        let _X_int = uint32 (x.Origin.X)
        let _Y_int = uint32 (x.Origin.Y)
        let _Z_int = uint32 (x.Origin.Z)
        let _Origin_Origin3D = new DawnRaw.WGPUOrigin3D()
        _Origin_Origin3D.X <- _X_int
        _Origin_Origin3D.Y <- _Y_int
        _Origin_Origin3D.Z <- _Z_int
        let _Origin_Origin3D = _Origin_Origin3D
        let _Aspect_TextureAspect = x.Aspect.GetValue()
        let native = DawnRaw.WGPUImageCopyTexture()
        native.Texture <- _Texture_Texture
        native.MipLevel <- _MipLevel_int
        native.Origin <- _Origin_Origin3D
        native.Aspect <- _Aspect_TextureAspect
        callback native
type RenderPassDescriptor =
    {
        Label : string
        ColorAttachments : array<RenderPassColorAttachment>
        DepthStencilAttachment : option<RenderPassDepthStencilAttachment>
        OcclusionQuerySet : QuerySet
    }
    static member Default(ColorAttachments: array<RenderPassColorAttachment>, DepthStencilAttachment: option<RenderPassDepthStencilAttachment>, OcclusionQuerySet: QuerySet) : RenderPassDescriptor =
        {
            Label = null
            ColorAttachments = ColorAttachments
            DepthStencilAttachment = DepthStencilAttachment
            OcclusionQuerySet = OcclusionQuerySet
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPURenderPassDescriptor -> 'a) : 'a = 
        let x = x
        let _Label_string = x.Label
        let _ColorAttachments_RenderPassColorAttachmentArrCount = if isNull x.ColorAttachments then 0 else x.ColorAttachments.Length
        let rec _ColorAttachments_RenderPassColorAttachmentArrCont (_ColorAttachments_RenderPassColorAttachmentArrinputs : array<RenderPassColorAttachment>) (_ColorAttachments_RenderPassColorAttachmentArroutputs : JsArray) (_ColorAttachments_RenderPassColorAttachmentArri : int) =
            if _ColorAttachments_RenderPassColorAttachmentArri >= _ColorAttachments_RenderPassColorAttachmentArrCount then
                let _ColorAttachments_RenderPassColorAttachmentArr = _ColorAttachments_RenderPassColorAttachmentArroutputs.Reference
                let inline _DepthStencilAttachment_RenderPassDepthStencilAttachmentOptCont _DepthStencilAttachment_RenderPassDepthStencilAttachmentOpt = 
                    let _OcclusionQuerySet_QuerySet = (if isNull x.OcclusionQuerySet then null else x.OcclusionQuerySet.Handle)
                    let native = DawnRaw.WGPURenderPassDescriptor()
                    native.Label <- _Label_string
                    native.ColorAttachments <- _ColorAttachments_RenderPassColorAttachmentArr
                    native.DepthStencilAttachment <- _DepthStencilAttachment_RenderPassDepthStencilAttachmentOpt
                    native.OcclusionQuerySet <- _OcclusionQuerySet_QuerySet
                    callback native
                match x.DepthStencilAttachment with
                | Some v ->
                    let _View_TextureView = (if isNull v.View then null else v.View.Handle)
                    let _DepthLoadOp_LoadOp = v.DepthLoadOp.GetValue()
                    let _DepthStoreOp_StoreOp = v.DepthStoreOp.GetValue()
                    let _ClearDepth_float32 = (v.ClearDepth)
                    let _DepthReadOnly_bool = v.DepthReadOnly
                    let _StencilLoadOp_LoadOp = v.StencilLoadOp.GetValue()
                    let _StencilStoreOp_StoreOp = v.StencilStoreOp.GetValue()
                    let _ClearStencil_int = uint32 (v.ClearStencil)
                    let _StencilReadOnly_bool = v.StencilReadOnly
                    let _n = new DawnRaw.WGPURenderPassDepthStencilAttachment()
                    _n.View <- _View_TextureView
                    _n.DepthLoadOp <- _DepthLoadOp_LoadOp
                    _n.DepthStoreOp <- _DepthStoreOp_StoreOp
                    _n.ClearDepth <- _ClearDepth_float32
                    _n.DepthReadOnly <- _DepthReadOnly_bool
                    _n.StencilLoadOp <- _StencilLoadOp_LoadOp
                    _n.StencilStoreOp <- _StencilStoreOp_StoreOp
                    _n.ClearStencil <- _ClearStencil_int
                    _n.StencilReadOnly <- _StencilReadOnly_bool
                    let _n = _n
                    _DepthStencilAttachment_RenderPassDepthStencilAttachmentOptCont _n
                | None -> _DepthStencilAttachment_RenderPassDepthStencilAttachmentOptCont null
            else
                let _View_TextureView = (if isNull _ColorAttachments_RenderPassColorAttachmentArrinputs.[_ColorAttachments_RenderPassColorAttachmentArri].View then null else _ColorAttachments_RenderPassColorAttachmentArrinputs.[_ColorAttachments_RenderPassColorAttachmentArri].View.Handle)
                let _ResolveTarget_TextureView = (if isNull _ColorAttachments_RenderPassColorAttachmentArrinputs.[_ColorAttachments_RenderPassColorAttachmentArri].ResolveTarget then null else _ColorAttachments_RenderPassColorAttachmentArrinputs.[_ColorAttachments_RenderPassColorAttachmentArri].ResolveTarget.Handle)
                let _LoadOp_LoadOp = _ColorAttachments_RenderPassColorAttachmentArrinputs.[_ColorAttachments_RenderPassColorAttachmentArri].LoadOp.GetValue()
                let _StoreOp_StoreOp = _ColorAttachments_RenderPassColorAttachmentArrinputs.[_ColorAttachments_RenderPassColorAttachmentArri].StoreOp.GetValue()
                let _n = new DawnRaw.WGPURenderPassColorAttachment()
                _n.View <- _View_TextureView
                _n.ResolveTarget <- _ResolveTarget_TextureView
                _n.LoadOp <- _LoadOp_LoadOp
                _n.StoreOp <- _StoreOp_StoreOp
                let _n = _n
                _ColorAttachments_RenderPassColorAttachmentArroutputs.[_ColorAttachments_RenderPassColorAttachmentArri] <- js _n
                _ColorAttachments_RenderPassColorAttachmentArrCont _ColorAttachments_RenderPassColorAttachmentArrinputs _ColorAttachments_RenderPassColorAttachmentArroutputs (_ColorAttachments_RenderPassColorAttachmentArri + 1)
        _ColorAttachments_RenderPassColorAttachmentArrCont x.ColorAttachments (if _ColorAttachments_RenderPassColorAttachmentArrCount > 0 then newArray _ColorAttachments_RenderPassColorAttachmentArrCount else null) 0
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
        let _Label_string = x.Label
        let _Layout_BindGroupLayout = (if isNull x.Layout then null else x.Layout.Handle)
        let _Entries_BindGroupEntryArrCount = if isNull x.Entries then 0 else x.Entries.Length
        let rec _Entries_BindGroupEntryArrCont (_Entries_BindGroupEntryArrinputs : array<BindGroupEntry>) (_Entries_BindGroupEntryArroutputs : JsArray) (_Entries_BindGroupEntryArri : int) =
            if _Entries_BindGroupEntryArri >= _Entries_BindGroupEntryArrCount then
                let _Entries_BindGroupEntryArr = _Entries_BindGroupEntryArroutputs.Reference
                let native = DawnRaw.WGPUBindGroupDescriptor()
                native.Label <- _Label_string
                native.Layout <- _Layout_BindGroupLayout
                native.Entries <- _Entries_BindGroupEntryArr
                callback native
            else
                let _Binding_int = uint32 (_Entries_BindGroupEntryArrinputs.[_Entries_BindGroupEntryArri].Binding)
                let _Buffer_Buffer = (if isNull _Entries_BindGroupEntryArrinputs.[_Entries_BindGroupEntryArri].Buffer then null else _Entries_BindGroupEntryArrinputs.[_Entries_BindGroupEntryArri].Buffer.Handle)
                let _Offset_uint64 = float (_Entries_BindGroupEntryArrinputs.[_Entries_BindGroupEntryArri].Offset)
                let _Size_uint64 = float (_Entries_BindGroupEntryArrinputs.[_Entries_BindGroupEntryArri].Size)
                let _Sampler_Sampler = (if isNull _Entries_BindGroupEntryArrinputs.[_Entries_BindGroupEntryArri].Sampler then null else _Entries_BindGroupEntryArrinputs.[_Entries_BindGroupEntryArri].Sampler.Handle)
                let _TextureView_TextureView = (if isNull _Entries_BindGroupEntryArrinputs.[_Entries_BindGroupEntryArri].TextureView then null else _Entries_BindGroupEntryArrinputs.[_Entries_BindGroupEntryArri].TextureView.Handle)
                let _n = new DawnRaw.WGPUBindGroupEntry()
                _n.Binding <- _Binding_int
                _n.Buffer <- _Buffer_Buffer
                _n.Offset <- _Offset_uint64
                _n.Size <- _Size_uint64
                _n.Sampler <- _Sampler_Sampler
                _n.TextureView <- _TextureView_TextureView
                let _n = _n
                _Entries_BindGroupEntryArroutputs.[_Entries_BindGroupEntryArri] <- js _n
                _Entries_BindGroupEntryArrCont _Entries_BindGroupEntryArrinputs _Entries_BindGroupEntryArroutputs (_Entries_BindGroupEntryArri + 1)
        _Entries_BindGroupEntryArrCont x.Entries (if _Entries_BindGroupEntryArrCount > 0 then newArray _Entries_BindGroupEntryArrCount else null) 0
type FragmentState =
    {
        Module : ShaderModule
        EntryPoint : string
        Constants : array<ConstantEntry>
        Targets : array<ColorTargetState>
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUFragmentState -> 'a) : 'a = 
        let x = x
        let _Module_ShaderModule = (if isNull x.Module then null else x.Module.Handle)
        let _EntryPoint_string = x.EntryPoint
        let _Constants_ConstantEntryArrCount = if isNull x.Constants then 0 else x.Constants.Length
        let rec _Constants_ConstantEntryArrCont (_Constants_ConstantEntryArrinputs : array<ConstantEntry>) (_Constants_ConstantEntryArroutputs : JsArray) (_Constants_ConstantEntryArri : int) =
            if _Constants_ConstantEntryArri >= _Constants_ConstantEntryArrCount then
                let _Constants_ConstantEntryArr = _Constants_ConstantEntryArroutputs.Reference
                let _Targets_ColorTargetStateArrCount = if isNull x.Targets then 0 else x.Targets.Length
                let rec _Targets_ColorTargetStateArrCont (_Targets_ColorTargetStateArrinputs : array<ColorTargetState>) (_Targets_ColorTargetStateArroutputs : JsArray) (_Targets_ColorTargetStateArri : int) =
                    if _Targets_ColorTargetStateArri >= _Targets_ColorTargetStateArrCount then
                        let _Targets_ColorTargetStateArr = _Targets_ColorTargetStateArroutputs.Reference
                        let native = DawnRaw.WGPUFragmentState()
                        native.Module <- _Module_ShaderModule
                        native.EntryPoint <- _EntryPoint_string
                        native.Constants <- _Constants_ConstantEntryArr
                        native.Targets <- _Targets_ColorTargetStateArr
                        callback native
                    else
                        let _Format_TextureFormat = _Targets_ColorTargetStateArrinputs.[_Targets_ColorTargetStateArri].Format.GetValue()
                        let inline _Blend_BlendStateOptCont _Blend_BlendStateOpt = 
                            let _WriteMask_ColorWriteMask = int (_Targets_ColorTargetStateArrinputs.[_Targets_ColorTargetStateArri].WriteMask)
                            let _n = new DawnRaw.WGPUColorTargetState()
                            _n.Format <- _Format_TextureFormat
                            _n.Blend <- _Blend_BlendStateOpt
                            _n.WriteMask <- _WriteMask_ColorWriteMask
                            let _n = _n
                            _Targets_ColorTargetStateArroutputs.[_Targets_ColorTargetStateArri] <- js _n
                            _Targets_ColorTargetStateArrCont _Targets_ColorTargetStateArrinputs _Targets_ColorTargetStateArroutputs (_Targets_ColorTargetStateArri + 1)
                        match _Targets_ColorTargetStateArrinputs.[_Targets_ColorTargetStateArri].Blend with
                        | Some v ->
                            let _Operation_BlendOperation = v.Color.Operation.GetValue()
                            let _SrcFactor_BlendFactor = v.Color.SrcFactor.GetValue()
                            let _DstFactor_BlendFactor = v.Color.DstFactor.GetValue()
                            let _Color_BlendComponent = new DawnRaw.WGPUBlendComponent()
                            _Color_BlendComponent.Operation <- _Operation_BlendOperation
                            _Color_BlendComponent.SrcFactor <- _SrcFactor_BlendFactor
                            _Color_BlendComponent.DstFactor <- _DstFactor_BlendFactor
                            let _Color_BlendComponent = _Color_BlendComponent
                            let _Operation_BlendOperation = v.Alpha.Operation.GetValue()
                            let _SrcFactor_BlendFactor = v.Alpha.SrcFactor.GetValue()
                            let _DstFactor_BlendFactor = v.Alpha.DstFactor.GetValue()
                            let _Alpha_BlendComponent = new DawnRaw.WGPUBlendComponent()
                            _Alpha_BlendComponent.Operation <- _Operation_BlendOperation
                            _Alpha_BlendComponent.SrcFactor <- _SrcFactor_BlendFactor
                            _Alpha_BlendComponent.DstFactor <- _DstFactor_BlendFactor
                            let _Alpha_BlendComponent = _Alpha_BlendComponent
                            let _n = new DawnRaw.WGPUBlendState()
                            _n.Color <- _Color_BlendComponent
                            _n.Alpha <- _Alpha_BlendComponent
                            let _n = _n
                            _Blend_BlendStateOptCont _n
                        | None -> _Blend_BlendStateOptCont null
                _Targets_ColorTargetStateArrCont x.Targets (if _Targets_ColorTargetStateArrCount > 0 then newArray _Targets_ColorTargetStateArrCount else null) 0
            else
                let _Key_string = _Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Key
                let _Value_float = (_Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Value)
                let _n = new DawnRaw.WGPUConstantEntry()
                _n.Key <- _Key_string
                _n.Value <- _Value_float
                let _n = _n
                _Constants_ConstantEntryArroutputs.[_Constants_ConstantEntryArri] <- js _n
                _Constants_ConstantEntryArrCont _Constants_ConstantEntryArrinputs _Constants_ConstantEntryArroutputs (_Constants_ConstantEntryArri + 1)
        _Constants_ConstantEntryArrCont x.Constants (if _Constants_ConstantEntryArrCount > 0 then newArray _Constants_ConstantEntryArrCount else null) 0
type ProgrammableStageDescriptor =
    {
        Module : ShaderModule
        EntryPoint : string
        Constants : array<ConstantEntry>
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUProgrammableStageDescriptor -> 'a) : 'a = 
        let x = x
        let _Module_ShaderModule = (if isNull x.Module then null else x.Module.Handle)
        let _EntryPoint_string = x.EntryPoint
        let _Constants_ConstantEntryArrCount = if isNull x.Constants then 0 else x.Constants.Length
        let rec _Constants_ConstantEntryArrCont (_Constants_ConstantEntryArrinputs : array<ConstantEntry>) (_Constants_ConstantEntryArroutputs : JsArray) (_Constants_ConstantEntryArri : int) =
            if _Constants_ConstantEntryArri >= _Constants_ConstantEntryArrCount then
                let _Constants_ConstantEntryArr = _Constants_ConstantEntryArroutputs.Reference
                let native = DawnRaw.WGPUProgrammableStageDescriptor()
                native.Module <- _Module_ShaderModule
                native.EntryPoint <- _EntryPoint_string
                native.Constants <- _Constants_ConstantEntryArr
                callback native
            else
                let _Key_string = _Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Key
                let _Value_float = (_Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Value)
                let _n = new DawnRaw.WGPUConstantEntry()
                _n.Key <- _Key_string
                _n.Value <- _Value_float
                let _n = _n
                _Constants_ConstantEntryArroutputs.[_Constants_ConstantEntryArri] <- js _n
                _Constants_ConstantEntryArrCont _Constants_ConstantEntryArrinputs _Constants_ConstantEntryArroutputs (_Constants_ConstantEntryArri + 1)
        _Constants_ConstantEntryArrCont x.Constants (if _Constants_ConstantEntryArrCount > 0 then newArray _Constants_ConstantEntryArrCount else null) 0
type VertexState =
    {
        Module : ShaderModule
        EntryPoint : string
        Constants : array<ConstantEntry>
        Buffers : array<VertexBufferLayout>
    }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUVertexState -> 'a) : 'a = 
        let x = x
        let _Module_ShaderModule = (if isNull x.Module then null else x.Module.Handle)
        let _EntryPoint_string = x.EntryPoint
        let _Constants_ConstantEntryArrCount = if isNull x.Constants then 0 else x.Constants.Length
        let rec _Constants_ConstantEntryArrCont (_Constants_ConstantEntryArrinputs : array<ConstantEntry>) (_Constants_ConstantEntryArroutputs : JsArray) (_Constants_ConstantEntryArri : int) =
            if _Constants_ConstantEntryArri >= _Constants_ConstantEntryArrCount then
                let _Constants_ConstantEntryArr = _Constants_ConstantEntryArroutputs.Reference
                let _Buffers_VertexBufferLayoutArrCount = if isNull x.Buffers then 0 else x.Buffers.Length
                let rec _Buffers_VertexBufferLayoutArrCont (_Buffers_VertexBufferLayoutArrinputs : array<VertexBufferLayout>) (_Buffers_VertexBufferLayoutArroutputs : JsArray) (_Buffers_VertexBufferLayoutArri : int) =
                    if _Buffers_VertexBufferLayoutArri >= _Buffers_VertexBufferLayoutArrCount then
                        let _Buffers_VertexBufferLayoutArr = _Buffers_VertexBufferLayoutArroutputs.Reference
                        let native = DawnRaw.WGPUVertexState()
                        native.Module <- _Module_ShaderModule
                        native.EntryPoint <- _EntryPoint_string
                        native.Constants <- _Constants_ConstantEntryArr
                        native.Buffers <- _Buffers_VertexBufferLayoutArr
                        callback native
                    else
                        let _ArrayStride_uint64 = float (_Buffers_VertexBufferLayoutArrinputs.[_Buffers_VertexBufferLayoutArri].ArrayStride)
                        let _StepMode_VertexStepMode = _Buffers_VertexBufferLayoutArrinputs.[_Buffers_VertexBufferLayoutArri].StepMode.GetValue()
                        let _Attributes_VertexAttributeArrCount = if isNull _Buffers_VertexBufferLayoutArrinputs.[_Buffers_VertexBufferLayoutArri].Attributes then 0 else _Buffers_VertexBufferLayoutArrinputs.[_Buffers_VertexBufferLayoutArri].Attributes.Length
                        let rec _Attributes_VertexAttributeArrCont (_Attributes_VertexAttributeArrinputs : array<VertexAttribute>) (_Attributes_VertexAttributeArroutputs : JsArray) (_Attributes_VertexAttributeArri : int) =
                            if _Attributes_VertexAttributeArri >= _Attributes_VertexAttributeArrCount then
                                let _Attributes_VertexAttributeArr = _Attributes_VertexAttributeArroutputs.Reference
                                let _n = new DawnRaw.WGPUVertexBufferLayout()
                                _n.ArrayStride <- _ArrayStride_uint64
                                _n.StepMode <- _StepMode_VertexStepMode
                                _n.Attributes <- _Attributes_VertexAttributeArr
                                let _n = _n
                                _Buffers_VertexBufferLayoutArroutputs.[_Buffers_VertexBufferLayoutArri] <- js _n
                                _Buffers_VertexBufferLayoutArrCont _Buffers_VertexBufferLayoutArrinputs _Buffers_VertexBufferLayoutArroutputs (_Buffers_VertexBufferLayoutArri + 1)
                            else
                                let _Format_VertexFormat = _Attributes_VertexAttributeArrinputs.[_Attributes_VertexAttributeArri].Format.GetValue()
                                let _Offset_uint64 = float (_Attributes_VertexAttributeArrinputs.[_Attributes_VertexAttributeArri].Offset)
                                let _ShaderLocation_int = uint32 (_Attributes_VertexAttributeArrinputs.[_Attributes_VertexAttributeArri].ShaderLocation)
                                let _n = new DawnRaw.WGPUVertexAttribute()
                                _n.Format <- _Format_VertexFormat
                                _n.Offset <- _Offset_uint64
                                _n.ShaderLocation <- _ShaderLocation_int
                                let _n = _n
                                _Attributes_VertexAttributeArroutputs.[_Attributes_VertexAttributeArri] <- js _n
                                _Attributes_VertexAttributeArrCont _Attributes_VertexAttributeArrinputs _Attributes_VertexAttributeArroutputs (_Attributes_VertexAttributeArri + 1)
                        _Attributes_VertexAttributeArrCont _Buffers_VertexBufferLayoutArrinputs.[_Buffers_VertexBufferLayoutArri].Attributes (if _Attributes_VertexAttributeArrCount > 0 then newArray _Attributes_VertexAttributeArrCount else null) 0
                _Buffers_VertexBufferLayoutArrCont x.Buffers (if _Buffers_VertexBufferLayoutArrCount > 0 then newArray _Buffers_VertexBufferLayoutArrCount else null) 0
            else
                let _Key_string = _Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Key
                let _Value_float = (_Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Value)
                let _n = new DawnRaw.WGPUConstantEntry()
                _n.Key <- _Key_string
                _n.Value <- _Value_float
                let _n = _n
                _Constants_ConstantEntryArroutputs.[_Constants_ConstantEntryArri] <- js _n
                _Constants_ConstantEntryArrCont _Constants_ConstantEntryArrinputs _Constants_ConstantEntryArroutputs (_Constants_ConstantEntryArri + 1)
        _Constants_ConstantEntryArrCont x.Constants (if _Constants_ConstantEntryArrCount > 0 then newArray _Constants_ConstantEntryArrCount else null) 0
type ComputePipelineDescriptor =
    {
        Label : string
        Layout : PipelineLayout
        Compute : ProgrammableStageDescriptor
    }
    static member Default(Layout: PipelineLayout, Compute: ProgrammableStageDescriptor) : ComputePipelineDescriptor =
        {
            Label = null
            Layout = Layout
            Compute = Compute
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPUComputePipelineDescriptor -> 'a) : 'a = 
        let x = x
        let _Label_string = x.Label
        let _Layout_PipelineLayout = (if isNull x.Layout then null else x.Layout.Handle)
        let _Module_ShaderModule = (if isNull x.Compute.Module then null else x.Compute.Module.Handle)
        let _EntryPoint_string = x.Compute.EntryPoint
        let _Constants_ConstantEntryArrCount = if isNull x.Compute.Constants then 0 else x.Compute.Constants.Length
        let rec _Constants_ConstantEntryArrCont (_Constants_ConstantEntryArrinputs : array<ConstantEntry>) (_Constants_ConstantEntryArroutputs : JsArray) (_Constants_ConstantEntryArri : int) =
            if _Constants_ConstantEntryArri >= _Constants_ConstantEntryArrCount then
                let _Constants_ConstantEntryArr = _Constants_ConstantEntryArroutputs.Reference
                let _Compute_ProgrammableStageDescriptor = new DawnRaw.WGPUProgrammableStageDescriptor()
                _Compute_ProgrammableStageDescriptor.Module <- _Module_ShaderModule
                _Compute_ProgrammableStageDescriptor.EntryPoint <- _EntryPoint_string
                _Compute_ProgrammableStageDescriptor.Constants <- _Constants_ConstantEntryArr
                let _Compute_ProgrammableStageDescriptor = _Compute_ProgrammableStageDescriptor
                let native = DawnRaw.WGPUComputePipelineDescriptor()
                native.Label <- _Label_string
                native.Layout <- _Layout_PipelineLayout
                native.Compute <- _Compute_ProgrammableStageDescriptor
                callback native
            else
                let _Key_string = _Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Key
                let _Value_float = (_Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Value)
                let _n = new DawnRaw.WGPUConstantEntry()
                _n.Key <- _Key_string
                _n.Value <- _Value_float
                let _n = _n
                _Constants_ConstantEntryArroutputs.[_Constants_ConstantEntryArri] <- js _n
                _Constants_ConstantEntryArrCont _Constants_ConstantEntryArrinputs _Constants_ConstantEntryArroutputs (_Constants_ConstantEntryArri + 1)
        _Constants_ConstantEntryArrCont x.Compute.Constants (if _Constants_ConstantEntryArrCount > 0 then newArray _Constants_ConstantEntryArrCount else null) 0
type RenderPipelineDescriptor =
    {
        Label : string
        Layout : PipelineLayout
        Vertex : VertexState
        Primitive : PrimitiveState
        DepthStencil : option<DepthStencilState>
        Multisample : MultisampleState
        Fragment : option<FragmentState>
    }
    static member Default(Layout: PipelineLayout, Vertex: VertexState, DepthStencil: option<DepthStencilState>, Fragment: option<FragmentState>) : RenderPipelineDescriptor =
        {
            Label = null
            Layout = Layout
            Vertex = Vertex
            Primitive = PrimitiveState.Default
            DepthStencil = DepthStencil
            Multisample = MultisampleState.Default
            Fragment = Fragment
        }

    member inline internal x.Pin<'a>(device : Device, callback : DawnRaw.WGPURenderPipelineDescriptor -> 'a) : 'a = 
        let x = x
        let _Label_string = x.Label
        let _Layout_PipelineLayout = (if isNull x.Layout then null else x.Layout.Handle)
        let _Module_ShaderModule = (if isNull x.Vertex.Module then null else x.Vertex.Module.Handle)
        let _EntryPoint_string = x.Vertex.EntryPoint
        let _Constants_ConstantEntryArrCount = if isNull x.Vertex.Constants then 0 else x.Vertex.Constants.Length
        let rec _Constants_ConstantEntryArrCont (_Constants_ConstantEntryArrinputs : array<ConstantEntry>) (_Constants_ConstantEntryArroutputs : JsArray) (_Constants_ConstantEntryArri : int) =
            if _Constants_ConstantEntryArri >= _Constants_ConstantEntryArrCount then
                let _Constants_ConstantEntryArr = _Constants_ConstantEntryArroutputs.Reference
                let _Buffers_VertexBufferLayoutArrCount = if isNull x.Vertex.Buffers then 0 else x.Vertex.Buffers.Length
                let rec _Buffers_VertexBufferLayoutArrCont (_Buffers_VertexBufferLayoutArrinputs : array<VertexBufferLayout>) (_Buffers_VertexBufferLayoutArroutputs : JsArray) (_Buffers_VertexBufferLayoutArri : int) =
                    if _Buffers_VertexBufferLayoutArri >= _Buffers_VertexBufferLayoutArrCount then
                        let _Buffers_VertexBufferLayoutArr = _Buffers_VertexBufferLayoutArroutputs.Reference
                        let _Vertex_VertexState = new DawnRaw.WGPUVertexState()
                        _Vertex_VertexState.Module <- _Module_ShaderModule
                        _Vertex_VertexState.EntryPoint <- _EntryPoint_string
                        _Vertex_VertexState.Constants <- _Constants_ConstantEntryArr
                        _Vertex_VertexState.Buffers <- _Buffers_VertexBufferLayoutArr
                        let _Vertex_VertexState = _Vertex_VertexState
                        let _Topology_PrimitiveTopology = x.Primitive.Topology.GetValue()
                        let _StripIndexFormat_IndexFormat = x.Primitive.StripIndexFormat.GetValue()
                        let _FrontFace_FrontFace = x.Primitive.FrontFace.GetValue()
                        let _CullMode_CullMode = x.Primitive.CullMode.GetValue()
                        let _Primitive_PrimitiveState = new DawnRaw.WGPUPrimitiveState()
                        _Primitive_PrimitiveState.Topology <- _Topology_PrimitiveTopology
                        _Primitive_PrimitiveState.StripIndexFormat <- _StripIndexFormat_IndexFormat
                        _Primitive_PrimitiveState.FrontFace <- _FrontFace_FrontFace
                        _Primitive_PrimitiveState.CullMode <- _CullMode_CullMode
                        let _Primitive_PrimitiveState = _Primitive_PrimitiveState
                        let inline _DepthStencil_DepthStencilStateOptCont _DepthStencil_DepthStencilStateOpt = 
                            let _Count_int = uint32 (x.Multisample.Count)
                            let _Mask_int = uint32 (x.Multisample.Mask)
                            let _AlphaToCoverageEnabled_bool = x.Multisample.AlphaToCoverageEnabled
                            let _Multisample_MultisampleState = new DawnRaw.WGPUMultisampleState()
                            _Multisample_MultisampleState.Count <- _Count_int
                            _Multisample_MultisampleState.Mask <- _Mask_int
                            _Multisample_MultisampleState.AlphaToCoverageEnabled <- _AlphaToCoverageEnabled_bool
                            let _Multisample_MultisampleState = _Multisample_MultisampleState
                            let inline _Fragment_FragmentStateOptCont _Fragment_FragmentStateOpt = 
                                let native = DawnRaw.WGPURenderPipelineDescriptor()
                                native.Label <- _Label_string
                                native.Layout <- _Layout_PipelineLayout
                                native.Vertex <- _Vertex_VertexState
                                native.Primitive <- _Primitive_PrimitiveState
                                native.DepthStencil <- _DepthStencil_DepthStencilStateOpt
                                native.Multisample <- _Multisample_MultisampleState
                                native.Fragment <- _Fragment_FragmentStateOpt
                                callback native
                            match x.Fragment with
                            | Some v ->
                                let _Module_ShaderModule = (if isNull v.Module then null else v.Module.Handle)
                                let _EntryPoint_string = v.EntryPoint
                                let _Constants_ConstantEntryArrCount = if isNull v.Constants then 0 else v.Constants.Length
                                let rec _Constants_ConstantEntryArrCont (_Constants_ConstantEntryArrinputs : array<ConstantEntry>) (_Constants_ConstantEntryArroutputs : JsArray) (_Constants_ConstantEntryArri : int) =
                                    if _Constants_ConstantEntryArri >= _Constants_ConstantEntryArrCount then
                                        let _Constants_ConstantEntryArr = _Constants_ConstantEntryArroutputs.Reference
                                        let _Targets_ColorTargetStateArrCount = if isNull v.Targets then 0 else v.Targets.Length
                                        let rec _Targets_ColorTargetStateArrCont (_Targets_ColorTargetStateArrinputs : array<ColorTargetState>) (_Targets_ColorTargetStateArroutputs : JsArray) (_Targets_ColorTargetStateArri : int) =
                                            if _Targets_ColorTargetStateArri >= _Targets_ColorTargetStateArrCount then
                                                let _Targets_ColorTargetStateArr = _Targets_ColorTargetStateArroutputs.Reference
                                                let _n = new DawnRaw.WGPUFragmentState()
                                                _n.Module <- _Module_ShaderModule
                                                _n.EntryPoint <- _EntryPoint_string
                                                _n.Constants <- _Constants_ConstantEntryArr
                                                _n.Targets <- _Targets_ColorTargetStateArr
                                                let _n = _n
                                                _Fragment_FragmentStateOptCont _n
                                            else
                                                let _Format_TextureFormat = _Targets_ColorTargetStateArrinputs.[_Targets_ColorTargetStateArri].Format.GetValue()
                                                let inline _Blend_BlendStateOptCont _Blend_BlendStateOpt = 
                                                    let _WriteMask_ColorWriteMask = int (_Targets_ColorTargetStateArrinputs.[_Targets_ColorTargetStateArri].WriteMask)
                                                    let _n = new DawnRaw.WGPUColorTargetState()
                                                    _n.Format <- _Format_TextureFormat
                                                    _n.Blend <- _Blend_BlendStateOpt
                                                    _n.WriteMask <- _WriteMask_ColorWriteMask
                                                    let _n = _n
                                                    _Targets_ColorTargetStateArroutputs.[_Targets_ColorTargetStateArri] <- js _n
                                                    _Targets_ColorTargetStateArrCont _Targets_ColorTargetStateArrinputs _Targets_ColorTargetStateArroutputs (_Targets_ColorTargetStateArri + 1)
                                                match _Targets_ColorTargetStateArrinputs.[_Targets_ColorTargetStateArri].Blend with
                                                | Some v ->
                                                    let _Operation_BlendOperation = v.Color.Operation.GetValue()
                                                    let _SrcFactor_BlendFactor = v.Color.SrcFactor.GetValue()
                                                    let _DstFactor_BlendFactor = v.Color.DstFactor.GetValue()
                                                    let _Color_BlendComponent = new DawnRaw.WGPUBlendComponent()
                                                    _Color_BlendComponent.Operation <- _Operation_BlendOperation
                                                    _Color_BlendComponent.SrcFactor <- _SrcFactor_BlendFactor
                                                    _Color_BlendComponent.DstFactor <- _DstFactor_BlendFactor
                                                    let _Color_BlendComponent = _Color_BlendComponent
                                                    let _Operation_BlendOperation = v.Alpha.Operation.GetValue()
                                                    let _SrcFactor_BlendFactor = v.Alpha.SrcFactor.GetValue()
                                                    let _DstFactor_BlendFactor = v.Alpha.DstFactor.GetValue()
                                                    let _Alpha_BlendComponent = new DawnRaw.WGPUBlendComponent()
                                                    _Alpha_BlendComponent.Operation <- _Operation_BlendOperation
                                                    _Alpha_BlendComponent.SrcFactor <- _SrcFactor_BlendFactor
                                                    _Alpha_BlendComponent.DstFactor <- _DstFactor_BlendFactor
                                                    let _Alpha_BlendComponent = _Alpha_BlendComponent
                                                    let _n = new DawnRaw.WGPUBlendState()
                                                    _n.Color <- _Color_BlendComponent
                                                    _n.Alpha <- _Alpha_BlendComponent
                                                    let _n = _n
                                                    _Blend_BlendStateOptCont _n
                                                | None -> _Blend_BlendStateOptCont null
                                        _Targets_ColorTargetStateArrCont v.Targets (if _Targets_ColorTargetStateArrCount > 0 then newArray _Targets_ColorTargetStateArrCount else null) 0
                                    else
                                        let _Key_string = _Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Key
                                        let _Value_float = (_Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Value)
                                        let _n = new DawnRaw.WGPUConstantEntry()
                                        _n.Key <- _Key_string
                                        _n.Value <- _Value_float
                                        let _n = _n
                                        _Constants_ConstantEntryArroutputs.[_Constants_ConstantEntryArri] <- js _n
                                        _Constants_ConstantEntryArrCont _Constants_ConstantEntryArrinputs _Constants_ConstantEntryArroutputs (_Constants_ConstantEntryArri + 1)
                                _Constants_ConstantEntryArrCont v.Constants (if _Constants_ConstantEntryArrCount > 0 then newArray _Constants_ConstantEntryArrCount else null) 0
                            | None -> _Fragment_FragmentStateOptCont null
                        match x.DepthStencil with
                        | Some v ->
                            let _Format_TextureFormat = v.Format.GetValue()
                            let _DepthWriteEnabled_bool = v.DepthWriteEnabled
                            let _DepthCompare_CompareFunction = v.DepthCompare.GetValue()
                            let _Compare_CompareFunction = v.StencilFront.Compare.GetValue()
                            let _FailOp_StencilOperation = v.StencilFront.FailOp.GetValue()
                            let _DepthFailOp_StencilOperation = v.StencilFront.DepthFailOp.GetValue()
                            let _PassOp_StencilOperation = v.StencilFront.PassOp.GetValue()
                            let _StencilFront_StencilFaceState = new DawnRaw.WGPUStencilFaceState()
                            _StencilFront_StencilFaceState.Compare <- _Compare_CompareFunction
                            _StencilFront_StencilFaceState.FailOp <- _FailOp_StencilOperation
                            _StencilFront_StencilFaceState.DepthFailOp <- _DepthFailOp_StencilOperation
                            _StencilFront_StencilFaceState.PassOp <- _PassOp_StencilOperation
                            let _StencilFront_StencilFaceState = _StencilFront_StencilFaceState
                            let _Compare_CompareFunction = v.StencilBack.Compare.GetValue()
                            let _FailOp_StencilOperation = v.StencilBack.FailOp.GetValue()
                            let _DepthFailOp_StencilOperation = v.StencilBack.DepthFailOp.GetValue()
                            let _PassOp_StencilOperation = v.StencilBack.PassOp.GetValue()
                            let _StencilBack_StencilFaceState = new DawnRaw.WGPUStencilFaceState()
                            _StencilBack_StencilFaceState.Compare <- _Compare_CompareFunction
                            _StencilBack_StencilFaceState.FailOp <- _FailOp_StencilOperation
                            _StencilBack_StencilFaceState.DepthFailOp <- _DepthFailOp_StencilOperation
                            _StencilBack_StencilFaceState.PassOp <- _PassOp_StencilOperation
                            let _StencilBack_StencilFaceState = _StencilBack_StencilFaceState
                            let _StencilReadMask_int = uint32 (v.StencilReadMask)
                            let _StencilWriteMask_int = uint32 (v.StencilWriteMask)
                            let _DepthBias_int32 = int32 (v.DepthBias)
                            let _DepthBiasSlopeScale_float32 = (v.DepthBiasSlopeScale)
                            let _DepthBiasClamp_float32 = (v.DepthBiasClamp)
                            let _n = new DawnRaw.WGPUDepthStencilState()
                            _n.Format <- _Format_TextureFormat
                            _n.DepthWriteEnabled <- _DepthWriteEnabled_bool
                            _n.DepthCompare <- _DepthCompare_CompareFunction
                            _n.StencilFront <- _StencilFront_StencilFaceState
                            _n.StencilBack <- _StencilBack_StencilFaceState
                            _n.StencilReadMask <- _StencilReadMask_int
                            _n.StencilWriteMask <- _StencilWriteMask_int
                            _n.DepthBias <- _DepthBias_int32
                            _n.DepthBiasSlopeScale <- _DepthBiasSlopeScale_float32
                            _n.DepthBiasClamp <- _DepthBiasClamp_float32
                            let _n = _n
                            _DepthStencil_DepthStencilStateOptCont _n
                        | None -> _DepthStencil_DepthStencilStateOptCont null
                    else
                        let _ArrayStride_uint64 = float (_Buffers_VertexBufferLayoutArrinputs.[_Buffers_VertexBufferLayoutArri].ArrayStride)
                        let _StepMode_VertexStepMode = _Buffers_VertexBufferLayoutArrinputs.[_Buffers_VertexBufferLayoutArri].StepMode.GetValue()
                        let _Attributes_VertexAttributeArrCount = if isNull _Buffers_VertexBufferLayoutArrinputs.[_Buffers_VertexBufferLayoutArri].Attributes then 0 else _Buffers_VertexBufferLayoutArrinputs.[_Buffers_VertexBufferLayoutArri].Attributes.Length
                        let rec _Attributes_VertexAttributeArrCont (_Attributes_VertexAttributeArrinputs : array<VertexAttribute>) (_Attributes_VertexAttributeArroutputs : JsArray) (_Attributes_VertexAttributeArri : int) =
                            if _Attributes_VertexAttributeArri >= _Attributes_VertexAttributeArrCount then
                                let _Attributes_VertexAttributeArr = _Attributes_VertexAttributeArroutputs.Reference
                                let _n = new DawnRaw.WGPUVertexBufferLayout()
                                _n.ArrayStride <- _ArrayStride_uint64
                                _n.StepMode <- _StepMode_VertexStepMode
                                _n.Attributes <- _Attributes_VertexAttributeArr
                                let _n = _n
                                _Buffers_VertexBufferLayoutArroutputs.[_Buffers_VertexBufferLayoutArri] <- js _n
                                _Buffers_VertexBufferLayoutArrCont _Buffers_VertexBufferLayoutArrinputs _Buffers_VertexBufferLayoutArroutputs (_Buffers_VertexBufferLayoutArri + 1)
                            else
                                let _Format_VertexFormat = _Attributes_VertexAttributeArrinputs.[_Attributes_VertexAttributeArri].Format.GetValue()
                                let _Offset_uint64 = float (_Attributes_VertexAttributeArrinputs.[_Attributes_VertexAttributeArri].Offset)
                                let _ShaderLocation_int = uint32 (_Attributes_VertexAttributeArrinputs.[_Attributes_VertexAttributeArri].ShaderLocation)
                                let _n = new DawnRaw.WGPUVertexAttribute()
                                _n.Format <- _Format_VertexFormat
                                _n.Offset <- _Offset_uint64
                                _n.ShaderLocation <- _ShaderLocation_int
                                let _n = _n
                                _Attributes_VertexAttributeArroutputs.[_Attributes_VertexAttributeArri] <- js _n
                                _Attributes_VertexAttributeArrCont _Attributes_VertexAttributeArrinputs _Attributes_VertexAttributeArroutputs (_Attributes_VertexAttributeArri + 1)
                        _Attributes_VertexAttributeArrCont _Buffers_VertexBufferLayoutArrinputs.[_Buffers_VertexBufferLayoutArri].Attributes (if _Attributes_VertexAttributeArrCount > 0 then newArray _Attributes_VertexAttributeArrCount else null) 0
                _Buffers_VertexBufferLayoutArrCont x.Vertex.Buffers (if _Buffers_VertexBufferLayoutArrCount > 0 then newArray _Buffers_VertexBufferLayoutArrCount else null) 0
            else
                let _Key_string = _Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Key
                let _Value_float = (_Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Value)
                let _n = new DawnRaw.WGPUConstantEntry()
                _n.Key <- _Key_string
                _n.Value <- _Value_float
                let _n = _n
                _Constants_ConstantEntryArroutputs.[_Constants_ConstantEntryArri] <- js _n
                _Constants_ConstantEntryArrCont _Constants_ConstantEntryArrinputs _Constants_ConstantEntryArroutputs (_Constants_ConstantEntryArri + 1)
        _Constants_ConstantEntryArrCont x.Vertex.Constants (if _Constants_ConstantEntryArrCount > 0 then newArray _Constants_ConstantEntryArrCount else null) 0


[<AllowNullLiteral>]
type Adapter(device : Device, handle : AdapterHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle : AdapterHandle = handle
    member x.IsDisposed = isDisposed
    member private x.Dispose(disposing : bool) =
        if not isDisposed then 
            let r = Interlocked.Decrement(&refCount.contents)
            isDisposed <- true
    member x.Dispose() = x.Dispose(true)
    member x.Clone() = 
        let mutable o = refCount.contents
        if o = 0 then raise <| System.ObjectDisposedException("Adapter")
        let mutable n = Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        while o <> n do
            o <- n
            if o = 0 then raise <| System.ObjectDisposedException("Adapter")
            n <- Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        new Adapter(device, handle, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : AdapterHandle) = new Adapter(device, handle, ref 1)
[<AllowNullLiteral>]
type BindGroupLayout(device : Device, handle : BindGroupLayoutHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle : BindGroupLayoutHandle = handle
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
type TextureView(device : Device, handle : TextureViewHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle : TextureViewHandle = handle
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
type CommandBuffer(device : Device, handle : CommandBufferHandle, descriptor : CommandBufferDescriptor, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.Descriptor = descriptor
    member x.ReferenceCount = !refCount
    member x.Handle : CommandBufferHandle = handle
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
        new CommandBuffer(device, handle, descriptor, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : CommandBufferHandle, descriptor : CommandBufferDescriptor) = new CommandBuffer(device, handle, descriptor, ref 1)
[<AllowNullLiteral>]
type RenderBundle(device : Device, handle : RenderBundleHandle, descriptor : RenderBundleDescriptor, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.Descriptor = descriptor
    member x.ReferenceCount = !refCount
    member x.Handle : RenderBundleHandle = handle
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
        new RenderBundle(device, handle, descriptor, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : RenderBundleHandle, descriptor : RenderBundleDescriptor) = new RenderBundle(device, handle, descriptor, ref 1)
[<AllowNullLiteral>]
type Surface(device : Device, handle : SurfaceHandle, descriptor : SurfaceDescriptor, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.Descriptor = descriptor
    member x.ReferenceCount = !refCount
    member x.Handle : SurfaceHandle = handle
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
        new Surface(device, handle, descriptor, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : SurfaceHandle, descriptor : SurfaceDescriptor) = new Surface(device, handle, descriptor, ref 1)
    member x.GetPreferredFormat(Adapter : Adapter) : TextureFormat = 
        let _Adapter_Adapter = (if isNull Adapter then null else Adapter.Handle)
        x.Handle.Reference.Invoke("getPreferredFormat", js _Adapter_Adapter) |> System.Convert.ToInt32 |> unbox<TextureFormat>
[<AllowNullLiteral>]
type SwapChain(device : Device, handle : SwapChainHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle : SwapChainHandle = handle
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
        let _Format_TextureFormat = Format.GetValue()
        let _AllowedUsage_TextureUsage = int (AllowedUsage)
        let _Width_int = uint32 (Width)
        let _Height_int = uint32 (Height)
        x.Handle.Reference.Invoke("configure", js _Format_TextureFormat, js _AllowedUsage_TextureUsage, js _Width_int, js _Height_int) |> ignore
    member x.GetCurrentTextureView() : TextureView = 
        new TextureView(x.Device, convert(x.Handle.Reference.Invoke("getCurrentTextureView")))
    member x.Present() : unit = 
        x.Handle.Reference.Invoke("present") |> ignore
[<AllowNullLiteral>]
type Buffer(device : Device, handle : BufferHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle : BufferHandle = handle
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
    member x.MapAsync(Mode : MapMode, Offset : unativeint, Size : unativeint, Callback : BufferMapCallback) : unit = 
        let _Mode_MapMode = int (Mode)
        let _Offset_unativeint = float (Offset)
        let _Size_unativeint = float (Size)
        let mutable _Callback_BufferMapCallbackGC = Unchecked.defaultof<System.Runtime.InteropServices.GCHandle>
        let _Callback_BufferMapCallbackFunction (Status : obj) (Userdata : float) = 
            let _Status_BufferMapAsyncStatus = Status |> System.Convert.ToInt32 |> unbox<BufferMapAsyncStatus>
            let _Userdata_nativeint = Userdata
            if _Callback_BufferMapCallbackGC.IsAllocated then _Callback_BufferMapCallbackGC.Free()
            Callback.Invoke(BufferMapAsyncStatus.Parse(_Status_BufferMapAsyncStatus), nativeint _Userdata_nativeint)
        let _Callback_BufferMapCallbackDel = WGPUBufferMapCallback(_Callback_BufferMapCallbackFunction)
        _Callback_BufferMapCallbackGC <- System.Runtime.InteropServices.GCHandle.Alloc(_Callback_BufferMapCallbackDel)
        let _Callback_BufferMapCallback = _Callback_BufferMapCallbackDel
        x.Handle.Reference.Invoke("mapAsync", js _Mode_MapMode, js _Offset_unativeint, js _Size_unativeint, js _Callback_BufferMapCallback) |> ignore
    member x.MapAsync(Mode : MapMode, Offset : unativeint, Size : unativeint, Callback : BufferMapCallback, Userdata : nativeint) : unit = 
        let _Mode_MapMode = int (Mode)
        let _Offset_unativeint = float (Offset)
        let _Size_unativeint = float (Size)
        let mutable _Callback_BufferMapCallbackGC = Unchecked.defaultof<System.Runtime.InteropServices.GCHandle>
        let _Callback_BufferMapCallbackFunction (Status : obj) (Userdata : float) = 
            let _Status_BufferMapAsyncStatus = Status |> System.Convert.ToInt32 |> unbox<BufferMapAsyncStatus>
            let _Userdata_nativeint = Userdata
            if _Callback_BufferMapCallbackGC.IsAllocated then _Callback_BufferMapCallbackGC.Free()
            Callback.Invoke(BufferMapAsyncStatus.Parse(_Status_BufferMapAsyncStatus), nativeint _Userdata_nativeint)
        let _Callback_BufferMapCallbackDel = WGPUBufferMapCallback(_Callback_BufferMapCallbackFunction)
        _Callback_BufferMapCallbackGC <- System.Runtime.InteropServices.GCHandle.Alloc(_Callback_BufferMapCallbackDel)
        let _Callback_BufferMapCallback = _Callback_BufferMapCallbackDel
        let _Userdata_nativeint = float (Userdata)
        x.Handle.Reference.Invoke("mapAsync", js _Mode_MapMode, js _Offset_unativeint, js _Size_unativeint, js _Callback_BufferMapCallback, js _Userdata_nativeint) |> ignore
    member x.GetMappedRange() : ArrayBuffer = 
        x.Handle.Reference.Invoke("getMappedRange") |> unbox<ArrayBuffer>
    member x.GetMappedRange(Offset : unativeint) : ArrayBuffer = 
        let _Offset_unativeint = float (Offset)
        x.Handle.Reference.Invoke("getMappedRange", js _Offset_unativeint) |> unbox<ArrayBuffer>
    member x.GetMappedRange(Offset : unativeint, Size : unativeint) : ArrayBuffer = 
        let _Offset_unativeint = float (Offset)
        let _Size_unativeint = float (Size)
        x.Handle.Reference.Invoke("getMappedRange", js _Offset_unativeint, js _Size_unativeint) |> unbox<ArrayBuffer>
    member x.GetConstMappedRange() : ArrayBuffer = 
        x.Handle.Reference.Invoke("getConstMappedRange") |> unbox<ArrayBuffer>
    member x.GetConstMappedRange(Offset : unativeint) : ArrayBuffer = 
        let _Offset_unativeint = float (Offset)
        x.Handle.Reference.Invoke("getConstMappedRange", js _Offset_unativeint) |> unbox<ArrayBuffer>
    member x.GetConstMappedRange(Offset : unativeint, Size : unativeint) : ArrayBuffer = 
        let _Offset_unativeint = float (Offset)
        let _Size_unativeint = float (Size)
        x.Handle.Reference.Invoke("getConstMappedRange", js _Offset_unativeint, js _Size_unativeint) |> unbox<ArrayBuffer>
    member x.SetLabel() : unit = 
        x.Handle.Reference.Invoke("setLabel") |> ignore
    member x.SetLabel(Label : string) : unit = 
        let _Label_string = Label
        x.Handle.Reference.Invoke("setLabel", js _Label_string) |> ignore
    member x.Unmap() : unit = 
        x.Handle.Reference.Invoke("unmap") |> ignore
    member x.Destroy() : unit = 
        x.Handle.Reference.Invoke("destroy") |> ignore
[<AllowNullLiteral>]
type ExternalTexture(device : Device, handle : ExternalTextureHandle, externalTextureDescriptor : ExternalTextureDescriptor, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ExternalTextureDescriptor = externalTextureDescriptor
    member x.ReferenceCount = !refCount
    member x.Handle : ExternalTextureHandle = handle
    member x.IsDisposed = isDisposed
    member private x.Dispose(disposing : bool) =
        if not isDisposed then 
            let r = Interlocked.Decrement(&refCount.contents)
            isDisposed <- true
    member x.Dispose() = x.Dispose(true)
    member x.Clone() = 
        let mutable o = refCount.contents
        if o = 0 then raise <| System.ObjectDisposedException("ExternalTexture")
        let mutable n = Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        while o <> n do
            o <- n
            if o = 0 then raise <| System.ObjectDisposedException("ExternalTexture")
            n <- Interlocked.CompareExchange(&refCount.contents, o + 1, o)
        new ExternalTexture(device, handle, externalTextureDescriptor, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : ExternalTextureHandle, externalTextureDescriptor : ExternalTextureDescriptor) = new ExternalTexture(device, handle, externalTextureDescriptor, ref 1)
    member x.Destroy() : unit = 
        x.Handle.Reference.Invoke("destroy") |> ignore
[<AllowNullLiteral>]
type PipelineLayout(device : Device, handle : PipelineLayoutHandle, descriptor : PipelineLayoutDescriptor, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.Descriptor = descriptor
    member x.ReferenceCount = !refCount
    member x.Handle : PipelineLayoutHandle = handle
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
        new PipelineLayout(device, handle, descriptor, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : PipelineLayoutHandle, descriptor : PipelineLayoutDescriptor) = new PipelineLayout(device, handle, descriptor, ref 1)
[<AllowNullLiteral>]
type QuerySet(device : Device, handle : QuerySetHandle, descriptor : QuerySetDescriptor, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.Descriptor = descriptor
    member x.ReferenceCount = !refCount
    member x.Handle : QuerySetHandle = handle
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
        new QuerySet(device, handle, descriptor, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : QuerySetHandle, descriptor : QuerySetDescriptor) = new QuerySet(device, handle, descriptor, ref 1)
    member x.Destroy() : unit = 
        x.Handle.Reference.Invoke("destroy") |> ignore
[<AllowNullLiteral>]
type Sampler(device : Device, handle : SamplerHandle, descriptor : SamplerDescriptor, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.Descriptor = descriptor
    member x.ReferenceCount = !refCount
    member x.Handle : SamplerHandle = handle
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
        new Sampler(device, handle, descriptor, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : SamplerHandle, descriptor : SamplerDescriptor) = new Sampler(device, handle, descriptor, ref 1)
[<AllowNullLiteral>]
type Texture(device : Device, handle : TextureHandle, descriptor : TextureDescriptor, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.Descriptor = descriptor
    member x.ReferenceCount = !refCount
    member x.Handle : TextureHandle = handle
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
        new Texture(device, handle, descriptor, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : TextureHandle, descriptor : TextureDescriptor) = new Texture(device, handle, descriptor, ref 1)
    member x.CreateView(Descriptor : TextureViewDescriptor) : TextureView = 
        let _Label_string = Descriptor.Label
        let _Format_TextureFormat = Descriptor.Format.GetValue()
        let _Dimension_TextureViewDimension = Descriptor.Dimension.GetValue()
        let _BaseMipLevel_int = uint32 (Descriptor.BaseMipLevel)
        let _MipLevelCount_int = uint32 (Descriptor.MipLevelCount)
        let _BaseArrayLayer_int = uint32 (Descriptor.BaseArrayLayer)
        let _ArrayLayerCount_int = uint32 (Descriptor.ArrayLayerCount)
        let _Aspect_TextureAspect = Descriptor.Aspect.GetValue()
        let _Descriptor_TextureViewDescriptor = new DawnRaw.WGPUTextureViewDescriptor()
        _Descriptor_TextureViewDescriptor.Label <- _Label_string
        _Descriptor_TextureViewDescriptor.Format <- _Format_TextureFormat
        _Descriptor_TextureViewDescriptor.Dimension <- _Dimension_TextureViewDimension
        _Descriptor_TextureViewDescriptor.BaseMipLevel <- _BaseMipLevel_int
        _Descriptor_TextureViewDescriptor.MipLevelCount <- _MipLevelCount_int
        _Descriptor_TextureViewDescriptor.BaseArrayLayer <- _BaseArrayLayer_int
        _Descriptor_TextureViewDescriptor.ArrayLayerCount <- _ArrayLayerCount_int
        _Descriptor_TextureViewDescriptor.Aspect <- _Aspect_TextureAspect
        let _Descriptor_TextureViewDescriptor = _Descriptor_TextureViewDescriptor
        new TextureView(x.Device, convert(x.Handle.Reference.Invoke("createView", js _Descriptor_TextureViewDescriptor)))
    member x.SetLabel() : unit = 
        x.Handle.Reference.Invoke("setLabel") |> ignore
    member x.SetLabel(Label : string) : unit = 
        let _Label_string = Label
        x.Handle.Reference.Invoke("setLabel", js _Label_string) |> ignore
    member x.Destroy() : unit = 
        x.Handle.Reference.Invoke("destroy") |> ignore
[<AllowNullLiteral>]
type Instance(device : Device, handle : InstanceHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle : InstanceHandle = handle
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
        new Surface(x.Device, convert(x.Handle.Reference.Invoke("createSurface")), SurfaceDescriptor.Default)
    member x.CreateSurface(Descriptor : SurfaceDescriptor) : Surface = 
        let _Label_string = Descriptor.Label
        let _Descriptor_SurfaceDescriptor = new DawnRaw.WGPUSurfaceDescriptor()
        _Descriptor_SurfaceDescriptor.Label <- _Label_string
        let _Descriptor_SurfaceDescriptor = _Descriptor_SurfaceDescriptor
        new Surface(x.Device, convert(x.Handle.Reference.Invoke("createSurface", js _Descriptor_SurfaceDescriptor)), Descriptor)
    member x.ProcessEvents() : unit = 
        x.Handle.Reference.Invoke("processEvents") |> ignore
    member x.RequestAdapter(Options : RequestAdapterOptions, Callback : RequestAdapterCallback) : unit = 
        let _CompatibleSurface_Surface = (if isNull Options.CompatibleSurface then null else Options.CompatibleSurface.Handle)
        let _PowerPreference_PowerPreference = Options.PowerPreference.GetValue()
        let _ForceFallbackAdapter_bool = Options.ForceFallbackAdapter
        let _Options_RequestAdapterOptions = new DawnRaw.WGPURequestAdapterOptions()
        _Options_RequestAdapterOptions.CompatibleSurface <- _CompatibleSurface_Surface
        _Options_RequestAdapterOptions.PowerPreference <- _PowerPreference_PowerPreference
        _Options_RequestAdapterOptions.ForceFallbackAdapter <- _ForceFallbackAdapter_bool
        let _Options_RequestAdapterOptions = _Options_RequestAdapterOptions
        let mutable _Callback_RequestAdapterCallbackGC = Unchecked.defaultof<System.Runtime.InteropServices.GCHandle>
        let _Callback_RequestAdapterCallbackFunction (Status : obj) (Adapter : AdapterHandle) (Message : string) (Userdata : float) = 
            let _Status_RequestAdapterStatus = Status |> System.Convert.ToInt32 |> unbox<RequestAdapterStatus>
            let _Adapter_Adapter = Adapter
            let _Message_string = Message
            let _Userdata_nativeint = Userdata
            if _Callback_RequestAdapterCallbackGC.IsAllocated then _Callback_RequestAdapterCallbackGC.Free()
            Callback.Invoke(RequestAdapterStatus.Parse(_Status_RequestAdapterStatus), new Adapter(x.Device, _Adapter_Adapter), _Message_string, nativeint _Userdata_nativeint)
        let _Callback_RequestAdapterCallbackDel = WGPURequestAdapterCallback(_Callback_RequestAdapterCallbackFunction)
        _Callback_RequestAdapterCallbackGC <- System.Runtime.InteropServices.GCHandle.Alloc(_Callback_RequestAdapterCallbackDel)
        let _Callback_RequestAdapterCallback = _Callback_RequestAdapterCallbackDel
        x.Handle.Reference.Invoke("requestAdapter", js _Options_RequestAdapterOptions, js _Callback_RequestAdapterCallback) |> ignore
    member x.RequestAdapter(Options : RequestAdapterOptions, Callback : RequestAdapterCallback, Userdata : nativeint) : unit = 
        let _CompatibleSurface_Surface = (if isNull Options.CompatibleSurface then null else Options.CompatibleSurface.Handle)
        let _PowerPreference_PowerPreference = Options.PowerPreference.GetValue()
        let _ForceFallbackAdapter_bool = Options.ForceFallbackAdapter
        let _Options_RequestAdapterOptions = new DawnRaw.WGPURequestAdapterOptions()
        _Options_RequestAdapterOptions.CompatibleSurface <- _CompatibleSurface_Surface
        _Options_RequestAdapterOptions.PowerPreference <- _PowerPreference_PowerPreference
        _Options_RequestAdapterOptions.ForceFallbackAdapter <- _ForceFallbackAdapter_bool
        let _Options_RequestAdapterOptions = _Options_RequestAdapterOptions
        let mutable _Callback_RequestAdapterCallbackGC = Unchecked.defaultof<System.Runtime.InteropServices.GCHandle>
        let _Callback_RequestAdapterCallbackFunction (Status : obj) (Adapter : AdapterHandle) (Message : string) (Userdata : float) = 
            let _Status_RequestAdapterStatus = Status |> System.Convert.ToInt32 |> unbox<RequestAdapterStatus>
            let _Adapter_Adapter = Adapter
            let _Message_string = Message
            let _Userdata_nativeint = Userdata
            if _Callback_RequestAdapterCallbackGC.IsAllocated then _Callback_RequestAdapterCallbackGC.Free()
            Callback.Invoke(RequestAdapterStatus.Parse(_Status_RequestAdapterStatus), new Adapter(x.Device, _Adapter_Adapter), _Message_string, nativeint _Userdata_nativeint)
        let _Callback_RequestAdapterCallbackDel = WGPURequestAdapterCallback(_Callback_RequestAdapterCallbackFunction)
        _Callback_RequestAdapterCallbackGC <- System.Runtime.InteropServices.GCHandle.Alloc(_Callback_RequestAdapterCallbackDel)
        let _Callback_RequestAdapterCallback = _Callback_RequestAdapterCallbackDel
        let _Userdata_nativeint = float (Userdata)
        x.Handle.Reference.Invoke("requestAdapter", js _Options_RequestAdapterOptions, js _Callback_RequestAdapterCallback, js _Userdata_nativeint) |> ignore
[<AllowNullLiteral>]
type Queue(device : Device, handle : QueueHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.ReferenceCount = !refCount
    member x.Handle : QueueHandle = handle
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
        let _Commands_CommandBufferArrCount = Commands.Length
        let _Commands_CommandBufferArrArray = newArray _Commands_CommandBufferArrCount
        for i in 0 .. _Commands_CommandBufferArrCount-1 do
            if isNull Commands.[i] then _Commands_CommandBufferArrArray.[i] <- null
            else _Commands_CommandBufferArrArray.[i] <- Commands.[i].Handle
        let _Commands_CommandBufferArr = _Commands_CommandBufferArrArray.Reference
        x.Handle.Reference.Invoke("submit", js _Commands_CommandBufferArr) |> ignore
    member x.OnSubmittedWorkDone(SignalValue : uint64, Callback : QueueWorkDoneCallback) : unit = 
        let _SignalValue_uint64 = float (SignalValue)
        let mutable _Callback_QueueWorkDoneCallbackGC = Unchecked.defaultof<System.Runtime.InteropServices.GCHandle>
        let _Callback_QueueWorkDoneCallbackFunction (Status : obj) (Userdata : float) = 
            let _Status_QueueWorkDoneStatus = Status |> System.Convert.ToInt32 |> unbox<QueueWorkDoneStatus>
            let _Userdata_nativeint = Userdata
            if _Callback_QueueWorkDoneCallbackGC.IsAllocated then _Callback_QueueWorkDoneCallbackGC.Free()
            Callback.Invoke(QueueWorkDoneStatus.Parse(_Status_QueueWorkDoneStatus), nativeint _Userdata_nativeint)
        let _Callback_QueueWorkDoneCallbackDel = WGPUQueueWorkDoneCallback(_Callback_QueueWorkDoneCallbackFunction)
        _Callback_QueueWorkDoneCallbackGC <- System.Runtime.InteropServices.GCHandle.Alloc(_Callback_QueueWorkDoneCallbackDel)
        let _Callback_QueueWorkDoneCallback = _Callback_QueueWorkDoneCallbackDel
        x.Handle.Reference.Invoke("onSubmittedWorkDone", js _SignalValue_uint64, js _Callback_QueueWorkDoneCallback) |> ignore
    member x.OnSubmittedWorkDone(SignalValue : uint64, Callback : QueueWorkDoneCallback, Userdata : nativeint) : unit = 
        let _SignalValue_uint64 = float (SignalValue)
        let mutable _Callback_QueueWorkDoneCallbackGC = Unchecked.defaultof<System.Runtime.InteropServices.GCHandle>
        let _Callback_QueueWorkDoneCallbackFunction (Status : obj) (Userdata : float) = 
            let _Status_QueueWorkDoneStatus = Status |> System.Convert.ToInt32 |> unbox<QueueWorkDoneStatus>
            let _Userdata_nativeint = Userdata
            if _Callback_QueueWorkDoneCallbackGC.IsAllocated then _Callback_QueueWorkDoneCallbackGC.Free()
            Callback.Invoke(QueueWorkDoneStatus.Parse(_Status_QueueWorkDoneStatus), nativeint _Userdata_nativeint)
        let _Callback_QueueWorkDoneCallbackDel = WGPUQueueWorkDoneCallback(_Callback_QueueWorkDoneCallbackFunction)
        _Callback_QueueWorkDoneCallbackGC <- System.Runtime.InteropServices.GCHandle.Alloc(_Callback_QueueWorkDoneCallbackDel)
        let _Callback_QueueWorkDoneCallback = _Callback_QueueWorkDoneCallbackDel
        let _Userdata_nativeint = float (Userdata)
        x.Handle.Reference.Invoke("onSubmittedWorkDone", js _SignalValue_uint64, js _Callback_QueueWorkDoneCallback, js _Userdata_nativeint) |> ignore
    member x.WriteBuffer(Buffer : Buffer, BufferOffset : uint64, Data : nativeint, Size : unativeint) : unit = 
        let _Buffer_Buffer = (if isNull Buffer then null else Buffer.Handle)
        let _BufferOffset_uint64 = float (BufferOffset)
        let _Data_nativeint = float (Data)
        let _Size_unativeint = float (Size)
        x.Handle.Reference.Invoke("writeBuffer", js _Buffer_Buffer, js _BufferOffset_uint64, js _Data_nativeint, js _Size_unativeint) |> ignore
    member x.WriteTexture(Destination : ImageCopyTexture, Data : nativeint, DataSize : unativeint, DataLayout : TextureDataLayout, WriteSize : Extent3D) : unit = 
        let _Texture_Texture = (if isNull Destination.Texture then null else Destination.Texture.Handle)
        let _MipLevel_int = uint32 (Destination.MipLevel)
        let _X_int = uint32 (Destination.Origin.X)
        let _Y_int = uint32 (Destination.Origin.Y)
        let _Z_int = uint32 (Destination.Origin.Z)
        let _Origin_Origin3D = new DawnRaw.WGPUOrigin3D()
        _Origin_Origin3D.X <- _X_int
        _Origin_Origin3D.Y <- _Y_int
        _Origin_Origin3D.Z <- _Z_int
        let _Origin_Origin3D = _Origin_Origin3D
        let _Aspect_TextureAspect = Destination.Aspect.GetValue()
        let _Destination_ImageCopyTexture = new DawnRaw.WGPUImageCopyTexture()
        _Destination_ImageCopyTexture.Texture <- _Texture_Texture
        _Destination_ImageCopyTexture.MipLevel <- _MipLevel_int
        _Destination_ImageCopyTexture.Origin <- _Origin_Origin3D
        _Destination_ImageCopyTexture.Aspect <- _Aspect_TextureAspect
        let _Destination_ImageCopyTexture = _Destination_ImageCopyTexture
        let _Data_nativeint = float (Data)
        let _DataSize_unativeint = float (DataSize)
        let _Offset_uint64 = float (DataLayout.Offset)
        let _BytesPerRow_int = uint32 (DataLayout.BytesPerRow)
        let _RowsPerImage_int = uint32 (DataLayout.RowsPerImage)
        let _DataLayout_TextureDataLayout = new DawnRaw.WGPUTextureDataLayout()
        _DataLayout_TextureDataLayout.Offset <- _Offset_uint64
        _DataLayout_TextureDataLayout.BytesPerRow <- _BytesPerRow_int
        _DataLayout_TextureDataLayout.RowsPerImage <- _RowsPerImage_int
        let _DataLayout_TextureDataLayout = _DataLayout_TextureDataLayout
        let _Width_int = uint32 (WriteSize.Width)
        let _Height_int = uint32 (WriteSize.Height)
        let _DepthOrArrayLayers_int = uint32 (WriteSize.DepthOrArrayLayers)
        let _WriteSize_Extent3D = new DawnRaw.WGPUExtent3D()
        _WriteSize_Extent3D.Width <- _Width_int
        _WriteSize_Extent3D.Height <- _Height_int
        _WriteSize_Extent3D.DepthOrArrayLayers <- _DepthOrArrayLayers_int
        let _WriteSize_Extent3D = _WriteSize_Extent3D
        x.Handle.Reference.Invoke("writeTexture", js _Destination_ImageCopyTexture, js _Data_nativeint, js _DataSize_unativeint, js _DataLayout_TextureDataLayout, js _WriteSize_Extent3D) |> ignore
    member x.CopyTextureForBrowser(Source : ImageCopyTexture, Destination : ImageCopyTexture, CopySize : Extent3D) : unit = 
        let _Texture_Texture = (if isNull Source.Texture then null else Source.Texture.Handle)
        let _MipLevel_int = uint32 (Source.MipLevel)
        let _X_int = uint32 (Source.Origin.X)
        let _Y_int = uint32 (Source.Origin.Y)
        let _Z_int = uint32 (Source.Origin.Z)
        let _Origin_Origin3D = new DawnRaw.WGPUOrigin3D()
        _Origin_Origin3D.X <- _X_int
        _Origin_Origin3D.Y <- _Y_int
        _Origin_Origin3D.Z <- _Z_int
        let _Origin_Origin3D = _Origin_Origin3D
        let _Aspect_TextureAspect = Source.Aspect.GetValue()
        let _Source_ImageCopyTexture = new DawnRaw.WGPUImageCopyTexture()
        _Source_ImageCopyTexture.Texture <- _Texture_Texture
        _Source_ImageCopyTexture.MipLevel <- _MipLevel_int
        _Source_ImageCopyTexture.Origin <- _Origin_Origin3D
        _Source_ImageCopyTexture.Aspect <- _Aspect_TextureAspect
        let _Source_ImageCopyTexture = _Source_ImageCopyTexture
        let _Texture_Texture = (if isNull Destination.Texture then null else Destination.Texture.Handle)
        let _MipLevel_int = uint32 (Destination.MipLevel)
        let _X_int = uint32 (Destination.Origin.X)
        let _Y_int = uint32 (Destination.Origin.Y)
        let _Z_int = uint32 (Destination.Origin.Z)
        let _Origin_Origin3D = new DawnRaw.WGPUOrigin3D()
        _Origin_Origin3D.X <- _X_int
        _Origin_Origin3D.Y <- _Y_int
        _Origin_Origin3D.Z <- _Z_int
        let _Origin_Origin3D = _Origin_Origin3D
        let _Aspect_TextureAspect = Destination.Aspect.GetValue()
        let _Destination_ImageCopyTexture = new DawnRaw.WGPUImageCopyTexture()
        _Destination_ImageCopyTexture.Texture <- _Texture_Texture
        _Destination_ImageCopyTexture.MipLevel <- _MipLevel_int
        _Destination_ImageCopyTexture.Origin <- _Origin_Origin3D
        _Destination_ImageCopyTexture.Aspect <- _Aspect_TextureAspect
        let _Destination_ImageCopyTexture = _Destination_ImageCopyTexture
        let _Width_int = uint32 (CopySize.Width)
        let _Height_int = uint32 (CopySize.Height)
        let _DepthOrArrayLayers_int = uint32 (CopySize.DepthOrArrayLayers)
        let _CopySize_Extent3D = new DawnRaw.WGPUExtent3D()
        _CopySize_Extent3D.Width <- _Width_int
        _CopySize_Extent3D.Height <- _Height_int
        _CopySize_Extent3D.DepthOrArrayLayers <- _DepthOrArrayLayers_int
        let _CopySize_Extent3D = _CopySize_Extent3D
        x.Handle.Reference.Invoke("copyTextureForBrowser", js _Source_ImageCopyTexture, js _Destination_ImageCopyTexture, js _CopySize_Extent3D) |> ignore
    member x.CopyTextureForBrowser(Source : ImageCopyTexture, Destination : ImageCopyTexture, CopySize : Extent3D, Options : CopyTextureForBrowserOptions) : unit = 
        let _Texture_Texture = (if isNull Source.Texture then null else Source.Texture.Handle)
        let _MipLevel_int = uint32 (Source.MipLevel)
        let _X_int = uint32 (Source.Origin.X)
        let _Y_int = uint32 (Source.Origin.Y)
        let _Z_int = uint32 (Source.Origin.Z)
        let _Origin_Origin3D = new DawnRaw.WGPUOrigin3D()
        _Origin_Origin3D.X <- _X_int
        _Origin_Origin3D.Y <- _Y_int
        _Origin_Origin3D.Z <- _Z_int
        let _Origin_Origin3D = _Origin_Origin3D
        let _Aspect_TextureAspect = Source.Aspect.GetValue()
        let _Source_ImageCopyTexture = new DawnRaw.WGPUImageCopyTexture()
        _Source_ImageCopyTexture.Texture <- _Texture_Texture
        _Source_ImageCopyTexture.MipLevel <- _MipLevel_int
        _Source_ImageCopyTexture.Origin <- _Origin_Origin3D
        _Source_ImageCopyTexture.Aspect <- _Aspect_TextureAspect
        let _Source_ImageCopyTexture = _Source_ImageCopyTexture
        let _Texture_Texture = (if isNull Destination.Texture then null else Destination.Texture.Handle)
        let _MipLevel_int = uint32 (Destination.MipLevel)
        let _X_int = uint32 (Destination.Origin.X)
        let _Y_int = uint32 (Destination.Origin.Y)
        let _Z_int = uint32 (Destination.Origin.Z)
        let _Origin_Origin3D = new DawnRaw.WGPUOrigin3D()
        _Origin_Origin3D.X <- _X_int
        _Origin_Origin3D.Y <- _Y_int
        _Origin_Origin3D.Z <- _Z_int
        let _Origin_Origin3D = _Origin_Origin3D
        let _Aspect_TextureAspect = Destination.Aspect.GetValue()
        let _Destination_ImageCopyTexture = new DawnRaw.WGPUImageCopyTexture()
        _Destination_ImageCopyTexture.Texture <- _Texture_Texture
        _Destination_ImageCopyTexture.MipLevel <- _MipLevel_int
        _Destination_ImageCopyTexture.Origin <- _Origin_Origin3D
        _Destination_ImageCopyTexture.Aspect <- _Aspect_TextureAspect
        let _Destination_ImageCopyTexture = _Destination_ImageCopyTexture
        let _Width_int = uint32 (CopySize.Width)
        let _Height_int = uint32 (CopySize.Height)
        let _DepthOrArrayLayers_int = uint32 (CopySize.DepthOrArrayLayers)
        let _CopySize_Extent3D = new DawnRaw.WGPUExtent3D()
        _CopySize_Extent3D.Width <- _Width_int
        _CopySize_Extent3D.Height <- _Height_int
        _CopySize_Extent3D.DepthOrArrayLayers <- _DepthOrArrayLayers_int
        let _CopySize_Extent3D = _CopySize_Extent3D
        let _FlipY_bool = Options.FlipY
        let _AlphaOp_AlphaOp = Options.AlphaOp.GetValue()
        let _Options_CopyTextureForBrowserOptions = new DawnRaw.WGPUCopyTextureForBrowserOptions()
        _Options_CopyTextureForBrowserOptions.FlipY <- _FlipY_bool
        _Options_CopyTextureForBrowserOptions.AlphaOp <- _AlphaOp_AlphaOp
        let _Options_CopyTextureForBrowserOptions = _Options_CopyTextureForBrowserOptions
        x.Handle.Reference.Invoke("copyTextureForBrowser", js _Source_ImageCopyTexture, js _Destination_ImageCopyTexture, js _CopySize_Extent3D, js _Options_CopyTextureForBrowserOptions) |> ignore
[<AllowNullLiteral>]
type ShaderModule(device : Device, handle : ShaderModuleHandle, descriptor : ShaderModuleDescriptor, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.Descriptor = descriptor
    member x.ReferenceCount = !refCount
    member x.Handle : ShaderModuleHandle = handle
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
        new ShaderModule(device, handle, descriptor, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : ShaderModuleHandle, descriptor : ShaderModuleDescriptor) = new ShaderModule(device, handle, descriptor, ref 1)
    member x.GetCompilationInfo(Callback : CompilationInfoCallback) : unit = 
        let mutable _Callback_CompilationInfoCallbackGC = Unchecked.defaultof<System.Runtime.InteropServices.GCHandle>
        let _Callback_CompilationInfoCallbackFunction (Status : obj) (CompilationInfo : DawnRaw.WGPUCompilationInfo) (Userdata : float) = 
            let _Status_CompilationInfoRequestStatus = Status |> System.Convert.ToInt32 |> unbox<CompilationInfoRequestStatus>
            let _CompilationInfo_CompilationInfoOpt = 
                if isNull(CompilationInfo) then None
                else 
                    let _CompilationInfo_CompilationInfo = 
                        {
                            Messages =
                                let _Messages_CompilationMessageArrVal = CompilationInfo.Messages
                                let _Messages_CompilationMessageArr =
                                    let len = (_Messages_CompilationMessageArrVal).GetObjectProperty("length") |> convert<int>
                                    Array.init len (fun i ->
                                        let item = (_Messages_CompilationMessageArrVal).GetObjectProperty(string i) |> convert<DawnRaw.WGPUCompilationMessage>
                                        let _item_CompilationMessage = 
                                            {
                                                Message =
                                                    let _Message_stringVal = item.Message
                                                    let _Message_string = _Message_stringVal
                                                    _Message_string
                                                Type =
                                                    let _Type_CompilationMessageTypeVal = item.Type
                                                    let _Type_CompilationMessageType = _Type_CompilationMessageTypeVal |> System.Convert.ToInt32 |> unbox<CompilationMessageType>
                                                    _Type_CompilationMessageType
                                                LineNum =
                                                    let _LineNum_uint64Val = item.LineNum
                                                    let _LineNum_uint64 = _LineNum_uint64Val |> System.Convert.ToUInt64
                                                    _LineNum_uint64
                                                LinePos =
                                                    let _LinePos_uint64Val = item.LinePos
                                                    let _LinePos_uint64 = _LinePos_uint64Val |> System.Convert.ToUInt64
                                                    _LinePos_uint64
                                                Offset =
                                                    let _Offset_uint64Val = item.Offset
                                                    let _Offset_uint64 = _Offset_uint64Val |> System.Convert.ToUInt64
                                                    _Offset_uint64
                                                Length =
                                                    let _Length_uint64Val = item.Length
                                                    let _Length_uint64 = _Length_uint64Val |> System.Convert.ToUInt64
                                                    _Length_uint64
                                            }
                                        let _item_CompilationMessage = Unchecked.defaultof<CompilationMessage>
                                        _item_CompilationMessage
                                    )
                                _Messages_CompilationMessageArr
                        }
                    let _CompilationInfo_CompilationInfo = Unchecked.defaultof<CompilationInfo>
                    Some _CompilationInfo_CompilationInfo
            let _Userdata_nativeint = Userdata
            if _Callback_CompilationInfoCallbackGC.IsAllocated then _Callback_CompilationInfoCallbackGC.Free()
            Callback.Invoke(CompilationInfoRequestStatus.Parse(_Status_CompilationInfoRequestStatus), _CompilationInfo_CompilationInfoOpt, nativeint _Userdata_nativeint)
        let _Callback_CompilationInfoCallbackDel = WGPUCompilationInfoCallback(_Callback_CompilationInfoCallbackFunction)
        _Callback_CompilationInfoCallbackGC <- System.Runtime.InteropServices.GCHandle.Alloc(_Callback_CompilationInfoCallbackDel)
        let _Callback_CompilationInfoCallback = _Callback_CompilationInfoCallbackDel
        x.Handle.Reference.Invoke("getCompilationInfo", js _Callback_CompilationInfoCallback) |> ignore
    member x.GetCompilationInfo(Callback : CompilationInfoCallback, Userdata : nativeint) : unit = 
        let mutable _Callback_CompilationInfoCallbackGC = Unchecked.defaultof<System.Runtime.InteropServices.GCHandle>
        let _Callback_CompilationInfoCallbackFunction (Status : obj) (CompilationInfo : DawnRaw.WGPUCompilationInfo) (Userdata : float) = 
            let _Status_CompilationInfoRequestStatus = Status |> System.Convert.ToInt32 |> unbox<CompilationInfoRequestStatus>
            let _CompilationInfo_CompilationInfoOpt = 
                if isNull(CompilationInfo) then None
                else 
                    let _CompilationInfo_CompilationInfo = 
                        {
                            Messages =
                                let _Messages_CompilationMessageArrVal = CompilationInfo.Messages
                                let _Messages_CompilationMessageArr =
                                    let len = (_Messages_CompilationMessageArrVal).GetObjectProperty("length") |> convert<int>
                                    Array.init len (fun i ->
                                        let item = (_Messages_CompilationMessageArrVal).GetObjectProperty(string i) |> convert<DawnRaw.WGPUCompilationMessage>
                                        let _item_CompilationMessage = 
                                            {
                                                Message =
                                                    let _Message_stringVal = item.Message
                                                    let _Message_string = _Message_stringVal
                                                    _Message_string
                                                Type =
                                                    let _Type_CompilationMessageTypeVal = item.Type
                                                    let _Type_CompilationMessageType = _Type_CompilationMessageTypeVal |> System.Convert.ToInt32 |> unbox<CompilationMessageType>
                                                    _Type_CompilationMessageType
                                                LineNum =
                                                    let _LineNum_uint64Val = item.LineNum
                                                    let _LineNum_uint64 = _LineNum_uint64Val |> System.Convert.ToUInt64
                                                    _LineNum_uint64
                                                LinePos =
                                                    let _LinePos_uint64Val = item.LinePos
                                                    let _LinePos_uint64 = _LinePos_uint64Val |> System.Convert.ToUInt64
                                                    _LinePos_uint64
                                                Offset =
                                                    let _Offset_uint64Val = item.Offset
                                                    let _Offset_uint64 = _Offset_uint64Val |> System.Convert.ToUInt64
                                                    _Offset_uint64
                                                Length =
                                                    let _Length_uint64Val = item.Length
                                                    let _Length_uint64 = _Length_uint64Val |> System.Convert.ToUInt64
                                                    _Length_uint64
                                            }
                                        let _item_CompilationMessage = Unchecked.defaultof<CompilationMessage>
                                        _item_CompilationMessage
                                    )
                                _Messages_CompilationMessageArr
                        }
                    let _CompilationInfo_CompilationInfo = Unchecked.defaultof<CompilationInfo>
                    Some _CompilationInfo_CompilationInfo
            let _Userdata_nativeint = Userdata
            if _Callback_CompilationInfoCallbackGC.IsAllocated then _Callback_CompilationInfoCallbackGC.Free()
            Callback.Invoke(CompilationInfoRequestStatus.Parse(_Status_CompilationInfoRequestStatus), _CompilationInfo_CompilationInfoOpt, nativeint _Userdata_nativeint)
        let _Callback_CompilationInfoCallbackDel = WGPUCompilationInfoCallback(_Callback_CompilationInfoCallbackFunction)
        _Callback_CompilationInfoCallbackGC <- System.Runtime.InteropServices.GCHandle.Alloc(_Callback_CompilationInfoCallbackDel)
        let _Callback_CompilationInfoCallback = _Callback_CompilationInfoCallbackDel
        let _Userdata_nativeint = float (Userdata)
        x.Handle.Reference.Invoke("getCompilationInfo", js _Callback_CompilationInfoCallback, js _Userdata_nativeint) |> ignore
    member x.SetLabel() : unit = 
        x.Handle.Reference.Invoke("setLabel") |> ignore
    member x.SetLabel(Label : string) : unit = 
        let _Label_string = Label
        x.Handle.Reference.Invoke("setLabel", js _Label_string) |> ignore
[<AllowNullLiteral>]
type BindGroup(device : Device, handle : BindGroupHandle, descriptor : BindGroupDescriptor, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.Descriptor = descriptor
    member x.ReferenceCount = !refCount
    member x.Handle : BindGroupHandle = handle
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
        new BindGroup(device, handle, descriptor, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : BindGroupHandle, descriptor : BindGroupDescriptor) = new BindGroup(device, handle, descriptor, ref 1)
[<AllowNullLiteral>]
type ComputePipeline(device : Device, handle : ComputePipelineHandle, descriptor : ComputePipelineDescriptor, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.Descriptor = descriptor
    member x.ReferenceCount = !refCount
    member x.Handle : ComputePipelineHandle = handle
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
        new ComputePipeline(device, handle, descriptor, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : ComputePipelineHandle, descriptor : ComputePipelineDescriptor) = new ComputePipeline(device, handle, descriptor, ref 1)
    member x.GetBindGroupLayout(GroupIndex : int) : BindGroupLayout = 
        let _GroupIndex_int = uint32 (GroupIndex)
        new BindGroupLayout(x.Device, convert(x.Handle.Reference.Invoke("getBindGroupLayout", js _GroupIndex_int)))
    member x.SetLabel() : unit = 
        x.Handle.Reference.Invoke("setLabel") |> ignore
    member x.SetLabel(Label : string) : unit = 
        let _Label_string = Label
        x.Handle.Reference.Invoke("setLabel", js _Label_string) |> ignore
[<AllowNullLiteral>]
type RenderPipeline(device : Device, handle : RenderPipelineHandle, descriptor : RenderPipelineDescriptor, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.Descriptor = descriptor
    member x.ReferenceCount = !refCount
    member x.Handle : RenderPipelineHandle = handle
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
        new RenderPipeline(device, handle, descriptor, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : RenderPipelineHandle, descriptor : RenderPipelineDescriptor) = new RenderPipeline(device, handle, descriptor, ref 1)
    member x.GetBindGroupLayout(GroupIndex : int) : BindGroupLayout = 
        let _GroupIndex_int = uint32 (GroupIndex)
        new BindGroupLayout(x.Device, convert(x.Handle.Reference.Invoke("getBindGroupLayout", js _GroupIndex_int)))
    member x.SetLabel() : unit = 
        x.Handle.Reference.Invoke("setLabel") |> ignore
    member x.SetLabel(Label : string) : unit = 
        let _Label_string = Label
        x.Handle.Reference.Invoke("setLabel", js _Label_string) |> ignore
[<AllowNullLiteral>]
type ComputePassEncoder(device : Device, handle : ComputePassEncoderHandle, descriptor : ComputePassDescriptor, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.Descriptor = descriptor
    member x.ReferenceCount = !refCount
    member x.Handle : ComputePassEncoderHandle = handle
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
        new ComputePassEncoder(device, handle, descriptor, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : ComputePassEncoderHandle, descriptor : ComputePassDescriptor) = new ComputePassEncoder(device, handle, descriptor, ref 1)
    member x.InsertDebugMarker(MarkerLabel : string) : unit = 
        let _MarkerLabel_string = MarkerLabel
        x.Handle.Reference.Invoke("insertDebugMarker", js _MarkerLabel_string) |> ignore
    member x.PopDebugGroup() : unit = 
        x.Handle.Reference.Invoke("popDebugGroup") |> ignore
    member x.PushDebugGroup(GroupLabel : string) : unit = 
        let _GroupLabel_string = GroupLabel
        x.Handle.Reference.Invoke("pushDebugGroup", js _GroupLabel_string) |> ignore
    member x.SetPipeline(Pipeline : ComputePipeline) : unit = 
        let _Pipeline_ComputePipeline = (if isNull Pipeline then null else Pipeline.Handle)
        x.Handle.Reference.Invoke("setPipeline", js _Pipeline_ComputePipeline) |> ignore
    member x.SetBindGroup(GroupIndex : int, Group : BindGroup, DynamicOffsets : uint32[]) : unit = 
        let _GroupIndex_int = uint32 (GroupIndex)
        let _Group_BindGroup = (if isNull Group then null else Group.Handle)
        let _DynamicOffsets_uint32Arr = if isNull DynamicOffsets then null else Uint32Array.op_Implicit(Span(DynamicOffsets))
        let _DynamicOffsets_uint32ArrCount = if isNull DynamicOffsets then 0 else DynamicOffsets.Length
        x.Handle.Reference.Invoke("setBindGroup", js _GroupIndex_int, js _Group_BindGroup, js _DynamicOffsets_uint32Arr) |> ignore
    member x.WriteTimestamp(QuerySet : QuerySet, QueryIndex : int) : unit = 
        let _QuerySet_QuerySet = (if isNull QuerySet then null else QuerySet.Handle)
        let _QueryIndex_int = uint32 (QueryIndex)
        x.Handle.Reference.Invoke("writeTimestamp", js _QuerySet_QuerySet, js _QueryIndex_int) |> ignore
    member x.BeginPipelineStatisticsQuery(QuerySet : QuerySet, QueryIndex : int) : unit = 
        let _QuerySet_QuerySet = (if isNull QuerySet then null else QuerySet.Handle)
        let _QueryIndex_int = uint32 (QueryIndex)
        x.Handle.Reference.Invoke("beginPipelineStatisticsQuery", js _QuerySet_QuerySet, js _QueryIndex_int) |> ignore
    member x.Dispatch(X : int) : unit = 
        let _X_int = uint32 (X)
        x.Handle.Reference.Invoke("dispatch", js _X_int) |> ignore
    member x.Dispatch(X : int, Y : int) : unit = 
        let _X_int = uint32 (X)
        let _Y_int = uint32 (Y)
        x.Handle.Reference.Invoke("dispatch", js _X_int, js _Y_int) |> ignore
    member x.Dispatch(X : int, Y : int, Z : int) : unit = 
        let _X_int = uint32 (X)
        let _Y_int = uint32 (Y)
        let _Z_int = uint32 (Z)
        x.Handle.Reference.Invoke("dispatch", js _X_int, js _Y_int, js _Z_int) |> ignore
    member x.DispatchIndirect(IndirectBuffer : Buffer, IndirectOffset : uint64) : unit = 
        let _IndirectBuffer_Buffer = (if isNull IndirectBuffer then null else IndirectBuffer.Handle)
        let _IndirectOffset_uint64 = float (IndirectOffset)
        x.Handle.Reference.Invoke("dispatchIndirect", js _IndirectBuffer_Buffer, js _IndirectOffset_uint64) |> ignore
    member x.EndPass() : unit = 
        x.Handle.Reference.Invoke("endPass") |> ignore
    member x.EndPipelineStatisticsQuery() : unit = 
        x.Handle.Reference.Invoke("endPipelineStatisticsQuery") |> ignore
[<AllowNullLiteral>]
type RenderBundleEncoder(device : Device, handle : RenderBundleEncoderHandle, descriptor : RenderBundleEncoderDescriptor, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.Descriptor = descriptor
    member x.ReferenceCount = !refCount
    member x.Handle : RenderBundleEncoderHandle = handle
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
        new RenderBundleEncoder(device, handle, descriptor, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : RenderBundleEncoderHandle, descriptor : RenderBundleEncoderDescriptor) = new RenderBundleEncoder(device, handle, descriptor, ref 1)
    member x.SetPipeline(Pipeline : RenderPipeline) : unit = 
        let _Pipeline_RenderPipeline = (if isNull Pipeline then null else Pipeline.Handle)
        x.Handle.Reference.Invoke("setPipeline", js _Pipeline_RenderPipeline) |> ignore
    member x.SetBindGroup(GroupIndex : int, Group : BindGroup, DynamicOffsets : uint32[]) : unit = 
        let _GroupIndex_int = uint32 (GroupIndex)
        let _Group_BindGroup = (if isNull Group then null else Group.Handle)
        let _DynamicOffsets_uint32Arr = if isNull DynamicOffsets then null else Uint32Array.op_Implicit(Span(DynamicOffsets))
        let _DynamicOffsets_uint32ArrCount = if isNull DynamicOffsets then 0 else DynamicOffsets.Length
        x.Handle.Reference.Invoke("setBindGroup", js _GroupIndex_int, js _Group_BindGroup, js _DynamicOffsets_uint32Arr) |> ignore
    member x.Draw(VertexCount : int) : unit = 
        let _VertexCount_int = uint32 (VertexCount)
        x.Handle.Reference.Invoke("draw", js _VertexCount_int) |> ignore
    member x.Draw(VertexCount : int, InstanceCount : int) : unit = 
        let _VertexCount_int = uint32 (VertexCount)
        let _InstanceCount_int = uint32 (InstanceCount)
        x.Handle.Reference.Invoke("draw", js _VertexCount_int, js _InstanceCount_int) |> ignore
    member x.Draw(VertexCount : int, InstanceCount : int, FirstVertex : int) : unit = 
        let _VertexCount_int = uint32 (VertexCount)
        let _InstanceCount_int = uint32 (InstanceCount)
        let _FirstVertex_int = uint32 (FirstVertex)
        x.Handle.Reference.Invoke("draw", js _VertexCount_int, js _InstanceCount_int, js _FirstVertex_int) |> ignore
    member x.Draw(VertexCount : int, InstanceCount : int, FirstVertex : int, FirstInstance : int) : unit = 
        let _VertexCount_int = uint32 (VertexCount)
        let _InstanceCount_int = uint32 (InstanceCount)
        let _FirstVertex_int = uint32 (FirstVertex)
        let _FirstInstance_int = uint32 (FirstInstance)
        x.Handle.Reference.Invoke("draw", js _VertexCount_int, js _InstanceCount_int, js _FirstVertex_int, js _FirstInstance_int) |> ignore
    member x.DrawIndexed(IndexCount : int) : unit = 
        let _IndexCount_int = uint32 (IndexCount)
        x.Handle.Reference.Invoke("drawIndexed", js _IndexCount_int) |> ignore
    member x.DrawIndexed(IndexCount : int, InstanceCount : int) : unit = 
        let _IndexCount_int = uint32 (IndexCount)
        let _InstanceCount_int = uint32 (InstanceCount)
        x.Handle.Reference.Invoke("drawIndexed", js _IndexCount_int, js _InstanceCount_int) |> ignore
    member x.DrawIndexed(IndexCount : int, InstanceCount : int, FirstIndex : int) : unit = 
        let _IndexCount_int = uint32 (IndexCount)
        let _InstanceCount_int = uint32 (InstanceCount)
        let _FirstIndex_int = uint32 (FirstIndex)
        x.Handle.Reference.Invoke("drawIndexed", js _IndexCount_int, js _InstanceCount_int, js _FirstIndex_int) |> ignore
    member x.DrawIndexed(IndexCount : int, InstanceCount : int, FirstIndex : int, BaseVertex : int32) : unit = 
        let _IndexCount_int = uint32 (IndexCount)
        let _InstanceCount_int = uint32 (InstanceCount)
        let _FirstIndex_int = uint32 (FirstIndex)
        let _BaseVertex_int32 = int32 (BaseVertex)
        x.Handle.Reference.Invoke("drawIndexed", js _IndexCount_int, js _InstanceCount_int, js _FirstIndex_int, js _BaseVertex_int32) |> ignore
    member x.DrawIndexed(IndexCount : int, InstanceCount : int, FirstIndex : int, BaseVertex : int32, FirstInstance : int) : unit = 
        let _IndexCount_int = uint32 (IndexCount)
        let _InstanceCount_int = uint32 (InstanceCount)
        let _FirstIndex_int = uint32 (FirstIndex)
        let _BaseVertex_int32 = int32 (BaseVertex)
        let _FirstInstance_int = uint32 (FirstInstance)
        x.Handle.Reference.Invoke("drawIndexed", js _IndexCount_int, js _InstanceCount_int, js _FirstIndex_int, js _BaseVertex_int32, js _FirstInstance_int) |> ignore
    member x.DrawIndirect(IndirectBuffer : Buffer, IndirectOffset : uint64) : unit = 
        let _IndirectBuffer_Buffer = (if isNull IndirectBuffer then null else IndirectBuffer.Handle)
        let _IndirectOffset_uint64 = float (IndirectOffset)
        x.Handle.Reference.Invoke("drawIndirect", js _IndirectBuffer_Buffer, js _IndirectOffset_uint64) |> ignore
    member x.DrawIndexedIndirect(IndirectBuffer : Buffer, IndirectOffset : uint64) : unit = 
        let _IndirectBuffer_Buffer = (if isNull IndirectBuffer then null else IndirectBuffer.Handle)
        let _IndirectOffset_uint64 = float (IndirectOffset)
        x.Handle.Reference.Invoke("drawIndexedIndirect", js _IndirectBuffer_Buffer, js _IndirectOffset_uint64) |> ignore
    member x.InsertDebugMarker(MarkerLabel : string) : unit = 
        let _MarkerLabel_string = MarkerLabel
        x.Handle.Reference.Invoke("insertDebugMarker", js _MarkerLabel_string) |> ignore
    member x.PopDebugGroup() : unit = 
        x.Handle.Reference.Invoke("popDebugGroup") |> ignore
    member x.PushDebugGroup(GroupLabel : string) : unit = 
        let _GroupLabel_string = GroupLabel
        x.Handle.Reference.Invoke("pushDebugGroup", js _GroupLabel_string) |> ignore
    member x.SetVertexBuffer(Slot : int, Buffer : Buffer) : unit = 
        let _Slot_int = uint32 (Slot)
        let _Buffer_Buffer = (if isNull Buffer then null else Buffer.Handle)
        x.Handle.Reference.Invoke("setVertexBuffer", js _Slot_int, js _Buffer_Buffer) |> ignore
    member x.SetVertexBuffer(Slot : int, Buffer : Buffer, Offset : uint64, Size : uint64) : unit = 
        let _Slot_int = uint32 (Slot)
        let _Buffer_Buffer = (if isNull Buffer then null else Buffer.Handle)
        let _Offset_uint64 = float (Offset)
        let _Size_uint64 = float (Size)
        x.Handle.Reference.Invoke("setVertexBuffer", js _Slot_int, js _Buffer_Buffer, js _Offset_uint64, js _Size_uint64) |> ignore
    member x.SetIndexBuffer(Buffer : Buffer, Format : IndexFormat) : unit = 
        let _Buffer_Buffer = (if isNull Buffer then null else Buffer.Handle)
        let _Format_IndexFormat = Format.GetValue()
        x.Handle.Reference.Invoke("setIndexBuffer", js _Buffer_Buffer, js _Format_IndexFormat) |> ignore
    member x.SetIndexBuffer(Buffer : Buffer, Format : IndexFormat, Offset : uint64, Size : uint64) : unit = 
        let _Buffer_Buffer = (if isNull Buffer then null else Buffer.Handle)
        let _Format_IndexFormat = Format.GetValue()
        let _Offset_uint64 = float (Offset)
        let _Size_uint64 = float (Size)
        x.Handle.Reference.Invoke("setIndexBuffer", js _Buffer_Buffer, js _Format_IndexFormat, js _Offset_uint64, js _Size_uint64) |> ignore
    member x.Finish() : RenderBundle = 
        new RenderBundle(x.Device, convert(x.Handle.Reference.Invoke("finish")), RenderBundleDescriptor.Default)
    member x.Finish(Descriptor : RenderBundleDescriptor) : RenderBundle = 
        let _Label_string = Descriptor.Label
        let _Descriptor_RenderBundleDescriptor = new DawnRaw.WGPURenderBundleDescriptor()
        _Descriptor_RenderBundleDescriptor.Label <- _Label_string
        let _Descriptor_RenderBundleDescriptor = _Descriptor_RenderBundleDescriptor
        new RenderBundle(x.Device, convert(x.Handle.Reference.Invoke("finish", js _Descriptor_RenderBundleDescriptor)), Descriptor)
[<AllowNullLiteral>]
type RenderPassEncoder(device : Device, handle : RenderPassEncoderHandle, descriptor : RenderPassDescriptor, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.Descriptor = descriptor
    member x.ReferenceCount = !refCount
    member x.Handle : RenderPassEncoderHandle = handle
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
        new RenderPassEncoder(device, handle, descriptor, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : RenderPassEncoderHandle, descriptor : RenderPassDescriptor) = new RenderPassEncoder(device, handle, descriptor, ref 1)
    member x.SetPipeline(Pipeline : RenderPipeline) : unit = 
        let _Pipeline_RenderPipeline = (if isNull Pipeline then null else Pipeline.Handle)
        x.Handle.Reference.Invoke("setPipeline", js _Pipeline_RenderPipeline) |> ignore
    member x.SetBindGroup(GroupIndex : int, Group : BindGroup, DynamicOffsets : uint32[]) : unit = 
        let _GroupIndex_int = uint32 (GroupIndex)
        let _Group_BindGroup = (if isNull Group then null else Group.Handle)
        let _DynamicOffsets_uint32Arr = if isNull DynamicOffsets then null else Uint32Array.op_Implicit(Span(DynamicOffsets))
        let _DynamicOffsets_uint32ArrCount = if isNull DynamicOffsets then 0 else DynamicOffsets.Length
        x.Handle.Reference.Invoke("setBindGroup", js _GroupIndex_int, js _Group_BindGroup, js _DynamicOffsets_uint32Arr) |> ignore
    member x.Draw(VertexCount : int) : unit = 
        let _VertexCount_int = uint32 (VertexCount)
        x.Handle.Reference.Invoke("draw", js _VertexCount_int) |> ignore
    member x.Draw(VertexCount : int, InstanceCount : int) : unit = 
        let _VertexCount_int = uint32 (VertexCount)
        let _InstanceCount_int = uint32 (InstanceCount)
        x.Handle.Reference.Invoke("draw", js _VertexCount_int, js _InstanceCount_int) |> ignore
    member x.Draw(VertexCount : int, InstanceCount : int, FirstVertex : int) : unit = 
        let _VertexCount_int = uint32 (VertexCount)
        let _InstanceCount_int = uint32 (InstanceCount)
        let _FirstVertex_int = uint32 (FirstVertex)
        x.Handle.Reference.Invoke("draw", js _VertexCount_int, js _InstanceCount_int, js _FirstVertex_int) |> ignore
    member x.Draw(VertexCount : int, InstanceCount : int, FirstVertex : int, FirstInstance : int) : unit = 
        let _VertexCount_int = uint32 (VertexCount)
        let _InstanceCount_int = uint32 (InstanceCount)
        let _FirstVertex_int = uint32 (FirstVertex)
        let _FirstInstance_int = uint32 (FirstInstance)
        x.Handle.Reference.Invoke("draw", js _VertexCount_int, js _InstanceCount_int, js _FirstVertex_int, js _FirstInstance_int) |> ignore
    member x.DrawIndexed(IndexCount : int) : unit = 
        let _IndexCount_int = uint32 (IndexCount)
        x.Handle.Reference.Invoke("drawIndexed", js _IndexCount_int) |> ignore
    member x.DrawIndexed(IndexCount : int, InstanceCount : int) : unit = 
        let _IndexCount_int = uint32 (IndexCount)
        let _InstanceCount_int = uint32 (InstanceCount)
        x.Handle.Reference.Invoke("drawIndexed", js _IndexCount_int, js _InstanceCount_int) |> ignore
    member x.DrawIndexed(IndexCount : int, InstanceCount : int, FirstIndex : int) : unit = 
        let _IndexCount_int = uint32 (IndexCount)
        let _InstanceCount_int = uint32 (InstanceCount)
        let _FirstIndex_int = uint32 (FirstIndex)
        x.Handle.Reference.Invoke("drawIndexed", js _IndexCount_int, js _InstanceCount_int, js _FirstIndex_int) |> ignore
    member x.DrawIndexed(IndexCount : int, InstanceCount : int, FirstIndex : int, BaseVertex : int32) : unit = 
        let _IndexCount_int = uint32 (IndexCount)
        let _InstanceCount_int = uint32 (InstanceCount)
        let _FirstIndex_int = uint32 (FirstIndex)
        let _BaseVertex_int32 = int32 (BaseVertex)
        x.Handle.Reference.Invoke("drawIndexed", js _IndexCount_int, js _InstanceCount_int, js _FirstIndex_int, js _BaseVertex_int32) |> ignore
    member x.DrawIndexed(IndexCount : int, InstanceCount : int, FirstIndex : int, BaseVertex : int32, FirstInstance : int) : unit = 
        let _IndexCount_int = uint32 (IndexCount)
        let _InstanceCount_int = uint32 (InstanceCount)
        let _FirstIndex_int = uint32 (FirstIndex)
        let _BaseVertex_int32 = int32 (BaseVertex)
        let _FirstInstance_int = uint32 (FirstInstance)
        x.Handle.Reference.Invoke("drawIndexed", js _IndexCount_int, js _InstanceCount_int, js _FirstIndex_int, js _BaseVertex_int32, js _FirstInstance_int) |> ignore
    member x.DrawIndirect(IndirectBuffer : Buffer, IndirectOffset : uint64) : unit = 
        let _IndirectBuffer_Buffer = (if isNull IndirectBuffer then null else IndirectBuffer.Handle)
        let _IndirectOffset_uint64 = float (IndirectOffset)
        x.Handle.Reference.Invoke("drawIndirect", js _IndirectBuffer_Buffer, js _IndirectOffset_uint64) |> ignore
    member x.DrawIndexedIndirect(IndirectBuffer : Buffer, IndirectOffset : uint64) : unit = 
        let _IndirectBuffer_Buffer = (if isNull IndirectBuffer then null else IndirectBuffer.Handle)
        let _IndirectOffset_uint64 = float (IndirectOffset)
        x.Handle.Reference.Invoke("drawIndexedIndirect", js _IndirectBuffer_Buffer, js _IndirectOffset_uint64) |> ignore
    member x.ExecuteBundles(Bundles : array<RenderBundle>) : unit = 
        let _Bundles_RenderBundleArrCount = Bundles.Length
        let _Bundles_RenderBundleArrArray = newArray _Bundles_RenderBundleArrCount
        for i in 0 .. _Bundles_RenderBundleArrCount-1 do
            if isNull Bundles.[i] then _Bundles_RenderBundleArrArray.[i] <- null
            else _Bundles_RenderBundleArrArray.[i] <- Bundles.[i].Handle
        let _Bundles_RenderBundleArr = _Bundles_RenderBundleArrArray.Reference
        x.Handle.Reference.Invoke("executeBundles", js _Bundles_RenderBundleArr) |> ignore
    member x.InsertDebugMarker(MarkerLabel : string) : unit = 
        let _MarkerLabel_string = MarkerLabel
        x.Handle.Reference.Invoke("insertDebugMarker", js _MarkerLabel_string) |> ignore
    member x.PopDebugGroup() : unit = 
        x.Handle.Reference.Invoke("popDebugGroup") |> ignore
    member x.PushDebugGroup(GroupLabel : string) : unit = 
        let _GroupLabel_string = GroupLabel
        x.Handle.Reference.Invoke("pushDebugGroup", js _GroupLabel_string) |> ignore
    member x.SetStencilReference(Reference : int) : unit = 
        let _Reference_int = uint32 (Reference)
        x.Handle.Reference.Invoke("setStencilReference", js _Reference_int) |> ignore
    member x.SetBlendConstant(Color : Color) : unit = 
        let _R_float = (Color.R)
        let _G_float = (Color.G)
        let _B_float = (Color.B)
        let _A_float = (Color.A)
        let _Color_Color = new DawnRaw.WGPUColor()
        _Color_Color.R <- _R_float
        _Color_Color.G <- _G_float
        _Color_Color.B <- _B_float
        _Color_Color.A <- _A_float
        let _Color_Color = _Color_Color
        x.Handle.Reference.Invoke("setBlendConstant", js _Color_Color) |> ignore
    member x.SetViewport(X : float32, Y : float32, Width : float32, Height : float32, MinDepth : float32, MaxDepth : float32) : unit = 
        let _X_float32 = (X)
        let _Y_float32 = (Y)
        let _Width_float32 = (Width)
        let _Height_float32 = (Height)
        let _MinDepth_float32 = (MinDepth)
        let _MaxDepth_float32 = (MaxDepth)
        x.Handle.Reference.Invoke("setViewport", js _X_float32, js _Y_float32, js _Width_float32, js _Height_float32, js _MinDepth_float32, js _MaxDepth_float32) |> ignore
    member x.SetScissorRect(X : int, Y : int, Width : int, Height : int) : unit = 
        let _X_int = uint32 (X)
        let _Y_int = uint32 (Y)
        let _Width_int = uint32 (Width)
        let _Height_int = uint32 (Height)
        x.Handle.Reference.Invoke("setScissorRect", js _X_int, js _Y_int, js _Width_int, js _Height_int) |> ignore
    member x.SetVertexBuffer(Slot : int, Buffer : Buffer) : unit = 
        let _Slot_int = uint32 (Slot)
        let _Buffer_Buffer = (if isNull Buffer then null else Buffer.Handle)
        x.Handle.Reference.Invoke("setVertexBuffer", js _Slot_int, js _Buffer_Buffer) |> ignore
    member x.SetVertexBuffer(Slot : int, Buffer : Buffer, Offset : uint64, Size : uint64) : unit = 
        let _Slot_int = uint32 (Slot)
        let _Buffer_Buffer = (if isNull Buffer then null else Buffer.Handle)
        let _Offset_uint64 = float (Offset)
        let _Size_uint64 = float (Size)
        x.Handle.Reference.Invoke("setVertexBuffer", js _Slot_int, js _Buffer_Buffer, js _Offset_uint64, js _Size_uint64) |> ignore
    member x.SetIndexBuffer(Buffer : Buffer, Format : IndexFormat) : unit = 
        let _Buffer_Buffer = (if isNull Buffer then null else Buffer.Handle)
        let _Format_IndexFormat = Format.GetValue()
        x.Handle.Reference.Invoke("setIndexBuffer", js _Buffer_Buffer, js _Format_IndexFormat) |> ignore
    member x.SetIndexBuffer(Buffer : Buffer, Format : IndexFormat, Offset : uint64, Size : uint64) : unit = 
        let _Buffer_Buffer = (if isNull Buffer then null else Buffer.Handle)
        let _Format_IndexFormat = Format.GetValue()
        let _Offset_uint64 = float (Offset)
        let _Size_uint64 = float (Size)
        x.Handle.Reference.Invoke("setIndexBuffer", js _Buffer_Buffer, js _Format_IndexFormat, js _Offset_uint64, js _Size_uint64) |> ignore
    member x.BeginOcclusionQuery(QueryIndex : int) : unit = 
        let _QueryIndex_int = uint32 (QueryIndex)
        x.Handle.Reference.Invoke("beginOcclusionQuery", js _QueryIndex_int) |> ignore
    member x.BeginPipelineStatisticsQuery(QuerySet : QuerySet, QueryIndex : int) : unit = 
        let _QuerySet_QuerySet = (if isNull QuerySet then null else QuerySet.Handle)
        let _QueryIndex_int = uint32 (QueryIndex)
        x.Handle.Reference.Invoke("beginPipelineStatisticsQuery", js _QuerySet_QuerySet, js _QueryIndex_int) |> ignore
    member x.EndOcclusionQuery() : unit = 
        x.Handle.Reference.Invoke("endOcclusionQuery") |> ignore
    member x.WriteTimestamp(QuerySet : QuerySet, QueryIndex : int) : unit = 
        let _QuerySet_QuerySet = (if isNull QuerySet then null else QuerySet.Handle)
        let _QueryIndex_int = uint32 (QueryIndex)
        x.Handle.Reference.Invoke("writeTimestamp", js _QuerySet_QuerySet, js _QueryIndex_int) |> ignore
    member x.EndPass() : unit = 
        x.Handle.Reference.Invoke("endPass") |> ignore
    member x.EndPipelineStatisticsQuery() : unit = 
        x.Handle.Reference.Invoke("endPipelineStatisticsQuery") |> ignore
[<AllowNullLiteral>]
type CommandEncoder(device : Device, handle : CommandEncoderHandle, descriptor : CommandEncoderDescriptor, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.Device = device
    member x.Descriptor = descriptor
    member x.ReferenceCount = !refCount
    member x.Handle : CommandEncoderHandle = handle
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
        new CommandEncoder(device, handle, descriptor, refCount)
    interface System.IDisposable with
        member x.Dispose() = x.Dispose()
    new(device : Device, handle : CommandEncoderHandle, descriptor : CommandEncoderDescriptor) = new CommandEncoder(device, handle, descriptor, ref 1)
    member x.Finish() : CommandBuffer = 
        new CommandBuffer(x.Device, convert(x.Handle.Reference.Invoke("finish")), CommandBufferDescriptor.Default)
    member x.Finish(Descriptor : CommandBufferDescriptor) : CommandBuffer = 
        let _Label_string = Descriptor.Label
        let _Descriptor_CommandBufferDescriptor = new DawnRaw.WGPUCommandBufferDescriptor()
        _Descriptor_CommandBufferDescriptor.Label <- _Label_string
        let _Descriptor_CommandBufferDescriptor = _Descriptor_CommandBufferDescriptor
        new CommandBuffer(x.Device, convert(x.Handle.Reference.Invoke("finish", js _Descriptor_CommandBufferDescriptor)), Descriptor)
    member x.BeginComputePass() : ComputePassEncoder = 
        new ComputePassEncoder(x.Device, convert(x.Handle.Reference.Invoke("beginComputePass")), ComputePassDescriptor.Default)
    member x.BeginComputePass(Descriptor : ComputePassDescriptor) : ComputePassEncoder = 
        let _Label_string = Descriptor.Label
        let _Descriptor_ComputePassDescriptor = new DawnRaw.WGPUComputePassDescriptor()
        _Descriptor_ComputePassDescriptor.Label <- _Label_string
        let _Descriptor_ComputePassDescriptor = _Descriptor_ComputePassDescriptor
        new ComputePassEncoder(x.Device, convert(x.Handle.Reference.Invoke("beginComputePass", js _Descriptor_ComputePassDescriptor)), Descriptor)
    member x.BeginRenderPass(Descriptor : RenderPassDescriptor) : RenderPassEncoder = 
        let _Label_string = Descriptor.Label
        let _ColorAttachments_RenderPassColorAttachmentArrCount = if isNull Descriptor.ColorAttachments then 0 else Descriptor.ColorAttachments.Length
        let rec _ColorAttachments_RenderPassColorAttachmentArrCont (_ColorAttachments_RenderPassColorAttachmentArrinputs : array<RenderPassColorAttachment>) (_ColorAttachments_RenderPassColorAttachmentArroutputs : JsArray) (_ColorAttachments_RenderPassColorAttachmentArri : int) =
            if _ColorAttachments_RenderPassColorAttachmentArri >= _ColorAttachments_RenderPassColorAttachmentArrCount then
                let _ColorAttachments_RenderPassColorAttachmentArr = _ColorAttachments_RenderPassColorAttachmentArroutputs.Reference
                let inline _DepthStencilAttachment_RenderPassDepthStencilAttachmentOptCont _DepthStencilAttachment_RenderPassDepthStencilAttachmentOpt = 
                    let _OcclusionQuerySet_QuerySet = (if isNull Descriptor.OcclusionQuerySet then null else Descriptor.OcclusionQuerySet.Handle)
                    let _Descriptor_RenderPassDescriptor = new DawnRaw.WGPURenderPassDescriptor()
                    _Descriptor_RenderPassDescriptor.Label <- _Label_string
                    _Descriptor_RenderPassDescriptor.ColorAttachments <- _ColorAttachments_RenderPassColorAttachmentArr
                    _Descriptor_RenderPassDescriptor.DepthStencilAttachment <- _DepthStencilAttachment_RenderPassDepthStencilAttachmentOpt
                    _Descriptor_RenderPassDescriptor.OcclusionQuerySet <- _OcclusionQuerySet_QuerySet
                    let _Descriptor_RenderPassDescriptor = _Descriptor_RenderPassDescriptor
                    new RenderPassEncoder(x.Device, convert(x.Handle.Reference.Invoke("beginRenderPass", js _Descriptor_RenderPassDescriptor)), Descriptor)
                match Descriptor.DepthStencilAttachment with
                | Some v ->
                    let _View_TextureView = (if isNull v.View then null else v.View.Handle)
                    let _DepthLoadOp_LoadOp = v.DepthLoadOp.GetValue()
                    let _DepthStoreOp_StoreOp = v.DepthStoreOp.GetValue()
                    let _ClearDepth_float32 = (v.ClearDepth)
                    let _DepthReadOnly_bool = v.DepthReadOnly
                    let _StencilLoadOp_LoadOp = v.StencilLoadOp.GetValue()
                    let _StencilStoreOp_StoreOp = v.StencilStoreOp.GetValue()
                    let _ClearStencil_int = uint32 (v.ClearStencil)
                    let _StencilReadOnly_bool = v.StencilReadOnly
                    let _n = new DawnRaw.WGPURenderPassDepthStencilAttachment()
                    _n.View <- _View_TextureView
                    _n.DepthLoadOp <- _DepthLoadOp_LoadOp
                    _n.DepthStoreOp <- _DepthStoreOp_StoreOp
                    _n.ClearDepth <- _ClearDepth_float32
                    _n.DepthReadOnly <- _DepthReadOnly_bool
                    _n.StencilLoadOp <- _StencilLoadOp_LoadOp
                    _n.StencilStoreOp <- _StencilStoreOp_StoreOp
                    _n.ClearStencil <- _ClearStencil_int
                    _n.StencilReadOnly <- _StencilReadOnly_bool
                    let _n = _n
                    _DepthStencilAttachment_RenderPassDepthStencilAttachmentOptCont _n
                | None -> _DepthStencilAttachment_RenderPassDepthStencilAttachmentOptCont null
            else
                let _View_TextureView = (if isNull _ColorAttachments_RenderPassColorAttachmentArrinputs.[_ColorAttachments_RenderPassColorAttachmentArri].View then null else _ColorAttachments_RenderPassColorAttachmentArrinputs.[_ColorAttachments_RenderPassColorAttachmentArri].View.Handle)
                let _ResolveTarget_TextureView = (if isNull _ColorAttachments_RenderPassColorAttachmentArrinputs.[_ColorAttachments_RenderPassColorAttachmentArri].ResolveTarget then null else _ColorAttachments_RenderPassColorAttachmentArrinputs.[_ColorAttachments_RenderPassColorAttachmentArri].ResolveTarget.Handle)
                let _LoadOp_LoadOp = _ColorAttachments_RenderPassColorAttachmentArrinputs.[_ColorAttachments_RenderPassColorAttachmentArri].LoadOp.GetValue()
                let _StoreOp_StoreOp = _ColorAttachments_RenderPassColorAttachmentArrinputs.[_ColorAttachments_RenderPassColorAttachmentArri].StoreOp.GetValue()
                let _n = new DawnRaw.WGPURenderPassColorAttachment()
                _n.View <- _View_TextureView
                _n.ResolveTarget <- _ResolveTarget_TextureView
                _n.LoadOp <- _LoadOp_LoadOp
                _n.StoreOp <- _StoreOp_StoreOp
                let _n = _n
                _ColorAttachments_RenderPassColorAttachmentArroutputs.[_ColorAttachments_RenderPassColorAttachmentArri] <- js _n
                _ColorAttachments_RenderPassColorAttachmentArrCont _ColorAttachments_RenderPassColorAttachmentArrinputs _ColorAttachments_RenderPassColorAttachmentArroutputs (_ColorAttachments_RenderPassColorAttachmentArri + 1)
        _ColorAttachments_RenderPassColorAttachmentArrCont Descriptor.ColorAttachments (if _ColorAttachments_RenderPassColorAttachmentArrCount > 0 then newArray _ColorAttachments_RenderPassColorAttachmentArrCount else null) 0
    member x.CopyBufferToBuffer(Source : Buffer, SourceOffset : uint64, Destination : Buffer, DestinationOffset : uint64, Size : uint64) : unit = 
        let _Source_Buffer = (if isNull Source then null else Source.Handle)
        let _SourceOffset_uint64 = float (SourceOffset)
        let _Destination_Buffer = (if isNull Destination then null else Destination.Handle)
        let _DestinationOffset_uint64 = float (DestinationOffset)
        let _Size_uint64 = float (Size)
        x.Handle.Reference.Invoke("copyBufferToBuffer", js _Source_Buffer, js _SourceOffset_uint64, js _Destination_Buffer, js _DestinationOffset_uint64, js _Size_uint64) |> ignore
    member x.CopyBufferToTexture(Source : ImageCopyBuffer, Destination : ImageCopyTexture, CopySize : Extent3D) : unit = 
        let _Offset_uint64 = float (Source.Offset)
        let _BytesPerRow_int = uint32 (Source.BytesPerRow)
        let _RowsPerImage_int = uint32 (Source.RowsPerImage)
        let _Buffer_Buffer = (if isNull Source.Buffer then null else Source.Buffer.Handle)
        let _Source_ImageCopyBuffer = new DawnRaw.WGPUImageCopyBuffer()
        _Source_ImageCopyBuffer.Offset <- _Offset_uint64
        _Source_ImageCopyBuffer.BytesPerRow <- _BytesPerRow_int
        _Source_ImageCopyBuffer.RowsPerImage <- _RowsPerImage_int
        _Source_ImageCopyBuffer.Buffer <- _Buffer_Buffer
        let _Source_ImageCopyBuffer = _Source_ImageCopyBuffer
        let _Texture_Texture = (if isNull Destination.Texture then null else Destination.Texture.Handle)
        let _MipLevel_int = uint32 (Destination.MipLevel)
        let _X_int = uint32 (Destination.Origin.X)
        let _Y_int = uint32 (Destination.Origin.Y)
        let _Z_int = uint32 (Destination.Origin.Z)
        let _Origin_Origin3D = new DawnRaw.WGPUOrigin3D()
        _Origin_Origin3D.X <- _X_int
        _Origin_Origin3D.Y <- _Y_int
        _Origin_Origin3D.Z <- _Z_int
        let _Origin_Origin3D = _Origin_Origin3D
        let _Aspect_TextureAspect = Destination.Aspect.GetValue()
        let _Destination_ImageCopyTexture = new DawnRaw.WGPUImageCopyTexture()
        _Destination_ImageCopyTexture.Texture <- _Texture_Texture
        _Destination_ImageCopyTexture.MipLevel <- _MipLevel_int
        _Destination_ImageCopyTexture.Origin <- _Origin_Origin3D
        _Destination_ImageCopyTexture.Aspect <- _Aspect_TextureAspect
        let _Destination_ImageCopyTexture = _Destination_ImageCopyTexture
        let _Width_int = uint32 (CopySize.Width)
        let _Height_int = uint32 (CopySize.Height)
        let _DepthOrArrayLayers_int = uint32 (CopySize.DepthOrArrayLayers)
        let _CopySize_Extent3D = new DawnRaw.WGPUExtent3D()
        _CopySize_Extent3D.Width <- _Width_int
        _CopySize_Extent3D.Height <- _Height_int
        _CopySize_Extent3D.DepthOrArrayLayers <- _DepthOrArrayLayers_int
        let _CopySize_Extent3D = _CopySize_Extent3D
        x.Handle.Reference.Invoke("copyBufferToTexture", js _Source_ImageCopyBuffer, js _Destination_ImageCopyTexture, js _CopySize_Extent3D) |> ignore
    member x.CopyTextureToBuffer(Source : ImageCopyTexture, Destination : ImageCopyBuffer, CopySize : Extent3D) : unit = 
        let _Texture_Texture = (if isNull Source.Texture then null else Source.Texture.Handle)
        let _MipLevel_int = uint32 (Source.MipLevel)
        let _X_int = uint32 (Source.Origin.X)
        let _Y_int = uint32 (Source.Origin.Y)
        let _Z_int = uint32 (Source.Origin.Z)
        let _Origin_Origin3D = new DawnRaw.WGPUOrigin3D()
        _Origin_Origin3D.X <- _X_int
        _Origin_Origin3D.Y <- _Y_int
        _Origin_Origin3D.Z <- _Z_int
        let _Origin_Origin3D = _Origin_Origin3D
        let _Aspect_TextureAspect = Source.Aspect.GetValue()
        let _Source_ImageCopyTexture = new DawnRaw.WGPUImageCopyTexture()
        _Source_ImageCopyTexture.Texture <- _Texture_Texture
        _Source_ImageCopyTexture.MipLevel <- _MipLevel_int
        _Source_ImageCopyTexture.Origin <- _Origin_Origin3D
        _Source_ImageCopyTexture.Aspect <- _Aspect_TextureAspect
        let _Source_ImageCopyTexture = _Source_ImageCopyTexture
        let _Offset_uint64 = float (Destination.Offset)
        let _BytesPerRow_int = uint32 (Destination.BytesPerRow)
        let _RowsPerImage_int = uint32 (Destination.RowsPerImage)
        let _Buffer_Buffer = (if isNull Destination.Buffer then null else Destination.Buffer.Handle)
        let _Destination_ImageCopyBuffer = new DawnRaw.WGPUImageCopyBuffer()
        _Destination_ImageCopyBuffer.Offset <- _Offset_uint64
        _Destination_ImageCopyBuffer.BytesPerRow <- _BytesPerRow_int
        _Destination_ImageCopyBuffer.RowsPerImage <- _RowsPerImage_int
        _Destination_ImageCopyBuffer.Buffer <- _Buffer_Buffer
        let _Destination_ImageCopyBuffer = _Destination_ImageCopyBuffer
        let _Width_int = uint32 (CopySize.Width)
        let _Height_int = uint32 (CopySize.Height)
        let _DepthOrArrayLayers_int = uint32 (CopySize.DepthOrArrayLayers)
        let _CopySize_Extent3D = new DawnRaw.WGPUExtent3D()
        _CopySize_Extent3D.Width <- _Width_int
        _CopySize_Extent3D.Height <- _Height_int
        _CopySize_Extent3D.DepthOrArrayLayers <- _DepthOrArrayLayers_int
        let _CopySize_Extent3D = _CopySize_Extent3D
        x.Handle.Reference.Invoke("copyTextureToBuffer", js _Source_ImageCopyTexture, js _Destination_ImageCopyBuffer, js _CopySize_Extent3D) |> ignore
    member x.CopyTextureToTexture(Source : ImageCopyTexture, Destination : ImageCopyTexture, CopySize : Extent3D) : unit = 
        let _Texture_Texture = (if isNull Source.Texture then null else Source.Texture.Handle)
        let _MipLevel_int = uint32 (Source.MipLevel)
        let _X_int = uint32 (Source.Origin.X)
        let _Y_int = uint32 (Source.Origin.Y)
        let _Z_int = uint32 (Source.Origin.Z)
        let _Origin_Origin3D = new DawnRaw.WGPUOrigin3D()
        _Origin_Origin3D.X <- _X_int
        _Origin_Origin3D.Y <- _Y_int
        _Origin_Origin3D.Z <- _Z_int
        let _Origin_Origin3D = _Origin_Origin3D
        let _Aspect_TextureAspect = Source.Aspect.GetValue()
        let _Source_ImageCopyTexture = new DawnRaw.WGPUImageCopyTexture()
        _Source_ImageCopyTexture.Texture <- _Texture_Texture
        _Source_ImageCopyTexture.MipLevel <- _MipLevel_int
        _Source_ImageCopyTexture.Origin <- _Origin_Origin3D
        _Source_ImageCopyTexture.Aspect <- _Aspect_TextureAspect
        let _Source_ImageCopyTexture = _Source_ImageCopyTexture
        let _Texture_Texture = (if isNull Destination.Texture then null else Destination.Texture.Handle)
        let _MipLevel_int = uint32 (Destination.MipLevel)
        let _X_int = uint32 (Destination.Origin.X)
        let _Y_int = uint32 (Destination.Origin.Y)
        let _Z_int = uint32 (Destination.Origin.Z)
        let _Origin_Origin3D = new DawnRaw.WGPUOrigin3D()
        _Origin_Origin3D.X <- _X_int
        _Origin_Origin3D.Y <- _Y_int
        _Origin_Origin3D.Z <- _Z_int
        let _Origin_Origin3D = _Origin_Origin3D
        let _Aspect_TextureAspect = Destination.Aspect.GetValue()
        let _Destination_ImageCopyTexture = new DawnRaw.WGPUImageCopyTexture()
        _Destination_ImageCopyTexture.Texture <- _Texture_Texture
        _Destination_ImageCopyTexture.MipLevel <- _MipLevel_int
        _Destination_ImageCopyTexture.Origin <- _Origin_Origin3D
        _Destination_ImageCopyTexture.Aspect <- _Aspect_TextureAspect
        let _Destination_ImageCopyTexture = _Destination_ImageCopyTexture
        let _Width_int = uint32 (CopySize.Width)
        let _Height_int = uint32 (CopySize.Height)
        let _DepthOrArrayLayers_int = uint32 (CopySize.DepthOrArrayLayers)
        let _CopySize_Extent3D = new DawnRaw.WGPUExtent3D()
        _CopySize_Extent3D.Width <- _Width_int
        _CopySize_Extent3D.Height <- _Height_int
        _CopySize_Extent3D.DepthOrArrayLayers <- _DepthOrArrayLayers_int
        let _CopySize_Extent3D = _CopySize_Extent3D
        x.Handle.Reference.Invoke("copyTextureToTexture", js _Source_ImageCopyTexture, js _Destination_ImageCopyTexture, js _CopySize_Extent3D) |> ignore
    member x.CopyTextureToTextureInternal(Source : ImageCopyTexture, Destination : ImageCopyTexture, CopySize : Extent3D) : unit = 
        let _Texture_Texture = (if isNull Source.Texture then null else Source.Texture.Handle)
        let _MipLevel_int = uint32 (Source.MipLevel)
        let _X_int = uint32 (Source.Origin.X)
        let _Y_int = uint32 (Source.Origin.Y)
        let _Z_int = uint32 (Source.Origin.Z)
        let _Origin_Origin3D = new DawnRaw.WGPUOrigin3D()
        _Origin_Origin3D.X <- _X_int
        _Origin_Origin3D.Y <- _Y_int
        _Origin_Origin3D.Z <- _Z_int
        let _Origin_Origin3D = _Origin_Origin3D
        let _Aspect_TextureAspect = Source.Aspect.GetValue()
        let _Source_ImageCopyTexture = new DawnRaw.WGPUImageCopyTexture()
        _Source_ImageCopyTexture.Texture <- _Texture_Texture
        _Source_ImageCopyTexture.MipLevel <- _MipLevel_int
        _Source_ImageCopyTexture.Origin <- _Origin_Origin3D
        _Source_ImageCopyTexture.Aspect <- _Aspect_TextureAspect
        let _Source_ImageCopyTexture = _Source_ImageCopyTexture
        let _Texture_Texture = (if isNull Destination.Texture then null else Destination.Texture.Handle)
        let _MipLevel_int = uint32 (Destination.MipLevel)
        let _X_int = uint32 (Destination.Origin.X)
        let _Y_int = uint32 (Destination.Origin.Y)
        let _Z_int = uint32 (Destination.Origin.Z)
        let _Origin_Origin3D = new DawnRaw.WGPUOrigin3D()
        _Origin_Origin3D.X <- _X_int
        _Origin_Origin3D.Y <- _Y_int
        _Origin_Origin3D.Z <- _Z_int
        let _Origin_Origin3D = _Origin_Origin3D
        let _Aspect_TextureAspect = Destination.Aspect.GetValue()
        let _Destination_ImageCopyTexture = new DawnRaw.WGPUImageCopyTexture()
        _Destination_ImageCopyTexture.Texture <- _Texture_Texture
        _Destination_ImageCopyTexture.MipLevel <- _MipLevel_int
        _Destination_ImageCopyTexture.Origin <- _Origin_Origin3D
        _Destination_ImageCopyTexture.Aspect <- _Aspect_TextureAspect
        let _Destination_ImageCopyTexture = _Destination_ImageCopyTexture
        let _Width_int = uint32 (CopySize.Width)
        let _Height_int = uint32 (CopySize.Height)
        let _DepthOrArrayLayers_int = uint32 (CopySize.DepthOrArrayLayers)
        let _CopySize_Extent3D = new DawnRaw.WGPUExtent3D()
        _CopySize_Extent3D.Width <- _Width_int
        _CopySize_Extent3D.Height <- _Height_int
        _CopySize_Extent3D.DepthOrArrayLayers <- _DepthOrArrayLayers_int
        let _CopySize_Extent3D = _CopySize_Extent3D
        x.Handle.Reference.Invoke("copyTextureToTextureInternal", js _Source_ImageCopyTexture, js _Destination_ImageCopyTexture, js _CopySize_Extent3D) |> ignore
    member x.InjectValidationError(Message : string) : unit = 
        let _Message_string = Message
        x.Handle.Reference.Invoke("injectValidationError", js _Message_string) |> ignore
    member x.InsertDebugMarker(MarkerLabel : string) : unit = 
        let _MarkerLabel_string = MarkerLabel
        x.Handle.Reference.Invoke("insertDebugMarker", js _MarkerLabel_string) |> ignore
    member x.PopDebugGroup() : unit = 
        x.Handle.Reference.Invoke("popDebugGroup") |> ignore
    member x.PushDebugGroup(GroupLabel : string) : unit = 
        let _GroupLabel_string = GroupLabel
        x.Handle.Reference.Invoke("pushDebugGroup", js _GroupLabel_string) |> ignore
    member x.ResolveQuerySet(QuerySet : QuerySet, FirstQuery : int, QueryCount : int, Destination : Buffer, DestinationOffset : uint64) : unit = 
        let _QuerySet_QuerySet = (if isNull QuerySet then null else QuerySet.Handle)
        let _FirstQuery_int = uint32 (FirstQuery)
        let _QueryCount_int = uint32 (QueryCount)
        let _Destination_Buffer = (if isNull Destination then null else Destination.Handle)
        let _DestinationOffset_uint64 = float (DestinationOffset)
        x.Handle.Reference.Invoke("resolveQuerySet", js _QuerySet_QuerySet, js _FirstQuery_int, js _QueryCount_int, js _Destination_Buffer, js _DestinationOffset_uint64) |> ignore
    member x.WriteBuffer(Buffer : Buffer, BufferOffset : uint64, Data : string, Size : uint64) : unit = 
        let _Buffer_Buffer = (if isNull Buffer then null else Buffer.Handle)
        let _BufferOffset_uint64 = float (BufferOffset)
        let _Data_string = Data
        let _Size_uint64 = float (Size)
        x.Handle.Reference.Invoke("writeBuffer", js _Buffer_Buffer, js _BufferOffset_uint64, js _Data_string, js _Size_uint64) |> ignore
    member x.WriteTimestamp(QuerySet : QuerySet, QueryIndex : int) : unit = 
        let _QuerySet_QuerySet = (if isNull QuerySet then null else QuerySet.Handle)
        let _QueryIndex_int = uint32 (QueryIndex)
        x.Handle.Reference.Invoke("writeTimestamp", js _QuerySet_QuerySet, js _QueryIndex_int) |> ignore
[<AllowNullLiteral>]
type Device(handle : DeviceHandle, refCount : ref<int>) = 
    let mutable isDisposed = false
    member x.ReferenceCount = !refCount
    member x.Handle : DeviceHandle = handle
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
        let _Label_string = Descriptor.Label
        let _Layout_BindGroupLayout = (if isNull Descriptor.Layout then null else Descriptor.Layout.Handle)
        let _Entries_BindGroupEntryArrCount = if isNull Descriptor.Entries then 0 else Descriptor.Entries.Length
        let rec _Entries_BindGroupEntryArrCont (_Entries_BindGroupEntryArrinputs : array<BindGroupEntry>) (_Entries_BindGroupEntryArroutputs : JsArray) (_Entries_BindGroupEntryArri : int) =
            if _Entries_BindGroupEntryArri >= _Entries_BindGroupEntryArrCount then
                let _Entries_BindGroupEntryArr = _Entries_BindGroupEntryArroutputs.Reference
                let _Descriptor_BindGroupDescriptor = new DawnRaw.WGPUBindGroupDescriptor()
                _Descriptor_BindGroupDescriptor.Label <- _Label_string
                _Descriptor_BindGroupDescriptor.Layout <- _Layout_BindGroupLayout
                _Descriptor_BindGroupDescriptor.Entries <- _Entries_BindGroupEntryArr
                let _Descriptor_BindGroupDescriptor = _Descriptor_BindGroupDescriptor
                new BindGroup(x, convert(x.Handle.Reference.Invoke("createBindGroup", js _Descriptor_BindGroupDescriptor)), Descriptor)
            else
                let _Binding_int = uint32 (_Entries_BindGroupEntryArrinputs.[_Entries_BindGroupEntryArri].Binding)
                let _Buffer_Buffer = (if isNull _Entries_BindGroupEntryArrinputs.[_Entries_BindGroupEntryArri].Buffer then null else _Entries_BindGroupEntryArrinputs.[_Entries_BindGroupEntryArri].Buffer.Handle)
                let _Offset_uint64 = float (_Entries_BindGroupEntryArrinputs.[_Entries_BindGroupEntryArri].Offset)
                let _Size_uint64 = float (_Entries_BindGroupEntryArrinputs.[_Entries_BindGroupEntryArri].Size)
                let _Sampler_Sampler = (if isNull _Entries_BindGroupEntryArrinputs.[_Entries_BindGroupEntryArri].Sampler then null else _Entries_BindGroupEntryArrinputs.[_Entries_BindGroupEntryArri].Sampler.Handle)
                let _TextureView_TextureView = (if isNull _Entries_BindGroupEntryArrinputs.[_Entries_BindGroupEntryArri].TextureView then null else _Entries_BindGroupEntryArrinputs.[_Entries_BindGroupEntryArri].TextureView.Handle)
                let _n = new DawnRaw.WGPUBindGroupEntry()
                _n.Binding <- _Binding_int
                _n.Buffer <- _Buffer_Buffer
                _n.Offset <- _Offset_uint64
                _n.Size <- _Size_uint64
                _n.Sampler <- _Sampler_Sampler
                _n.TextureView <- _TextureView_TextureView
                let _n = _n
                _Entries_BindGroupEntryArroutputs.[_Entries_BindGroupEntryArri] <- js _n
                _Entries_BindGroupEntryArrCont _Entries_BindGroupEntryArrinputs _Entries_BindGroupEntryArroutputs (_Entries_BindGroupEntryArri + 1)
        _Entries_BindGroupEntryArrCont Descriptor.Entries (if _Entries_BindGroupEntryArrCount > 0 then newArray _Entries_BindGroupEntryArrCount else null) 0
    member x.CreateBindGroupLayout(Descriptor : BindGroupLayoutDescriptor) : BindGroupLayout = 
        let _Label_string = Descriptor.Label
        let _Entries_BindGroupLayoutEntryArrCount = if isNull Descriptor.Entries then 0 else Descriptor.Entries.Length
        let rec _Entries_BindGroupLayoutEntryArrCont (_Entries_BindGroupLayoutEntryArrinputs : array<BindGroupLayoutEntry>) (_Entries_BindGroupLayoutEntryArroutputs : JsArray) (_Entries_BindGroupLayoutEntryArri : int) =
            if _Entries_BindGroupLayoutEntryArri >= _Entries_BindGroupLayoutEntryArrCount then
                let _Entries_BindGroupLayoutEntryArr = _Entries_BindGroupLayoutEntryArroutputs.Reference
                let _Descriptor_BindGroupLayoutDescriptor = new DawnRaw.WGPUBindGroupLayoutDescriptor()
                _Descriptor_BindGroupLayoutDescriptor.Label <- _Label_string
                _Descriptor_BindGroupLayoutDescriptor.Entries <- _Entries_BindGroupLayoutEntryArr
                let _Descriptor_BindGroupLayoutDescriptor = _Descriptor_BindGroupLayoutDescriptor
                new BindGroupLayout(x, convert(x.Handle.Reference.Invoke("createBindGroupLayout", js _Descriptor_BindGroupLayoutDescriptor)))
            else
                let _Binding_int = uint32 (_Entries_BindGroupLayoutEntryArrinputs.[_Entries_BindGroupLayoutEntryArri].Binding)
                let _Visibility_ShaderStage = int (_Entries_BindGroupLayoutEntryArrinputs.[_Entries_BindGroupLayoutEntryArri].Visibility)
                let inline _Buffer_BufferBindingLayoutOptCont _Buffer_BufferBindingLayoutOpt = 
                    let inline _Sampler_SamplerBindingLayoutOptCont _Sampler_SamplerBindingLayoutOpt = 
                        let inline _Texture_TextureBindingLayoutOptCont _Texture_TextureBindingLayoutOpt = 
                            let inline _StorageTexture_StorageTextureBindingLayoutOptCont _StorageTexture_StorageTextureBindingLayoutOpt = 
                                let _n = new DawnRaw.WGPUBindGroupLayoutEntry()
                                _n.Binding <- _Binding_int
                                _n.Visibility <- _Visibility_ShaderStage
                                _n.Buffer <- _Buffer_BufferBindingLayoutOpt
                                _n.Sampler <- _Sampler_SamplerBindingLayoutOpt
                                _n.Texture <- _Texture_TextureBindingLayoutOpt
                                _n.StorageTexture <- _StorageTexture_StorageTextureBindingLayoutOpt
                                let _n = _n
                                _Entries_BindGroupLayoutEntryArroutputs.[_Entries_BindGroupLayoutEntryArri] <- js _n
                                _Entries_BindGroupLayoutEntryArrCont _Entries_BindGroupLayoutEntryArrinputs _Entries_BindGroupLayoutEntryArroutputs (_Entries_BindGroupLayoutEntryArri + 1)
                            match _Entries_BindGroupLayoutEntryArrinputs.[_Entries_BindGroupLayoutEntryArri].StorageTexture with
                            | Some v ->
                                let _Access_StorageTextureAccess = v.Access.GetValue()
                                let _Format_TextureFormat = v.Format.GetValue()
                                let _ViewDimension_TextureViewDimension = v.ViewDimension.GetValue()
                                let _n = new DawnRaw.WGPUStorageTextureBindingLayout()
                                _n.Access <- _Access_StorageTextureAccess
                                _n.Format <- _Format_TextureFormat
                                _n.ViewDimension <- _ViewDimension_TextureViewDimension
                                let _n = _n
                                _StorageTexture_StorageTextureBindingLayoutOptCont _n
                            | None -> _StorageTexture_StorageTextureBindingLayoutOptCont null
                        match _Entries_BindGroupLayoutEntryArrinputs.[_Entries_BindGroupLayoutEntryArri].Texture with
                        | Some v ->
                            let _SampleType_TextureSampleType = v.SampleType.GetValue()
                            let _ViewDimension_TextureViewDimension = v.ViewDimension.GetValue()
                            let _Multisampled_bool = v.Multisampled
                            let _n = new DawnRaw.WGPUTextureBindingLayout()
                            _n.SampleType <- _SampleType_TextureSampleType
                            _n.ViewDimension <- _ViewDimension_TextureViewDimension
                            _n.Multisampled <- _Multisampled_bool
                            let _n = _n
                            _Texture_TextureBindingLayoutOptCont _n
                        | None -> _Texture_TextureBindingLayoutOptCont null
                    match _Entries_BindGroupLayoutEntryArrinputs.[_Entries_BindGroupLayoutEntryArri].Sampler with
                    | Some v ->
                        let _Type_SamplerBindingType = v.Type.GetValue()
                        let _n = new DawnRaw.WGPUSamplerBindingLayout()
                        _n.Type <- _Type_SamplerBindingType
                        let _n = _n
                        _Sampler_SamplerBindingLayoutOptCont _n
                    | None -> _Sampler_SamplerBindingLayoutOptCont null
                match _Entries_BindGroupLayoutEntryArrinputs.[_Entries_BindGroupLayoutEntryArri].Buffer with
                | Some v ->
                    let _Type_BufferBindingType = v.Type.GetValue()
                    let _HasDynamicOffset_bool = v.HasDynamicOffset
                    let _MinBindingSize_uint64 = float (v.MinBindingSize)
                    let _n = new DawnRaw.WGPUBufferBindingLayout()
                    _n.Type <- _Type_BufferBindingType
                    _n.HasDynamicOffset <- _HasDynamicOffset_bool
                    _n.MinBindingSize <- _MinBindingSize_uint64
                    let _n = _n
                    _Buffer_BufferBindingLayoutOptCont _n
                | None -> _Buffer_BufferBindingLayoutOptCont null
        _Entries_BindGroupLayoutEntryArrCont Descriptor.Entries (if _Entries_BindGroupLayoutEntryArrCount > 0 then newArray _Entries_BindGroupLayoutEntryArrCount else null) 0
    member x.CreateBuffer(Descriptor : BufferDescriptor) : Buffer = 
        let _Label_string = Descriptor.Label
        let _Usage_BufferUsage = int (Descriptor.Usage)
        let _Size_uint64 = float (Descriptor.Size)
        let _MappedAtCreation_bool = Descriptor.MappedAtCreation
        let _Descriptor_BufferDescriptor = new DawnRaw.WGPUBufferDescriptor()
        _Descriptor_BufferDescriptor.Label <- _Label_string
        _Descriptor_BufferDescriptor.Usage <- _Usage_BufferUsage
        _Descriptor_BufferDescriptor.Size <- _Size_uint64
        _Descriptor_BufferDescriptor.MappedAtCreation <- _MappedAtCreation_bool
        let _Descriptor_BufferDescriptor = _Descriptor_BufferDescriptor
        new Buffer(x, convert(x.Handle.Reference.Invoke("createBuffer", js _Descriptor_BufferDescriptor)))
    member x.CreateErrorBuffer() : Buffer = 
        new Buffer(x, convert(x.Handle.Reference.Invoke("createErrorBuffer")))
    member x.CreateCommandEncoder() : CommandEncoder = 
        new CommandEncoder(x, convert(x.Handle.Reference.Invoke("createCommandEncoder")), CommandEncoderDescriptor.Default)
    member x.CreateCommandEncoder(Descriptor : CommandEncoderDescriptor) : CommandEncoder = 
        let _Label_string = Descriptor.Label
        let _Descriptor_CommandEncoderDescriptor = new DawnRaw.WGPUCommandEncoderDescriptor()
        _Descriptor_CommandEncoderDescriptor.Label <- _Label_string
        let _Descriptor_CommandEncoderDescriptor = _Descriptor_CommandEncoderDescriptor
        new CommandEncoder(x, convert(x.Handle.Reference.Invoke("createCommandEncoder", js _Descriptor_CommandEncoderDescriptor)), Descriptor)
    member x.CreateComputePipeline(Descriptor : ComputePipelineDescriptor) : ComputePipeline = 
        let _Label_string = Descriptor.Label
        let _Layout_PipelineLayout = (if isNull Descriptor.Layout then null else Descriptor.Layout.Handle)
        let _Module_ShaderModule = (if isNull Descriptor.Compute.Module then null else Descriptor.Compute.Module.Handle)
        let _EntryPoint_string = Descriptor.Compute.EntryPoint
        let _Constants_ConstantEntryArrCount = if isNull Descriptor.Compute.Constants then 0 else Descriptor.Compute.Constants.Length
        let rec _Constants_ConstantEntryArrCont (_Constants_ConstantEntryArrinputs : array<ConstantEntry>) (_Constants_ConstantEntryArroutputs : JsArray) (_Constants_ConstantEntryArri : int) =
            if _Constants_ConstantEntryArri >= _Constants_ConstantEntryArrCount then
                let _Constants_ConstantEntryArr = _Constants_ConstantEntryArroutputs.Reference
                let _Compute_ProgrammableStageDescriptor = new DawnRaw.WGPUProgrammableStageDescriptor()
                _Compute_ProgrammableStageDescriptor.Module <- _Module_ShaderModule
                _Compute_ProgrammableStageDescriptor.EntryPoint <- _EntryPoint_string
                _Compute_ProgrammableStageDescriptor.Constants <- _Constants_ConstantEntryArr
                let _Compute_ProgrammableStageDescriptor = _Compute_ProgrammableStageDescriptor
                let _Descriptor_ComputePipelineDescriptor = new DawnRaw.WGPUComputePipelineDescriptor()
                _Descriptor_ComputePipelineDescriptor.Label <- _Label_string
                _Descriptor_ComputePipelineDescriptor.Layout <- _Layout_PipelineLayout
                _Descriptor_ComputePipelineDescriptor.Compute <- _Compute_ProgrammableStageDescriptor
                let _Descriptor_ComputePipelineDescriptor = _Descriptor_ComputePipelineDescriptor
                new ComputePipeline(x, convert(x.Handle.Reference.Invoke("createComputePipeline", js _Descriptor_ComputePipelineDescriptor)), Descriptor)
            else
                let _Key_string = _Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Key
                let _Value_float = (_Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Value)
                let _n = new DawnRaw.WGPUConstantEntry()
                _n.Key <- _Key_string
                _n.Value <- _Value_float
                let _n = _n
                _Constants_ConstantEntryArroutputs.[_Constants_ConstantEntryArri] <- js _n
                _Constants_ConstantEntryArrCont _Constants_ConstantEntryArrinputs _Constants_ConstantEntryArroutputs (_Constants_ConstantEntryArri + 1)
        _Constants_ConstantEntryArrCont Descriptor.Compute.Constants (if _Constants_ConstantEntryArrCount > 0 then newArray _Constants_ConstantEntryArrCount else null) 0
    member x.CreateComputePipelineAsync(Descriptor : ComputePipelineDescriptor, Callback : CreateComputePipelineAsyncCallback) : unit = 
        let _Label_string = Descriptor.Label
        let _Layout_PipelineLayout = (if isNull Descriptor.Layout then null else Descriptor.Layout.Handle)
        let _Module_ShaderModule = (if isNull Descriptor.Compute.Module then null else Descriptor.Compute.Module.Handle)
        let _EntryPoint_string = Descriptor.Compute.EntryPoint
        let _Constants_ConstantEntryArrCount = if isNull Descriptor.Compute.Constants then 0 else Descriptor.Compute.Constants.Length
        let rec _Constants_ConstantEntryArrCont (_Constants_ConstantEntryArrinputs : array<ConstantEntry>) (_Constants_ConstantEntryArroutputs : JsArray) (_Constants_ConstantEntryArri : int) =
            if _Constants_ConstantEntryArri >= _Constants_ConstantEntryArrCount then
                let _Constants_ConstantEntryArr = _Constants_ConstantEntryArroutputs.Reference
                let _Compute_ProgrammableStageDescriptor = new DawnRaw.WGPUProgrammableStageDescriptor()
                _Compute_ProgrammableStageDescriptor.Module <- _Module_ShaderModule
                _Compute_ProgrammableStageDescriptor.EntryPoint <- _EntryPoint_string
                _Compute_ProgrammableStageDescriptor.Constants <- _Constants_ConstantEntryArr
                let _Compute_ProgrammableStageDescriptor = _Compute_ProgrammableStageDescriptor
                let _Descriptor_ComputePipelineDescriptor = new DawnRaw.WGPUComputePipelineDescriptor()
                _Descriptor_ComputePipelineDescriptor.Label <- _Label_string
                _Descriptor_ComputePipelineDescriptor.Layout <- _Layout_PipelineLayout
                _Descriptor_ComputePipelineDescriptor.Compute <- _Compute_ProgrammableStageDescriptor
                let _Descriptor_ComputePipelineDescriptor = _Descriptor_ComputePipelineDescriptor
                let mutable _Callback_CreateComputePipelineAsyncCallbackGC = Unchecked.defaultof<System.Runtime.InteropServices.GCHandle>
                let _Callback_CreateComputePipelineAsyncCallbackFunction (Status : obj) (Pipeline : ComputePipelineHandle) (Message : string) (Userdata : float) = 
                    let _Status_CreatePipelineAsyncStatus = Status |> System.Convert.ToInt32 |> unbox<CreatePipelineAsyncStatus>
                    let _Pipeline_ComputePipeline = Pipeline
                    let _Message_string = Message
                    let _Userdata_nativeint = Userdata
                    if _Callback_CreateComputePipelineAsyncCallbackGC.IsAllocated then _Callback_CreateComputePipelineAsyncCallbackGC.Free()
                    Callback.Invoke(CreatePipelineAsyncStatus.Parse(_Status_CreatePipelineAsyncStatus), new ComputePipeline(x, _Pipeline_ComputePipeline, Unchecked.defaultof<_>), _Message_string, nativeint _Userdata_nativeint)
                let _Callback_CreateComputePipelineAsyncCallbackDel = WGPUCreateComputePipelineAsyncCallback(_Callback_CreateComputePipelineAsyncCallbackFunction)
                _Callback_CreateComputePipelineAsyncCallbackGC <- System.Runtime.InteropServices.GCHandle.Alloc(_Callback_CreateComputePipelineAsyncCallbackDel)
                let _Callback_CreateComputePipelineAsyncCallback = _Callback_CreateComputePipelineAsyncCallbackDel
                x.Handle.Reference.Invoke("createComputePipelineAsync", js _Descriptor_ComputePipelineDescriptor, js _Callback_CreateComputePipelineAsyncCallback) |> ignore
            else
                let _Key_string = _Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Key
                let _Value_float = (_Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Value)
                let _n = new DawnRaw.WGPUConstantEntry()
                _n.Key <- _Key_string
                _n.Value <- _Value_float
                let _n = _n
                _Constants_ConstantEntryArroutputs.[_Constants_ConstantEntryArri] <- js _n
                _Constants_ConstantEntryArrCont _Constants_ConstantEntryArrinputs _Constants_ConstantEntryArroutputs (_Constants_ConstantEntryArri + 1)
        _Constants_ConstantEntryArrCont Descriptor.Compute.Constants (if _Constants_ConstantEntryArrCount > 0 then newArray _Constants_ConstantEntryArrCount else null) 0
    member x.CreateComputePipelineAsync(Descriptor : ComputePipelineDescriptor, Callback : CreateComputePipelineAsyncCallback, Userdata : nativeint) : unit = 
        let _Label_string = Descriptor.Label
        let _Layout_PipelineLayout = (if isNull Descriptor.Layout then null else Descriptor.Layout.Handle)
        let _Module_ShaderModule = (if isNull Descriptor.Compute.Module then null else Descriptor.Compute.Module.Handle)
        let _EntryPoint_string = Descriptor.Compute.EntryPoint
        let _Constants_ConstantEntryArrCount = if isNull Descriptor.Compute.Constants then 0 else Descriptor.Compute.Constants.Length
        let rec _Constants_ConstantEntryArrCont (_Constants_ConstantEntryArrinputs : array<ConstantEntry>) (_Constants_ConstantEntryArroutputs : JsArray) (_Constants_ConstantEntryArri : int) =
            if _Constants_ConstantEntryArri >= _Constants_ConstantEntryArrCount then
                let _Constants_ConstantEntryArr = _Constants_ConstantEntryArroutputs.Reference
                let _Compute_ProgrammableStageDescriptor = new DawnRaw.WGPUProgrammableStageDescriptor()
                _Compute_ProgrammableStageDescriptor.Module <- _Module_ShaderModule
                _Compute_ProgrammableStageDescriptor.EntryPoint <- _EntryPoint_string
                _Compute_ProgrammableStageDescriptor.Constants <- _Constants_ConstantEntryArr
                let _Compute_ProgrammableStageDescriptor = _Compute_ProgrammableStageDescriptor
                let _Descriptor_ComputePipelineDescriptor = new DawnRaw.WGPUComputePipelineDescriptor()
                _Descriptor_ComputePipelineDescriptor.Label <- _Label_string
                _Descriptor_ComputePipelineDescriptor.Layout <- _Layout_PipelineLayout
                _Descriptor_ComputePipelineDescriptor.Compute <- _Compute_ProgrammableStageDescriptor
                let _Descriptor_ComputePipelineDescriptor = _Descriptor_ComputePipelineDescriptor
                let mutable _Callback_CreateComputePipelineAsyncCallbackGC = Unchecked.defaultof<System.Runtime.InteropServices.GCHandle>
                let _Callback_CreateComputePipelineAsyncCallbackFunction (Status : obj) (Pipeline : ComputePipelineHandle) (Message : string) (Userdata : float) = 
                    let _Status_CreatePipelineAsyncStatus = Status |> System.Convert.ToInt32 |> unbox<CreatePipelineAsyncStatus>
                    let _Pipeline_ComputePipeline = Pipeline
                    let _Message_string = Message
                    let _Userdata_nativeint = Userdata
                    if _Callback_CreateComputePipelineAsyncCallbackGC.IsAllocated then _Callback_CreateComputePipelineAsyncCallbackGC.Free()
                    Callback.Invoke(CreatePipelineAsyncStatus.Parse(_Status_CreatePipelineAsyncStatus), new ComputePipeline(x, _Pipeline_ComputePipeline, Unchecked.defaultof<_>), _Message_string, nativeint _Userdata_nativeint)
                let _Callback_CreateComputePipelineAsyncCallbackDel = WGPUCreateComputePipelineAsyncCallback(_Callback_CreateComputePipelineAsyncCallbackFunction)
                _Callback_CreateComputePipelineAsyncCallbackGC <- System.Runtime.InteropServices.GCHandle.Alloc(_Callback_CreateComputePipelineAsyncCallbackDel)
                let _Callback_CreateComputePipelineAsyncCallback = _Callback_CreateComputePipelineAsyncCallbackDel
                let _Userdata_nativeint = float (Userdata)
                x.Handle.Reference.Invoke("createComputePipelineAsync", js _Descriptor_ComputePipelineDescriptor, js _Callback_CreateComputePipelineAsyncCallback, js _Userdata_nativeint) |> ignore
            else
                let _Key_string = _Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Key
                let _Value_float = (_Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Value)
                let _n = new DawnRaw.WGPUConstantEntry()
                _n.Key <- _Key_string
                _n.Value <- _Value_float
                let _n = _n
                _Constants_ConstantEntryArroutputs.[_Constants_ConstantEntryArri] <- js _n
                _Constants_ConstantEntryArrCont _Constants_ConstantEntryArrinputs _Constants_ConstantEntryArroutputs (_Constants_ConstantEntryArri + 1)
        _Constants_ConstantEntryArrCont Descriptor.Compute.Constants (if _Constants_ConstantEntryArrCount > 0 then newArray _Constants_ConstantEntryArrCount else null) 0
    member x.CreateExternalTexture(ExternalTextureDescriptor : ExternalTextureDescriptor) : ExternalTexture = 
        let _Label_string = ExternalTextureDescriptor.Label
        let _Plane0_TextureView = (if isNull ExternalTextureDescriptor.Plane0 then null else ExternalTextureDescriptor.Plane0.Handle)
        let _Format_TextureFormat = ExternalTextureDescriptor.Format.GetValue()
        let _ExternalTextureDescriptor_ExternalTextureDescriptor = new DawnRaw.WGPUExternalTextureDescriptor()
        _ExternalTextureDescriptor_ExternalTextureDescriptor.Label <- _Label_string
        _ExternalTextureDescriptor_ExternalTextureDescriptor.Plane0 <- _Plane0_TextureView
        _ExternalTextureDescriptor_ExternalTextureDescriptor.Format <- _Format_TextureFormat
        let _ExternalTextureDescriptor_ExternalTextureDescriptor = _ExternalTextureDescriptor_ExternalTextureDescriptor
        new ExternalTexture(x, convert(x.Handle.Reference.Invoke("createExternalTexture", js _ExternalTextureDescriptor_ExternalTextureDescriptor)), ExternalTextureDescriptor)
    member x.CreatePipelineLayout(Descriptor : PipelineLayoutDescriptor) : PipelineLayout = 
        let _Label_string = Descriptor.Label
        let _BindGroupLayouts_BindGroupLayoutArrCount = Descriptor.BindGroupLayouts.Length
        let _BindGroupLayouts_BindGroupLayoutArrArray = newArray _BindGroupLayouts_BindGroupLayoutArrCount
        for i in 0 .. _BindGroupLayouts_BindGroupLayoutArrCount-1 do
            if isNull Descriptor.BindGroupLayouts.[i] then _BindGroupLayouts_BindGroupLayoutArrArray.[i] <- null
            else _BindGroupLayouts_BindGroupLayoutArrArray.[i] <- Descriptor.BindGroupLayouts.[i].Handle
        let _BindGroupLayouts_BindGroupLayoutArr = _BindGroupLayouts_BindGroupLayoutArrArray.Reference
        let _Descriptor_PipelineLayoutDescriptor = new DawnRaw.WGPUPipelineLayoutDescriptor()
        _Descriptor_PipelineLayoutDescriptor.Label <- _Label_string
        _Descriptor_PipelineLayoutDescriptor.BindGroupLayouts <- _BindGroupLayouts_BindGroupLayoutArr
        let _Descriptor_PipelineLayoutDescriptor = _Descriptor_PipelineLayoutDescriptor
        new PipelineLayout(x, convert(x.Handle.Reference.Invoke("createPipelineLayout", js _Descriptor_PipelineLayoutDescriptor)), Descriptor)
    member x.CreateQuerySet(Descriptor : QuerySetDescriptor) : QuerySet = 
        let _Label_string = Descriptor.Label
        let _Type_QueryType = Descriptor.Type.GetValue()
        let _Count_int = uint32 (Descriptor.Count)
        let inline _PipelineStatistics_PipelineStatisticNameOptCont _PipelineStatistics_PipelineStatisticNameOpt =
            let _PipelineStatisticsCount_int = uint32 (Descriptor.PipelineStatisticsCount)
            let _Descriptor_QuerySetDescriptor = new DawnRaw.WGPUQuerySetDescriptor()
            _Descriptor_QuerySetDescriptor.Label <- _Label_string
            _Descriptor_QuerySetDescriptor.Type <- _Type_QueryType
            _Descriptor_QuerySetDescriptor.Count <- _Count_int
            _Descriptor_QuerySetDescriptor.PipelineStatistics <- _PipelineStatistics_PipelineStatisticNameOpt
            _Descriptor_QuerySetDescriptor.PipelineStatisticsCount <- _PipelineStatisticsCount_int
            let _Descriptor_QuerySetDescriptor = _Descriptor_QuerySetDescriptor
            new QuerySet(x, convert(x.Handle.Reference.Invoke("createQuerySet", js _Descriptor_QuerySetDescriptor)), Descriptor)
        match Descriptor.PipelineStatistics with
        | Some o ->
            _PipelineStatistics_PipelineStatisticNameOptCont(o.GetValue())
        | _ ->
            _PipelineStatistics_PipelineStatisticNameOptCont null
    member x.CreateRenderPipelineAsync(Descriptor : RenderPipelineDescriptor, Callback : CreateRenderPipelineAsyncCallback) : unit = 
        let _Label_string = Descriptor.Label
        let _Layout_PipelineLayout = (if isNull Descriptor.Layout then null else Descriptor.Layout.Handle)
        let _Module_ShaderModule = (if isNull Descriptor.Vertex.Module then null else Descriptor.Vertex.Module.Handle)
        let _EntryPoint_string = Descriptor.Vertex.EntryPoint
        let _Constants_ConstantEntryArrCount = if isNull Descriptor.Vertex.Constants then 0 else Descriptor.Vertex.Constants.Length
        let rec _Constants_ConstantEntryArrCont (_Constants_ConstantEntryArrinputs : array<ConstantEntry>) (_Constants_ConstantEntryArroutputs : JsArray) (_Constants_ConstantEntryArri : int) =
            if _Constants_ConstantEntryArri >= _Constants_ConstantEntryArrCount then
                let _Constants_ConstantEntryArr = _Constants_ConstantEntryArroutputs.Reference
                let _Buffers_VertexBufferLayoutArrCount = if isNull Descriptor.Vertex.Buffers then 0 else Descriptor.Vertex.Buffers.Length
                let rec _Buffers_VertexBufferLayoutArrCont (_Buffers_VertexBufferLayoutArrinputs : array<VertexBufferLayout>) (_Buffers_VertexBufferLayoutArroutputs : JsArray) (_Buffers_VertexBufferLayoutArri : int) =
                    if _Buffers_VertexBufferLayoutArri >= _Buffers_VertexBufferLayoutArrCount then
                        let _Buffers_VertexBufferLayoutArr = _Buffers_VertexBufferLayoutArroutputs.Reference
                        let _Vertex_VertexState = new DawnRaw.WGPUVertexState()
                        _Vertex_VertexState.Module <- _Module_ShaderModule
                        _Vertex_VertexState.EntryPoint <- _EntryPoint_string
                        _Vertex_VertexState.Constants <- _Constants_ConstantEntryArr
                        _Vertex_VertexState.Buffers <- _Buffers_VertexBufferLayoutArr
                        let _Vertex_VertexState = _Vertex_VertexState
                        let _Topology_PrimitiveTopology = Descriptor.Primitive.Topology.GetValue()
                        let _StripIndexFormat_IndexFormat = Descriptor.Primitive.StripIndexFormat.GetValue()
                        let _FrontFace_FrontFace = Descriptor.Primitive.FrontFace.GetValue()
                        let _CullMode_CullMode = Descriptor.Primitive.CullMode.GetValue()
                        let _Primitive_PrimitiveState = new DawnRaw.WGPUPrimitiveState()
                        _Primitive_PrimitiveState.Topology <- _Topology_PrimitiveTopology
                        _Primitive_PrimitiveState.StripIndexFormat <- _StripIndexFormat_IndexFormat
                        _Primitive_PrimitiveState.FrontFace <- _FrontFace_FrontFace
                        _Primitive_PrimitiveState.CullMode <- _CullMode_CullMode
                        let _Primitive_PrimitiveState = _Primitive_PrimitiveState
                        let inline _DepthStencil_DepthStencilStateOptCont _DepthStencil_DepthStencilStateOpt = 
                            let _Count_int = uint32 (Descriptor.Multisample.Count)
                            let _Mask_int = uint32 (Descriptor.Multisample.Mask)
                            let _AlphaToCoverageEnabled_bool = Descriptor.Multisample.AlphaToCoverageEnabled
                            let _Multisample_MultisampleState = new DawnRaw.WGPUMultisampleState()
                            _Multisample_MultisampleState.Count <- _Count_int
                            _Multisample_MultisampleState.Mask <- _Mask_int
                            _Multisample_MultisampleState.AlphaToCoverageEnabled <- _AlphaToCoverageEnabled_bool
                            let _Multisample_MultisampleState = _Multisample_MultisampleState
                            let inline _Fragment_FragmentStateOptCont _Fragment_FragmentStateOpt = 
                                let _Descriptor_RenderPipelineDescriptor = new DawnRaw.WGPURenderPipelineDescriptor()
                                _Descriptor_RenderPipelineDescriptor.Label <- _Label_string
                                _Descriptor_RenderPipelineDescriptor.Layout <- _Layout_PipelineLayout
                                _Descriptor_RenderPipelineDescriptor.Vertex <- _Vertex_VertexState
                                _Descriptor_RenderPipelineDescriptor.Primitive <- _Primitive_PrimitiveState
                                _Descriptor_RenderPipelineDescriptor.DepthStencil <- _DepthStencil_DepthStencilStateOpt
                                _Descriptor_RenderPipelineDescriptor.Multisample <- _Multisample_MultisampleState
                                _Descriptor_RenderPipelineDescriptor.Fragment <- _Fragment_FragmentStateOpt
                                let _Descriptor_RenderPipelineDescriptor = _Descriptor_RenderPipelineDescriptor
                                let mutable _Callback_CreateRenderPipelineAsyncCallbackGC = Unchecked.defaultof<System.Runtime.InteropServices.GCHandle>
                                let _Callback_CreateRenderPipelineAsyncCallbackFunction (Status : obj) (Pipeline : RenderPipelineHandle) (Message : string) (Userdata : float) = 
                                    let _Status_CreatePipelineAsyncStatus = Status |> System.Convert.ToInt32 |> unbox<CreatePipelineAsyncStatus>
                                    let _Pipeline_RenderPipeline = Pipeline
                                    let _Message_string = Message
                                    let _Userdata_nativeint = Userdata
                                    if _Callback_CreateRenderPipelineAsyncCallbackGC.IsAllocated then _Callback_CreateRenderPipelineAsyncCallbackGC.Free()
                                    Callback.Invoke(CreatePipelineAsyncStatus.Parse(_Status_CreatePipelineAsyncStatus), new RenderPipeline(x, _Pipeline_RenderPipeline, Unchecked.defaultof<_>), _Message_string, nativeint _Userdata_nativeint)
                                let _Callback_CreateRenderPipelineAsyncCallbackDel = WGPUCreateRenderPipelineAsyncCallback(_Callback_CreateRenderPipelineAsyncCallbackFunction)
                                _Callback_CreateRenderPipelineAsyncCallbackGC <- System.Runtime.InteropServices.GCHandle.Alloc(_Callback_CreateRenderPipelineAsyncCallbackDel)
                                let _Callback_CreateRenderPipelineAsyncCallback = _Callback_CreateRenderPipelineAsyncCallbackDel
                                x.Handle.Reference.Invoke("createRenderPipelineAsync", js _Descriptor_RenderPipelineDescriptor, js _Callback_CreateRenderPipelineAsyncCallback) |> ignore
                            match Descriptor.Fragment with
                            | Some v ->
                                let _Module_ShaderModule = (if isNull v.Module then null else v.Module.Handle)
                                let _EntryPoint_string = v.EntryPoint
                                let _Constants_ConstantEntryArrCount = if isNull v.Constants then 0 else v.Constants.Length
                                let rec _Constants_ConstantEntryArrCont (_Constants_ConstantEntryArrinputs : array<ConstantEntry>) (_Constants_ConstantEntryArroutputs : JsArray) (_Constants_ConstantEntryArri : int) =
                                    if _Constants_ConstantEntryArri >= _Constants_ConstantEntryArrCount then
                                        let _Constants_ConstantEntryArr = _Constants_ConstantEntryArroutputs.Reference
                                        let _Targets_ColorTargetStateArrCount = if isNull v.Targets then 0 else v.Targets.Length
                                        let rec _Targets_ColorTargetStateArrCont (_Targets_ColorTargetStateArrinputs : array<ColorTargetState>) (_Targets_ColorTargetStateArroutputs : JsArray) (_Targets_ColorTargetStateArri : int) =
                                            if _Targets_ColorTargetStateArri >= _Targets_ColorTargetStateArrCount then
                                                let _Targets_ColorTargetStateArr = _Targets_ColorTargetStateArroutputs.Reference
                                                let _n = new DawnRaw.WGPUFragmentState()
                                                _n.Module <- _Module_ShaderModule
                                                _n.EntryPoint <- _EntryPoint_string
                                                _n.Constants <- _Constants_ConstantEntryArr
                                                _n.Targets <- _Targets_ColorTargetStateArr
                                                let _n = _n
                                                _Fragment_FragmentStateOptCont _n
                                            else
                                                let _Format_TextureFormat = _Targets_ColorTargetStateArrinputs.[_Targets_ColorTargetStateArri].Format.GetValue()
                                                let inline _Blend_BlendStateOptCont _Blend_BlendStateOpt = 
                                                    let _WriteMask_ColorWriteMask = int (_Targets_ColorTargetStateArrinputs.[_Targets_ColorTargetStateArri].WriteMask)
                                                    let _n = new DawnRaw.WGPUColorTargetState()
                                                    _n.Format <- _Format_TextureFormat
                                                    _n.Blend <- _Blend_BlendStateOpt
                                                    _n.WriteMask <- _WriteMask_ColorWriteMask
                                                    let _n = _n
                                                    _Targets_ColorTargetStateArroutputs.[_Targets_ColorTargetStateArri] <- js _n
                                                    _Targets_ColorTargetStateArrCont _Targets_ColorTargetStateArrinputs _Targets_ColorTargetStateArroutputs (_Targets_ColorTargetStateArri + 1)
                                                match _Targets_ColorTargetStateArrinputs.[_Targets_ColorTargetStateArri].Blend with
                                                | Some v ->
                                                    let _Operation_BlendOperation = v.Color.Operation.GetValue()
                                                    let _SrcFactor_BlendFactor = v.Color.SrcFactor.GetValue()
                                                    let _DstFactor_BlendFactor = v.Color.DstFactor.GetValue()
                                                    let _Color_BlendComponent = new DawnRaw.WGPUBlendComponent()
                                                    _Color_BlendComponent.Operation <- _Operation_BlendOperation
                                                    _Color_BlendComponent.SrcFactor <- _SrcFactor_BlendFactor
                                                    _Color_BlendComponent.DstFactor <- _DstFactor_BlendFactor
                                                    let _Color_BlendComponent = _Color_BlendComponent
                                                    let _Operation_BlendOperation = v.Alpha.Operation.GetValue()
                                                    let _SrcFactor_BlendFactor = v.Alpha.SrcFactor.GetValue()
                                                    let _DstFactor_BlendFactor = v.Alpha.DstFactor.GetValue()
                                                    let _Alpha_BlendComponent = new DawnRaw.WGPUBlendComponent()
                                                    _Alpha_BlendComponent.Operation <- _Operation_BlendOperation
                                                    _Alpha_BlendComponent.SrcFactor <- _SrcFactor_BlendFactor
                                                    _Alpha_BlendComponent.DstFactor <- _DstFactor_BlendFactor
                                                    let _Alpha_BlendComponent = _Alpha_BlendComponent
                                                    let _n = new DawnRaw.WGPUBlendState()
                                                    _n.Color <- _Color_BlendComponent
                                                    _n.Alpha <- _Alpha_BlendComponent
                                                    let _n = _n
                                                    _Blend_BlendStateOptCont _n
                                                | None -> _Blend_BlendStateOptCont null
                                        _Targets_ColorTargetStateArrCont v.Targets (if _Targets_ColorTargetStateArrCount > 0 then newArray _Targets_ColorTargetStateArrCount else null) 0
                                    else
                                        let _Key_string = _Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Key
                                        let _Value_float = (_Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Value)
                                        let _n = new DawnRaw.WGPUConstantEntry()
                                        _n.Key <- _Key_string
                                        _n.Value <- _Value_float
                                        let _n = _n
                                        _Constants_ConstantEntryArroutputs.[_Constants_ConstantEntryArri] <- js _n
                                        _Constants_ConstantEntryArrCont _Constants_ConstantEntryArrinputs _Constants_ConstantEntryArroutputs (_Constants_ConstantEntryArri + 1)
                                _Constants_ConstantEntryArrCont v.Constants (if _Constants_ConstantEntryArrCount > 0 then newArray _Constants_ConstantEntryArrCount else null) 0
                            | None -> _Fragment_FragmentStateOptCont null
                        match Descriptor.DepthStencil with
                        | Some v ->
                            let _Format_TextureFormat = v.Format.GetValue()
                            let _DepthWriteEnabled_bool = v.DepthWriteEnabled
                            let _DepthCompare_CompareFunction = v.DepthCompare.GetValue()
                            let _Compare_CompareFunction = v.StencilFront.Compare.GetValue()
                            let _FailOp_StencilOperation = v.StencilFront.FailOp.GetValue()
                            let _DepthFailOp_StencilOperation = v.StencilFront.DepthFailOp.GetValue()
                            let _PassOp_StencilOperation = v.StencilFront.PassOp.GetValue()
                            let _StencilFront_StencilFaceState = new DawnRaw.WGPUStencilFaceState()
                            _StencilFront_StencilFaceState.Compare <- _Compare_CompareFunction
                            _StencilFront_StencilFaceState.FailOp <- _FailOp_StencilOperation
                            _StencilFront_StencilFaceState.DepthFailOp <- _DepthFailOp_StencilOperation
                            _StencilFront_StencilFaceState.PassOp <- _PassOp_StencilOperation
                            let _StencilFront_StencilFaceState = _StencilFront_StencilFaceState
                            let _Compare_CompareFunction = v.StencilBack.Compare.GetValue()
                            let _FailOp_StencilOperation = v.StencilBack.FailOp.GetValue()
                            let _DepthFailOp_StencilOperation = v.StencilBack.DepthFailOp.GetValue()
                            let _PassOp_StencilOperation = v.StencilBack.PassOp.GetValue()
                            let _StencilBack_StencilFaceState = new DawnRaw.WGPUStencilFaceState()
                            _StencilBack_StencilFaceState.Compare <- _Compare_CompareFunction
                            _StencilBack_StencilFaceState.FailOp <- _FailOp_StencilOperation
                            _StencilBack_StencilFaceState.DepthFailOp <- _DepthFailOp_StencilOperation
                            _StencilBack_StencilFaceState.PassOp <- _PassOp_StencilOperation
                            let _StencilBack_StencilFaceState = _StencilBack_StencilFaceState
                            let _StencilReadMask_int = uint32 (v.StencilReadMask)
                            let _StencilWriteMask_int = uint32 (v.StencilWriteMask)
                            let _DepthBias_int32 = int32 (v.DepthBias)
                            let _DepthBiasSlopeScale_float32 = (v.DepthBiasSlopeScale)
                            let _DepthBiasClamp_float32 = (v.DepthBiasClamp)
                            let _n = new DawnRaw.WGPUDepthStencilState()
                            _n.Format <- _Format_TextureFormat
                            _n.DepthWriteEnabled <- _DepthWriteEnabled_bool
                            _n.DepthCompare <- _DepthCompare_CompareFunction
                            _n.StencilFront <- _StencilFront_StencilFaceState
                            _n.StencilBack <- _StencilBack_StencilFaceState
                            _n.StencilReadMask <- _StencilReadMask_int
                            _n.StencilWriteMask <- _StencilWriteMask_int
                            _n.DepthBias <- _DepthBias_int32
                            _n.DepthBiasSlopeScale <- _DepthBiasSlopeScale_float32
                            _n.DepthBiasClamp <- _DepthBiasClamp_float32
                            let _n = _n
                            _DepthStencil_DepthStencilStateOptCont _n
                        | None -> _DepthStencil_DepthStencilStateOptCont null
                    else
                        let _ArrayStride_uint64 = float (_Buffers_VertexBufferLayoutArrinputs.[_Buffers_VertexBufferLayoutArri].ArrayStride)
                        let _StepMode_VertexStepMode = _Buffers_VertexBufferLayoutArrinputs.[_Buffers_VertexBufferLayoutArri].StepMode.GetValue()
                        let _Attributes_VertexAttributeArrCount = if isNull _Buffers_VertexBufferLayoutArrinputs.[_Buffers_VertexBufferLayoutArri].Attributes then 0 else _Buffers_VertexBufferLayoutArrinputs.[_Buffers_VertexBufferLayoutArri].Attributes.Length
                        let rec _Attributes_VertexAttributeArrCont (_Attributes_VertexAttributeArrinputs : array<VertexAttribute>) (_Attributes_VertexAttributeArroutputs : JsArray) (_Attributes_VertexAttributeArri : int) =
                            if _Attributes_VertexAttributeArri >= _Attributes_VertexAttributeArrCount then
                                let _Attributes_VertexAttributeArr = _Attributes_VertexAttributeArroutputs.Reference
                                let _n = new DawnRaw.WGPUVertexBufferLayout()
                                _n.ArrayStride <- _ArrayStride_uint64
                                _n.StepMode <- _StepMode_VertexStepMode
                                _n.Attributes <- _Attributes_VertexAttributeArr
                                let _n = _n
                                _Buffers_VertexBufferLayoutArroutputs.[_Buffers_VertexBufferLayoutArri] <- js _n
                                _Buffers_VertexBufferLayoutArrCont _Buffers_VertexBufferLayoutArrinputs _Buffers_VertexBufferLayoutArroutputs (_Buffers_VertexBufferLayoutArri + 1)
                            else
                                let _Format_VertexFormat = _Attributes_VertexAttributeArrinputs.[_Attributes_VertexAttributeArri].Format.GetValue()
                                let _Offset_uint64 = float (_Attributes_VertexAttributeArrinputs.[_Attributes_VertexAttributeArri].Offset)
                                let _ShaderLocation_int = uint32 (_Attributes_VertexAttributeArrinputs.[_Attributes_VertexAttributeArri].ShaderLocation)
                                let _n = new DawnRaw.WGPUVertexAttribute()
                                _n.Format <- _Format_VertexFormat
                                _n.Offset <- _Offset_uint64
                                _n.ShaderLocation <- _ShaderLocation_int
                                let _n = _n
                                _Attributes_VertexAttributeArroutputs.[_Attributes_VertexAttributeArri] <- js _n
                                _Attributes_VertexAttributeArrCont _Attributes_VertexAttributeArrinputs _Attributes_VertexAttributeArroutputs (_Attributes_VertexAttributeArri + 1)
                        _Attributes_VertexAttributeArrCont _Buffers_VertexBufferLayoutArrinputs.[_Buffers_VertexBufferLayoutArri].Attributes (if _Attributes_VertexAttributeArrCount > 0 then newArray _Attributes_VertexAttributeArrCount else null) 0
                _Buffers_VertexBufferLayoutArrCont Descriptor.Vertex.Buffers (if _Buffers_VertexBufferLayoutArrCount > 0 then newArray _Buffers_VertexBufferLayoutArrCount else null) 0
            else
                let _Key_string = _Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Key
                let _Value_float = (_Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Value)
                let _n = new DawnRaw.WGPUConstantEntry()
                _n.Key <- _Key_string
                _n.Value <- _Value_float
                let _n = _n
                _Constants_ConstantEntryArroutputs.[_Constants_ConstantEntryArri] <- js _n
                _Constants_ConstantEntryArrCont _Constants_ConstantEntryArrinputs _Constants_ConstantEntryArroutputs (_Constants_ConstantEntryArri + 1)
        _Constants_ConstantEntryArrCont Descriptor.Vertex.Constants (if _Constants_ConstantEntryArrCount > 0 then newArray _Constants_ConstantEntryArrCount else null) 0
    member x.CreateRenderPipelineAsync(Descriptor : RenderPipelineDescriptor, Callback : CreateRenderPipelineAsyncCallback, Userdata : nativeint) : unit = 
        let _Label_string = Descriptor.Label
        let _Layout_PipelineLayout = (if isNull Descriptor.Layout then null else Descriptor.Layout.Handle)
        let _Module_ShaderModule = (if isNull Descriptor.Vertex.Module then null else Descriptor.Vertex.Module.Handle)
        let _EntryPoint_string = Descriptor.Vertex.EntryPoint
        let _Constants_ConstantEntryArrCount = if isNull Descriptor.Vertex.Constants then 0 else Descriptor.Vertex.Constants.Length
        let rec _Constants_ConstantEntryArrCont (_Constants_ConstantEntryArrinputs : array<ConstantEntry>) (_Constants_ConstantEntryArroutputs : JsArray) (_Constants_ConstantEntryArri : int) =
            if _Constants_ConstantEntryArri >= _Constants_ConstantEntryArrCount then
                let _Constants_ConstantEntryArr = _Constants_ConstantEntryArroutputs.Reference
                let _Buffers_VertexBufferLayoutArrCount = if isNull Descriptor.Vertex.Buffers then 0 else Descriptor.Vertex.Buffers.Length
                let rec _Buffers_VertexBufferLayoutArrCont (_Buffers_VertexBufferLayoutArrinputs : array<VertexBufferLayout>) (_Buffers_VertexBufferLayoutArroutputs : JsArray) (_Buffers_VertexBufferLayoutArri : int) =
                    if _Buffers_VertexBufferLayoutArri >= _Buffers_VertexBufferLayoutArrCount then
                        let _Buffers_VertexBufferLayoutArr = _Buffers_VertexBufferLayoutArroutputs.Reference
                        let _Vertex_VertexState = new DawnRaw.WGPUVertexState()
                        _Vertex_VertexState.Module <- _Module_ShaderModule
                        _Vertex_VertexState.EntryPoint <- _EntryPoint_string
                        _Vertex_VertexState.Constants <- _Constants_ConstantEntryArr
                        _Vertex_VertexState.Buffers <- _Buffers_VertexBufferLayoutArr
                        let _Vertex_VertexState = _Vertex_VertexState
                        let _Topology_PrimitiveTopology = Descriptor.Primitive.Topology.GetValue()
                        let _StripIndexFormat_IndexFormat = Descriptor.Primitive.StripIndexFormat.GetValue()
                        let _FrontFace_FrontFace = Descriptor.Primitive.FrontFace.GetValue()
                        let _CullMode_CullMode = Descriptor.Primitive.CullMode.GetValue()
                        let _Primitive_PrimitiveState = new DawnRaw.WGPUPrimitiveState()
                        _Primitive_PrimitiveState.Topology <- _Topology_PrimitiveTopology
                        _Primitive_PrimitiveState.StripIndexFormat <- _StripIndexFormat_IndexFormat
                        _Primitive_PrimitiveState.FrontFace <- _FrontFace_FrontFace
                        _Primitive_PrimitiveState.CullMode <- _CullMode_CullMode
                        let _Primitive_PrimitiveState = _Primitive_PrimitiveState
                        let inline _DepthStencil_DepthStencilStateOptCont _DepthStencil_DepthStencilStateOpt = 
                            let _Count_int = uint32 (Descriptor.Multisample.Count)
                            let _Mask_int = uint32 (Descriptor.Multisample.Mask)
                            let _AlphaToCoverageEnabled_bool = Descriptor.Multisample.AlphaToCoverageEnabled
                            let _Multisample_MultisampleState = new DawnRaw.WGPUMultisampleState()
                            _Multisample_MultisampleState.Count <- _Count_int
                            _Multisample_MultisampleState.Mask <- _Mask_int
                            _Multisample_MultisampleState.AlphaToCoverageEnabled <- _AlphaToCoverageEnabled_bool
                            let _Multisample_MultisampleState = _Multisample_MultisampleState
                            let inline _Fragment_FragmentStateOptCont _Fragment_FragmentStateOpt = 
                                let _Descriptor_RenderPipelineDescriptor = new DawnRaw.WGPURenderPipelineDescriptor()
                                _Descriptor_RenderPipelineDescriptor.Label <- _Label_string
                                _Descriptor_RenderPipelineDescriptor.Layout <- _Layout_PipelineLayout
                                _Descriptor_RenderPipelineDescriptor.Vertex <- _Vertex_VertexState
                                _Descriptor_RenderPipelineDescriptor.Primitive <- _Primitive_PrimitiveState
                                _Descriptor_RenderPipelineDescriptor.DepthStencil <- _DepthStencil_DepthStencilStateOpt
                                _Descriptor_RenderPipelineDescriptor.Multisample <- _Multisample_MultisampleState
                                _Descriptor_RenderPipelineDescriptor.Fragment <- _Fragment_FragmentStateOpt
                                let _Descriptor_RenderPipelineDescriptor = _Descriptor_RenderPipelineDescriptor
                                let mutable _Callback_CreateRenderPipelineAsyncCallbackGC = Unchecked.defaultof<System.Runtime.InteropServices.GCHandle>
                                let _Callback_CreateRenderPipelineAsyncCallbackFunction (Status : obj) (Pipeline : RenderPipelineHandle) (Message : string) (Userdata : float) = 
                                    let _Status_CreatePipelineAsyncStatus = Status |> System.Convert.ToInt32 |> unbox<CreatePipelineAsyncStatus>
                                    let _Pipeline_RenderPipeline = Pipeline
                                    let _Message_string = Message
                                    let _Userdata_nativeint = Userdata
                                    if _Callback_CreateRenderPipelineAsyncCallbackGC.IsAllocated then _Callback_CreateRenderPipelineAsyncCallbackGC.Free()
                                    Callback.Invoke(CreatePipelineAsyncStatus.Parse(_Status_CreatePipelineAsyncStatus), new RenderPipeline(x, _Pipeline_RenderPipeline, Unchecked.defaultof<_>), _Message_string, nativeint _Userdata_nativeint)
                                let _Callback_CreateRenderPipelineAsyncCallbackDel = WGPUCreateRenderPipelineAsyncCallback(_Callback_CreateRenderPipelineAsyncCallbackFunction)
                                _Callback_CreateRenderPipelineAsyncCallbackGC <- System.Runtime.InteropServices.GCHandle.Alloc(_Callback_CreateRenderPipelineAsyncCallbackDel)
                                let _Callback_CreateRenderPipelineAsyncCallback = _Callback_CreateRenderPipelineAsyncCallbackDel
                                let _Userdata_nativeint = float (Userdata)
                                x.Handle.Reference.Invoke("createRenderPipelineAsync", js _Descriptor_RenderPipelineDescriptor, js _Callback_CreateRenderPipelineAsyncCallback, js _Userdata_nativeint) |> ignore
                            match Descriptor.Fragment with
                            | Some v ->
                                let _Module_ShaderModule = (if isNull v.Module then null else v.Module.Handle)
                                let _EntryPoint_string = v.EntryPoint
                                let _Constants_ConstantEntryArrCount = if isNull v.Constants then 0 else v.Constants.Length
                                let rec _Constants_ConstantEntryArrCont (_Constants_ConstantEntryArrinputs : array<ConstantEntry>) (_Constants_ConstantEntryArroutputs : JsArray) (_Constants_ConstantEntryArri : int) =
                                    if _Constants_ConstantEntryArri >= _Constants_ConstantEntryArrCount then
                                        let _Constants_ConstantEntryArr = _Constants_ConstantEntryArroutputs.Reference
                                        let _Targets_ColorTargetStateArrCount = if isNull v.Targets then 0 else v.Targets.Length
                                        let rec _Targets_ColorTargetStateArrCont (_Targets_ColorTargetStateArrinputs : array<ColorTargetState>) (_Targets_ColorTargetStateArroutputs : JsArray) (_Targets_ColorTargetStateArri : int) =
                                            if _Targets_ColorTargetStateArri >= _Targets_ColorTargetStateArrCount then
                                                let _Targets_ColorTargetStateArr = _Targets_ColorTargetStateArroutputs.Reference
                                                let _n = new DawnRaw.WGPUFragmentState()
                                                _n.Module <- _Module_ShaderModule
                                                _n.EntryPoint <- _EntryPoint_string
                                                _n.Constants <- _Constants_ConstantEntryArr
                                                _n.Targets <- _Targets_ColorTargetStateArr
                                                let _n = _n
                                                _Fragment_FragmentStateOptCont _n
                                            else
                                                let _Format_TextureFormat = _Targets_ColorTargetStateArrinputs.[_Targets_ColorTargetStateArri].Format.GetValue()
                                                let inline _Blend_BlendStateOptCont _Blend_BlendStateOpt = 
                                                    let _WriteMask_ColorWriteMask = int (_Targets_ColorTargetStateArrinputs.[_Targets_ColorTargetStateArri].WriteMask)
                                                    let _n = new DawnRaw.WGPUColorTargetState()
                                                    _n.Format <- _Format_TextureFormat
                                                    _n.Blend <- _Blend_BlendStateOpt
                                                    _n.WriteMask <- _WriteMask_ColorWriteMask
                                                    let _n = _n
                                                    _Targets_ColorTargetStateArroutputs.[_Targets_ColorTargetStateArri] <- js _n
                                                    _Targets_ColorTargetStateArrCont _Targets_ColorTargetStateArrinputs _Targets_ColorTargetStateArroutputs (_Targets_ColorTargetStateArri + 1)
                                                match _Targets_ColorTargetStateArrinputs.[_Targets_ColorTargetStateArri].Blend with
                                                | Some v ->
                                                    let _Operation_BlendOperation = v.Color.Operation.GetValue()
                                                    let _SrcFactor_BlendFactor = v.Color.SrcFactor.GetValue()
                                                    let _DstFactor_BlendFactor = v.Color.DstFactor.GetValue()
                                                    let _Color_BlendComponent = new DawnRaw.WGPUBlendComponent()
                                                    _Color_BlendComponent.Operation <- _Operation_BlendOperation
                                                    _Color_BlendComponent.SrcFactor <- _SrcFactor_BlendFactor
                                                    _Color_BlendComponent.DstFactor <- _DstFactor_BlendFactor
                                                    let _Color_BlendComponent = _Color_BlendComponent
                                                    let _Operation_BlendOperation = v.Alpha.Operation.GetValue()
                                                    let _SrcFactor_BlendFactor = v.Alpha.SrcFactor.GetValue()
                                                    let _DstFactor_BlendFactor = v.Alpha.DstFactor.GetValue()
                                                    let _Alpha_BlendComponent = new DawnRaw.WGPUBlendComponent()
                                                    _Alpha_BlendComponent.Operation <- _Operation_BlendOperation
                                                    _Alpha_BlendComponent.SrcFactor <- _SrcFactor_BlendFactor
                                                    _Alpha_BlendComponent.DstFactor <- _DstFactor_BlendFactor
                                                    let _Alpha_BlendComponent = _Alpha_BlendComponent
                                                    let _n = new DawnRaw.WGPUBlendState()
                                                    _n.Color <- _Color_BlendComponent
                                                    _n.Alpha <- _Alpha_BlendComponent
                                                    let _n = _n
                                                    _Blend_BlendStateOptCont _n
                                                | None -> _Blend_BlendStateOptCont null
                                        _Targets_ColorTargetStateArrCont v.Targets (if _Targets_ColorTargetStateArrCount > 0 then newArray _Targets_ColorTargetStateArrCount else null) 0
                                    else
                                        let _Key_string = _Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Key
                                        let _Value_float = (_Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Value)
                                        let _n = new DawnRaw.WGPUConstantEntry()
                                        _n.Key <- _Key_string
                                        _n.Value <- _Value_float
                                        let _n = _n
                                        _Constants_ConstantEntryArroutputs.[_Constants_ConstantEntryArri] <- js _n
                                        _Constants_ConstantEntryArrCont _Constants_ConstantEntryArrinputs _Constants_ConstantEntryArroutputs (_Constants_ConstantEntryArri + 1)
                                _Constants_ConstantEntryArrCont v.Constants (if _Constants_ConstantEntryArrCount > 0 then newArray _Constants_ConstantEntryArrCount else null) 0
                            | None -> _Fragment_FragmentStateOptCont null
                        match Descriptor.DepthStencil with
                        | Some v ->
                            let _Format_TextureFormat = v.Format.GetValue()
                            let _DepthWriteEnabled_bool = v.DepthWriteEnabled
                            let _DepthCompare_CompareFunction = v.DepthCompare.GetValue()
                            let _Compare_CompareFunction = v.StencilFront.Compare.GetValue()
                            let _FailOp_StencilOperation = v.StencilFront.FailOp.GetValue()
                            let _DepthFailOp_StencilOperation = v.StencilFront.DepthFailOp.GetValue()
                            let _PassOp_StencilOperation = v.StencilFront.PassOp.GetValue()
                            let _StencilFront_StencilFaceState = new DawnRaw.WGPUStencilFaceState()
                            _StencilFront_StencilFaceState.Compare <- _Compare_CompareFunction
                            _StencilFront_StencilFaceState.FailOp <- _FailOp_StencilOperation
                            _StencilFront_StencilFaceState.DepthFailOp <- _DepthFailOp_StencilOperation
                            _StencilFront_StencilFaceState.PassOp <- _PassOp_StencilOperation
                            let _StencilFront_StencilFaceState = _StencilFront_StencilFaceState
                            let _Compare_CompareFunction = v.StencilBack.Compare.GetValue()
                            let _FailOp_StencilOperation = v.StencilBack.FailOp.GetValue()
                            let _DepthFailOp_StencilOperation = v.StencilBack.DepthFailOp.GetValue()
                            let _PassOp_StencilOperation = v.StencilBack.PassOp.GetValue()
                            let _StencilBack_StencilFaceState = new DawnRaw.WGPUStencilFaceState()
                            _StencilBack_StencilFaceState.Compare <- _Compare_CompareFunction
                            _StencilBack_StencilFaceState.FailOp <- _FailOp_StencilOperation
                            _StencilBack_StencilFaceState.DepthFailOp <- _DepthFailOp_StencilOperation
                            _StencilBack_StencilFaceState.PassOp <- _PassOp_StencilOperation
                            let _StencilBack_StencilFaceState = _StencilBack_StencilFaceState
                            let _StencilReadMask_int = uint32 (v.StencilReadMask)
                            let _StencilWriteMask_int = uint32 (v.StencilWriteMask)
                            let _DepthBias_int32 = int32 (v.DepthBias)
                            let _DepthBiasSlopeScale_float32 = (v.DepthBiasSlopeScale)
                            let _DepthBiasClamp_float32 = (v.DepthBiasClamp)
                            let _n = new DawnRaw.WGPUDepthStencilState()
                            _n.Format <- _Format_TextureFormat
                            _n.DepthWriteEnabled <- _DepthWriteEnabled_bool
                            _n.DepthCompare <- _DepthCompare_CompareFunction
                            _n.StencilFront <- _StencilFront_StencilFaceState
                            _n.StencilBack <- _StencilBack_StencilFaceState
                            _n.StencilReadMask <- _StencilReadMask_int
                            _n.StencilWriteMask <- _StencilWriteMask_int
                            _n.DepthBias <- _DepthBias_int32
                            _n.DepthBiasSlopeScale <- _DepthBiasSlopeScale_float32
                            _n.DepthBiasClamp <- _DepthBiasClamp_float32
                            let _n = _n
                            _DepthStencil_DepthStencilStateOptCont _n
                        | None -> _DepthStencil_DepthStencilStateOptCont null
                    else
                        let _ArrayStride_uint64 = float (_Buffers_VertexBufferLayoutArrinputs.[_Buffers_VertexBufferLayoutArri].ArrayStride)
                        let _StepMode_VertexStepMode = _Buffers_VertexBufferLayoutArrinputs.[_Buffers_VertexBufferLayoutArri].StepMode.GetValue()
                        let _Attributes_VertexAttributeArrCount = if isNull _Buffers_VertexBufferLayoutArrinputs.[_Buffers_VertexBufferLayoutArri].Attributes then 0 else _Buffers_VertexBufferLayoutArrinputs.[_Buffers_VertexBufferLayoutArri].Attributes.Length
                        let rec _Attributes_VertexAttributeArrCont (_Attributes_VertexAttributeArrinputs : array<VertexAttribute>) (_Attributes_VertexAttributeArroutputs : JsArray) (_Attributes_VertexAttributeArri : int) =
                            if _Attributes_VertexAttributeArri >= _Attributes_VertexAttributeArrCount then
                                let _Attributes_VertexAttributeArr = _Attributes_VertexAttributeArroutputs.Reference
                                let _n = new DawnRaw.WGPUVertexBufferLayout()
                                _n.ArrayStride <- _ArrayStride_uint64
                                _n.StepMode <- _StepMode_VertexStepMode
                                _n.Attributes <- _Attributes_VertexAttributeArr
                                let _n = _n
                                _Buffers_VertexBufferLayoutArroutputs.[_Buffers_VertexBufferLayoutArri] <- js _n
                                _Buffers_VertexBufferLayoutArrCont _Buffers_VertexBufferLayoutArrinputs _Buffers_VertexBufferLayoutArroutputs (_Buffers_VertexBufferLayoutArri + 1)
                            else
                                let _Format_VertexFormat = _Attributes_VertexAttributeArrinputs.[_Attributes_VertexAttributeArri].Format.GetValue()
                                let _Offset_uint64 = float (_Attributes_VertexAttributeArrinputs.[_Attributes_VertexAttributeArri].Offset)
                                let _ShaderLocation_int = uint32 (_Attributes_VertexAttributeArrinputs.[_Attributes_VertexAttributeArri].ShaderLocation)
                                let _n = new DawnRaw.WGPUVertexAttribute()
                                _n.Format <- _Format_VertexFormat
                                _n.Offset <- _Offset_uint64
                                _n.ShaderLocation <- _ShaderLocation_int
                                let _n = _n
                                _Attributes_VertexAttributeArroutputs.[_Attributes_VertexAttributeArri] <- js _n
                                _Attributes_VertexAttributeArrCont _Attributes_VertexAttributeArrinputs _Attributes_VertexAttributeArroutputs (_Attributes_VertexAttributeArri + 1)
                        _Attributes_VertexAttributeArrCont _Buffers_VertexBufferLayoutArrinputs.[_Buffers_VertexBufferLayoutArri].Attributes (if _Attributes_VertexAttributeArrCount > 0 then newArray _Attributes_VertexAttributeArrCount else null) 0
                _Buffers_VertexBufferLayoutArrCont Descriptor.Vertex.Buffers (if _Buffers_VertexBufferLayoutArrCount > 0 then newArray _Buffers_VertexBufferLayoutArrCount else null) 0
            else
                let _Key_string = _Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Key
                let _Value_float = (_Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Value)
                let _n = new DawnRaw.WGPUConstantEntry()
                _n.Key <- _Key_string
                _n.Value <- _Value_float
                let _n = _n
                _Constants_ConstantEntryArroutputs.[_Constants_ConstantEntryArri] <- js _n
                _Constants_ConstantEntryArrCont _Constants_ConstantEntryArrinputs _Constants_ConstantEntryArroutputs (_Constants_ConstantEntryArri + 1)
        _Constants_ConstantEntryArrCont Descriptor.Vertex.Constants (if _Constants_ConstantEntryArrCount > 0 then newArray _Constants_ConstantEntryArrCount else null) 0
    member x.CreateRenderBundleEncoder(Descriptor : RenderBundleEncoderDescriptor) : RenderBundleEncoder = 
        let _Label_string = Descriptor.Label
        let _ColorFormats_TextureFormatArrCount = Descriptor.ColorFormats.Length
        let _ColorFormats_TextureFormatArrArray = newArray (_ColorFormats_TextureFormatArrCount)
        for i in 0 .. _ColorFormats_TextureFormatArrCount-1 do
            _ColorFormats_TextureFormatArrArray.[i] <- Descriptor.ColorFormats.[i].GetValue()
        let _ColorFormats_TextureFormatArr = _ColorFormats_TextureFormatArrArray.Reference
        let _DepthStencilFormat_TextureFormat = Descriptor.DepthStencilFormat.GetValue()
        let _SampleCount_int = uint32 (Descriptor.SampleCount)
        let _Descriptor_RenderBundleEncoderDescriptor = new DawnRaw.WGPURenderBundleEncoderDescriptor()
        _Descriptor_RenderBundleEncoderDescriptor.Label <- _Label_string
        _Descriptor_RenderBundleEncoderDescriptor.ColorFormats <- _ColorFormats_TextureFormatArr
        _Descriptor_RenderBundleEncoderDescriptor.DepthStencilFormat <- _DepthStencilFormat_TextureFormat
        _Descriptor_RenderBundleEncoderDescriptor.SampleCount <- _SampleCount_int
        let _Descriptor_RenderBundleEncoderDescriptor = _Descriptor_RenderBundleEncoderDescriptor
        new RenderBundleEncoder(x, convert(x.Handle.Reference.Invoke("createRenderBundleEncoder", js _Descriptor_RenderBundleEncoderDescriptor)), Descriptor)
    member x.CreateRenderPipeline(Descriptor : RenderPipelineDescriptor) : RenderPipeline = 
        let _Label_string = Descriptor.Label
        let _Layout_PipelineLayout = (if isNull Descriptor.Layout then null else Descriptor.Layout.Handle)
        let _Module_ShaderModule = (if isNull Descriptor.Vertex.Module then null else Descriptor.Vertex.Module.Handle)
        let _EntryPoint_string = Descriptor.Vertex.EntryPoint
        let _Constants_ConstantEntryArrCount = if isNull Descriptor.Vertex.Constants then 0 else Descriptor.Vertex.Constants.Length
        let rec _Constants_ConstantEntryArrCont (_Constants_ConstantEntryArrinputs : array<ConstantEntry>) (_Constants_ConstantEntryArroutputs : JsArray) (_Constants_ConstantEntryArri : int) =
            if _Constants_ConstantEntryArri >= _Constants_ConstantEntryArrCount then
                let _Constants_ConstantEntryArr = _Constants_ConstantEntryArroutputs.Reference
                let _Buffers_VertexBufferLayoutArrCount = if isNull Descriptor.Vertex.Buffers then 0 else Descriptor.Vertex.Buffers.Length
                let rec _Buffers_VertexBufferLayoutArrCont (_Buffers_VertexBufferLayoutArrinputs : array<VertexBufferLayout>) (_Buffers_VertexBufferLayoutArroutputs : JsArray) (_Buffers_VertexBufferLayoutArri : int) =
                    if _Buffers_VertexBufferLayoutArri >= _Buffers_VertexBufferLayoutArrCount then
                        let _Buffers_VertexBufferLayoutArr = _Buffers_VertexBufferLayoutArroutputs.Reference
                        let _Vertex_VertexState = new DawnRaw.WGPUVertexState()
                        _Vertex_VertexState.Module <- _Module_ShaderModule
                        _Vertex_VertexState.EntryPoint <- _EntryPoint_string
                        _Vertex_VertexState.Constants <- _Constants_ConstantEntryArr
                        _Vertex_VertexState.Buffers <- _Buffers_VertexBufferLayoutArr
                        let _Vertex_VertexState = _Vertex_VertexState
                        let _Topology_PrimitiveTopology = Descriptor.Primitive.Topology.GetValue()
                        let _StripIndexFormat_IndexFormat = Descriptor.Primitive.StripIndexFormat.GetValue()
                        let _FrontFace_FrontFace = Descriptor.Primitive.FrontFace.GetValue()
                        let _CullMode_CullMode = Descriptor.Primitive.CullMode.GetValue()
                        let _Primitive_PrimitiveState = new DawnRaw.WGPUPrimitiveState()
                        _Primitive_PrimitiveState.Topology <- _Topology_PrimitiveTopology
                        _Primitive_PrimitiveState.StripIndexFormat <- _StripIndexFormat_IndexFormat
                        _Primitive_PrimitiveState.FrontFace <- _FrontFace_FrontFace
                        _Primitive_PrimitiveState.CullMode <- _CullMode_CullMode
                        let _Primitive_PrimitiveState = _Primitive_PrimitiveState
                        let inline _DepthStencil_DepthStencilStateOptCont _DepthStencil_DepthStencilStateOpt = 
                            let _Count_int = uint32 (Descriptor.Multisample.Count)
                            let _Mask_int = uint32 (Descriptor.Multisample.Mask)
                            let _AlphaToCoverageEnabled_bool = Descriptor.Multisample.AlphaToCoverageEnabled
                            let _Multisample_MultisampleState = new DawnRaw.WGPUMultisampleState()
                            _Multisample_MultisampleState.Count <- _Count_int
                            _Multisample_MultisampleState.Mask <- _Mask_int
                            _Multisample_MultisampleState.AlphaToCoverageEnabled <- _AlphaToCoverageEnabled_bool
                            let _Multisample_MultisampleState = _Multisample_MultisampleState
                            let inline _Fragment_FragmentStateOptCont _Fragment_FragmentStateOpt = 
                                let _Descriptor_RenderPipelineDescriptor = new DawnRaw.WGPURenderPipelineDescriptor()
                                _Descriptor_RenderPipelineDescriptor.Label <- _Label_string
                                _Descriptor_RenderPipelineDescriptor.Layout <- _Layout_PipelineLayout
                                _Descriptor_RenderPipelineDescriptor.Vertex <- _Vertex_VertexState
                                _Descriptor_RenderPipelineDescriptor.Primitive <- _Primitive_PrimitiveState
                                _Descriptor_RenderPipelineDescriptor.DepthStencil <- _DepthStencil_DepthStencilStateOpt
                                _Descriptor_RenderPipelineDescriptor.Multisample <- _Multisample_MultisampleState
                                _Descriptor_RenderPipelineDescriptor.Fragment <- _Fragment_FragmentStateOpt
                                let _Descriptor_RenderPipelineDescriptor = _Descriptor_RenderPipelineDescriptor
                                new RenderPipeline(x, convert(x.Handle.Reference.Invoke("createRenderPipeline", js _Descriptor_RenderPipelineDescriptor)), Descriptor)
                            match Descriptor.Fragment with
                            | Some v ->
                                let _Module_ShaderModule = (if isNull v.Module then null else v.Module.Handle)
                                let _EntryPoint_string = v.EntryPoint
                                let _Constants_ConstantEntryArrCount = if isNull v.Constants then 0 else v.Constants.Length
                                let rec _Constants_ConstantEntryArrCont (_Constants_ConstantEntryArrinputs : array<ConstantEntry>) (_Constants_ConstantEntryArroutputs : JsArray) (_Constants_ConstantEntryArri : int) =
                                    if _Constants_ConstantEntryArri >= _Constants_ConstantEntryArrCount then
                                        let _Constants_ConstantEntryArr = _Constants_ConstantEntryArroutputs.Reference
                                        let _Targets_ColorTargetStateArrCount = if isNull v.Targets then 0 else v.Targets.Length
                                        let rec _Targets_ColorTargetStateArrCont (_Targets_ColorTargetStateArrinputs : array<ColorTargetState>) (_Targets_ColorTargetStateArroutputs : JsArray) (_Targets_ColorTargetStateArri : int) =
                                            if _Targets_ColorTargetStateArri >= _Targets_ColorTargetStateArrCount then
                                                let _Targets_ColorTargetStateArr = _Targets_ColorTargetStateArroutputs.Reference
                                                let _n = new DawnRaw.WGPUFragmentState()
                                                _n.Module <- _Module_ShaderModule
                                                _n.EntryPoint <- _EntryPoint_string
                                                _n.Constants <- _Constants_ConstantEntryArr
                                                _n.Targets <- _Targets_ColorTargetStateArr
                                                let _n = _n
                                                _Fragment_FragmentStateOptCont _n
                                            else
                                                let _Format_TextureFormat = _Targets_ColorTargetStateArrinputs.[_Targets_ColorTargetStateArri].Format.GetValue()
                                                let inline _Blend_BlendStateOptCont _Blend_BlendStateOpt = 
                                                    let _WriteMask_ColorWriteMask = int (_Targets_ColorTargetStateArrinputs.[_Targets_ColorTargetStateArri].WriteMask)
                                                    let _n = new DawnRaw.WGPUColorTargetState()
                                                    _n.Format <- _Format_TextureFormat
                                                    _n.Blend <- _Blend_BlendStateOpt
                                                    _n.WriteMask <- _WriteMask_ColorWriteMask
                                                    let _n = _n
                                                    _Targets_ColorTargetStateArroutputs.[_Targets_ColorTargetStateArri] <- js _n
                                                    _Targets_ColorTargetStateArrCont _Targets_ColorTargetStateArrinputs _Targets_ColorTargetStateArroutputs (_Targets_ColorTargetStateArri + 1)
                                                match _Targets_ColorTargetStateArrinputs.[_Targets_ColorTargetStateArri].Blend with
                                                | Some v ->
                                                    let _Operation_BlendOperation = v.Color.Operation.GetValue()
                                                    let _SrcFactor_BlendFactor = v.Color.SrcFactor.GetValue()
                                                    let _DstFactor_BlendFactor = v.Color.DstFactor.GetValue()
                                                    let _Color_BlendComponent = new DawnRaw.WGPUBlendComponent()
                                                    _Color_BlendComponent.Operation <- _Operation_BlendOperation
                                                    _Color_BlendComponent.SrcFactor <- _SrcFactor_BlendFactor
                                                    _Color_BlendComponent.DstFactor <- _DstFactor_BlendFactor
                                                    let _Color_BlendComponent = _Color_BlendComponent
                                                    let _Operation_BlendOperation = v.Alpha.Operation.GetValue()
                                                    let _SrcFactor_BlendFactor = v.Alpha.SrcFactor.GetValue()
                                                    let _DstFactor_BlendFactor = v.Alpha.DstFactor.GetValue()
                                                    let _Alpha_BlendComponent = new DawnRaw.WGPUBlendComponent()
                                                    _Alpha_BlendComponent.Operation <- _Operation_BlendOperation
                                                    _Alpha_BlendComponent.SrcFactor <- _SrcFactor_BlendFactor
                                                    _Alpha_BlendComponent.DstFactor <- _DstFactor_BlendFactor
                                                    let _Alpha_BlendComponent = _Alpha_BlendComponent
                                                    let _n = new DawnRaw.WGPUBlendState()
                                                    _n.Color <- _Color_BlendComponent
                                                    _n.Alpha <- _Alpha_BlendComponent
                                                    let _n = _n
                                                    _Blend_BlendStateOptCont _n
                                                | None -> _Blend_BlendStateOptCont null
                                        _Targets_ColorTargetStateArrCont v.Targets (if _Targets_ColorTargetStateArrCount > 0 then newArray _Targets_ColorTargetStateArrCount else null) 0
                                    else
                                        let _Key_string = _Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Key
                                        let _Value_float = (_Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Value)
                                        let _n = new DawnRaw.WGPUConstantEntry()
                                        _n.Key <- _Key_string
                                        _n.Value <- _Value_float
                                        let _n = _n
                                        _Constants_ConstantEntryArroutputs.[_Constants_ConstantEntryArri] <- js _n
                                        _Constants_ConstantEntryArrCont _Constants_ConstantEntryArrinputs _Constants_ConstantEntryArroutputs (_Constants_ConstantEntryArri + 1)
                                _Constants_ConstantEntryArrCont v.Constants (if _Constants_ConstantEntryArrCount > 0 then newArray _Constants_ConstantEntryArrCount else null) 0
                            | None -> _Fragment_FragmentStateOptCont null
                        match Descriptor.DepthStencil with
                        | Some v ->
                            let _Format_TextureFormat = v.Format.GetValue()
                            let _DepthWriteEnabled_bool = v.DepthWriteEnabled
                            let _DepthCompare_CompareFunction = v.DepthCompare.GetValue()
                            let _Compare_CompareFunction = v.StencilFront.Compare.GetValue()
                            let _FailOp_StencilOperation = v.StencilFront.FailOp.GetValue()
                            let _DepthFailOp_StencilOperation = v.StencilFront.DepthFailOp.GetValue()
                            let _PassOp_StencilOperation = v.StencilFront.PassOp.GetValue()
                            let _StencilFront_StencilFaceState = new DawnRaw.WGPUStencilFaceState()
                            _StencilFront_StencilFaceState.Compare <- _Compare_CompareFunction
                            _StencilFront_StencilFaceState.FailOp <- _FailOp_StencilOperation
                            _StencilFront_StencilFaceState.DepthFailOp <- _DepthFailOp_StencilOperation
                            _StencilFront_StencilFaceState.PassOp <- _PassOp_StencilOperation
                            let _StencilFront_StencilFaceState = _StencilFront_StencilFaceState
                            let _Compare_CompareFunction = v.StencilBack.Compare.GetValue()
                            let _FailOp_StencilOperation = v.StencilBack.FailOp.GetValue()
                            let _DepthFailOp_StencilOperation = v.StencilBack.DepthFailOp.GetValue()
                            let _PassOp_StencilOperation = v.StencilBack.PassOp.GetValue()
                            let _StencilBack_StencilFaceState = new DawnRaw.WGPUStencilFaceState()
                            _StencilBack_StencilFaceState.Compare <- _Compare_CompareFunction
                            _StencilBack_StencilFaceState.FailOp <- _FailOp_StencilOperation
                            _StencilBack_StencilFaceState.DepthFailOp <- _DepthFailOp_StencilOperation
                            _StencilBack_StencilFaceState.PassOp <- _PassOp_StencilOperation
                            let _StencilBack_StencilFaceState = _StencilBack_StencilFaceState
                            let _StencilReadMask_int = uint32 (v.StencilReadMask)
                            let _StencilWriteMask_int = uint32 (v.StencilWriteMask)
                            let _DepthBias_int32 = int32 (v.DepthBias)
                            let _DepthBiasSlopeScale_float32 = (v.DepthBiasSlopeScale)
                            let _DepthBiasClamp_float32 = (v.DepthBiasClamp)
                            let _n = new DawnRaw.WGPUDepthStencilState()
                            _n.Format <- _Format_TextureFormat
                            _n.DepthWriteEnabled <- _DepthWriteEnabled_bool
                            _n.DepthCompare <- _DepthCompare_CompareFunction
                            _n.StencilFront <- _StencilFront_StencilFaceState
                            _n.StencilBack <- _StencilBack_StencilFaceState
                            _n.StencilReadMask <- _StencilReadMask_int
                            _n.StencilWriteMask <- _StencilWriteMask_int
                            _n.DepthBias <- _DepthBias_int32
                            _n.DepthBiasSlopeScale <- _DepthBiasSlopeScale_float32
                            _n.DepthBiasClamp <- _DepthBiasClamp_float32
                            let _n = _n
                            _DepthStencil_DepthStencilStateOptCont _n
                        | None -> _DepthStencil_DepthStencilStateOptCont null
                    else
                        let _ArrayStride_uint64 = float (_Buffers_VertexBufferLayoutArrinputs.[_Buffers_VertexBufferLayoutArri].ArrayStride)
                        let _StepMode_VertexStepMode = _Buffers_VertexBufferLayoutArrinputs.[_Buffers_VertexBufferLayoutArri].StepMode.GetValue()
                        let _Attributes_VertexAttributeArrCount = if isNull _Buffers_VertexBufferLayoutArrinputs.[_Buffers_VertexBufferLayoutArri].Attributes then 0 else _Buffers_VertexBufferLayoutArrinputs.[_Buffers_VertexBufferLayoutArri].Attributes.Length
                        let rec _Attributes_VertexAttributeArrCont (_Attributes_VertexAttributeArrinputs : array<VertexAttribute>) (_Attributes_VertexAttributeArroutputs : JsArray) (_Attributes_VertexAttributeArri : int) =
                            if _Attributes_VertexAttributeArri >= _Attributes_VertexAttributeArrCount then
                                let _Attributes_VertexAttributeArr = _Attributes_VertexAttributeArroutputs.Reference
                                let _n = new DawnRaw.WGPUVertexBufferLayout()
                                _n.ArrayStride <- _ArrayStride_uint64
                                _n.StepMode <- _StepMode_VertexStepMode
                                _n.Attributes <- _Attributes_VertexAttributeArr
                                let _n = _n
                                _Buffers_VertexBufferLayoutArroutputs.[_Buffers_VertexBufferLayoutArri] <- js _n
                                _Buffers_VertexBufferLayoutArrCont _Buffers_VertexBufferLayoutArrinputs _Buffers_VertexBufferLayoutArroutputs (_Buffers_VertexBufferLayoutArri + 1)
                            else
                                let _Format_VertexFormat = _Attributes_VertexAttributeArrinputs.[_Attributes_VertexAttributeArri].Format.GetValue()
                                let _Offset_uint64 = float (_Attributes_VertexAttributeArrinputs.[_Attributes_VertexAttributeArri].Offset)
                                let _ShaderLocation_int = uint32 (_Attributes_VertexAttributeArrinputs.[_Attributes_VertexAttributeArri].ShaderLocation)
                                let _n = new DawnRaw.WGPUVertexAttribute()
                                _n.Format <- _Format_VertexFormat
                                _n.Offset <- _Offset_uint64
                                _n.ShaderLocation <- _ShaderLocation_int
                                let _n = _n
                                _Attributes_VertexAttributeArroutputs.[_Attributes_VertexAttributeArri] <- js _n
                                _Attributes_VertexAttributeArrCont _Attributes_VertexAttributeArrinputs _Attributes_VertexAttributeArroutputs (_Attributes_VertexAttributeArri + 1)
                        _Attributes_VertexAttributeArrCont _Buffers_VertexBufferLayoutArrinputs.[_Buffers_VertexBufferLayoutArri].Attributes (if _Attributes_VertexAttributeArrCount > 0 then newArray _Attributes_VertexAttributeArrCount else null) 0
                _Buffers_VertexBufferLayoutArrCont Descriptor.Vertex.Buffers (if _Buffers_VertexBufferLayoutArrCount > 0 then newArray _Buffers_VertexBufferLayoutArrCount else null) 0
            else
                let _Key_string = _Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Key
                let _Value_float = (_Constants_ConstantEntryArrinputs.[_Constants_ConstantEntryArri].Value)
                let _n = new DawnRaw.WGPUConstantEntry()
                _n.Key <- _Key_string
                _n.Value <- _Value_float
                let _n = _n
                _Constants_ConstantEntryArroutputs.[_Constants_ConstantEntryArri] <- js _n
                _Constants_ConstantEntryArrCont _Constants_ConstantEntryArrinputs _Constants_ConstantEntryArroutputs (_Constants_ConstantEntryArri + 1)
        _Constants_ConstantEntryArrCont Descriptor.Vertex.Constants (if _Constants_ConstantEntryArrCount > 0 then newArray _Constants_ConstantEntryArrCount else null) 0
    member x.CreateSampler() : Sampler = 
        new Sampler(x, convert(x.Handle.Reference.Invoke("createSampler")), SamplerDescriptor.Default)
    member x.CreateSampler(Descriptor : SamplerDescriptor) : Sampler = 
        let _Label_string = Descriptor.Label
        let _AddressModeU_AddressMode = Descriptor.AddressModeU.GetValue()
        let _AddressModeV_AddressMode = Descriptor.AddressModeV.GetValue()
        let _AddressModeW_AddressMode = Descriptor.AddressModeW.GetValue()
        let _MagFilter_FilterMode = Descriptor.MagFilter.GetValue()
        let _MinFilter_FilterMode = Descriptor.MinFilter.GetValue()
        let _MipmapFilter_FilterMode = Descriptor.MipmapFilter.GetValue()
        let _LodMinClamp_float32 = (Descriptor.LodMinClamp)
        let _LodMaxClamp_float32 = (Descriptor.LodMaxClamp)
        let _Compare_CompareFunction = Descriptor.Compare.GetValue()
        let _MaxAnisotropy_uint16 = uint16 (Descriptor.MaxAnisotropy)
        let _Descriptor_SamplerDescriptor = new DawnRaw.WGPUSamplerDescriptor()
        _Descriptor_SamplerDescriptor.Label <- _Label_string
        _Descriptor_SamplerDescriptor.AddressModeU <- _AddressModeU_AddressMode
        _Descriptor_SamplerDescriptor.AddressModeV <- _AddressModeV_AddressMode
        _Descriptor_SamplerDescriptor.AddressModeW <- _AddressModeW_AddressMode
        _Descriptor_SamplerDescriptor.MagFilter <- _MagFilter_FilterMode
        _Descriptor_SamplerDescriptor.MinFilter <- _MinFilter_FilterMode
        _Descriptor_SamplerDescriptor.MipmapFilter <- _MipmapFilter_FilterMode
        _Descriptor_SamplerDescriptor.LodMinClamp <- _LodMinClamp_float32
        _Descriptor_SamplerDescriptor.LodMaxClamp <- _LodMaxClamp_float32
        _Descriptor_SamplerDescriptor.Compare <- _Compare_CompareFunction
        _Descriptor_SamplerDescriptor.MaxAnisotropy <- _MaxAnisotropy_uint16
        let _Descriptor_SamplerDescriptor = _Descriptor_SamplerDescriptor
        new Sampler(x, convert(x.Handle.Reference.Invoke("createSampler", js _Descriptor_SamplerDescriptor)), Descriptor)
    member x.CreateShaderModule() : ShaderModule = 
        new ShaderModule(x, convert(x.Handle.Reference.Invoke("createShaderModule")), ShaderModuleDescriptor.Default)
    member x.CreateShaderModule(Descriptor : ShaderModuleDescriptor) : ShaderModule = 
        let _Label_string = Descriptor.Label
        let _Descriptor_ShaderModuleDescriptor = new DawnRaw.WGPUShaderModuleDescriptor()
        _Descriptor_ShaderModuleDescriptor.Label <- _Label_string
        let _Descriptor_ShaderModuleDescriptor = _Descriptor_ShaderModuleDescriptor
        new ShaderModule(x, convert(x.Handle.Reference.Invoke("createShaderModule", js _Descriptor_ShaderModuleDescriptor)), Descriptor)
    member x.CreateSwapChain(Surface : Surface, Descriptor : SwapChainDescriptor) : SwapChain = 
        let _Surface_Surface = (if isNull Surface then null else Surface.Handle)
        let _Label_string = Descriptor.Label
        let _Usage_TextureUsage = int (Descriptor.Usage)
        let _Format_TextureFormat = Descriptor.Format.GetValue()
        let _Width_int = uint32 (Descriptor.Width)
        let _Height_int = uint32 (Descriptor.Height)
        let _PresentMode_PresentMode = Descriptor.PresentMode.GetValue()
        let _Implementation_uint64 = float (Descriptor.Implementation)
        let _Descriptor_SwapChainDescriptor = new DawnRaw.WGPUSwapChainDescriptor()
        _Descriptor_SwapChainDescriptor.Label <- _Label_string
        _Descriptor_SwapChainDescriptor.Usage <- _Usage_TextureUsage
        _Descriptor_SwapChainDescriptor.Format <- _Format_TextureFormat
        _Descriptor_SwapChainDescriptor.Width <- _Width_int
        _Descriptor_SwapChainDescriptor.Height <- _Height_int
        _Descriptor_SwapChainDescriptor.PresentMode <- _PresentMode_PresentMode
        _Descriptor_SwapChainDescriptor.Implementation <- _Implementation_uint64
        let _Descriptor_SwapChainDescriptor = _Descriptor_SwapChainDescriptor
        new SwapChain(x, convert(x.Handle.Reference.Invoke("createSwapChain", js _Surface_Surface, js _Descriptor_SwapChainDescriptor)))
    member x.CreateTexture(Descriptor : TextureDescriptor) : Texture = 
        let _Label_string = Descriptor.Label
        let _Usage_TextureUsage = int (Descriptor.Usage)
        let _Dimension_TextureDimension = Descriptor.Dimension.GetValue()
        let _Width_int = uint32 (Descriptor.Size.Width)
        let _Height_int = uint32 (Descriptor.Size.Height)
        let _DepthOrArrayLayers_int = uint32 (Descriptor.Size.DepthOrArrayLayers)
        let _Size_Extent3D = new DawnRaw.WGPUExtent3D()
        _Size_Extent3D.Width <- _Width_int
        _Size_Extent3D.Height <- _Height_int
        _Size_Extent3D.DepthOrArrayLayers <- _DepthOrArrayLayers_int
        let _Size_Extent3D = _Size_Extent3D
        let _Format_TextureFormat = Descriptor.Format.GetValue()
        let _MipLevelCount_int = uint32 (Descriptor.MipLevelCount)
        let _SampleCount_int = uint32 (Descriptor.SampleCount)
        let _Descriptor_TextureDescriptor = new DawnRaw.WGPUTextureDescriptor()
        _Descriptor_TextureDescriptor.Label <- _Label_string
        _Descriptor_TextureDescriptor.Usage <- _Usage_TextureUsage
        _Descriptor_TextureDescriptor.Dimension <- _Dimension_TextureDimension
        _Descriptor_TextureDescriptor.Size <- _Size_Extent3D
        _Descriptor_TextureDescriptor.Format <- _Format_TextureFormat
        _Descriptor_TextureDescriptor.MipLevelCount <- _MipLevelCount_int
        _Descriptor_TextureDescriptor.SampleCount <- _SampleCount_int
        let _Descriptor_TextureDescriptor = _Descriptor_TextureDescriptor
        new Texture(x, convert(x.Handle.Reference.Invoke("createTexture", js _Descriptor_TextureDescriptor)), Descriptor)
    member x.Destroy() : unit = 
        x.Handle.Reference.Invoke("destroy") |> ignore
    member x.GetLimits(Limits : SupportedLimits) : bool = 
        let _MaxTextureDimension1D_int = uint32 (Limits.Limits.MaxTextureDimension1D)
        let _MaxTextureDimension2D_int = uint32 (Limits.Limits.MaxTextureDimension2D)
        let _MaxTextureDimension3D_int = uint32 (Limits.Limits.MaxTextureDimension3D)
        let _MaxTextureArrayLayers_int = uint32 (Limits.Limits.MaxTextureArrayLayers)
        let _MaxBindGroups_int = uint32 (Limits.Limits.MaxBindGroups)
        let _MaxDynamicUniformBuffersPerPipelineLayout_int = uint32 (Limits.Limits.MaxDynamicUniformBuffersPerPipelineLayout)
        let _MaxDynamicStorageBuffersPerPipelineLayout_int = uint32 (Limits.Limits.MaxDynamicStorageBuffersPerPipelineLayout)
        let _MaxSampledTexturesPerShaderStage_int = uint32 (Limits.Limits.MaxSampledTexturesPerShaderStage)
        let _MaxSamplersPerShaderStage_int = uint32 (Limits.Limits.MaxSamplersPerShaderStage)
        let _MaxStorageBuffersPerShaderStage_int = uint32 (Limits.Limits.MaxStorageBuffersPerShaderStage)
        let _MaxStorageTexturesPerShaderStage_int = uint32 (Limits.Limits.MaxStorageTexturesPerShaderStage)
        let _MaxUniformBuffersPerShaderStage_int = uint32 (Limits.Limits.MaxUniformBuffersPerShaderStage)
        let _MaxUniformBufferBindingSize_uint64 = float (Limits.Limits.MaxUniformBufferBindingSize)
        let _MaxStorageBufferBindingSize_uint64 = float (Limits.Limits.MaxStorageBufferBindingSize)
        let _MinUniformBufferOffsetAlignment_int = uint32 (Limits.Limits.MinUniformBufferOffsetAlignment)
        let _MinStorageBufferOffsetAlignment_int = uint32 (Limits.Limits.MinStorageBufferOffsetAlignment)
        let _MaxVertexBuffers_int = uint32 (Limits.Limits.MaxVertexBuffers)
        let _MaxVertexAttributes_int = uint32 (Limits.Limits.MaxVertexAttributes)
        let _MaxVertexBufferArrayStride_int = uint32 (Limits.Limits.MaxVertexBufferArrayStride)
        let _MaxInterStageShaderComponents_int = uint32 (Limits.Limits.MaxInterStageShaderComponents)
        let _MaxComputeWorkgroupStorageSize_int = uint32 (Limits.Limits.MaxComputeWorkgroupStorageSize)
        let _MaxComputeInvocationsPerWorkgroup_int = uint32 (Limits.Limits.MaxComputeInvocationsPerWorkgroup)
        let _MaxComputeWorkgroupSizeX_int = uint32 (Limits.Limits.MaxComputeWorkgroupSizeX)
        let _MaxComputeWorkgroupSizeY_int = uint32 (Limits.Limits.MaxComputeWorkgroupSizeY)
        let _MaxComputeWorkgroupSizeZ_int = uint32 (Limits.Limits.MaxComputeWorkgroupSizeZ)
        let _MaxComputeWorkgroupsPerDimension_int = uint32 (Limits.Limits.MaxComputeWorkgroupsPerDimension)
        let _Limits_Limits = new DawnRaw.WGPULimits()
        _Limits_Limits.MaxTextureDimension1D <- _MaxTextureDimension1D_int
        _Limits_Limits.MaxTextureDimension2D <- _MaxTextureDimension2D_int
        _Limits_Limits.MaxTextureDimension3D <- _MaxTextureDimension3D_int
        _Limits_Limits.MaxTextureArrayLayers <- _MaxTextureArrayLayers_int
        _Limits_Limits.MaxBindGroups <- _MaxBindGroups_int
        _Limits_Limits.MaxDynamicUniformBuffersPerPipelineLayout <- _MaxDynamicUniformBuffersPerPipelineLayout_int
        _Limits_Limits.MaxDynamicStorageBuffersPerPipelineLayout <- _MaxDynamicStorageBuffersPerPipelineLayout_int
        _Limits_Limits.MaxSampledTexturesPerShaderStage <- _MaxSampledTexturesPerShaderStage_int
        _Limits_Limits.MaxSamplersPerShaderStage <- _MaxSamplersPerShaderStage_int
        _Limits_Limits.MaxStorageBuffersPerShaderStage <- _MaxStorageBuffersPerShaderStage_int
        _Limits_Limits.MaxStorageTexturesPerShaderStage <- _MaxStorageTexturesPerShaderStage_int
        _Limits_Limits.MaxUniformBuffersPerShaderStage <- _MaxUniformBuffersPerShaderStage_int
        _Limits_Limits.MaxUniformBufferBindingSize <- _MaxUniformBufferBindingSize_uint64
        _Limits_Limits.MaxStorageBufferBindingSize <- _MaxStorageBufferBindingSize_uint64
        _Limits_Limits.MinUniformBufferOffsetAlignment <- _MinUniformBufferOffsetAlignment_int
        _Limits_Limits.MinStorageBufferOffsetAlignment <- _MinStorageBufferOffsetAlignment_int
        _Limits_Limits.MaxVertexBuffers <- _MaxVertexBuffers_int
        _Limits_Limits.MaxVertexAttributes <- _MaxVertexAttributes_int
        _Limits_Limits.MaxVertexBufferArrayStride <- _MaxVertexBufferArrayStride_int
        _Limits_Limits.MaxInterStageShaderComponents <- _MaxInterStageShaderComponents_int
        _Limits_Limits.MaxComputeWorkgroupStorageSize <- _MaxComputeWorkgroupStorageSize_int
        _Limits_Limits.MaxComputeInvocationsPerWorkgroup <- _MaxComputeInvocationsPerWorkgroup_int
        _Limits_Limits.MaxComputeWorkgroupSizeX <- _MaxComputeWorkgroupSizeX_int
        _Limits_Limits.MaxComputeWorkgroupSizeY <- _MaxComputeWorkgroupSizeY_int
        _Limits_Limits.MaxComputeWorkgroupSizeZ <- _MaxComputeWorkgroupSizeZ_int
        _Limits_Limits.MaxComputeWorkgroupsPerDimension <- _MaxComputeWorkgroupsPerDimension_int
        let _Limits_Limits = _Limits_Limits
        let _Limits_SupportedLimits = new DawnRaw.WGPUSupportedLimits()
        _Limits_SupportedLimits.Limits <- _Limits_Limits
        let _Limits_SupportedLimits = _Limits_SupportedLimits
        x.Handle.Reference.Invoke("getLimits", js _Limits_SupportedLimits) |> convert
    member x.GetQueue() : Queue = 
        let handle = x.Handle.Reference.GetObjectProperty("queue") |> convert<QueueHandle>
        new Queue(x, handle)
    member x.InjectError(Type : ErrorType, Message : string) : unit = 
        let _Type_ErrorType = Type.GetValue()
        let _Message_string = Message
        x.Handle.Reference.Invoke("injectError", js _Type_ErrorType, js _Message_string) |> ignore
    member x.LoseForTesting() : unit = 
        x.Handle.Reference.Invoke("loseForTesting") |> ignore
    member x.Tick() : unit = 
        x.Handle.Reference.Invoke("tick") |> ignore
    member x.SetUncapturedErrorCallback(Callback : ErrorCallback) : unit = 
        let _Callback_ErrorCallbackFunction (Type : obj) (Message : string) (Userdata : float) = 
            let _Type_ErrorType = Type |> System.Convert.ToInt32 |> unbox<ErrorType>
            let _Message_string = Message
            let _Userdata_nativeint = Userdata
            Callback.Invoke(ErrorType.Parse(_Type_ErrorType), _Message_string, nativeint _Userdata_nativeint)
        let _Callback_ErrorCallbackDel = WGPUErrorCallback(_Callback_ErrorCallbackFunction)
        let _Callback_ErrorCallbackGC = System.Runtime.InteropServices.GCHandle.Alloc(_Callback_ErrorCallbackDel)
        let _Callback_ErrorCallback = _Callback_ErrorCallbackDel
        x.Handle.Reference.Invoke("setUncapturedErrorCallback", js _Callback_ErrorCallback) |> ignore
    member x.SetUncapturedErrorCallback(Callback : ErrorCallback, Userdata : nativeint) : unit = 
        let _Callback_ErrorCallbackFunction (Type : obj) (Message : string) (Userdata : float) = 
            let _Type_ErrorType = Type |> System.Convert.ToInt32 |> unbox<ErrorType>
            let _Message_string = Message
            let _Userdata_nativeint = Userdata
            Callback.Invoke(ErrorType.Parse(_Type_ErrorType), _Message_string, nativeint _Userdata_nativeint)
        let _Callback_ErrorCallbackDel = WGPUErrorCallback(_Callback_ErrorCallbackFunction)
        let _Callback_ErrorCallbackGC = System.Runtime.InteropServices.GCHandle.Alloc(_Callback_ErrorCallbackDel)
        let _Callback_ErrorCallback = _Callback_ErrorCallbackDel
        let _Userdata_nativeint = float (Userdata)
        x.Handle.Reference.Invoke("setUncapturedErrorCallback", js _Callback_ErrorCallback, js _Userdata_nativeint) |> ignore
    member x.SetLoggingCallback(Callback : LoggingCallback) : unit = 
        let _Callback_LoggingCallbackFunction (Type : obj) (Message : string) (Userdata : float) = 
            let _Type_LoggingType = Type |> System.Convert.ToInt32 |> unbox<LoggingType>
            let _Message_string = Message
            let _Userdata_nativeint = Userdata
            Callback.Invoke(LoggingType.Parse(_Type_LoggingType), _Message_string, nativeint _Userdata_nativeint)
        let _Callback_LoggingCallbackDel = WGPULoggingCallback(_Callback_LoggingCallbackFunction)
        let _Callback_LoggingCallbackGC = System.Runtime.InteropServices.GCHandle.Alloc(_Callback_LoggingCallbackDel)
        let _Callback_LoggingCallback = _Callback_LoggingCallbackDel
        x.Handle.Reference.Invoke("setLoggingCallback", js _Callback_LoggingCallback) |> ignore
    member x.SetLoggingCallback(Callback : LoggingCallback, Userdata : nativeint) : unit = 
        let _Callback_LoggingCallbackFunction (Type : obj) (Message : string) (Userdata : float) = 
            let _Type_LoggingType = Type |> System.Convert.ToInt32 |> unbox<LoggingType>
            let _Message_string = Message
            let _Userdata_nativeint = Userdata
            Callback.Invoke(LoggingType.Parse(_Type_LoggingType), _Message_string, nativeint _Userdata_nativeint)
        let _Callback_LoggingCallbackDel = WGPULoggingCallback(_Callback_LoggingCallbackFunction)
        let _Callback_LoggingCallbackGC = System.Runtime.InteropServices.GCHandle.Alloc(_Callback_LoggingCallbackDel)
        let _Callback_LoggingCallback = _Callback_LoggingCallbackDel
        let _Userdata_nativeint = float (Userdata)
        x.Handle.Reference.Invoke("setLoggingCallback", js _Callback_LoggingCallback, js _Userdata_nativeint) |> ignore
    member x.SetDeviceLostCallback(Callback : DeviceLostCallback) : unit = 
        let _Callback_DeviceLostCallbackFunction (Reason : obj) (Message : string) (Userdata : float) = 
            let _Reason_DeviceLostReason = Reason |> System.Convert.ToInt32 |> unbox<DeviceLostReason>
            let _Message_string = Message
            let _Userdata_nativeint = Userdata
            Callback.Invoke(DeviceLostReason.Parse(_Reason_DeviceLostReason), _Message_string, nativeint _Userdata_nativeint)
        let _Callback_DeviceLostCallbackDel = WGPUDeviceLostCallback(_Callback_DeviceLostCallbackFunction)
        let _Callback_DeviceLostCallbackGC = System.Runtime.InteropServices.GCHandle.Alloc(_Callback_DeviceLostCallbackDel)
        let _Callback_DeviceLostCallback = _Callback_DeviceLostCallbackDel
        x.Handle.Reference.Invoke("setDeviceLostCallback", js _Callback_DeviceLostCallback) |> ignore
    member x.SetDeviceLostCallback(Callback : DeviceLostCallback, Userdata : nativeint) : unit = 
        let _Callback_DeviceLostCallbackFunction (Reason : obj) (Message : string) (Userdata : float) = 
            let _Reason_DeviceLostReason = Reason |> System.Convert.ToInt32 |> unbox<DeviceLostReason>
            let _Message_string = Message
            let _Userdata_nativeint = Userdata
            Callback.Invoke(DeviceLostReason.Parse(_Reason_DeviceLostReason), _Message_string, nativeint _Userdata_nativeint)
        let _Callback_DeviceLostCallbackDel = WGPUDeviceLostCallback(_Callback_DeviceLostCallbackFunction)
        let _Callback_DeviceLostCallbackGC = System.Runtime.InteropServices.GCHandle.Alloc(_Callback_DeviceLostCallbackDel)
        let _Callback_DeviceLostCallback = _Callback_DeviceLostCallbackDel
        let _Userdata_nativeint = float (Userdata)
        x.Handle.Reference.Invoke("setDeviceLostCallback", js _Callback_DeviceLostCallback, js _Userdata_nativeint) |> ignore
    member x.PushErrorScope(Filter : ErrorFilter) : unit = 
        let _Filter_ErrorFilter = Filter.GetValue()
        x.Handle.Reference.Invoke("pushErrorScope", js _Filter_ErrorFilter) |> ignore
    member x.PopErrorScope(Callback : ErrorCallback) : bool = 
        let mutable _Callback_ErrorCallbackGC = Unchecked.defaultof<System.Runtime.InteropServices.GCHandle>
        let _Callback_ErrorCallbackFunction (Type : obj) (Message : string) (Userdata : float) = 
            let _Type_ErrorType = Type |> System.Convert.ToInt32 |> unbox<ErrorType>
            let _Message_string = Message
            let _Userdata_nativeint = Userdata
            if _Callback_ErrorCallbackGC.IsAllocated then _Callback_ErrorCallbackGC.Free()
            Callback.Invoke(ErrorType.Parse(_Type_ErrorType), _Message_string, nativeint _Userdata_nativeint)
        let _Callback_ErrorCallbackDel = WGPUErrorCallback(_Callback_ErrorCallbackFunction)
        _Callback_ErrorCallbackGC <- System.Runtime.InteropServices.GCHandle.Alloc(_Callback_ErrorCallbackDel)
        let _Callback_ErrorCallback = _Callback_ErrorCallbackDel
        x.Handle.Reference.Invoke("popErrorScope", js _Callback_ErrorCallback) |> convert
    member x.PopErrorScope(Callback : ErrorCallback, Userdata : nativeint) : bool = 
        let mutable _Callback_ErrorCallbackGC = Unchecked.defaultof<System.Runtime.InteropServices.GCHandle>
        let _Callback_ErrorCallbackFunction (Type : obj) (Message : string) (Userdata : float) = 
            let _Type_ErrorType = Type |> System.Convert.ToInt32 |> unbox<ErrorType>
            let _Message_string = Message
            let _Userdata_nativeint = Userdata
            if _Callback_ErrorCallbackGC.IsAllocated then _Callback_ErrorCallbackGC.Free()
            Callback.Invoke(ErrorType.Parse(_Type_ErrorType), _Message_string, nativeint _Userdata_nativeint)
        let _Callback_ErrorCallbackDel = WGPUErrorCallback(_Callback_ErrorCallbackFunction)
        _Callback_ErrorCallbackGC <- System.Runtime.InteropServices.GCHandle.Alloc(_Callback_ErrorCallbackDel)
        let _Callback_ErrorCallback = _Callback_ErrorCallbackDel
        let _Userdata_nativeint = float (Userdata)
        x.Handle.Reference.Invoke("popErrorScope", js _Callback_ErrorCallback, js _Userdata_nativeint) |> convert
