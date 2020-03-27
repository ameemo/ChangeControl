using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SistemaCC.Models;

namespace SistemaCC.Controllers
{
    public class HomeController : Controller
    {
        BDControlCambioDataContext BD = new BDControlCambioDataContext();
        Mensajes Mensaje = new Mensajes();
        public ActionResult Index(string mensaje)
        {
            ViewBag.Revision_CC = (from cc in BD.ControlCambio where cc.Estado == "EnEvaluacion" select cc).ToList();
            //Seccion de mensajes para la vista
            string MC = "";
            string ME = "";
            if (mensaje != null)
            {
                int numero = Convert.ToInt32(mensaje.Substring(1, 1));
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

        public ActionResult About()
        {
            ViewData["Message"] = "Your application description page 90.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }

        public ActionResult MenuInicio()
        {
            return View();
        }
        public ActionResult CambiarContrasena()
        {
            return View();
        }
    }
}
