#r "paket: groupref Build //"
#r "netstandard"
#r "System.Xml"
#r "System.Net.Http"
#load ".fake/build.fsx/intellisense.fsx"

open System
open System.IO
open Fake
open Fake.Core
open Fake.Core.Operators
open Fake.Core.TargetOperators
open Fake.IO.Globbing.Operators
open Fake.DotNet
open Fake.Tools.Git
open Fake.DotNet.NuGet
open System.Runtime.InteropServices
open Fake.IO

module Server =
    open Suave
    open Suave.WebPart
    open Suave.Filters
    open Suave.Successful
    open Suave.Operators
    open Suave.Writers
    open Suave.Sockets
    open Suave.Sockets.Control
    open Suave.WebSocket

    let run (folder : string) =

        let reloadScript =
            """

            <style>

            .lds-roller {
              display: inline-block;
              position: relative;
              width: 80px;
              height: 80px;
            }
            .lds-roller div {
              animation: lds-roller 1.2s cubic-bezier(0.5, 0, 0.5, 1) infinite;
              transform-origin: 40px 40px;
            }
            .lds-roller div:after {
              content: " ";
              display: block;
              position: absolute;
              width: 7px;
              height: 7px;
              border-radius: 50%;
              background: #fff;
              margin: -4px 0 0 -4px;
            }
            .lds-roller div:nth-child(1) {
              animation-delay: -0.036s;
            }
            .lds-roller div:nth-child(1):after {
              top: 63px;
              left: 63px;
            }
            .lds-roller div:nth-child(2) {
              animation-delay: -0.072s;
            }
            .lds-roller div:nth-child(2):after {
              top: 68px;
              left: 56px;
            }
            .lds-roller div:nth-child(3) {
              animation-delay: -0.108s;
            }
            .lds-roller div:nth-child(3):after {
              top: 71px;
              left: 48px;
            }
            .lds-roller div:nth-child(4) {
              animation-delay: -0.144s;
            }
            .lds-roller div:nth-child(4):after {
              top: 72px;
              left: 40px;
            }
            .lds-roller div:nth-child(5) {
              animation-delay: -0.18s;
            }
            .lds-roller div:nth-child(5):after {
              top: 71px;
              left: 32px;
            }
            .lds-roller div:nth-child(6) {
              animation-delay: -0.216s;
            }
            .lds-roller div:nth-child(6):after {
              top: 68px;
              left: 24px;
            }
            .lds-roller div:nth-child(7) {
              animation-delay: -0.252s;
            }
            .lds-roller div:nth-child(7):after {
              top: 63px;
              left: 17px;
            }
            .lds-roller div:nth-child(8) {
              animation-delay: -0.288s;
            }
            .lds-roller div:nth-child(8):after {
              top: 56px;
              left: 12px;
            }
            @keyframes lds-roller {
              0% {
                transform: rotate(0deg);
              }
              100% {
                transform: rotate(360deg);
              }
            }


            </style>

            <script>
            var ws = new WebSocket("ws://localhost:8080/ws");
            ws.onopen = function() { console.warn("connected"); };
            ws.onmessage = function(msg) { 
                if(msg.data == "reload") {
                    location.reload(true);
                }
                else if(msg.data == "failed") {
                    const hans = document.getElementsByClassName("loaderoverlay");
                    while(hans.length > 0) {
                        hans[0].remove();
                    }
                }
                else if(msg.data == "compile") {
                    const o = document.getElementById("overlay");
                    if(o) o.remove();


                    let e = document.createElement("div");
                    e.id = "overlay";
                    e.style.position = "fixed";
                    e.style.backgroundColor = "rgba(0,0,0,0.5)";
                    e.style.width = "100%";
                    e.style.height = "100%";
                    e.style.display = "flex";
                    e.style.flexDirection = "column";
                    e.style.justifyContent = "center";
                    e.style.alignItems = "center";
                    e.style.top = "0";
                    e.style.left = "0";
                    e.style.cursor = "text";

                    let t = document.createElement("div");
                    t.className="loaderoverlay";
                    t.innerText = "building";
                    t.style.fontFamily = "Consolas";
                    t.style.fontSize = "30pt";
                    t.style.color = "white";
                    t.style.position = "absolute";
                    t.style.right = "130px";
                    t.style.bottom = "30px";
                    t.style.userSelect = "none";
                    t.style.pointerEvents = "none";

                    e.appendChild(t);


                    let s = document.createElement("div");
                    s.className="loaderoverlay";
                    s.innerHTML = "<div class='lds-roller'><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div>";
                    //s.style.marginBottom = "10px";
                    s.style.position = "absolute";
                    s.style.right = "30px";
                    s.style.bottom = "10px";
                    s.style.userSelect = "none";
                    s.style.pointerEvents = "none";
                    e.appendChild(s);

                    let log = document.createElement("div");
                    log.id = "log";
                    log.style.fontFamily = "Consolas";
                    log.style.fontSize = "10pt";
                    log.style.color = "green";
                    log.style.width = "100%";
                    log.style.height = "50%";
                    log.style.backgroundColor = "rgba(0,0,0,0.8)";
                    log.style.overflowX = "hidden";
                    log.style.overflowY = "auto";
                    log.style.wordWrap = "break-word"
                    log.style.flexGrow = "10";
                    e.appendChild(log);


                    document.body.appendChild(e);

                }
                else {
                    const log = document.getElementById("log");
                    if(log) {
                        const str = msg.data;
                        const d = document.createElement("pre");
                        if(str.startsWith("e")) d.style.color = "red";
                        d.innerText = str.substring(1);
                        d.style.margin = "0";
                        d.style.width = "100%";
                        d.style.whiteSpace = "pre-wrap";
                        log.appendChild(d);
                        d.scrollIntoView();
                    }
                }
            };
            </script>
            """


        let index (ctx : HttpContext) = 
            async {
                let content = File.ReadAllText (Path.Combine(folder, "index.html")) 
                let content = reloadScript + content
                return! OK content ctx
            }


        let sockets = System.Collections.Concurrent.ConcurrentDictionary<WebSocket, HttpContext>()


        let send (message : string) =
            let byteResponse =
                message
                |> System.Text.Encoding.ASCII.GetBytes
                |> ByteSegment

            let all = sockets.Keys |> Seq.toArray

            for webSocket in all do
                try webSocket.send Text byteResponse true |> Async.RunSynchronously |> ignore
                with _ -> sockets.TryRemove webSocket |> ignore

        let socket (webSocket : WebSocket) (ctx : HttpContext) =
            socket {
                sockets.GetOrAdd(webSocket, ctx) |> ignore
                Trace.tracefn "connected"


                let loop = ref true

                while !loop do
                    let! msg = webSocket.read()

                    match msg with
                    | (Text, data, true) ->
                        let str = UTF8.toString data
                        let response = sprintf "response to %s" str
                        let byteResponse =
                            response
                            |> System.Text.Encoding.ASCII.GetBytes
                            |> ByteSegment
                        do! webSocket.send Text byteResponse true

                    | (Close, _, _) ->
                        let emptyResponse = [||] |> ByteSegment
                        do! webSocket.send Close emptyResponse true
                        sockets.TryRemove webSocket |> ignore
                        loop := false

                    | _ -> ()
              }

            
        let part = 
            WebPart.choose [
                GET >=> path "/" >=> index
                GET >=> path "/index.html" >=> index
                path "/ws" >=> handShake socket
                GET >=> Files.browse (Path.GetFullPath "./bin/wasm")
                RequestErrors.NOT_FOUND "Page not found." 

            ]

        let defaultMimeTypesMap = function
          | ".css" -> createMimeType "text/css" true
          | ".gif" -> createMimeType "image/gif" false
          | ".png" -> createMimeType "image/png" false
          | ".htm"
          | ".html" -> createMimeType "text/html" true
          | ".jpe"
          | ".jpeg"
          | ".jpg" -> createMimeType "image/jpeg" false
          | ".js"  -> createMimeType "application/x-javascript" true
          
          | ".wasm" -> createMimeType "application/wasm" true

          | _      -> createMimeType "text/plain" true

        let cancel = new System.Threading.CancellationTokenSource()

        let config =
            { defaultConfig with
                homeFolder = Some (Path.GetFullPath "./bin/wasm")
                mimeTypesMap = defaultMimeTypesMap
            }
        let config = config.withCancellationToken(cancel.Token)

        let start, stopped = startWebServerAsync config part
        Async.StartAsTask start |> ignore
        Async.StartAsTask stopped |> ignore


        if RuntimeInformation.IsOSPlatform OSPlatform.Windows then
            Process.shellExec {
                Program = "cmd.exe"
                Args = []
                CommandLine = "/C START http://localhost:8080"
                WorkingDir = "."
            } |> ignore
            
            ()
            

        { new IDisposable with member x.Dispose() = cancel.Cancel() }, send

