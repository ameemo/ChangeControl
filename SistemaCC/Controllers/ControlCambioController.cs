using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;
using System.Web.Mvc;
using SistemaCC.Models;
using System.Web;
using System.IO;

namespace SistemaCC.Controllers
{
    public class ControlCambioController : Controller
    {
        BDControlCambioDataContext BD = new BDControlCambioDataContext();
        DateTime fecha(string fecha)
        {
            int dia, mes, anio;
            dia = Convert.ToInt32(fecha.Substring(8, 2));
            mes = Convert.ToInt32(fecha.Substring(5, 2));
            anio = Convert.ToInt32(fecha.Substring(0, 4));
            return new DateTime(anio, mes, dia);
        }
        //Funcion para dar formato a las actividades
        List<Actividades> actividades(string[] act, string tipo)
        {
            List<Actividades> actividades = new List<Actividades>();
            string[] act_prev_desc = act[0].Split(new string[] { "&,", "&" }, StringSplitOptions.RemoveEmptyEntries);
            string[] act_prev_obs = act[1].Split(new string[] { "&,", "&" }, StringSplitOptions.RemoveEmptyEntries);
            string[] act_prev_fecha = act[2].Split(new Char[] { ','});
            string[] act_prev_usuario = act[3].Split(new Char[] { ',' });
            //Remover el último '&'
            for (var i = 0; i < act_prev_desc.Length; i++)
            {
                Actividades actividades_ = new Actividades();
                actividades_.Descripcion = act_prev_desc[i];
                actividades_.Observaciones = act_prev_obs[i];
                actividades_.FechaRealizacion = fecha(act_prev_fecha[i]);
                actividades_.Responsable = Convert.ToInt32(act_prev_usuario[i]);
                actividades_.Tipo = tipo;
                actividades.Add(actividades_);
            }
            return actividades;
        }
        //Funcion para insertar las actividades
        void anadir_actividades(int id_CC, string[] act_prev, string[] act_cc)
        {
            if (!act_prev.Contains(null))
            {
                List<Actividades> act_prev_ = actividades(act_prev, "Previa");
                foreach (var a in act_prev_)
                {
                    BD.Actividades.InsertOnSubmit(a);
                    BD.SubmitChanges();
                    //Se inserta en la tabla intermedia
                    ActividadesControl ac = new ActividadesControl();
                    ac.fk_CC = id_CC;
                    ac.fk_Ac = a.Id_Ac;
                    BD.ActividadesControl.InsertOnSubmit(ac);
                    BD.SubmitChanges();
                }
            }
            if (!act_cc.Contains(null))
            {
                List<Actividades> act_cc_ = actividades(act_cc, "ControlCambio");
                foreach (var a in act_cc_)
                {
                    BD.Actividades.InsertOnSubmit(a);
                    BD.SubmitChanges();
                    //Se inserta en la tabla intermedia
                    ActividadesControl ac = new ActividadesControl();
                    ac.fk_CC = id_CC;
                    ac.fk_Ac = a.Id_Ac;
                    BD.ActividadesControl.InsertOnSubmit(ac);
                    BD.SubmitChanges();
                }
            }
        }
        //Funcion para dar formato a las actividades
        List<Riesgos> riesgos(string ries, string tipo, int id_CC)
        {
            List<Riesgos> riesgos = new List<Riesgos>();
            string[] ries_desc = ries.Split(new string[] { "&,", "&" }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < ries_desc.Length; i++)
            {
                Riesgos riesgos_ = new Riesgos();
                riesgos_.fk_CC = id_CC;
                riesgos_.Descripcion = ries_desc[i];
                riesgos_.Tipo = tipo;
                riesgos.Add(riesgos_);
            }
            return riesgos;
        }
        //Funcion para insertar las riesgos
        void anadir_riesgos(int id_CC, string ries, string ries_no)
        {
            if (ries != null)
            {
                List<Riesgos> ries_ = riesgos(ries, "ControlCambio", id_CC);
                foreach (var r in ries_)
                {
                    BD.Riesgos.InsertOnSubmit(r);
                    BD.SubmitChanges();
                }
            }
            if (ries_no != null)
            {
                List<Riesgos> ries_no_ = riesgos(ries_no, "No", id_CC);
                foreach (var r in ries_no_)
                {
                    BD.Riesgos.InsertOnSubmit(r);
                    BD.SubmitChanges();
                }
            }
        }
        //Funcion para dar formato a los servicios
        void servicios(int id_CC, string[] ser)
        {
            List<ControlServicio> servicios = new List<ControlServicio>();
            if(!ser.Contains(null))
            {
                string[] id_CS = ser[0].Split(new Char[] { '&', ',' });
                string[] fecha_inicio = ser[1].Split(new Char[] { '&', ',' });
                string[] fecha_termino = ser[2].Split(new Char[] { ',' });
                for (var i = 0; i < id_CS.Length; i++)
                {
                    ControlServicio servicios_ = new ControlServicio();
                    servicios_.fk_CC = id_CC;
                    servicios_.fk_SA = Convert.ToInt32(id_CS[i]);
                    servicios_.FechaInicio = fecha(fecha_inicio[i]);
                    servicios_.FechaFinal = fecha(fecha_termino[i]);
                    servicios.Add(servicios_);
                }
                anadir_servicios(servicios);
            }
        }
        //Funcion para insertar los servicios
        void anadir_servicios(List<ControlServicio> servicios)
        {
            if (servicios != null)
            {
                foreach (var s in servicios)
                {
                    BD.ControlServicio.InsertOnSubmit(s);
                    BD.SubmitChanges();
                }
            }
        }
        void adjuntos(int id_CC, HttpPostedFileBase[] adjuntos)
        {
            List<Documentos> documentos = new List<Documentos>();
            if (adjuntos != null)
            {
                foreach (var a in adjuntos)
                {
                    string path = Path.Combine(Server.MapPath("~/Archivos/CC_" + id_CC + "/Adjuntos/"), Path.GetFileName(a.FileName));
                    Documentos documento = new Documentos();
                    a.SaveAs(path);
                    documento.DocPath = path;
                    documento.fk_CC = id_CC;
                    documentos.Add(documento);
                }
                anadir_adjuntos(documentos);
            }
        }
        //Funcion para insertar los adjuntos
        void anadir_adjuntos(List<Documentos> documentos) 
        {
            if(documentos != null)
            {
                foreach(var d in documentos)
                {
                    BD.Documentos.InsertOnSubmit(d);
                    BD.SubmitChanges();
                }
            }
        }
        
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
        public ActionResult Crear(ControlCambio model, HttpPostedFileBase[] adjuntos_, FormCollection collection)
        {
            try
            {
                //PRIMERO: Insertar en la tabla control de cambio
                ControlCambio controlCambio = new ControlCambio();
                controlCambio.Titulo = model.Titulo;
                controlCambio.FechaEjecucion = model.FechaEjecucion;
                controlCambio.Introduccion = model.Introduccion;
                controlCambio.Objetivos = model.Objetivos;
                controlCambio.Tipo = model.Tipo;
                controlCambio.Estado = "Creado";
                controlCambio.Creador = 1;
                BD.ControlCambio.InsertOnSubmit(controlCambio);
                BD.SubmitChanges();
                //Llamar anadir actividades
                string[] act_prev = new string[4];
                string[] act_cc = new string[4];
                act_prev[0] = collection["actividades_prev_descripcion"];
                act_prev[1] = collection["actividades_prev_observaciones"];
                act_prev[2] = collection["actividades_prev_fecha"];
                act_prev[3] = collection["actividades_prev_usuarios"];
                act_cc[0] = collection["actividades_cc_descripcion"];
                act_cc[1] = collection["actividades_cc_observaciones"];
                act_cc[2] = collection["actividades_cc_fecha"];
                act_cc[3] = collection["actividades_cc_usuarios"];
                anadir_actividades(controlCambio.Id_CC, act_prev, act_cc);
                //Llamar anadir riesgos
                string ries = collection["riesgos_descripcion"];
                string ries_no = collection["riesgos_no_descripcion"];
                anadir_riesgos(controlCambio.Id_CC, ries, ries_no);
                //Llamar anadir servicios
                string[] servicios_ = new string[3];
                servicios_[0] = collection["servicio_servicios"];
                servicios_[1] = collection["servicio_inicio"];
                servicios_[2] = collection["servicio_temino"];
                servicios(controlCambio.Id_CC, servicios_);
                //Llamar a adjuntos
                adjuntos(controlCambio.Id_CC, adjuntos_);
                return RedirectToAction("./../Home/Index");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Console.Write(ex);
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