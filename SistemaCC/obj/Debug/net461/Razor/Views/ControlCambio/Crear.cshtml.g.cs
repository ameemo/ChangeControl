#pragma checksum "C:\Users\ACER\source\repos\AmeDaf\ControlCambios\SistemaCC\Views\ControlCambio\Crear.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5ee6ef42c62c0225b1d0339a7ff4f887adafbdf7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ControlCambio_Crear), @"mvc.1.0.view", @"/Views/ControlCambio/Crear.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/ControlCambio/Crear.cshtml", typeof(AspNetCore.Views_ControlCambio_Crear))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\ACER\source\repos\AmeDaf\ControlCambios\SistemaCC\Views\_ViewImports.cshtml"
using SistemaCC;

#line default
#line hidden
#line 2 "C:\Users\ACER\source\repos\AmeDaf\ControlCambios\SistemaCC\Views\_ViewImports.cshtml"
using SistemaCC.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5ee6ef42c62c0225b1d0339a7ff4f887adafbdf7", @"/Views/ControlCambio/Crear.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"82ee2762a268df865ade1898bdf4fb95469fbf4c", @"/Views/_ViewImports.cshtml")]
    public class Views_ControlCambio_Crear : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "C:\Users\ACER\source\repos\AmeDaf\ControlCambios\SistemaCC\Views\ControlCambio\Crear.cshtml"
  
    ViewData["Title"] = "Crear";

#line default
#line hidden
            BeginContext(43, 3302, true);
            WriteLiteral(@"<div class=""col-md-12 seccion"">
    <h1>Nuevo control del cambio</h1>
</div>
<div class=""accordion"" id=""accordionFormulario"">
    <div class=""card"">
        <div class=""card-header"" id=""Titulo1"">
            <h2 class=""mb-0"">
                <button class=""btn btn-collapse"" type=""button"" data-toggle=""collapse"" data-target=""#Section1"" aria-expanded=""true"" aria-controls=""Section1"">
                    Collapsible Group Item #1
                </button>
            </h2>
        </div>
        <div id=""Section1"" class=""collapse show"" aria-labelledby=""Titulo1"" data-parent=""#accordionFormulario"">
            <div id=""max_fila"" class=""card-body"">
                <div class=""col-md-12 contenedor_agregar"">
                    <a id=""boton_agregar"" class=""btn btn-success"" href=""#"" onclick=""agregar('0')"">Agregar</a>
                </div>
            </div>
        </div>
    </div>
    <div class=""card"">
        <div class=""card-header"" id=""Titulo2"">
            <h2 class=""mb-0"">
               ");
            WriteLiteral(@" <button class=""btn btn-collapse collapsed"" type=""button"" data-toggle=""collapse"" data-target=""#Seccion2"" aria-expanded=""false"" aria-controls=""Seccion2"">
                    Collapsible Group Item #2
                </button>
            </h2>
        </div>
        <div id=""Seccion2"" class=""collapse"" aria-labelledby=""Titulo2"" data-parent=""#accordionFormulario"">
            <div class=""card-body"">
                Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher vice lomo. Leggings occaecat craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven't heard of them accusamus labore sustainable VHS");
            WriteLiteral(@".
            </div>
        </div>
    </div>
    <div class=""card"">
        <div class=""card-header"" id=""Titulo3"">
            <h2 class=""mb-0"">
                <button class=""btn btn-collapse collapsed"" type=""button"" data-toggle=""collapse"" data-target=""#Seccion3"" aria-expanded=""false"" aria-controls=""Seccion3"">
                    Collapsible Group Item #3
                </button>
            </h2>
        </div>
        <div id=""Seccion3"" class=""collapse"" aria-labelledby=""Titulo3"" data-parent=""#accordionFormulario"">
            <div class=""card-body"">
                Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon tempor, sunt aliqua put a bird on it squid single-origin coffee nulla assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore wes anderson cred nesciunt sapiente ea proident. Ad vegan except");
            WriteLiteral("eur butcher vice lomo. Leggings occaecat craft beer farm-to-table, raw denim aesthetic synth nesciunt you probably haven\'t heard of them accusamus labore sustainable VHS.\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591