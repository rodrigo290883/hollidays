#pragma checksum "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e37e2dd23777b81db9851c6bc646124b6de3dcb1"
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
using desconectate;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/rodrigolira/Proyectos/hollidays/Views/_ViewImports.cshtml"
using desconectate.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e37e2dd23777b81db9851c6bc646124b6de3dcb1", @"/Views/Solicitante/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1185a8b3bb4e90f1e146f85307bfc4dace37d116", @"/Views/_ViewImports.cshtml")]
    public class Views_Solicitante_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "2", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "5", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "3", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "4", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
    Layout = "~/Views/Shared/_LayoutSession.cshtml";


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

    #i_avatar {
        width: 200px;
        position: absolute;
        top: 0px;
        left: -200px;
    }
</style>

<link rel=""stylesheet"" href=""lib/simple-calendar/dist/simple-calendar.css"" />
<link rel=""stylesheet"" href=""lib/bootstrap-select/dist/css/bootstrap-select.css"" />

<div>
    <img id=""i_doodle"" src=""images/imagen.png"">
</div>

<div class=""container"">
    <div class=""row"">
        <div class=""col-1"">
            <img id=""i_avatar"" src=""images/Avatar.gif"" />
        </div>
        <div class=""col-6"" style=""color:#b41547;"">
            
                
                    <h3 class=""font-weight-bold"">Victoria Rodriguez Trejo</h3>
                    <h4>IDSA");
            WriteLiteral(@"P: 2839430</h4>
                
        </div>
        <div class=""col-5 text-center"">
            <h4>DESCARGA TU PÓLIZA DE GMM</h4>
            <button id=""b_descargar"" class=""btn btn-desconectate"" >DESCARGAR</button>
        </div>
    </div>
    <div class=""row"">
        <div class=""col-8"">
            
            <div class=""row"">
                <div class=""col-6 font-weight-bold"">ANTIGÜEDAD: 3 AÑOS</div>
                <div class=""col-6 font-weight-bold"">FECHA ANIVERSARIO: 21 OCTUBRE</div>
            </div>
            <div class=""row"">
                <div class=""col-6 font-weight-bold"">DÍAS POR DISFRUTAR: 11</div>
                <div class=""col-6 font-weight-bold"">DÍAS CADUCAN: 22 OCTUBRE</div>
            </div>
            <div class=""row"">
                <div class=""col-6 font-weight-bold"">DÍAS TOMADOS: 2</div>
                <div class=""col-6 font-weight-bold"">PERIODO: 2019</div>
            </div>
           
        </div> 
    </div>

    <div class=""row mt-5"">
        <div class=""co");
            WriteLiteral(@"l-4"">
            <div class=""container"">
                <div class=""row"">
                    <div class=""dropdown col-12"">
                        <label for=""FormControlSelect"" class=""font-weight-bold"">SELECCIONA TU DESCONECTE:</label>
                        <select class=""selectpicker"">
                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e37e2dd23777b81db9851c6bc646124b6de3dcb17209", async() => {
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e37e2dd23777b81db9851c6bc646124b6de3dcb18384", async() => {
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e37e2dd23777b81db9851c6bc646124b6de3dcb19568", async() => {
                WriteLiteral("PERMISO");
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e37e2dd23777b81db9851c6bc646124b6de3dcb110740", async() => {
                WriteLiteral("PERMISO MATERNIDAD");
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
            WriteLiteral("\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e37e2dd23777b81db9851c6bc646124b6de3dcb111924", async() => {
                WriteLiteral("PERMISO PATERNIDAD");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
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
                <div class=""col-6 text-center"">
                    <input id=""b_solicitar"" type=""submit"" class=""btn btn-desconectate"" value=""SOLICITAR"" />
                </div>
            </div>
        </div>
    </div>
    


    <div class=""row"">
        <div class=""col-10"">
            <table class=""table table-striped"">
                <thead style=""background-color: ");
            WriteLiteral(@"#821c61; color: white;"">
                    <th>FOLIO</th>
                    <th>SOLICITUD</th>
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
        var disfrutar = 11;
        var bandera = 0;

        $(""#b_solicitar"").click(function () {
            if (bandera == 0) {
                swal(""Felicidades"", ""Tu solicitud se registro Correctamente"", ""success"");
            }
            else {
                swal(""Cuidado"", ""Tu seleccion sobrepasa los dias permitidos"", ""warning"");
            }
        });

        $(""#d_calendar"").simpleCalendar({
        /");
            WriteLiteral(@"/Defaults options below
        //string of months starting from january

        months: ['enero','febrero','marzo','abril','mayo','junio','julio','agosto','septiembre','octubre','noviembre','diciembre'],
        days: ['domingo','lunes','martes','miercoles','jueves','viernes','sabado'],
        onDateSelect: function (date, events) {
            //Se agrega la fecha al array de vacaciones
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
                // se cuentan los dias habiles
                var cont = 0;
                for (var i = 0; i < vacaciones.length; i++) {
                    var aux = vacaciones[i];
                ");
            WriteLiteral(@"    var dia = new Date(aux);
                    var dia_sem = dia.getDay();
                    if (dia_sem > 0 && dia_sem < 6)
                        cont++;
                }

                if (cont > disfrutar) {
                    swal(""Cuidado"",""Tu seleccion sobrepasa los dias permitidos"", ""warning"");
                    bandera = 1;
                }
                else {
                    bandera = 0;
                }
                // se cuentan los dias habiles
                var fecha_inicio = new Date(vacaciones[0]);
                var fecha_fin = new Date(vacaciones[vacaciones.length - 1]);
                $(""#p_fecha_inicio"").html(fecha_inicio.toISOString().substr(0,10));
                $(""#p_fecha_fin"").html(fecha_fin.toISOString().substr(0,10));
            } else {
                $(""#p_fecha_inicio"").html('');
                $(""#p_fecha_fin"").html('');
            }
            
            //console.log(vacaciones);
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
