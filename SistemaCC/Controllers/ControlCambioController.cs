using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;
using System.Web.Mvc;
using SistemaCC.Models;

namespace SistemaCC.Controllers
{
    public class ControlCambioController : Controller
    {
        BDControlCambioDataContext BD = new BDControlCambioDataContext();

        // GET: ControlCambio/Ver/5
        public ActionResult Ver(int id)
        {
            return View();
        }

        // GET: ControlCambio/Crear
        public ActionResult Crear()
        {
            var usuarios = (from a in BD.Usuario select a).ToList();
            List<Usuario> usuarios2 = new List<Usuario>();
            foreach (var usuario in usuarios)
            {
                usuarios2.Add(new Usuario { Id_U = usuario.Id_U, Nombre = usuario.Nombre + " " + usuario.ApePaterno + " " + usuario.ApeMaterno });
            }
            var servicios = (from a in BD.ServiciosAplicaciones select a).ToList();
            ViewBag.servicios = servicios;
            ViewBag.usuarios = usuarios2;
            return View();
        }

        // POST: ControlCambio/Crear
        [HttpPost]
        public ActionResult Crear(ControlCambio model, FormCollection collection)
        {
            try
            {
                //TODO: Add insert logic here
                return RedirectToAction("./../Home/Index");
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
        public ActionResult Editar(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("~/Home/Index");
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
        public ActionResult Cerrar(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add Cerrar logic here

                return RedirectToAction("~/Home/Index");
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
        public ActionResult Revisar(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add Cerrar logic here

                return RedirectToAction("~/Home/Index");
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
        public ActionResult AnadirAdjuntos(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add Cerrar logic here

                return RedirectToAction("~/Home/Index");
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
        public ActionResult Monitorear(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add Cerrar logic here

                return RedirectToAction("~/Home/Index");
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
        public ActionResult Notificar(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add Cerrar logic here

                return RedirectToAction("~/Home/Index");
            }
            catch
            {
                return View();
            }
        }
    }
}