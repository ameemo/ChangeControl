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
        private string codigo;
        private string[] subject;
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
            this.subject = new string[] { "",
                "Autorización para ejecutar el control de cambio.",
                "Autorización para termino del control de cambio.",
                "Se ha excedido el tiempo de la autorización.",
                "Se ha excedido la cantidad de controles de cambio en ejecución.",
                "Hay una nueva revisión."
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
                mensaje = "El control de cambio con la clave: <b>" + this.clave_cc + "</b> y con fecha de ejecución de <b>" + this.fecha_ejecucion_cc + "</b> requiere de su autorización, ya que " + this.funcion + ".</br>Para ello dar click en <a href =\"www.prueba.com/ControlCambio/Ver?id=" + this.id_cc + "\">" + this.clave_cc + "</a>.";
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
            }
            return retornar;
        }
    }
}