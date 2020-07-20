// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp
module Bla 

open System
open WebAssembly
open WebAssembly.Core
// C:\Users\Schorsch\Downloads\mono-wasm-28315c66b74>
//    packager.exe --appdir=bla --search-path=C:\Users\Schorsch\.nuget\packages\fsharp.core\4.7.0\lib\netstandard2.0 "C:\Users\Schorsch\Development\wasm\Wasm\bin\Debug\netstandard2.1\Wasm.dll" C:\Users\Schorsch\.nuget\packages\fsharp.core\4.7.0\lib\netstandard2.0\FSharp.Core.dll

[<AllowNullLiteral>]
type JsObj(r : JSObject) =
    static let unbox (o : obj) =
        match o with
        | :? JsObj as o -> o.Reference :> obj
        | _ -> o

    member x.Reference = r

    member x.Call(meth : string, a0 : 'a) =
        r.Invoke(meth, unbox a0)
        
    member x.Call(meth : string, a0 : 'a, a1 : 'b) =
        r.Invoke(meth, unbox a0, unbox a1)

    member x.Call(meth : string, a0 : 'a, a1 : 'b, a2 : 'c) =
        r.Invoke(meth, unbox a0, unbox a1, unbox a2)

    member x.Call(meth : string, a0 : 'a, a1 : 'b, a2 : 'c, a3 : 'd) =
        r.Invoke(meth, unbox a0, unbox a1, unbox a2, unbox a3)

    member x.Item
        with get(name : string) = r.GetObjectProperty(name)
        and set (name : string) (value : obj) = r.SetObjectProperty(name, value)

    member private x.Dispose(disposing : bool) =
        if disposing then GC.SuppressFinalize x
        r.Dispose()

    override x.Finalize() = x.Dispose false
    member x.Dispose() = x.Dispose true


let inline (?) (o : JsObj) (name : string) : 'a =
    if typeof<'a> = typeof<JsObj> then o.[name] |> unbox<JSObject> |> JsObj |> unbox<'a>
    else o.[name] |> unbox<'a>

let inline (?<-) (o : JsObj) (name : string) (value : 'a) =
    match value :> obj with
    | :? JsObj as v -> o.[name] <- v
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


[<AllowNullLiteral>]
type WebGLProgram(ref : JSObject) =
    inherit JsObj(ref)

[<AllowNullLiteral>]
type WebGLShader(ref : JSObject) =
    inherit JsObj(ref)

[<AllowNullLiteral>]
type WebGLBuffer(ref : JSObject) =
    inherit JsObj(ref)

[<AllowNullLiteral>]
type WebGLFramebuffer(ref : JSObject) =
    inherit JsObj(ref)

[<AllowNullLiteral>]
type WebGLRenderbuffer(ref : JSObject) =
    inherit JsObj(ref)
    
[<AllowNullLiteral>]
type WebGLTexture(ref : JSObject) =
    inherit JsObj(ref)

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

    /// The WebGLRenderingContext.createBuffer() method of the WebGL API creates and initializes a WebGLBuffer storing data such as vertices or colors.
    member x.CreateBuffer() =
        ref.Invoke("createBuffer") |> unbox<JSObject> |> WebGLBuffer

    member x.CreateShader(typ : ShaderType) =
        ref.Invoke("createShader", int typ) |> unbox<JSObject> |> WebGLShader
        
    member x.ShaderSource(shader : WebGLShader, source : string) =
        ref.Invoke("shaderSource", shader.Reference, source) |> ignore
        
    member x.CompileShader(shader : WebGLShader) =
        ref.Invoke("compileShader", shader.Reference) |> ignore
        
    member x.CreateProgram() =
        ref.Invoke("createProgram") |> unbox<JSObject> |> WebGLProgram
        
    member x.LinkProgram(program : WebGLProgram) =
        ref.Invoke("linkProgram", program.Reference) |> ignore
        
    member x.GetShaderInfoLog(shader : WebGLShader) =
        ref.Invoke("getShaderInfoLog", shader.Reference) |> unbox<string>

    member x.GetProgramInfoLog(program : WebGLProgram) =
        ref.Invoke("getProgramInfoLog", program.Reference) |> unbox<string>

    member x.UseProgram(program : WebGLProgram) =
        if isNull program then ref.Invoke("useProgram", [| null |]) |> ignore
        else ref.Invoke("useProgram", program.Reference) |> ignore
    
    member x.EnableVertexAttribArray(index : int) =
        ref.Invoke("enableVertexAttribArray", index) |> ignore

    member x.DisableVertexAttribArray(index : int) =
        ref.Invoke("disableVertexAttribArray", index) |> ignore

    member x.VertexAttribPointer(index : int, size : int, typ : VertexAttribType, normalized : bool, stride : int, offset : int) =
        ref.Invoke("vertexAttribPointer", index, size, int typ, normalized, stride, offset) |> ignore

    member x.DrawArrays(mode : PrimitiveTopology, first : int, count : int) =
        ref.Invoke("drawArrays", int mode, first, count) |> ignore

    member x.ClearColor(r : float, g : float, b : float, a : float) =
        ref.Invoke("clearColor", r, g, b, a) |> ignore

    member x.Clear(flags : ClearBuffers) =
        ref.Invoke("clear", int flags) |> ignore



type CSSStyleDeclaration(r : JSObject) =
    inherit JsObj(r)


    member x.Width
        with get() : string = r.GetObjectProperty("width") |> unbox<string>
        and set (v : string) = r.SetObjectProperty("width", v)

    member x.Height
        with get() : string = r.GetObjectProperty("height") |> unbox<string>
        and set (v : string) = r.SetObjectProperty("height", v)

    member x.BackgroundColor
        with get() : string = r.GetObjectProperty("background-color") |> unbox<string>
        and set (v : string) = r.SetObjectProperty("background-color", v)


type HTMLElement(r : JSObject) =
    inherit JsObj(r)


    
    /// Is a DOMString representing the id of the element.
    member x.Id
        with get() = r.GetObjectProperty("id") |> unbox<string>
        and set (id : string) = r.SetObjectProperty("id", id)

    /// Is a DOMString representing the class of the element.
    member x.Class
        with get() = r.GetObjectProperty("className") |> unbox<string>
        and set (id : string) = r.SetObjectProperty("className", id)
        
    /// Returns a Number representing the inner height of the element.
    member x.ClientHeight = r.GetObjectProperty("clientHeight") |> unbox<float>
    /// Returns a Number representing the inner width of the element.
    member x.ClientWidth = r.GetObjectProperty("clientWidth") |> unbox<float>
    /// Returns a Number representing the width of the left border of the element.
    member x.ClientLeft = r.GetObjectProperty("clientLeft") |> unbox<float>
    /// Returns a Number representing the width of the top border of the element.
    member x.ClientTop = r.GetObjectProperty("clientTop") |> unbox<float>

    
    
    /// Is a DOMString representing the markup of the element's content.
    member x.InnerHTML
        with get() = r.GetObjectProperty("innerHTML") |> unbox<string>
        and set (id : string) = r.SetObjectProperty("innerHTML", id)
        
    /// A DOMString representing the local part of the qualified name of the element.
    member x.LocalName = r.GetObjectProperty("localName") |> unbox<string>


    /// Returns a String containing the element's assigned access key.
    member x.AccessKeyLabel = r.GetObjectProperty("accessKeyLabel") |> unbox<string>
    /// Returns a String containing the element's assigned access key.
    member x.NamespaceURI = r.GetObjectProperty("namespaceURI") |> unbox<string>

    
    /// Is a DOMString representing the markup of the element's content.
    member x.OuterHTML
        with get() = r.GetObjectProperty("outerHTML") |> unbox<string>
        and set (id : string) = r.SetObjectProperty("outerHTML", id)
        
        
    /// Returns a String with the name of the tag for the given element.
    member x.TagName = r.GetObjectProperty("tagName") |> unbox<string>

    /// Is a String, where a value of true means the element is editable and a value of false means it isn't.
    member x.ContentEditable 
        with get() = r.GetObjectProperty("contentEditable") |> unbox<string>
        and set (v : string) = r.SetObjectProperty("contentEditable", v)

    /// Returns a Boolean that indicates whether or not the content of the element can be edited.
    member x.IsContentEditable = r.GetObjectProperty("isContentEditable") |> unbox<bool>
    
    /// Is a String, reflecting the dir global attribute, representing the directionality of the element. Possible values are "ltr", "rtl", and "auto".
    member x.Dir 
        with get() = r.GetObjectProperty("dir") |> unbox<string>
        and set (v : string) = r.SetObjectProperty("dir", v)
        
    /// Is a Boolean indicating if the element is hidden or not.
    member x.Hidden 
        with get() = r.GetObjectProperty("hidden") |> unbox<bool>
        and set (v : bool) = r.SetObjectProperty("hidden", v)
        
    /// Is a Boolean indicating whether the user agent must act as though the given node is absent for the purposes of user interaction events, in-page text searches ("find in page"), and text selection.
    member x.Inert 
        with get() = r.GetObjectProperty("inert") |> unbox<bool>
        and set (v : bool) = r.SetObjectProperty("inert", v)

    /// Represents the "rendered" text content of a node and its descendants. As a getter, it approximates the text the user would get if they highlighted the contents of the element with the cursor and then copied it to the clipboard.
    member x.InnerText
        with get() = r.GetObjectProperty("innerText") |> unbox<string>
        and set (v : string) = r.SetObjectProperty("innerText", v)

    /// Is a String representing the language of an element's attributes, text, and element contents.
    member x.Lang
        with get() = r.GetObjectProperty("lang") |> unbox<string>
        and set (v : string) = r.SetObjectProperty("lang", v)

    /// Returns the cryptographic number used once that is used by Content Security Policy to determine whether a given fetch will be allowed to proceed.
    member x.Nonce
        with get() = r.GetObjectProperty("nonce") |> unbox<int>
        and set (v : int) = r.SetObjectProperty("nonce", v)
        

        
    /// Returns a double, the distance from this element's left border to its offsetParent's left border.
    member x.OffsetLeft = r.GetObjectProperty("offsetLeft") |> unbox<float>
    /// Returns a double, the distance from this element's top border to its offsetParent's top border.
    member x.OffsetTop = r.GetObjectProperty("offsetTop") |> unbox<float>
    /// Returns a double containing the width of an element, relative to the layout.
    member x.OffsetWidth = r.GetObjectProperty("offsetWidth") |> unbox<float>
    /// Returns a double containing the height of an element, relative to the layout.
    member x.OffsetHeight = r.GetObjectProperty("offsetHeight") |> unbox<float>

    /// Is a CSSStyleDeclaration, an object representing the declarations of an element's style attributes.
    member x.Style =
        r.GetObjectProperty("style") |> unbox<JSObject> |> CSSStyleDeclaration

    /// Is a long representing the position of the element in the tabbing order.
    member x.TabIndex
        with get() = r.GetObjectProperty("tabIndex") |> unbox<int>
        and set (id : int) = r.SetObjectProperty("tabIndex", id)
        
    /// Is a String containing the text that appears in a popup box when mouse is over the element.
    member x.Title
        with get() = r.GetObjectProperty("title") |> unbox<string>
        and set (id : string) = r.SetObjectProperty("title", id)

    member x.AppendChild(e : HTMLElement) =
        r.Invoke("appendChild", e.Reference) |> ignore
        

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


type HTMLDocument(r : JSObject) =
    inherit JsObj(r)

    member x.Body = r.GetObjectProperty("body") |> unbox<JSObject> |> HTMLElement

    member x.CreateElement(tagName : string) = r.Invoke("createElement", tagName) |> unbox<JSObject> |> HTMLElement
    
    member x.CreateCanvasElement() = r.Invoke("createElement", "canvas") |> unbox<JSObject> |> HTMLCanvasElement


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
    

[<AutoOpen>]
module RuntimValues = 
    let Window = JsObj (unbox (Runtime.GetGlobalObject "window"))
    let Document = HTMLDocument (unbox (Runtime.GetGlobalObject "document"))
    let Console = Console (unbox (Runtime.GetGlobalObject "console"))



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

    let print = Runtime.CompileFunction("return function(a) { console.log('js[0]', a[0], '=?=', 100); };")
    let f = Runtime.CompileFunction("return function(a) { a[0] = 100; };")
    f.Call(null, a) |> ignore

    Console.Log("f#[0]", a.[0], "=?=", 100)
    print.Call(null, a) |> ignore
    Console.End()

open Aardvark.Base

[<EntryPoint>]
let main _argv =
    testDynamicMethod()
    testArrayBuffer()
    testAdaptive()

    Console.Log(V2d(1,2).ToString())

    Aardvark.Base.Telemetry.ResetTelemetrySystem()
    let p = Aardvark.Base.Telemetry.CpuTime()
    let c = Document.CreateCanvasElement()
    c.Id <- "bla"
    c.Style.BackgroundColor <- "red"
    c.Width <- 400
    c.Height <- 300
    c.Class <- "hans sepp"
    Document.Body.AppendChild c
    Console.Warn(p.ValueDouble)
    let gl = c.GetWebGLContext()

    gl.ClearColor(0.0, 0.0, 0.0, 1.0)
    gl.Clear ClearBuffers.Color


    let pos = Float32Array.op_Implicit (Span [| 0.0f; 0.0f; 0.0f; 1.0f; 0.0f; 0.0f; 1.0f; 1.0f; 0.0f|])
    let col = Uint8Array.op_Implicit (Span [|255uy; 0uy; 0uy; 255uy; 0uy; 255uy; 0uy; 255uy; 0uy; 0uy; 255uy; 255uy|])

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
            f_color = v_color;
        }


        """

    let psc = 
        """#version 300 es
        precision highp float;
        in vec4 f_color;
        out vec4 c;

        void main() {
            c = f_color;
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


    gl.UseProgram(p)

    gl.BindBuffer(BufferTarget.Array, pb)
    gl.EnableVertexAttribArray 0
    gl.VertexAttribPointer(0, 3, VertexAttribType.Float, false, 12, 0)

    
    gl.BindBuffer(BufferTarget.Array, cb)
    gl.EnableVertexAttribArray 1
    gl.VertexAttribPointer(1, 4, VertexAttribType.UnsignedByte, true, 4, 0)
    

    gl.DrawArrays(PrimitiveTopology.Triangles, 0, 3)

    gl.UseProgram(null)




    0