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
        HomeController General = new HomeController();
        // GET: Monitoreo
        public ActionResult Index(string mensaje)
        {
            // Notificaciones para navbar
            List<ControlCambio> ccs = (from n in BD.Notificaciones join cc in BD.ControlCambio on n.fk_CC equals cc.Id_CC where n.fk_U == 1 select cc).ToList();
            ViewBag.Notificaciones_claves = General.generarListaClave(ccs);
            ViewBag.Notificaciones = (from n in BD.Notificaciones where n.fk_U == 1 select n).ToList();
            // Consultas para mostrar informacion
            ViewBag.Tiempos = (from m in BD.Monitoreo where m.Tipo == "Tiempo" select m).ToList();
            ViewBag.Cantidad = (from m in BD.Monitoreo where m.Tipo == "Cantidad" select m).ToList();
            return View();
        }

        // GET: Monitoreo/Create
        public ActionResult Crear(string tipo)
        {
            ViewData["Tipo"] = tipo;
            Monitoreo m = new Monitoreo();
            m.Cantidad = 1;
            return View(m);
        }

        // POST: Monitoreo/Create
        [HttpPost]
        public ActionResult Crear(Monitoreo model, string tipo, FormCollection collection)
        {
            try
            {
                Monitoreo monitoreo = new Monitoreo();
                monitoreo.fk_U = 1;
                monitoreo.Cantidad = Int32.Parse(collection["cantidad"]);
                monitoreo.Fecha = DateTime.Today;
                monitoreo.Tipo = tipo;
                BD.Monitoreo.InsertOnSubmit(monitoreo);
                BD.SubmitChanges();
                return RedirectToAction("Index", new
                {
                    mensaje = "C"
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