module MyPaket =
    
    open global.Paket
    open global.Paket.Domain
    
    let getDependencies (file : string) = 
        let lock = Paket.LockFile.LoadFrom "paket.lock"
    
        let hull = lock.GetPackageHull(GroupName "Main", ReferencesFile.FromFile file)

        let all = lock.GetResolvedPackages().[GroupName "Main"]

        let map =
            all |> List.map (fun i ->
                i.Name.CompareString, string i.Version
            ) |> Map.ofList

        hull
        |> Seq.map (fun (KeyValue((_,k),_)) -> k.Name, map.[k.CompareString])
        |> Seq.toList

type CopyMode =
    | Always
    | IfNewer

type EntryPoint =
    {
        Assembly : string
        Class : string
        Name : string
    }

type PackagerConfig =
    {
        Output : string
        CopyMode : CopyMode
        SearchPaths : list<string>
        Assets : list<string>
        Threads : bool
        DynamicRuntime : bool
        ZLib : bool
        References : list<string>
        EntryPoint : EntryPoint
    }
    
type PackagerProjectConfig =
    {
        Project : string
        OutputPath : string
        Output : string
        CopyMode : CopyMode
        Threads : bool
        DynamicRuntime : bool
        ZLib : bool
        EntryPoint : EntryPoint
    }

