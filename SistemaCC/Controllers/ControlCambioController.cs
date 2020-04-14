using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;
using System.Web.Mvc;
using SistemaCC.Models;
using SistemaCC.Controllers;
using System.Web;
using System.IO;

namespace SistemaCC.Controllers
{
    public class ControlCambioController : Controller
    {
        BDControlCambioDataContext BD = new BDControlCambioDataContext();
        HomeController clave = new HomeController();
        Mensajes Mensaje = new Mensajes();
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
        //Funcion para insertar los documetos adjuntos
        void anadir_adjuntos(int id_CC, HttpPostedFileBase[] adjuntos, string tipo)
        {
            if (adjuntos != null)
            {
                if(tipo == "Adjunto")
                {
                    foreach (var a in adjuntos)
                    {
                        Documentos documento = new Documentos();
                        documento.DocPath = "";
                        documento.TipoDoc = tipo;
                        documento.fk_CC = id_CC;
                        BD.Documentos.InsertOnSubmit(documento);
                        BD.SubmitChanges();
                        //Creamos en archivo con el id que le corresponde
                        string carpetaCC = Path.Combine(Server.MapPath("~/Archivos/"), "CC_" + id_CC);
                        string carpetaA = Path.Combine(Server.MapPath("~/Archivos/CC_" + id_CC), "Adjuntos");
                        Directory.CreateDirectory(carpetaCC);
                        Directory.CreateDirectory(carpetaA);
                        string path = Path.Combine(Server.MapPath("~/Archivos/CC_" + id_CC + "/Adjuntos/"), documento.Id_Do + "_" + Path.GetFileName(a.FileName));
                        a.SaveAs(path);
                        documento.DocPath = path;
                        BD.SubmitChanges();
                    }
                }
                if(tipo == "Evidencia")
                {

                }
            }
        }
        // GET: ControlCambio/Ver/5
        public ActionResult Ver(int id)
        {
            // Notificaciones para navbar
            List<ControlCambio> ccs = (from n in BD.Notificaciones join cc in BD.ControlCambio on n.fk_CC equals cc.Id_CC where n.fk_U == 1 select cc).ToList();
            ViewBag.Notificaciones_claves = clave.generarListaClave(ccs);
            ViewBag.Notificaciones = (from n in BD.Notificaciones where n.fk_U == 1 select n).ToList();
            // Demas codigo
            ViewBag.Informacion = (from cc in BD.ControlCambio where cc.Id_CC == id select cc).SingleOrDefault();
            ViewBag.Actividades_Prev = (from ac in BD.ActividadesControl join ap in BD.Actividades on ac.fk_Ac equals ap.Id_Ac where ac.fk_CC == id && ap.Tipo == "Previa" select ap).ToList();
            ViewBag.Actividades_CC = (from ac in BD.ActividadesControl join ap in BD.Actividades on ac.fk_Ac equals ap.Id_Ac where ac.fk_CC == id && ap.Tipo == "ControlCambio" select ap).ToList();
            ViewBag.Servicios = (from sc in BD.ControlServicio join s in BD.ServiciosAplicaciones on sc.fk_SA equals s.Id_SA where sc.fk_CC == id select sc).ToList();
            ViewBag.Riesgos_CC = (from r in BD.Riesgos where r.fk_CC == id && r.Tipo == "ControlCambio" select r).ToList();
            ViewBag.Riesgos_No = (from r in BD.Riesgos where r.fk_CC == id && r.Tipo == "No" select r).ToList();
            var documentos = (from d in BD.Documentos where d.fk_CC == id && d.TipoDoc == "Adjunto" select d);
            List<Documentos> Documentos_imagenes = new List<Documentos>();
            List<Documentos> Documentos_pdf = new List<Documentos>();
            foreach(var doc in documentos)
            {
                var nombre_ = doc.DocPath.Split(new char[] { '\\' });
                var nombre = nombre_[nombre_.Length - 1];
                doc.DocPath = nombre;
                if (nombre.Contains(".pdf"))
                {
                    Documentos_pdf.Add(doc);
                }
                else
                {
                    Documentos_imagenes.Add(doc);
                }
            }
            ViewBag.Documentos_imagenes = Documentos_imagenes;
            ViewBag.Documentos_pdf = Documentos_pdf;
            return View();
        }

