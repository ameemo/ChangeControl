using SistemaCC.Controllers;
using SistemaCC.Controllers.Clases;
using SistemaCC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace SistemaCC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public virtual void monitoreo()
        {
            while(true)
            {
                BDControlCambioDataContext BD = new BDControlCambioDataContext();
                HomeController General = new HomeController();
                // Condiciones que obedece el sistema
                int tiempo = (from m in BD.Monitoreo where m.Tipo == "Tiempo" orderby m.Fecha descending select m.Cantidad).ToList()[0];
                int cantidad = (from m in BD.Monitoreo where m.Tipo == "Cantidad" orderby m.Fecha descending select m.Cantidad).ToList()[0];
                string admin = (from ur in BD.UsuarioRol where ur.fk_Rol == 3 && ur.Usuario.Activo == true select ur.Usuario.Email).SingleOrDefault();
                Notificacion noti = new Notificacion(0, 0);
                DateTime fecha = DateTime.Today;
                // Cambiar de Autorizado a EnEjecucion un control
                List<ControlCambio> controles = (from cc in BD.ControlCambio where cc.FechaEjecucion == fecha select cc).ToList();
                foreach (var control in controles)
                {
                    if (control.Estado == "Autorizado")
                    {
                        control.Estado = "EnEjecucion";
                        BD.SubmitChanges();
                        // notifico a los involucrados y al dueño
                        // saber que act o ser tiene el usuario
                        var act = (from a in BD.ActividadesControl where a.fk_CC == control.Id_CC group a by a.Actividades.Responsable).ToList();
                        var sevapp = (from s in BD.ControlServicio where s.fk_CC == control.Id_CC group s by s.ServiciosAplicaciones.Dueno).ToList();
                        foreach (var a in act)
                        {
                            string email = (from u in BD.Usuario where u.Id_U == a.Key select u.Email).SingleOrDefault();
                            Notificacion not = new Notificacion(control.Id_CC, 1);
                            not.fecha_ejecucion_cc = control.FechaEjecucion.ToString().Substring(0, 10);
                            not.clave_cc = General.generarClave(control);
                            not.email = true;
                            General.Email(email, not.getSubject(11), not.generate(11));
                        }
                        foreach (var s in sevapp)
                        {
                            string email = (from u in BD.Usuario where u.Id_U == s.Key select u.Email).SingleOrDefault();
                            Notificacion not = new Notificacion(control.Id_CC, 1);
                            not.fecha_ejecucion_cc = control.FechaEjecucion.ToString().Substring(0, 10);
                            not.clave_cc = General.generarClave(control);
                            not.email = true;
                            General.Email(email, not.getSubject(11), not.generate(11));
                        }
                    }
                    if (control.Estado == "PausadoE")
                    {
                        var revisiones = (from r in BD.Revisiones where r.fk_CC == control.Id_CC select r).ToList();
                        control.Estado = "EnCorreccion";
                        BD.SubmitChanges();
                        if (revisiones.Count != 0)
                        {
                            // Actualizamos la información
                            revisiones[0].InfGeneral = "La fecha de ejecución se ha pasado, favor de escoger una nueva";
                            revisiones[0].Actividades = "";
                            revisiones[0].Servicios = "";
                            revisiones[0].Riesgos = "";
                            revisiones[0].fk_CC = control.Id_CC;
                            BD.SubmitChanges();
                        }
                        else
                        {
                            // Insertamos la informacion de las notas
                            Revisiones revisiones_ = new Revisiones();
                            revisiones_.InfGeneral = "La fecha de ejecución se ha pasado, favor de escoger una nueva";
                            revisiones_.Actividades = "";
                            revisiones_.Servicios = "";
                            revisiones_.Riesgos = "";
                            revisiones_.fk_CC = control.Id_CC;
                            BD.Revisiones.InsertOnSubmit(revisiones_);
                            BD.SubmitChanges();
                        }
                        // Enviar notificación de la correccion
                        Notificacion noti2 = new Notificacion(control.Id_CC, 4);
                        noti2.clave_cc = General.generarClave(control);
                        noti2.fecha_ejecucion_cc = control.FechaEjecucion.ToString().Substring(0, 10);
                        Notificaciones not = new Notificaciones();
                        not.fk_CC = control.Id_CC;
                        not.fk_U = control.Usuario.Id_U;
                        not.FechaEnvio = DateTime.Now;
                        not.Activa = true;
                        not.Tipo = "Correccion";
                        not.Contenido = noti2.generate(9);
                        BD.Notificaciones.InsertOnSubmit(not);
                        BD.SubmitChanges();
                        // enviamos correo
                        noti2.email = true;
                        General.Email(control.Usuario.Email, noti2.getSubject(9), noti2.generate(9));
                    }
                }
                int autorizados = (from cc in BD.ControlCambio where cc.Estado == "EnEjecucion" select cc).ToList().Count;
                if (autorizados > cantidad)
                {
                    //avisar al admin
                    noti.enEjecucion = autorizados;
                    noti.email = true;
                    General.Email(admin, noti.getSubject(4), noti.generate(4));
                }
                // Verificar que las autorizaciones se hagan en tiempo
                fecha = fecha.AddDays(-tiempo);
                List<Notificaciones> notis = (from n in BD.Notificaciones where n.FechaEnvio == fecha && (n.Tipo == "AutorizarE" || n.Tipo == "AutorizarT") && n.Activa == true select n).ToList();
                if (notis.Count > 0)
                {
                    // mandamos un recordatorio
                    foreach (var n in notis)
                    {
                        //actualizamos fecha de notificacion
                        n.FechaEnvio = DateTime.Today;
                        BD.SubmitChanges();
                        // Validamos que si es SAmin quien no ha autorizado
                        List<UsuarioRol> urs = (from u in BD.UsuarioRol where u.fk_Us == n.fk_U select u).ToList();
                        foreach (var ur in urs)
                        {
                            if (ur.fk_Rol == 2)
                            {
                                //enviamos recordatorio
                                Notificacion not = new Notificacion(n.ControlCambio.Id_CC, 1);
                                not.fecha_ejecucion_cc = n.ControlCambio.FechaEjecucion.ToString().Substring(0, 10);
                                not.clave_cc = General.generarClave(n.ControlCambio);
                                not.email = true;
                                General.Email(n.Usuario.Email, not.getSubject(3), not.generate(7));
                                break;
                            }
                        }
                        int autET = n.Tipo == "AutorizarE" ? 1 : 2;
                        // saber que act o ser tiene el usuario
                        var act = (from a in BD.ActividadesControl where a.fk_CC == n.fk_CC && a.Actividades.Responsable == n.fk_U select a).ToList();
                        var sevapp = (from s in BD.ControlServicio where s.fk_CC == n.fk_CC && s.ServiciosAplicaciones.Dueno == n.fk_U select s).ToList();
                        if (act.Count > 0)
                        {
                            Notificacion not = new Notificacion(n.ControlCambio.Id_CC, 1);
                            not.fecha_ejecucion_cc = n.ControlCambio.FechaEjecucion.ToString().Substring(0, 10);
                            not.clave_cc = General.generarClave(n.ControlCambio);
                            not.email = true;
                            General.Email(n.Usuario.Email, not.getSubject(3), not.generate(autET));
                        }
                        if (sevapp.Count > 0)
                        {
                            Notificacion not = new Notificacion(n.ControlCambio.Id_CC, 2);
                            not.fecha_ejecucion_cc = n.ControlCambio.FechaEjecucion.ToString().Substring(0, 10);
                            not.clave_cc = General.generarClave(n.ControlCambio);
                            not.email = true;
                            General.Email(n.Usuario.Email, not.getSubject(3), not.generate(autET));

                        }
                    }
                    // enviar notificación al admin de que se recordaron a los usuarios anteriores
                    noti.noAutorizo = notis;
                    noti.email = true;
                    General.Email(admin, noti.getSubject(3), noti.generate(3));
                }
                Thread.Sleep(86400000);
            }
        }
        protected void Application_Start()
        {
        var currentMonitoreo = new Thread(monitoreo)
        {
            IsBackground = true,
            Name = "ThreadMonitoreo"
        };
        currentMonitoreo.Start();
        AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
