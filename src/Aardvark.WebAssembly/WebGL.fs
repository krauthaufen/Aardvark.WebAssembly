namespace rec Aardvark.WebAssembly

open System
open WebAssembly
open WebAssembly.Core
open Aardvark.Base
open System.Runtime.CompilerServices

type ShaderType =
    | Vertex = 0x8B31
    | Fragment = 0x8B30

[<Flags>]
type ClearBuffers =
    | Color             = 0x4000
    | Stencil           = 0x0400
    | Depth             = 0x0100
    | DepthStencil      = 0x84F9

type BufferTarget =
    | Array = 0x8892
    | Element = 0x8893
    | CopyRead = 0x8F36
    | CopyWrite = 0x8F37
    | TransformFeedback = 0x8C8E
    | Uniform = 0x8A11
    | PixelPack = 0x88EB
    | PixelUnpack = 0x88EC

type BufferParameterName =
    | Size    = 0x8764
    | Usage   = 0x8765

type BufferUsage =
    | StaticDraw = 0x88E4
    | StreamDraw = 0x88E0
    | DynamicDraw = 0x88E8
    
    | StaticRead = 0x88E5
    | StreamRead = 0x88E1
    | DynamicRead = 0x88E9
    
    | StaticCopy = 0x88E6
    | StreamCopy = 0x88E2
    | DynamicCopy = 0x88EA

type VertexAttribType =
    | Byte = 0x1400
    | UnsignedByte = 0x1401
    | Short = 0x1402
    | UnsignedShort = 0x1403
    | Int = 0x1404
    | UnsignedInt = 0x1405
    | Float = 0x1406
    
type PrimitiveTopology =
    | Points = 0x0000
    | Lines = 0x0001
    | LineLoop = 0x0002
    | LineStrip = 0x0003
    | Triangles = 0x0004
    | TriangleStrip = 0x0005
    | TriangleFan = 0x0006

type FramebufferTarget =
    | Framebuffer = 0x8D40
    | Read = 0x8CA8
    | Draw = 0x8CA9

type TextureTarget =
    | Texture2d = 0x0DE1
    | Texture3d = 0x806F
    | Texture2dArray = 0x8C1A
    | TextureCube = 0x8513

    | TextureCubePositiveX = 0x8515
    | TextureCubeNegativeX = 0x8516
    | TextureCubePositiveY = 0x8517
    | TextureCubeNegativeY = 0x8518
    | TextureCubePositiveZ = 0x8519
    | TextureCubeNegativeZ = 0x851A
     
type BlendEquation =
    | Add               = 0x8006
    | Subtract          = 0x800A
    | ReverseSubtract   = 0x800B
    //| Min
    //| Max

type BlendFactor =
    | Zero = 0
    | One = 1
    | SrcColor = 0x0300
    | OneMinusSrcColor = 0x0301
    | DstColor = 0x0306
    | OneMinusDstColor = 0x0307
    | SrcAlpha = 0x0302
    | OneMinusSrcAlpha = 0x0303
    | DstAlpha = 0x0304
    | OneMinusDstAlpha = 0x0305
    | ConstantColor = 0x8001
    | OneMinusConstantColor = 0x8002
    | ConstantAlpha = 0x8003
    | OneMinusConstantAlpha = 0x8004
    | SrcAlphaSaturate = 0x0308

type CompressedFormat =
    | RgbDxt1 = 0x83F0
    | RgbaDxt1 = 0x83F1
    | RgbaDxt3 = 0x83F2
    | RgbaDxt5 = 0x83F3
    | SRgbDxt1 = 0x8C4C
    | SRgbaDxt1 = 0x8C4D
    | SRgbaDxt3 = 0x8C4E
    | SRgbaDxt5 = 0x8C4F
    | R11Eac = 0x9270
    | SignedR11Eac = 0x9271
    | Rg11Eac = 0x9272
    | SignedRg11Eac = 0x9273
    | Rgb8Etc2 = 0x9274
    | SRgb8Etc2 = 0x9275
    | Rgba8Etc2 = 0x9278
    | SRgba8Etc2 = 0x9279
    | Rgb8A1Etc2 = 0x9276
    | SRgb8A1Etc2 = 0x9277
    | RgbPvrtc4 = 0x8C00
    | RgbaPvrtc4 = 0x8C02
    | RgbPvrtc2 = 0x8C01
    | RgbaPvrtc2 = 0x8C03
    | RgbEtc1 = 0x8D64

    
type InternalFormat =
    | A = 0 // TODO
     
type PixelFormat =
    | Alpha = 0x1906
    | Rgb = 0x1907
    | Rgba = 0x1908
    | Luminance = 0x1909
    | LuminanceAlpha = 0x190A

type PixelType =
    | Byte = 0x1400
    | UnsignedByte = 0x1401
    | Short = 0x1402
    | UnsignedShort = 0x1403
    | Int = 0x1404
    | UnsignedInt = 0x1405
    | Float = 0x1406

type CullFaceMode =
    | Front = 0x0404
    | Back = 0x0405
    | FrontAndBack = 0x0408

type DepthFunc =
    | Never     = 0x0200
    | Less      = 0x0201
    | Equal     = 0x0202
    | Lequal    = 0x0203
    | Greater   = 0x0204
    | NotEqual  = 0x0205
    | Gequal    = 0x0206
    | Always    = 0x0207
    
type StencilFunc =
    | Never     = 0x0200
    | Less      = 0x0201
    | Equal     = 0x0202
    | Lequal    = 0x0203
    | Greater   = 0x0204
    | NotEqual  = 0x0205
    | Gequal    = 0x0206
    | Always    = 0x0207

type EnableCap =
    | Blend                     = 0x0BE2
    | CullFace                  = 0x0B44
    | DepthTest                 = 0x0B71
    | Dither                    = 0x0BD0
    | PolygonOffsetFill         = 0x8037
    | SampleAlphaToCoverage     = 0x809E
    | ScissorTest               = 0x0C11
    | StencilTest               = 0x0B90
    | RasterizerDiscard         = 0x8C89

type IndexType =
    | Byte      = 0x1401
    | Short     = 0x1403
    | Int       = 0x1405

type FramebufferAttachment =
    | Color0 = 0x8CE0
    | Color1 = 0x8CE1
    | Color2 = 0x8CE2
    | Color3 = 0x8CE3
    | Color4 = 0x8CE4
    | Color5 = 0x8CE5
    | Color6 = 0x8CE6
    | Color7 = 0x8CE7
    | Color8 = 0x8CE8
    | Color9 = 0x8CE9
    | Color10 = 0x8CEA
    | Color11 = 0x8CEB
    | Color12 = 0x8CEC
    | Color13 = 0x8CED
    | Color14 = 0x8CEE
    | Color15 = 0x8CEF
    | Depth = 0x8D00
    | Stencil = 0x8D20
    | DepthStencil = 0x821A

type PrimitiveType =
    | Byte = 0x1400
    | UnsignedByte = 0x1401
    | Short = 0x1402
    | UnsignedShort = 0x1403
    | Int = 0x1404
    | UnsignedInt = 0x1405
    | Float = 0x1406
    | Fixed = 0x140C

[<AllowNullLiteral>]
type WebGLProgram(ref : JSObject) =
    inherit JsObj(ref)
    new(o : JsObj) = WebGLProgram(o.Reference)
    
[<AllowNullLiteral>]
type WebGLUniformLocation(ref : JSObject) =
    inherit JsObj(ref)
    new(o : JsObj) = WebGLUniformLocation(o.Reference)

[<AllowNullLiteral>]
type WebGLShader(ref : JSObject) =
    inherit JsObj(ref)
    new(o : JsObj) = WebGLShader(o.Reference)

[<AllowNullLiteral>]
type WebGLBuffer(ref : JSObject) =
    inherit JsObj(ref)
    new(o : JsObj) = WebGLBuffer(o.Reference)

[<AllowNullLiteral>]
type WebGLFramebuffer(ref : JSObject) =
    inherit JsObj(ref)
    new(o : JsObj) = WebGLFramebuffer(o.Reference)

[<AllowNullLiteral>]
type WebGLRenderbuffer(ref : JSObject) =
    inherit JsObj(ref)
    new(o : JsObj) = WebGLRenderbuffer(o.Reference)
    
[<AllowNullLiteral>]
type WebGLTexture(ref : JSObject) =
    inherit JsObj(ref)
    new(o : JsObj) = WebGLTexture(o.Reference)

[<AllowNullLiteral>]
type WebGLActiveInfo(ref : JSObject) =
    inherit JsObj(ref)

    /// The read-only WebGLActiveInfo.name property represents the name of the requested data returned by calling the getActiveAttrib() or getActiveUniform() methods.
    member x.Name = ref.GetObjectProperty("name") |> convert<string>

    /// The read-only WebGLActiveInfo.size property is a Number representing the size of the requested data returned by calling the getActiveAttrib() or getActiveUniform() methods.
    member x.Size = ref.GetObjectProperty("size") |> convert<int>

    /// The read-only WebGLActiveInfo.type property represents the type of the requested data returned by calling the getActiveAttrib() or getActiveUniform() methods.
    member x.Type = ref.GetObjectProperty("type") |> convert<int> |> unbox<PrimitiveType>


    new(o : JsObj) = WebGLActiveInfo(o.Reference)


[<RequireQualifiedAccess>]
type WebGLPowerPreference =
    | Default
    | Low
    | HighPerformance

type WebGLError =
    | NoError = 0
    | InvalidEnum = 0x0500
    | InvalidValue = 0x0501
    | InvalidOperation = 0x0502
    | OutOfMemory = 0x0505

type FramebufferAttachmentParameter =
    | ObjectType = 0x8CD0
    | ObjectName = 0x8CD1
    | TextureLevel = 0x8CD2
    | CubeMapFace = 0x8CD3
    | ColorEncoding = 0x8210
    | RedSize = 0x8212
    | GreenSize = 0x8213
    | BlueSize = 0x8214
    | AlphaSize = 0x8215
    | DepthSize = 0x8216
    | StencilSize = 0x8217
    | TextureLayer = 0x8CD4
    | TextureNumViews = 0x9630
    | TextureBaseView = 0x9632


[<Struct>]
type ParameterName<'T>(value : int, get : obj -> 'T) =
    member x.EnumValue = value
    member x.Getter = get

