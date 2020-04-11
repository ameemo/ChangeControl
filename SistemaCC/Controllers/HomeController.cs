using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;
using SistemaCC.Models;
using SistemaCC.Controllers.Clases;

namespace SistemaCC.Controllers
{
    public class HomeController : Controller
    {
        BDControlCambioDataContext BD = new BDControlCambioDataContext();
        Mensajes Mensaje = new Mensajes();
        public string generarClave(ControlCambio cc)
        {
            Usuario creador = (from u in BD.Usuario where u.Id_U == cc.Creador select u).SingleOrDefault();
            string iniciales = creador.Nombre.Substring(0, 1) + creador.ApePaterno.Substring(0, 1) + creador.ApeMaterno.Substring(0, 1);
            string tipo_id = cc.Tipo.Substring(0, 1) + cc.Id_CC;
            var ac = (from a in BD.ActividadesControl where a.fk_CC == cc.Id_CC select a).ToList();
            var s = (from sc in BD.ControlServicio where sc.fk_CC == cc.Id_CC select sc).ToList();
            var r = (from rc in BD.Riesgos where rc.fk_CC == cc.Id_CC select rc).ToList();
            string asr = "";
            if(ac.Count > 0)
            {
                asr += "A";
            }
            if(s.Count > 0)
            {
                asr += "S";
            }
            if(r.Count > 0)
            {
                asr += "R";
            }
            if(asr.Length > 0)
            {
                return iniciales + "-" + tipo_id + "-" + asr;
            }
            else
            {
                return iniciales + "-" + tipo_id;
            }
        }
        public List<string> generarListaClave(List<ControlCambio> ccs)
        {
            List<string> claves = new List<string>();
            foreach(var cc in ccs)
            {
                claves.Add(generarClave(cc));
            }
            return claves;
        }
        public int Email(String to, String subject, String body)
        {
            // Variable para tratar los errores y mostrar los mensajes
            int error = 0;
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.office365.com", 587);
            mail.To.Add(to);
            mail.From = new MailAddress("tt2019a071_notificaciones@hotmail.com");
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;
            SmtpServer.EnableSsl = true;
            SmtpServer.Credentials = new System.Net.NetworkCredential("tt2019a071_notificaciones@hotmail.com", "tt-2019-a071");
            try
            {
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                error = 7;
            }
            return error;
        }
        public int generarNotificacion(ControlCambio cc, int tipo, int? key)
        {
            // Variable para tratar los errores y mostrar los mensajes
            int error = 0;
            try
            {
                Notificaciones notificaciones = new Notificaciones();
                Notificacion notificacion = new Notificacion(cc.Id_CC, tipo);
                Usuario creador = (from u in BD.Usuario where u.Id_U == cc.Creador select u).SingleOrDefault();
                // Agrego lo necesario para el mensaje de la notificación
                notificacion.fecha_emision = DateTime.Today.ToString().Substring(0, 10);
                notificacion.fecha_ejecucion_cc = cc.FechaEjecucion.ToString().Substring(0, 10);
                notificacion.creador_cc = creador.Nombre + " " + creador.ApePaterno;
                notificacion.clave_cc = generarClave(cc);
                // Agrego lo necesario para la notificación en la BD
                notificaciones.fk_CC = cc.Id_CC;
                notificaciones.fk_U = key;
                notificaciones.FechaEnvio = DateTime.Today;
                notificaciones.Contenido = notificacion.generateNAut_ejecucion();
                BD.Notificaciones.InsertOnSubmit(notificaciones);
                BD.SubmitChanges();
                // Enviar el correo
                //string to = (from u in BD.Usuario where u.Id_U == key select u.Email).SingleOrDefault();
                //notificacion.email = true;
                //error = Email(to, notificacion.getSubject(0), notificacion.generateNAut_ejecucion());
            } 
            catch(Exception e)
            {
                error = 1;
            }
            return error;
        }
        public int generarNotificaciones(ControlCambio cc)
        {
            // Variable para tratar los errores y mostrar los mensajes
            int error = 0;
            var actividades = (from ac in BD.ActividadesControl join a in BD.Actividades on ac.fk_Ac equals a.Id_Ac where ac.fk_CC == cc.Id_CC group a by a.Responsable).ToList();
            var servapp = (from sc in BD.ControlServicio join sa in BD.ServiciosAplicaciones on sc.fk_SA equals sa.Id_SA where sc.fk_CC == cc.Id_CC group sa by sa.Dueno).ToList();
            foreach(var act in actividades)
            {
                error = generarNotificacion(cc, 1, act.Key);
            }
            foreach(var s in servapp)
            {
                error = generarNotificacion(cc, 2, s.Key);
            }
            return error;
        }
        public ActionResult Index(string mensaje)
        {
            // Notificaciones para navbar
            List<ControlCambio> ccs = (from n in BD.Notificaciones join cc in BD.ControlCambio on n.fk_CC equals cc.Id_CC where n.fk_U == 1 select cc).ToList();
            ViewBag.Notificaciones_claves = generarListaClave(ccs);
            ViewBag.Notificaciones = (from n in BD.Notificaciones where n.fk_U == 1 select n).ToList();

            // Termina lo necesario para notificacionespara navbar
            ViewBag.Creados_CC = (from cc in BD.ControlCambio where cc.Estado == "Creado" || cc.Estado == "EnEvaluacion" select cc).ToList();
            ViewBag.Claves_ccc = generarListaClave(ViewBag.Creados_CC);
            ViewBag.Revisados_CC = (from cc in BD.ControlCambio where cc.Estado == "EnCorreccion" || cc.Estado == "Aprobado" select cc).ToList();
            ViewBag.Claves_rcc = generarListaClave(ViewBag.Revisados_CC);
            ViewBag.ParaAut_CC = (from cc in BD.ControlCambio where cc.Estado == "Pausado" || cc.Estado == "Autorizado" select cc).ToList();
            ViewBag.Claves_pacc = generarListaClave(ViewBag.ParaAut_CC);
            ViewBag.Ejecucion_CC = (from cc in BD.ControlCambio where cc.Estado == "EnEjecucion" select cc).ToList();
            ViewBag.Claves_ecc = generarListaClave(ViewBag.Ejecucion_CC);
            ViewBag.Revision_CC = (from cc in BD.ControlCambio where cc.Estado == "EnEvaluacion" select cc).ToList();
            ViewBag.Claves_enrcc = generarListaClave(ViewBag.Revision_CC);
            //Seccion de mensajes para la vista
            string MC = "";
            string ME = "";
            if (mensaje != null)
            {
                int numero = Convert.ToInt32(mensaje.Substring(1, mensaje.Length - 1));
                if (mensaje.Substring(0, 1) == "C")
                {
                    MC = Mensaje.getMConfirmacion(numero);
                }
                else
                {
                    ME = Mensaje.getMError(numero);
                }
            }
            ViewData["MC"] = MC;
            ViewData["ME"] = ME;
            return View();
        }
        public ActionResult About()
        {
            ViewData["Message"] = "Your application description page 90.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public ActionResult Privacy()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult CambiarContrasena()
        {
            return View();
        }
        //Cambia el estado en el control de cambio
        public ActionResult cambiarEstado(int id)
        {
            // Variable para tratar los errores y mostrar los mensajes
            int error = 0;
            ControlCambio control = (from cc in BD.ControlCambio where cc.Id_CC == id select cc).SingleOrDefault();
            if(control.Estado == "Creado")
            {
                control.Estado = "EnEvaluacion";
                BD.SubmitChanges();
            }
            if(control.Estado == "Aprobado")
            {
                control.Estado = "Pausado";
                error = generarNotificaciones(control);
                BD.SubmitChanges();
            }
            if(error == 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("./../Home/Index", new
                {
                    mensaje = "E" + error
                }) ;
            }
        }
    }
}
