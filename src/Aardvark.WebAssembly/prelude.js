(function(){
    var glsl = import ("https://unpkg.com/@webgpu/glslang@0.0.15/dist/web-devel-onefile/glslang.js");
    window.glslang = glsl.then((a) => a.default());
})()