module ParameterName =
    let private array<'T> (v : obj) =
        let o = convert<JsObj> v
        let len = convert<int> o.["length"]
        Array.init len (fun i -> convert<'T> o.[string i])

    let ActiveTexture = ParameterName(0x84E0, fun v -> convert<int> v - 0x84C0)
    let AliasedLineWidthRange = ParameterName(0x846E, array<float32> >> Range1f)
    let AliasedPointSizeRange = ParameterName(0x846D, array<float32> >> Range1f)
    let AlphaBits = ParameterName(0x846D, convert<int>)
    let RedBits = ParameterName(0x0D52, convert<int>)
    let GreenBits = ParameterName(0x0D53, convert<int>)
    let BlueBits = ParameterName(0x0D54, convert<int>)
    let DepthBits = ParameterName(0x0D56, convert<int>)
    let StencilBits = ParameterName(0x0D57, convert<int>)
    let ArrayBufferBinding = ParameterName(0x8894, convert<WebGLBuffer>)
    let ElementBufferBinding = ParameterName(0x8895, convert<WebGLBuffer>)
    let FramebufferBinding = ParameterName(0x8CA6, convert<WebGLFramebuffer>)
    let Blend = ParameterName(0x0BE2, convert<bool>)
    let BlendColor = ParameterName(0x8005, array<float32> >> V4f >> C4f)
    let BlendDstAlpha = ParameterName(0x80CA, convert<int> >> unbox<BlendFactor>)
    let BlendDstRgb = ParameterName(0x80C8, convert<int> >> unbox<BlendFactor>)
    let BlendSrcAlpha = ParameterName(0x80CB, convert<int> >> unbox<BlendFactor>)
    let BlendSrcRgb = ParameterName(0x80C9, convert<int> >> unbox<BlendFactor>)
    let BlendEquation = ParameterName(0x8009, convert<int> >> unbox<BlendEquation>)
    let BlendEquationRgb = ParameterName(0x8009, convert<int> >> unbox<BlendEquation>)
    let BlendEquationAlpha = ParameterName(0x883D, convert<int> >> unbox<BlendEquation>)
    let ClearColor = ParameterName(0x0C22, array<float32> >> V4f >> C4f)
    let ClearDepth = ParameterName(0x0B73, convert<float32>)
    let ClearStencil = ParameterName(0x0B91, convert<int>)
    let ColorWriteMask = ParameterName(0x0C23, array<bool>)
    let CompressedTextureFormats = ParameterName(0x86A3, array<int> >> Array.map unbox<CompressedFormat>)
    let CullFace = ParameterName(0x0B44, convert<bool>)
    let CullFaceMode = ParameterName(0x0B45, convert<int> >> unbox<CullFaceMode>)
    let CurrentProgram = ParameterName(0x8B8D, convert<WebGLProgram>)
    let DepthFunc = ParameterName(0x0B74, convert<int> >> unbox<DepthFunc>)
    let DepthRange = ParameterName(0x0B70, array<float32> >> Range1f)
    let DepthTest = ParameterName(0x0B71, convert<bool>)
    let DepthWriteMask = ParameterName(0x0B72, convert<bool>)
    let Dither = ParameterName(0x0BD0, convert<bool>)
    let FrontFace = ParameterName(0x0B46, convert<int> >> (function 0x0900 -> Winding.CW | _ -> Winding.CCW))
    let ImplementationColorReadFormat = ParameterName(0x8B9B, convert<int> >> unbox<PixelFormat>)
    let ImplementationColorReadType = ParameterName(0x8B9A, convert<int> >> unbox<PixelType>)
    let LineWidth = ParameterName(0x0B21, convert<float32>)
    let CombinedTextureImageUnits = ParameterName(0x8B4D, convert<int>)

    let MaxCubeMapTextureSize = ParameterName(0x851C, convert<int>)
    let MaxFragmentUniformVectors = ParameterName(0x8DFD, convert<int>)
    let MaxRenderbufferSize = ParameterName(0x84E8, convert<int>)
    let MaxTextureImageUnits = ParameterName(0x8872, convert<int>)
    let MaxTextureSize = ParameterName(0x0D33, convert<int>)
    let MaxVaryingVectors = ParameterName(0x8DFC, convert<int>)
    let MaxVertexAttributes = ParameterName(0x8869, convert<int>)
    let MaxVertexTextureImageUnits = ParameterName(0x8B4C, convert<int>)
    let MaxVertexUniformVectors = ParameterName(0x8DFB, convert<int>)
    let MaxViewportDim = ParameterName(0x0D3A, array<int> >> V2i)
    let PackAlignment = ParameterName(0x0D05, convert<int>)
    let UnpackAlignment = ParameterName(0x0CF5, convert<int>)
    let PolygonOffsetFactor = ParameterName(0x8038, convert<float32>)
    let PolygonOffsetFill = ParameterName(0x8037, convert<bool>)
    let PolygonOffsetUnits = ParameterName(0x2A00, convert<float32>)
    let RenderbufferBinding = ParameterName(0x8CA7, convert<WebGLRenderbuffer>)
    let Renderer = ParameterName(0x1F01, convert<string>)
    let SampleBuffers = ParameterName(0x80A8, convert<int>)
    let SampleCoverageInvert = ParameterName(0x80AB, convert<bool>)
    let SampleCoverageValue = ParameterName(0x80AA, convert<float32>)
    let Samples = ParameterName(0x80A9, convert<int>)
    let ScissorBox = ParameterName(0x0C10, array<int> >> (fun v -> Box2i.FromMinAndSize(v.[0], v.[1], v.[2], v.[3])))
    let ScissorTest = ParameterName(0x0C11, convert<bool>)
    let ShadingLanguageVersion = ParameterName(0x8B8C, convert<string>)
    let StencilBackFail = ParameterName(0x8801, convert<int>) // TODO
    let StencilBackFunc = ParameterName(0x8800, convert<int>) // TODO
    let StencilBackDepthFail = ParameterName(0x8802, convert<int>) // TODO
    let StencilBackDepthPass = ParameterName(0x8803, convert<int>) // TODO
    let StencilBackRef = ParameterName(0x8CA3, convert<int>) // TODO
    let StencilBackValueMask = ParameterName(0x8CA4, convert<int>) // TODO
    let StencilBackWriteMask = ParameterName(0x8CA5, convert<int>) // TODO
    let StencilFunc = ParameterName(0x0B92, convert<int>) // TODO
    let StencilFail = ParameterName(0x0B94, convert<int>) // TODO
    let StencilDepthFail = ParameterName(0x0B95, convert<int>) // TODO
    let StencilDepthPass = ParameterName(0x0B96, convert<int>) // TODO
    let StencilRef = ParameterName(0x0B97, convert<int>) // TODO
    let StencilValueMask = ParameterName(0x0B93, convert<int>) // TODO
    let StencilWriteMask = ParameterName(0x0B98, convert<int>) // TODO
    let StencilTest = ParameterName(0x0B90, convert<bool>)
    let SubPixelBits = ParameterName(0x0D50, convert<int>)
    let TextureBinding2D = ParameterName(0x8069, convert<WebGLTexture>)
    let TextureBindingCubeMap = ParameterName(0x8514, convert<WebGLTexture>)
    let UnpackColorSpaceConversion = ParameterName(0x9243, convert<int>)
    let UnpackFlipY = ParameterName(0x9240, convert<bool>)
    let UnpackPremultiplyAlpha = ParameterName(0x9241, convert<bool>)
    let Vendor = ParameterName(0x1F00, convert<string>)
    let Version = ParameterName(0x1F02, convert<string>)
    let Viewport = ParameterName(0x0BA2, array<int> >> (fun v -> Box2i.FromMinAndSize(v.[0], v.[1], v.[2], v.[3])))

    let CopyReadBufferBinding = ParameterName(0x8F36, convert<WebGLBuffer>)
    let CopyWriteBufferBinding = ParameterName(0x8F37, convert<WebGLBuffer>)
    let DrawBuffer (i : int)  = ParameterName(0x8825 + i, convert<int> >> unbox<FramebufferAttachment>)
    let DrawFramebufferBinding = ParameterName(0x8CA6, convert<WebGLFramebuffer>)
    let MaxTexture3DSize = ParameterName(0x8073, convert<int>)
    let MaxArrayTextureLayers = ParameterName(0x88FF, convert<int>)
    let MaxClientWaitTimeout = ParameterName(0x9247, convert<int64>)
    let MaxColorAttachments = ParameterName(0x8CDF, convert<int>)
    let MaxCombinedFragmentUniformComponents = ParameterName(0x8A33, convert<int>)
    let MaxCombinedUniformBlocks = ParameterName(0x8A2E, convert<int>)
    let MaxCombinedVertexUniformComponents = ParameterName(0x8A31, convert<int>)
    let MaxDrawBuffers = ParameterName(0x8824, convert<int>)
    let MaxElementIndex = ParameterName(0x8D6B, convert<int64>)
    let MaxElementIndices = ParameterName(0x80E9, convert<int>)
    let MaxElementVertices = ParameterName(0x80E8, convert<int>)
    let MaxFragmentInputComponents = ParameterName(0x9125, convert<int>)
    let MaxFragmentUniformBlocks = ParameterName(0x8A2D, convert<int>)
    let MaxFragmentUniformComponents = ParameterName(0x8B49, convert<int>)
    let MaxProgramTexelOffset = ParameterName(0x8905, convert<int>)
    let MaxSamples = ParameterName(0x8D57, convert<int>)
    let MaxServerWaitTimeout = ParameterName(0x9111, convert<int>)
    let MaxTextureLodBias = ParameterName(0x84FD, convert<float32>)
    let MaxTransformFeedbackInterleavedComponents = ParameterName(0x8C8A, convert<int>)
    let MaxTransformFeedbackSeparateAttributes = ParameterName(0x8C8B, convert<int>)
    let MaxTransformFeedbackSeparateComponents = ParameterName(0x8C80, convert<int>)
    let MaxUniformBlockSize = ParameterName(0x8A30, convert<int64>)
    let MaxUniformBufferBindings = ParameterName(0x8A2F, convert<int>)
    let MaxVaryingComponents = ParameterName(0x8B4B, convert<int>)
    let MaxVertexOutputComponents = ParameterName(0x9122, convert<int>)
    let MaxVertexUniformBlocks = ParameterName(0x8A2B, convert<int>)
    let MaxVertexUniformComponents = ParameterName(0x8B4A, convert<int>)
    let MinProgramTexelOffset = ParameterName(0x8904, convert<int>)
    let PackRowLength = ParameterName(0x0D02, convert<int>)
    let PackSkipPixels = ParameterName(0x0D04, convert<int>)
    let PackSkipRows = ParameterName(0x0D03, convert<int>)
    let PixelPackBufferBinding = ParameterName(0x88ED, convert<WebGLBuffer>)
    let PixelunpackBufferBinding = ParameterName(0x88EF, convert<WebGLBuffer>)
    let RasterizerDiscard = ParameterName(0x8C89, convert<bool>)
    let ReadBuffer = ParameterName(0x0C02, convert<int>) // TODO
    let ReadFramebufferBinding = ParameterName(0x8CAA, convert<WebGLFramebuffer>)
    let SampleAlphaToCoverage = ParameterName(0x809E, convert<bool>)
    let SampleCoverage = ParameterName(0x80A0, convert<bool>)
    let SamplerBinding = ParameterName(0x8919, convert<JsObj>) // TODO
    let TextureBinding2DArray = ParameterName(0x8C1D, convert<WebGLTexture>)
    let TextureBinding3D = ParameterName(0x806A, convert<WebGLTexture>)
    let TransformFeedbackActive = ParameterName(0x8E24, convert<bool>)
    let TransformFeedbackBinding = ParameterName(0x8E25, convert<JsObj>) // TODO
    let TransformFeedbackBufferBinding = ParameterName(0x8C8F, convert<WebGLBuffer>)
    let TransformFeedbackPaused = ParameterName(0x8E23, convert<bool>)
    let UniformBufferBinding = ParameterName(0x8A28, convert<WebGLBuffer>)
    let UniformBufferOffsetAlignment = ParameterName(0x8A34, convert<int>)
    let UnpackImageHeight = ParameterName(0x806E, convert<int>)
    let UnpackRowLength = ParameterName(0x0CF2, convert<int>)
    let UnpackSkipImages = ParameterName(0x806D, convert<int>)
    let UnpackSkipPixels = ParameterName(0x0CF4, convert<int>)
    let UnpackSkipRows = ParameterName(0x0CF3, convert<int>)
    let VertexArrayBinding = ParameterName(0x85B5, convert<JsObj>) // TODO


[<Struct>]
type ProgramParameter<'T>(value : int, get : obj -> 'T) =
    member x.EnumValue = value
    member x.Getter = get

