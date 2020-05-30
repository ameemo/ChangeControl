using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaCC.Models;

namespace SistemaCC.Controllers.Clases
{
    public class Notificacion
    {
        public int id_cc;
        public int enEjecucion;
        public string fecha_emision, clave_cc, fecha_ejecucion_cc, funcion;
        public Boolean email;
        public Boolean emergente;
        public List<Autorizaciones> motivos;
        public List<Notificaciones> noAutorizo;
        public string emailAdmin;
        public string contrasena;
        private string codigo;
        private string dominio;
        private string[] subject;
        public Notificacion(int id_cc = 0, int funcion = 0, Boolean emergente = false)
        {
            this.id_cc = id_cc;
            this.fecha_emision = "";
            this.clave_cc = "";
            this.fecha_ejecucion_cc = "";
            this.email = false;
            this.emailAdmin = "";
            this.contrasena = "";
            this.dominio = "https://www.quack.com/";
            //Inicia subject (asunto)
            this.subject = new string[] { "",
                "Autorización para ejecutar el control de cambio." + (emergente == true ? "EMERGENTE": ""),
                "Autorización para termino del control de cambio.",
                "Se ha excedido el tiempo de la autorización.",
                "Se ha excedido la cantidad de controles de cambio en ejecución.",
                "Hay una nueva revisión.",
                "",
                "",
                "Control de cambio NO autorizado.",
                "Control de cambio revisado.",
                "Creacion de cuenta.",
                "Control cambio EN EJECUCIÓN."
            };
            // Saber la funcion que cumple o algun estado
            switch(funcion)
            {
                case 1:
                    this.funcion = "tiene actividades asignadas";
                    break;
                case 2:
                    this.funcion = "un o varios servicios se ven afectados";
                    break;
                case 3:
                    this.funcion = "aprobado";
                    break;
                case 4:
                    this.funcion = "enviado a corrección";
                    break;
                case 5:
                    this.funcion = "tiene actividades asignadas y un o varios servicios se ven afectados";
                    break;
            }
        }
        public Notificacion(string codigo, string clave)
        {
            this.codigo = codigo;
            this.clave_cc = clave;
            //Inicia subject (asunto)
            this.subject = new string[] { "Autorizacion pendiente." };
        }
        private string generateNAut_ejecucion() 
        {
            string mensaje = "";
            if(this.email)
            {
                if(emergente)
                {
                    mensaje = "El control de cambio con la clave: <b>" + this.clave_cc + "</b> y con fecha de ejecución de <b>" + this.fecha_ejecucion_cc + "</b>, tipo <b>EMERGENTE</b>, requiere de su REVISION, ya que " + this.funcion + ".</br>Para ello dar click en <a href =\"" + this.dominio + "ControlCambio/Ver/" + this.id_cc + "\">" + this.clave_cc + "</a>.";
                }
                else
                {
                    mensaje = "El control de cambio con la clave: <b>" + this.clave_cc + "</b> y con fecha de ejecución de <b>" + this.fecha_ejecucion_cc + "</b> requiere de su autorización, ya que " + this.funcion + ".</br>Para ello dar click en <a href =\"" + this.dominio + "ControlCambio/Ver/" + this.id_cc + "\">" + this.clave_cc + "</a>.";
                }
            }
            else
            {
                mensaje = "Para ejecutar.&El control de cambio con la clave anterior y con fecha de ejecución de&requiere de su autorización, ya que " + this.funcion + ".";
            }
            return mensaje; 
        }
        private string generateNAut_termino()
        {
            string mensaje = "";
            if (this.email)
            {
                mensaje = "El control de cambio con la clave: <b>" + this.clave_cc + "</b>, que fue ejecutado el <b>" + this.fecha_ejecucion_cc + "</b> requiere de su autorización para su termino.</br>Para ello dar click en <a href =\"" + this.dominio + "ControlCambio/Ver/" + this.id_cc + "\">" + this.clave_cc + "</a>.";
            }
            else
            {
                mensaje = "Termino.&El control de cambio con la clave anterior y con fecha de ejecución de&requiere de su autorización para el cierre.";
            }
            return mensaje;
        }
        private string generateNLim_tiempo()
        {
            string mensaje = "";
            if (this.email)
            {
                string tabla = "<table><thead><th>Clave</th><th>Usuario</th></thead><tbody>";
                foreach (var a in noAutorizo as IEnumerable<Notificaciones>)
                {
                    HomeController general = new HomeController();
                    tabla += "<tr><td>" + general.generarClave(a.ControlCambio) + "</td><td>" + a.Usuario.Nombre + " " + a.Usuario.ApePaterno + "</td></tr>";
                }
                tabla += "</tbody></table>";
                mensaje = "Se enviado recordatorio para su autorización a los siguientes usuarios: <br/>" + tabla;
            }
            return mensaje;
        }
        private string generateNLim_controles() 
        {
            string mensaje = "";
            if(email)
            {
                mensaje += "Hoy <b>" + DateTime.Now.ToString().Substring(0, 10) + "</b> se contó el total de controles de cambio de cambio EN EJECUCION: <b>" + this.enEjecucion + "</b>, ha sobre pasado la regla de cantidad máxima.";
            }
            return mensaje; 
        }
        private string generateNRevision() 
        {
            string mensaje = "";
            if (this.email)
            {
                mensaje = "El control de cambio con la clave: <b>" + this.clave_cc + "</b> y con fecha de ejecución de <b>" + this.fecha_ejecucion_cc + "</b> requiere de su revisión.</br>Para ello dar click en <a href =\"" + this.dominio + "ControlCambio/Ver/" + this.id_cc + "\">" + this.clave_cc + "</a>.";
            }
            else
            {
                mensaje = "Para revisar.&El control de cambio con la clave anterior y con fecha de ejecución de&requiere de su revision.";
            }
            return mensaje; 
        }
        private string generateDosPasos()
        {
            string mensaje = "";
            if(this.email && this.codigo.Length > 0)
            {
                mensaje = "El código para el control de cambio <b>" + this.clave_cc + "</b> es:<br/><font size=\"5\">" + this.codigo + "</font>";
            }
            return mensaje;
        }
        private string generateAutSuper()
        {
            string mensaje = "";
            if (email)
            {
                mensaje = "Para ejecutar.<br/>El control de cambio con la clave <b>" + this.clave_cc + "</b> y fecha de ejecución de <b>" + this.fecha_ejecucion_cc + "</b> require su autorización para ser ejecutado.</br>Para ello dar click en <a href =\"" + this.dominio + "ControlCambio/Ver/" + this.id_cc + "\">" + this.clave_cc + "</a>.";
            }
            else
            {
                mensaje = "Para ejecutar.&El control de cambio con la clave anterior y con fecha de ejecución de&requiere de su autorización.";
            }
            return mensaje;
        }
        private string generateNoAut()
        {
            string mensaje = "";
            if (email)
            {
                string tabla = "<table><thead><th>Motivo</th><th>Usuario</th></thead><tbody>";
                foreach(var a in motivos as IEnumerable<Autorizaciones>)
                {
                    tabla += "<tr><td>" + a.Motivo + "</td><td>" + a.Usuario.Nombre + " " + a.Usuario.ApePaterno + "</td></tr>";
                }
                tabla += "</tbody></table>";
                mensaje = "No autorizado.<br/>El control de cambio con la clave <b>" + this.clave_cc + "</b> y fecha de ejecución de <b>" + this.fecha_ejecucion_cc + "</b> no fue autorizado debido a los siguientes motivos:</br>" + tabla + "</br>Para revisar el control de cambio <a href =\"" + this.dominio + "ControlCambio/Ver/" + this.id_cc + "\">" + this.clave_cc + "</a>.";
            }
            else
            {
                mensaje = "No autorizado.&El control de cambio con la clave anterior y con fecha de ejecución de&no fue auorizado por los siguientes motivos:";
            }
            return mensaje;
        }
        private string generateNCorreccion()
        {
            string mensaje = "";
            if (this.email)
            {
                mensaje = "El control de cambio con la clave: <b>" + this.clave_cc + "</b> y con fecha de ejecución de <b>" + this.fecha_ejecucion_cc + "</b> ha sido revisado y " + this.funcion + ".</br>Para ver más detalles dar click en <a href =\"" + this.dominio + "ControlCambio/Corregir/" + this.id_cc + "\">" + this.clave_cc + "</a>.";
            }
            else
            {
                mensaje = "El control de cambio ha sido revisado.&El control de cambio con la clave anterior y con fecha de ejecución de&ha sido revisado y " + this.funcion;
            }
            return mensaje;
        }
        private string generateNCreacionU()
        {
            string mensaje = "";
            if(email)
            {
                mensaje = "Cuenta creada.<br/>Esta dirección de correo ha sido utilizada para la creación de una cuenta dentro del <b>SistemaCC</b>.<p style=\"text-align: center\"><b>Contraseña: </b>" + this.contrasena + "<br/>Para ingresar dar click en <a href =\"" + this.dominio + "\">SistemaCC</a>.<br/><i>Al iniciar sesión se le llevara al cambio de su contraseña, es importante que no olvide la contraseña que cree.</i></p><br/>Si usted no solicitó o no estaba enterado de esta operación, favor de comunicarse con el administrador del sistema: " + this.emailAdmin;
            }
            return mensaje;
        }
        private string generateNEnEjecucion()
        {
            string mensaje = "";
            if(email)
            {
                mensaje += "En ejecución.<br/>El control cambios con la clave <b>" + this.clave_cc + "</b> ha cambiado de Autorizado a En Ejecución. Esté atento a las funciones que cumple dentro de este control.</br>Para saber sus funciones puede apoyarse en su calendario y/o ver más detalles en <a href =\"" + this.dominio + "ControlCambio/Ver/" + this.id_cc + "\">" + this.clave_cc + "</a>.";
            }
            return mensaje;
        }
        public string getSubject(int numero)
        {
            return this.subject[numero];
        }
        public string generate(int numero)
        {
            string retornar = "";
            switch(numero)
            {
                case 1:
                    retornar = generateNAut_ejecucion();
                    break;
                case 2:
                    retornar = generateNAut_termino();
                    break;
                case 3:
                    retornar = generateNLim_tiempo();
                    break;
                case 4:
                    retornar = generateNLim_controles();
                    break;
                case 5:
                    retornar = generateNRevision();
                    break;
                case 6:
                    retornar = generateDosPasos();
                    break;
                case 7:
                    retornar = generateAutSuper();
                    break;
                case 8:
                    retornar = generateNoAut();
                    break;
                case 9:
                    retornar = generateNCorreccion();
                    break;
                case 10:
                    retornar = generateNCreacionU();
                    break;
                case 11:
                    retornar = generateNEnEjecucion();
                    break;
            }
            return retornar;
        }
    }
}