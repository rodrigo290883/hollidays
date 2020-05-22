#pragma checksum "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5d315e2dc3876d1392ef2c220d65f5c016b22774"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5d315e2dc3876d1392ef2c220d65f5c016b22774", @"/Views/Solicitante/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1185a8b3bb4e90f1e146f85307bfc4dace37d116", @"/Views/_ViewImports.cshtml")]
    public class Views_Solicitante_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<desconectate.Models.Solicitud>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Avatares", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("frmSolicitud"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 4 "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml"
  
    ViewData["Title"] = "Solicitante|Desconéctate";
    Layout = "~/Views/Shared/_LayoutSession.cshtml";



    Empleados empleado = ViewBag.empleado;

    string avatar = empleado.avatar;

    if (empleado.sexo == 'M')
    {
        if (empleado.meses_ultimo_desconecte <= 2)
            avatar = avatar+".png";
        else if (empleado.meses_ultimo_desconecte > 2 && empleado.meses_ultimo_desconecte <= 6)
            avatar = avatar + ".png";
        else
            avatar = avatar + ".png";
    }
    else
    {
        if (empleado.meses_ultimo_desconecte <= 2)
            avatar = avatar + ".png";
        else if (empleado.meses_ultimo_desconecte > 2 && empleado.meses_ultimo_desconecte <= 6)
            avatar = avatar + ".png";
        else
            avatar = avatar + ".png";
    }

    string antiguedad = (@empleado.antiguedad/12)+" años y "+(@empleado.antiguedad%12)+ " meses";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
    <style>
    body {
        background-image: url(""images/doodle-background.png"");
        background-repeat: no-repeat;
        background-size: cover;
        background-position: bottom right;
        color: #5c2a7e;
    }

    #i_doodle {
        width: 30%;
        position: absolute;
        bottom: 0px;
        right: 0px;
    }



    
    #i_avatar{
        width: 100%;
        position: relative;
        top: 0px;
        left: 0%;
    }

    </style>

<link rel=""stylesheet"" href=""lib/simple-calendar/dist/simple-calendar.css"" />
<link rel=""stylesheet"" href=""lib/bootstrap-select/dist/css/bootstrap-select.css"" />

<div>
    <img id=""i_doodle"" src=""images/imagen.png"">
</div>

<div class=""container"">
    <div class=""row"">
        <div class=""col-sm-12 col-md-2"">
            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5d315e2dc3876d1392ef2c220d65f5c016b227746646", async() => {
                WriteLiteral("\r\n                <img id=\"i_avatar\"");
                BeginWriteAttribute("class", " class=\"", 1884, "\"", 1892, 0);
                EndWriteAttribute();
                BeginWriteAttribute("src", " src=\"", 1893, "\"", 1913, 2);
                WriteAttributeValue("", 1899, "images/", 1899, 7, true);
#nullable restore
#line 75 "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml"
WriteAttributeValue("", 1906, avatar, 1906, 7, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
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
            WriteLiteral("\r\n        </div>\r\n        <div class=\"col-sm-12 col-md-5 align-self-center\" style=\"color:#b41547;\">\r\n\r\n\r\n            <h3 class=\"font-weight-bold\">");
#nullable restore
#line 81 "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml"
                                    Write(empleado.Nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n            <h4>IDSAP: ");
#nullable restore
#line 82 "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml"
                  Write(empleado.IdSAP);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h4>

        </div>
        <div class=""col-sm-12 col-md-5 text-center align-self-center"">
            <h4>DESCARGA TU PÓLIZA DE GMM</h4>
            <button id=""b_descargar"" class=""btn btn-desconectate"">DESCARGAR</button>
        </div>
    </div>
    <div class=""row"">
        <div class=""col-sm-12 col-md-8"">

            <div class=""row"">
                <div class=""col-sm-12 col-md-6 font-weight-bold"">ANTIGÜEDAD: ");
#nullable restore
#line 94 "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml"
                                                                        Write(antiguedad);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                <div class=\"col-sm-12 col-md-6 font-weight-bold\">FECHA ANIVERSARIO: ");
#nullable restore
#line 95 "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml"
                                                                               Write(empleado.fecha_ingreso.ToString().Substring(0, 10));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n            </div>\r\n            <div class=\"row\">\r\n                <div class=\"col-sm-12 col-md-6 font-weight-bold\">DÍAS POR DISFRUTAR: ");
