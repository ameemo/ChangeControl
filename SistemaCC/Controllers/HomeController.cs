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
        public int Sesion = 1;
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
            //mail.From = new MailAddress("ame_rainbow@hotmail.com");
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;
            SmtpServer.EnableSsl = true;
            SmtpServer.Credentials = new System.Net.NetworkCredential("tt2019a071_notificaciones@hotmail.com", "tt-2019-a071");
            //SmtpServer.Credentials = new System.Net.NetworkCredential("ame_rainbow@hotmail.com", "fuLLbuster4");
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
        public int generarNotiicacionAutSA(Usuario super, ControlCambio cc, string tipo)
        {
            int error = 0;
            try
            {
                Notificacion notificacion = new Notificacion(cc.Id_CC, 0);
                notificacion.clave_cc = generarClave(cc);
                notificacion.fecha_ejecucion_cc = cc.FechaEjecucion.ToString().Substring(0, 10);
                // Creo la autorización para despues editarla
                Autorizaciones autorizacion = new Autorizaciones();
                autorizacion.fk_CC = cc.Id_CC;
                autorizacion.fk_U = super.Id_U;
                autorizacion.Tipo = tipo;
                autorizacion.Fecha = DateTime.Today;
                autorizacion.Autorizado = false;
                BD.Autorizaciones.InsertOnSubmit(autorizacion);
                BD.SubmitChanges();
                Notificaciones not = new Notificaciones();
                not.fk_CC = cc.Id_CC;
                not.fk_U = super.Id_U;
                not.FechaEnvio = DateTime.Today;
                not.Contenido = notificacion.generate(7);
                not.Activa = true;
                not.Tipo = tipo == "Ejecutar" ? "AutorizarE": "AutorizarT";
                BD.Notificaciones.InsertOnSubmit(not);
                BD.SubmitChanges();
                notificacion.email = true;
                Email(super.Email, notificacion.getSubject(1), notificacion.generate(7));
            }
            catch(Exception e)
            {
                error = 1;
            }
            return error;
        }
        public int generarNotificacionRev(ControlCambio control)
        {
            int error = 0;
            try
            {
                Usuario admin = (from ur in BD.UsuarioRol where ur.fk_Rol == 2 select ur.Usuario).SingleOrDefault();
                Notificacion noti = new Notificacion(control.Id_CC, 0);
                noti.clave_cc = generarClave(control);
                noti.fecha_ejecucion_cc = control.FechaEjecucion.ToString().Substring(0, 10);
                Notificaciones not = new Notificaciones();
                not.fk_U = admin.Id_U;
                not.fk_CC = control.Id_CC;
                not.Activa = true;
                not.FechaEnvio = DateTime.Today;
                not.Tipo = "Revision";
                not.Contenido = noti.generate(5);
                BD.Notificaciones.InsertOnSubmit(not);
                BD.SubmitChanges();
                // enviar correo al admin
                noti.email = true;
                Email(admin.Email, noti.getSubject(5), noti.generate(5));
            }
            catch(Exception e)
            {
                error = 1;
            }
            return error;
        }
        public int generarNotificacionAut(ControlCambio cc, int tipo, int generate, int? key, Boolean emergente = false)
        {
            // Variable para tratar los errores y mostrar los mensajes
            int error = 0;
            try
            {
                Notificaciones notificaciones = new Notificaciones();
                Notificacion notificacion = new Notificacion(cc.Id_CC, tipo, emergente);
                Usuario creador = (from u in BD.Usuario where u.Id_U == cc.Creador select u).SingleOrDefault();
                // Agrego lo necesario para el mensaje de la notificación
                notificacion.fecha_emision = DateTime.Today.ToString().Substring(0, 10);
                notificacion.fecha_ejecucion_cc = cc.FechaEjecucion.ToString().Substring(0, 10);
                notificacion.clave_cc = generarClave(cc);
                // Agrego lo necesario para la notificación en la BD
                notificaciones.fk_CC = cc.Id_CC;
                notificaciones.fk_U = key;
                notificaciones.FechaEnvio = DateTime.Today;
                notificaciones.Contenido = notificacion.generate(generate);
                notificaciones.Activa = true;
                notificaciones.Tipo = generate == 1 ? "AutorizarE" : "AutorizarT";
                BD.Notificaciones.InsertOnSubmit(notificaciones);
                BD.SubmitChanges();
                //Enviar el correo
                string to = (from u in BD.Usuario where u.Id_U == key select u.Email).SingleOrDefault();
                notificacion.email = true;
                error = Email(to, notificacion.getSubject(generate), notificacion.generate(generate));
                // Creo la autorización para despues editarla si no es emergente
                if(!emergente)
                {
                    Autorizaciones aut = new Autorizaciones();
                    aut.fk_CC = cc.Id_CC;
                    aut.fk_U = key;
                    aut.Tipo = generate == 1 ? "Ejecutar" : "Termino";
                    aut.Fecha = DateTime.Now;
                    aut.Autorizado = false;
                    BD.Autorizaciones.InsertOnSubmit(aut);
                    BD.SubmitChanges();
                }
            }
            catch (Exception e)
            {
                error = 1;
            }
            return error;
        }
        public int generarNotificacionesAut(ControlCambio cc, int tipo, Boolean emergente = false)
        {
            // Variable para tratar los errores y mostrar los mensajes
            int error = 0;
            var actividades = (from ac in BD.ActividadesControl join a in BD.Actividades on ac.fk_Ac equals a.Id_Ac where ac.fk_CC == cc.Id_CC group a by a.Responsable).ToList();
            var servapp = (from sc in BD.ControlServicio join sa in BD.ServiciosAplicaciones on sc.fk_SA equals sa.Id_SA where sc.fk_CC == cc.Id_CC group sa by sa.Dueno).ToList();
            // si el único involucrado es el dueño se manda la notificacion sólo al super admin, si no hay serv ni act se notifica al super
            switch(actividades.Count + servapp.Count)
            {
                case 0:
                    Usuario super = (from u in BD.UsuarioRol where u.fk_Rol == 2 select u.Usuario).SingleOrDefault();
                    generarNotiicacionAutSA(super, cc, tipo == 1 ? "Ejecutar" : "Termino");
                    break;
                case 1:
                    if(actividades.Count != 0)
                    {
                        if(actividades[0].Key == cc.Usuario.Id_U)
                        {
                            Usuario super3 = (from u in BD.UsuarioRol where u.fk_Rol == 2 select u.Usuario).SingleOrDefault();
                            generarNotiicacionAutSA(super3, cc, tipo == 1 ? "Ejecutar" : "Termino");
                        }
                    }
                    else
                    {
                        if(servapp[0].Key == cc.Usuario.Id_U)
                        {
                            Usuario super3 = (from u in BD.UsuarioRol where u.fk_Rol == 2 select u.Usuario).SingleOrDefault();
                            generarNotiicacionAutSA(super3, cc, tipo == 1 ? "Ejecutar" : "Termino");
                        }
                    }
                    break;
                case 2:
                    if((actividades[0].Key == servapp[0].Key) && actividades[0].Key == cc.Usuario.Id_U)
                    {
                        Usuario super2 = (from u in BD.UsuarioRol where u.fk_Rol == 2 select u.Usuario).SingleOrDefault();
                        generarNotiicacionAutSA(super2, cc, tipo == 1 ? "Ejecutar" : "Termino");
                    }
                    break;
            }
            // revisar si se repiden usuario en actividades y servapp
            List<int?> ambos = new List<int?>();
            foreach(var act in actividades)
            {
                foreach (var s in servapp)
                {
                    if (s.Key == act.Key)
                    {
                        error = generarNotificacionAut(cc, 5, tipo, act.Key, emergente);
                        ambos.Add(s.Key);
                    }
                }
            }
            foreach(var act in actividades)
            {
                if(act.Key != cc.Usuario.Id_U && !ambos.Contains(act.Key))
                { 
                    error = generarNotificacionAut(cc, 1, tipo, act.Key, emergente);
                }
            }
            foreach(var s in servapp)
            {
                if (s.Key != cc.Usuario.Id_U && !ambos.Contains(s.Key))
                {
                    error = generarNotificacionAut(cc, 2, tipo, s.Key, emergente);
                }
            }
            return error;
        }
        public ActionResult Index(string mensaje)
        {
            // Notificaciones para navbar
            List<ControlCambio> ccs = (from n in BD.Notificaciones join cc in BD.ControlCambio on n.fk_CC equals cc.Id_CC where n.fk_U == Sesion && n.Activa == true select cc).ToList();
            ViewBag.Notificaciones_claves = generarListaClave(ccs);
            ViewBag.Notificaciones = (from n in BD.Notificaciones where n.fk_U == Sesion && n.Activa select n).ToList();
            // Termina lo necesario para notificacionespara navbar
            ViewBag.Creados_CC = (from cc in BD.ControlCambio where (cc.Estado == "Creado" || cc.Estado == "EnEvaluacion") && cc.Creador == Sesion select cc).ToList();
            ViewBag.Claves_ccc = generarListaClave(ViewBag.Creados_CC);
            ViewBag.Revisados_CC = (from cc in BD.ControlCambio where (cc.Estado == "EnCorreccion" || cc.Estado == "Aprobado") && cc.Creador == Sesion select cc).ToList();
            ViewBag.Claves_rcc = generarListaClave(ViewBag.Revisados_CC);
            ViewBag.ParaAut_CC = (from cc in BD.ControlCambio where (cc.Estado == "PausadoE" || cc.Estado == "Autorizado" || cc.Estado == "PausadoT") && cc.Creador == Sesion select cc).ToList();
            ViewBag.Claves_pacc = generarListaClave(ViewBag.ParaAut_CC);
            ViewBag.Ejecucion_CC = (from cc in BD.ControlCambio where (cc.Estado == "EnEjecucion" || cc.Estado == "PausadoT") && cc.Creador == Sesion select cc).ToList();
            ViewBag.Claves_ecc = generarListaClave(ViewBag.Ejecucion_CC);
            ViewBag.Revision_CC = (from cc in BD.ControlCambio where cc.Estado == "EnEvaluacion" && cc.Creador == Sesion select cc).ToList();
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
            if (control.Estado == "Creado")
            {
                control.Estado = "EnEvaluacion";
                BD.SubmitChanges();
                // Generar notificación de revisión
                error = generarNotificacionRev(control);
                if (error == 0)
                {
                    return RedirectToAction("./../Home/Index", new
                    {
                        mensaje = "C16"
                    });
                }
                else
                {
                    return RedirectToAction("./../Home/Index", new
                    {
                        mensaje = "E" + error
                    });
                }

            }
            if(control.Estado == "Aprobado")
            {
                control.Estado = "PausadoE";
                error = generarNotificacionesAut(control,1);// 1 = autorizar ejecucion
                BD.SubmitChanges();
                // Desactivamos la notificacion de No se autorizo, si la hay y de aprobado
                List<Notificaciones> not = (from n in BD.Notificaciones where n.fk_CC == id && n.fk_U == control.Usuario.Id_U && n.Activa == true select n).ToList();
                foreach(var n in not)
                {
                    if(n.Tipo == "NoAutorizo" || n.Tipo == "Aprobado")
                    {
                        n.Activa = false;
                        BD.SubmitChanges();
                    }
                }
                if (error == 0)
                {
                    if(control.FechaEjecucion == DateTime.Now && control.Tipo != "Emergente")
                    { 
                        return RedirectToAction("./../Home/Index", new
                        {
                            mensaje = "E21"
                        });
                    }
                    else
                    {
                        return RedirectToAction("./../Home/Index", new
                        {
                            mensaje = "C15"
                        });
                    }
                }
                else
                {
                    return RedirectToAction("./../Home/Index", new
                    {
                        mensaje = "E" + error
                    });
                }
            }
            return RedirectToAction("./../Home/Index", new
            {
                mensaje = "E1"
            });
        }
    }
}
