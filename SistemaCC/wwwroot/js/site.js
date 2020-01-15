// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function tabs(seccion)
{
    //Activar o desactivar los div indicados
    var activo = document.getElementsByClassName("seccion-activa")
    $(activo).addClass("seccion-inactiva")
    $(activo).removeClass("seccion-activa")
    $("#Seccion" + seccion).addClass("seccion-activa")
    $("#Seccion" + seccion).removeClass("seccion-inactiva")
    //Asignar las clases a los links para dar el efecto y saber en que sección está posicionado
    var link = document.getElementsByClassName("active")
    $(link).addClass("inactivo")
    $(link).removeClass("active")
    $("#Link" + seccion).addClass("active")
    $("#Link" + seccion).removeClass("inactivo")
}