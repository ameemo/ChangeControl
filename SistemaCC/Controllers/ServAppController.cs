﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SistemaCC.Models;

namespace SistemaCC.Controllers
{
    public class ServAppController : Controller
    {
        static BDControlCambioDataContext BD = new BDControlCambioDataContext();
        Mensajes Mensaje = new Mensajes();
        List<Notificaciones> _notificaciones = (from n in BD.Notificaciones where n.fk_U == 1 select n).ToList();
        // GET: ServApp
        public ActionResult Index(string mensaje)
        {
            ViewBag.Notificaciones = _notificaciones;
            var datos = (from a in BD.ServiciosAplicaciones select a);
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

        // GET: ServApp/Ver/5
        public ActionResult Ver(int id)
        {
            ViewBag.Notificaciones = _notificaciones;
            ViewBag.Modelo = (from sa in BD.ServiciosAplicaciones where sa.Id_SA == id select sa).SingleOrDefault();
            return View();
        }

        // GET: ServApp/Crear
        public ActionResult Crear()
        {
            ViewBag.Notificaciones = _notificaciones;
            var usuarios = (from a in BD.Usuario select a).ToList();
            List<Usuario> usuarios2 = new List<Usuario>();
            foreach (var usuario in usuarios)
            {
                usuarios2.Add(new Usuario { Id_U = usuario.Id_U, Nombre = usuario.Nombre + " " + usuario.ApePaterno + " " + usuario.ApeMaterno });
            }
            ViewData["usuarios"] = new SelectList(usuarios2, "Id_U", "Nombre");
            return View();
        }

        // POST: ServApp/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(ServiciosAplicaciones modelo, FormCollection collection)
        {
            try
            {
                ServiciosAplicaciones servapp = new ServiciosAplicaciones();
                servapp.Nombre = modelo.Nombre;
                servapp.Descripcion = modelo.Descripcion;
                servapp.Acronimo = modelo.Acronimo;
                servapp.Activo = true;
                servapp.Dueno = modelo.Dueno;
                BD.ServiciosAplicaciones.InsertOnSubmit(servapp);
                BD.SubmitChanges();
                return RedirectToAction("Index", new { mensaje = "C5" });
            }
            catch
            {
                return RedirectToAction("Index", new { mensaje = "E1" });
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