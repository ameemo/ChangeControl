// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function tabs(seccion, sc, sl)
{
    //Activar o desactivar los div indicados
    var activo = document.getElementsByClassName(sc)
    $(activo).addClass("seccion-inactiva")
    $(activo).removeClass("seccion-activa " + sc)
    $("#Seccion" + seccion).addClass("seccion-activa " + sc)
    $("#Seccion" + seccion).removeClass("seccion-inactiva")
    //Asignar las clases a los links para dar el efecto y saber en que sección está posicionado
    var link = document.getElementsByClassName(sl)
    $(link).addClass("inactivo")
    $(link).removeClass("active " + sl)
    $("#Link" + seccion).addClass("active " + sl)
    $("#Link" + seccion).removeClass("inactivo")
}
// Funcion para contador de caracteres
function contar(posicion, clase, conteo_id)
{
    var contadores = document.getElementsByClassName(clase)
    var conteo = $(contadores[posicion]).val()
    //modificar &
    contadores[posicion].value = conteo.replace(/&+/, "y")
    var div_conteo = document.getElementById(conteo_id + posicion)
    var texto_viejo = div_conteo.children[0]
    div_conteo.removeChild(texto_viejo)
    var maxlength = contadores[posicion].getAttribute("maxlength")
    var div = document.createElement("div")
    var texto_nuevo = document.createTextNode("Caracteres: " + conteo.length + "/" + maxlength)
    div.appendChild(texto_nuevo)
    div_conteo.appendChild(div)
}
// Funciones para agregar y quitar campos dinamicos
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
    var conteo1 = crear_elemento("div", [{ att: "id", val: "conteo-act-desc-" + id }], "campos_ultimos conteo-act-desc")
    var div_conteo1 = document.createElement("div")
    var conteo_num1 = document.createTextNode("Caracteres: 0/150")
    var textarea1 = crear_elemento("textarea", [{ att: "name", val: "actividades_prev_descripcion" },
                                                { att: "maxlength", val: "150"},
                                                { att: "placeholder", val: "Descripción" },
                                                { att: "pattern", val: "[A-Za-z0-9 ,.;-_/*+´¨]+" },
                                                { att: "onkeyup", val: "contar('" + id + "', 'contar-act-desc', 'conteo-act-desc-')"},
                                                { att: "oninvalid", val: "error_campos(2)"},
                                                { att: "required", val: "required" }], "form-control contar-act-desc")
    var conteo2  = crear_elemento("div", [{ att: "id", val: "conteo-act-obs-" + id }], "campos_ultimos conteo-act-obs")
    var div_conteo2 = document.createElement("div")
    var conteo_num2 = document.createTextNode("Caracteres: 0/150")
    var textarea2 = crear_elemento("textarea", [{ att: "name", val: "actividades_prev_observaciones" },
                                                { att: "maxlength", val: "150"},
                                                { att: "placeholder", val: "Observaciones" },
                                                { att: "onkeyup", val: "contar('" + id + "', 'contar-act-obs', 'conteo-act-obs-')"},
                                                { att: "oninvalid", val: "error_campos(2)"},
                                                { att: "required", val: "required" }], "form-control contar-act-obs")
    var input1 = crear_elemento("input", [{ att: "name", val: "actividades_prev_fecha" },
                                          { att: "type", val: "date" },
                                          { att: "oninvalid", val: "error_campos(2)"},
                                          { att: "required", val: "required" }], "form-control")
    var select = clonar_select("usuarios2", "actividades_prev_usuarios")
    var cerrar = crear_elemento("a", [{ att: "onclick", val: "quitar('actividad" + id_ + "')" }],"btn btn-outline-danger cerrar")
    var numeracion = document.createTextNode("A" + id_)
    var cerrar_x = document.createTextNode("X")
    var contenedor = document.getElementById("actividades_contenedor")
    //Agregar los nodos conforme al arbol 
    actividad_numeracion.appendChild(numeracion)
    div_conteo1.appendChild(conteo_num1)
    conteo1.appendChild(div_conteo1)
    div_conteo2.appendChild(conteo_num2)
    conteo2.appendChild(div_conteo2)
    cerrar.appendChild(cerrar_x)
    quitar.appendChild(cerrar)
    campos_ultimos_1.appendChild(actividad_numeracion)
    campos_ultimos_2.appendChild(textarea1)
    campos_ultimos_2.appendChild(conteo1)
    campos_ultimos_3.appendChild(textarea2)
    campos_ultimos_3.appendChild(conteo2)
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
    var quitar = crear_elemento("div", [], "quitar")
    var conteo1 = crear_elemento("div", [{ att: "id", val: "conteo-act_cc-desc-" + id }], "campos_ultimos conteo-act_cc-desc")
    var div_conteo1 = document.createElement("div")
    var conteo_num1 = document.createTextNode("Caracteres: 0/150")
    var textarea1 = crear_elemento("textarea", [{ att: "name", val: "actividades_cc_descripcion" },
                                                { att: "maxlength", val: "150" },
                                                { att: "placeholder", val: "Descripción" },
                                                { att: "onkeyup", val: "contar('" + id + "', 'contar-act_cc-desc', 'conteo-act_cc-desc-')" },
                                                { att: "oninvalid", val: "error_campos(2)"},
                                                { att: "required", val: "required" }], "form-control contar-act_cc-desc")
    var conteo2 = crear_elemento("div", [{ att: "id", val: "conteo-act_cc-obs-" + id }], "campos_ultimos conteo-act_cc-obs")
    var div_conteo2 = document.createElement("div")
    var conteo_num2 = document.createTextNode("Caracteres: 0/150")
    var textarea2 = crear_elemento("textarea", [{ att: "name", val: "actividades_cc_observaciones" },
                                                { att: "maxlength", val: "150" },
                                                { att: "placeholder", val: "Observaciones" },
                                                { att: "onkeyup", val: "contar('" + id + "', 'contar-act_cc-obs', 'conteo-act_cc-obs-')" },
                                                { att: "oninvalid", val: "error_campos(2)"},
                                                { att: "required", val: "required" }], "form-control contar-act_cc-obs")
    var input1 = crear_elemento("input", [{ att: "name", val: "actividades_cc_fecha" },
                                          { att: "type", val: "date" },
                                          { att: "oninvalid", val: "error_campos(2)"},
                                          { att: "required", val: "required" }], "form-control")
    var select = clonar_select("usuarios2","actividades_cc_usuarios")
    var cerrar = crear_elemento("a", [{ att: "onclick", val: "quitar('actividad_cc" + id_ + "')" }], "btn btn-outline-danger cerrar")
    var numeracion = document.createTextNode("A" + id_)
    var cerrar_x = document.createTextNode("X")
    var contenedor = document.getElementById("actividades_cc_contenedor")
    //Agregar los nodos conforme al arbol 
    actividad_numeracion.appendChild(numeracion)
    div_conteo1.appendChild(conteo_num1)
    conteo1.appendChild(div_conteo1)
    div_conteo2.appendChild(conteo_num2)
    conteo2.appendChild(div_conteo2)
    cerrar.appendChild(cerrar_x)
    quitar.appendChild(cerrar)
    campos_ultimos_1.appendChild(actividad_numeracion)
    campos_ultimos_2.appendChild(textarea1)
    campos_ultimos_2.appendChild(conteo1)
    campos_ultimos_3.appendChild(textarea2)
    campos_ultimos_3.appendChild(conteo2)
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
function servicio_agregar(id) {
    //Asigar id a la nueva fila
    var id_ = parseInt(id) + 1
    var campos_dos = crear_elemento("div", [{ att: "id", val: "servicio" + id_ }], "campos campos_dos")
    var campos_dos_kai = crear_elemento("div", [], "campos campos_dos_kai")
    var campos_tres = crear_elemento("div", [], "campos campos_tres")
    var campos_ultimos_1 = crear_elemento("div", [{ att: "name", val: "servicio" }], "campos campos_ultimos numeracion")
    var campos_ultimos_2 = crear_elemento("div", [], "campos campos_ultimos")
    var campos_ultimos_3 = crear_elemento("div", [], "campos campos_ultimos")
    var campos_ultimos_4 = crear_elemento("div", [], "campos campos_ultimos")
    var campos_ultimos_5 = crear_elemento("div", [], "campos campos_ultimos")
    var servicio_numeracion = document.createElement("div")
    var quitar = crear_elemento("div" , [], "quitar")
    var select = clonar_select("servicios","servicio_servicios")
    var input1 = crear_elemento("input", [{ att: "name", val: "servicio_inicio" },
                                          { att: "type", val: "date" },
                                          { att: "oninvalid", val: "error_campos(2)"},
                                          { att: "required", val: "required" }], "form-control")
    var input2 = crear_elemento("input", [{ att: "name", val: "servicio_temino" },
                                          { att: "type", val: "date" },
                                          { att: "oninvalid", val: "error_campos(2)"},
                                          { att: "required", val: "required" }], "form-control")
    var cerrar = crear_elemento("a", [{ att: "onclick", val: "quitar('servicio" + id_ + "')" }], "btn btn-outline-danger cerrar")
    var numeracion = document.createTextNode("S" + id_)
    var cerrar_x = document.createTextNode("X")
    var contenedor = document.getElementById("servicio_contenedor")
    //Agregar los nodos conforme al arbol 
    servicio_numeracion.appendChild(numeracion)
    cerrar.appendChild(cerrar_x)
    quitar.appendChild(cerrar)
    campos_ultimos_1.appendChild(servicio_numeracion)
    campos_ultimos_2.appendChild(select)
    campos_ultimos_3.appendChild(input1)
    campos_ultimos_4.appendChild(input2)
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
    var aa = document.getElementById("servicio_agregar")
    aa.removeAttribute("onclick")
    aa.setAttribute("onclick", "servicio_agregar('" + id_ + "')")
}
function riesgo_agregar(id) {
    //Asigar id a la nueva fila
    var id_ = parseInt(id) + 1
    var campos_dos = crear_elemento("div", [{ att: "id", val: "riesgo" + id_ }], "campos campos_dos")
    var campos_dos_kai = crear_elemento("div", [], "campos campos_dos_kai")
    var campos_tres = crear_elemento("div", [], "campos campos_tres")
    var campos_ultimos_1 = crear_elemento("div", [{ att: "name", val: "riesgo" }], "campos campos_ultimos numeracion")
    var campos_ultimos_2 = crear_elemento("div", [], "campos campos_ultimos")
    var campos_ultimos_3 = crear_elemento("div", [], "campos campos_ultimos")
    var campos_ultimos_4 = crear_elemento("div", [], "campos campos_ultimos")
    var campos_ultimos_5 = crear_elemento("div", [], "campos campos_ultimos")
    var riesgo_numeracion = document.createElement("div")
    var conteo = crear_elemento("div", [{ att: "id", val: "conteo-riesgo-desc-" + id }], "campos_ultimos conteo-riesgo-desc")
    var div_conteo = document.createElement("div")
    var conteo_num = document.createTextNode("Caracteres: 0/150")
    var textarea = crear_elemento("textarea", [{ att: "name", val: "riesgos_descripcion" },
                                                { att: "maxlength", val: "150" },
                                                { att: "placeholder", val: "Descripción" },
                                                { att: "onkeyup", val: "contar('" + id + "', 'contar-riesgo-desc', 'conteo-riesgo-desc-')" },
                                                { att: "oninvalid", val: "error_campos(3)"},
                                                { att: "required", val: "required" }], "form-control contar-riesgo-desc")
    var quitar = crear_elemento("div", [], "quitar")
    var cerrar = crear_elemento("a", [{ att: "onclick", val: "quitar('riesgo" + id_ + "')" }], "btn btn-outline-danger cerrar")
    var numeracion = document.createTextNode("R" + id_)
    var cerrar_x = document.createTextNode("X")
    var contenedor = document.getElementById("riesgos_contenedor")
    //Agregar los nodos conforme al arbol 
    cerrar.appendChild(cerrar_x)
    div_conteo.appendChild(conteo_num)
    conteo.appendChild(div_conteo)
    quitar.appendChild(cerrar)
    riesgo_numeracion.appendChild(numeracion)
    campos_ultimos_1.appendChild(riesgo_numeracion)
    campos_ultimos_2.appendChild(textarea)
    campos_ultimos_2.appendChild(conteo)
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
    var aa = document.getElementById("riesgo_agregar")
    aa.removeAttribute("onclick")
    aa.setAttribute("onclick", "riesgo_agregar('" + id_ + "')")
}
function riesgo_no_agregar(id) {
    //Asigar id a la nueva fila
    var id_ = parseInt(id) + 1
    var campos_dos = crear_elemento("div", [{ att: "id", val: "riesgo_no" + id_ }], "campos campos_dos")
    var campos_dos_kai = crear_elemento("div", [], "campos campos_dos_kai")
    var campos_tres = crear_elemento("div", [], "campos campos_tres")
    var campos_ultimos_1 = crear_elemento("div", [{ att: "name", val: "riesgo_no" }], "campos campos_ultimos numeracion")
    var campos_ultimos_2 = crear_elemento("div", [], "campos campos_ultimos")
    var campos_ultimos_3 = crear_elemento("div", [], "campos campos_ultimos")
    var campos_ultimos_4 = crear_elemento("div", [], "campos campos_ultimos")
    var campos_ultimos_5 = crear_elemento("div", [], "campos campos_ultimos")
    var riesgo_numeracion = document.createElement("div")
    var conteo = crear_elemento("div", [{ att: "id", val: "conteo-riesgo_no-desc-" + id }], "campos_ultimos conteo-riesgo_no-desc")
    var div_conteo = document.createElement("div")
    var conteo_num = document.createTextNode("Caracteres: 0/150")
    var textarea = crear_elemento("textarea", [{ att: "name", val: "riesgos_no_descripcion" },
                                               { att: "maxlength", val: "150" },
                                               { att: "placeholder", val: "Descripción" },
                                               { att: "onkeyup", val: "contar('" + id + "', 'contar-riesgo_no-desc', 'conteo-riesgo_no-desc-')" },
                                               { att: "oninvalid", val: "error_campos(3)"},
                                               { att: "required", val: "required" }], "form-control contar-riesgo_no-desc")
    var quitar = crear_elemento("div", [], "quitar")
    var cerrar = crear_elemento("a", [{ att: "onclick", val: "quitar('riesgo_no" + id_ + "')" }], "btn btn-outline-danger cerrar")
    var numeracion = document.createTextNode("R" + id_)
    var cerrar_x = document.createTextNode("X")
    var contenedor = document.getElementById("riesgos_no_contenedor")
    //Agregar los nodos conforme al arbol 
    cerrar.appendChild(cerrar_x)
    div_conteo.appendChild(conteo_num)
    conteo.appendChild(div_conteo)
    quitar.appendChild(cerrar)
    riesgo_numeracion.appendChild(numeracion)
    campos_ultimos_1.appendChild(riesgo_numeracion)
    campos_ultimos_2.appendChild(textarea)
    campos_ultimos_2.appendChild(conteo)
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
    var aa = document.getElementById("riesgo_no_agregar")
    aa.removeAttribute("onclick")
    aa.setAttribute("onclick", "riesgo_no_agregar('" + id_ + "')")
}
function adjunto_agregar(id) {
    var id_ = parseInt(id) + 1
    var campos_tres = crear_elemento("div", [{ att: "id", val: "adjunto" + id_ }], "campos campos_tres")
    var campos_ultimos_0 = crear_elemento("div", [{ att: "name", val: "adjunto" }], "campos campos_ultimos numeracion")
    var campos_ultimos_1 = crear_elemento("div",[], "campos campos_ultimos custom-file")
    var campos_ultimos_2 = crear_elemento("div", [], "campos campos_ultimos")
    var adjunto_numeracion = document.createElement("div")
    var quitar = crear_elemento("div", [], "quitar")
    var input = crear_elemento("input", [{ att: "type", val: "file" },
                                         { att: "name", val: "adjuntos_" },
                                         { att: "accept", val: "application/PDF, image/jpg, image/jpeg, image/png" },
                                         { att: "onchange", val: "revisar_extension('" + id_ + "')" },
                                         { att: "oninvalid", val: "error_campos(4)"},
                                         { att: "required", val: "required"}], "custom-file-input")
    var label = crear_elemento("label", [{ att: "for", val: "customFile" }], "custom-file-label normal")
    var numeracion = document.createTextNode("A" + id_)
    var p = document.createElement("p")
    var label_texto = document.createTextNode("Adjuntar archivo tipo PNG, JPG, JPEG, o PDF")
    var cerrar = crear_elemento("a", [{ att: "onclick", val: "quitar('adjunto" + id_ + "')" }], "btn btn-outline-danger cerrar")
    var cerrar_x = document.createTextNode("X")
    var contenedor = document.getElementById("adjuntos_contenedor")
    //Añadir a el contenedor
    adjunto_numeracion.appendChild(numeracion)
    p.appendChild(label_texto)
    label.appendChild(p)
    cerrar.appendChild(cerrar_x)
    quitar.appendChild(cerrar)
    campos_ultimos_0.appendChild(adjunto_numeracion)
    campos_ultimos_1.appendChild(input)
    campos_ultimos_1.appendChild(label)
    campos_ultimos_2.appendChild(quitar)
    campos_tres.appendChild(campos_ultimos_0)
    campos_tres.appendChild(campos_ultimos_1)
    campos_tres.appendChild(campos_ultimos_2)
    contenedor.appendChild(campos_tres)
    //cambiar el campo del atributo onclick para mantener la númeración
    var aa = document.getElementById("adjunto_agregar")
    aa.removeAttribute("onclick")
    aa.setAttribute("onclick", "adjunto_agregar('" + id_ + "')")
}
function crear_elemento(nombre, atributos, clases) {
    var elemento = document.createElement(nombre)
    for (var i = 0; i < atributos.length; i++) {
        elemento.setAttribute(atributos[i].att,atributos[i].val)
    }
    $(elemento).addClass(clases)
    return elemento
}
function clonar_select(id, name) {
    var select2 = document.getElementById(id)
    var opciones = select2.children
    var select = crear_elemento("select", [{ att: "name", val: name }, { att: "required", val: "required" }, { att: "oninvalid", val: "error_campos(2)"}], "form-control")
    for (var i = 0; i < opciones.length; i++) {
        var opcion = crear_elemento("option", [{ att: "value", val: opciones[i].getAttribute("value") }])
        var texto = document.createTextNode(opciones[i].textContent)
        opcion.appendChild(texto)
        select.appendChild(opcion)
    }
    return select
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
function revisar_extension(id) {
    var id_ = parseInt(id) - 1
    var extensiones = ["jpg", "jpeg", "png", "pdf"]
    var adjunto = document.getElementsByName("adjuntos_")[id_]
    var archivo_ = $(adjunto).val()
    var archivo = archivo_.substr(12, archivo_.length - 12)
    var extension = archivo.substr(-3, 3)
    var label = document.getElementsByClassName("normal")[id_]
    var texto_viejo = label.children[0]
    var p = document.createElement("p")
    //validar la extension
    if (!extensiones.includes(extension)) {
        var texto_nuevo = document.createTextNode("Adjuntar archivo tipo PNG, JPG, JPEG, o PDF")
        p.appendChild(texto_nuevo)
        adjunto.value = ""
        alert("El archivo no cumple con las especificaciones. Sólo archivos: jpg, jpeg, png y pdf")//Cambio en el texto de la label para mostrarla al cliente
    }
    else { 
        var texto_nuevo = document.createTextNode(archivo)
        p.appendChild(texto_nuevo)
    }
    label.removeChild(texto_viejo)
    label.appendChild(p)
}
// funcion para mostrar la sección en la que el campo no está respondido
function error_campos(seccion) {
    var collapse = document.getElementsByClassName("collapse")
    var show = document.getElementsByClassName("show")
    $(show).removeClass("show")
    $(collapse[seccion]).addClass("show")
}
// funcion para cambiar al nombre del input con el id del rol
function setNombre() {
    var index = this.getAttribute("id")
    var rol_input = document.getElementsByClassName("rol_input")
    if (this.checked) {
        rol_input[index].setAttribute("name", "rol_input")
    }
    else {
        rol_input[index].setAttribute("name", "")
    }
}
// Funcion para ocultar las notas en revisión
function ocultar_mostrar_nota(ocultar, mostrar) {
    var ocultar_ = document.getElementById(ocultar);
    ocultar_.style.display = "none"
    var mostrar_ = document.getElementById(mostrar);
    mostrar_.style.display = "block"
}
// Funcion para saber que tipo de submit
function revisarSubmit(tipo) {
    var submit = document.getElementById("submit")
    var validacion1 = true;
    var validacion2 = true;
    if (tipo == "corregir") {
        var textareas = document.getElementsByClassName("contador")
        for (var i = 0; i < textareas.length; i++) {
                validacion1 = $(textareas[i]).val().length > 0
        }
    }
    if (tipo == "aprobar") {
        var textareas = document.getElementsByClassName("contador")
        for (var i = 0; i < textareas.length; i++) {
                validacion2 = ! $(textareas[i]).val().length > 0
        }
    }
    if (validacion1 && validacion2) {
        submit.setAttribute("name", tipo)
        $(submit).trigger('click')
    }
    else {
        if (!validacion1) {
            alert('No se puede enviar a correccion, sin observaciones en las secciones.')
        }
        if (!validacion2) {
            alert('No se puede enviar a aprobar, con observaciones en las secciones.')
        }
    }
}