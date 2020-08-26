// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

Date.prototype.formatDDMMYYYY = function () {

    var day = this.getDate();
    day = (day.toString().length == 1) ? '0' + day : day;

    var month = (this.getMonth() + 1);
    month = (month.toString().length == 1) ? '0' + month : month;

    return day +
        "/" + month +
        "/" + this.getFullYear();
}

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
        var arreglo = aux.split('&');
        var cadenas = arreglo.length;

        if (cadenas == 1) {
            var arreglo2 = aux.split('|');

            if (arreglo2.length == 1)//Consulta normal, un solo texto
            {
                $(tabla + ' tbody tr').show();
                aux = aux.toUpperCase();
                $(tabla + ' tbody tr:visible').not(':contains(' + aux + ')').hide();
            }
            else  //Consulta con | que combinan textos
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