@echo off
pushd %~dp0

SET OUTPUT=%~dp0..\..\bin\%~1\netstandard2.1

..\..\tools\packager.exe --appdir=..\..\bin\wasm\ ^
   --copy=always ^
   --search-path=%OUTPUT% ^
   --asset=%~dp0index.html ^
   %OUTPUT%\FSharp.Core.dll ^
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
   %OUTPUT%\FSharp.Data.Adaptive.dll

   REM netstandard.dll ^
   REM System.Runtime.dll ^
   REM System.dll ^

   REM %OUTPUT%\System.Runtime.CompilerServices.Unsafe.dll ^
   REM %OUTPUT%\System.Reflection.TypeExtensions.dll ^
   REM %OUTPUT%\System.Reflection.Metadata.dll ^
   REM %OUTPUT%\System.ObjectModel.dll ^
   REM %OUTPUT%\System.Numerics.Vectors.dll ^
   REM %OUTPUT%\System.Linq.dll ^
   REM %OUTPUT%\System.Linq.Expressions.dll ^
   REM %OUTPUT%\System.Dynamic.Runtime.dll ^
   REM %OUTPUT%\System.Buffers.dll ^
   REM %OUTPUT%\System.Threading.dll ^
   REM %OUTPUT%\FSharp.Data.Adaptive.dll ^

popd