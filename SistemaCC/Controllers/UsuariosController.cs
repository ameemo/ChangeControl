using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SistemaCC.Controllers.Clases;
using SistemaCC.Models;

namespace SistemaCC.Controllers
{
    public class UsuariosController : Controller
    {
        BDControlCambioDataContext BD = new BDControlCambioDataContext();
        static HomeController General = new HomeController();
        Mensajes Mensaje = new Mensajes();
        int Sesion = General.Sesion;
        //Función para agregar los roles
        int anadir_roles(int id_U, string roles)
        {
            if(roles != null)
            {
                string[] rol = roles.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach(var r in rol)
                {
                    // Validar si es Administrador o S Administrador, ya que sólo puede haber 1
                    if(Convert.ToInt32(r) == 2)
                    {
                        List<UsuarioRol> usuariosRol = (from urp in BD.UsuarioRol where urp.fk_Rol == 2 && urp.Usuario.Activo == true select urp).ToList();
                        if(usuariosRol.Count == 1) { return 19; }
                    }
                    if(Convert.ToInt32(r) == 3)
                    {
                        List<UsuarioRol> usuariosRol = (from urp in BD.UsuarioRol where urp.fk_Rol == 3 && urp.Usuario.Activo == true select urp).ToList();
                        if(usuariosRol.Count == 1) { return 20; }
                    }
                    UsuarioRol ur = new UsuarioRol();
                    ur.fk_Rol = Convert.ToInt32(r);
                    ur.fk_Us = id_U;
                    BD.UsuarioRol.InsertOnSubmit(ur);
                    BD.SubmitChanges();
                }
            }
            return 0;
        }
        // Funcion para eliminar roles
        void eliminar_roles(string usuarioRoles)
        {
            if(usuarioRoles != null)
            {
                string[] usuarioRol = usuarioRoles.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for(var i = 0; i < usuarioRol.Length; i++)
                {
                    UsuarioRol ur = (from urol in BD.UsuarioRol where urol.Id_UR == Int32.Parse(usuarioRol[i]) select urol).SingleOrDefault();
                    BD.UsuarioRol.DeleteOnSubmit(ur);
                    BD.SubmitChanges();
                }
            }
        }
        // GET: Usuarios
        public ActionResult Index(string mensaje)
        {
            // Notificaciones para navbar
            List<ControlCambio> ccs = (from n in BD.Notificaciones join cc in BD.ControlCambio on n.fk_CC equals cc.Id_CC where n.fk_U == Sesion && n.Activa == true select cc).ToList();
            var rol = (from ur in BD.UsuarioRol where ur.fk_Us == Sesion && (ur.fk_Rol == 2 || ur.fk_Rol == 3) select ur).SingleOrDefault();
            ViewData["NavRol"] = rol != null ? "Admin" : "Funcional";
            ViewData["NavNombre"] = (from u in BD.Usuario where u.Id_U == Sesion select u.Nombre).SingleOrDefault();
            ViewBag.Notificaciones_claves = General.generarListaClave(ccs);
            ViewBag.Notificaciones = (from n in BD.Notificaciones where n.fk_U == Sesion && n.Activa select n).ToList();
            // Demas codigo
            var datos = (from a in BD.Usuario select a).ToList();
            ViewBag.datos = datos;
            //Seccion de mensajes para la vista
            string MC = "";
            string ME = "";
            if (mensaje != null)
            {
                int numero = Convert.ToInt32(mensaje.Substring(1, mensaje.Length - 1));
                if (mensaje.Substring(0, 1) == "C")
                {
                    MC = Mensaje.getMConfirmacion(numero);
                }
                else
                {
                    ME = Mensaje.getMError(numero);
                }
            }
            ViewData["MC"] = MC;
            ViewData["ME"] = ME;
            return View();
        }

        // GET: Usuarios/Ver/5
        public ActionResult Ver(int id)
        {
            // Notificaciones para navbar
            List<ControlCambio> ccs = (from n in BD.Notificaciones join cc in BD.ControlCambio on n.fk_CC equals cc.Id_CC where n.fk_U == Sesion && n.Activa == true select cc).ToList();
            var rol = (from ur in BD.UsuarioRol where ur.fk_Us == Sesion && (ur.fk_Rol == 2 || ur.fk_Rol == 3) select ur).SingleOrDefault();
            ViewData["NavRol"] = rol != null ? "Admin" : "Funcional";
            ViewData["NavNombre"] = (from u in BD.Usuario where u.Id_U == Sesion select u.Nombre).SingleOrDefault();
            ViewBag.Notificaciones_claves = General.generarListaClave(ccs);
            ViewBag.Notificaciones = (from n in BD.Notificaciones where n.fk_U == Sesion && n.Activa select n).ToList();
            // Demas codigo
            ViewBag.Modelo = (from u in BD.Usuario where u.Id_U == id select u).SingleOrDefault();
            ViewBag.Roles = (from ur in BD.UsuarioRol where ur.fk_Us == id select ur).ToList();
            return View();
        }