        // GET: ControlCambio/Crear
        public ActionResult Crear()
        {
            // Notificaciones para navbar
            List<ControlCambio> ccs = (from n in BD.Notificaciones join cc in BD.ControlCambio on n.fk_CC equals cc.Id_CC where n.fk_U == 1 select cc).ToList();
            ViewBag.Notificaciones_claves = clave.generarListaClave(ccs);
            ViewBag.Notificaciones = (from n in BD.Notificaciones where n.fk_U == 1 select n).ToList();
            // Demas codigo
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
                anadir_adjuntos(controlCambio.Id_CC, adjuntos_, "Adjunto");
                return RedirectToAction("./../Home/Index", new {
                    mensaje = "C8" });
                }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return RedirectToAction("./../Home/Index", new {
                    mensaje = "E1" });
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
            // Notificaciones para navbar
            List<ControlCambio> ccs = (from n in BD.Notificaciones join cc in BD.ControlCambio on n.fk_CC equals cc.Id_CC where n.fk_U == 1 select cc).ToList();
            ViewBag.Notificaciones_claves = clave.generarListaClave(ccs);
            ViewBag.Notificaciones = (from n in BD.Notificaciones where n.fk_U == 1 select n).ToList();
            // Demas codigo
            ViewBag.Informacion = (from cc in BD.ControlCambio where cc.Id_CC == id select cc).SingleOrDefault();
            ViewBag.Actividades_Prev = (from ac in BD.ActividadesControl join ap in BD.Actividades on ac.fk_Ac equals ap.Id_Ac where ac.fk_CC == id && ap.Tipo == "Previa" select ap).ToList();
            ViewBag.Actividades_CC = (from ac in BD.ActividadesControl join ap in BD.Actividades on ac.fk_Ac equals ap.Id_Ac where ac.fk_CC == id && ap.Tipo == "ControlCambio" select ap).ToList();
            ViewBag.Servicios = (from sc in BD.ControlServicio join s in BD.ServiciosAplicaciones on sc.fk_SA equals s.Id_SA where sc.fk_CC == id select sc).ToList();
            ViewBag.Riesgos_CC = (from r in BD.Riesgos where r.fk_CC == id && r.Tipo == "ControlCambio" select r).ToList();
            ViewBag.Riesgos_No = (from r in BD.Riesgos where r.fk_CC == id && r.Tipo == "No" select r).ToList();
            var documentos = (from d in BD.Documentos where d.fk_CC == id && d.TipoDoc == "Adjunto" select d);
            List<Documentos> Documentos_imagenes = new List<Documentos>();
            List<Documentos> Documentos_pdf = new List<Documentos>();
            foreach (var doc in documentos)
            {
                var nombre_ = doc.DocPath.Split(new char[] { '\\' });
                var nombre = nombre_[nombre_.Length - 1];
                doc.DocPath = nombre;
                if (nombre.Contains(".pdf"))
                {
                    Documentos_pdf.Add(doc);
                }
                else
                {
                    Documentos_imagenes.Add(doc);
                }
            }
            ViewBag.Documentos_imagenes = Documentos_imagenes;
            ViewBag.Documentos_pdf = Documentos_pdf;
            // Validacion para saber si ya ha habido una revision anterior
            var model = (from r in BD.Revisiones where r.fk_CC == id select r).ToList();
            Revisiones revision = new Revisiones();
            if(model.Count != 0)
            {
                revision = model[0];
                return View(revision);
            }
            else
            {
                return View();
            }
        }

