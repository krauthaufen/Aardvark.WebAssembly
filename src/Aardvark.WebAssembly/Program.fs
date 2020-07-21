// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp
module Bla 

open System
open WebAssembly
open WebAssembly.Core
open Aardvark.Base
open Aardvark.WebAssembly


let inline (?) (o : JsObj) (name : string) : 'a =
    if typeof<'a> = typeof<JsObj> then o.[name] |> unbox<JSObject> |> JsObj |> unbox<'a>
    else o.[name] |> unbox<'a>

let inline (?<-) (o : JsObj) (name : string) (value : 'a) =
    match value :> obj with
    | :? JsObj as v -> o.[name] <- v.Reference
    | _ -> o.[name] <- value

type ShaderType =
    | Vertex = 0x8B31
    | Fragment = 0x8B30


[<Flags>]
type ClearBuffers =
    | Color     = 0x4000
    | Stencil   = 0x0400
    | Depth     = 0x0100

type BufferTarget =
    | Array = 0x8892
    | Element = 0x8893
    | CopyRead = 0x8F36
    | CopyWrite = 0x8F37
    | TransformFeedback = 0x8C8E
    | Uniform = 0x8A11
    | PixelPack = 0x88EB
    | PixelUnpack = 0x88EC

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
    | A = 0
     
type PixelFormat =
    | Alpha = 0x1906
    | Rgb = 0x1907
    | Rgba = 0x1908
    | Luminance = 0x1909
    | LuminanceAlpha = 0x190A

type PixelType =
    | A = 0

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

[<AllowNullLiteral>]
type WebGLProgram(ref : JSObject) =
    inherit JsObj(ref)
    new(o : JsObj) = WebGLProgram(o.Reference)

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

type WebGLRenderingContext(ref : JSObject) =
    inherit JsObj(ref)

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
        




    member x.ShaderSource(shader : WebGLShader, source : string) =
        ref.Invoke("shaderSource", shader.Reference, source) |> ignore
        
    member x.LinkProgram(program : WebGLProgram) =
        ref.Invoke("linkProgram", program.Reference) |> ignore
        
    member x.GetShaderInfoLog(shader : WebGLShader) =
        ref.Invoke("getShaderInfoLog", shader.Reference) |> unbox<string>

    member x.GetProgramInfoLog(program : WebGLProgram) =
        ref.Invoke("getProgramInfoLog", program.Reference) |> unbox<string>

    member x.UseProgram(program : WebGLProgram) =
        if isNull program then ref.Invoke("useProgram", [| null |]) |> ignore
        else ref.Invoke("useProgram", program.Reference) |> ignore
    
    member x.VertexAttribPointer(index : int, size : int, typ : VertexAttribType, normalized : bool, stride : int, offset : int) =
        ref.Invoke("vertexAttribPointer", index, size, int typ, normalized, stride, offset) |> ignore
        
    member x.Uniform(location : int, value : int) =
        ref.Invoke("uniform1i", location, value) |> ignore

    member x.BindBufferRange(target : BufferTarget, index : int, buffer : WebGLBuffer, offset : int, size : int) =
        if isNull buffer then ref.Invoke("bindBufferRange", int target, index, null, offset, size) |> ignore
        else ref.Invoke("bindBufferRange", int target, index, buffer.Reference, offset, size) |> ignore
        
    member __.Viewport(x : int, y : int, w : int, h : int) =
        ref.Invoke("viewport", x,y,w,h) |> ignore

    new(o : JsObj) = WebGLRenderingContext(o.Reference)


type HTMLCanvasElement(r : JSObject) =
    inherit HTMLElement(r)
    
    member x.Width
        with get() = r.GetObjectProperty("width") |> unbox<int>
        and set (id : int) = r.SetObjectProperty("width", id)

    member x.Height
        with get() = r.GetObjectProperty("height") |> unbox<int>
        and set (id : int) = r.SetObjectProperty("height", id)

    member x.GetWebGLContext() =
        r.Invoke("getContext", "webgl2") |> unbox<JSObject> |> WebGLRenderingContext

    new(o : JsObj) = HTMLCanvasElement(o.Reference)


type HTMLDocument(r : JSObject) =
    inherit Element(r)

    member x.Body = r.GetObjectProperty("body") |> unbox<JSObject> |> HTMLElement

    member x.CreateElement(tagName : string) = r.Invoke("createElement", tagName) |> unbox<JSObject> |> HTMLElement
    
    member x.CreateCanvasElement() = r.Invoke("createElement", "canvas") |> unbox<JSObject> |> HTMLCanvasElement
    
    member x.GetElementById(id : string) = r.Invoke("getElementById", id) |> convert<Element>


    /// The Document method exitFullscreen() requests that the element on this document which is currently being presented in full-screen mode be taken out of full-screen mode, restoring the previous state of the screen. This usually reverses the effects of a previous call to Element.requestFullscreen().
    member x.ExitFullscreen() =
        r.Invoke("exitFullscreen") |> ignore

    member x.FullscreenElement =
        r.GetObjectProperty("fullscreenElement") |> convert<Element>

    new(o : JsObj) = HTMLDocument(o.Reference)
    

type Console(r : JSObject) =
    inherit JsObj(r)

    member x.Begin(name : string) =
        r.Invoke("group", name) |> ignore
        
    member x.BeginCollapsed(name : string) =
        r.Invoke("groupCollapsed", name) |> ignore

    member x.End() =
        r.Invoke("groupEnd") |> ignore

    member x.Log([<ParamArray>] values : obj[]) =
        r.Invoke("log", values) |> ignore
    
    member x.Warn([<ParamArray>] values : obj[]) =
        r.Invoke("warn", values) |> ignore
        
    new(o : JsObj) = Console(o.Reference)
    

[<AutoOpen>]
module RuntimValues = 
    let Window = JsObj (unbox<JSObject> (Runtime.GetGlobalObject "window"))
    let Document = HTMLDocument (unbox<JSObject> (Runtime.GetGlobalObject "document"))
    let Console = Console (unbox<JSObject> (Runtime.GetGlobalObject "console"))



open FSharp.Data.Adaptive
open System.Reflection.Emit
open System.Reflection
 
let testDynamicMethod() =
    Console.Begin "DynamicMethod"
    let m = DynamicMethod("bla", typeof<int>, [| typeof<int> |] )
    let il = m.GetILGenerator()
    il.Emit(OpCodes.Ldarg_0)
    il.Emit(OpCodes.Ldc_I4, 5)
    il.Emit(OpCodes.Add)
    il.Emit(OpCodes.Ret)

    let f = m.CreateDelegate(typeof<Func<int, int>>) |> unbox<Func<int, int>>
    Console.Log("dyn", 15, "=?=", f.Invoke(10))
    Console.End()

open FSharp.Data.Adaptive

let testAdaptive() =
    Console.Begin "Adaptive"
    let a = cval 10
    let b = a |> AVal.map ((+) 5) 
    printfn "%A =?= 15" (AVal.force b)
    transact (fun () -> a.Value <- 5)
    printfn "%A =?= 10" (AVal.force b)

    Console.End()

let testArrayBuffer() =
    Console.Begin "ArrayBuffer" 
    let data = Array.init 1024 byte
    use a = Uint8Array.op_Implicit(Span data)
     
    let f = Runtime.CompileFunction("return function(a) { a[0] = 100; };")
    f.Call(null, a) |> ignore
      
    Console.Log("f#[0]", a.[0], "=?=", 100)
    let print = Runtime.CompileFunction("return function(a) { console.log('js[0]', a[0], '=?=', 100); };")
    print.Call(null, a) |> ignore
    Console.End()

let testAardvarkBase() =
    Console.Begin "Aardvark.Base" 
    Console.Log("[1, 1.5] =?= ", V2d(1.0, 1.5).ToString())
    Console.End()


[<EntryPoint>]
let main _argv =
    testDynamicMethod()
    testArrayBuffer()
    testAdaptive()
    testAardvarkBase()

    Document.Body.Style.Background <- "w3-win8-cobalt"
    Document.Body.Style.Margin <- "0"
    Document.Body.Style.Padding <- "0"



    let c = Document.CreateCanvasElement()

    c.AddEventListener("pointerdown", fun e -> 
        let e = PointerEvent e
        let t = HTMLElement e.Target
        Console.Log(t.Id)
        Console.Begin("pointer")
        Console.Log("button", string e.Button)
        Console.Log("buttons", string e.Buttons)
        Console.Log("pointerId", e.PointerId)
        Console.Log("client", e.ClientX, e.ClientY)
        Console.Log("offset", e.OffsetX, e.OffsetY)
        Console.Log("page", e.PageX, e.PageY)
        Console.Log("screen", e.ScreenX, e.ScreenY)
        Console.Log("alt", e.Alt)
        Console.Log("ctrl", e.Ctrl)
        Console.Log("meta", e.Meta)
        Console.Log("shift", e.Shift)
        Console.End()
        if isNull Document.FullscreenElement then c.RequestFullscreen().ContinueWith (fun _ -> Console.Warn("done")) |> ignore
        else Document.ExitFullscreen()
    )
    //let d = c.SubscribeEventListener("pointermove", fun e -> Console.Log("mouse", e?clientX, e?clientY))
    
    //c.AddEventListener("mousedown", fun _ -> d.Dispose())

    c.Id <- "bla"
    //c.Width <- 800
    //c.Height <- 600
    c.Style.Width <- "100%"
    c.Style.Height <- "100%"
    c.ClassName <- "hans hugo"
    //c.Style.Transform <- "rotate(20deg)"


    printfn "%A" c.Style.CssText

    c.ClassList.Add "franz"

    for c in c.ClassList do
        printfn "%s" c

    Document.Body.AppendChild c
    let gl = c.GetWebGLContext()
     
    let d = Document.CreateElement "button"
    d.InnerText <- "Clicky"
    d.Style.Position <- "absolute"
    d.Style.Left <- "0"
    d.Style.Top <- "0"
    d.Style.ZIndex <- "100"
    d.OnClick.Add (fun e ->
        c.RequestFullscreen() |> ignore
        
        e.PreventDefault()
        e.StopImmediatePropagation()
    )


    c.InsertAdjacentElement(InsertMode.BeforeBegin, d)

    

    let e = Document.GetElementById "bla"
    printfn "%s" e.OuterHTML

    gl.ClearColor(0.0, 0.0, 0.0, 1.0)
    gl.Clear ClearBuffers.Color
    
    let pos = 
        Float32Array.op_Implicit (
            Span [| 
                -0.9f; -0.9f; 0.0f
                0.9f; -0.9f; 0.0f
                0.9f; 0.9f; 0.0f

                
                -0.9f; -0.9f; 0.0f
                0.9f; 0.9f; 0.0f
                -0.9f; 0.9f; 0.0f
            |]
        )

    let col = 
        Uint8Array.op_Implicit (
            Span [|
                255uy; 0uy; 0uy; 255uy
                255uy; 255uy; 255uy; 255uy
                0uy; 0uy; 255uy; 255uy
                
                255uy; 0uy; 0uy;   255uy
                0uy; 0uy; 255uy;   255uy
                0uy; 255uy; 0uy; 255uy

            |]
        )

    let pb = gl.CreateBuffer()
    gl.BindBuffer(BufferTarget.Array, pb)
    gl.BufferData(BufferTarget.Array, pos, BufferUsage.StaticDraw)
    gl.BindBuffer(BufferTarget.Array, null)
    
    let cb = gl.CreateBuffer()
    gl.BindBuffer(BufferTarget.Array, cb)
    gl.BufferData(BufferTarget.Array, col, BufferUsage.StaticDraw)
    gl.BindBuffer(BufferTarget.Array, null)

    let vsc =
        """#version 300 es
        layout(location = 0) in vec4 v_position;
        layout(location = 1) in vec4 v_color;

        out vec4 f_color;

        void main() {
            gl_Position = v_position;
            f_color = vec4(v_color.rgb, 1.0);
        }


        """

    let psc = 
        """#version 300 es
        precision highp float;
        in vec4 f_color;
        out vec4 c;

        uniform MyBuffer {
            float time;
        };

        void main() {
            float x = sin(5.0*time - dot(gl_FragCoord.xy / 100.0, normalize(vec2(1,2)))) * sin(3.0*time - dot(gl_FragCoord.xy / 50.0, normalize(vec2(-2,1))));
            float a = 0.4 * x + 0.6;
            c = vec4(f_color.rgb * a, 1.0);
        }

        """

    let vs = gl.CreateShader(ShaderType.Vertex)
    gl.ShaderSource(vs, vsc)
    gl.CompileShader(vs)
    let log = gl.GetShaderInfoLog vs
    if not (String.IsNullOrWhiteSpace log) then
        Console.Warn(log)
        
    let fs = gl.CreateShader(ShaderType.Fragment)
    gl.ShaderSource(fs, psc)
    gl.CompileShader(fs)
    let log = gl.GetShaderInfoLog fs
    if not (String.IsNullOrWhiteSpace log) then
        Console.Warn(log)

    let p = gl.CreateProgram()
    gl.AttachShader(p, vs)
    gl.AttachShader(p, fs)
    gl.LinkProgram(p)

    let log = gl.GetProgramInfoLog(p)
    if not (String.IsNullOrWhiteSpace log) then
        Console.Warn(log)
        
    let ub = gl.CreateBuffer()
    gl.BindBuffer(BufferTarget.Uniform, ub)
    gl.BufferData(BufferTarget.Uniform, Float32Array.op_Implicit (Span (Array.create 64 1.0f)), BufferUsage.DynamicDraw)
    gl.BindBuffer(BufferTarget.Uniform, null)

    let mutable cnt = 0
    let mutable lastPrint = 0.0
    let rec draw(t : float) = 
        
        let dt = t - lastPrint
        if dt > 2000.0 then
            let fps = ((1000.0 * float cnt / dt) * 100.0 |> round) / 100.0
            d.InnerHTML <- fps.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)
            //Console.Log("fps", fps.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture))
            lastPrint <- t 
            cnt <- 0

        cnt <- cnt + 1 

        let s = c.GetBoundingClientRect()
        c.Width <- int s.Size.X
        c.Height <- int s.Size.Y

        gl.Viewport(0,0,int s.Size.X, int s.Size.Y)
        gl.BindBuffer(BufferTarget.Uniform, ub)
        gl.BufferData(BufferTarget.Uniform, Float32Array.op_Implicit (Span (Array.create 64 (float32 t / 1000.0f))), BufferUsage.DynamicDraw)
        gl.BindBuffer(BufferTarget.Uniform, null)
        
        let a = HSVf(float32 (0.05 * t / 1000.0 % 1.0), 0.7f, 0.5f).ToC3f()

        gl.ClearColor(float a.R, float a.G, float a.B, 1.0)
        gl.Clear ClearBuffers.Color
    
        gl.UseProgram(p)

        gl.BindBuffer(BufferTarget.Array, pb)
        gl.EnableVertexAttribArray 0
        gl.VertexAttribPointer(0, 3, VertexAttribType.Float, false, 12, 0)

    
        gl.BindBuffer(BufferTarget.Array, cb)
        gl.EnableVertexAttribArray 1
        gl.VertexAttribPointer(1, 4, VertexAttribType.UnsignedByte, true, 4, 0)
    
        gl.BindBufferRange(BufferTarget.Uniform, 0, ub, 0, 256)

        gl.DrawArrays(PrimitiveTopology.Triangles, 0, 6)

        gl.UseProgram(null)

        Window.Reference.Invoke("requestAnimationFrame", System.Action<float>(fun dt -> draw dt)) |> ignore

    draw 0.0


    0