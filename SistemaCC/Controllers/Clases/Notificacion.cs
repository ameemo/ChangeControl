using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaCC.Controllers.Clases
{
    public class Notificacion
    {
        public int id_cc;
        public string fecha_emision, clave_cc, creador_cc, fecha_ejecucion_cc, funcion;
        public Boolean email;
        public string[] subject;
        public Notificacion(int id_cc, int funcion)
        {
            this.id_cc = id_cc;
            this.fecha_emision = "";
            this.clave_cc = "";
            this.creador_cc = "";
            this.fecha_ejecucion_cc = "";
            this.funcion = funcion == 1 ? "tiene actividades asignadas" : "un servicio se ve afectado";
            this.email = false;
            //Inicia subject (asunto)
            this.subject = new string[] { 
                "Autorización para ejecutar el control de cambio.",
                "Autorización para termino del control de cambio.",
                "Se ha excedido el tiempo de la autorización.",
                "Se ha excedido la cantidad de controles de cambio en ejecución.",
                "Hay una nueva revisión."
            };
        }
        public string generateNAut_ejecucion() 
        {
            string mensaje = "";
            if(this.email)
            {
                mensaje = "El control de cambio con la clave: <b>" + this.clave_cc + "</b> y con fecha de ejecución de <b>" + this.fecha_ejecucion_cc + "</b> requiere de su autorización, ya que " + this.funcion + ".</br>Para ello dar click en <a href =\"www.prueba.com/ControlCambio/Ver?id=" + this.id_cc + "\">" + this.clave_cc + "</a>.";
            }
            else
            {
                mensaje = "<a href=\"~/ControlCambio/Ver?id=" + this.id_cc + "\"><span class=\"bold\">" + this.clave_cc + "</span></a></br>Para ejecutar.</br>El control de cambio con la clave anterior y con fecha de ejecución de <span class=\"bold\">" + this.fecha_ejecucion_cc + "</span> requiere de su autorización, ya que " + this.funcion + ".";
            }
            return mensaje; 
        }
        public string generateNAut_termino() 
        { 
            return "";
        }
        public string generateNLim_tiempo() 
        {
            return "";
        }
        public string generateNLim_controles() 
        { 
            return ""; 
        }
        public string generateNRevision() 
        { 
            return ""; 
        }
        public string getSubject(int numero)
        {
            return this.subject[numero];
        }
    }
}