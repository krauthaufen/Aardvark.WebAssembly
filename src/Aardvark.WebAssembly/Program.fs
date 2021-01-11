// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp
module Bla 

open System
open WebAssembly
open WebAssembly.Core
open Aardvark.Base
open Aardvark.WebAssembly
open WebGPU
open System.Runtime.CompilerServices
open Microsoft.FSharp.NativeInterop

#nowarn "9"

let inline (?) (o : JsObj) (name : string) : 'a =
    if typeof<'a> = typeof<JsObj> then o.[name] |> unbox<JSObject> |> JsObj |> unbox<'a>
    else o.[name] |> unbox<'a>

let inline (?<-) (o : JsObj) (name : string) (value : 'a) =
    match value :> obj with
    | :? JsObj as v -> o.[name] <- v.Reference
    | _ -> o.[name] <- value



open FSharp.Data.Adaptive
open System.Reflection.Emit
open System.Reflection
 
let testDynamicMethod() =
    Console.BeginCollapsed "DynamicMethod"
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
    Console.BeginCollapsed "Adaptive"
    let a = cval 10
    let b = a |> AVal.map ((+) 5) 
    printfn "%A =?= 15" (AVal.force b)
    transact (fun () -> a.Value <- 5)
    printfn "%A =?= 10" (AVal.force b)

    Console.End()

let testArrayBuffer() =
    Console.BeginCollapsed "ArrayBuffer" 
    let data = Array.init 1024 byte
    use a = Uint8Array.op_Implicit(Span data)
     
    use f = Runtime.CompileFunction("return function(a) { a[0] = 100; };")
    f.Call(null, a) |> ignore
      
    Console.Log("f#[0]", a.[0], "=?=", 100)
    let print = Runtime.CompileFunction("return function(a) { console.log('js[0]', a[0], '=?=', 100); };")
    print.Call(null, a) |> ignore
    Console.End()

let testAardvarkBase() =
    Console.BeginCollapsed "Aardvark.Base" 
    Console.Log("[1, 1.5] =?= ", V2d(1.0, 1.5).ToString())
    Console.End()

type MyDelegate = delegate of int * string * obj -> unit

let testCallback() =
    
    Console.BeginCollapsed "Callback" 
    let cb = MyDelegate(fun a b c -> Console.Log(sprintf "cb: %A %A %A" a b c))

    use f = Runtime.CompileFunction("return function(a) { a(10, \"hi callback\", { field : 100 }); };")
    f.Call(null, cb) |> ignore
    Console.End()

module Shader =
    open FShade

    //type ColorAttribute() = inherit SemanticAttribute("gl_FragColor")

    type Vertex =
        {
            [<Position>] p : V4d
            [<Color>] c : V4d
        }

    let vertex (v : Vertex) =
        vertex {
            return v
        }

    let frag (v : Vertex) =
        fragment {
            return V4d(v.c.XYZ * (0.5 + 0.5 * sin(uniform?time) ** 2.0), 1.0)
        }


    open FShade.GLSL

    let cfg : EffectConfig =
        {
            depthRange = Range1d(-1.0, 1.0)
            flipHandedness = false
            lastStage = ShaderStage.Fragment
            outputs = Map.ofList ["Colors", (typeof<V4d>, 0)]
        }

    let backend300 =
        Backend.Create {
            bindingMode = BindingMode.None
            version = GLSLVersion(3,0,0, "es")
            enabledExtensions = Set.empty
            createUniformBuffers = true
            createDescriptorSets = false
            stepDescriptorSets = false
            createInputLocations = true
            createPassingLocations = false
            createOutputLocations = false
            useInOut = true
            depthWriteMode = false
            createPerStageUniforms = false
            reverseMatrixLogic = true
        }
    let backend100 =
        Backend.Create {
            bindingMode = BindingMode.None
            version = GLSLVersion(1,0,0)
            enabledExtensions = Set.empty
            createUniformBuffers = false
            createDescriptorSets = false
            stepDescriptorSets = false
            createInputLocations = false
            createPassingLocations = false
            createOutputLocations = false
            useInOut = false
            depthWriteMode = false
            createPerStageUniforms = false
            reverseMatrixLogic = true
        }

    let compile300 (outputs : Map<string, Type * int>) (e : Effect) =
        let glsl = 
            e 
            |> Effect.toModule { cfg with outputs = outputs}
            |> ModuleCompiler.compileGLSL backend300
        glsl.code
        
    let compile100 (outputs : Map<string, Type * int>) (e : Effect) =
        if Map.remove "Colors" outputs |> Map.isEmpty |> not then
            failwith "cannot render to multiple outputs in WebGL 1.0"

        let glsl = 
            e 
            |> Effect.toModule { cfg with outputs = outputs}
            |> ModuleCompiler.compileGLSL backend100
        glsl.code
            .Replace("varying vec4 ColorsOut;", "")
            .Replace("ColorsOut", "gl_FragColor")





    let test(ctx : WebGLRenderingContext) =
        let outputs = Map.ofList ["Colors", (typeof<V4d>, 0)]
        let compile = if ctx.IsWebGL2 then compile300 else compile100
        compile outputs (
            Effect.compose [
                Effect.ofFunction vertex
                Effect.ofFunction frag
            ]
        )


module FFT = 
    open System.Runtime.InteropServices
    open Microsoft.FSharp.NativeInterop

    let naiveBitReverse (n : int) (bits : int) =
        let mutable n = n
        let mutable reversedN = n;
        let mutable count = bits - 1;
 
        n <- n >>> 1;
        while (n > 0) do
            reversedN <- (reversedN <<< 1) ||| (n &&& 1);
            count <- count - 1
            n <- n >>> 1;
 
        ((reversedN <<< count) &&& ((1 <<< bits) - 1));

    let bitReverse (n : int) (bits : int) =
        let mutable v = n
        
        v <- ((v >>> 1) &&& 0x55555555) ||| ((v &&& 0x55555555) <<< 1)
        v <- ((v >>> 2) &&& 0x33333333) ||| ((v &&& 0x33333333) <<< 2)
        v <- ((v >>> 4) &&& 0x0F0F0F0F) ||| ((v &&& 0x0F0F0F0F) <<< 4)
        v <- ((v >>> 8) &&& 0x00FF00FF) ||| ((v &&& 0x00FF00FF) <<< 8)
        v <-  (v >>> 16)                |||  (v <<< 16)

        v >>> (32 - bits)

    let internal fftInPlace (data : ComplexD[]) =
        let cnt = data.Length
        let gc = GCHandle.Alloc(data, GCHandleType.Pinned)
        try
            let ptr = NativePtr.ofNativeInt<ComplexD> (gc.AddrOfPinnedObject())
            let mutable temp = Unchecked.defaultof<ComplexD>

            let bits = Fun.Log2Int cnt
            for j in 1 .. cnt - 1 do
                let swapPos = bitReverse j bits
                if swapPos > j then
                    temp <- NativePtr.get ptr j
                    NativePtr.set ptr j (NativePtr.get ptr swapPos)
                    NativePtr.set ptr swapPos temp


            let mutable N = 2
            while N <= cnt do
                let Nh = N / 2
                let mutable i = 0
                while i < cnt do
                    let mutable k = 0
                    while k < Nh do
                        let evenIdx = i + k
                        let oddIdx = evenIdx + Nh

                        let even = NativePtr.get ptr evenIdx //data.[evenIdx]
                        let odd = NativePtr.get ptr oddIdx //data.[oddIdx]
                        let phi = -Constant.PiTimesTwo * float k / float N
                        let exp = ComplexD(cos phi, sin phi) * odd

                        NativePtr.set ptr evenIdx (even + exp)
                        NativePtr.set ptr oddIdx (even - exp)
                        //data.[evenIdx] <- even + exp
                        //data.[oddIdx] <- even - exp
                        k <- k + 1
                    i <- i + N
                N <- N <<< 1

        finally
            gc.Free()


    let rec fftrec (a : ComplexD[]) (a0 : int) (n : int) (step : int) =
        if step < n then
            let s2 = step <<< 1
            fftrec a a0 n s2
            fftrec a (a0 + step) n s2
            

            let mutable i = 0
            while i < n do
                let angle = -Constant.Pi * float i / float n

                let t = ComplexD(cos angle, sin angle) * a.[a0 + i + step]
                a.[a0 + i / 2] <- a.[a0 + i] + t
                a.[a0 + (i + n) / 2] <- a.[a0 + i] - t
                i <- i + s2

    let fft (data : ComplexD[]) =

        let twiddle =
            let mutable w = 0
            let arr = Array.zeroCreate data.Length
            let mutable N = 2
            while N <= data.Length do
                for i in 0 .. N/2 - 1 do
                    let angle = Constant.PiTimesTwo * float i / float data.Length
                    arr.[w] <- ComplexD(cos angle, sin angle)
                    w <- w + 1
                N <- N <<< 1
            arr

        let x = Array.copy data

        let power = Fun.Log2Int data.Length

        for i in 0 .. x.Length - 1 do
            let mutable v = i  
            v <- ((v >>> 1) &&& 0x55555555) ||| ((v &&& 0x55555555) <<< 1)
            v <- ((v >>> 2) &&& 0x33333333) ||| ((v &&& 0x33333333) <<< 2)
            v <- ((v >>> 4) &&& 0x0F0F0F0F) ||| ((v &&& 0x0F0F0F0F) <<< 4)
            v <- ((v >>> 8) &&& 0x00FF00FF) ||| ((v &&& 0x00FF00FF) <<< 8)
            v <-  (v >>> 16)                |||  (v <<< 16)
            let y = v >>> (32 - power)

            if y > i then Fun.Swap(&x.[i], &x.[y])

        //let twiddle : ComplexD[] = failwith ""

        let mutable n = 1
        while n < data.Length do

            let mutable k = 0
            let mutable l = k+n
            let mutable w = n - 1
            while k < data.Length do
                let f = twiddle.[w]
                let y = x.[k] - f*x.[l]
                x.[k] <- x.[k] + f*x.[l]
                x.[l] <- y

                k <- k + 1
                l <- l + 1
                if k % n = 0 then
                    k <- k + n
                    l <- l + n
                    w <- n - 1
                else
                    w <- w + 1

            n <- n <<< 1

        x

        
    let ifft (data : ComplexD[]) =
        let n = data.Length
        let a = fft data
        for i in 0 .. n - 1 do a.[i] <- a.[i].Conjugated / float n
        a

    //let fftold (data : ComplexD[]) =
    //    let n2 = Fun.NextPowerOfTwo data.Length
    //    let res = Array.zeroCreate n2
        
    //    Span(data).CopyTo(Span(res, 0, data.Length))
    //    if n2 > data.Length then 
    //        let r = n2 - data.Length
    //        Span(data, 0, r).CopyTo(Span(res, data.Length, r))

    //    printfn "%A" res
    //    fftInPlace res

    //    let f = 1.0 / float n2
    //    for i in 0 .. res.Length - 1 do
    //        res.[i] <- res.[i] * f

    //    res

    //let ifft (data : ComplexD[]) =
    //    let n2 = Fun.NextPowerOfTwo data.Length
    //    let res = Array.zeroCreate n2
        
    //    Span(data).CopyTo(Span(res, 0, data.Length))
    //    if n2 > data.Length then 
    //        let r = n2 - data.Length
    //        Span(data, 0, r).CopyTo(Span(res, data.Length, r))

    //    fftInPlace res
    //    res



    
    
    let test() =
        let arr =
            Array.init 8 (fun i ->
                if i < 4 then ComplexD.One
                else ComplexD.Zero
                //sin (Constant.PiQuarter * float i) |> ComplexD
            
            )
        let res = fft arr

        Console.Begin "fft"
        for a in res do
            printfn "%A" a
        Console.End()

        
        Console.Begin "ifft"
        let bla = ifft res
        for (a,b) in Array.zip arr bla do
            printfn "%A %A" a b
        Console.End()
        
open System.Threading.Tasks

[<AbstractClass; Sealed; Extension>]
type TypedArray private() =
    
    // ===============================================================================
    // TypedArray.CopyTo('a[])
    // ===============================================================================
    [<Extension>]
    static member CopyTo(src : TypedArray<_,'a>, dst : 'a[], startIndex : int) =
        if startIndex < 0 then raise <| ArgumentException("startIndex")
        if src.Length > dst.Length - startIndex then raise <| IndexOutOfRangeException()
        src.CopyTo(Span(dst, startIndex, src.Length)) |> ignore
        
    [<Extension>]
    static member CopyTo(src : TypedArray<_,'a>, dst : 'a[]) =
        if src.Length > dst.Length then raise <| IndexOutOfRangeException()
        src.CopyTo(Span dst) |> ignore
        
    // ===============================================================================
    // 'a[].CopyTo(TypedArray)
    // ===============================================================================
    [<Extension>]
    static member CopyTo(src : 'a[], dst : TypedArray<_, 'a>, startIndex : int) =
        if startIndex < 0 then raise <| ArgumentException("startIndex")
        if src.Length > dst.Length - startIndex then raise <| IndexOutOfRangeException()
        dst.CopyFrom(ReadOnlySpan(src, startIndex, dst.Length)) |> ignore

    [<Extension>]
    static member CopyTo(src : 'a[], dst : TypedArray<_, 'a>) =
        if src.Length > dst.Length then raise <| IndexOutOfRangeException()
        dst.CopyFrom(ReadOnlySpan(src)) |> ignore


    // ===============================================================================
    // 'a[].CopyTo(ArrayBuffer)
    // ===============================================================================
    [<Extension>]
    static member CopyTo<'a when 'a : unmanaged>(src : 'a[], dst : ArrayBuffer, byteOffset : int) =
        if byteOffset < 0 then raise <| ArgumentException("byteOffset")
        let srcSize = sizeof<'a> * src.Length
        if srcSize > dst.ByteLength - byteOffset then raise <| IndexOutOfRangeException()

        use ptr = fixed src
        use dst = new Uint8Array(dst, byteOffset)
        let src = ReadOnlySpan<byte>(NativePtr.toVoidPtr ptr, srcSize)
        dst.CopyFrom(src) |> ignore

    [<Extension>]
    static member CopyTo<'a when 'a : unmanaged>(src : 'a[], dst : ArrayBuffer) =
        let srcSize = sizeof<'a> * src.Length
        if srcSize > dst.ByteLength then raise <| IndexOutOfRangeException()

        use ptr = fixed src
        use dst = new Uint8Array(dst)
        let src = ReadOnlySpan<byte>(NativePtr.toVoidPtr ptr, srcSize)
        dst.CopyFrom(src) |> ignore

    // ===============================================================================
    // ArrayBuffer.CopyTo('a[])
    // ===============================================================================
    [<Extension>]
    static member CopyTo<'a when 'a : unmanaged>(src : ArrayBuffer, dst : 'a[], startIndex : int) =
        if startIndex < 0 then raise <| ArgumentException("startIndex")
        if src.Length > dst.Length - startIndex then raise <| IndexOutOfRangeException()
        use src = new Uint8Array(src)
        use pDst = fixed dst
        src.CopyTo(Span<byte>(NativePtr.toVoidPtr (NativePtr.add pDst startIndex), src.Length))
        
    [<Extension>]
    static member CopyTo<'a when 'a : unmanaged>(src : ArrayBuffer, dst : 'a[]) =
        if src.Length > dst.Length then raise <| IndexOutOfRangeException()
        use src = new Uint8Array(src)
        use pDst = fixed dst
        src.CopyTo(Span<byte>(NativePtr.toVoidPtr pDst, src.Length))

        

    static member ofArray(arr : uint8[]) = Uint8Array.op_Implicit (Span arr)
    static member ofArray(arr : int8[]) = Int8Array.op_Implicit (Span arr)
    static member ofArray(arr : uint16[]) = Uint16Array.op_Implicit (Span arr)
    static member ofArray(arr : int16[]) = Int16Array.op_Implicit (Span arr)
    static member ofArray(arr : uint32[]) = Uint32Array.op_Implicit (Span arr)
    static member ofArray(arr : int32[]) = Int32Array.op_Implicit (Span arr)
    static member ofArray(arr : float32[]) = Float32Array.op_Implicit (Span arr)
    static member ofArray(arr : float[]) = Float64Array.op_Implicit (Span arr)

    static member toArray (arr : Uint8Array) =
        let res = Array.zeroCreate arr.Length
        arr.CopyTo res
        res
    static member toArray (arr : Uint8ClampedArray) =
        let res = Array.zeroCreate arr.Length
        arr.CopyTo res
        res
    static member toArray (arr : Int8Array) =
        let res = Array.zeroCreate arr.Length
        arr.CopyTo res
        res
    static member toArray (arr : Uint16Array) =
        let res = Array.zeroCreate arr.Length
        arr.CopyTo res
        res
    static member toArray (arr : Int16Array) =
        let res = Array.zeroCreate arr.Length
        arr.CopyTo res
        res
    static member toArray (arr : Uint32Array) =
        let res = Array.zeroCreate arr.Length
        arr.CopyTo res
        res
    static member toArray (arr : Int32Array) =
        let res = Array.zeroCreate arr.Length
        arr.CopyTo res
        res
    static member toArray (arr : Float32Array) =
        let res = Array.zeroCreate arr.Length
        arr.CopyTo res
        res
    static member toArray (arr : Float64Array) =
        let res = Array.zeroCreate arr.Length
        arr.CopyTo res
        res

    static member ArrayBufferToArray(buffer : ArrayBuffer, _ : uint8[]) =
        use src = new Uint8Array(buffer)
        TypedArray.toArray src
        
    static member ArrayBufferToArray(buffer : ArrayBuffer, _ : int8[]) =
        use src = new Int8Array(buffer)
        TypedArray.toArray src

    static member ArrayBufferToArray(buffer : ArrayBuffer, _ : uint16[]) =
        use src = new Uint16Array(buffer)
        TypedArray.toArray src
        
    static member ArrayBufferToArray(buffer : ArrayBuffer, _ : int16[]) =
        use src = new Int16Array(buffer)
        TypedArray.toArray src

    static member ArrayBufferToArray(buffer : ArrayBuffer, _ : uint32[]) =
        use src = new Uint32Array(buffer)
        TypedArray.toArray src
        
    static member ArrayBufferToArray(buffer : ArrayBuffer, _ : int32[]) =
        use src = new Int32Array(buffer)
        TypedArray.toArray src
        
    static member ArrayBufferToArray(buffer : ArrayBuffer, _ : float32[]) =
        use src = new Float32Array(buffer)
        TypedArray.toArray src
        
    static member ArrayBufferToArray(buffer : ArrayBuffer, _ : float[]) =
        use src = new Float64Array(buffer)
        TypedArray.toArray src
    static member inline toArrayAux (d : ^d, a : ArrayBuffer) : ^a[] =
        ((^a or ^d) : (static member ArrayBufferToArray : ArrayBuffer * ^a[] -> ^a[]) (a, null))

    static member inline toArray a = TypedArray.toArrayAux(Unchecked.defaultof<TypedArray>, a)
    
    [<Extension>]
    static member inline ToArray a = TypedArray.toArrayAux(Unchecked.defaultof<TypedArray>, a)





type Device with   

    member x.GetTempWriteBuffer(size : int) =
        let cap = Fun.NextPowerOfTwo size |> max 4096
        let tempName = sprintf "__write%d" cap
        let bb = x.Handle.Reference.GetObjectProperty tempName
        if isNull bb then
            let bb = x.CreateBuffer { Label = null; Size = uint64 cap; Usage = BufferUsage.CopySrc ||| BufferUsage.MapWrite; MappedAtCreation = false }
            x.Handle.Reference.SetObjectProperty(tempName, bb.Handle.Reference)
            bb
        else
            let b = bb |> convert<BufferHandle>
            new Buffer(x, b)

    member x.CreateBuffer (usage : BufferUsage, data : 'a[]) =
        async {
            let sa = sizeof<'a> 
            let byteSize = sa * data.Length
        
            let temp = x.GetTempWriteBuffer byteSize
            let real = 
                x.CreateBuffer {
                    Label = null
                    Size = uint64 byteSize
                    Usage = usage ||| BufferUsage.CopyDst
                    MappedAtCreation = false
                }

            let! ptr = temp.Map(MapMode.Write, 0un, unativeint byteSize)
            data.CopyTo(ptr, 0)
            temp.Unmap()

            let queue = x.GetDefaultQueue()
            let cmd = x.CreateCommandEncoder()
            cmd.CopyBufferToBuffer(temp, 0UL, real, 0UL, uint64 byteSize)
            queue.Submit [| cmd.Finish() |]

            return real
        }

    member x.Upload(dst : Buffer, data : 'a[]) =
        async {
            let sa = sizeof<'a> 
            let byteSize = sa * data.Length
        
            let temp = x.GetTempWriteBuffer byteSize

            let! ptr = temp.Map(MapMode.Write, 0un, unativeint byteSize)
            data.CopyTo(ptr, 0)
            temp.Unmap()

            let queue = x.GetDefaultQueue()
            let cmd = x.CreateCommandEncoder()
            cmd.CopyBufferToBuffer(temp, 0UL, dst, 0UL, uint64 byteSize)
            queue.Submit [| cmd.Finish() |]

        }



let testWebGPU() =
    async {
        Console.Begin "Buffer Roundtrip"

        let! gpu = Navigator.GPU.RequestAdapter()
        Console.Log("got Adapter")
        let! dev = gpu.RequestDevice()
        Console.Log("got Device")
        let queue = dev.GetDefaultQueue()
        
        let canvas = Document.CreateCanvasElement()
        canvas.Style.Width <- "100%"
        canvas.Style.Height <- "100%"
        canvas.Style.Background <- "red"
        canvas.Width <- 1024
        canvas.Height <- 768
        Document.Body.AppendChild canvas
        Console.Log "created Canvas"

        let ctx = canvas.GetGPUPresentContext()
        let! fmt = ctx.GetSwapChainPreferredFormat(gpu)
        let depthFormat = TextureFormat.Depth24PlusStencil8
        let samples = 1

        Console.Log "got Context"
        let swap = 
            ctx.ConfigureSwapChain {
                Device = dev
                Usage = TextureUsage.CopyDst ||| TextureUsage.OutputAttachment
                Format = fmt
            }
        Console.Log "got SwapChain"

        let! vertex = 
            dev.CreateGLSLShaderModule(ShaderStage.Vertex, ["Vertex"],
                """#version 450

                layout(set = 0, binding = 0) uniform PerBla {
                    mat4 trafo;
                };

                layout(location = 0) in vec4 p;
                layout(location = 1) in vec4 c;
                layout(location = 0) out vec4 fs_color; 
                void main() {
                    fs_color = c;
                    gl_Position = p * trafo;
                }   
                """
            )
            
        let! fragment = 
            dev.CreateGLSLShaderModule(ShaderStage.Fragment, ["Fragment"],
                """#version 450
                layout(location = 0) in vec4 fs_color;
                layout(location = 0) out vec4 color;
                void main() {
                    color = fs_color;
                }   
                """
            )


        let bindGroupLayout =
            dev.CreateBindGroupLayout {
                Label = null
                Entries =
                    [|
                        {
                            BindGroupLayoutEntry.Binding = 0
                            BindGroupLayoutEntry.HasDynamicOffset = false
                            BindGroupLayoutEntry.MinBufferBindingSize = 64UL
                            BindGroupLayoutEntry.Multisampled = false
                            BindGroupLayoutEntry.StorageTextureFormat = TextureFormat.Undefined
                            BindGroupLayoutEntry.TextureComponentType = TextureComponentType.Float
                            BindGroupLayoutEntry.ViewDimension = TextureViewDimension.Undefined
                            BindGroupLayoutEntry.Visibility = ShaderStage.Vertex ||| ShaderStage.Fragment
                            BindGroupLayoutEntry.Type = BindingType.UniformBuffer
                        }
                    |]
            }

        let layout =
            dev.CreatePipelineLayout {
                Label = null
                BindGroupLayouts = 
                    [|
                        bindGroupLayout
                    |]
            }

        let! ub = dev.CreateBuffer(BufferUsage.Uniform, [| M44f.Identity |])

        //do! dev.Upload(ub, [| M44f.Identity |])

        let bindGroup = 
            dev.CreateBindGroup {
                Label = null
                Layout = bindGroupLayout
                Entries =
                    [|
                        {
                            Binding = 0
                            Resource = BindingResource.Buffer(ub, 0UL, 64UL)
                        }
                    |]
            }

        let pipeline = 
            dev.CreateRenderPipeline {
                Label = null
                Layout = layout
                VertexStage = 
                    {
                        Module = vertex
                        EntryPoint = "main"
                    }
                FragmentStage =
                    Some {
                        Module = fragment
                        EntryPoint = "main"
                    }
                VertexState =
                    Some {
                        IndexFormat = IndexFormat.Undefined
                        VertexBuffers  =
                            [|
                                {
                                    ArrayStride = 12UL
                                    Attributes = 
                                        [|
                                            {
                                                Format = VertexFormat.Float3
                                                Offset = 0UL
                                                ShaderLocation = 0
                                            }
                                        |]
                                    StepMode = InputStepMode.Vertex
                                }
                                
                                {
                                    ArrayStride = 4UL
                                    Attributes = 
                                        [|
                                            {
                                                Format = VertexFormat.UChar4Norm
                                                Offset = 0UL
                                                ShaderLocation = 1
                                            }
                                        |]
                                    StepMode = InputStepMode.Vertex
                                }
                            |]
                    }
                PrimitiveTopology = WebGPU.PrimitiveTopology.TriangleList
                RasterizationState = Some RasterizationStateDescriptor.Default
                    //Some {
                    //    CullMode = CullMode.None
                    //    DepthBias = 0
                    //    DepthBiasClamp = 1.0f
                    //    DepthBiasSlopeScale = 0.0f
                    //    FrontFace = FrontFace.CCW
                    //}
                SampleCount = samples
                DepthStencilState =
                    Some {
                        DepthCompare = CompareFunction.Always
                        DepthWriteEnabled = true
                        Format = depthFormat
                        StencilBack = StencilStateFaceDescriptor.Default
                        StencilFront = StencilStateFaceDescriptor.Default
                        StencilReadMask = 0
                        StencilWriteMask = 0
                    }

                ColorStates =
                    [|
                        { 
                            Format = fmt
                            AlphaBlend = BlendDescriptor.Default
                            WriteMask = ColorWriteMask.All
                            ColorBlend = BlendDescriptor.Default
                        }
                    |]

                SampleMask = 0xFF
                AlphaToCoverageEnabled = false

            }
            
        let! positions = 
            dev.CreateBuffer(BufferUsage.Vertex, [| -1.0f; -1.0f; 0.0f; 1.0f; -1.0f; 0.0f; 1.0f; 1.0f; 1.0f|])
     
        let! colors = 
            dev.CreateBuffer(BufferUsage.Vertex, [| 255uy;0uy;0uy;255uy; 0uy;255uy;0uy;255uy; 0uy;0uy;255uy;255uy |])


        Console.Log "configured SwapChain"

        let mutable depth = 
            let size = V2i(canvas.Width, canvas.Height)
            dev.CreateTexture {
                Label = null
                Usage = TextureUsage.OutputAttachment
                Dimension = TextureDimension.D2D
                Size = { Width = size.X; Height = size.Y; Depth = 1 }
                Format = depthFormat
                MipLevelCount = 1
                SampleCount = samples
            }
            
        let mutable color = 
            if samples > 1 then
                let size = V2i(canvas.Width, canvas.Height)
                dev.CreateTexture {
                    Label = null
                    Usage = TextureUsage.CopySrc ||| TextureUsage.OutputAttachment
                    Dimension = TextureDimension.D2D
                    Size = { Width = size.X; Height = size.Y; Depth = 1 }
                    Format = fmt
                    MipLevelCount = 1
                    SampleCount = samples
                }
            else
                null
        
        let mutable swap = swap
        let currentSize = ref (V2i(canvas.Width, canvas.Height))
        let currentSamples = ref samples
        let fixSize() =
            let s = canvas.GetBoundingClientRect().Size + V2d.II |> round |> V2i
            if s <> !currentSize || samples <> !currentSamples then
                currentSize := s
                currentSamples := samples
                canvas.Width <- s.X
                canvas.Height <- s.Y

                depth.Destroy()
                depth <- 
                    dev.CreateTexture {
                        Label = null
                        Usage = TextureUsage.OutputAttachment
                        Dimension = TextureDimension.D2D
                        Size = { Width = s.X; Height = s.Y; Depth = 1 }
                        Format = depthFormat
                        MipLevelCount = 1
                        SampleCount = samples
                    }

                color <-
                    if samples > 1 then
                        color.Destroy()
                        dev.CreateTexture {
                            Label = null
                            Usage = TextureUsage.CopySrc ||| TextureUsage.OutputAttachment
                            Dimension = TextureDimension.D2D
                            Size = { Width = s.X; Height = s.Y; Depth = 1 }
                            Format = fmt
                            MipLevelCount = 1
                            SampleCount = samples
                        }
                    else
                        null

                swap <- 
                    ctx.ConfigureSwapChain {
                        Device = dev
                        Usage = TextureUsage.CopyDst ||| TextureUsage.OutputAttachment
                        Format = fmt
                    }
            
        let rec render(t : float) = 
            async {
                let v = t / 10000.0
                let m = M44f.RotationZ(float32 v)
                do! dev.Upload(ub, [| m |])

                fixSize()
                let cmd = dev.CreateCommandEncoder()
                let back = swap.GetCurrentTexture()
                

                let c = HSVf(float32 v % 1.0f, 1.0f, 1.0f).ToC3f().ToV3d()

                let pass = 
                    cmd.BeginRenderPass {
                        Label = null
                        ColorAttachments = 
                            [|
                                {
                                    Attachment = if samples > 1 then color.CreateView() else back.CreateView()
                                    ResolveTarget = if samples > 1 then back.CreateView() else null
                                    LoadValue = LoadOp.Color { R = c.X; G = c.Y; B = c.Z; A = 1.0 } 
                                    StoreOp = StoreOp.Store
                                }
                            |]
                        DepthStencilAttachment =
                            Some {
                                Attachment = depth.CreateView()
                                DepthLoadValue = DepthLoadOp.Depth 1.0
                                DepthReadOnly = false
                                DepthStoreOp = StoreOp.Store
                                StencilLoadValue = StencilLoadOp.Stencil 0
                                StencilReadOnly = false
                                StencilStoreOp = StoreOp.Store
                            }
                        OcclusionQuerySet = null
                    }

                pass.SetPipeline(pipeline)
                pass.SetBindGroup(0, bindGroup, null)
                pass.SetVertexBuffer(0, positions)
                pass.SetVertexBuffer(1, colors)
                //pass.SetIndexBuffer(index, IndexFormat.Uint32, 0UL, uint64 indexSize)
                pass.Draw(3, 1, 0, 0)
                pass.EndPass()


                let cmdBuf = cmd.Finish()
                queue.Submit [| cmdBuf |]
                do! queue.OnSubmittedWorkDone()
            }
            
        do! render 0.0

        let rec run (dt : float) =
            async {
                do! render(dt)
                Window.RequestAnimationFrame(run)
            }

        ()
        Window.RequestAnimationFrame(run)


    } |> Async.Start
     


[<EntryPoint>]
let main _argv =

    let rx = System.Text.RegularExpressions.Regex @"^\#version[ \t]+.*"


    //testDynamicMethod()
    testArrayBuffer()
    //testAdaptive()
    //testAardvarkBase()
    //testCallback()
    testWebGPU()


    //Document.Body.Style.Background <- "w3-win8-cobalt"
    //Document.Body.Style.Margin <- "0"
    //Document.Body.Style.Padding <- "0"



    //let c = Document.CreateCanvasElement()

    //c.OnPointerDown.Add(fun e -> 
    //    let e = PointerEvent e
    //    let t = HTMLElement e.Target
    //    Console.BeginCollapsed("pointer")
    //    Console.Log("target", t.Id)
    //    Console.Log("button", string e.Button)
    //    Console.Log("buttons", string e.Buttons)
    //    Console.Log("pointerId", e.PointerId)
    //    Console.Log("client", e.ClientX, e.ClientY)
    //    Console.Log("offset", e.OffsetX, e.OffsetY)
    //    Console.Log("page", e.PageX, e.PageY)
    //    Console.Log("screen", e.ScreenX, e.ScreenY)
    //    Console.Log("alt", e.Alt)
    //    Console.Log("ctrl", e.Ctrl)
    //    Console.Log("meta", e.Meta)
    //    Console.Log("shift", e.Shift)
    //    Console.End()

    //    if e.Button = MouseButton.Secondary then
    //        if isNull Document.FullscreenElement then c.RequestFullscreen().ContinueWith (fun _ -> Console.Log("fullscreen")) |> ignore
    //        else Document.ExitFullscreen()
    //)

    //c.OnContextMenu.Add (fun e ->
    //    e.PreventDefault()
    //)

    ////let d = c.SubscribeEventListener("pointermove", fun e -> Console.Log("mouse", e?clientX, e?clientY))
    
    ////c.AddEventListener("mousedown", fun _ -> d.Dispose())

    //c.Id <- "bla"
    ////c.Width <- 800
    ////c.Height <- 600
    //c.Style.Width <- "100%"
    //c.Style.Height <- "100%"
    //c.ClassName <- "hans hugo"
    ////c.Style.Transform <- "rotate(20deg)"

    //c.ClassList.Add "franz"
    //Document.Body.AppendChild c
    //let gl = c.GetWebGLContext(WebGLContextAttributes(Desynchronized = false)) :?> WebGL2RenderingContext
     
    //let code = Shader.test gl
     
    //Console.Begin "OpenGL"
    //gl.GetParameter(ParameterName.Vendor)                       |> printfn "vendor:   %s"
    //gl.GetParameter(ParameterName.Renderer)                     |> printfn "renderer: %s"
    //gl.GetParameter(ParameterName.Version)                      |> printfn "version:  %s"
    //gl.GetParameter(ParameterName.ShadingLanguageVersion)       |> printfn "glsl:     %s"
    //Console.End()

    //let d = Document.CreateElement "button"
    //d.InnerText <- "Clicky"
    //d.Style.Position <- "absolute"
    //d.Style.Left <- "0"
    //d.Style.Top <- "0"
    //d.Style.ZIndex <- "100"
    //d.OnClick.Add (fun e ->
    //    c.RequestFullscreen() |> ignore

    //    e.PreventDefault()
    //    e.StopImmediatePropagation()
    //)


    //c.InsertAdjacentElement(InsertMode.BeforeBegin, d)

   
    //gl.ClearColor(0.0, 0.0, 0.0, 1.0)
    //gl.Clear ClearBuffers.Color
    
    //let pos = 
    //    Float32Array.op_Implicit (
    //        Span [| 
    //            -0.9f; -0.9f; 0.0f
    //            0.9f; -0.9f; 0.0f
    //            0.9f; 0.9f; 0.0f

                
    //            -0.9f; -0.9f; 0.0f
    //            0.9f; 0.9f; 0.0f
    //            -0.9f; 0.9f; 0.0f
    //        |]
    //    )

    //let col = 
    //    Uint8Array.op_Implicit (
    //        Span [|
    //            255uy; 0uy; 0uy; 255uy
    //            255uy; 255uy; 255uy; 255uy
    //            0uy; 0uy; 255uy; 255uy
                
    //            255uy; 0uy; 0uy;   255uy
    //            0uy; 0uy; 255uy;   255uy
    //            0uy; 255uy; 0uy; 255uy

    //        |]
    //    )

    //let pb = gl.CreateBuffer()
    //gl.BindBuffer(BufferTarget.Array, pb)
    //gl.BufferData(BufferTarget.Array, pos, BufferUsage.StaticDraw)
    //gl.BindBuffer(BufferTarget.Array, null)
    
    //let cb = gl.CreateBuffer()
    //gl.BindBuffer(BufferTarget.Array, cb)
    //gl.BufferData(BufferTarget.Array, col, BufferUsage.StaticDraw)
    //gl.BindBuffer(BufferTarget.Array, null)

    
    //Console.BeginCollapsed "Shader"
    //let vsc = rx.Replace(code, fun m -> m.Value + "\nprecision highp float;\n#define Vertex")
    //printfn "%s" vsc

    //let psc = rx.Replace(code, fun m -> m.Value + "\nprecision highp float;\n#define Fragment")
    //let vs = gl.CreateShader(ShaderType.Vertex)
    //gl.ShaderSource(vs, vsc)
    //gl.CompileShader(vs)
    //let log = gl.GetShaderInfoLog vs
    //if not (String.IsNullOrWhiteSpace log) then
    //    Console.Warn(log)
        
    //let fs = gl.CreateShader(ShaderType.Fragment)
    //gl.ShaderSource(fs, psc)
    //gl.CompileShader(fs)
    //let log = gl.GetShaderInfoLog fs
    //if not (String.IsNullOrWhiteSpace log) then
    //    Console.Warn(log)

    //let p = gl.CreateProgram()
    //gl.AttachShader(p, vs)
    //gl.AttachShader(p, fs)
    //gl.LinkProgram(p)
        
    //let log = gl.GetProgramInfoLog(p)
    //if not (String.IsNullOrWhiteSpace log) then
    //    Console.Warn(log)
        
    //Console.End()

    //let ub = gl.CreateBuffer()
    //gl.BindBuffer(BufferTarget.Uniform, ub)
    //gl.BufferData(BufferTarget.Uniform, Float32Array.op_Implicit (Span (Array.create 64 1.0f)), BufferUsage.DynamicDraw)
    //gl.BindBuffer(BufferTarget.Uniform, null)
    

    //let mutable cnt = 0
    //let mutable lastPrint = 0.0
    //let rec draw(t : float) = 
        
    //    let dt = t - lastPrint
    //    if dt > 2000.0 then
    //        let fps = ((1000.0 * float cnt / dt) * 100.0 |> round) / 100.0
    //        d.InnerText <- fps.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + "fps"
    //        //Console.Log("fps", fps.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture))
    //        lastPrint <- t 
    //        cnt <- 0

    //    cnt <- cnt + 1 

    //    let s = c.GetBoundingClientRect()
    //    c.Width <- int s.Size.X
    //    c.Height <- int s.Size.Y

    //    gl.Viewport(0,0,int s.Size.X, int s.Size.Y)
    //    gl.BindBuffer(BufferTarget.Uniform, ub)
    //    gl.BufferData(BufferTarget.Uniform, Float32Array.op_Implicit (Span (Array.create 64 (float32 t / 1000.0f))), BufferUsage.DynamicDraw)
    //    gl.BindBuffer(BufferTarget.Uniform, null)
        
    //    let a = HSVf(float32 (0.05 * t / 1000.0 % 1.0), 0.7f, 0.5f).ToC3f()

    //    gl.ClearColor(float a.R, float a.G, float a.B, 1.0)
    //    //gl.ClearColor(1.0, 0.0, 0.0, 1.0)
    //    gl.Clear ClearBuffers.Color
    
    //    gl.UseProgram(p)

    //    gl.BindBuffer(BufferTarget.Array, pb)
    //    gl.EnableVertexAttribArray 1
    //    gl.VertexAttribPointer(1, 3, VertexAttribType.Float, false, 12, 0)

    
    //    gl.BindBuffer(BufferTarget.Array, cb)
    //    gl.EnableVertexAttribArray 0
    //    gl.VertexAttribPointer(0, 4, VertexAttribType.UnsignedByte, true, 4, 0)
    
    //    gl.BindBufferRange(BufferTarget.Uniform, 0, ub, 0, 256)

    //    gl.DrawArrays(PrimitiveTopology.Triangles, 0, 6)

    //    gl.UseProgram(null)

    //    Window.Reference.Invoke("requestAnimationFrame", System.Action<float>(fun dt -> draw dt)) |> ignore


    //for e in Document.GetElementsByTagName("canvas") do
    //    printfn "%A" e.Id

    //draw 0.0



    0