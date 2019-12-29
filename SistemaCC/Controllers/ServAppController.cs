using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SistemaCC.Controllers
{
    public class ServAppController : Controller
    {
        // GET: ServApp
        public ActionResult Index()
        {
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
            return View();
        }

        // POST: ServApp/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(IFormCollection collection)
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
        public ActionResult Editar(int id, IFormCollection collection)
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
        public ActionResult Bloquear(int id, IFormCollection collection)
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