#nullable restore
#line 98 "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml"
                                                                                Write(empleado.dias_disponibles);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                <div class=\"col-sm-12 col-md-6 font-weight-bold\">DÍAS CADUCAN: ");
#nullable restore
#line 99 "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml"
                                                                          Write(empleado.caducidad.ToString().Substring(0, 10));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n            </div>\r\n            <div class=\"row\">\r\n                <div class=\"col-sm-12 col-md-6 font-weight-bold\">DÍAS TOMADOS: </div>\r\n                <div class=\"col-sm-12 col-md-6 font-weight-bold\">PERIODO: ");
#nullable restore
#line 103 "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml"
                                                                     Write(ViewBag.periodo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n            </div>\r\n            <div class=\"row\">\r\n                <div class=\"col-sm-12 col-md-6 font-weight-bold\"></div>\r\n                <div class=\"col-sm-12 col-md-6 font-weight-bold\">ÚLTIMO DESCONECTE: ");
#nullable restore
#line 107 "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml"
                                                                               Write(empleado.ultimo_desconecte.ToString().Substring(0, 10));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n            </div>\r\n\r\n\r\n        </div>\r\n    </div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5d315e2dc3876d1392ef2c220d65f5c016b2277412507", async() => {
                WriteLiteral(@"
        <div class=""row mt-5"">

            <div class=""col-sm-12 col-md-4"">
                <div class=""container"">
                    <div class=""row"">
                        <div class=""dropdown col-12"">
                            <label for=""FormControlSelect"" class=""font-weight-bold"">SELECCIONA TU DESCONECTE:</label>
                            <select id=""s_tipo_solicitud"" class=""selectpicker"" name=""tipo_solicitud"">
");
#nullable restore
#line 122 "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml"
                                 foreach (var opt in ViewBag.Opciones)
                                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5d315e2dc3876d1392ef2c220d65f5c016b2277413543", async() => {
#nullable restore
#line 124 "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml"
                                                                                                                           Write(opt.solicitud);

#line default
#line hidden
#nullable disable
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 124 "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml"
                                       WriteLiteral(opt.id_tipo_solicitud);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "aux", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 124 "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml"
AddHtmlAttributeValue("", 4255, opt.maximo_dias, 4255, 16, false);

#line default
#line hidden
#nullable disable
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "aux", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 124 "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml"
AddHtmlAttributeValue("", 4278, opt.consume_vacaciones, 4278, 23, false);

#line default
#line hidden
#nullable disable
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
#nullable restore
#line 125 "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml"
                                }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                            </select>
                        </div>
                    </div>
                </div>
            </div>
            <div class=""col-sm-12 col-md-4"">
                <label for=""FormControlSelect"" class=""font-weight-bold"">SELECCIONA INICIO Y FIN DE TU DESCONECTE:</label>
                <div id=""d_calendar""></div>
            </div>
            <div class=""col-sm-12 col-md-4"">
                <div class=""row mt-5"">
                    <div class=""col-6 font-weight-bold"">FECHA INICIO:<input name=""fecha_inicio"" id=""i_fecha_inicio"" /></div>
                    <div class=""col-6 font-weight-bold"">FECHA FIN:<input name=""fecha_fin"" id=""i_fecha_fin"" /></div>
                </div>
                <div class=""row mt-2"">
                    <div class=""col-6 text-center"">
                        <input style=""display:none"" name=""id_sap""");
                BeginWriteAttribute("value", " value=\"", 5251, "\"", 5274, 1);
#nullable restore
#line 142 "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml"
WriteAttributeValue("", 5259, empleado.IdSAP, 5259, 15, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                        <input style=\"display:none\" name=\"id_sap_aprobador\"");
                BeginWriteAttribute("value", " value=\"", 5355, "\"", 5384, 1);
#nullable restore
#line 143 "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml"
WriteAttributeValue("", 5363, empleado.Idsap_padre, 5363, 21, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />
                        <input data-toggle=""modal"" data-target=""#solicitudModal"" class=""btn btn-desconectate"" value=""SOLICITAR"" />
                    </div>
                </div>
            </div>

        </div>

        <div class=""modal"" id=""solicitudModal"" tabindex=""-1"" role=""dialog"">
            <div class=""modal-dialog modal-dialog-centered"" role=""document"">
                <div class=""modal-content"">
                    <div class=""modal-header"">
                        <h5 class=""modal-title"">Solicitud Vacaciones</h5>
                        <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                            <span aria-hidden=""true"">&times;</span>
                        </button>
                    </div>
                    <div class=""modal-body"">
                        <p>Utiliza este espacio, para realizar alguna observacion o comentario sobre tus dias que solicitas:</p>
                        <textarea  class=""form-control"" rows=""");
                WriteLiteral(@"3"" id=""ta_observacion"" name=""observacion""></textarea>
                    </div>
                    <div class=""modal-footer"">
                        <button id=""b_solicitud"" type=""button"" class=""btn btn-primary"">Guardar Solicitud</button>
                        <button type=""button"" class=""btn btn-secondary"" data-dismiss=""modal"" >Close</button>
                    </div>
                </div>
            </div>
        </div>
    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"


    <div class=""row mt-5 mb-5"">
        <div class=""col-sm-12 col-md-12"">
            <table id=""t_solicitudes"" class=""table table-responsive table-striped"">
                <thead style=""background-color: #821c61; color: white;"">
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
        var maximo_dias = 30;
        var consume_vacaciones = 'S';



        $(""#s_tipo_solicitud"").change(function () {
");
            WriteLiteral(@"            maximo_dias = $(""#s_tipo_solicitud option:selected"").attr(""aux"");
            consume_vacaciones = $(""#s_tipo_solicitud option:selected"").attr(""aux2"");
        });


        $(""#b_solicitud"").bind('click', function () {

            if ($(""#i_fecha_inicio"").val() == """" || $(""#ta_observacion"").val() == """") {
                swal(""Ocurrio un Error"", ""Debes seleccionar una fecha e ingresar una observacion"", ""warning"");
            }
            else {
                url = """);
#nullable restore
#line 219 "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml"
                  Write(Url.Content("~/Solicitante/CreaSolicitud"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\";\r\n                parametros = {\r\n                    \"id_sap\":");
#nullable restore
#line 221 "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml"
                        Write(empleado.IdSAP);

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n                    \"id_sap_aprobador\":");
#nullable restore
#line 222 "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml"
                                  Write(empleado.Idsap_padre);

#line default
#line hidden
#nullable disable
            WriteLiteral(@",
                    ""fecha_inicio"": $(""#i_fecha_inicio"").val(),
                    ""fecha_fin"": $(""#i_fecha_fin"").val(),
                    ""tipo_solicitud"": $(""#s_tipo_solicitud"").val(),
                    ""observacion_solicitante"": $(""#ta_observacion"").val()
                };

                $.post(url, parametros, function (data) {
                    if (!isNaN(data)) {
                        $('#solicitudModal').modal('hide');
                        actualizaLista();
                        limpiaForma();
                        swal(""Felicidades"", ""Tu solicitud se registro Correctamente"", ""success"");
                        EnviaCorreo(data);

                    } else {
                        swal(""Ocurrio un Error"", data, ""error"");
                    }
                });
            }
        });


        $(""#b_descargar"").click(function () {
            url = """);
#nullable restore
#line 246 "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml"
              Write(Url.Content("~/Solicitante/DescargaPoliza"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\";\r\n\r\n            window.open(url)\r\n        });\r\n       \r\n\r\n        function EnviaCorreo(folio) {\r\n\r\n            url = \"");
#nullable restore
#line 254 "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml"
              Write(Url.Content("~/Solicitante/EnviaCorreo"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@""";

            $.post(url, { ""folio"":folio }, function (data) {
                if (data != 1) {
                    swal(""Ocurrio un Error"", data, ""error"");
                }
            });

        }

        $(""#d_calendar"").simpleCalendar({
            //Defaults options below
            //string of months starting from january

            months: ['enero', 'febrero', 'marzo', 'abril', 'mayo', 'junio', 'julio', 'agosto', 'septiembre', 'octubre', 'noviembre', 'diciembre'],
            days: ['domingo', 'lunes', 'martes', 'miercoles', 'jueves', 'viernes', 'sabado'],
            onDateSelect: function (date, events) {
                //Se agrega la fecha al array de vacaciones
                //Falta agregar una validacion para remover y no repetir fechas.
                var fecha = date.valueOf();
                if (vacaciones.indexOf(fecha) == -1) {
                    vacaciones.push(fecha);
                }
                else {
                    vacaciones.splice(vacaci");
            WriteLiteral(@"ones.indexOf(fecha), 1);
                }

                if (vacaciones.length != 0) {
                    vacaciones.sort(function (a, b) { return a - b });
                    // se cuentan los dias habiles
                    var cont = 0;
                    for (var i = 0; i < vacaciones.length; i++) {
                        var aux = vacaciones[i];
                        var dia = new Date(aux);
                        var dia_sem = dia.getDay();
                        if (dia_sem > 0 && dia_sem < 6)
                            cont++;
                    }

                    if (cont > disfrutar) {
                        swal(""Cuidado"", ""Tu seleccion sobrepasa los dias permitidos"", ""warning"");
                        bandera = 1;
                    }
                    else {
                        bandera = 0;
                    }
                    // se cuentan los dias habiles
                    var fecha_inicio = new Date(vacaciones[0]);
                    v");
            WriteLiteral(@"ar fecha_fin = new Date(vacaciones[vacaciones.length - 1]);
                    $(""#i_fecha_inicio"").val(fecha_inicio.toISOString().substr(0, 10));
                    $(""#i_fecha_fin"").val(fecha_fin.toISOString().substr(0, 10));
                } else {
                    $(""#i_fecha_inicio"").val('');
                    $(""#i_fecha_fin"").val('');
                }

                //console.log(vacaciones);
            }
        });


        function limpiaForma() {
            $(""#i_fecha_inicio"").val('');
            $(""#i_fecha_fin"").val('');
            maximo_dias = 30;
            consume_vacaciones = 'S';
            vacaciones = new Array();
            $(""#s_tipo_solicitud"").selectpicker('val', '0');
            $(""#d_calendar .day"").removeClass(""has-event"");
            $(""#ta_observacion"").val('')
        }

        actualizaLista();


        function actualizaLista() {

            url = """);
#nullable restore
#line 331 "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml"
              Write(Url.Content("~/Solicitante/EnlistaSolicitudes"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\"\r\n\r\n            $.post(url, { \"id_sap\":");
#nullable restore
#line 333 "/Users/rodrigolira/Proyectos/hollidays/Views/Solicitante/Index.cshtml"
                              Write(empleado.IdSAP);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"}, function (data) {

                var num = data.length;
                var html = """";
                var estatus = """";
                $(""#t_solicitudes tbody"").html("""");
                for (var i = 0; i < num; i++) {
                    var dato = data[i];

                    switch (dato.estatus) {
                        case 0:
                            estatus = ""PENDIENTE"";
                            break;
                        case 1:
                            estatus = ""APROBADO"";
                            break;
                        case 2:
                            estatus = ""DENEGADO"";
                            break;
                    }

                    html = ""<tr data-toggle='tooltip' title='"" + dato.observacion_solicitante + ""'><td>"" + dato.folio + ""</td><td>"" + dato.tipo_solicitud + ""</td><td>"" + dato.fecha_inicio.substring(0, 10) + ""</td><td>"" + dato.fecha_fin.substring(0, 10) + ""</td><td>"" + dato.aprobador + ""</td><td>"" + estatus + ""</td><");
            WriteLiteral("/tr>\";\r\n                    $(\"#t_solicitudes tbody\").append(html);\r\n                }\r\n                $(\'[data-toggle=\"tooltip\"]\').tooltip();\r\n            });\r\n\r\n        }\r\n    });\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<desconectate.Models.Solicitud> Html { get; private set; }
    }
}
#pragma warning restore 1591
