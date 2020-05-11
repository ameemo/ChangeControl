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
        public string fecha_emision, clave_cc, fecha_ejecucion_cc, funcion;
        public Boolean email;
        public List<Autorizaciones> motivos;
        private string codigo;
        private string[] subject;
        public Notificacion(int id_cc, int funcion)
        {
            this.id_cc = id_cc;
            this.fecha_emision = "";
            this.clave_cc = "";
            this.fecha_ejecucion_cc = "";
            this.funcion = funcion == 1 ? "tiene actividades asignadas" : "un servicio se ve afectado";
            this.email = false;
            //Inicia subject (asunto)
            this.subject = new string[] { "",
                "Autorización para ejecutar el control de cambio.",
                "Autorización para termino del control de cambio.",
                "Se ha excedido el tiempo de la autorización.",
                "Se ha excedido la cantidad de controles de cambio en ejecución.",
                "Hay una nueva revisión.",
                "",
                "",
                "Control de cambio NO autorizado."
            };
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
                mensaje = "El control de cambio con la clave: <b>" + this.clave_cc + "</b> y con fecha de ejecución de <b>" + this.fecha_ejecucion_cc + "</b> requiere de su autorización, ya que " + this.funcion + ".</br>Para ello dar click en <a href =\"www.prueba.com/ControlCambio/Ver/" + this.id_cc + "\">" + this.clave_cc + "</a>.";
            }
            else
            {
                mensaje = "Para ejecutar.&El control de cambio con la clave anterior y con fecha de ejecución de&requiere de su autorización, ya que " + this.funcion + ".";
            }
            return mensaje; 
        }
        private string generateNAut_termino() 
        { 
            return "";
        }
        private string generateNLim_tiempo() 
        {
            return "";
        }
        private string generateNLim_controles() 
        { 
            return ""; 
        }
        private string generateNRevision() 
        { 
            return ""; 
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
                mensaje = "Para ejecutar.<br/>El control de cambio con la clave <b>" + this.clave_cc + "</b> y fecha de ejecución de <b>" + this.fecha_ejecucion_cc + "</b> require su autorización para ser ejecutado.</br>Para ello dar click en <a href =\"www.prueba.com/ControlCambio/Ver/" + this.id_cc + "\">" + this.clave_cc + "</a>.";
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
                mensaje = "No autorizado.<br/>El control de cambio con la clave <b>" + this.clave_cc + "</b> y fecha de ejecución de <b>" + this.fecha_ejecucion_cc + "</b> no fue autorizado debido a los siguientes motivos:</br>" + tabla + "</br>Para revisar el control de cambio <a href =\"www.prueba.com/ControlCambio/Ver/" + this.id_cc + "\">" + this.clave_cc + "</a>.";
            }
            else
            {
                mensaje = "No autorizado.&El control de cambio con la clave anterior y con fecha de ejecución de&no fue auorizado por los siguientes motivos:";
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
                    retornar = generateNLim_controles();
                    break;
                case 4:
                    retornar = generateNLim_tiempo();
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
            }
            return retornar;
        }
    }
}