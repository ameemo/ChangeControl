#pragma checksum "C:\Users\ACER\source\repos\AmeDaf\ControlCambios\SistemaCC\Views\ServApp\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ea8f96e7b5df27d7324abb666e36bbbdea541486"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ServApp_Index), @"mvc.1.0.view", @"/Views/ServApp/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/ServApp/Index.cshtml", typeof(AspNetCore.Views_ServApp_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ea8f96e7b5df27d7324abb666e36bbbdea541486", @"/Views/ServApp/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"82ee2762a268df865ade1898bdf4fb95469fbf4c", @"/Views/_ViewImports.cshtml")]
    public class Views_ServApp_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("fancybox fancybox-iframe"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "ServApp", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Ver", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "C:\Users\ACER\source\repos\AmeDaf\ControlCambios\SistemaCC\Views\ServApp\Index.cshtml"
  
    ViewData["Title"] = "ServiciosAplicacioes";

#line default
#line hidden
            BeginContext(58, 833, true);
            WriteLiteral(@"    <div class=""row"">
        <div class=""col-md-10 seccion"">
            <h1>Servicios y aplicaciones</h1>
        </div>
        <div class=""col-md-2 seccion_boton"">
            <a class=""btn btn-success"" href=""#""><span class=""icon-plus-square""></span> Crear</a>
        </div>

        <table class=""table table-hover"">
            <thead class=""thead-info"">
                <tr>
                    <th>Columna 1</th>
                    <th>Columna 2</th>
                    <th class=""link""></th>
                    <th class=""link""></th>
                    <th class=""link""></th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>Valor 1</td>
                    <td>Valor 2</td>
                    <td class=""link"">
                        ");
            EndContext();
            BeginContext(891, 112, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d1ddf5ec8edf44649ed0ad130930890e", async() => {
                BeginContext(969, 30, true);
                WriteLiteral("<span class=\"icon-eye\"></span>");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1003, 843, true);
            WriteLiteral(@"
                    </td>
                    <td class=""link"">
                        <a href=""#""><span class=""icon-edit""></span></a>
                    </td>
                    <td class=""link"">
                        <a href=""#""><span class=""icon-lock""></span></a>
                    </td>
                </tr>
                <tr>
                    <td>Valor 5</td>
                    <td>Valor 1</td>
                    <td>Valor 8</td>
                    <td>Valor 2</td>
                    <td>3</td>
                </tr>
                <tr>
                    <td>Valor 3</td>
                    <td>Valor 4</td>
                    <td>Valor 9</td>
                    <td>Valor 1</td>
                    <td>11</td>
                </tr>
            </tbody>
        </table>
    </div>

");
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