        // GET: Usuarios/Crear
        public ActionResult Crear()
        {
            // Notificaciones para navbar
            List<ControlCambio> ccs = (from n in BD.Notificaciones join cc in BD.ControlCambio on n.fk_CC equals cc.Id_CC where n.fk_U == Sesion && n.Activa == true select cc).ToList();
            var rol = (from ur in BD.UsuarioRol where ur.fk_Us == Sesion && (ur.fk_Rol == 2 || ur.fk_Rol == 3) select ur).SingleOrDefault();
            ViewData["NavRol"] = rol != null ? "Admin" : "Funcional";
            ViewData["NavNombre"] = (from u in BD.Usuario where u.Id_U == Sesion select u.Nombre).SingleOrDefault();
            ViewBag.Notificaciones_claves = General.generarListaClave(ccs);
            ViewBag.Notificaciones = (from n in BD.Notificaciones where n.fk_U == Sesion && n.Activa select n).ToList();
            // Demas codigo
            ViewBag.Roles = (from r in BD.Roles select r).ToList();
            ViewData["ME1"] = Mensaje.getMError(0);
            ViewData["MA"] = Mensaje.getMAdvertencia(2);
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
                if (correo.Count != 0)
                {
                    // Notificaciones para navbar
                    List<ControlCambio> ccs = (from n in BD.Notificaciones join cc in BD.ControlCambio on n.fk_CC equals cc.Id_CC where n.fk_U == Sesion && n.Activa == true select cc).ToList();
                    var rol = (from ur in BD.UsuarioRol where ur.fk_Us == Sesion && (ur.fk_Rol == 2 || ur.fk_Rol == 3) select ur).SingleOrDefault();
                    ViewData["NavRol"] = rol != null ? "Admin" : "Funcional";
                    ViewData["NavNombre"] = (from u in BD.Usuario where u.Id_U == Sesion select u.Nombre).SingleOrDefault();
                    ViewBag.Notificaciones_claves = General.generarListaClave(ccs);
                    ViewBag.Notificaciones = (from n in BD.Notificaciones where n.fk_U == Sesion && n.Activa select n).ToList();
                    // Demas codigo
                    ViewBag.Roles = (from r in BD.Roles select r).ToList();
                    ViewData["ME1"] = Mensaje.getMError(12);
                    ViewData["MA"] = Mensaje.getMAdvertencia(2);
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
                usuario.Contrasena = modelo.ApePaterno + "" + DateTime.Now.Year;
                BD.Usuario.InsertOnSubmit(usuario);
                BD.SubmitChanges();
                int res = anadir_roles(usuario.Id_U, collection["rol_input"]);
                // Enviar correo para informa que fue creado en el sistema
                Notificacion noti = new Notificacion();
                noti.emailAdmin = (from ur in BD.UsuarioRol where ur.fk_Rol == 3 select ur.Usuario.Email).SingleOrDefault();
                noti.contrasena = usuario.Contrasena;
                noti.email = true;
                General.Email(usuario.Email, noti.getSubject(10), noti.generate(10));
                return RedirectToAction("Index", new { mensaje = res == 0 ? "C7" : "E" + res });
            }
            catch(Exception e)
            {
                return RedirectToAction("Index", new { mensaje = "E1" });
            }
        }

