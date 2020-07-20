@echo off
pushd %~dp0

SET OUTPUT=%~dp0..\..\bin\%~1\netstandard2.0

..\..\tools\packager.exe --appdir=..\..\bin\wasm\ ^
   --copy=ifnewer ^
   --search-path=%OUTPUT% ^
   --search-path=%userprofile%\.nuget\packages\system.collections.immutable\1.7.1\lib\netstandard2.0\ ^
   --search-path=%userprofile%\.nuget\packages\devilsharp\0.2.9\lib\netstandard2.0\ ^
   --search-path=%userprofile%\.nuget\packages\system.collections.immutable\1.7.1\lib\netstandard2.0\ ^
   --search-path=%userprofile%\.nuget\packages\system.reflection.metadata\1.8.1\lib\netstandard2.0\ ^
   --search-path=%userprofile%\.nuget\packages\system.runtime.compilerservices.unsafe\4.7.1\lib\netstandard2.0\ ^
   --search-path=%userprofile%\.nuget\packages\aardvark.base\5.0.14\lib\netstandard2.0\ ^
   --asset=%~dp0index.html ^
   --threads ^
   --dynamic-runtime ^
   --zlib ^
   %OUTPUT%\Aardvark.WebAssembly.dll ^
   System.IO.dll ^
   System.ValueTuple.dll ^
   System.Reflection.dll ^
   System.Threading.dll ^
   System.Reflection.Emit.Lightweight.dll ^
   System.Reflection.Emit.ILGeneration.dll ^
   System.Core.dll ^
   System.Runtime.dll ^
   System.Console.dll ^
   System.Globalization.dll ^
   System.Runtime.Extensions.dll ^
   System.Dynamic.Runtime.dll ^
   System.ObjectModel.dll ^
   System.Buffers.dll ^
   System.Numerics.dll ^
   System.Numerics.Vectors.dll ^
   System.IO.Compression.dll ^
   System.Runtime.InteropServices.dll ^
   System.Collections.dll ^
   System.Collections.Concurrent.dll ^
   System.ComponentModel.Composition.dll ^
   System.Data.dll ^
   System.Transactions.dll ^
   System.IO.Compression.FileSystem.dll ^
   System.Runtime.Serialization.dll ^
   System.ServiceModel.Internals.dll ^
   System.Xml.dll ^
   Mono.Security.dll ^
   %userprofile%\.nuget\packages\fsharp.core\4.7.2\lib\netstandard2.0\FSharp.Core.dll ^
   %userprofile%\.nuget\packages\fsharp.data.adaptive\1.0.0\lib\netstandard2.0\FSharp.Data.Adaptive.dll ^
   %userprofile%\.nuget\packages\system.collections.immutable\1.7.1\lib\netstandard2.0\System.Collections.Immutable.dll ^
   %userprofile%\.nuget\packages\system.reflection.metadata\1.8.1\lib\netstandard2.0\System.Reflection.Metadata.dll ^
   %userprofile%\.nuget\packages\devilsharp\0.2.9\lib\netstandard2.0\DevILSharp.dll ^
   %userprofile%\.nuget\packages\aardvark.base.telemetry\5.0.14\lib\netstandard2.0\Aardvark.Base.Telemetry.dll ^
   %userprofile%\.nuget\packages\aardvark.base\5.0.14\lib\netstandard2.0\Aardvark.Base.dll
   

popd