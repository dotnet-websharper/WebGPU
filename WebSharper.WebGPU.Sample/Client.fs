namespace WebSharper.WebGPU.Sample

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Templating
open WebSharper.WebGPU

[<JavaScript>]
module Client =

    // The template is the whole document, loaded from the DOM. Editing
    // index.html and refreshing the browser is enough; rebuild only when the
    // holes change.
    type IndexTemplate = Template<"index.html", ClientLoad.FromDocument>

    // ------------------------------------------------------------------
    // Page state
    // ------------------------------------------------------------------

    let themeStorageKey = "websharper-webgpu-theme"

    let theme = Var.Create "light"
    let themeLabel = Var.Create "🌙 Dark mode"

    let status = Var.Create "Ready."
    let adapterText = Var.Create "—"
    let featuresVar = Var.Create "—"
    let limitsVar = Var.Create "—"
    let formatVar = Var.Create "—"
    let computeResult = Var.Create "The compute shader doubles [1, 2, … 8]. Run it to see the result."
    let renderState = Var.Create "Not started."
    let renderLabel = Var.Create "▶ Start rendering"

    // ------------------------------------------------------------------
    // Inlines for globals the binding intentionally does not model
    // ------------------------------------------------------------------

    [<Inline "typeof navigator !== 'undefined' && !!navigator.gpu">]
    let gpuAvailable () : bool = X<bool>

    [<Inline "navigator.gpu">]
    let gpu () : GPU = X<GPU>

    [<Inline "$canvas.getContext('webgpu')">]
    let webgpuContext (canvas: HTMLCanvasElement) : GPUCanvasContext = X<GPUCanvasContext>

    [<Inline "Array.from($features).join(', ')">]
    let featuresToString (features: GPUSupportedFeatures) : string = X<string>

    // ------------------------------------------------------------------
    // Theme
    // ------------------------------------------------------------------

    let applyTheme () =
        JS.Document.DocumentElement.SetAttribute("data-theme", theme.Value)
        themeLabel.Value <- (if theme.Value = "dark" then "☀️ Light mode" else "🌙 Dark mode")

    let toggleTheme () =
        theme.Value <- (if theme.Value = "dark" then "light" else "dark")
        try JS.Window.LocalStorage.SetItem(themeStorageKey, theme.Value) with _ -> ()
        applyTheme ()

    // ------------------------------------------------------------------
    // Shaders (WGSL)
    // ------------------------------------------------------------------

    let renderShader = """
struct Uniforms { angle : vec4<f32>, };
@group(0) @binding(0) var<uniform> u : Uniforms;

@vertex
fn vs_main(@builtin(vertex_index) i : u32) -> @builtin(position) vec4<f32> {
    var p = array<vec2<f32>, 3>(
        vec2<f32>( 0.0,  0.65),
        vec2<f32>(-0.6, -0.5),
        vec2<f32>( 0.6, -0.5));
    let q = p[i];
    let c = cos(u.angle.x);
    let s = sin(u.angle.x);
    return vec4<f32>(q.x * c - q.y * s, q.x * s + q.y * c, 0.0, 1.0);
}

@fragment
fn fs_main(@builtin(position) pos : vec4<f32>) -> @location(0) vec4<f32> {
    let t = pos.y / 432.0;
    return vec4<f32>(0.45 + 0.25 * (1.0 - t), 0.30 + 0.35 * t, 0.92, 1.0);
}
"""

    let computeShader = """
@group(0) @binding(0) var<storage, read> input : array<f32>;
@group(0) @binding(1) var<storage, read_write> output : array<f32>;

@compute @workgroup_size(1)
fn main(@builtin(global_invocation_id) gid : vec3<u32>) {
    let i = gid.x;
    output[i] = input[i] * 2.0;
}
"""

    // ------------------------------------------------------------------
    // GPU state
    // ------------------------------------------------------------------

    let mutable device : GPUDevice = null
    let mutable context : GPUCanvasContext = null
    let mutable canvasFormat : GPUFormat = null
    let mutable uniformBuffer : GPUBuffer = null
    let mutable bindGroup : GPUBindGroup = null
    let mutable pipeline : GPURenderPipeline = null
    let mutable renderRunning = false

    // ------------------------------------------------------------------
    // Render setup and loop
    // ------------------------------------------------------------------

    let setupRender (dev: GPUDevice) =
        let canvas = JS.Document.GetElementById("gpu-canvas") |> As<HTMLCanvasElement>
        context <- webgpuContext canvas
        canvasFormat <- gpu().GetPreferredCanvasFormat()
        context.Configure(GPUCanvasConfiguration(
            device = dev,
            format = canvasFormat,
            AlphaMode = AlphaMode.Opaque))

        uniformBuffer <- dev.CreateBuffer(GPUBufferDescriptor(
            size = 16,
            usage = (GPUBufferUsage.UNIFORM ||| GPUBufferUsage.COPY_DST)))

        let shader = dev.CreateShaderModule(GPUShaderModuleDescriptor(code = renderShader))

        let bgl = dev.CreateBindGroupLayout(GPUBindGroupLayoutDescriptor(
            entries = [|
                GPUBindGroupLayoutEntry(
                    binding = 0,
                    visibility = (GPUShaderStage.VERTEX ||| GPUShaderStage.FRAGMENT),
                    Buffer = GPUBufferBindingLayout(Type = GPUBufferBindingType.Uniform))
            |]))

        let layout = dev.CreatePipelineLayout(GPUPipelineLayoutDescriptor(
            bindGroupLayouts = [| bgl |]))

        pipeline <- dev.CreateRenderPipeline(GPURenderPipelineDescriptor(
            layout = layout,
            vertex = VertexObject(entryPoint = "vs_main", ``module`` = shader),
            Fragment = FragmentObject(
                entryPoint = "fs_main",
                ``module`` = shader,
                Targets = [| ColorTargetState(format = canvasFormat) |]),
            Primitive = PrimitiveObject(Topology = Topology.Triangle_list)))

        bindGroup <- dev.CreateBindGroup(GPUBindGroupDescriptor(
            layout = bgl,
            entries = [|
                GPUBindGroupEntry(binding = 0, resource = GPUBufferBinding(buffer = uniformBuffer))
            |]))

    let rec drawFrame (t: float) =
        if renderRunning && not (isNull (box device)) then
            // Rotate the triangle; only .x of the uniform vec4 is read.
            let angle = float32 (t / 1000.0 * 1.2)
            let data = Float32Array(4)
            data.Set(0, angle)
            device.Queue.WriteBuffer(uniformBuffer, 0, data)

            let encoder = device.CreateCommandEncoder()
            let view = context.GetCurrentTexture().CreateView()
            let pass = encoder.BeginRenderPass(RenderPassDescriptor(
                ColorAttachments = [|
                    ColorAttachmentObject(
                        loadOp = LoadOp.Clear,
                        storeOp = StoreOp.Store,
                        view = view,
                        ClearValue = Colors(r = 0.05, g = 0.07, b = 0.12, a = 1.0))
                |]))
            pass.SetPipeline(pipeline)
            pass.SetBindGroup(0, bindGroup)
            pass.Draw(3)
            pass.End()
            device.Queue.Submit([| encoder.Finish() |])

            JS.Window.RequestAnimationFrame(drawFrame) |> ignore

    let startRender () =
        if isNull (box device) then
            status.Value <- "Request the adapter first."
        else
            if not renderRunning then
                renderRunning <- true
                renderLabel.Value <- "⏸ Stop rendering"
                renderState.Value <- "Rendering a rotating triangle at 60 fps…"
                JS.Window.RequestAnimationFrame(drawFrame) |> ignore

    let toggleRender () =
        if renderRunning then
            renderRunning <- false
            renderLabel.Value <- "▶ Start rendering"
            renderState.Value <- "Stopped."
        else
            startRender ()

    // ------------------------------------------------------------------
    // Compute pass
    // ------------------------------------------------------------------

    let runCompute (dev: GPUDevice) =
        let n = 8
        let byteSize = n * 4

        // Fill the input synchronously: a `for` loop inside `promise { }` would
        // compile to a sequential (microtask-deferred) loop.
        let input = Float32Array(n)
        for i in 0 .. n - 1 do
            input.Set(i, float32 (i + 1))

        promise {
            computeResult.Value <- "Running…"

            let inputBuffer = dev.CreateBuffer(GPUBufferDescriptor(
                size = byteSize,
                usage = (GPUBufferUsage.STORAGE ||| GPUBufferUsage.COPY_DST)))
            let outputBuffer = dev.CreateBuffer(GPUBufferDescriptor(
                size = byteSize,
                usage = (GPUBufferUsage.STORAGE ||| GPUBufferUsage.COPY_SRC)))
            let readBuffer = dev.CreateBuffer(GPUBufferDescriptor(
                size = byteSize,
                usage = (GPUBufferUsage.MAP_READ ||| GPUBufferUsage.COPY_DST)))

            dev.Queue.WriteBuffer(inputBuffer, 0, input)

            let shader = dev.CreateShaderModule(GPUShaderModuleDescriptor(code = computeShader))

            let bgl = dev.CreateBindGroupLayout(GPUBindGroupLayoutDescriptor(
                entries = [|
                    GPUBindGroupLayoutEntry(
                        binding = 0,
                        visibility = GPUShaderStage.COMPUTE,
                        Buffer = GPUBufferBindingLayout(Type = GPUBufferBindingType.Read_only_storage))
                    GPUBindGroupLayoutEntry(
                        binding = 1,
                        visibility = GPUShaderStage.COMPUTE,
                        Buffer = GPUBufferBindingLayout(Type = GPUBufferBindingType.Storage))
                |]))

            let layout = dev.CreatePipelineLayout(GPUPipelineLayoutDescriptor(
                bindGroupLayouts = [| bgl |]))

            let cpipeline = dev.CreateComputePipeline(GPUComputePipelineDescriptor(
                compute = GPUProgrammableStage(entryPoint = "main", ``module`` = shader),
                layout = layout))

            let bg = dev.CreateBindGroup(GPUBindGroupDescriptor(
                layout = bgl,
                entries = [|
                    GPUBindGroupEntry(binding = 0, resource = GPUBufferBinding(buffer = inputBuffer))
                    GPUBindGroupEntry(binding = 1, resource = GPUBufferBinding(buffer = outputBuffer))
                |]))

            let encoder = dev.CreateCommandEncoder()
            let pass = encoder.BeginComputePass()
            pass.SetPipeline(cpipeline)
            pass.SetBindGroup(0, bg)
            pass.DispatchWorkgroups(n)
            pass.End()
            encoder.CopyBufferToBuffer(outputBuffer, 0, readBuffer, 0, byteSize)
            dev.Queue.Submit([| encoder.Finish() |])

            do! readBuffer.MapAsync(GPUMapMode.READ)
            let range = readBuffer.GetMappedRange()
            let result = Float32Array(range)
            let values = [ for i in 0 .. n - 1 -> result.Get(i) ]
            computeResult.Value <-
                "input  = [1, 2, 3, 4, 5, 6, 7, 8]\noutput = ["
                + (values |> List.map string |> String.concat ", ") + "]"
            readBuffer.Unmap()
        }
        |> ignore

    // ------------------------------------------------------------------
    // Adapter / device
    // ------------------------------------------------------------------

    let requestAdapter () =
        if not (gpuAvailable ()) then
            status.Value <-
                "WebGPU (navigator.gpu) is not available in this browser. \
                 Use a browser that ships WebGPU over a secure context (HTTPS or localhost)."
        else
            promise {
                status.Value <- "Requesting adapter…"
                let! adapter = gpu().RequestAdapter()
                if isNull (box adapter) then
                    status.Value <- "No WebGPU adapter is available."
                else
                    let! dev = adapter.RequestDevice()
                    device <- dev

                    let info = adapter.Info
                    adapterText.Value <-
                        if isNull (box info) then "unavailable"
                        else
                            [ info.Vendor; info.Architecture; info.Device ]
                            |> List.filter (fun s -> not (System.String.IsNullOrEmpty s))
                            |> String.concat " · "

                    featuresVar.Value <- featuresToString adapter.Features
                    canvasFormat <- gpu().GetPreferredCanvasFormat()
                    formatVar.Value <- string canvasFormat

                    let l = dev.Limits
                    limitsVar.Value <-
                        sprintf "maxTextureDimension2D=%d · maxBufferSize=%d · maxComputeWorkgroupsPerDimension=%d · maxStorageBufferBindingSize=%d"
                            l.MaxTextureDimension2D l.MaxBufferSize
                            l.MaxComputeWorkgroupsPerDimension l.MaxStorageBufferBindingSize

                    setupRender dev
                    runCompute dev
                    startRender ()
                    status.Value <- "Adapter and device ready."
            }
            |> ignore

    // ------------------------------------------------------------------
    // Entry point
    // ------------------------------------------------------------------

    let boot () =
        if not (gpuAvailable ()) then
            status.Value <-
                "WebGPU (navigator.gpu) is not available in this browser. \
                 Use a browser that ships WebGPU over a secure context (HTTPS or localhost)."
        else
            requestAdapter ()

    [<SPAEntryPoint>]
    let Main () =
        // Restore the stored theme before the first render.
        let stored = try JS.Window.LocalStorage.GetItem(themeStorageKey) with _ -> null
        if stored = "dark" || stored = "light" then theme.Value <- stored
        applyTheme ()

        IndexTemplate()
            .status(status.V)
            .adapterInfo(adapterText.V)
            .format(formatVar.V)
            .features(featuresVar.V)
            .limits(limitsVar.V)
            .renderLabel(renderLabel.V)
            .renderState(renderState.V)
            .computeResult(computeResult.V)
            .themeLabel(themeLabel.V)
            .ToggleTheme(fun _ -> toggleTheme ())
            .RequestAdapter(fun _ -> requestAdapter ())
            .ToggleRender(fun _ -> toggleRender ())
            .RunCompute(fun _ ->
                if isNull (box device) then status.Value <- "Request the adapter first."
                else runCompute device)
            .Bind()

        boot ()
