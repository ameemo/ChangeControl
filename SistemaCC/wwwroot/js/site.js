// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function tabs(seccion)
{
    //div de abajo
    var activo = document.getElementsByClassName("seccion-activa")
    $(activo).addClass("seccion-inactiva")
    $(activo).removeClass("seccion-activa")
    $("#Seccion" + seccion).addClass("seccion-activa")
    $("#Seccion" + seccion).removeClass("seccion-inactiva")
    //tabs
    var link = document.getElementsByClassName("active")
    $(link).removeClass("active")
    $("#Link" + seccion).addClass("active")
}