module ProgramParameter =
    let DeleteStatus = ProgramParameter(0x8B80, convert<bool>)
    let LinkStatus = ProgramParameter(0x8B82, convert<bool>)
    let ValidateStatus = ProgramParameter(0x8B83, convert<bool>)
    let AttachedShaders = ProgramParameter(0x8B85, convert<int>)
    let ActiveAttributes = ProgramParameter(0x8B89, convert<int>)
    let ActiveUniforms = ProgramParameter(0x8B86, convert<int>)
    let TransformFeedbackBufferMode = ProgramParameter(0x8C7F, convert<int>) // TODO
    let TransformFeedbackVaryings = ProgramParameter(0x8C83, convert<int>)
    let ActiveUniformBlocks = ProgramParameter(0x8A36, convert<int>)

    

[<Struct>]
type RenderbufferParameter<'T>(value : int, get : obj -> 'T) =
    member x.EnumValue = value
    member x.Getter = get

module RenderbufferParameter =
    let Width = RenderbufferParameter(0x8D42, convert<int>)
    let Height = RenderbufferParameter(0x8D43, convert<int>)
    let InternalFormat = RenderbufferParameter(0x8D44, convert<InternalFormat>)
    let RedSize = RenderbufferParameter(0x8D50, convert<int>)
    let GreenSize = RenderbufferParameter(0x8D51, convert<int>)
    let BlueSize = RenderbufferParameter(0x8D52, convert<int>)
    let AlphaSize = RenderbufferParameter(0x8D53, convert<int>)
    let DepthSize = RenderbufferParameter(0x8D54, convert<int>)
    let StencilSize = RenderbufferParameter(0x8D55, convert<int>)

[<Struct>]
type ShaderParameter<'T>(value : int, get : obj -> 'T) =
    member x.EnumValue = value
    member x.Getter = get
    
module ShaderParameter = 
    let DeleteStatus = ShaderParameter(0x8B80, convert<bool>)
    let CompileStatus = ProgramParameter(0x8B81, convert<bool>)
    let ShaderType = ProgramParameter(0x8B4F, convert<int> >> unbox<ShaderType>)

type ShaderPrecision =
    | LowFloat      = 0x8DF0
    | MediumFloat   = 0x8DF1
    | HighFloat     = 0x8DF2
    | LowInt        = 0x8DF3
    | MediumInt     = 0x8DF4
    | HighInt       = 0x8DF5

[<Struct>]
type TextureParameter<'T>(value : int, get : obj -> 'T) =
    member x.EnumValue = value
    member x.Getter = get
    

module TextureParameter = 
    let MagFilter = TextureParameter(0x2800, convert<int>) // TODO
    let MinFilter = TextureParameter(0x2801, convert<int>) // TODO
    let WrapS = TextureParameter(0x2802, convert<int>) // TODO
    let WrapT = TextureParameter(0x2803, convert<int>) // TODO
    let WrapR = TextureParameter(0x8072, convert<int>) // TODO
    let MaxAnisotropy = TextureParameter(0x84FE, convert<float32>)
    let BaseLevel = TextureParameter(0x813C, convert<int>)
    let CompareFunc = TextureParameter(0x884D, convert<int>) // TODO
    let CompareMode = TextureParameter(0x884C, convert<int>) // TODO
    let ImmutableFormat = TextureParameter(0x912F, convert<bool>)
    let ImmutableLevels = TextureParameter(0x82DF, convert<int>)
    let MaxLevel = TextureParameter(0x813D, convert<int>)
    let MaxLod = TextureParameter(0x813B, convert<float32>)
    let MinLod = TextureParameter(0x813A, convert<float32>)

[<Struct>]
type VertexAttributeParameter<'T>(value : int, get : obj -> 'T) =
    member x.EnumValue = value
    member x.Getter = get
    

