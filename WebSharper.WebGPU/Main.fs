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
