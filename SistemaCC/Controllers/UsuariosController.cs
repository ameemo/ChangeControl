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
            ViewBag.Roles = (from r in BD.Roles select r).ToList();
            ViewData["ME1"] = "";
            ViewData["ME2"] = "";
            return View();
        }

        // POST: Usuarios/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Usuario modelo, FormCollection collection)
        {
            ViewBag.Roles = (from r in BD.Roles select r).ToList();
            try
            {
                var correo = (from u in BD.Usuario where u.Email == modelo.Email select u).ToList();
                if(correo.Count != 0)
                {
                    ViewData["ME1"] = "El correo ingresado ya ha sido registrado";
                    return View();
                }
                Usuario usuario = new Usuario();
                usuario.Nombre = modelo.Nombre;
                usuario.ApePaterno = modelo.ApePaterno;
                usuario.ApeMaterno = modelo.ApeMaterno;
                usuario.NoExt = modelo.NoExt;
                usuario.Email = modelo.Email;
                usuario.Activo = true;
                return View();
                //return RedirectToAction(nameof(Index));
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