module VertexAttributeParameter =
    let private array<'T> (v : obj) =
        let o = convert<JsObj> v
        let len = convert<int> o.["length"]
        Array.init len (fun i -> convert<'T> o.[string i])

    let ArrayBufferBinding = VertexAttributeParameter(0x889F, convert<WebGLBuffer>)
    let Enabled = VertexAttributeParameter(0x8622, convert<bool>)
    let Size = VertexAttributeParameter(0x8623, convert<int>)
    let Stride = VertexAttributeParameter(0x8624, convert<int>)
    let Type = VertexAttributeParameter(0x8625, convert<int> >> unbox<PrimitiveType>)
    let Normalized = VertexAttributeParameter(0x886A, convert<bool>)
    let Current = VertexAttributeParameter(0x8626, array<float32>)
    let CurrentInteger = VertexAttributeParameter(0x88FD, array<int>)
    let Divisor = VertexAttributeParameter(0x88FE, convert<int>)

type PixelStoreParameter =
    | PackAlignment                 = 0x0D05
    | UnpackAlignment               = 0x0CF5
    | UnpackFlipY                   = 0x9240
    | UnpackPremultiplyAlpha        = 0x9241
    | UnpackColorSpaceConversion    = 0x9243
    | PackRowLength                 = 0x0D02
    | PackSkipPixels                = 0x0D04
    | PackSkipRows                  = 0x0D03
    | UnpackRowLength               = 0x0CF2
    | UnpackSkipPixels              = 0x0CF4
    | UnpackSkipRows                = 0x0D03
    | UnpackImageHeight             = 0x806E

type StencilOperation =
    | Keep              = 0x1E00
    | Zero              = 0
    | Replace           = 0x1E01
    | Increment         = 0x1E02
    | IncrementWrap     = 0x8507
    | Decrement         = 0x1E03
    | DecrementWrap     = 0x8508
    | Invert            = 0x150A

[<AllowNullLiteral>]
type WebGLContextAttributes(r : JSObject) =
    inherit JsObj(r)

    /// If the value is true and the implementation supports antialiasing the drawing buffer will perform antialiasing using its choice of technique (multisample/supersample) and quality. If the value is false or the implementation does not support antialiasing, no antialiasing is performed.
    member x.Antialias
        with get() = r.GetObjectProperty("antialias") |> convert<bool>
        and set (v : bool) = r.SetObjectProperty("antialias", v)

    /// If the value is true, the drawing buffer has an alpha channel for the purposes of performing OpenGL destination alpha operations and compositing with the page. If the value is false, no alpha buffer is available.
    member x.Alpha
        with get() = r.GetObjectProperty("alpha") |> convert<bool>
        and set (v : bool) = r.SetObjectProperty("alpha", v)

    /// If the value is true, the drawing buffer has a depth buffer of at least 16 bits. If the value is false, no depth buffer is available.
    member x.Depth
        with get() = r.GetObjectProperty("depth") |> convert<bool>
        and set (v : bool) = r.SetObjectProperty("depth", v)
        
    /// If the value is true, the drawing buffer has a stencil buffer of at least 8 bits. If the value is false, no stencil buffer is available.
    member x.Stencil
        with get() = r.GetObjectProperty("stencil") |> convert<bool>
        and set (v : bool) = r.SetObjectProperty("stencil", v)
        
    /// If the value is true, then the user agent may optimize the rendering of the canvas to reduce the latency, as measured from input events to rasterization, by desynchronizing the canvas paint cycle from the event loop, bypassing the ordinary user agent rendering algorithm, or both. Insofar as this mode involves bypassing the usual paint mechanisms, rasterization, or both, it might introduce visible tearing artifacts.
    member x.Desynchronized
        with get() = r.GetObjectProperty("desynchronized") |> convert<bool>
        and set (v : bool) = r.SetObjectProperty("desynchronized", v)
        
    /// If false, once the drawing buffer is presented as described in theDrawing Buffer section, the contents of the drawing buffer are cleared to their default values. All elements of the drawing buffer (color, depth and stencil) are cleared. If the value is true the buffers will not be cleared and will preserve their values until cleared or overwritten by the author.
    member x.PreserveDrawingBuffer
        with get() = r.GetObjectProperty("preserveDrawingBuffer") |> convert<bool>
        and set (v : bool) = r.SetObjectProperty("preserveDrawingBuffer", v)
        
    /// If the value is true, context creation will fail if the implementation determines that the performance of the created WebGL context would be dramatically lower than that of a native application making equivalent OpenGL calls. This could happen for a number of reasons, including:
    member x.FailIfMajorPerformanceCaveat
        with get() = r.GetObjectProperty("failIfMajorPerformanceCaveat") |> convert<bool>
        and set (v : bool) = r.SetObjectProperty("failIfMajorPerformanceCaveat", v)
    
    /// If the value is true the page compositor will assume the drawing buffer contains colors with premultiplied alpha. If the value is false the page compositor will assume that colors in the drawing buffer are not premultiplied. This flag is ignored if the alpha flag is false. See Premultiplied Alpha for more information on the effects of the premultipliedAlpha flag.
    member x.PremultipliedAlpha
        with get() = r.GetObjectProperty("premultipliedAlpha") |> convert<bool>
        and set (v : bool) = r.SetObjectProperty("premultipliedAlpha", v)

    /// Provides a hint to the user agent indicating what configuration of GPU is suitable for this WebGL context. This may influence which GPU is used in a system with multiple GPUs. For example, a dual-GPU system might have one GPU that consumes less power at the expense of rendering performance. Note that this property is only a hint and a WebGL implementation may choose to ignore it.
    member x.PowerPreference
        with get() = 
            match r.GetObjectProperty("powerPreference") |> convert<string> with
            | "low-power" -> WebGLPowerPreference.Low
            | "high-performance" -> WebGLPowerPreference.HighPerformance
            | _ -> WebGLPowerPreference.Default
        and set (v : WebGLPowerPreference) =
            match v with
            | WebGLPowerPreference.Default -> r.SetObjectProperty("powerPreference", "default")
            | WebGLPowerPreference.Low -> r.SetObjectProperty("powerPreference", "low-power")
            | WebGLPowerPreference.HighPerformance -> r.SetObjectProperty("powerPreference", "high-performance")

    override x.ToString() =
        String.concat "; " [
            sprintf "Antialias: %A" x.Antialias
            sprintf "Alpha: %A" x.Alpha
            sprintf "Depth: %A" x.Depth
            sprintf "Stencil: %A" x.Stencil
            sprintf "PremultipliedAlpha: %A" x.PremultipliedAlpha
            sprintf "PowerPreference: %A" x.PowerPreference
            sprintf "FailIfMajorPerformanceCaveat: %A" x.FailIfMajorPerformanceCaveat
            sprintf "Desynchronized: %A" x.Desynchronized
        ] |> sprintf "{ %s }"

    new() =
        let o = new JSObject()

        o.SetObjectProperty("alpha", true)
        o.SetObjectProperty("antialias", true)
        o.SetObjectProperty("depth", true)
        o.SetObjectProperty("stencil", true)
        o.SetObjectProperty("premultipliedAlpha", true)
        o.SetObjectProperty("powerPreference", "default")

        WebGLContextAttributes(o)

    static member Default = 
        WebGLContextAttributes()

    new(o : JsObj) = WebGLContextAttributes(o.Reference)

type WebGLRenderingContext(ref : JSObject) =
    inherit JsObj(ref)

    abstract member IsWebGL2 : bool
    default x.IsWebGL2 = false

    /// The WebGLRenderingContext.activeTexture() method of the WebGL API specifies which texture unit to make active.
    member x.ActiveTexture(unit : int) =
        ref.Invoke("activeTexture", 0x84C0 + unit) |> ignore

    /// The WebGLRenderingContext.attachShader() method of the WebGL API attaches either a fragment or vertex WebGLShader to a WebGLProgram.
    member x.AttachShader(program : WebGLProgram, shader : WebGLShader) =
        if isNull program then raise <| ArgumentException "glAttachShader: program cannot be null"

        if isNull shader then ref.Invoke("attachShader", program.Reference, null) |> ignore
        else ref.Invoke("attachShader", program.Reference, shader.Reference) |> ignore

    /// The WebGLRenderingContext.bindAttribLocation() method of the WebGL API binds a generic vertex index to an attribute variable.
    member x.BindAttribLocation(program : WebGLProgram, index : int, name : string) =
        if isNull program then raise <| ArgumentException "glBindAttribLocation: program cannot be null"
        ref.Invoke("bindAttribLocation", program.Reference, index, name) |> ignore
        
    /// The WebGLRenderingContext.bindBuffer() method of the WebGL API binds a given WebGLBuffer to a target.
    member x.BindBuffer(target : BufferTarget, buffer : WebGLBuffer) =
        if isNull buffer then ref.Invoke("bindBuffer", int target, null) |> ignore
        else ref.Invoke("bindBuffer", int target, buffer.Reference) |> ignore

    /// The WebGLRenderingContext.bindFramebuffer() method of the WebGL API binds a given WebGLFramebuffer to a target
    member x.BindFramebuffer(target : FramebufferTarget, framebuffer : WebGLFramebuffer) =
        if isNull framebuffer then ref.Invoke("bindFramebuffer", int target, null) |> ignore
        else ref.Invoke("bindFramebuffer", int target, framebuffer.Reference) |> ignore
        
    /// The WebGLRenderingContext.bindRenderbuffer() method of the WebGL API binds a given WebGLRenderbuffer to a target, which must be gl.RENDERBUFFER
    member x.BindRenderbuffer(renderbuffer : WebGLRenderbuffer) =
        if isNull renderbuffer then ref.Invoke("bindRenderbuffer", 0x8D41, null) |> ignore
        else ref.Invoke("bindRenderbuffer", 0x8D41, renderbuffer.Reference) |> ignore
        
    /// The WebGLRenderingContext.bindTexture() method of the WebGL API binds a given WebGLTexture to a target (binding point).
    member x.BindTexture(target : TextureTarget, texture : WebGLTexture) =
        if isNull texture then ref.Invoke("bindTexture", int target, null) |> ignore
        else ref.Invoke("bindTexture", int target, texture.Reference) |> ignore
    
    /// The WebGLRenderingContext.blendColor() method of the WebGL API is used to set the source and destination blending factors.
    member x.BlendColor(r : float, g : float, b : float, a : float) =
        ref.Invoke("blendColor", r, g, b, a) |> ignore
       
    /// The WebGLRenderingContext.blendEquation() method of the WebGL API is used to set both the RGB blend equation and alpha blend equation to a single equation.
    /// The blend equation determines how a new pixel is combined with a pixel already in the WebGLFramebuffer.
    member x.BlendEquation(mode : BlendEquation) =
        ref.Invoke("blendEquation", int mode) |> ignore
     
    /// The WebGLRenderingContext.blendEquationSeparate() method of the WebGL API is used to set the RGB blend equation and alpha blend equation separately.
    /// The blend equation determines how a new pixel is combined with a pixel already in the WebGLFramebuffer.
    member x.BlendEquation(rgb : BlendEquation, alpha : BlendEquation) =
        ref.Invoke("blendEquationSeparate", int rgb, int alpha) |> ignore

    /// The WebGLRenderingContext.blendFunc() method of the WebGL API defines which function is used for blending pixel arithmetic.
    member x.BlendFunc(src : BlendFactor, dst : BlendFactor) =
        ref.Invoke("blendFunc", int src, int dst) |> ignore
        
    /// The WebGLRenderingContext.blendFuncSeparate() method of the WebGL API defines which function is used for blending pixel arithmetic for RGB and alpha components separately.
    member x.BlendFunc(srcRGB : BlendFactor, dstRGB : BlendFactor, srcAlpha : BlendFactor, dstAlpha : BlendFactor) =
        ref.Invoke("blendFuncSeparate", int srcRGB, int dstRGB, int srcAlpha, int dstAlpha) |> ignore
        

    /// The WebGLRenderingContext.bufferData() method of the WebGL API initializes and creates the buffer object's data store.
    member x.BufferData(target : BufferTarget, size : int, usage : BufferUsage) =
        ref.Invoke("bufferData", int target, size, int usage) |> ignore

    /// The WebGLRenderingContext.bufferData() method of the WebGL API initializes and creates the buffer object's data store.
    member x.BufferData(target : BufferTarget, data : ArrayBuffer, usage : BufferUsage) =
        ref.Invoke("bufferData", int target, data, int usage) |> ignore
        
    /// The WebGLRenderingContext.bufferData() method of the WebGL API initializes and creates the buffer object's data store.
    member x.BufferData(target : BufferTarget, data : SharedArrayBuffer, usage : BufferUsage) =
        ref.Invoke("bufferData", int target, data, int usage) |> ignore

    /// The WebGLRenderingContext.bufferData() method of the WebGL API initializes and creates the buffer object's data store.
    member x.BufferData(target : BufferTarget, data : TypedArray<_,_>, usage : BufferUsage) =
        ref.Invoke("bufferData", int target, data, int usage) |> ignore
        
    /// The WebGLRenderingContext.bufferSubData() method of the WebGL API updates a subset of a buffer object's data store.
    member x.BufferSubData(target : BufferTarget, offset : int, data : ArrayBuffer) =
        ref.Invoke("bufferData", int target, offset, data) |> ignore
        
    /// The WebGLRenderingContext.bufferSubData() method of the WebGL API updates a subset of a buffer object's data store.
    member x.BufferSubData(target : BufferTarget, offset : int, data : SharedArrayBuffer) =
        ref.Invoke("bufferData", int target, offset, data) |> ignore

    /// The WebGLRenderingContext.bufferSubData() method of the WebGL API updates a subset of a buffer object's data store.
    member x.BufferSubData(target : BufferTarget, offset : int, data : TypedArray<_,_>) =
        ref.Invoke("bufferSubData", int target, offset, data) |> ignore

    /// The WebGLRenderingContext.checkFramebufferStatus() method of the WebGL API returns the completeness status of the WebGLFramebuffer object.
    member x.CheckFramebuferStatus(target : FramebufferTarget) =
        ref.Invoke("checkFramebufferStatus", int target) |> ignore
        
    /// The WebGLRenderingContext.clear() method of the WebGL API clears buffers to preset values.
    member x.Clear(flags : ClearBuffers) =
        ref.Invoke("clear", int flags) |> ignore
        
    /// The WebGLRenderingContext.clearColor() method of the WebGL API specifies the color values used when clearing color buffers.
    member x.ClearColor(r : float, g : float, b : float, a : float) =
        ref.Invoke("clearColor", r, g, b, a) |> ignore

    /// The WebGLRenderingContext.clearDepth() method of the WebGL API specifies the clear value for the depth buffer.
    member x.ClearDepth(d : float) =
        ref.Invoke("clearDepth", d) |> ignore
        
    /// The WebGLRenderingContext.clearStencil() method of the WebGL API specifies the clear value for the stencil buffer.
    member x.ClearStencil(s : int) =
        ref.Invoke("clearStencil", s) |> ignore

    /// The WebGLRenderingContext.colorMask() method of the WebGL API sets which color components to enable or to disable when drawing or rendering to a WebGLFramebuffer.
    member x.ColorMask(r : bool, g : bool, b : bool, a : bool) =
        ref.Invoke("colorMask", r, g, b, a) |> ignore
        
    /// The WebGLRenderingContext.commit() method pushes frames back to the original HTMLCanvasElement, if the context is not directly fixed to a specific canvas.
    member x.Commit() =
        ref.Invoke("commit") |> ignore
        
    /// The WebGLRenderingContext.compileShader() method of the WebGL API compiles a GLSL shader into binary data so that it can be used by a WebGLProgram.
    member x.CompileShader(shader : WebGLShader) =
        ref.Invoke("compileShader", shader.Reference) |> ignore
        
    /// The WebGLRenderingContext.compressedTexImage2D()  and WebGL2RenderingContext.compressedTexImage3D() methods of the WebGL API specify a two- or three-dimensional texture image in a compressed format.
    member x.CompressedTexImage2D(target : TextureTarget, level : int, internalFormat : CompressedFormat, width : int, height : int, border : int, data : ArrayBuffer) =
        ref.Invoke("compressedTexImage2D", int target, level, int internalFormat, width, height, border, data) |> ignore
        
    /// The WebGLRenderingContext.compressedTexImage2D()  and WebGL2RenderingContext.compressedTexImage3D() methods of the WebGL API specify a two- or three-dimensional texture image in a compressed format.
    member x.CompressedTexImage2D(target : TextureTarget, level : int, internalFormat : CompressedFormat, width : int, height : int, border : int, imageSize : int, offset : int) =
        ref.Invoke("compressedTexImage2D", int target, level, int internalFormat, width, height, border, imageSize, offset) |> ignore
        
    /// The WebGLRenderingContext.compressedTexImage2D()  and WebGL2RenderingContext.compressedTexImage3D() methods of the WebGL API specify a two- or three-dimensional texture image in a compressed format.
    member x.CompressedTexImage2D(target : TextureTarget, level : int, internalFormat : CompressedFormat, width : int, height : int, border : int, data : TypedArray<_,_>, srcOffset : int, srcLength : int) =
        ref.Invoke("compressedTexImage2D", int target, level, int internalFormat, width, height, border, data, srcOffset, srcLength) |> ignore
        
    /// The WebGLRenderingContext.compressedTexImage2D()  and WebGL2RenderingContext.compressedTexImage3D() methods of the WebGL API specify a two- or three-dimensional texture image in a compressed format.
    member x.CompressedTexImage3D(target : TextureTarget, level : int, internalFormat : CompressedFormat, width : int, height : int, depth : int, border : int, imageSize : int, offset : int) =
        ref.Invoke("compressedTexImage3D", int target, level, int internalFormat, width, height, depth, border, imageSize, offset) |> ignore
        
    /// The WebGLRenderingContext.compressedTexImage2D()  and WebGL2RenderingContext.compressedTexImage3D() methods of the WebGL API specify a two- or three-dimensional texture image in a compressed format.
    member x.CompressedTexImage3D(target : TextureTarget, level : int, internalFormat : CompressedFormat, width : int, height : int, depth : int, border : int, data : TypedArray<_,_>, srcOffset : int, srcLength : int) =
        ref.Invoke("compressedTexImage3D", int target, level, int internalFormat, width, height, depth, border, data, srcOffset, srcLength) |> ignore
        
    /// The WebGLRenderingContext.compressedTexSubImage2D() method of the WebGL API specifies a two-dimensional sub-rectangle for a texture image in a compressed format.
    member x.CompressedTexSubImage2D(target : TextureTarget, level : int, xOffset : int, yOffset : int, width : int, height : int, format : CompressedFormat, data : TypedArray<_,_>) =
        ref.Invoke("compressedTexSubImage2D", int target, level, xOffset, yOffset, width, height, int format, data)
        
    /// The WebGLRenderingContext.compressedTexSubImage2D() method of the WebGL API specifies a two-dimensional sub-rectangle for a texture image in a compressed format.
    member x.CompressedTexSubImage2D(target : TextureTarget, level : int, xOffset : int, yOffset : int, width : int, height : int, format : CompressedFormat, imageSize : int, offset : int) =
        ref.Invoke("compressedTexSubImage2D", int target, level, xOffset, yOffset, width, height, int format, imageSize, offset)
        
    /// The WebGLRenderingContext.compressedTexSubImage2D() method of the WebGL API specifies a two-dimensional sub-rectangle for a texture image in a compressed format.
    member x.CompressedTexSubImage2D(target : TextureTarget, level : int, xOffset : int, yOffset : int, width : int, height : int, format : CompressedFormat, data : TypedArray<_,_>, srcOffset : int, srcLength : int) =
        ref.Invoke("compressedTexSubImage2D", int target, level, xOffset, yOffset, width, height, int format, data, srcOffset, srcLength)

    /// The WebGLRenderingContext.copyTexImage2D() method of the WebGL API copies pixels from the current WebGLFramebuffer into a 2D texture image.
    member _.CopyTexImage2D(target : TextureTarget, level : int, format : PixelFormat, x : int, y : int, width : int, height : int) =    
        ref.Invoke("copyTexImage2D", int target, level, int format, x, y, width, height)
        
    /// The WebGLRenderingContext.copyTexSubImage2D() method of the WebGL API copies pixels from the current WebGLFramebuffer into an existing 2D texture sub-image.
    member _.CopyTexSubImage2D(target : TextureTarget, level : int, xOffset : int, yOffset : int, x : int, y : int, width : int, height : int) =    
        ref.Invoke("copyTexSubImage2D", int target, level, xOffset, yOffset, x, y, width, height)

    /// The WebGLRenderingContext.createBuffer() method of the WebGL API creates and initializes a WebGLBuffer storing data such as vertices or colors.
    member x.CreateBuffer() =
        ref.Invoke("createBuffer") |> unbox<JSObject> |> WebGLBuffer
        
    /// The WebGLRenderingContext.createFramebuffer() method of the WebGL API creates and initializes a WebGLFramebuffer object.
    member x.CreateFramebuffer() =
        ref.Invoke("createFramebuffer") |> unbox<JSObject> |> WebGLFramebuffer

    /// The WebGLRenderingContext.createProgram() method of the WebGL API creates and initializes a WebGLProgram object.
    member x.CreateProgram() =
        ref.Invoke("createProgram") |> unbox<JSObject> |> WebGLProgram

    /// The WebGLRenderingContext.createRenderbuffer() method of the WebGL API creates and initializes a WebGLRenderbuffer object.
    member x.CreateRenderbuffer() =
        ref.Invoke("createRenderbuffer") |> unbox<JSObject> |> WebGLRenderbuffer
        
    /// The WebGLRenderingContext method createShader() of the WebGL API creates a WebGLShader that can then be configured further using WebGLRenderingContext.shaderSource() and WebGLRenderingContext.compileShader().
    member x.CreateShader(typ : ShaderType) =
        ref.Invoke("createShader", int typ) |> unbox<JSObject> |> WebGLShader

    /// The WebGLRenderingContext.createTexture() method of the WebGL API creates and initializes a WebGLTexture object.
    member x.CreateTexture() =
        ref.Invoke("createTexture") |> unbox<JSObject> |> WebGLTexture
        
    /// The WebGLRenderingContext.cullFace() method of the WebGL API specifies whether or not front- and/or back-facing polygons can be culled.
    member x.CullFace(mode : CullFaceMode) =
        ref.Invoke("cullFace", int mode) |> ignore 
    
    /// The WebGLRenderingContext.deleteBuffer() method of the WebGL API deletes a given WebGLBuffer. This method has no effect if the buffer has already been deleted.
    member x.DeleteBuffer(buffer : WebGLBuffer) =
        ref.Invoke("deleteBuffer", js buffer)

    /// The WebGLRenderingContext.deleteFramebuffer() method of the WebGL API deletes a given WebGLFramebuffer object. This method has no effect if the frame buffer has already been deleted.
    member x.DeleteFramebuffer(buffer : WebGLFramebuffer) =
        ref.Invoke("deleteFramebuffer", js buffer)

    /// The WebGLRenderingContext.deleteProgram() method of the WebGL API deletes a given WebGLProgram object. This method has no effect if the program has already been deleted.
    member x.DeleteProgram(buffer : WebGLProgram) =
        ref.Invoke("deleteProgram", js buffer)

    /// The WebGLRenderingContext.deleteRenderbuffer() method of the WebGL API deletes a given WebGLRenderbuffer object. This method has no effect if the render buffer has already been deleted.
    member x.DeleteRenderbuffer(buffer : WebGLRenderbuffer) =
        ref.Invoke("deleteRenderbuffer", js buffer)

    /// The WebGLRenderingContext.deleteShader() method of the WebGL API marks a given WebGLShader object for deletion. It will then be deleted whenever the shader is no longer in use. This method has no effect if the shader has already been deleted, and the WebGLShader is automatically marked for deletion when it is destroyed by the garbage collector.
    member x.DeleteShader(shader : WebGLShader) =
        ref.Invoke("deleteShader", js shader)

    /// The WebGLRenderingContext.deleteTexture() method of the WebGL API deletes a given WebGLTexture object. This method has no effect if the texture has already been deleted.
    member x.DeleteTexture(shader : WebGLTexture) =
        ref.Invoke("deleteTexture", js shader)

    /// The WebGLRenderingContext.depthFunc() method of the WebGL API specifies a function that compares incoming pixel depth to the current depth buffer value.
    member x.DepthFunc(func : DepthFunc) =
        ref.Invoke("depthFunc", int func)
        
    /// The WebGLRenderingContext.depthMask() method of the WebGL API sets whether writing into the depth buffer is enabled or disabled.
    member x.DepthMask(mask : bool) =
        ref.Invoke("depthMask", mask)
        
    /// The WebGLRenderingContext.depthRange() method of the WebGL API specifies the depth range mapping from normalized device coordinates to window or viewport coordinates.
    member x.DepthRange(zNear : float, zFar : float) =
        ref.Invoke("depthRange", zNear, zFar)
        
    /// The WebGLRenderingContext.detachShader() method of the WebGL API detaches a previously attached WebGLShader from a WebGLProgram.
    member x.DetachShader(program : WebGLProgram, shader : WebGLShader) =
        ref.Invoke("detachShader", js program, js shader)
        
    /// The WebGLRenderingContext.disable() method of the WebGL API disables specific WebGL capabilities for this context.
    member x.Disable(cap : EnableCap) =
        ref.Invoke("disable", int cap)
        
    /// The WebGLRenderingContext.disableVertexAttribArray() method of the WebGL API turns the generic vertex attribute array off at a given index position.
    member x.DisableVertexAttribArray(index : int) =
        ref.Invoke("disableVertexAttribArray", index) |> ignore
        
    /// The WebGLRenderingContext.drawArrays() method of the WebGL API renders primitives from array data.
    member x.DrawArrays(mode : PrimitiveTopology, first : int, count : int) =
        ref.Invoke("drawArrays", int mode, first, count) |> ignore

    /// The WebGLRenderingContext.drawElements() method of the WebGL API renders primitives from array data.
    member x.DrawElements(mode : PrimitiveTopology, count : int, typ : IndexType, offset : int) =
        ref.Invoke("drawArrays", int mode, count, int typ, offset) |> ignore

    /// The WebGLRenderingContext.enable() method of the WebGL API enables specific WebGL capabilities for this context.
    member x.Enable(cap : EnableCap) =
        ref.Invoke("enable", int cap)
    
    /// The WebGLRenderingContext method enableVertexAttribArray(), part of the WebGL API, turns on the generic vertex attribute array at the specified index into the list of attribute arrays.
    member x.EnableVertexAttribArray(index : int) =
        ref.Invoke("enableVertexAttribArray", index) |> ignore

    /// The WebGLRenderingContext.finish() method of the WebGL API blocks execution until all previously called commands are finished.
    member x.Finish() =
        ref.Invoke("finish") |> ignore
        
    /// The WebGLRenderingContext.flush() method of the WebGL API empties different buffer commands, causing all commands to be executed as quickly as possible.
    member x.Flush() =
        ref.Invoke("flush") |> ignore
        
    /// The WebGLRenderingContext.framebufferRenderbuffer() method of the WebGL API attaches a WebGLRenderbuffer object to a WebGLFramebuffer object.
    member x.FramebufferRenderbuffer(target : FramebufferTarget, attachment : FramebufferAttachment, renderbuffer : WebGLRenderbuffer) =
        ref.Invoke("framebufferRenderbuffer", int target, int attachment, 0x8D41, js renderbuffer)

    /// The WebGLRenderingContext.framebufferTexture2D() method of the WebGL API attaches a texture to a WebGLFramebuffer.
    member x.FramebufferTexture2D(target : FramebufferTarget, attachment : FramebufferAttachment, textureTarget : TextureTarget, texture : WebGLTexture) =
        ref.Invoke("framebufferTexture2D", int target, int attachment, int textureTarget, js texture)

    /// The WebGLRenderingContext.frontFace() method of the WebGL API specifies whether polygons are front- or back-facing by setting a winding orientation.
    member x.FrontFace(winding : Winding) =
        let w =
            match winding with
            | Winding.CCW -> 0x0901
            | Winding.CW -> 0x0900
            | _ -> failwithf "bad winding: %A" winding
        ref.Invoke("frontFace", w)

    /// The WebGLRenderingContext.generateMipmap() method of the WebGL API generates a set of mipmaps for a WebGLTexture object.
    member x.GenerateMipmap(target : TextureTarget) =
        ref.Invoke("generateMipmap", int target)

    /// The WebGLRenderingContext.getActiveAttrib() method of the WebGL API returns a WebGLActiveInfo object containing size, type, and name of a vertex attribute. It is generally used when querying unknown attributes either for debugging or generic library creation.
    member x.GetActiveAttrib(program : WebGLProgram, index : int) =
        ref.Invoke("getActiveAttrib", js program, index) |> unbox<JSObject> |> WebGLActiveInfo

    /// The WebGLRenderingContext.getActiveUniform() method of the WebGL API returns a WebGLActiveInfo object containing size, type, and name of a uniform attribute. It is generally used when querying unknown uniforms either for debugging or generic library creation.
    member x.GetActiveUniform(program : WebGLProgram, index : int) =
        ref.Invoke("getActiveUniform", js program, index) |> unbox<JSObject> |> WebGLActiveInfo

    /// The WebGLRenderingContext.getAttachedShaders() method of the WebGL API returns a list of WebGLShader objects attached to a WebGLProgram.
    member x.GetAttachedShaders(program : WebGLProgram) =
        let o = ref.Invoke("getAttachedShaders", js program) |> unbox<JSObject>
        let l = o.GetObjectProperty("length") |> convert<int>
        Array.init l (fun i -> o.GetObjectProperty(string i) |> convert<WebGLShader>)

    /// The WebGLRenderingContext.getAttribLocation() method of the WebGL API returns the location of an attribute variable in a given WebGLProgram.
    member x.GetAttribLocation(program : WebGLProgram, name : string) =
        ref.Invoke("getAttribLocation", js program, name) |> convert<int>

    /// The WebGLRenderingContext.getBufferParameter() method of the WebGL API returns information about the buffer.
    member x.GetBufferParameter(target : BufferTarget, pname : BufferParameterName) =
        ref.Invoke("getBufferParameter", int target, int pname) |> convert<int>

    /// The WebGLRenderingContext.getContextAttributes() method returns a WebGLContextAttributes object that contains the actual context parameters. Might return null, if the context is lost.
    member x.GetContextAttributes() =
        ref.Invoke("getContextAttributes") |> convert<WebGLContextAttributes>

    /// The WebGLRenderingContext.getError() method of the WebGL API returns error information.
    member x.GetError() =
        ref.Invoke("getError") |> convert<int> |> unbox<WebGLError>

    /// The WebGLRenderingContext.getExtension() method enables a WebGL extension.
    member x.GetExtension(name : string) =
        ref.Invoke("getExtension", name) |> convert<JsObj>

    /// The WebGLRenderingContext.getFramebufferAttachmentParameter() method of the WebGL API returns information about a framebuffer's attachment.
    member x.GetFramebufferAttachmentParameter<'a>(target : FramebufferTarget, attachment : FramebufferAttachment, name : FramebufferAttachmentParameter) =
        ref.Invoke("getFramebufferAttachmentParameter", int target, int attachment, int name) |> convert<'a>

    /// The WebGLRenderingContext.getParameter() method of the WebGL API returns a value for the passed parameter name.
    member x.GetParameter(name : ParameterName<'a>) =
        ref.Invoke("getParameter", name.EnumValue) |> name.Getter

    /// The WebGLRenderingContext.getProgramInfoLog returns the information log for the specified WebGLProgram object. It contains errors that occurred during failed linking or validation of WebGLProgram objects.
    member x.GetProgramInfoLog(program : WebGLProgram) =
        ref.Invoke("getProgramInfoLog", program.Reference) |> unbox<string>

    /// The WebGLRenderingContext.getProgramParameter() method of the WebGL API returns information about the given program.
    member x.GetProgramParameter(program : WebGLProgram, parameter : ProgramParameter<'a>) =
        ref.Invoke("getProgramParameter", program.Reference, parameter.EnumValue) |> parameter.Getter

    /// The WebGLRenderingContext.getRenderbufferParameter() method of the WebGL API returns information about the renderbuffer.
    member x.GetRenderbufferParameter(renderbuffer : WebGLRenderbuffer, parameter : RenderbufferParameter<'a>) = 
        ref.Invoke("getRenderbufferParameter", renderbuffer.Reference, parameter.EnumValue) |> parameter.Getter

    /// The WebGLRenderingContext.getShaderInfoLog returns the information log for the specified WebGLShader object. It contains warnings, debugging and compile information.
    member x.GetShaderInfoLog(shader : WebGLShader) =
        ref.Invoke("getShaderInfoLog", shader.Reference) |> unbox<string>

    /// The WebGLRenderingContext.getShaderParameter() method of the WebGL API returns information about the given shader.
    member x.GetShaderParameter(program : WebGLShader, parameter : ShaderParameter<'a>) =
        ref.Invoke("getShaderParameter", program.Reference, parameter.EnumValue) |> parameter.Getter

    /// The WebGLRenderingContext.getShaderPrecisionFormat() method of the WebGL API returns a new WebGLShaderPrecisionFormat object describing the range and precision for the specified shader numeric format.
    member x.GetShaderPrecisionFormat(shaderType : ShaderType, precision : ShaderPrecision) =
        ref.Invoke("getShaderPrecisionFormat", int shaderType, int precision)

    /// The WebGLRenderingContext.getShaderSource() method of the WebGL API returns the source code of a WebGLShader as a DOMString.
    member x.GetShaderSource(shader : WebGLShader) =    
        ref.Invoke("getShaderSource", shader.Reference)

    /// The WebGLRenderingContext.getSupportedExtensions() method returns a list of all the supported WebGL extensions.
    member x.GetSupportedExtensions() =
        let arr = ref.Invoke("getSupportedExtensions") |> convert<JsObj>
        let len = arr.["length"] |> convert<int>
        Array.init len (fun i -> arr.[string i] |> convert<string>)

    /// The WebGLRenderingContext.getTexParameter() method of the WebGL API returns information about the given texture.
    member x.GetTexParameter(target : TextureTarget, name : TextureParameter<'T>) =
        ref.Invoke("getTexParameter", int target, name.EnumValue) |> name.Getter
        
    /// The WebGLRenderingContext.getUniform() method of the WebGL API returns the value of a uniform variable at a given location.
    member x.GetUniform(program : WebGLProgram, location : int) =
        ref.Invoke("getUniform", program.Reference, location) |> convert<JsObj>

    /// Part of the WebGL API, the WebGLRenderingContext method getUniformLocation() returns the location of a specific uniform variable which is part of a given WebGLProgram. The uniform variable is returned as a WebGLUniformLocation object, which is an opaque identifier used to specify where in the GPU's memory that uniform variable is located.
    member x.GetUniformLocation(program : WebGLProgram, name : string) =
        ref.Invoke("getUniformLocation", program.Reference, name) |> convert<WebGLUniformLocation>

    /// The WebGLRenderingContext.getVertexAttrib() method of the WebGL API returns information about a vertex attribute at a given position.
    member x.GetVertexAttribute(index : int, name : VertexAttributeParameter<'T>) =
        ref.Invoke("getVertexAttrib", index, name.EnumValue) |> name.Getter

    /// The WebGLRenderingContext.getVertexAttribOffset() method of the WebGL API returns the address of a specified vertex attribute.
    member x.GetVertexAttribOffset(index : int, name : string) =
        ref.Invoke("getVertexAttribOffset", index, name) |> convert<int>

    /// The WebGLRenderingContext.isBuffer() method of the WebGL API returns true if the passed WebGLBuffer is valid and false otherwise.
    member x.IsBuffer(buffer : WebGLBuffer) =
        ref.Invoke("isBuffer", buffer.Reference) |> convert<bool>

    /// The WebGLRenderingContext.isContextLost() method returns a Boolean indicating whether or not the WebGL context has been lost and must be re-established before rendering can resume.
    member x.IsContextLost =
        ref.Invoke("isContextLost") |> convert<bool>

    /// The WebGLRenderingContext.isEnabled() method of the WebGL API tests whether a specific WebGL capability is enabled or not for this context.
    member x.IsEnabled(cap : EnableCap) =
        ref.Invoke("isEnabled", int cap) |> convert<bool>

    /// The WebGLRenderingContext.isFramebuffer() method of the WebGL API returns true if the passed WebGLFramebuffer is valid and false otherwise.
    member x.IsFramebuffer(fbo : WebGLFramebuffer) =
        ref.Invoke("isFramebuffer", fbo.Reference) |> convert<bool>

    /// The WebGLRenderingContext.isProgram() method of the WebGL API returns true if the passed WebGLProgram is valid, false otherwise.
    member x.IsProgram(program : WebGLProgram) =
        ref.Invoke("isProgram", program.Reference) |> convert<bool>

    /// The WebGLRenderingContext.isRenderbuffer() method of the WebGL API returns true if the passed WebGLRenderbuffer is valid and false otherwise.
    member x.IsRenderbuffer(renderbuffer : WebGLRenderbuffer) = 
        ref.Invoke("isRenderbuffer", renderbuffer.Reference) |> convert<bool>

    /// The WebGLRenderingContext.isShader() method of the WebGL API returns true if the passed WebGLShader is valid, false otherwise.
    member x.IsShader(shader : WebGLShader) = 
        ref.Invoke("isShader", shader.Reference) |> convert<bool>

    /// The WebGLRenderingContext.isTexture() method of the WebGL API returns true if the passed WebGLTexture is valid and false otherwise.
    member x.IsTexture(texture : WebGLTexture) = 
        ref.Invoke("isTexture", texture.Reference) |> convert<bool>
        
    /// The WebGLRenderingContext.lineWidth() method of the WebGL API sets the line width of rasterized lines.
    member x.LineWidth(width : float32) =
        ref.Invoke("lineWidth", width) |> ignore
        
    /// The WebGLRenderingContext interface's linkProgram() method links a given WebGLProgram, completing the process of preparing the GPU code for the program's fragment and vertex shaders.
    member x.LinkProgram(program : WebGLProgram) =
        ref.Invoke("linkProgram", program.Reference) |> ignore
        
    /// The WebGLRenderingContext.pixelStorei() method of the WebGL API specifies the pixel storage modes.
    member x.PixelStore(name : PixelStoreParameter, value : int) =
        ref.Invoke("pixelStorei", int name, value) |> ignore

    /// The WebGLRenderingContext.polygonOffset() method of the WebGL API specifies the scale factors and units to calculate depth values.
    member x.PolygonOffset(factor : float32, units : float32) =
        ref.Invoke("polygonOffset", factor, units) |> ignore

    /// The WebGLRenderingContext.readPixels() method of the WebGL API reads a block of pixels from a specified rectangle of the current color framebuffer into an ArrayBufferView object.
    member _.ReadPixels(x : int, y : int, width : int, height : int, format : PixelFormat, typ : PixelType, pixels : TypedArray<_,_>) =
        ref.Invoke("readPixels", x, y, width, height, int format, int typ, pixels) |> ignore
        
    /// The WebGLRenderingContext.readPixels() method of the WebGL API reads a block of pixels from a specified rectangle of the current color framebuffer into an ArrayBufferView object.
    member _.ReadPixels(x : int, y : int, width : int, height : int, format : PixelFormat, typ : PixelType, offset : int) =
        ref.Invoke("readPixels", x, y, width, height, int format, int typ, offset) |> ignore
        
    /// The WebGLRenderingContext.readPixels() method of the WebGL API reads a block of pixels from a specified rectangle of the current color framebuffer into an ArrayBufferView object.
    member _.ReadPixels(x : int, y : int, width : int, height : int, format : PixelFormat, typ : PixelType, pixels : TypedArray<_,_>, dstOffset : int) =
        ref.Invoke("readPixels", x, y, width, height, int format, int typ, pixels, dstOffset) |> ignore
        
    /// The WebGLRenderingContext.renderbufferStorage() method of the WebGL API creates and initializes a renderbuffer object's data store.
    member x.RenderbufferStorage(internalFormat : InternalFormat, width : int, height : int) =
        ref.Invoke("renderbufferStorage", 0x8D41, int internalFormat, width, height) |> ignore

    /// The WebGLRenderingContext.sampleCoverage() method of the WebGL API specifies multi-sample coverage parameters for anti-aliasing effects.
    member x.SampleCoverage(value : float32, invert : bool) =
        ref.Invoke("sampleCoverage", value, invert) |> ignore

    /// The WebGLRenderingContext.scissor() method of the WebGL API sets a scissor box, which limits the drawing to a specified rectangle.
    member _.Scissor(x : int, y : int, w : int, h : int) =
        ref.Invoke("scissor", x, y, w, h) |> ignore

    /// The WebGLRenderingContext.shaderSource() method of the WebGL API sets the source code of a WebGLShader.
    member x.ShaderSource(shader : WebGLShader, source : string) =
        ref.Invoke("shaderSource", shader.Reference, source) |> ignore

    /// The WebGLRenderingContext.stencilFunc() method of the WebGL API sets the front and back function and reference value for stencil testing.
    member x.StencilFunc(func : StencilFunc, r : int, mask : int) =
        ref.Invoke("stencilFunc", int func, r, mask) |> ignore
        
    /// The WebGLRenderingContext.stencilFuncSeparate() method of the WebGL API sets the front and/or back function and reference value for stencil testing.
    member x.StencilFunc(face : CullFaceMode, func : StencilFunc, r : int, mask : int) =
        ref.Invoke("stencilFuncSeparate", int face, int func, r, mask) |> ignore
        
    /// The WebGLRenderingContext.stencilMask() method of the WebGL API controls enabling and disabling of both the front and back writing of individual bits in the stencil planes.
    member x.StencilMask(mask : int) =
        ref.Invoke("stencilMask", mask)

    /// The WebGLRenderingContext.stencilMaskSeparate() method of the WebGL API controls enabling and disabling of front and/or back writing of individual bits in the stencil planes.
    member x.StencilMask(face : CullFaceMode, mask : int) =
        ref.Invoke("stencilMaskSeparate", int face, mask)

    /// The WebGLRenderingContext.stencilOp() method of the WebGL API sets both the front and back-facing stencil test actions.
    member x.StencilOperation(fail : StencilOperation, zfail : StencilOperation, zpass : StencilOperation) =
        ref.Invoke("stencilOp", int fail, int zfail, int zpass)
        
    /// The WebGLRenderingContext.stencilOpSeparate() method of the WebGL API sets the front and/or back-facing stencil test actions.
    member x.StencilOperation(face : CullFaceMode, fail : StencilOperation, zfail : StencilOperation, zpass : StencilOperation) =
        ref.Invoke("stencilOpSeparate", int face, int fail, int zfail, int zpass)
        
    /// The WebGLRenderingContext.texImage2D() method of the WebGL API specifies a two-dimensional texture image.
    member x.TexImage2D(target : TextureTarget, level : int, internalFormat : InternalFormat, width : int, height : int, border : int, format : PixelFormat, typ : PixelType, data : TypedArray<_,_>) =
        ref.Invoke("texImage2D", int target, level, int internalFormat, width, height, border, int format, int typ, data)
        
    /// The WebGLRenderingContext.texImage2D() method of the WebGL API specifies a two-dimensional texture image.
    member x.TexImage2D(target : TextureTarget, level : int, internalFormat : InternalFormat, format : PixelFormat, typ : PixelType, data : JsObj) =
        ref.Invoke("texImage2D", int target, level, int internalFormat, int format, int typ, data.Reference)
        
    /// The WebGLRenderingContext.texImage2D() method of the WebGL API specifies a two-dimensional texture image.
    member x.TexImage2D(target : TextureTarget, level : int, internalFormat : InternalFormat, width : int, height : int, border : int, format : PixelFormat, typ : PixelType, offset : int) =
        ref.Invoke("texImage2D", int target, level, int internalFormat, width, height, border, int format, int typ, offset)
        
    /// The WebGLRenderingContext.texImage2D() method of the WebGL API specifies a two-dimensional texture image.
    member x.TexImage2D(target : TextureTarget, level : int, internalFormat : InternalFormat, width : int, height : int, border : int, format : PixelFormat, typ : PixelType, data : JsObj) =
        ref.Invoke("texImage2D", int target, level, int internalFormat, width, height, border, int format, int typ, data.Reference)
        
    /// The WebGLRenderingContext.texImage2D() method of the WebGL API specifies a two-dimensional texture image.
    member x.TexImage2D(target : TextureTarget, level : int, internalFormat : InternalFormat, width : int, height : int, border : int, format : PixelFormat, typ : PixelType, data : TypedArray<_,_>, srcOffset : int) =
        ref.Invoke("texImage2D", int target, level, int internalFormat, width, height, border, int format, int typ, data, srcOffset)

    /// The WebGLRenderingContext.texParameter[fi]() methods of the WebGL API set texture parameters.
    member x.TexParameter(target : TextureTarget, parameter : TextureParameter<'T>, value : 'T) =
        if typeof<'T> = typeof<float32> then ref.Invoke("texParameterf", int target, parameter.EnumValue, value) |> ignore
        else ref.Invoke("texParameteri", int target, parameter.EnumValue, value) |> ignore

    /// The WebGLRenderingContext.texSubImage2D() method of the WebGL API specifies a sub-rectangle of the current texture.
    member x.TexSubImage2D(target : TextureTarget, level : int, xoffset : int, yoffset : int, width : int, height : int, format : PixelFormat, typ : PixelType, data : TypedArray<_,_>) =
        ref.Invoke("texSubImage2D", int target, level, xoffset, yoffset, width, height, int format, int typ, data) |> ignore
        
    /// The WebGLRenderingContext.texSubImage2D() method of the WebGL API specifies a sub-rectangle of the current texture.
    member x.TexSubImage2D(target : TextureTarget, level : int, xoffset : int, yoffset : int, width : int, height : int, image : JsObj) =
        ref.Invoke("texSubImage2D", int target, level, xoffset, yoffset, width, height, image.Reference) |> ignore
        
    /// The WebGLRenderingContext.texSubImage2D() method of the WebGL API specifies a sub-rectangle of the current texture.
    member x.TexSubImage2D(target : TextureTarget, level : int, xoffset : int, yoffset : int, width : int, height : int, format : PixelFormat, typ : PixelType, offset : int) =
        ref.Invoke("texSubImage2D", int target, level, xoffset, yoffset, width, height, int format, int typ, offset) |> ignore
        
    /// The WebGLRenderingContext.texSubImage2D() method of the WebGL API specifies a sub-rectangle of the current texture.
    member x.TexSubImage2D(target : TextureTarget, level : int, xoffset : int, yoffset : int, width : int, height : int, format : PixelFormat, typ : PixelType, data : JsObj) =
        ref.Invoke("texSubImage2D", int target, level, xoffset, yoffset, width, height, int format, int typ, data.Reference) |> ignore
        
    /// The WebGLRenderingContext.uniform[1234][fi][v]() methods of the WebGL API specify values of uniform variables.
    member x.Uniform1(location : WebGLUniformLocation, value : int) =
        ref.Invoke("uniform1i", location.Reference, value) |> ignore
        
    /// The WebGLRenderingContext.uniform[1234][fi][v]() methods of the WebGL API specify values of uniform variables.
    member x.Uniform1(location : WebGLUniformLocation, value : int[]) =
        ref.Invoke("uniform1iv", location.Reference, Int32Array.op_Implicit(Span value)) |> ignore
        
    /// The WebGLRenderingContext.uniform[1234][fi][v]() methods of the WebGL API specify values of uniform variables.
    member x.Uniform1(location : WebGLUniformLocation, value : float32) =
        ref.Invoke("uniform1f", location.Reference, value) |> ignore
        
    /// The WebGLRenderingContext.uniform[1234][fi][v]() methods of the WebGL API specify values of uniform variables.
    member x.Uniform1(location : WebGLUniformLocation, value : float32[]) =
        ref.Invoke("uniform1fv", location.Reference, Float32Array.op_Implicit(Span value)) |> ignore

        
    /// The WebGLRenderingContext.uniform[1234][fi][v]() methods of the WebGL API specify values of uniform variables.
    member x.Uniform2(location : WebGLUniformLocation, value : V2i) =
        ref.Invoke("uniform2i", location.Reference, value.X, value.Y) |> ignore
        
    /// The WebGLRenderingContext.uniform[1234][fi][v]() methods of the WebGL API specify values of uniform variables.
    member x.Uniform2(location : WebGLUniformLocation, value : V2i[]) =
        let r = new Int32Array(value.Length * 2)
        let mutable oi = 0
        for i in 0 .. value.Length - 1 do r.[oi] <- Nullable value.[i].X; r.[oi + 1] <- Nullable value.[i].Y; oi <- oi + 2
        ref.Invoke("uniform2iv", location.Reference, r) |> ignore
        
    /// The WebGLRenderingContext.uniform[1234][fi][v]() methods of the WebGL API specify values of uniform variables.
    member x.Uniform2(location : WebGLUniformLocation, value : V2f) =
        ref.Invoke("uniform2f", location.Reference, value.X, value.Y) |> ignore
        
    /// The WebGLRenderingContext.uniform[1234][fi][v]() methods of the WebGL API specify values of uniform variables.
    member x.Uniform2(location : WebGLUniformLocation, value : V2f[]) =
        let r = new Float32Array(value.Length * 2)
        let mutable oi = 0
        for i in 0 .. value.Length - 1 do r.[oi] <- Nullable value.[i].X; r.[oi + 1] <- Nullable value.[i].Y; oi <- oi + 2
        ref.Invoke("uniform2fv", location.Reference, r) |> ignore

        
    /// The WebGLRenderingContext.uniform[1234][fi][v]() methods of the WebGL API specify values of uniform variables.
    member x.Uniform3(location : WebGLUniformLocation, value : V3i) =
        ref.Invoke("uniform3i", location.Reference, value.X, value.Y, value.Z) |> ignore
        
    /// The WebGLRenderingContext.uniform[1234][fi][v]() methods of the WebGL API specify values of uniform variables.
    member x.Uniform3(location : WebGLUniformLocation, value : V3i[]) =
        let r = new Int32Array(value.Length * 3)
        let mutable oi = 0
        for i in 0 .. value.Length - 1 do r.[oi] <- Nullable value.[i].X; r.[oi + 1] <- Nullable value.[i].Y; r.[oi + 2] <- Nullable value.[i].Z; oi <- oi + 3
        ref.Invoke("uniform3iv", location.Reference, r) |> ignore
        
    /// The WebGLRenderingContext.uniform[1234][fi][v]() methods of the WebGL API specify values of uniform variables.
    member x.Uniform3(location : WebGLUniformLocation, value : V3f) =
        ref.Invoke("uniform3f", location.Reference, value.X, value.Y, value.Z) |> ignore
        
    /// The WebGLRenderingContext.uniform[1234][fi][v]() methods of the WebGL API specify values of uniform variables.
    member x.Uniform3(location : WebGLUniformLocation, value : V3f[]) =
        let r = new Float32Array(value.Length * 3)
        let mutable oi = 0
        for i in 0 .. value.Length - 1 do r.[oi] <- Nullable value.[i].X; r.[oi + 1] <- Nullable value.[i].Y; r.[oi + 2] <- Nullable value.[i].Z; oi <- oi + 3
        ref.Invoke("uniform3fv", location.Reference, r) |> ignore

        
    /// The WebGLRenderingContext.uniform[1234][fi][v]() methods of the WebGL API specify values of uniform variables.
    member x.Uniform4(location : WebGLUniformLocation, value : V4i) =
        ref.Invoke("uniform4i", location.Reference, value.X, value.Y, value.Z, value.W) |> ignore
        
    /// The WebGLRenderingContext.uniform[1234][fi][v]() methods of the WebGL API specify values of uniform variables.
    member x.Uniform4(location : WebGLUniformLocation, value : V4i[]) =
        let r = new Int32Array(value.Length * 4)
        let mutable oi = 0
        for i in 0 .. value.Length - 1 do r.[oi] <- Nullable value.[i].X; r.[oi + 1] <- Nullable value.[i].Y; r.[oi + 2] <- Nullable value.[i].Z; r.[oi + 3] <- Nullable value.[i].W; oi <- oi + 4
        ref.Invoke("uniform4iv", location.Reference, r) |> ignore
        
    /// The WebGLRenderingContext.uniform[1234][fi][v]() methods of the WebGL API specify values of uniform variables.
    member x.Uniform4(location : WebGLUniformLocation, value : V4f) =
        ref.Invoke("uniform4f", location.Reference, value.X, value.Y, value.Z, value.W) |> ignore
        
    /// The WebGLRenderingContext.uniform[1234][fi][v]() methods of the WebGL API specify values of uniform variables.
    member x.Uniform4(location : WebGLUniformLocation, value : V4f[]) =
        let r = new Float32Array(value.Length * 4)
        let mutable oi = 0
        for i in 0 .. value.Length - 1 do r.[oi] <- Nullable value.[i].X; r.[oi + 1] <- Nullable value.[i].Y; r.[oi + 2] <- Nullable value.[i].Z; r.[oi + 3] <- Nullable value.[i].W; oi <- oi + 4
        ref.Invoke("uniform4fv", location.Reference, r) |> ignore

    /// The WebGLRenderingContext.uniformMatrix[234]fv() methods of the WebGL API specify matrix values for uniform variables.
    member x.UniformMatrix(location : WebGLUniformLocation, value : M22d) =
        ref.Invoke("uniformMatrix2fv", location.Reference, false, Float32Array.op_Implicit(Span((M22f value).ToArray())))
        
    /// The WebGLRenderingContext.uniformMatrix[234]fv() methods of the WebGL API specify matrix values for uniform variables.
    member x.UniformMatrix(location : WebGLUniformLocation, value : M33d) =
        ref.Invoke("uniformMatrix3fv", location.Reference, false, Float32Array.op_Implicit(Span((M33f value).ToArray())))
        
    /// The WebGLRenderingContext.uniformMatrix[234]fv() methods of the WebGL API specify matrix values for uniform variables.
    member x.UniformMatrix(location : WebGLUniformLocation, value : M44d) =
        ref.Invoke("uniformMatrix4fv", location.Reference, false, Float32Array.op_Implicit(Span((M44f value).ToArray())))

    /// The WebGLRenderingContext.useProgram() method of the WebGL API sets the specified WebGLProgram as part of the current rendering state.
    member x.UseProgram(program : WebGLProgram) =
        if isNull program then ref.Invoke("useProgram", [| null |]) |> ignore
        else ref.Invoke("useProgram", program.Reference) |> ignore
    
    /// The WebGLRenderingContext.validateProgram() method of the WebGL API validates a WebGLProgram. It checks if it is successfully linked and if it can be used in the current WebGL state.
    member x.ValidateProgram(program : WebGLProgram) =
        if isNull program then ref.Invoke("validateProgram", [| null |]) |> ignore
        else ref.Invoke("validateProgram", program.Reference) |> ignore

    /// The WebGLRenderingContext.vertexAttrib[1234]f[v]() methods of the WebGL API specify constant values for generic vertex attributes.
    member x.VertexAttrib(index : int, value : float32) =
        ref.Invoke("vertexAttrib1f", index, value) |> ignore

    /// The WebGLRenderingContext.vertexAttrib[1234]f[v]() methods of the WebGL API specify constant values for generic vertex attributes.
    member x.VertexAttrib(index : int, value : V2f) =
        ref.Invoke("vertexAttrib2f", index, value.X, value.Y) |> ignore

    /// The WebGLRenderingContext.vertexAttrib[1234]f[v]() methods of the WebGL API specify constant values for generic vertex attributes.
    member x.VertexAttrib(index : int, value : V3f) =
        ref.Invoke("vertexAttrib3f", index, value.X, value.Y, value.Z) |> ignore
        
    /// The WebGLRenderingContext.vertexAttrib[1234]f[v]() methods of the WebGL API specify constant values for generic vertex attributes.
    member x.VertexAttrib(index : int, value : V4f) =
        ref.Invoke("vertexAttrib4f", index, value.X, value.Y, value.Z, value.W) |> ignore

    /// The WebGLRenderingContext.vertexAttribPointer() method of the WebGL API binds the buffer currently bound to gl.ARRAY_BUFFER to a generic vertex attribute of the current vertex buffer object and specifies its layout.
    member x.VertexAttribPointer(index : int, size : int, typ : VertexAttribType, normalized : bool, stride : int, offset : int) =
        ref.Invoke("vertexAttribPointer", index, size, int typ, normalized, stride, offset) |> ignore
        
    /// The WebGLRenderingContext.viewport() method of the WebGL API sets the viewport, which specifies the affine transformation of x and y from normalized device coordinates to window coordinates.
    member _.Viewport(x : int, y : int, w : int, h : int) =
        ref.Invoke("viewport", x,y,w,h) |> ignore


    new(o : JsObj) = WebGLRenderingContext(o.Reference)



type QueryTarget =
    | AnySamplesPassed = 0x8C2F
    | AnySamplesPassedConservative = 0x8D6A
    | TransformFeedbackPrimitivesWritten = 0x8C88

[<AllowNullLiteral>]
type WebGLQuery(ref : JSObject) =
    inherit JsObj(ref)
    new(o : JsObj) = WebGLQuery(o.Reference)
    
[<AllowNullLiteral>]
type WebGLSampler(ref : JSObject) =
    inherit JsObj(ref)
    new(o : JsObj) = WebGLSampler(o.Reference)
    
[<AllowNullLiteral>]
type WebGLTransformFeedback(ref : JSObject) =
    inherit JsObj(ref)
    new(o : JsObj) = WebGLTransformFeedback(o.Reference)
       
[<AllowNullLiteral>]
type WebGLVertexArray(ref : JSObject) =
    inherit JsObj(ref)
    new(o : JsObj) = WebGLVertexArray(o.Reference)
    
[<AllowNullLiteral>]
type WebGLSync(ref : JSObject) =
    inherit JsObj(ref)
    new(o : JsObj) = WebGLSync(o.Reference)

type Filter =
    | Nearest = 0x2600
    | Linear = 0x2601

type SyncFlags =
    | None = 0
    | FlushCommands = 0x00000001

type WaitStatus =
    | AlreadySignaled = 0x911A
    | TimeoutExpired = 0x911B
    | ConditionSatisfied = 0x911C
    | WaitFailed = 0x911D

type WebGL2RenderingContext(ref : JSObject) =
    inherit WebGLRenderingContext(ref)

    override x.IsWebGL2 = true

    /// The WebGL2RenderingContext.beginQuery() method of the WebGL 2 API starts an asynchronous query. The target parameter indicates which kind of query to begin.
    member x.BeginQuery(target : QueryTarget, query : WebGLQuery) =
        if isNull query then ref.Invoke("beginQuery", int target, null) |> ignore
        else ref.Invoke("beginQuery", int target, query.Reference) |> ignore

    /// The WebGL2RenderingContext.beginTransformFeedback() method of the WebGL 2 API starts a transform feedback operation.
    member x.BeginTransformFeedback(primitiveMode : PrimitiveTopology) =
        ref.Invoke("beginTransformFeedback", int primitiveMode) |> ignore

    /// The WebGL2RenderingContext.bindBufferBase() method of the WebGL 2 API binds a given WebGLBuffer to a given binding point (target) at a given index.
    member x.BindBufferBase(target : BufferTarget, index : int, buffer : WebGLBuffer) =
        if isNull buffer then ref.Invoke("bindBufferBase", int target, index, null) |> ignore
        else ref.Invoke("bindBufferBase", int target, index, buffer.Reference) |> ignore

    /// The WebGL2RenderingContext.bindBufferRange() method of the WebGL 2 API binds a range of a given WebGLBuffer to a given binding point (target) at a given index.
    member x.BindBufferRange(target : BufferTarget, index : int, buffer : WebGLBuffer, offset : int, size : int) =
        if isNull buffer then ref.Invoke("bindBufferRange", int target, index, null, offset, size) |> ignore
        else ref.Invoke("bindBufferRange", int target, index, buffer.Reference, offset, size) |> ignore
        
    /// The WebGL2RenderingContext.bindSampler() method of the WebGL 2 API binds a passed WebGLSampler object to the texture unit at the passed index.
    member x.BindSampler(unit : int, sampler : WebGLSampler) =  
        if isNull sampler then ref.Invoke("bindSampler", unit, null) |> ignore
        else ref.Invoke("bindSampler", unit, sampler.Reference) |> ignore
         
    /// The WebGL2RenderingContext.bindTransformFeedback() method of the WebGL 2 API binds a passed WebGLTransformFeedback object to the current GL state.
    member x.BindTransformFeedback(feedback : WebGLTransformFeedback) =
        if isNull feedback then ref.Invoke("bindTransformFeedback", 0x8C8E, null) |> ignore
        else ref.Invoke("bindTransformFeedback", 0x8C8E, feedback.Reference) |> ignore
        
    /// The WebGL2RenderingContext.bindVertexArray() method of the WebGL 2 API binds a passed WebGLVertexArrayObject object to the buffer.
    member x.BindVertexArray(vao : WebGLVertexArray) =
        if isNull vao then ref.Invoke("bindVertexArray", [| null |]) |> ignore
        else ref.Invoke("bindVertexArray", vao.Reference) |> ignore
        
    /// The WebGL2RenderingContext.blitFramebuffer() method of the WebGL 2 API transfers a block of pixels from the read framebuffer to the draw framebuffer.
    member x.BlitFramebuffer(srcX0 : int, srcY0 : int, srcX1 : int, srcY1 : int, dstX0 : int, dstY0 : int, dstX1 : int, dstY1 : int, mask : ClearBuffers, filter : Filter) =
        ref.Invoke("blitFramebuffer", srcX0, srcY0, srcX1, srcY1, dstX0, dstY0, dstX1, dstY1, int mask, int filter)

    /// The WebGL2RenderingContext.clearBuffer[fiuv]() methods of the WebGL 2 API clear buffers from the currently bound framebuffer.
    member x.ClearBufferColor(drawBuffer : int, value : C4f) =
        ref.Invoke("clearBufferfv", 0x1800, drawBuffer, Float32Array.op_Implicit(Span [| value.R; value.G; value.B; value.A |])) |> ignore
        
    /// The WebGL2RenderingContext.clearBuffer[fiuv]() methods of the WebGL 2 API clear buffers from the currently bound framebuffer.
    member x.ClearBufferDepth(drawBuffer : int, value : float) =
        ref.Invoke("clearBufferfv", 0x1801, drawBuffer, Float32Array.op_Implicit(Span [| float32 value |])) |> ignore
        
    /// The WebGL2RenderingContext.clearBuffer[fiuv]() methods of the WebGL 2 API clear buffers from the currently bound framebuffer.
    member x.ClearBufferDepthStencil(drawBuffer : int, depth : float, stencil : int) =
        ref.Invoke("clearBufferfi", 0x84F9, drawBuffer, depth, stencil) |> ignore

    /// The WebGL2RenderingContext.clientWaitSync() method of the WebGL 2 API blocks and waits for a WebGLSync object to become signaled or a given timeout to be passed.
    member x.ClientWaitSync(sync : WebGLSync, flags : SyncFlags, timeout : int64) =
        ref.Invoke("clientWaitSync", sync.Reference, int flags, timeout) |> convert<int> |> unbox<WaitStatus>


[<AbstractClass; Sealed; Extension>]
type HTMLCanvasWebGLExtensions private() =

    [<Extension>]
    static member GetWebGLContext(self : HTMLCanvasElement) =
        let r = self.Reference
        let o = r.Invoke("getContext", "webgl2") |> unbox<JSObject>
        if isNull o then
            let names = [|"webgl"; "experimental-webgl" |]
            names |> Array.pick (fun n ->
                let o = r.Invoke("getContext", n) |> unbox<JSObject>
                if isNull o then None
                else Some (WebGLRenderingContext o)
            )
        else
            WebGL2RenderingContext(o) :> WebGLRenderingContext
        
    [<Extension>]
    static member GetWebGLContext(self : HTMLCanvasElement, att : WebGLContextAttributes) =
        let r = self.Reference
        let o = r.Invoke("getContext", "webgl2", js att) |> unbox<JSObject>
        if isNull o then
            let names = [|"webgl"; "experimental-webgl" |]
            names |> Array.pick (fun n ->
                let o = r.Invoke("getContext", n, js att) |> unbox<JSObject>
                if isNull o then None
                else Some (WebGLRenderingContext o)
            )
        else
            WebGL2RenderingContext(o) :> WebGLRenderingContext

