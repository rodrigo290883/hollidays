#pragma checksum "/Users/rodrigolira/Proyectos/hollidays/Views/Aprobador/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b400260f0d7308effbf9469391058bbeef70f8a5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Aprobador_Index), @"mvc.1.0.view", @"/Views/Aprobador/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b400260f0d7308effbf9469391058bbeef70f8a5", @"/Views/Aprobador/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1185a8b3bb4e90f1e146f85307bfc4dace37d116", @"/Views/_ViewImports.cshtml")]
    public class Views_Aprobador_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "0", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "1", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "2", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("frmAprobadorSolicitud"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "/Users/rodrigolira/Proyectos/hollidays/Views/Aprobador/Index.cshtml"
  

    ViewData["Title"] = "Aprobador|Desconéctate";
    if (ViewBag.tipo == "S")
        Layout = "~/Views/Shared/_LayoutSessionS.cshtml";
    else
        Layout = "~/Views/Shared/_LayoutSessionA.cshtml";

    Empleados empleado = ViewBag.empleado;


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

</style>

<link rel=""stylesheet"" href=""lib/bootstrap-select/dist/css/bootstrap-select.css"" />

<div>
    <img id=""i_doodle"" src=""images/imagen.png"">
</div>

<div class=""container"">
    <div class=""row"">
        <div class=""col-6"" style=""color:#b41547;"">
            <h3 class=""font-weight-bold"">");
#nullable restore
#line 41 "/Users/rodrigolira/Proyectos/hollidays/Views/Aprobador/Index.cshtml"
                                    Write(empleado.Nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n            <h4>IDSAP: ");
#nullable restore
#line 42 "/Users/rodrigolira/Proyectos/hollidays/Views/Aprobador/Index.cshtml"
                  Write(empleado.IdSAP);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h4>
        </div>
    </div>
    <div class=""row mt-2"">
        <div class=""col-12"">
            <Strong><span style=""font-size:x-large;"">Solicitudes Hechas</span></Strong>
            <table class=""table table-responsive table-striped"" id=""t_Slt_Pendiente"">
                <thead style=""background-color: #821c61; color: white;"">
                    <tr>
                        <th scope=""col"">FOLIO</th>
                        <th>NOMBRE</th>
                        <th scope=""col"">SOLICITUD</th>
                        <th scope=""col"">FECHA INICIO</th>
                        <th scope=""col"">FECHA FINAL</th>
                        <th scope=""col"">ESTATUS</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
    </div>
    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b400260f0d7308effbf9469391058bbeef70f8a56924", async() => {
                WriteLiteral(@"
        <div class=""row mt-2"">
            <div class=""col-sm-12 col-md-8"">
                <div class=""container"">
                    <div class=""row"">
                        <div class=""col-8"">
                            SOLICITANTE: <b id=""data_nombre""></b> <br />
                            IDSAP: <b id=""data_idsap""></b>
                        </div>
                    </div>
                    <div class=""row"">
                        <div class=""col-6 font-weight-bold"">ANTIGÜEDAD: <b id=""data_antiguedad""></b></div>
                        <div class=""col-6 font-weight-bold"">FECHA ANIVERSARIO: <b id=""data_aniversario""></b></div>
                    </div>
                    <div class=""row"">
                        <div class=""col-6 font-weight-bold"">DÍAS POR DISFRUTAR: <b id=""data_disfrutar""></b></div>
                        <div class=""col-6 font-weight-bold"">DÍAS CADUCAN: <b id=""data_caducan""></b></div>
                    </div>
                    <div class=""row"">
      ");
                WriteLiteral(@"                  <div class=""col-6 font-weight-bold"">DÍAS TOMADOS: <b id=""data_tomados""></b></div>
                        <div class=""col-6 font-weight-bold"">PERIODO: <b id=""data_periodo""></b></div>
                    </div>
                    <div class=""row"">
                        <div class=""col-6 font-weight-bold"">SOLICITUD: <b id=""data_tipo""></b></div>

                    </div>
                    <div class=""row"">
                        <div class=""col-6 font-weight-bold"">FECHA INICIO: <b id=""data_inicio""></b></div>
                        <div class=""col-6 font-weight-bold"">FECHA FIN: <b id=""data_fin""></b></div>
                    </div>
                    <div class=""row"">
                        <div class=""col-12 font-weight-bold"">
                            OBSERVACION SOLICITANTE: <b id=""observacion_solicitante""></b>
                        </div>
                    </div>
                </div>
            </div>
            <div class=""col-sm-12 col-md-4 mt-2"">
  ");
                WriteLiteral("              <div class=\"container\">\r\n                    <div class=\"row\">\r\n                        <label>CAMBIAR ESTATUS:</label>\r\n                        <select id=\"s_estatus\" name=\"s_estatus\" class=\"selectpicker\">\r\n                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b400260f0d7308effbf9469391058bbeef70f8a59630", async() => {
                    WriteLiteral("PENDIENTE");
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b400260f0d7308effbf9469391058bbeef70f8a510868", async() => {
                    WriteLiteral("APROBADO");
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
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b400260f0d7308effbf9469391058bbeef70f8a512106", async() => {
                    WriteLiteral("DENEGADO");
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
                WriteLiteral(@"
                        </select>
                    </div>
                    <div id=""d_con_goce"" class=""row"">
                        <label> Permiso:  </label>
                        <div class=""form-check"">
                            <input id=""i_con_goce_s"" type=""radio"" class=""form-check-input"" name=""con_goce"" value=""S"" checked/>
                            <label class=""form-check-label"" for=""i_con_goce_s"">Con goce   </label>
                        </div>
                        <div class=""form-check"">
                            <input id=""i_con_goce_n"" type=""radio"" class=""form-check-input"" name=""con_goce"" value=""N"" />
                            <label class=""form-check-label"" for=""i_con_goce_n"">Sin goce</label>
                        </div>
                    </div>
                    <div class=""row rounded mt-2"">
                        <textarea id=""ta_comentarios"" name=""comentarios"" class=""form-control"" rows=""3"" placeholder=""Observaciones...""></textarea>
               ");
                WriteLiteral("     </div>\r\n                    <div class=\"row mt-2 text-center\">\r\n                        <input type=\"hidden\" name=\"id_folio\" id=\"id_folio\"");
                BeginWriteAttribute("value", " value=\"", 5496, "\"", 5504, 0);
                EndWriteAttribute();
                WriteLiteral("/>\r\n                        <input type=\"submit\" class=\"btn btn-desconectate\" value=\"GUARDAR\" />\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    ");
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
</div>


    <script src=""lib/bootstrap-select/dist/js/bootstrap-select.js""></script>

    <script>
        $(function () {
            var folio = '';
            actualizaLista();
            limpiarForma();

            function actualizaLista() {
                url = """);
#nullable restore
#line 145 "/Users/rodrigolira/Proyectos/hollidays/Views/Aprobador/Index.cshtml"
                  Write(Url.Content("~/Aprobador/SolicitudesPendientes"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\"\r\n\r\n                $.post(url, { \"id_sap\":");
#nullable restore
#line 147 "/Users/rodrigolira/Proyectos/hollidays/Views/Aprobador/Index.cshtml"
                                  Write(empleado.IdSAP);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"}, function (data) {
                    var num = data.length;
                    $(""#t_Slt_Pendiente tbody"").html("""");
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
                        html = '<tr class=""renglon"" alt=""' + dato.folio + '"" id=""folio""><th scope=""row"">' + dato.folio + '</th><td>' + dato.nombre + '</td><td>' + dato.solicitudName + '</td><td>' + dato.fecha_inicio.substring(0, 10) + '</td><td>' + dato.fecha_fin.substring(0, 10) + '</td><td>' + dato.estatusDescripcion + '</");
            WriteLiteral(@"td></tr>';
                        $(""#t_Slt_Pendiente tbody"").append(html);
                    }
                });
            }

            $(""#t_Slt_Pendiente tbody"").on(""click"", ""tr"", function () {
                limpiarForma();
                folio = $(this).attr('alt');

                url = """);
#nullable restore
#line 174 "/Users/rodrigolira/Proyectos/hollidays/Views/Aprobador/Index.cshtml"
                  Write(Url.Content("~/Aprobador/solicitudInfo"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"""

                $.post(url, { ""id_folio"": folio }, function (data) {
                    //alert(JSON.stringify(data));
                    if (data.tipo_solicitud_goce == 1)
                        $(""#d_con_goce"").show();
                    else
                        $(""#d_con_goce"").hide();


                    $(""#data_idsap"").html(data.id_sap);
                    $(""#data_nombre"").html(data.nombre);
                    $(""#data_antiguedad"").html(parseInt(data.antiguedad / 12) + "" años y "" + (data.antiguedad % 12) + "" meses "");
                    $(""#data_aniversario"").html(data.aniversario.substring(0, 10));
                    $(""#data_disfrutar"").html(data.fecha_fin - data.fecha_inicio);
                    $(""#data_tipo"").html(data.solicitudName);
                    $(""#data_caducan"").html(4);
                    $(""#data_tomados"").html(0);
                    $(""#data_periodo"").html(2020);
                    $(""#data_inicio"").html(data.fecha_inicio.substring(0, 10));
  ");
            WriteLiteral(@"                  $(""#data_fin"").html(data.fecha_fin.substring(0, 10));
                    $(""#id_folio"").val(data.folio);
                    $(""#observacion_solicitante"").val(data.observacion_solicitante);



                });


            });
            function limpiarForma() {
                $(""b"").html('');
                $(""#id_folio"").val('');
                $(""#ta_comentarios"").val('');
                $(""#s_estatus"").selectpicker('val', '0');
                $(""#i_con_goce_s"").prop(""checked"", true);
                $(""#d_con_goce"").hide();
            }


            $(""#frmAprobadorSolicitud"").submit(function (e) {
                e.preventDefault();

                url = """);
#nullable restore
#line 217 "/Users/rodrigolira/Proyectos/hollidays/Views/Aprobador/Index.cshtml"
                  Write(Url.Content("~/Aprobador/ActualizarSolicitud"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"""
                parametros = $(this).serialize();

                alert(JSON.stringify(parametros));

                if ($(""#ta_comentarios"").val() == '' || $(""#s_estatus"").val() == 0) {
                    swal(""Ocurrio un Error"", ""Debes ingresar una observacion y cambiar el estatus de la solicitud"", ""warning"");
                }
                else {

                    //alert(JSON.stringify(parametros));

                    $.post(url, parametros, function (data) {
                        if (data = ""1"") {
                            url2 = """);
#nullable restore
#line 231 "/Users/rodrigolira/Proyectos/hollidays/Views/Aprobador/Index.cshtml"
                               Write(Url.Content("~/Aprobador/EnviaCorreo"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"""
                            $.post(url2, { ""folio"": $(""#id_folio"").val() }, function (data2) {
                                if (data2 != 1) {
                                    swal(""Ocurrio un Error en el envio de correo"", data, ""error"");
                                }
                            });
                            limpiarForma();
                            actualizaLista();
                            swal(""Se actualizo la solicitud correctamente"", ""Ok"", ""success"");
                        } else {
                            swal(""Ocurrio un Error"", data, ""error"");
                        }
                    });
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
