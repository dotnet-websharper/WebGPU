namespace WebSharper.WebGPU

open WebSharper
open WebSharper.JavaScript
open WebSharper.InterfaceGenerator

module Definition =
    let powerPreference = 
        Pattern.EnumStrings "powerPreference" [
            "undefined"
            "low-power"
            "high-performance"
        ]

    let GPUDeviceLostInfoReason = 
        Pattern.EnumStrings "GPUDeviceLostInfoReason" ["destroyed"]

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

    let GPUBufferMapSate = 
        Pattern.EnumStrings "GPUBufferMapSate" [
            "unmapped"; "pending"; "mapped"
        ]

    let GPUMapMode = 
        Pattern.EnumStrings "GPUMapMode" [
            "READ"; "WRITE"
        ]

    let GPUShaderStage =
        Pattern.EnumStrings "GPUShaderStage" [
            "COMPUTE"; 
            "FRAGMENT"; 
            "VERTEX"
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
        Pattern.EnumInlines "GPUTextureUsage" [
            "COPY_SRC", "1"
            "COPY_DST", "2"
            "TEXTURE_BINDING", "4"
            "STORAGE_BINDING", "8"
            "RENDER_ATTACHMENT", "16"
        ]

    let GPUBufferUsage = 
        Pattern.EnumInlines "GPUBufferUsage" [
            "MAP_READ", "1"
            "MAP_WRITE", "2"
            "COPY_SRC", "4"
            "COPY_DST", "8"
            "INDEX", "16"
            "VERTEX", "32"
            "UNIFORM", "64"
            "STORAGE", "128"
            "INDIRECT", "256"
            "QUERY_RESOLVE", "512"
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

    let GPUColorWrite =
        Pattern.EnumStrings "GPUColorWrite" [
            "RED"
            "GREEN"
            "BLUE"
            "ALPHA"
            "ALL"
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
        Pattern.EnumStrings "ErrorFilter" [
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

    let WGSLLanguageFeatures = 
        Class "WGSLLanguageFeatures"
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
        |=> Inherits WGSLLanguageFeatures

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
            Required = ["source", T<obj>]
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
            "usage" =? GPUTextureUsage.Type
            "width" =? T<int>

            "createView" => CreateViewDescriptor?descrptor ^-> GPUTextureView
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

            "mapState" =? GPUBufferMapSate.Type
            "size" =? T<int>
            "usage" =? GPUBufferUsage.Type

            "destroy" => T<unit> ^-> T<unit>
            "getMappedRange" => !? T<int>?offset * !? T<int>?size ^-> T<ArrayBuffer>
            "mapAsync" => !? T<int>?offset * !? T<int>?size ^-> T<Promise<unit>>
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

    let GPUQueue =
        Class "GPUQueue"
        |+> Instance [
            "label" =@ T<string>

            "copyExternalImageToTexture" => GPUImageSource?source * GPUImageDestination?destination * ArrayOrGPUExtent3D?copySize ^-> T<unit>
            "onSubmittedWorkDone" => T<unit> ^-> T<Promise<unit>>
            "submit" => (!|GPUCommandBuffer)?commandBuffers ^-> T<unit>
            "writeBuffer" => GPUBuffer?buffer * T<int>?bufferOffset * T<obj>?data * !? T<int>?dataOffset * !? T<int>?size ^-> T<unit>
            "writeTexture" => GPUDestination?destination * T<obj>?data * GPUTextureDataLayout * ArrayOrSize?size ^-> T<unit>
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

    let EntryObject = 
        Pattern.Config "EntryObject" {
            Required = [
                "binding", T<int>
                "visibility", !| GPUShaderStage.Type
            ]
            Optional = []
        }

    let GPUBindGroupLayoutDescriptor = 
        Pattern.Config "GPUBindGroupLayoutDescriptor" {
            Required = ["entries", !| EntryObject]
            Optional = ["label", T<string>]
        }

    let GPUBindGroupDescriptor = 
        Pattern.Config "GPUBindGroupDescriptor" {
            Required = [
                "layout", GPUBindGroupLayout.Type
            ]
            Optional = []
        }
        |=> Inherits GPUBindGroupLayoutDescriptor

    let GPUBufferDescriptor =
        Pattern.Config "GPUBufferDescriptor" {
            Required = [
                "size", T<int>
                "usage", GPUBufferUsage.Type
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
            "type" =? GPUQueryType

            "destroy" => T<unit> ^-> T<unit>
        ]

    
        
    let Assembly =
        Assembly [
            Namespace "WebSharper.WebGPU" [
                 
            ]
        ]

[<Sealed>]
type Extension() =
    interface IExtension with
        member ext.Assembly =
            Definition.Assembly

[<assembly: Extension(typeof<Extension>)>]
do ()
