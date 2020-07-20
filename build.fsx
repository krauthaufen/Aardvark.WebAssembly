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
open Fake.DotNet
open Fake.Tools.Git
open Fake.DotNet.NuGet
open System.Runtime.InteropServices



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
        |> Seq.map (fun (KeyValue((_,k),v)) -> k.Name, map.[k.CompareString])
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
                    sprintf "\"%s\"" (Path.GetFullPath r)

                for s in systemLibs do
                    s

            ]

        let path, additional =
            if RuntimeInformation.IsOSPlatform OSPlatform.Windows then Path.Combine("tools", "packager.exe"), []
            else "mono", [Path.GetFullPath (Path.Combine("tools", "packager.exe"))]

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

"Restore" ==> "Build"
"Build" ==> "Packager"
"Packager" ==> "Default"


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