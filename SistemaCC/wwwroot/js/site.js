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
//Funcion para contador de caracteres
function contar(posicion)
{
    var contadores = document.getElementsByClassName("contador")
    var conteo = $(contadores[posicion]).val()
    var div_conteo = document.getElementById("conteo-" + posicion)
    var texto_viejo = div_conteo.children[0]
    div_conteo.removeChild(texto_viejo)
    var algo = contadores[posicion].getAttribute("maxlength")
    var div = document.createElement("div")
    var texto_nuevo = document.createTextNode("Caracteres: " + conteo.length + "/" + algo)
    div.appendChild(texto_nuevo)
    div_conteo.appendChild(div)
}
//Funciones para agregar y quitar campos dinamicos
function actividades(id)
{
    //Asigar id a la nueva fila
    var id_ = parseInt(id) + 1
    console.log(id + " - " + id_)
    var fila = document.createElement("div")
    var div1 = document.createElement("div")
    var div2 = document.createElement("div")
    var div3 = document.createElement("div")
    var input = document.createElement("input")
    var textarea = document.createElement("textarea")
    var a2 = document.createElement("a")
    var t2 = document.createTextNode("X")
    var max = document.getElementById("max_fila")
    //Add atributos 
    fila.setAttribute("id", "fila" + id_)
    input.setAttribute("type", "text")
    input.setAttribute("placeholder", "In")
    input.setAttribute("name", "input")
    textarea.setAttribute("placeholder", "In")
    textarea.setAttribute("name", "textarea")
    a2.setAttribute("href", "#")
    a2.setAttribute("onclick", "quitar('" + id_ + "')")
    div3.style.textAlign = "right"
    //Agregar los nodos conforme al arbol 
    a2.appendChild(t2)
    div1.appendChild(input)
    div2.appendChild(textarea)
    div3.appendChild(a2)
    fila.appendChild(div1)
    fila.appendChild(div2)
    fila.appendChild(div3)
    max.appendChild(fila)
    //añadir clase para el estilo
    $(fila).addClass("fila")
    $(a2).addClass("btn")
    $(a2).addClass("btn-outline-danger")
    //cambiar el campo del atributo onclick para mantener la númeración
    var a = document.getElementById("boton_agregar")
    a.removeAttribute("onclick")
    a.setAttribute("onclick","agregar('"+id_+"')")

}
function quitar(id)
{
    var fila = document.getElementById("fila" + id)
    fila.remove()
}