        // GET: Usuarios/Editar/5
        public ActionResult Editar(int id)
        {
            // Notificaciones para navbar
            List<ControlCambio> ccs = (from n in BD.Notificaciones join cc in BD.ControlCambio on n.fk_CC equals cc.Id_CC where n.fk_U == Sesion && n.Activa == true select cc).ToList();
            var rol = (from ur in BD.UsuarioRol where ur.fk_Us == Sesion && (ur.fk_Rol == 2 || ur.fk_Rol == 3) select ur).SingleOrDefault();
            ViewData["NavRol"] = rol != null ? "Admin" : "Funcional";
            ViewData["NavNombre"] = (from u in BD.Usuario where u.Id_U == Sesion select u.Nombre).SingleOrDefault();
            ViewBag.Notificaciones_claves = General.generarListaClave(ccs);
            ViewBag.Notificaciones = (from n in BD.Notificaciones where n.fk_U == Sesion && n.Activa select n).ToList();
            // Codigo para saber que roles tiene en este momendo el usuario
            List<Roles> Roles = (from r in BD.Roles select r).ToList();
            List<UsuarioRol> UsuarioRoles = (from r in BD.UsuarioRol where r.fk_Us == id select r).ToList();
            List<List<string>> rol = new List<List<string>>();
            foreach (var r in Roles)
            {
                Boolean anadir = true;
                List<string> list = new List<string>();
                foreach (var ur in UsuarioRoles)
                {
                    if (r.Id_Rol == ur.fk_Rol)
                    {
                        anadir = false;
                        list.Add("" + ur.Id_UR);
                        list.Add(r.Rol);
                        list.Add("1");
                        rol.Add(list);
                        break;
                    }
                    else
                    {
                        anadir = true;
                    }
                }
                if (anadir)
                {
                    list.Add("" + r.Id_Rol);
                    list.Add(r.Rol);
                    list.Add("0");
                    rol.Add(list);
                }
            }
            ViewBag.Roles = rol;
            // Codigo para el modelo y los mensajes
            Usuario model = (from u in BD.Usuario where u.Id_U == id select u).SingleOrDefault();
            ViewData["ME1"] = Mensaje.getMError(0);
            ViewData["MA"] = Mensaje.getMAdvertencia(2);
            return View(model);
        }

        // POST: Usuarios/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, Usuario modelo, FormCollection collection)
        {
            try
            {
                var correo = (from u in BD.Usuario where u.Email == modelo.Email && u.Id_U != id select u).ToList();
                if (correo.Count != 0)
                {
                    // Notificaciones para navbar
                    List<ControlCambio> ccs = (from n in BD.Notificaciones join cc in BD.ControlCambio on n.fk_CC equals cc.Id_CC where n.fk_U == Sesion && n.Activa == true select cc).ToList();
                    var rol = (from ur in BD.UsuarioRol where ur.fk_Us == Sesion && (ur.fk_Rol == 2 || ur.fk_Rol == 3) select ur).SingleOrDefault();
                    ViewData["NavRol"] = rol != null ? "Admin" : "Funcional";
                    ViewData["NavNombre"] = (from u in BD.Usuario where u.Id_U == Sesion select u.Nombre).SingleOrDefault();
                    ViewBag.Notificaciones_claves = General.generarListaClave(ccs);
                    ViewBag.Notificaciones = (from n in BD.Notificaciones where n.fk_U == Sesion && n.Activa select n).ToList();
                    // Codigo para saber que roles tiene en este momendo el usuario
                    List<Roles> Roles = (from r in BD.Roles select r).ToList();
                    List<UsuarioRol> UsuarioRoles = (from r in BD.UsuarioRol where r.fk_Us == id select r).ToList();
                    List<List<string>> rol = new List<List<string>>();
                    foreach (var r in Roles)
                    {
                        Boolean anadir = true;
                        List<string> list = new List<string>();
                        foreach (var ur in UsuarioRoles)
                        {
                            if (r.Id_Rol == ur.fk_Rol)
                            {
                                anadir = false;
                                list.Add("" + ur.Id_UR);
                                list.Add(r.Rol);
                                list.Add("1");
                                rol.Add(list);
                                break;
                            }
                            else
                            {
                                anadir = true;
                            }
                        }
                        if (anadir)
                        {
                            list.Add("" + r.Id_Rol);
                            list.Add(r.Rol);
                            list.Add("0");
                            rol.Add(list);
                        }
                    }
                    ViewBag.Roles = rol;
                    // Codigo para el modelo y los mensajes
                    Usuario model = (from u in BD.Usuario where u.Id_U == id select u).SingleOrDefault();
                    ViewData["ME1"] = Mensaje.getMError(12);
                    ViewData["MA"] = Mensaje.getMAdvertencia(2);
                    return View();
                }
                Usuario usuario = (from u in BD.Usuario where u.Id_U == id select u).SingleOrDefault();
                usuario.Nombre = modelo.Nombre.ToUpper();
                usuario.ApePaterno = modelo.ApePaterno.ToUpper();
                usuario.ApeMaterno = modelo.ApeMaterno.ToUpper();
                usuario.NoExt = modelo.NoExt;
                usuario.Email = modelo.Email;
                BD.SubmitChanges();
                eliminar_roles(collection["rol_input_eliminado"]);
                int res = anadir_roles(usuario.Id_U, collection["rol_input"]);
                return RedirectToAction("Index", new { mensaje = res == 0 ? "C13" : "E" + res });
            }
            catch
            {
                return RedirectToAction("Index", new { mensaje = "E1" });
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