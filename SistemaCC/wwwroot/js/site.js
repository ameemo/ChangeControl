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
function actividad_agregar(id)
{
    //Asigar id a la nueva fila
    var id_ = parseInt(id) + 1
    var campos_dos = crear_elemento("div", [{ att: "id", val: "actividad" + id_ }], "campos campos_dos")
    var campos_dos_kai = crear_elemento("dov", [], "campos campos_dos_kai")
    var campos_tres = crear_elemento("div", [], "campos campos_tres")
    var campos_ultimos_1 = crear_elemento("div", [{ att: "name", val: "actividad" }], "campos campos_ultimos numeracion")
    var campos_ultimos_2 = crear_elemento("div", [], "campos campos_ultimos")
    var campos_ultimos_3 = crear_elemento("div", [], "campos campos_ultimos")
    var campos_ultimos_4 = crear_elemento("div", [], "campos campos_ultimos")
    var campos_ultimos_5 = crear_elemento("div", [], "campos campos_ultimos")
    var actividad_numeracion = document.createElement("div")
    var quitar = crear_elemento("div", [], "quitar")
    var textarea1 = crear_elemento("textarea", [{ att: "name", val: "actividades_prev_descripcion" },
                                                { att: "placeholder", val: "Descripción" },
                                                { att: "required", val: "required" }], "form-control")
    var textarea2 = crear_elemento("textarea", [{ att: "name", val: "actividades_prev_observaciones" },
                                                { att: "placeholder", val: "Observaciones" },
                                                { att: "required", val: "required" }], "form-control")
    var input1 = crear_elemento("input", [{ att: "name", val: "actividades_prev_fecha" },
                                          { att: "type", val: "date" },
                                          { att: "required", val: "required" }], "form-control")
    var select = clonar_usuarios("actividades_prev_usuarios")
    var cerrar = crear_elemento("a", [{ att: "onclick", val: "quitar('actividad" + id_ + "')" }],"btn btn-outline-danger")
    var numeracion = document.createTextNode("A" + id_)
    var cerrar_x = document.createTextNode("X")
    var contenedor = document.getElementById("actividades_contenedor")
    //Agregar los nodos conforme al arbol 
    actividad_numeracion.appendChild(numeracion)
    cerrar.appendChild(cerrar_x)
    quitar.appendChild(cerrar)
    campos_ultimos_1.appendChild(actividad_numeracion)
    campos_ultimos_2.appendChild(textarea1)
    campos_ultimos_3.appendChild(textarea2)
    campos_ultimos_4.appendChild(input1)
    campos_ultimos_4.appendChild(select)
    campos_ultimos_5.appendChild(quitar)
    campos_dos_kai.appendChild(campos_ultimos_1)
    campos_dos_kai.appendChild(campos_ultimos_2)
    campos_tres.appendChild(campos_ultimos_3)
    campos_tres.appendChild(campos_ultimos_4)
    campos_tres.appendChild(campos_ultimos_5)
    campos_dos.appendChild(campos_dos_kai)
    campos_dos.appendChild(campos_tres)
    contenedor.appendChild(campos_dos)
    //cambiar el campo del atributo onclick para mantener la númeración
    var aa = document.getElementById("actividad_agregar")
    aa.removeAttribute("onclick")
    aa.setAttribute("onclick","actividad_agregar('"+id_+"')")
}
function actividad_cc_agregar(id) {
    //Asigar id a la nueva fila
    var id_ = parseInt(id) + 1
    var campos_dos = crear_elemento("div", [{ att: "id", val: "actividad_cc" + id_ }], "campos campos_dos")
    var campos_dos_kai = crear_elemento("div", [], "campos campos_dos_kai")
    var campos_tres = crear_elemento("div", [], "campos campos_tres")
    var campos_ultimos_1 = crear_elemento("div", [{ att: "name", val: "actividad_cc" }], "campos campos_ultimos numeracion")
    var campos_ultimos_2 = crear_elemento("div", [], "campos campos_ultimos")
    var campos_ultimos_3 = crear_elemento("div", [], "campos campos_ultimos")
    var campos_ultimos_4 = crear_elemento("div", [], "campos campos_ultimos")
    var campos_ultimos_5 = crear_elemento("div", [], "campos campos_ultimos")
    var actividad_numeracion = document.createElement("div")
    var quitar = crear_elemento("div" , [], "quitar")
    var textarea1 = crear_elemento("textarea", [{ att: "name", val: "actividades_cc_descripcion" },
                                                { att: "placeholder", val: "Descripción" },
                                                { att: "required", val: "required" }], "form-control")
    var textarea2 = crear_elemento("textarea", [{ att: "name", val: "actividades_cc_observaciones" },
                                                { att: "placeholder", val: "Observaciones" },
                                                { att: "required", val: "required" }], "form-control")
    var input1 = crear_elemento("input", [{ att: "name", val: "actividades_cc_fecha" },
                                          { att: "type", val: "date" },
                                          { att: "required", val: "required" }], "form-control")
    var select = clonar_usuarios("actividades_cc_usuarios")
    var cerrar = crear_elemento("a", [{ att: "onclick", val: "quitar('actividad_cc" + id_ + "')" }], "btn btn-outline-danger")
    var numeracion = document.createTextNode("A" + id_)
    var cerrar_x = document.createTextNode("X")
    var contenedor = document.getElementById("actividades_cc_contenedor")
    //Agregar los nodos conforme al arbol 
    actividad_numeracion.appendChild(numeracion)
    cerrar.appendChild(cerrar_x)
    quitar.appendChild(cerrar)
    campos_ultimos_1.appendChild(actividad_numeracion)
    campos_ultimos_2.appendChild(textarea1)
    campos_ultimos_3.appendChild(textarea2)
    campos_ultimos_4.appendChild(input1)
    campos_ultimos_4.appendChild(select)
    campos_ultimos_5.appendChild(quitar)
    campos_dos_kai.appendChild(campos_ultimos_1)
    campos_dos_kai.appendChild(campos_ultimos_2)
    campos_tres.appendChild(campos_ultimos_3)
    campos_tres.appendChild(campos_ultimos_4)
    campos_tres.appendChild(campos_ultimos_5)
    campos_dos.appendChild(campos_dos_kai)
    campos_dos.appendChild(campos_tres)
    contenedor.appendChild(campos_dos)
    //cambiar el campo del atributo onclick para mantener la númeración
    var aa = document.getElementById("actividad_cc_agregar")
    aa.removeAttribute("onclick")
    aa.setAttribute("onclick", "actividad_cc_agregar('" + id_ + "')")
}
function crear_elemento(nombre, atributos, clases) {
    var elemento = document.createElement(nombre)
    for (var i = 0; i < atributos.length; i++) {
        elemento.setAttribute(atributos[i].att,atributos[i].val)
    }
    $(elemento).addClass(clases)
    return elemento
}
function clonar_usuarios(name) {
    var usuarios2 = document.getElementById("usuarios2")
    var opciones = usuarios2.children
    var usuarios = crear_elemento("select", [{ att: "name", val: name }, {att: "required", val: "required"}], "form-control")
    for (var i = 0; i < opciones.length; i++) {
        var opcion = crear_elemento("option", [{ att: "value", val: opciones[i].getAttribute("value") }])
        var texto = document.createTextNode(opciones[i].textContent)
        opcion.appendChild(texto)
        usuarios.appendChild(opcion)
    }
    return usuarios
}
function quitar(id)
{
    var tipo = id.substr(0, id.length-1)
    //Decrementamos el id en 1 para tener conteo adecuado del tipo
    var aa = document.getElementById(tipo + "_agregar")
    var arg_str = aa.getAttribute("onclick")
    var id_ = parseInt(arg_str.substr(arg_str.length - 3, 1)) - 1
    aa.removeAttribute("onclick")
    aa.setAttribute("onclick", tipo + "_agregar('" + id_ + "')")
    //Eliminar
    var campos_dos = document.getElementById(id)
    campos_dos.remove()
    //Corregir numeración
    var numeracion = document.getElementsByName(tipo)
    var letter_A = tipo.charAt(0).toUpperCase()
    for (var i = id_ - 1; i >= 0; i--)
    {
        var actividad_numeracion = document.createElement("div")
        var texto1 = document.createTextNode(letter_A + (i + 1))
        var remover = numeracion[i].children[0]
        actividad_numeracion.appendChild(texto1)
        numeracion[i].removeChild(remover)
        numeracion[i].appendChild(actividad_numeracion)
    }
}