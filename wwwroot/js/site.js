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