        // POST: ControlCambio/Cerrar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Revisar(int id, Revisiones model, FormCollection collection)
        {
            try
            {
                //Revisamos sino existe una revision anterior
                var revisiones = (from r in BD.Revisiones where r.fk_CC == id select r).ToList();
                // Revisar si es aprobación o corrección
                var controlcambio = (from cc in BD.ControlCambio where cc.Id_CC == id select cc).SingleOrDefault();
                if (collection["corregir"] != null)
                {
                    controlcambio.Estado = "EnCorreccion";
                    BD.SubmitChanges();
                    if (revisiones.Count != 0)
                    {
                        // Actualizamos la información
                        revisiones[0].InfGeneral = model.InfGeneral;
                        revisiones[0].Actividades = model.Actividades;
                        revisiones[0].Servicios = model.Servicios;
                        revisiones[0].Riesgos = model.Riesgos;
                        revisiones[0].fk_CC = id;
                        BD.SubmitChanges();
                    }
                    else
                    {
                        // Insertamos la informacion de las notas
                        Revisiones revisiones_ = new Revisiones();
                        revisiones_.InfGeneral = model.InfGeneral;
                        revisiones_.Actividades = model.Actividades;
                        revisiones_.Servicios = model.Servicios;
                        revisiones_.Riesgos = model.Riesgos;
                        revisiones_.fk_CC = id;
                        BD.Revisiones.InsertOnSubmit(revisiones_);
                        BD.SubmitChanges();
                    }
                }
                if (collection["aprobar"] != null)
                {
                    controlcambio.Estado = "Aprobado";
                    BD.SubmitChanges();
                }
                return RedirectToAction("./../Home/Index", new
                {
                    mensaje = "C10"
                });
            }
            catch(Exception e)
            {
                return RedirectToAction("./../Home/Index", new
                {
                    mensaje = "E1"
                });
            }
        }

        // GET: ControlCambio/Cerrar/5
        public ActionResult Autorizar(int id, string tipo)
        {
            // Notificaciones para navbar
            List<ControlCambio> ccs = (from n in BD.Notificaciones join cc in BD.ControlCambio on n.fk_CC equals cc.Id_CC where n.fk_U == 1 select cc).ToList();
            ViewBag.Notificaciones_claves = clave.generarListaClave(ccs);
            ViewBag.Notificaciones = (from n in BD.Notificaciones where n.fk_U == 1 select n).ToList();
            // Demas codigo
            ViewBag.Informacion = (from cc in BD.ControlCambio where cc.Id_CC == id select cc).SingleOrDefault();
            ViewBag.Actividades_Prev = (from ac in BD.ActividadesControl join ap in BD.Actividades on ac.fk_Ac equals ap.Id_Ac where ac.fk_CC == id && ap.Tipo == "Previa" select ap).ToList();
            ViewBag.Actividades_CC = (from ac in BD.ActividadesControl join ap in BD.Actividades on ac.fk_Ac equals ap.Id_Ac where ac.fk_CC == id && ap.Tipo == "ControlCambio" select ap).ToList();
            ViewBag.Servicios = (from sc in BD.ControlServicio join s in BD.ServiciosAplicaciones on sc.fk_SA equals s.Id_SA where sc.fk_CC == id select sc).ToList();
            ViewBag.Riesgos_CC = (from r in BD.Riesgos where r.fk_CC == id && r.Tipo == "ControlCambio" select r).ToList();
            ViewBag.Riesgos_No = (from r in BD.Riesgos where r.fk_CC == id && r.Tipo == "No" select r).ToList();
            var documentos = (from d in BD.Documentos where d.fk_CC == id && d.TipoDoc == "Adjunto" select d);
            List<Documentos> Documentos_imagenes = new List<Documentos>();
            List<Documentos> Documentos_pdf = new List<Documentos>();
            foreach (var doc in documentos)
            {
                var nombre_ = doc.DocPath.Split(new char[] { '\\' });
                var nombre = nombre_[nombre_.Length - 1];
                doc.DocPath = nombre;
                if (nombre.Contains(".pdf"))
                {
                    Documentos_pdf.Add(doc);
                }
                else
                {
                    Documentos_imagenes.Add(doc);
                }
            }
            ViewBag.Documentos_imagenes = Documentos_imagenes;
            ViewBag.Documentos_pdf = Documentos_pdf;
            // Variables para los PASOS a seguir
            ViewData["Paso"] = "1";
            return View();
        }

