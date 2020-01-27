using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;

namespace SistemaCC.Controllers
{
    public class ControlCambioController : Controller
    {
        
        // GET: ControlCambio
        public ActionResult Index()
        {
            return View();
        }

        // GET: ControlCambio/Ver/5
        public ActionResult Ver(int id)
        {
            return View();
        }

        // GET: ControlCambio/Crear
        public ActionResult Crear()
        {
            return View(); 
        }

        // POST: ControlCambio/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(ControlCambio model, IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                ControlCambio nuevo = new ControlCambio();
                nuevo.Titulo = model.Titulo;
                
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: ControlCambio/Editar/5
        public ActionResult Editar(int id)
        {
            return View();
        }

        // POST: ControlCambio/Editar/5
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

        // GET: ControlCambio/Cerrar/5
        public ActionResult Cerrar(int id)
        {
            return View();
        }

        // POST: ControlCambio/Cerrar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cerrar(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add Cerrar logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ControlCambio/Cerrar/5
        public ActionResult Revisar(int id)
        {
            return View();
        }

        // POST: ControlCambio/Cerrar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Revisar(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add Cerrar logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ControlCambio/Cerrar/5
        public ActionResult AnadirAdjuntos(int id)
        {
            return View();
        }

        // POST: ControlCambio/Cerrar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnadirAdjuntos(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add Cerrar logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ControlCambio/Cerrar/5
        public ActionResult Monitorear(int id)
        {
            return View();
        }

        // POST: ControlCambio/Cerrar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Monitorear(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add Cerrar logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ControlCambio/Cerrar/5
        public ActionResult Notificar(int id)
        {
            return View();
        }

        // POST: ControlCambio/Cerrar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Notificar(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add Cerrar logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}