module Packager =

    let private index =
        String.concat "\r\n" [
            "<script>"
            "window.Module = {};"
            "window.Module.onRuntimeInitialized = () => {"
            "    const config = {"
            "        vfsPrefix: \"managed\","
            "        deployPrefix: \"managed\","
            "        enableDebugging: 0"
            "    };"
            "    const assemblies = ["
            "         {{ASSEMBLIES}}"
            "    ];"
            "    MONO.mono_load_runtime_and_bcl("
            "        config.vfsPrefix,"
            "        config.deployPrefix,"
            "        config.enableDebugging,"
            "        assemblies,"
            "        () => {"
            "            Module.mono_bindings_init(\"[WebAssembly.Bindings]WebAssembly.Runtime\");"
            "            const main = Module.mono_bind_static_method(\"[{{ASSEMBLYNAME}}] {{CLASS}}:{{METHOD}}\");"
            "            main([]);"
            "        }"
            "   )"
            "};"
            "</script>"
            "<script async src=\"dotnet.js\"></script>"
        ]

    let private systemLibs =
        [
            "System.IO.dll"
            "System.ValueTuple.dll"
            "System.Reflection.dll"
            "System.Threading.dll"
            "System.Reflection.Emit.Lightweight.dll"
            "System.Reflection.Emit.ILGeneration.dll"
            "System.Core.dll"
            "System.Runtime.dll"
            "System.Console.dll"
            "System.Globalization.dll"
            "System.Runtime.Extensions.dll"
            "System.Dynamic.Runtime.dll"
            "System.ObjectModel.dll"
            "System.Buffers.dll"
            "System.Numerics.dll"
            "System.Numerics.Vectors.dll"
            "System.IO.Compression.dll"
            "System.Runtime.InteropServices.dll"
            "System.Collections.dll"
            "System.Collections.Concurrent.dll"
            "System.ComponentModel.Composition.dll"
            "System.Data.dll"
            "System.Transactions.dll"
            "System.IO.Compression.FileSystem.dll"
            "System.Runtime.Serialization.dll"
            "System.ServiceModel.Internals.dll"
            "System.Xml.dll"
            "Mono.Security.dll"
        ]

    let package (config : PackagerConfig) =
        
        Directory.Delete(config.Output, true)

        let args =
            [
                match config.CopyMode with
                | IfNewer -> "--copy=ifnewer"
                | Always -> "--copy=always"

                sprintf "--appdir=\"%s\"" (Path.GetFullPath config.Output)
                
                for p in config.SearchPaths do
                    sprintf "--search-path:\"%s\"" (Path.GetFullPath p)

                for a in config.Assets do
                    sprintf "--asset=\"%s\"" (Path.GetFullPath a)

                if config.Threads then "--threads"
                if config.DynamicRuntime then "--dynamic-runtime"
                if config.ZLib then "--zlib"

                for r in config.References do
                    let r = Path.GetFullPath r
                    if not (File.Exists r) then
                        Trace.traceErrorfn "not found: %A" r
                    else
                        sprintf "\"%s\"" (Path.GetFullPath r)

                for s in systemLibs do
                    s

            ]

        let path, additional =
            if RuntimeInformation.IsOSPlatform OSPlatform.Windows then Path.Combine("tools", "packager.exe"), []
            else "mono", [Path.GetFullPath (Path.Combine("tools", "packager.exe"))]

        let mm = Path.Combine(config.Output, "managed")
        if not (Directory.Exists config.Output) then Directory.CreateDirectory config.Output |> ignore
        if not (Directory.Exists mm) then Directory.CreateDirectory mm |> ignore

        Fake.Core.Process.shellExec {
            ExecParams.Program = path
            ExecParams.Args = []
            ExecParams.CommandLine = String.concat " " (additional @ args)
            ExecParams.WorkingDir = "tools"
        } |> ignore


        let indexHtml = 
            let allNames = 
                "mscorlib.dll"::"System.dll"::"netstandard.dll"::"WebAssembly.Bindings.dll":: systemLibs @ (config.References |> List.map Path.GetFileName)
                |> Seq.map (sprintf "        '%s'")
                |> String.concat ",\r\n"

            index.Replace("{{ASSEMBLIES}}", allNames).Replace("{{ASSEMBLYNAME}}", config.EntryPoint.Assembly).Replace("{{CLASS}}", config.EntryPoint.Class).Replace("{{METHOD}}", config.EntryPoint.Name)

        File.WriteAllText(Path.Combine(config.Output, "index.html"), indexHtml)

        Threading.Thread.Sleep 300

        ()

    let packageProject (config : PackagerProjectConfig) =

        let directory = Path.GetDirectoryName config.Project
        let references = 
            MyPaket.getDependencies (Path.Combine(directory, "paket.references"))
            |> List.collect (fun (k,v) ->
                let probes = [v; v + ".0"; v + ".0.0"]
                let packagePath = 
                    probes |> List.tryPick (fun v ->
                        let packagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".nuget", "packages", k.ToLower(), v)
                        if Directory.Exists packagePath then Some packagePath
                        else None
                    )

                match packagePath with
                | Some packagePath ->

                    let refs = 
                        let n20 = Path.Combine(packagePath, "lib", "netstandard2.0")
                        let n16 = Path.Combine(packagePath, "lib", "netstandard1.6")
                        if Directory.Exists n20 then Directory.GetFiles(n20, "*.dll")
                        elif Directory.Exists n16 then Directory.GetFiles(n16, "*.dll")
                        else [||]

                    Array.toList refs
                | None ->
                    Trace.traceErrorfn "could not find package %s (%s)" k v
                    []
            )

        for r in references do 
            Trace.tracefn "%s -> %s" (Path.GetFileName r) (Path.GetDirectoryName r)

        package {
            Output = config.Output
            CopyMode = config.CopyMode
            SearchPaths = references |> List.map Path.GetDirectoryName
            Assets = []
            Threads = config.Threads
            DynamicRuntime = config.DynamicRuntime
            ZLib = config.ZLib
            References = config.OutputPath :: references
            EntryPoint = config.EntryPoint
        }

