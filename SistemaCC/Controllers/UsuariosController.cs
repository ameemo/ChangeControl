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
        Mensajes Mensaje = new Mensajes();
        //Función para agregar los roles
        void anadir_roles(int id_U, string roles)
        {
            if(roles != null)
            {
                string[] rol = roles.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for(var i = 0; i < rol.Length; i++)
                {
                    UsuarioRol ur = new UsuarioRol();
                    ur.fk_Rol = Convert.ToInt32(rol[i]);
                    ur.fk_Us = id_U;
                    BD.UsuarioRol.InsertOnSubmit(ur);
                    BD.SubmitChanges();
                }
            }
        }
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
            ViewBag.Modelo = (from u in BD.Usuario where u.Id_U == id select u).SingleOrDefault();
            ViewBag.Roles = (from ur in BD.UsuarioRol where ur.fk_Us == id select ur).ToList();
            return View();
        }

        // GET: Usuarios/Crear
        public ActionResult Crear()
        {
            ViewBag.Roles = (from r in BD.Roles select r).ToList();
            ViewData["ME1"] = Mensaje.getMError(0);
            return View();
        }

        // POST: Usuarios/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Usuario modelo, FormCollection collection)
        {
            try
            {
                var correo = (from u in BD.Usuario where u.Email == modelo.Email select u).ToList();
                if(correo.Count != 0)
                {
                    ViewBag.Roles = (from r in BD.Roles select r).ToList();
                    ViewData["ME1"] = Mensaje.getMError(12);
                    return View();
                }
                Usuario usuario = new Usuario();
                usuario.Nombre = modelo.Nombre.ToUpper();
                usuario.ApePaterno = modelo.ApePaterno.ToUpper();
                usuario.ApeMaterno = modelo.ApeMaterno.ToUpper();
                usuario.NoExt = modelo.NoExt;
                usuario.Email = modelo.Email;
                usuario.Activo = true;
                usuario.ClaveUnica = "";
                BD.Usuario.InsertOnSubmit(usuario);
                BD.SubmitChanges();
                anadir_roles(usuario.Id_U, collection["rol_input"]);
                return RedirectToAction("Index");
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