@echo off
pushd %~dp0

..\..\tools\packager.exe --appdir=..\..\bin\wasm\ --search-path=C:\Users\Schorsch\.nuget\packages\fsharp.core\4.7.0\lib\netstandard2.0 "..\..\bin\%~1\netstandard2.1\Aardvark.WebAssembly.dll" C:\Users\Schorsch\.nuget\packages\fsharp.core\4.7.0\lib\netstandard2.0\FSharp.Core.dll

xcopy /Y /R index.html ..\..\bin\wasm\

popd