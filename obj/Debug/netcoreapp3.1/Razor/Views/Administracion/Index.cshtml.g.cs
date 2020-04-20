#pragma checksum "/Users/rodrigolira/Proyectos/hollidays/Views/Administracion/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "95474dbbdbe21330b63c0a906310a5649a87f3c2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Administracion_Index), @"mvc.1.0.view", @"/Views/Administracion/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"95474dbbdbe21330b63c0a906310a5649a87f3c2", @"/Views/Administracion/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1185a8b3bb4e90f1e146f85307bfc4dace37d116", @"/Views/_ViewImports.cshtml")]
    public class Views_Administracion_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "/Users/rodrigolira/Proyectos/hollidays/Views/Administracion/Index.cshtml"
  
    ViewData["Title"] = "Administración|Desconéctate";
    Layout = "~/Views/Shared/_LayoutSession.cshtml";

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

<div>
    <img id=""i_doodle"" src=""images/imagen.png"">
</div>

<div class=""container-fluid"">
    <div class=""row"">
        <div class=""col-12"">
            <label for=""filtro_idsap"">Buscar:</label>
            <input class=""form-control ifiltro"" tabla=""t_empleados"" id=""filtro_idsap"" name=""filtro_idsap"" placeholder=""Tu Busqueda..."" />
        </div>
    </div>
    <div class=""row"">
        <div class=""col-12"">
            <table class=""table table-striped table-responsive"" id=""t_empleados"">
                <thead style=""background-color: #821c61; color: white;"">
                    <tr>
                        <th scope=""col"">IDSAP<");
            WriteLiteral(@"/th>
                        <th>NOMBRE</th>
                        <th scope=""col"">AREA</th>
                        <th scope=""col"">PUESTO</th>
                        <th scope=""col"">NO POLIZA</th>
                        <th scope=""col"">DESCARGA</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>3478</td>
                        <td>ISABEL PEREZ LARA</td>
                        <td>MERCADOTECNIA</td>
                        <td>GERENCIA</td>
                        <td>93427839-34</td>
                        <td>
                            <button type=""submit"" class=""btn btn-desconectate"">DESCARGA</button>
                        </td>
                    </tr>
                    <tr>
                        <td>3480</td>
                        <td>SAMUEL MEDINA MONTEJO</td>
                        <td>MERCADOTECNIA</td>
                        <td>AUXILIAR</td>
                        <td>2468");
            WriteLiteral(@"3297-54</td>
                        <td>
                            <button type=""submit"" class=""btn btn-desconectate"">DESCARGA</button>
                        </td>
                    </tr>
                    <tr>
                        <td>3484</td>
                        <td>GRACIELA EZQUIVEL RODRIGUEZ</td>
                        <td>PEOPLE</td>
                        <td>AUXILIAR</td>
                        <td>47389874-58</td>
                        <td>
                            <button type=""submit"" class=""btn btn-desconectate"">DESCARGA</button>
                        </td>
                    </tr>
                    <tr>
                        <td>3490</td>
                        <td>RAMIRO RAMIREZ PEREZ</td>
                        <td>PEOPLE</td>
                        <td>GERENCIA</td>
                        <td>32349987-99</td>
                        <td>
                            <button type=""submit"" class=""btn btn-desconectate"">DESCARGA</button>
   ");
            WriteLiteral(@"                     </td>
                    </tr>
                    <tr>
                        <td>3492</td>
                        <td>SANTIAGO GONZALEZ SUAREZ</td>
                        <td>TI</td>
                        <td>DESARROLLO</td>
                        <td>23984787-12</td>
                        <td>
                            <button type=""submit"" class=""btn btn-desconectate"">DESCARGA</button>
                        </td>
                    </tr>
                    <tr>
                        <td>3500</td>
                        <td>RAUL DOMINGUEZ MARTINEZ</td>
                        <td>TI</td>
                        <td>DESARROLLO</td>
                        <td>82736227-35</td>
                        <td>
                            <button type=""submit"" class=""btn btn-desconectate"">DESCARGA</button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</div>

<script>");
            WriteLiteral(@"

    $(function () {

        $.expr[':'].contains = $.expr.createPseudo(function (arg) {
            return function (elem) {
                return $(elem).text().toUpperCase().indexOf(arg.toUpperCase()) >= 0;
            };
        });

        $('.ifiltro').change(function () {
            var aux = $(this).val();
            var tabla = $(this).attr('tabla');
            var tabla = '#' + tabla;
            if (aux == '') {
                $(tabla + ' tbody tr').show();
            } else {
                var arreglo = aux.split(',');
                var cadenas = arreglo.length;

                if (cadenas == 1) {
                    var arreglo2 = aux.split('|');

                    if (arreglo2.length == 1)//Consulta normal, un solo texto
                    {
                        $(tabla + ' tbody tr').show();
                        aux = aux.toUpperCase();
                        $(tabla + ' tbody tr:visible').not(':contains(' + aux + ')').hide();
                    }
                  ");
            WriteLiteral(@"  else  //Consulta con | que combinan textos
                    {
                        $(tabla + ' tbody tr').hide();
                        for (var i = 0; i < arreglo2.length; i++) {
                            var aux2 = arreglo2[i];
                            $(tabla + ' tbody tr:contains(' + aux2 + ')').show();
                        }
                    }
                } else { //Consulta encadenada
                    $(tabla + ' tbody tr').show();
                    for (var i = 0; i < cadenas; i++) {

                        var aux2 = arreglo[i];

                        aux2 = aux2.toUpperCase();
                        $(tabla + ' tbody tr:visible').not(':contains(' + aux2 + ')').hide();
                    }
                }
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
