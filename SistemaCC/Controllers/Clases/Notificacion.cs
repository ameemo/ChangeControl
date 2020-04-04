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
        public Notificacion(int id_cc, int funcion)
        {
            this.id_cc = id_cc;
            this.fecha_emision = "";
            this.clave_cc = "";
            this.creador_cc = "";
            this.fecha_ejecucion_cc = "";
            this.funcion = funcion == 1 ? "tiene actividades asignadas" : "un servicio se ve afectado";
            this.email = false;
        }
        public string generateNAut_ejecucion() 
        {
            string mensaje = "";
            if(this.email)
            {

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
    }
}