module Watcher =
    open System.IO
    
    let rec buildWatcher (callback : string -> unit) (directory : string) =
        let w = new FileSystemWatcher(directory, "*")
        w.Renamed.Add (fun e -> callback e.FullPath)
        w.Changed.Add (fun e -> callback e.FullPath)
        w.Created.Add (fun e -> callback e.FullPath)
        w.Deleted.Add (fun e -> callback e.FullPath)
        w.EnableRaisingEvents <- true

        let sub = Directory.GetDirectories(directory)
        if sub.Length = 0 then
            w :> IDisposable
        else
            let sub = sub |> Array.map (buildWatcher callback)
            { new IDisposable with
                member x.Dispose() =
                    w.Dispose()
                    for s in sub do s.Dispose()
            }


        
    open System.Threading

    let run (timeout : int) (callback : unit -> unit) (files : seq<string>) =
        let files = files |> Seq.map Path.GetFullPath
        let dirs = files |> Seq.map Path.getDirectory |> Set.ofSeq

        let rec readForSure (tries : int) (file : string) =
            if tries <= 0 then
                Trace.traceErrorfn "empty file: %A" file
                [||]
                //failwithf "cannot access file: %A" file
            else
                try File.ReadAllBytes file
                with _ ->
                    Thread.Sleep 100
                    readForSure (tries - 1) file


        let fileSet = Set.ofSeq files
        let computeHash() =
            use ms = new MemoryStream()
            for f in fileSet do
                let arr = System.Text.Encoding.UTF8.GetBytes f
                ms.Write(arr, 0, arr.Length)
                let t = readForSure 3 f
                ms.Write(t, 0, t.Length)

            ms.Seek(0L, SeekOrigin.Begin) |> ignore
            let md5 = System.Security.Cryptography.MD5.Create()
            md5.ComputeHash(ms) |> System.Guid


        let hash = ref (computeHash())
        let changed = ref false
        let lockObj = obj()
        let _thread = 
            let run() =
                while true do
                    lock lockObj (fun () ->
                        while not !changed do
                            Monitor.Wait lockObj |> ignore
                        changed := false

                    )
                    let h = computeHash()
                    if h <> !hash then
                        Trace.tracefn "hash changed %A" h
                        hash := h
                        callback()
                    

            let t = Threading.Thread(Threading.ThreadStart(run), IsBackground = true)
            t.Start()
            t


        let timer = 
            let change _ =
                lock lockObj (fun () ->
                    changed := true
                    Monitor.PulseAll lockObj
                )
            new Timer(TimerCallback(change), null, Timeout.Infinite, Timeout.Infinite)

        let fileSet = files |> Seq.map (fun str -> str.ToLowerInvariant()) |> Set.ofSeq
        let callback(path : string) =
            let path = path.ToLowerInvariant()
            if Set.contains path fileSet then
                while not (timer.Change(timeout, Timeout.Infinite)) do
                    Trace.traceImportantfn "could not change timer"

        let all = dirs |> Set.toArray |> Array.map (buildWatcher callback)

        { new IDisposable with
            member x.Dispose() =
                timer.Dispose()
                for a in all do a.Dispose()
        }

    