        // POST: ControlCambio/Cerrar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Autorizar(int id, string tipo, FormCollection collection)
        {
            try
            {
                if(collection["Paso1"] != null)
                {
                    Autorizaciones aut = new Autorizaciones();
                    if (collection["autorizacion"] != null)
                    {
                        aut.Autorizado = true;
                    }
                    else
                    {
                        aut.Autorizado = false;
                    }
                    aut.Fecha = DateTime.Today;
                    aut.Tipo = tipo;
                    aut.fk_CC = id;
                    aut.fk_U = 1;
                    BD.Autorizaciones.InsertOnSubmit(aut);
                    BD.SubmitChanges();
                    ViewData["MA"] = Mensaje.getMAdvertencia(3);
                    ViewData["Paso"] = "2";
                    return View(aut);
                }
                if(collection["Paso2"] != null)
                {
                    // Comparacion de codigos 7u7
                    return RedirectToAction("./../Home/Index", new
                    {
                        mensaje = "C11"
                    });
                }
                return RedirectToAction("~/Home/Index", new
                {
                    mensaje = "E1"
                });
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
            Notificaciones model = new Notificaciones();
            string error = "";
            try
            {
                Notificaciones consulta = (from n in BD.Notificaciones where n.Id_No == id select n).SingleOrDefault();
                ControlCambio cc = (from control in BD.ControlCambio where control.Id_CC == consulta.fk_CC select control).SingleOrDefault();
                model = consulta;
                ViewBag.Contenido = consulta.Contenido.Split(new string[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
                ViewData["Clave"] = clave.generarClave(cc);
            }
            catch(Exception e)
            {
                error = Mensaje.getMError(1);
            }
            ViewData["ME"] = error;
            return View(model);
        }
        public ActionResult Calendario(int id)
        {
            DateTime hoy = DateTime.Today;
            DateTime inicio = new DateTime(hoy.Year, hoy.Month, 1);
            string[] meses = new string[] { "", 
                                            "Enero", 
                                            "Febrero", 
                                            "Marzo", 
                                            "Abril", 
                                            "Mayo", 
                                            "Junio", 
                                            "Julio", 
                                            "Agosto", 
                                            "Septiembre", 
                                            "Octubre", 
                                            "Noviembre", 
                                            "Diciembre" 
                                        };
            List<Dia> dias = new List<Dia>();
            List<string> claves = new List<string>();
            int tamano = DateTime.DaysInMonth(hoy.Year, hoy.Month);
            for (var i = 1; i <= tamano; i ++)
            {
                Dia dia = new Dia();
                DateTime dia_ = new DateTime(hoy.Year, hoy.Month, i);
                //Tomar los controles que pertenecen a el usuario
                List<ControlCambio> ccs = (from cc in BD.ControlCambio where cc.FechaEjecucion == dia_ && cc.Creador == id && cc.Estado == "Autorizado" select cc).ToList();
                if (ccs.Count > 0)
                {
                    claves = clave.generarListaClave(ccs);
                    dia.setControlCambio(ccs, claves);
                }
                //Tomar los controles de cambio donde es responsable de actividades
                List<ControlCambio> actividades = (from ac in BD.ActividadesControl join a in BD.Actividades on ac.fk_Ac equals a.Id_Ac join cc in BD.ControlCambio on ac.fk_CC equals cc.Id_CC where a.FechaRealizacion == dia_ && cc.Creador == id && cc.Estado == "Autorizado" select cc).ToList();
                if (actividades.Count > 0)
                {
                    claves = clave.generarListaClave(actividades);
                    dia.setActividad(actividades, claves);
                }
                //Tomar los controles donde es afectado algún servicio del usuario
                List<ControlCambio> servapp = (from sc in BD.ControlServicio join cc in BD.ControlCambio on sc.fk_CC equals cc.Id_CC join sa in BD.ServiciosAplicaciones on sc.fk_SA equals sa.Id_SA where (sc.FechaInicio <= dia_ && sc.FechaFinal >= dia_) && cc.Creador == id && cc.Estado == "Autorizado" select cc).ToList();
                //Tomar los servicios para el nombre
                List<ServiciosAplicaciones> servapp_nombres = (from sc in BD.ControlServicio join cc in BD.ControlCambio on sc.fk_CC equals cc.Id_CC join sa in BD.ServiciosAplicaciones on sc.fk_SA equals sa.Id_SA where (sc.FechaInicio <= dia_ && sc.FechaFinal >= dia_) && sa.Dueno == id && cc.Estado == "Autorizado" select sa).ToList();
                if (servapp.Count > 0)
                {
                    claves = clave.generarListaClave(servapp);
                    dia.setServApp(servapp, claves, servapp_nombres);
                }
                dias.Add(dia);
            }
            ViewData["inicio"] = Convert.ToInt32(inicio.DayOfWeek); 
            ViewData["mes"] = meses[hoy.Month];
            ViewBag.Dias = dias;
            return View();
        }
    }
}