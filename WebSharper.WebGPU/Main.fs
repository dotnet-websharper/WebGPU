namespace WebSharper.WebGPU

open WebSharper
open WebSharper.JavaScript
open WebSharper.InterfaceGenerator

module Definition =
    /// Numeric bit-flag constants (WebGPU usage/visibility masks). They are typed as
    /// `int` so consumers can combine them with F#'s `|||` operator, for example
    /// `GPUShaderStage.VERTEX ||| GPUShaderStage.FRAGMENT`.
    let private FlagsEnum name (values: (string * int) seq) =
        Class name
        |+> Static [
            for (n, v) in values ->
                Getter n T<int>
                |> WithGetterInline (string v) :> _
        ]

    let PowerPreference = 
        Pattern.EnumStrings "PowerPreference" [
            "low-power"
            "high-performance"
        ]

    let GPUDeviceLostInfoReason = 
        Pattern.EnumStrings "GPUDeviceLostInfoReason" [
            "destroyed"
            "unknown"
        ]

    let GPUAspect = 
        Pattern.EnumStrings "GPUAspect" [
            "all"; "depth-only"; "stencil-only"
        ]

    let DestinationColorSpace = 
        Pattern.EnumStrings "DestinationColorSpace" [
            "srgb"; "display-p3"
        ]

    let GPUTextureDimension = 
        Pattern.EnumStrings "GPUTextureDimension" [
            "1d"; "2d"; "3d"
        ]

    let DescriptorDimension = 
        Pattern.EnumStrings "DescriptorDimension" [
            "1d"; "2d"; "2d-array"; "cube"; "cube-array"; "3d"
        ]

    let GPUBufferMapState = 
        Pattern.EnumStrings "GPUBufferMapState" [
            "unmapped"; "pending"; "mapped"
        ]

    // GPUMapMode values are the numeric bit flags exposed by the browser.
    let GPUMapMode = 
        Pattern.EnumInlines "GPUMapMode" [
            "READ", "1"
            "WRITE", "2"
        ]

    // GPUShaderStage values are the numeric bit flags exposed by the browser.
    let GPUShaderStage =
        FlagsEnum "GPUShaderStage" [
            "VERTEX", 1
            "FRAGMENT", 2
            "COMPUTE", 4
        ]

    let GPUFormat =
        Pattern.EnumStrings "GPUFormat" [
            // 8-bit formats
            "r8unorm"
            "r8snorm"
            "r8uint"
            "r8sint"

            // 16-bit formats
            "r16uint"
            "r16sint"
            "r16float"
            "rg8unorm"
            "rg8snorm"
            "rg8uint"
            "rg8sint"

            // 32-bit formats
            "r32uint"
            "r32sint"
            "r32float"
            "rg16uint"
            "rg16sint"
            "rg16float"
            "rgba8unorm"
            "rgba8unorm-srgb"
            "rgba8snorm"
            "rgba8uint"
            "rgba8sint"
            "bgra8unorm"
            "bgra8unorm-srgb"

            // Packed 32-bit formats
            "rgb9e5ufloat"
            "rgb10a2uint"
            "rgb10a2unorm"
            "rg11b10ufloat"

            // 64-bit formats
            "rg32uint"
            "rg32sint"
            "rg32float"
            "rgba16uint"
            "rgba16sint"
            "rgba16float"

            // 128-bit formats
            "rgba32uint"
            "rgba32sint"
            "rgba32float"

            // Depth/stencil formats
            "stencil8"
            "depth16unorm"
            "depth24plus"
            "depth24plus-stencil8"
            "depth32float"
            "depth32float-stencil8"

            // BC compressed formats
            "bc1-rgba-unorm"
            "bc1-rgba-unorm-srgb"
            "bc2-rgba-unorm"
            "bc2-rgba-unorm-srgb"
            "bc3-rgba-unorm"
            "bc3-rgba-unorm-srgb"
            "bc4-r-unorm"
            "bc4-r-snorm"
            "bc5-rg-unorm"
            "bc5-rg-snorm"
            "bc6h-rgb-ufloat"
            "bc6h-rgb-float"
            "bc7-rgba-unorm"
            "bc7-rgba-unorm-srgb"

            // ETC2 compressed formats
            "etc2-rgb8unorm"
            "etc2-rgb8unorm-srgb"
            "etc2-rgb8a1unorm"
            "etc2-rgb8a1unorm-srgb"
            "etc2-rgba8unorm"
            "etc2-rgba8unorm-srgb"
            "eac-r11unorm"
            "eac-r11snorm"
            "eac-rg11unorm"
            "eac-rg11snorm"

            // ASTC compressed formats
            "astc-4x4-unorm"
            "astc-4x4-unorm-srgb"
            "astc-5x4-unorm"
            "astc-5x4-unorm-srgb"
            "astc-5x5-unorm"
            "astc-5x5-unorm-srgb"
            "astc-6x5-unorm"
            "astc-6x5-unorm-srgb"
            "astc-6x6-unorm"
            "astc-6x6-unorm-srgb"
            "astc-8x5-unorm"
            "astc-8x5-unorm-srgb"
            "astc-8x6-unorm"
            "astc-8x6-unorm-srgb"
            "astc-8x8-unorm"
            "astc-8x8-unorm-srgb"
            "astc-10x5-unorm"
            "astc-10x5-unorm-srgb"
            "astc-10x6-unorm"
            "astc-10x6-unorm-srgb"
            "astc-10x8-unorm"
            "astc-10x8-unorm-srgb"
            "astc-10x10-unorm"
            "astc-10x10-unorm-srgb"
            "astc-12x10-unorm"
            "astc-12x10-unorm-srgb"
            "astc-12x12-unorm"
            "astc-12x12-unorm-srgb"
        ]

    let GPUTextureUsage = 
        FlagsEnum "GPUTextureUsage" [
            "COPY_SRC", 1
            "COPY_DST", 2
            "TEXTURE_BINDING", 4
            "STORAGE_BINDING", 8
            "RENDER_ATTACHMENT", 16
        ]

    let GPUBufferUsage = 
        FlagsEnum "GPUBufferUsage" [
            "MAP_READ", 1
            "MAP_WRITE", 2
            "COPY_SRC", 4
            "COPY_DST", 8
            "INDEX", 16
            "VERTEX", 32
            "UNIFORM", 64
            "STORAGE", 128
            "INDIRECT", 256
            "QUERY_RESOLVE", 512
        ]

    let GPUQueryType =
        Pattern.EnumStrings "GPUQueryType" [
            "occlusion"; "timestamp"
        ]

    let GPUComputePassLocation = 
        Pattern.EnumStrings "GPUComputePassLocation" [
            "beginning"; "end"
        ]

    let LoadOp = 
        Pattern.EnumStrings "LoadOp" [
            "clear"; "load"
        ]

    let StoreOp = 
        Pattern.EnumStrings "StoreOp" [
            "discard"; "store"
        ]

    let TimestampLocation = 
        Pattern.EnumStrings "TimestampLocation" [
            "beginning"; "end"
        ]

    let IndexFormat = 
        Pattern.EnumStrings "IndexFormat" [
            "uint16"; "uint32"
        ]

    let GPUCompilationMessageType = 
        Pattern.EnumStrings "GPUCompilationMessageType" [
            "error"; "info"; "warning"
        ]
        
    let Compare = 
        Pattern.EnumStrings "DepthCompare" [
            "never"; "less"; "equal"; "less-equal"; "greater"; "not-equal"; "greater-equal"; "always"
        ]

    let StencilOperation = 
        Pattern.EnumStrings "StencilOperation" [
            "decrement-clamp"; "decrement-wrap"; "invert"; "increment-clamp"; "increment-wrap"; "keep"; "replace"; "zero"
        ]

    let BlendFactor =
        Pattern.EnumStrings "BlendFactor" [
            "constant"
            "dst"
            "dst-alpha"
            "one"
            "one-minus-dst"
            "one-minus-src"
            "one-minus-src-alpha"
            "one-minus-dst-alpha"
            "one-minus-constant"
            "src"
            "src-alpha"
            "src-alpha-saturated"
            "zero"
        ]

    let BlendOperation =
        Pattern.EnumStrings "BlendOperation" [
            "add"
            "max"
            "min"
            "reverse-subtract"
            "subtract"
        ]

    // GPUColorWrite values are the numeric bit flags exposed by the browser.
    let GPUColorWrite =
        FlagsEnum "GPUColorWrite" [
            "RED", 1
            "GREEN", 2
            "BLUE", 4
            "ALPHA", 8
            "ALL", 15
        ]

    let CullMode =
        Pattern.EnumStrings "CullMode" [
            "back"; "front"; "none"
        ]

    let FrontFace =
        Pattern.EnumStrings "FrontFace" [
            "ccw"; "cw"
        ]

    let StripIndexFormat =
        Pattern.EnumStrings "StripIndexFormat" [
            "uint16"; "uint32"
        ]

    let Topology =
        Pattern.EnumStrings "Topology" [
            "line-list"
            "line-strip"
            "point-list"
            "triangle-list"
            "triangle-strip"
        ]

    let StepMode =
        Pattern.EnumStrings "StepMode" [
            "instance"
            "vertex"
        ]

    let GPUVertexFormat =
        Pattern.EnumStrings "GPUVertexFormat" [
            "uint8x2"
            "uint8x4"
            "sint8x2"
            "sint8x4"
            "unorm8x2"
            "unorm8x4"
            "snorm8x2"
            "snorm8x4"
            "uint16x2"
            "uint16x4"
            "sint16x2"
            "sint16x4"
            "unorm16x2"
            "unorm16x4"
            "snorm16x2"
            "snorm16x4"
            "float16x2"
            "float16x4"
            "float32"
            "float32x2"
            "float32x3"
            "float32x4"
            "uint32"
            "uint32x2"
            "uint32x3"
            "uint32x4"
            "sint32"
            "sint32x2"
            "sint32x3"
            "sint32x4"
            "unorm10-10-10-2"
        ]

    let AddressMode =
        Pattern.EnumStrings "AddressMode" [
            "clamp-to-edge"
            "repeat"
            "mirror-repeat"
        ]

    let FilterMode =
        Pattern.EnumStrings "FilterMode" [
            "nearest"; "linear"
        ]

    let TextureDimension =
        Pattern.EnumStrings "TextureDimension" [
            "1d"; "2d"; "3d"
        ]

    let VideoColorSpace =
        Pattern.EnumStrings "VideoColorSpace" [
            "srgb"; "display-p3"
        ]

    let GPUErrorFilter =
        Pattern.EnumStrings "GPUErrorFilter" [
            "internal"
            "out-of-memory"
            "validation"
        ]

    let AlphaMode = 
        Pattern.EnumStrings "AlphaMode" [
            "opaque"; "premultiplied"
        ]

    let ColorSpace = 
        Pattern.EnumStrings "ColorSpace" [
            "srgb"; "display-p3"
        ]

    let GPUWGSLLanguageFeatures = 
        Class "GPUWGSLLanguageFeatures"
        |+> Instance [
            "size" =? T<int>

            "has" => T<obj>?value ^-> T<bool>
            "values" => T<unit> ^-> T<obj>
            "keys" => T<unit> ^-> T<obj>
            "entries" => T<unit> ^-> T<obj>
            "forEach" => T<obj>?callback * TSelf?thisArg ^-> T<unit>
        ]

    let GPUSupportedFeatures = 
        Class "GPUSupportedFeatures"
        |=> Inherits GPUWGSLLanguageFeatures

    let GPUSupportedLimits =
        Pattern.Config "GPUSupportedLimits" {
            Required = []
            Optional = [
                "maxTextureDimension1D", T<int>
                "maxTextureDimension2D", T<int>
                "maxTextureDimension3D", T<int>
                "maxTextureArrayLayers", T<int>
                "maxBindGroups", T<int>
                "maxBindingsPerBindGroup", T<int>
                "maxDynamicUniformBuffersPerPipelineLayout", T<int>
                "maxDynamicStorageBuffersPerPipelineLayout", T<int>
                "maxSampledTexturesPerShaderStage", T<int>
                "maxSamplersPerShaderStage", T<int>
                "maxStorageBuffersPerShaderStage", T<int>
                "maxStorageTexturesPerShaderStage", T<int>
                "maxUniformBuffersPerShaderStage", T<int>
                "maxUniformBufferBindingSize", T<int>
                "maxStorageBufferBindingSize", T<int>
                "minUniformBufferOffsetAlignment", T<int>
                "minStorageBufferOffsetAlignment", T<int>
                "maxVertexBuffers", T<int>
                "maxBufferSize", T<int>
                "maxVertexAttributes", T<int>
                "maxVertexBufferArrayStride", T<int>
                "maxInterStageShaderComponents", T<int>
                "maxInterStageShaderVariables", T<int>
                "maxColorAttachments", T<int>
                "maxColorAttachmentBytesPerSample", T<int>
                "maxComputeWorkgroupStorageSize", T<int>
                "maxComputeInvocationsPerWorkgroup", T<int>
                "maxComputeWorkgroupSizeX", T<int>
                "maxComputeWorkgroupSizeY", T<int>
                "maxComputeWorkgroupSizeZ", T<int>
                "maxComputeWorkgroupsPerDimension", T<int>
            ]
        }

    let GPUAdapterInfo =
        Class "GPUAdapterInfo"
        |+> Instance [
            "architecture" =? T<string>
            "description" =? T<string>
            "device" =? T<string>
            "vendor" =? T<string>
        ]

    let GPUDeviceLostInfo = 
        Class "GPUDeviceLostInfo"
        |+> Instance [
            "message" =? T<string>
            "reason" =? GPUDeviceLostInfoReason.Type
        ]

    let SourceOrigin =
        Pattern.Config "SourceOrigin" {
            Required = [
                "x", T<int>
                "y", T<int>
            ]
            Optional = []
        }

    let ArrayOrSourceOrigin = !| T<int> + SourceOrigin

    let GPUImageSource = 
        Pattern.Config "GPUImageSource" {
            Required = ["source", T<HTMLVideoElement> + T<HTMLCanvasElement> + T<OffscreenCanvas>]
            Optional = [
                "origin", ArrayOrSourceOrigin
                "flipY", T<bool>
            ]
        }

    let GPULabel = 
        Pattern.Config "GPULabel" {
            Required = []
            Optional = [
                "label", T<string>
            ]
        }

    let GPUTextureView = 
        Class "GPUTextureView"
        |=> Inherits GPULabel

    let GPUExternalTexture = 
        Class "GPUExternalTexture"
        |=> Inherits GPULabel

    let GPUSampler = 
        Class "GPUSampler" 
        |=> Inherits GPULabel

    let CreateViewDescriptor = 
        Pattern.Config "CreateViewDescriptor" {
            Required = []
            Optional = [
                "arrayLayerCount", T<int>
                "aspect", GPUAspect.Type
                "baseArrayLayer", T<int>
                "baseMipLevel", T<int>
                "dimension", DescriptorDimension.Type
                "format", GPUFormat.Type
                "label", T<string>
                "mipLevelCount", T<int>
            ]
        }

    let GPUTexture =
        Class "GPUTexture"
        |+> Instance [
            "label" =@ T<string>

            "depthOrArrayLayers" =? T<int>
            "dimension" =? GPUTextureDimension.Type
            "format" =? GPUFormat.Type
            "height" =? T<int>
            "mipLevelCount" =? T<int>
            "sampleCount" =? T<int>
            "usage" =? T<int>
            "width" =? T<int>

            "createView" => !? CreateViewDescriptor?descriptor ^-> GPUTextureView
            "destroy" => T<unit> ^-> T<unit>
        ]

    let DestinationOrigin = 
        Pattern.Config "DestinationOrigin" {
            Required = [
                "x", T<int>
                "y", T<int>
                "z", T<int>
            ]
            Optional = []
        }

    let ArrayOrDestinationOrigin = !| T<int> + DestinationOrigin

    let GPUDestination = 
        Pattern.Config "GPUDestination" {
            Required = []
            Optional = [
                "aspect", GPUAspect.Type
                "mipLevel", T<int>
                "origin", ArrayOrDestinationOrigin
                "texture", GPUTexture.Type  
            ]
        }

    let GPUImageDestination = 
        Pattern.Config "GPUImageDestination" {
            Required = []
            Optional = [
                "colorSpace", DestinationColorSpace.Type
                "premultipliedAlpha", T<bool>
            ]
        }
        |=> Inherits GPUDestination

    let GPUCommandBuffer = 
        Class "GPUCommandBuffer"
        |=> Inherits GPULabel

    let GPUBuffer = 
        Class "GPUBuffer" 
        |+> Instance [
            "label" =@ T<string>

            "mapState" =? GPUBufferMapState.Type
            "size" =? T<int>
            "usage" =? T<int>

            "destroy" => T<unit> ^-> T<unit>
            "getMappedRange" => !? T<int>?offset * !? T<int>?size ^-> T<ArrayBuffer>
            "mapAsync" => GPUMapMode.Type?mode * !? T<int>?offset * !? T<int>?size ^-> T<Promise<unit>>
            "unmap" => T<unit> ^-> T<unit>
        ]

    let GPUTextureDataLayout = 
        Pattern.Config "GPUTextureDataLayout" {
            Required = []
            Optional = [
                "offset", T<int>
                "bytesPerRow", T<int>
                "rowsPerImage", T<int>
            ]
        }

    let GPUExtent3D = 
        Pattern.Config "GPUExtent3D" {
            Required = [
                "width", T<int>
                "height", T<int>
                "depthOrArrayLayers", T<int>
            ]
            Optional = []
        }

    let ArrayOrGPUExtent3D = !| T<int> + GPUExtent3D

    let Size = 
        Pattern.Config "Size" {
            Required = [
                "width", T<int>
                "height", T<int>
                "depth", T<int>
            ]
            Optional = []
        }

    let ArrayOrSize = !| T<int> + Size

    let Data = T<ArrayBuffer> + T<ArrayBufferView>

    let GPUQueue =
        Class "GPUQueue"
        |+> Instance [
            "label" =@ T<string>

            "copyExternalImageToTexture" => GPUImageSource?source * GPUImageDestination?destination * ArrayOrGPUExtent3D?copySize ^-> T<unit>
            "onSubmittedWorkDone" => T<unit> ^-> T<Promise<unit>>
            "submit" => (!|GPUCommandBuffer)?commandBuffers ^-> T<unit>
            "writeBuffer" => GPUBuffer?buffer * T<int>?bufferOffset * Data?data * !? T<int>?dataOffset * !? T<int>?size ^-> T<unit>
            "writeTexture" => GPUDestination?destination * Data?data * GPUTextureDataLayout?dataLayout * ArrayOrSize?size ^-> T<unit>
        ]

    let GPUBindGroupLayout = 
        Class "GPUBindGroupLayout"
        |=> Inherits GPULabel

    let GPUBindGroup = 
        Class "GPUBindGroup"
        |=> Inherits GPULabel

    let GPUBufferBinding =  
        Pattern.Config "GPUBufferBinding" {
            Required = ["buffer", GPUBuffer.Type]
            Optional = [
                "offset", T<int>
                "size", T<int>
            ]
        }

    let GPUBufferBindingType =
        Pattern.EnumStrings "GPUBufferBindingType" [
            "uniform"; "storage"; "read-only-storage"
        ]

    let GPUSamplerBindingType =
        Pattern.EnumStrings "GPUSamplerBindingType" [
            "filtering"; "non-filtering"; "comparison"
        ]

    let GPUTextureSampleType =
        Pattern.EnumStrings "GPUTextureSampleType" [
            "float"; "unfilterable-float"; "depth"; "sint"; "uint"
        ]

    let GPUStorageTextureAccess =
        Pattern.EnumStrings "GPUStorageTextureAccess" [
            "write-only"; "read-only"; "read-write"
        ]

    let GPUBufferBindingLayout =
        Pattern.Config "GPUBufferBindingLayout" {
            Required = []
            Optional = [
                "type", GPUBufferBindingType.Type
                "hasDynamicOffset", T<bool>
                "minBindingSize", T<int>
            ]
        }

    let GPUSamplerBindingLayout =
        Pattern.Config "GPUSamplerBindingLayout" {
            Required = []
            Optional = [
                "type", GPUSamplerBindingType.Type
            ]
        }

    let GPUTextureBindingLayout =
        Pattern.Config "GPUTextureBindingLayout" {
            Required = []
            Optional = [
                "sampleType", GPUTextureSampleType.Type
                "viewDimension", DescriptorDimension.Type
                "multisampled", T<bool>
            ]
        }

    let GPUStorageTextureBindingLayout =
        Pattern.Config "GPUStorageTextureBindingLayout" {
            Required = []
            Optional = [
                "access", GPUStorageTextureAccess.Type
                "format", GPUFormat.Type
                "viewDimension", DescriptorDimension.Type
            ]
        }

    let GPUExternalTextureBindingLayout =
        Pattern.Config "GPUExternalTextureBindingLayout" {
            Required = []
            Optional = []
        }

    let GPUBindGroupLayoutEntry = 
        Pattern.Config "GPUBindGroupLayoutEntry" {
            Required = [
                "binding", T<int>
                "visibility", T<int>
            ]
            Optional = [
                "buffer", GPUBufferBindingLayout.Type
                "externalTexture", GPUExternalTextureBindingLayout.Type
                "sampler", GPUSamplerBindingLayout.Type
                "storageTexture", GPUStorageTextureBindingLayout.Type
                "texture", GPUTextureBindingLayout.Type
            ]
        }

    let GPUBindGroupLayoutDescriptor = 
        Pattern.Config "GPUBindGroupLayoutDescriptor" {
            Required = ["entries", !| GPUBindGroupLayoutEntry.Type]
            Optional = ["label", T<string>]
        }

    // A resource inside a bind group: a buffer binding, a sampler, a texture view or an external texture.
    let GPUBindingResource =
        GPUBufferBinding + GPUSampler + GPUTextureView + GPUExternalTexture

    let GPUBindGroupEntry = 
        Pattern.Config "GPUBindGroupEntry" {
            Required = [
                "binding", T<int>
                "resource", GPUBindingResource
            ]
            Optional = []
        }

    let GPUBindGroupDescriptor = 
        Pattern.Config "GPUBindGroupDescriptor" {
            Required = [
                "entries", !| GPUBindGroupEntry.Type
                "layout", GPUBindGroupLayout.Type
            ]
            Optional = [
                "label", T<string>
            ]
        }

    let GPUBufferDescriptor =
        Pattern.Config "GPUBufferDescriptor" {
            Required = [
                "size", T<int>
                "usage", T<int>
            ]
            Optional = [
                "label", T<string>
                "mappedAtCreation", T<bool>
            ]
        }

    let GPUCommandEncoderDescriptor =
        Pattern.Config "GPUCommandEncoderDescriptor" {
            Required = []
            Optional = [
                "label", T<string>
            ]
        }    

    let GPUQuerySet =
        Class "GPUQuerySet"
        |+> Instance [
            "label" =@ T<string>

            "count" =? T<int>            
            "type" =? GPUQueryType.Type

            "destroy" => T<unit> ^-> T<unit>
        ]

    let GPUComputePassTimestampWrites =
        Pattern.Config "GPUComputePassTimestampWrites" {
            Required = []
            Optional = [
                "location", GPUComputePassLocation.Type
                "queryIndex", T<int>
                "querySet", GPUQuerySet.Type
            ]
        }

    let GPUComputePassDescriptor =
        Pattern.Config "GPUComputePassDescriptor" {
            Required = []
            Optional = [
                "label", T<string>
                "timestampWrites", !| GPUComputePassTimestampWrites
            ]
        }

    let GPUComputePipeline = 
        Class "GPUComputePipeline"
        |=> Inherits GPULabel
        |+> Instance [
            "getBindGroupLayout" => T<int>?index ^-> GPUBindGroupLayout
        ]

    let GPUComputePassEncoder =
        Class "GPUComputePassEncoder"
        |+> Instance [
            "label" =? T<string>

            "dispatchWorkgroups" => T<int>?workgroupCountX * !? T<int>?workgroupCountY * !? T<int>?workgroupCountZ ^-> T<unit>
            "dispatchWorkgroupsIndirect" => GPUBuffer?indirectBuffer * T<int>?indirectOffset ^-> T<unit>
            "end" => T<unit> ^-> T<unit>
            "insertDebugMarker" => T<string>?markerLabel ^-> T<unit>
            "popDebugGroup" => T<unit> ^-> T<unit>
            "pushDebugGroup" => T<string>?groupLabel ^-> T<unit>
            "setBindGroup" => T<int>?index * GPUBindGroup?bindGroup * !? (!|T<int>+ T<Uint32Array>)?dynamicOffsets * !? T<int>?dynamicOffsetStart * !? T<int>?dynamicOffsetLength ^-> T<unit>
            "setPipeline" => GPUComputePipeline?pipeline ^-> T<unit>
        ]

    let TimestampWrite = 
        Pattern.Config "TimestampWrite" {
            Required = [
                "location", TimestampLocation.Type
                "queryIndex", T<int>
                "querySet", GPUQuerySet.Type
            ]
            Optional = []
        }

    let Colors = 
        Pattern.Config "Colors" {
            Required = [
                "r", T<float>
                "g", T<float>
                "b", T<float>
                "a", T<float>
            ]
            Optional = []
        }

    let ArrayOrColors = !| T<float> + Colors

    let ColorAttachmentObject = 
        Pattern.Config "ColorAttachmentObject" {
            Required = [
                "loadOp", LoadOp.Type
                "storeOp", StoreOp.Type
                "view", GPUTextureView.Type
            ]
            Optional = [
                "clearValue", ArrayOrColors                
                "resolveTarget", GPUTextureView.Type                
            ]
        }

    let DepthStencilAttachmentObject = 
        Pattern.Config "DepthStencilAttachmentObject" {
            Required = []
            Optional = [
                "depthClearValue", T<float>
                "depthLoadOp", LoadOp.Type
                "depthReadOnly", T<bool>
                "depthStoreOp", StoreOp.Type
                "stencilClearValue", T<int>
                "stencilLoadOp", LoadOp.Type
                "stencilStoreOp", StoreOp.Type
                "stencilReadOnly", T<bool>
                "view", GPUTextureView.Type
            ]
        }

    let RenderPassDescriptor = 
        Pattern.Config "RenderPassDescriptor" {
            Required = []
            Optional = [
                "colorAttachments", !| ColorAttachmentObject
                "depthStencilAttachment", DepthStencilAttachmentObject.Type
                "label", T<string>
                "maxDrawCount", T<int>
                "occlusionQuerySet", GPUQuerySet.Type
                "timestampWrites", !| TimestampWrite
            ]
        }

    let GPURenderBundle = 
        Class "GPURenderBundle"
        |=> Inherits GPULabel

    let GPURenderPipeline = 
        Class "GPURenderPipeline"
        |=> Inherits GPULabel
        |+> Instance [
            "getBindGroupLayout" => T<int>?index ^-> GPUBindGroupLayout
        ]

    let GPURenderEncoder = 
        Class "GPURenderEncoder"
        |+> Instance [
            "label" =? T<string>

            "draw" => T<int>?vertexCount * !? T<int>?instanceCount * !? T<int>?firstIndex * !? T<int>?firstInstance ^-> T<unit>
            "drawIndexed" => T<int>?indexCount * !? T<int>?instanceCount * !? T<int>?firstIndex * !? T<int>?baseVertex * !? T<int>?firstInstance ^-> T<unit>
            "drawIndexedIndirect" => GPUBuffer?indirectBuffer * T<int>?indirectOffset ^-> T<unit>
            "drawIndirect" => GPUBuffer?indirectBuffer * T<int>?indirectOffset ^-> T<unit>
            "insertDebugMarker" => T<string>?markerLabel ^-> T<unit>
            "popDebugGroup" => T<unit> ^-> T<unit>
            "pushDebugGroup" => T<string>?groupLabel ^-> T<unit>
            "setBindGroup" => T<int>?index * GPUBindGroup?bindGroup * !? (!| T<int> + T<Uint32Array>)?dynamicOffsets * !? T<int>?dynamicOffsetsStart * !? T<int>?dynamicOffsetsLength ^-> T<unit>
            "setIndexBuffer" => GPUBuffer?buffer * IndexFormat?indexFormat * !? T<int>?offset * !? T<int>?size ^-> T<unit>
            "setPipeline" => GPURenderPipeline?pipeline ^-> T<unit>
            "setVertexBuffer" => T<int>?slot * GPUBuffer?buffer * !? T<int>?offset * !? T<int>?size ^-> T<unit>
        ]

    let GPURenderPassEncoder =
        Class "GPURenderPassEncoder"
        |=> Inherits GPURenderEncoder
        |+> Instance [
            "beginOcclusionQuery" => T<int> ^-> T<unit>
            "end" => T<unit> ^-> T<unit>
            "endOcclusionQuery" => T<unit> ^-> T<unit>
            "executeBundles" => (!| GPURenderBundle)?bundles ^-> T<unit>
            "setBlendConstant" => ArrayOrColors?color ^-> T<unit>            
            "setScissorRect" => T<float>?x * T<float>?y * T<float>?width * T<float>?height ^-> T<unit>
            "setStencilReference" => T<int>?reference ^-> T<unit>            
            "setViewport" => T<float>?x * T<float>?y * T<float>?width * T<float>?height * T<float>?minDepth * T<float>?maxDepth ^-> T<unit>
        ] 
        
    let GPURenderBundleEncoder = 
        Class "GPURenderBundleEncoder"
        |=> Inherits GPURenderEncoder
        |+> Instance [
            "finish" => GPULabel?descriptor ^-> GPURenderBundle
        ]

    let GPUBufferCopyView =
        Pattern.Config "GPUBufferCopyView" {
            Required = [
                "buffer", GPUBuffer.Type
            ]
            Optional = [
                "offset", T<int>
                "bytesPerRow", T<int>
                "rowsPerImage", T<int>
            ]
        }

    let GPUTextureCopyView = 
        Class "GPUTextureCopyView"
        |=> Inherits GPUDestination

    let GPUCommandEncoder =
        Class "GPUCommandEncoder"
        |+> Instance [
            "label" =? T<string>

            "beginComputePass" => !?GPUComputePassDescriptor?descriptor ^-> GPUComputePassEncoder
            "beginRenderPass" => RenderPassDescriptor?descriptor ^-> GPURenderPassEncoder
            "clearBuffer" => GPUBuffer?buffer * T<int>?offset * T<int>?size ^-> T<unit>
            "copyBufferToBuffer" => GPUBuffer?source * T<int>?sourceOffset * GPUBuffer?destination * T<int>?destinationOffset * T<int>?size ^-> T<unit>
            "copyBufferToTexture" => GPUBufferCopyView?source * GPUTextureCopyView?destination * ArrayOrGPUExtent3D?copySize ^-> T<unit>
            "copyTextureToBuffer" => GPUTextureCopyView?source * GPUBufferCopyView?destination * ArrayOrGPUExtent3D?copySize ^-> T<unit>
            "copyTextureToTexture" => GPUTextureCopyView?source * GPUTextureCopyView?destination * ArrayOrGPUExtent3D?copySize ^-> T<unit>
            "finish" => !? GPULabel?descriptor ^-> GPUCommandBuffer
            "insertDebugMarker" => T<string>?markerLabel ^-> T<unit>
            "popDebugGroup" => T<unit> ^-> T<unit>
            "pushDebugGroup" => T<string>?groupLabel ^-> T<unit>
            "resolveQuerySet" => GPUQuerySet?querySet * T<int>?firstQuery * T<int>?queryCount * GPUBuffer?destination * T<int>?destinationOffset ^-> T<unit>
            "writeTimestamp" => GPUQuerySet?querySet * T<int>?queryIndex ^-> T<unit>
        ]

    let GPUPipelineLayout = 
        Class "GPUPipelineLayout" 
        |=> Inherits GPULabel

    let GPUCompilationMessage = 
        Class "GPUCompilationMessage" 
        |+> Instance [
            "length" =? T<int>
            "lineNum" =? T<int>
            "linePos" =? T<int>
            "message" =? T<string>
            "offset" =? T<int>
            "type" =? GPUCompilationMessageType.Type
        ]

    let GPUCompilationInfo = 
        Class "GPUCompilationInfo" 
        |+> Instance [
            "messages" =? !| GPUCompilationMessage
        ]
        
    let GPUShaderModule = 
        Class "GPUShaderModule"
        |=> Inherits GPULabel
        |+> Instance [
            "getCompilationInfo" => T<unit> ^-> T<Promise<_>>[GPUCompilationInfo]
        ]

    let GPUProgrammableStage = 
        Pattern.Config "GPUProgrammableStage" {
            Required = [
                "entryPoint", T<string>
                "module", GPUShaderModule.Type
            ]
            Optional = [
                // Pipeline-overridable constants, keyed by name.
                "constants", T<obj>
            ]
        }

    let GPUComputePipelineDescriptor = 
        Pattern.Config "GPUComputePipelineDescriptor" {
            Required = [
                "compute", GPUProgrammableStage.Type
                "layout", T<string> + GPUPipelineLayout.Type
            ]
            Optional = [
                "label", T<string>
            ]
        }

    let GPUPipelineLayoutDescriptor = 
        Pattern.Config "GPUPipelineLayoutDescriptor" {
            Required = [
                "bindGroupLayouts", !| GPUBindGroupLayout
            ]
            Optional = [
                "label", T<string>
            ]
        }

    let GPUQuerySetDescriptor = 
        Pattern.Config "GPUQuerySetDescriptor" {
            Required = [
                "count", T<int>
                "type", GPUQueryType.Type
            ]
            Optional = [
                "label", T<string>
            ]
        }

    let GPURenderBundleEncoderDescriptor = 
        Pattern.Config "GPURenderBundleEncoderDescriptor" {
            Required = [
                "colorFormats", !| GPUFormat.Type
            ]
            Optional = [
                "depthReadOnly", T<bool>
                "depthStencilFormat", GPUFormat.Type
                "label", T<string>
                "sampleCount", T<int>
                "stencilReadOnly", T<bool>
            ]
        }

    let StencilFaceState =
        Pattern.Config "StencilFaceState" {
            Required = []
            Optional = [
                "compare", Compare.Type
                "depthFailOp", StencilOperation.Type
                "failOp", StencilOperation.Type
                "passOp", StencilOperation.Type
            ]
        }

    let DepthStencilObject =
        Pattern.Config "DepthStencilObject" {
            Required = [
                "depthCompare", Compare.Type
                "depthWriteEnabled", T<bool>
                "format", GPUFormat.Type
            ]
            Optional = [
                "depthBias", T<int>
                "depthBiasClamp", T<int>
                "depthBiasSlopeScale", T<int>
                "stencilBack", StencilFaceState.Type
                "stencilFront", StencilFaceState.Type                
                "stencilReadMask", T<uint>
                "stencilWriteMask", T<uint>
            ]
        }    

    let BlendComponent =
        Pattern.Config "BlendComponent" {
            Required = []
            Optional = [
                "dstFactor", BlendFactor.Type
                "operation", BlendOperation.Type
                "srcFactor", BlendFactor.Type
            ]
        }

    let BlendState =
        Pattern.Config "BlendState" {
            Required = []
            Optional = [
                "alpha", BlendComponent.Type
                "color", BlendComponent.Type
            ]
        }

    let ColorTargetState =
        Pattern.Config "ColorTargetState" {
            Required = [
                "format", GPUFormat.Type
            ]
            Optional = [
                "blend", BlendState.Type
                "writeMask", T<int>
            ]
        }

    let FragmentObject = 
        Pattern.Config "FragmentObject" {
            Required = [
                "entryPoint", T<string>
                "module", GPUShaderModule.Type
            ]
            Optional = [
                "constants", T<obj>
                "targets", !| ColorTargetState.Type
            ]
        }

    let MultisampleObject =
        Pattern.Config "MultisampleObject" {
            Required = []
            Optional = [
                "alphaToCoverageEnabled", T<bool>
                "count", T<int>
                "mask", T<uint>
            ]
        }

    let PrimitiveObject =
        Pattern.Config "PrimitiveObject" {
            Required = []
            Optional = [
                "cullMode", CullMode.Type
                "frontFace", FrontFace.Type
                "stripIndexFormat", StripIndexFormat.Type
                "topology", Topology.Type
                "unclippedDepth", T<bool>
            ]
        }   

    let VertexAttribute =
        Pattern.Config "VertexAttribute" {
            Required = [
                "format", GPUVertexFormat.Type
                "offset", T<int>
                "shaderLocation", T<int>
            ]
            Optional = []
        }

    let VertexBufferLayout =
        Pattern.Config "VertexBufferLayout" {
            Required = [
                "arrayStride", T<int>
                "attributes", !| VertexAttribute
            ]
            Optional = [
                "stepMode", StepMode.Type
            ]
        }

    let VertexObject =
        Pattern.Config "VertexObject" {
            Required = [
                "entryPoint", T<string>
                "module", GPUShaderModule.Type
            ]
            Optional = [
                "constants", T<obj>
                "buffers", !| VertexBufferLayout.Type
            ]
        }

    let GPURenderPipelineDescriptor =
        Pattern.Config "GPURenderPipelineDescriptor" {
            Required = [
                "layout", T<string> + GPUPipelineLayout
                "vertex", VertexObject.Type
            ]
            Optional = [
                "depthStencil", DepthStencilObject.Type
                "fragment", FragmentObject.Type
                "label", T<string>
                "multisample", MultisampleObject.Type
                "primitive", PrimitiveObject.Type
            ]
        }
        

    let GPUSamplerDescriptor = 
        Pattern.Config "GPUSamplerDescriptor" {
            Required = []
            Optional = [
                "addressModeU", AddressMode.Type
                "addressModeV", AddressMode.Type
                "addressModeW", AddressMode.Type
                "compare", Compare.Type
                "label", T<string>
                "lodMinClamp", T<float>
                "lodMaxClamp", T<float>
                "maxAnisotropy", T<int>
                "magFilter", FilterMode.Type
                "minFilter", FilterMode.Type
                "mipmapFilter", FilterMode.Type
            ]
        }

    let GPUShaderModuleDescriptor =
        Pattern.Config "GPUShaderModuleDescriptor" {
            Required = [
                "code", T<string>
            ]
            Optional = [
                "hints", !| T<obj>
                "label", T<string>
                "sourceMap", T<string>
            ]
        }

    let SizeObject =
        Pattern.Config "SizeObject" {
            Required = [
                "width", T<int>
            ]
            Optional = [
                "height", T<int>
                "depthOrArrayLayers", T<int>
            ]
        }

    let GPUTextureDescriptor =
        Pattern.Config "GPUTextureDescriptor" {
            Required = [
                "format", GPUFormat.Type
                "size",  ArrayOrGPUExtent3D
                "usage", T<int>
            ]
            Optional = [
                "dimension", TextureDimension.Type
                "label", T<string>
                "mipLevelCount", T<int>
                "sampleCount", T<int>
                "viewFormats", !| GPUFormat.Type
            ]
        }

    let GPUExternalTextureDescriptor =
        Pattern.Config "GPUExternalTextureDescriptor" {
            Required = [
                "source", T<HTMLVideoElement>
            ]
            Optional = [
                "colorSpace", VideoColorSpace.Type
                "label", T<string>
            ]
        }

    let GPUError =
        Class "GPUError"
        |+> Instance [
            "message" =? T<string>
        ]

    let GPUInternalError =
        Class "GPUInternalError"
        |=> Inherits GPUError
        |+> Static [
            Constructor(T<string>?message)
        ]

    let GPUOutOfMemoryError =
        Class "GPUOutOfMemoryError"
        |=> Inherits GPUError
        |+> Static [
            Constructor(T<string>?message)
        ]

    let GPUValidationError =
        Class "GPUValidationError"
        |=> Inherits GPUError
        |+> Static [
            Constructor(T<string>?message)
        ]

    let GPUPopErrorScopeResult = GPUInternalError + GPUOutOfMemoryError + GPUValidationError 
        
    let GPUDevice =
        Class "GPUDevice"
        |=> Inherits T<Dom.EventTarget>
        |+> Instance [
            "features" =? GPUSupportedFeatures
            "label" =? T<string>
            "limits" =? GPUSupportedLimits
            "lost" =? T<Promise<_>>[GPUDeviceLostInfo]
            "queue" =? GPUQueue

            "createBindGroup" => GPUBindGroupDescriptor?descriptor ^-> GPUBindGroup
            "createBindGroupLayout" => GPUBindGroupLayoutDescriptor?descriptor ^-> GPUBindGroupLayout
            "createBuffer" => GPUBufferDescriptor?descriptor ^-> GPUBuffer
            "createCommandEncoder" => !?GPUCommandEncoderDescriptor?descriptor ^-> GPUCommandEncoder
            "createComputePipeline" => GPUComputePipelineDescriptor?descriptor ^-> GPUComputePipeline
            "createComputePipelineAsync" => GPUComputePipelineDescriptor?descriptor ^-> T<Promise<_>>[GPUComputePipeline]
            "createPipelineLayout" => GPUPipelineLayoutDescriptor?descriptor ^-> GPUPipelineLayout
            "createQuerySet" => GPUQuerySetDescriptor?descriptor ^-> GPUQuerySet
            "createRenderBundleEncoder" => GPURenderBundleEncoderDescriptor?descriptor ^-> GPURenderBundleEncoder
            "createRenderPipeline" => GPURenderPipelineDescriptor?descriptor ^-> GPURenderPipeline
            "createRenderPipelineAsync" => GPURenderPipelineDescriptor?descriptor ^-> T<Promise<_>>[GPURenderPipeline]
            "createSampler" => !? GPUSamplerDescriptor?descriptor ^-> GPUSampler
            "createShaderModule" => GPUShaderModuleDescriptor?descriptor ^-> GPUShaderModule
            "createTexture" => GPUTextureDescriptor?descriptor ^-> GPUTexture
            "destroy" => T<unit> ^-> T<unit>
            "importExternalTexture" => GPUExternalTextureDescriptor?descriptor ^-> GPUExternalTexture
            "popErrorScope" => T<unit> ^-> T<Promise<_>>[GPUPopErrorScopeResult]
            "pushErrorScope" => GPUErrorFilter?filter ^-> T<unit>
        ]

    let GPUQueueDescriptor =
        Pattern.Config "GPUQueueDescriptor" {
            Required = []
            Optional = [
                "label", T<string>
            ]
        }

    let GPURequestAdapterOptions =
        Pattern.Config "GPURequestAdapterOptions" {
            Required = []
            Optional = [
                "powerPreference", PowerPreference.Type
                "forceFallbackAdapter", T<bool>
                "compatibleSurface", T<HTMLCanvasElement> + T<OffscreenCanvas>
            ]
        }

    let RequestDeviceDescriptor =
        Pattern.Config "RequestDeviceDescriptor" {
            Required = []
            Optional = [
                "defaultQueue", GPUQueueDescriptor.Type
                "label", T<string>
                "requiredFeatures", !| T<string>
                "requiredLimits", GPUSupportedLimits.Type
            ]
        }

    let GPUAdapter = 
        Class "GPUAdapter"
        |+> Instance [
            "features" =? GPUSupportedFeatures.Type
            "info" =? GPUAdapterInfo.Type
            "isFallbackAdapter" =? T<bool>
            "limits" =? GPUSupportedLimits.Type

            "requestAdapterInfo" => T<unit> ^-> T<Promise<_>>[GPUAdapterInfo]
            |> ObsoleteWithMessage "requestAdapterInfo() was removed from the WebGPU spec; use the info property instead."
            "requestDevice" => !? RequestDeviceDescriptor?descriptor ^-> T<Promise<_>>[GPUDevice]
        ] 

    let GPU = 
        Class "GPU"
        |+> Instance [
            "wgslLanguageFeatures" =? GPUWGSLLanguageFeatures.Type

            "requestAdapter" => !? GPURequestAdapterOptions?options ^-> T<Promise<_>>[GPUAdapter]
            "getPreferredCanvasFormat" => T<unit> ^-> GPUFormat.Type
        ]

    let GPUCanvasConfiguration = 
        Pattern.Config "GPUCanvasConfiguration" {
            Required = [
                "device", GPUDevice.Type
                "format", GPUFormat.Type
            ]
            Optional = [
                "alphaMode", AlphaMode.Type
                "colorSpace", ColorSpace.Type
                "usage", T<int>
                "viewFormats", !| GPUFormat.Type
            ]
        }

    let GPUCanvasContext =
        Class "GPUCanvasContext"
        |+> Instance [
            "canvas" =? T<HTMLCanvasElement> + T<OffscreenCanvas>

            "configure" => GPUCanvasConfiguration?configuration ^-> T<unit>
            "getCurrentTexture" => T<unit> ^-> GPUTexture
            "unconfigure" => T<unit> ^-> T<unit>
        ]

    let GPUUncapturedErrorEventOptions = 
        Pattern.Config "GPUUncapturedErrorEventOptions" {
            Required = ["error", GPUError.Type]
            Optional = []
        }

    let GPUUncapturedErrorEvent = 
        Class "GPUUncapturedErrorEvent"
        |=> Inherits T<Dom.Event>
        |+> Static [
            Constructor (T<string>?``type`` * GPUUncapturedErrorEventOptions?options)
        ]
        |+> Instance [
            "error" =? GPUError.Type
        ]

    let GPUPipelineErrorReason = 
        Pattern.EnumStrings "GPUPipelineErrorReason" [
            "validation"
            "internal"
        ]

    let GPUPipelineError = 
        Class "GPUPipelineError"
        |=> Inherits T<Dom.Exception>
        |+> Static [
            Constructor (GPUPipelineErrorReason.Type?reason * !? T<string>?message)
        ]
        |+> Instance [
            "reason" =? GPUPipelineErrorReason.Type
        ]

        
    let Assembly =
        Assembly [
            Namespace "WebSharper.WebGPU" [
                //Class
                GPU
                GPUAdapter
                GPUAdapterInfo
                GPUBindGroup
                GPUBindGroupLayout
                GPUBuffer
                GPUCanvasContext
                GPUCommandBuffer
                GPUCommandEncoder
                GPUCompilationInfo
                GPUCompilationMessage
                GPUComputePassEncoder
                GPUComputePipeline
                GPUDevice
                GPUDeviceLostInfo
                GPUError
                GPUExternalTexture
                GPUInternalError
                GPUOutOfMemoryError
                GPUPipelineError
                GPUPipelineLayout
                GPUQuerySet
                GPUQueue
                GPURenderBundle
                GPURenderBundleEncoder
                GPURenderPassEncoder
                GPURenderPipeline
                GPUSampler
                GPUShaderModule
                GPUSupportedFeatures
                GPUSupportedLimits
                GPUTexture
                GPUTextureView
                GPUUncapturedErrorEvent
                //Enum
                ColorSpace; AlphaMode; GPUErrorFilter; VideoColorSpace; TextureDimension; FilterMode; AddressMode; GPUVertexFormat; StepMode; Topology
                StripIndexFormat; FrontFace; CullMode; GPUColorWrite; BlendOperation; BlendFactor; StencilOperation; Compare; GPUCompilationMessageType
                IndexFormat; TimestampLocation; StoreOp; LoadOp; GPUComputePassLocation; GPUQueryType; PowerPreference; GPUDeviceLostInfoReason
                GPUAspect; DestinationColorSpace; GPUTextureDimension; DescriptorDimension; GPUBufferMapState; GPUMapMode; GPUShaderStage
                GPUFormat; GPUTextureUsage; GPUBufferUsage; GPUBufferBindingType; GPUSamplerBindingType; GPUTextureSampleType
                GPUStorageTextureAccess
                //Interface
                GPUTextureDataLayout; GPUImageDestination; GPUDestination; DestinationOrigin; CreateViewDescriptor; GPULabel; GPUImageSource
                SourceOrigin; GPUWGSLLanguageFeatures; RenderPassDescriptor; DepthStencilAttachmentObject
                ColorAttachmentObject; Colors; TimestampWrite; GPUComputePassDescriptor; GPUComputePassTimestampWrites; GPUCommandEncoderDescriptor
                GPUBufferDescriptor; GPUBindGroupDescriptor; GPUBindGroupLayoutDescriptor; GPUBindGroupLayoutEntry; GPUBindGroupEntry
                GPUBufferBinding; GPUBufferBindingLayout; GPUSamplerBindingLayout; GPUTextureBindingLayout
                GPUStorageTextureBindingLayout; GPUExternalTextureBindingLayout; Size; GPUExtent3D
                GPUSamplerDescriptor; GPURenderPipelineDescriptor; VertexObject; VertexBufferLayout; VertexAttribute; PrimitiveObject; MultisampleObject
                FragmentObject; ColorTargetState; BlendState; BlendComponent; DepthStencilObject; StencilFaceState; GPURenderBundleEncoderDescriptor
                GPUQuerySetDescriptor; GPUPipelineLayoutDescriptor; GPUComputePipelineDescriptor; GPUProgrammableStage; GPUTextureCopyView
                GPUBufferCopyView; GPURenderEncoder; GPUPipelineErrorReason; GPUUncapturedErrorEventOptions; GPUCanvasConfiguration
                RequestDeviceDescriptor; GPUQueueDescriptor; GPURequestAdapterOptions; GPUExternalTextureDescriptor; GPUTextureDescriptor; SizeObject; GPUShaderModuleDescriptor
            ]
        ]

[<Sealed>]
type Extension() =
    interface IExtension with
        member ext.Assembly =
            Definition.Assembly

[<assembly: Extension(typeof<Extension>)>]
do ()
