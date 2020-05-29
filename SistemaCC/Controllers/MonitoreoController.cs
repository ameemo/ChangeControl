using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaCC.Models;

namespace SistemaCC.Controllers
{
    public class MonitoreoController : Controller
    {
        BDControlCambioDataContext BD = new BDControlCambioDataContext();
        static HomeController General = new HomeController();
        Mensajes Mensaje = new Mensajes();
        int Sesion = General.Sesion;
        // GET: Monitoreo
        public ActionResult Index(string mensaje)
        {
            // Notificaciones para navbar
            List<ControlCambio> ccs = (from n in BD.Notificaciones join cc in BD.ControlCambio on n.fk_CC equals cc.Id_CC where n.fk_U == Sesion && n.Activa == true select cc).ToList();
            var rol = (from ur in BD.UsuarioRol where ur.fk_Us == Sesion && (ur.fk_Rol == 2 || ur.fk_Rol == 3) select ur).SingleOrDefault();
            ViewData["NavRol"] = rol != null ? "Admin" : "Funcional";
            ViewData["NavNombre"] = (from u in BD.Usuario where u.Id_U == Sesion select u.Nombre).SingleOrDefault();
            ViewBag.Notificaciones_claves = General.generarListaClave(ccs);
            ViewBag.Notificaciones = (from n in BD.Notificaciones where n.fk_U == Sesion && n.Activa select n).ToList();
            //validar rol
            if(rol == null)
            {
                return RedirectToAction("./../Home/Index");
            }
            // Consultas para mostrar informacion
            ViewBag.Tiempos = (from m in BD.Monitoreo where m.Tipo == "Tiempo" orderby m.Fecha descending select m).ToList();
            ViewBag.Cantidad = (from m in BD.Monitoreo where m.Tipo == "Cantidad" orderby m.Fecha descending select m).ToList();
            //Seccion de mensajes para la vista
            string MC = "";
            string MA = "";
            string ME = "";
            if (mensaje != null)
            {
                int numero = Convert.ToInt32(mensaje.Substring(1, mensaje.Length - 1));
                if (mensaje.Substring(0, 1) == "C")
                {
                    MC = Mensaje.getMConfirmacion(numero);
                }
                if(mensaje.Substring(0,1) == "A")
                {
                    MA = Mensaje.getMAdvertencia(numero);
                }
                if (mensaje.Substring(0, 1) == "E")
                {
                    ME = Mensaje.getMError(numero);
                }
            }
            ViewData["MC"] = MC;
            ViewData["MA"] = MA;
            ViewData["ME"] = ME;
            return View();
        }

        // GET: Monitoreo/Create
        public ActionResult Crear(string tipo)
        {
            var rol = (from ur in BD.UsuarioRol where ur.fk_Us == Sesion && (ur.fk_Rol == 2 || ur.fk_Rol == 3) select ur).SingleOrDefault();
            //validar rol
            if (rol == null)
            {
                return RedirectToAction("./../Home/Index");
            }
            ViewData["Tipo"] = tipo;
            return View();
        }

        // POST: Monitoreo/Create
        [HttpPost]
        public ActionResult Crear(string tipo, FormCollection collection)
        {
            try
            {
                List<Monitoreo> ultimo = (from m in BD.Monitoreo where m.Tipo == tipo orderby m.Fecha descending select m).ToList();
                if(ultimo[0].Cantidad == Int32.Parse(collection["cantidad"]))
                {
                    return RedirectToAction("Index", new
                    {
                        mensaje = "A4"
                    });
                }
                Monitoreo monitoreo = new Monitoreo();
                monitoreo.fk_U = 1;
                monitoreo.Cantidad = Int32.Parse(collection["cantidad"]);
                monitoreo.Fecha = DateTime.Now;
                monitoreo.Tipo = tipo;
                BD.Monitoreo.InsertOnSubmit(monitoreo);
                BD.SubmitChanges();
                return RedirectToAction("Index", new
                {
                    mensaje = "C12"
                });
            }
            catch
            {
                return RedirectToAction("Index", new
                {
                    mensaje = "E1"
                });
            }
        }
    }
}