Target.create "Restore" (fun _ ->
    Fake.DotNet.Paket.restore id
    "Aardvark.WebAssembly.sln" |> Fake.DotNet.DotNet.restore id
)

Target.create "Build" (fun _ ->
    "Aardvark.WebAssembly.sln" |> Fake.DotNet.DotNet.build (fun o ->
        { o with
            Configuration = Fake.DotNet.DotNet.BuildConfiguration.Release
            NoRestore = true
        }
    )
)

Target.create "Packager" (fun _ ->
    Packager.packageProject {
        Project = Path.Combine("src", "Aardvark.WebAssembly", "Aardvark.WebAssembly.fsproj")
        OutputPath = Path.Combine("bin", "Release", "netstandard2.0", "Aardvark.WebAssembly.dll")
        Output = Path.Combine("bin", "wasm")
        CopyMode = IfNewer
        Threads = true
        DynamicRuntime = true
        ZLib = true
        EntryPoint =
            {
                Assembly = "Aardvark.WebAssembly"
                Class = "Bla"
                Name = "main"
            }
    }
)

Target.create "Default" ignore

Target.create "Watch" (fun _ ->
    let all = !!"./src/**/*.fs" ++ "./**/*.fsproj" ++ "./**/paket.references" ++ "./**/paket.lock" -- "./**/obj/**/*.fs"

    for a in all do
        Trace.tracefn "%s" a

    let server, send = Server.run "bin/wasm"



    let watcher = 
        all |> Watcher.run 200 (fun () ->
            try
                use s = Console.OpenStandardOutput()
                use w = new StreamWriter(s)
                let myWriter =
                    let currentLine = ref ""
                    { new TextWriter() with
                        member x.Encoding = System.Text.Encoding.UTF8
                        member x.Write(str : string) =
                            w.Write(str)
                            currentLine := !currentLine + str
                        member x.WriteLine() =
                            send ("m" + !currentLine)
                            currentLine := ""
                            w.WriteLine()
                    }

                use s = Console.OpenStandardError()
                use w = new StreamWriter(s)
                let myError =
                    let currentLine = ref ""
                    { new TextWriter() with
                        member x.Encoding = System.Text.Encoding.UTF8
                        member x.Write(str : string) =
                            w.Write(str)
                            currentLine := !currentLine + str
                        member x.WriteLine() =
                            send ("e" + !currentLine)
                            currentLine := ""
                            w.WriteLine()
                    }


                let o = Console.Out
                let e = Console.Error
                Console.SetOut myWriter
                Console.SetError myError
                try
                    try
                        send "compile"
                        Target.run 1 "Packager" []
                        send "reload"
                    with _ ->
                        send "failed"
                finally
                    Console.SetOut o
                    Console.SetError e
            with _ ->
                ()
        )



    let mutable line = ""
    while line <> "quit" do
        line <- Console.ReadLine().Trim().ToLower()

    server.Dispose()
    watcher.Dispose()
)


"Restore" ==> "Build"
"Build" ==> "Packager"
"Packager" ==> "Default"
"Packager" ==> "Watch"

do
    Environment.SetEnvironmentVariable("Platform", "Any CPU")
    let argv = Environment.GetCommandLineArgs() |> Array.skip 2 // yeah really
    let argv = argv |> Array.filter (fun str -> not (str.StartsWith "-")) |> Array.toList

    let target, args =
        match argv with
            | [] -> "Default", []
            | t::rest -> t, rest   
    let target = 
        try ignore (Target.get target); target
        with _ -> "Help"

    Target.run 1 target args