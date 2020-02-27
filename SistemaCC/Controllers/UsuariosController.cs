using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SistemaCC.Models;

namespace SistemaCC.Controllers
{
    public class UsuariosController : Controller
    {
        BDControlCambioDataContext BD = new BDControlCambioDataContext();
        // GET: Usuarios
        public ActionResult Index()
        {
            var datos = (from a in BD.Usuario select a).ToList();
            ViewBag.datos = datos;
            return View();
        }

        // GET: Usuarios/Ver/5
        public ActionResult Ver(int id)
        {
            return View();
        }

        // GET: Usuarios/Crear
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Usuarios/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Usuario model, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Usuario row = new Usuario();
                row.Nombre = model.Nombre;
                BD.Usuario.InsertOnSubmit(row);
                BD.SubmitChanges();
                var rol = collection.Get("rol");
                UsuarioRol row2 = new UsuarioRol();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuarios/Editar/5
        public ActionResult Editar(int id)
        {
            return View();
        }

        // POST: Usuarios/Editar/5
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

        // GET: Usuarios/Bloquear/5
        public ActionResult Bloquear(int id)
        {
            var row = (from a in BD.Usuario where a.Id_U == id select a).SingleOrDefault();
            row.Activo = false;
            BD.SubmitChanges();
            return RedirectToAction("Index");
        }

        // GET: Usuarios/Bloquear/5
        public ActionResult Desbloquear(int id)
        {
            var row = (from a in BD.Usuario where a.Id_U == id select a).SingleOrDefault();
            row.Activo = true;
            BD.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}