#pragma checksum "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f8bbfea26bfddc8b9346655ba4110e06dff085b0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Solicitante_Index), @"mvc.1.0.view", @"/Views/Solicitante/Index.cshtml")]
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
#nullable restore
#line 1 "/Users/rodrigolira/Proyectos/hollidays/Views/_ViewImports.cshtml"
using mvc_dotnet;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/rodrigolira/Proyectos/hollidays/Views/_ViewImports.cshtml"
using mvc_dotnet.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f8bbfea26bfddc8b9346655ba4110e06dff085b0", @"/Views/Solicitante/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c384c19b7b8c301be84450f34c8071306f6ce98f", @"/Views/_ViewImports.cshtml")]
    public class Views_Solicitante_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "2", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "3", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "4", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml"
  
    ViewData["Title"] = "Solicitante|Desconéctate";


#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<style>
    body{
        background-image: url(""images/doodle-background.png"");
        background-repeat: no-repeat;
        background-size: cover;
        background-position: bottom right; 
        color : #5c2a7e;
    }

    #i_doodle{
        width:30%;
        position:absolute;
        bottom:0px;
        right:0px;
    }

    .b_data{
        
    }
</style>

<link rel=""stylesheet"" href=""lib/simple-calendar/dist/simple-calendar.css"" />
<link rel=""stylesheet"" href=""lib/bootstrap-select/dist/css/bootstrap-select.css"" />

<div>
    <img id=""i_doodle"" src=""images/imagen.png"">
</div>

<div class=""container"">
    <div class=""row"">
        <div class=""col-6"">
            
                <div class=""row"" style=""color:#b41547;"">
                    <h3 class=""font-weight-bold"">Juan Alberto Rodriguez Trejo</h3>
                    <h4>IDSAP: 2839430</h4>
                </div>
        </div>
    </div>
    <div class=""row"">
        <div class=""col-8"">
            
            <div class=""row"">
             ");
            WriteLiteral(@"   <div class=""col-6 font-weight-bold"">ANTIGÜEDAD:</div>
                <div class=""col-6 font-weight-bold"">FECHA ANIVERSARIO:</div>
            </div>
            <div class=""row"">
                <div class=""col-6 font-weight-bold"">DÍAS POR DISFRUTAR:</div>
                <div class=""col-6 font-weight-bold"">DÍAS CADUCAN:</div>
            </div>
            <div class=""row"">
                <div class=""col-6 font-weight-bold"">DÍAS TOMADOS:</div>
                <div class=""col-6 font-weight-bold"">PERIODO:</div>
            </div>
           
        </div> 
    </div>

    <div class=""row mt-5"">
        <div class=""col-4"">
            <div class=""container"">
                <div class=""row"">
                    <div class=""dropdown col-12"">
                        <label for=""FormControlSelect"" class=""font-weight-bold"">SELECCIONA TU DESCONECTE:</label>
                        <select class=""selectpicker"">
                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f8bbfea26bfddc8b9346655ba4110e06dff085b06426", async() => {
                WriteLiteral("VACACIONES");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f8bbfea26bfddc8b9346655ba4110e06dff085b07601", async() => {
                WriteLiteral("ADELANTO VACACIONES");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f8bbfea26bfddc8b9346655ba4110e06dff085b08785", async() => {
                WriteLiteral("PERMISO MATERNIDAD");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f8bbfea26bfddc8b9346655ba4110e06dff085b09968", async() => {
                WriteLiteral("PERMISO PATERNIDAD");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                        </select>
                    </div>
                </div>
            </div>
        </div>
        <div class=""col-4"">
            <label for=""FormControlSelect"" class=""font-weight-bold"">SELECCIONA INICIO Y FIN DE TU DESCONECTE:</label>
            <div id=""d_calendar""></div>
        </div>
        <div class=""col-4"">
            <div class=""row mt-5"">
                <div class=""col-6 font-weight-bold"">FECHA INICIO:<p id=""p_fecha_inicio""></p></div>
                <div class=""col-6 font-weight-bold"">FECHA FIN:<p id=""p_fecha_fin""></p></div>
            </div>
            <div class=""row mt-2"">
                <div class=""col-6 d-flex justify-content-center"">
                    <input type=""submit"" class=""btn btn-desconectate"" value=""SOLICITAR"" />
                </div>
            </div>
        </div>
    </div>
    


    <div class=""row"">
        <div class=""col-10"">
            <table class=""table table-striped"">
                <thead style=""background-color:");
            WriteLiteral(@" #821c61; color: white;"">
                    <th>FOLIO</th>
                    <th>TIPO</th>
                    <th>FECHA INICIO</th>
                    <th>FECHA FIN</th>
                    <th>APROBADOR</th>
                    <th>ESTATUS</th>
                </thead>
                <tbody>

                </tbody>
            </table>
        </div>
    </div>
</div>

<script src=""lib/simple-calendar/dist/jquery.simple-calendar.js""></script>
<script src=""lib/bootstrap-select/dist/js/bootstrap-select.js""></script>

<script>
    $(function() {
        var vacaciones = new Array();

        $(""#d_calendar"").simpleCalendar({
        //Defaults options below
        //string of months starting from january
        months: ['enero','febrero','marzo','abril','mayo','junio','julio','agosto','septiembre','octubre','noviembre','diciembre'],
        days: ['domingo','lunes','martes','miercoles','jueves','viernes','sabado'],
        onDateSelect: function (date, events) {
            //Se agrega la fecha al ar");
            WriteLiteral(@"ray de vacaciones
            //Falta agregar una validacion para remover y no repetir fechas.
            var fecha = date.valueOf();
            if (vacaciones.indexOf(fecha) == -1) {
                vacaciones.push(fecha);
            }
            else {
                vacaciones.splice(vacaciones.indexOf(fecha), 1);
            }

            if (vacaciones.length != 0) {
                vacaciones.sort(function (a, b) { return a - b });
                var fecha_inicio = new Date(vacaciones[0]);
                var fecha_fin = new Date(vacaciones[vacaciones.length - 1]);
                $(""#p_fecha_inicio"").html(fecha_inicio.toISOString().substr(0,10));
                $(""#p_fecha_fin"").html(fecha_fin.toISOString().substr(0,10));
            } else {
                $(""#p_fecha_inicio"").html('');
                $(""#p_fecha_fin"").html('');
            }
            
            
            

            console.log(vacaciones);
            }
        });
    });
</script>");
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
