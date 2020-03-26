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
        public ActionResult Index()
        {
            ViewBag.Revision_CC = (from cc in BD.ControlCambio where cc.Estado == "EnEvaluacion" select cc).ToList();
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
