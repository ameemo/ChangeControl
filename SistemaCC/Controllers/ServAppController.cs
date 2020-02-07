using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SistemaCC.Models;

namespace SistemaCC.Controllers
{
    public class ServAppController : Controller
    {
        BDControlCambioDataContext BD = new BDControlCambioDataContext();
        // GET: ServApp
        public ActionResult Index()
        {
            var datos = (from a in BD.ServiciosAplicaciones select a);
            ViewBag.datos = datos;
            return View();
        }

        // GET: ServApp/Ver/5
        public ActionResult Ver(int id)
        {
            return View();
        }

        // GET: ServApp/Crear
        public ActionResult Crear()
        {
            var usuarios = (from a in BD.Usuario select a);
            ViewData["usuarios"] = new SelectList(usuarios, "Id_U", "Nombre");
            return View();
        }

        // POST: ServApp/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ServApp/Editar/5
        public ActionResult Editar(int id)
        {
            return View();
        }

        // POST: ServApp/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ServApp/Bloquear/5
        public ActionResult Bloquear(int id)
        {
            return View();
        }

        // POST: ServApp/Bloquear/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bloquear(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add Bloquear logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}