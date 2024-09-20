namespace WebSharper.WebGPU

open WebSharper
open WebSharper.JavaScript
open WebSharper.InterfaceGenerator

module Definition =


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
