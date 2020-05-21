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
using SistemaCC.Controllers.Clases;

namespace SistemaCC.Controllers
{
    public class ControlCambioController : Controller
    {
        BDControlCambioDataContext BD = new BDControlCambioDataContext();
        static HomeController General = new HomeController();
        Mensajes Mensaje = new Mensajes();
        int Sesion = General.Sesion;
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
        //Funcion para actualizar las actividades
        void actualizar_actividades(string id_AC, string[] act)
        {
            if (!act.Contains(null))
            { 
                List<string> id_CA = id_AC.Split(new string[] { "&,", "&" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                string[] act_desc = act[0].Split(new string[] { "&,", "&" }, StringSplitOptions.RemoveEmptyEntries);
                string[] act_obs = act[1].Split(new string[] { "&,", "&" }, StringSplitOptions.RemoveEmptyEntries);
                string[] act_fecha = act[2].Split(new Char[] { ',' });
                string[] act_usuario = act[3].Split(new Char[] { ',' });
                foreach (var id in id_CA)
                {
                    ActividadesControl ac = (from actc in BD.ActividadesControl where actc.Id_CA == Int32.Parse(id) select actc).SingleOrDefault();
                    ac.Actividades.Descripcion = act_desc[id_CA.IndexOf(id)];
                    ac.Actividades.Observaciones = act_obs[id_CA.IndexOf(id)];
                    ac.Actividades.FechaRealizacion = fecha(act_fecha[id_CA.IndexOf(id)]);
                    ac.Actividades.Responsable = Int32.Parse(act_usuario[id_CA.IndexOf(id)]);
                    BD.SubmitChanges();
                }
            }
        }
        //Funcion para eliminar actividades
        void eliminar_actividades(string id_A)
        {
            if (id_A != null)
            {
                List<string> id_CA = id_A.Split(new Char[] { '&', ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (var id in id_CA)
                {
                    ActividadesControl ca = (from ac in BD.ActividadesControl where ac.Id_CA == Int32.Parse(id) select ac).SingleOrDefault();
                    Actividades act = (from a in BD.Actividades where a.Id_Ac == ca.fk_Ac select a).SingleOrDefault();
                    BD.ActividadesControl.DeleteOnSubmit(ca);
                    BD.SubmitChanges();
                    BD.Actividades.DeleteOnSubmit(act);
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
        //Funcion para actualizar los riesgos
        void actualizar_riesgos(string id_r, string rie)
        {
            if (rie != null)
            { 
                List<string> id_CR = id_r.Split(new string[] { "&,", "&" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                string[] rie_desc = rie.Split(new string[] { "&,", "&" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var id in id_CR)
                {
                    Riesgos riesgo = (from riec in BD.Riesgos where riec.Id_Ri == Int32.Parse(id) select riec).SingleOrDefault();
                    riesgo.Descripcion = rie_desc[id_CR.IndexOf(id)];
                    BD.SubmitChanges();
                }
            }
        }
        //Funcion para eliminar riesgos
        void eliminar_riesgos(string id_R)
        {
            if (id_R != null)
            {
                List<string> id_CR = id_R.Split(new Char[] { '&', ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (var id in id_CR)
                {
                    Riesgos cr = (from riesgo in BD.Riesgos where riesgo.Id_Ri == Int32.Parse(id) select riesgo).SingleOrDefault();
                    BD.Riesgos.DeleteOnSubmit(cr);
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
                string[] id_CS = ser[0].Split(new Char[] { '&', ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] fecha_inicio = ser[1].Split(new Char[] { '&', ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] fecha_termino = ser[2].Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
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
        //Funcion para actualizar los servicios
        void actualizar_servicios(string id_CS, string[] ser)
        {
            if (!ser.Contains(null))
            {
                List<string> id_CoS = id_CS.Split(new Char[] { '&', ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                string[] id_S = ser[0].Split(new Char[] { '&', ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] fecha_inicio = ser[1].Split(new Char[] { '&', ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] fecha_termino = ser[2].Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach(var s in id_CoS)
                {
                    ControlServicio servicios_ = (from sc in BD.ControlServicio where sc.Id_CS == Int32.Parse(s) select sc).SingleOrDefault();
                    servicios_.fk_SA = Int32.Parse(id_S[id_CoS.IndexOf(s)]);
                    servicios_.FechaInicio = fecha(fecha_inicio[id_CoS.IndexOf(s)]);
                    servicios_.FechaFinal = fecha(fecha_termino[id_CoS.IndexOf(s)]);
                    BD.SubmitChanges();
                }
            }
        }
        //Funcion para eliminar servicios
        void eliminar_servicios(string id_S)
        {
            if(id_S != null)
            {
                List<string> id_CS = id_S.Split(new Char[] { '&', ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach(var id in id_CS)
                {
                    ControlServicio cs = (from cos in BD.ControlServicio where cos.Id_CS == Int32.Parse(id) select cos).SingleOrDefault();
                    BD.ControlServicio.DeleteOnSubmit(cs);
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
                        Directory.CreateDirectory(carpetaCC);
                        string path = Path.Combine(Server.MapPath("~/Archivos/CC_" + id_CC), documento.Id_Do + "_Evidencia.pdf");
                        a.SaveAs(path);
                        documento.DocPath = path;
                        BD.SubmitChanges();
                    }

                }
            }
        }
        //Funcioon para eliminar los documentos adjuntos
        void eliminar_adjuntos(string id_a)
        {
            if (id_a != null)
            {
                List<string> id_CA = id_a.Split(new Char[] { '&', ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (var id in id_CA)
                {
                    Documentos doc = (from d in BD.Documentos where d.Id_Do == Int32.Parse(id) select d).SingleOrDefault();
                    string path = doc.DocPath;
                    BD.Documentos.DeleteOnSubmit(doc);
                    BD.SubmitChanges();
                    if (System.IO.File.Exists(path))
                    {
                        try
                        {
                            System.IO.File.Delete(path);
                        }
                        catch (System.IO.IOException e)
                        {
                        }
                    }
                }
            }
        }
        // Revisar las autorizaciones
        void revisarAut(ControlCambio cc)
        {
            List<Notificaciones> notificaciones = (from n in BD.Notificaciones where n.fk_CC == cc.Id_CC && n.Activa == true && n.Tipo == "Autorizar" select n).ToList();
            if(notificaciones.Count > 0) { return; }
            List<Autorizaciones> aut = (from a in BD.Autorizaciones where a.fk_CC == cc.Id_CC && a.Autorizado == false select a).ToList();
            try
            {
                if (aut.Count == 0)
                {
                    // Se envia la notificacion al Super Admin
                    Usuario super = (from ur in BD.UsuarioRol where ur.fk_Rol == 2 && ur.Usuario.Activo == true select ur.Usuario).SingleOrDefault();
                    General.generarNotiicacionAutSA(super, cc, "Ejecutar");
                }
                else
                {
                    Notificacion notificacion = new Notificacion(cc.Id_CC, 0);
                    notificacion.clave_cc = General.generarClave(cc);
                    notificacion.fecha_ejecucion_cc = cc.FechaEjecucion.ToString().Substring(0, 10);
                    // Se envia la notificacion al dueno del control
                    notificacion.motivos = (from a in BD.Autorizaciones where a.fk_CC == cc.Id_CC && a.Autorizado == false select a).ToList();
                    Usuario dueno = cc.Usuario;
                    Notificaciones not = new Notificaciones();
                    not.fk_CC = cc.Id_CC;
                    not.fk_U = dueno.Id_U;
                    not.FechaEnvio = DateTime.Today;
                    not.Contenido = notificacion.generate(8);
                    not.Activa = true;
                    not.Tipo = "NoAutorizado";
                    BD.Notificaciones.InsertOnSubmit(not);
                    BD.SubmitChanges();
                    notificacion.email = true;
                    General.Email(dueno.Email, notificacion.getSubject(8), notificacion.generate(8));
                    // Cambio de estado
                    cc.Estado = cc.Estado == "PausadoE" ? "Creado" : "EnEjecucion";
                }
            }
            catch(Exception e) 
            {
            }
        }
        // GET: ControlCambio/Ver/5
        public ActionResult Ver(int id)
        {
            // Notificaciones para navbar
            List<ControlCambio> ccs = (from n in BD.Notificaciones join cc in BD.ControlCambio on n.fk_CC equals cc.Id_CC where n.fk_U == Sesion && n.Activa == true select cc).ToList();
            ViewBag.Notificaciones_claves = General.generarListaClave(ccs);
            ViewBag.Notificaciones = (from n in BD.Notificaciones where n.fk_U == Sesion && n.Activa select n).ToList();
            // Demas codigo
            ViewBag.Informacion = (from cc in BD.ControlCambio where cc.Id_CC == id select cc).SingleOrDefault();
            ViewBag.Actividades_Prev = (from ac in BD.ActividadesControl join ap in BD.Actividades on ac.fk_Ac equals ap.Id_Ac where ac.fk_CC == id && ap.Tipo == "Previa" select ap).ToList();
            ViewBag.Actividades_CC = (from ac in BD.ActividadesControl join ap in BD.Actividades on ac.fk_Ac equals ap.Id_Ac where ac.fk_CC == id && ap.Tipo == "ControlCambio" select ap).ToList();
            ViewBag.Servicios = (from sc in BD.ControlServicio where sc.fk_CC == id select sc).ToList();
            ViewBag.Riesgos_CC = (from r in BD.Riesgos where r.fk_CC == id && r.Tipo == "ControlCambio" select r).ToList();
            ViewBag.Riesgos_No = (from r in BD.Riesgos where r.fk_CC == id && r.Tipo == "No" select r).ToList();
            var documentos = (from d in BD.Documentos where d.fk_CC == id && d.TipoDoc == "Adjunto" select d);
            List<Documentos> Documentos_imagenes = new List<Documentos>();
            List<Documentos> Documentos_pdf = new List<Documentos>();
            Documentos evidencia = new Documentos();
            foreach(var doc in documentos)
            {
                var nombre_ = doc.DocPath.Split(new char[] { '\\' });
                var nombre = nombre_[nombre_.Length - 1];
                doc.DocPath = nombre;
                if (nombre.Contains(".pdf"))
                {
                    Documentos_pdf.Add(doc);
                    if (doc.TipoDoc == "Evidencia") { evidencia = doc; }
                }
                else
                {
                    Documentos_imagenes.Add(doc);
                }
            }
            ViewBag.Documentos_imagenes = Documentos_imagenes;
            ViewBag.Documentos_pdf = Documentos_pdf;
            ViewBag.Evidencia = evidencia;
            return View();
        }

        // GET: ControlCambio/Crear
        public ActionResult Crear()
        {
            // Notificaciones para navbar
            List<ControlCambio> ccs = (from n in BD.Notificaciones join cc in BD.ControlCambio on n.fk_CC equals cc.Id_CC where n.fk_U == Sesion && n.Activa == true select cc).ToList();
            ViewBag.Notificaciones_claves = General.generarListaClave(ccs);
            ViewBag.Notificaciones = (from n in BD.Notificaciones where n.fk_U == Sesion && n.Activa select n).ToList();
            // Demas codigo
            List<Usuario> usuarios = (from a in BD.Usuario where a.Activo == true select a).ToList();
            List<ServiciosAplicaciones> servicios = (from a in BD.ServiciosAplicaciones where a.Activo == true select a).ToList();
            ViewBag.servicios = servicios;
            ViewBag.usuarios = usuarios;
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
                controlCambio.Conclusion = "";
                controlCambio.Exito = false;
                controlCambio.Creador = Sesion;
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
            // Notificaciones para navbar
            List<ControlCambio> ccs = (from n in BD.Notificaciones join cc in BD.ControlCambio on n.fk_CC equals cc.Id_CC where n.fk_U == Sesion && n.Activa == true select cc).ToList();
            ViewBag.Notificaciones_claves = General.generarListaClave(ccs);
            ViewBag.Notificaciones = (from n in BD.Notificaciones where n.fk_U == Sesion && n.Activa select n).ToList();
            // Codigo general
            ControlCambio model = (from cc in BD.ControlCambio where cc.Id_CC == id select cc).SingleOrDefault();
            List<Usuario> usuarios = (from a in BD.Usuario where a.Activo == true select a).ToList();
            List<ServiciosAplicaciones> servicios = (from a in BD.ServiciosAplicaciones where a.Activo == true select a).ToList();
            ViewBag.servicios = servicios;
            ViewBag.usuarios = usuarios;
            // Codigo para servicios/aplicaciones/actividades/riesgos/adjuntos asociados al control
            ViewBag.Actividades_Prev = (from ac in BD.ActividadesControl join ap in BD.Actividades on ac.fk_Ac equals ap.Id_Ac where ac.fk_CC == id && ap.Tipo == "Previa" select ac).ToList();
            ViewBag.Actividades_CC = (from ac in BD.ActividadesControl join ap in BD.Actividades on ac.fk_Ac equals ap.Id_Ac where ac.fk_CC == id && ap.Tipo == "ControlCambio" select ac).ToList();
            ViewBag.ControlServicios = (from sc in BD.ControlServicio where sc.fk_CC == id select sc).ToList();
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
            return View(model);
        }

        // POST: ControlCambio/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, ControlCambio modelo, HttpPostedFileBase[] adjuntos_, FormCollection collection)
        {
            try
            {
                // Información general
                ControlCambio control = (from cc in BD.ControlCambio where cc.Id_CC == id select cc).SingleOrDefault();
                control.Titulo = modelo.Titulo;
                control.Objetivos = modelo.Objetivos;
                control.Introduccion = modelo.Introduccion;
                control.Tipo = modelo.Tipo;
                control.FechaEjecucion = modelo.FechaEjecucion;
                control.Estado = control.Estado != "Creado" ? "EnEvaluacion" : "Creado";
                BD.SubmitChanges();
                // Se elimina las autorizaciones
                List<Autorizaciones> aut = (from a in BD.Autorizaciones where a.fk_CC == id select a).ToList();
                foreach(var a in aut)
                {
                    BD.Autorizaciones.DeleteOnSubmit(a);
                    BD.SubmitChanges();
                }
                //Llamar anadir actividades nuevas si las hay
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
                anadir_actividades(id, act_prev, act_cc);
                //Llamar actualizar actividades, si editaron alguna
                act_prev[0] = collection["act_prev_desc_editado"];
                act_prev[1] = collection["act_prev_obs_editado"];
                act_prev[2] = collection["act_prev_fecha_editado"];
                act_prev[3] = collection["act_prev_res_editado"];
                act_cc[0] = collection["act_cc_desc_editado"];
                act_cc[1] = collection["act_cc_obs_editado"];
                act_cc[2] = collection["act_cc_fecha_editado"];
                act_cc[3] = collection["act_cc_res_editado"];
                actualizar_actividades(collection["act_prev_editado"], act_prev);
                actualizar_actividades(collection["act_cc_editado"], act_cc);
                //Llamar eliminar actividades, si se elimino alguna
                eliminar_actividades(collection["act_cc_eliminado"]);
                eliminar_actividades(collection["act_prev_eliminado"]);
                //Llamar anadir riesgos nuevos si los hay
                anadir_riesgos(id, collection["riesgos_descripcion"], collection["riesgos_no_descripcion"]);
                //Llamar actualizar riesgos, si editaron alguno
                actualizar_riesgos(collection["rie_cc_id_editado"], collection["rie_cc_desc_editado"]);
                actualizar_riesgos(collection["rie_no_id_editado"], collection["rie_no_desc_editado"]);
                //Llamar eliminar riesgos, si se elimino alguno
                eliminar_riesgos(collection["rie_cc_eliminado"]);
                eliminar_riesgos(collection["rie_no_eliminado"]);
                //Llamar anadir servicios nuevos, si los hay
                string[] servicios_ = new string[3];
                servicios_[0] = collection["servicio_servicios"];
                servicios_[1] = collection["servicio_inicio"];
                servicios_[2] = collection["servicio_temino"];
                servicios(id, servicios_);
                //Llamar actualizar servicios, si editaron alguno
                servicios_[0] = collection["ser_ser_editado"];
                servicios_[1] = collection["ser_inicio_editado"];
                servicios_[2] = collection["ser_final_editado"];
                actualizar_servicios(collection["ser_id_editado"], servicios_);
                //Llamar eliminar servicios, si se elimino alguno
                eliminar_servicios(collection["ser_id_eliminado"]);
                //Llamar a anadir adjuntos nuevos, si los hay
                anadir_adjuntos(id, adjuntos_, "Adjunto");
                //Llamar a eliminar adjuntos, si se elimino alguno
                eliminar_adjuntos(collection["doc_img_eliminado"]);
                eliminar_adjuntos(collection["doc_doc_eliminado"]);
                // Desactivar notificacion de correccion si la hay
                Notificaciones not = (from n in BD.Notificaciones where n.fk_CC == id && n.fk_U == control.Usuario.Id_U && n.Tipo == "Correccion" && n.Activa == true select n).SingleOrDefault();
                // crear notificacion para revision
                General.generarNotificacionRev(control);
                if(not != null)
                {
                    not.Activa = false;
                    BD.SubmitChanges();
                }
                return RedirectToAction("./../Home/Index", new
                {
                    mensaje = "C9"
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
        public ActionResult Cerrar(int id)
        {
            return View();
        }

        // POST: ControlCambio/Cerrar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cerrar(int id, ControlCambio model, HttpPostedFileBase[] evidencia, FormCollection collection)
        {
            try
            {
                ControlCambio controlCambio = (from cc in BD.ControlCambio where cc.Id_CC == id select cc).SingleOrDefault();
                controlCambio.Estado = "PausadoT";
                controlCambio.Conclusion = model.Conclusion;
                if(collection["exito"] != null)
                {
                    controlCambio.Exito = true;
                }
                BD.SubmitChanges();
                //Llamar a adjuntos
                anadir_adjuntos(controlCambio.Id_CC, evidencia, "Evidencia");
                BD.SubmitChanges();
                // generar notificaciones para autorizar el cierre
                General.generarNotificacionesAut(model, 2);
                return RedirectToAction("./../Home/Index", new
                {
                    mensaje = "C9"
                });
            }
            catch
            {
                return RedirectToAction("./../Home/Index", new
                {
                    mensaje = "E1"
                });
            }
        }

        // GET: ControlCambio/Cerrar/5
        public ActionResult Revisar(int id)
        {
            // Notificaciones para navbar
            List<ControlCambio> ccs = (from n in BD.Notificaciones join cc in BD.ControlCambio on n.fk_CC equals cc.Id_CC where n.fk_U == Sesion && n.Activa == true select cc).ToList();
            ViewBag.Notificaciones_claves = General.generarListaClave(ccs);
            ViewBag.Notificaciones = (from n in BD.Notificaciones where n.fk_U == Sesion && n.Activa select n).ToList();
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
            Documentos evidencia = new Documentos();
            foreach (var doc in documentos)
            {
                var nombre_ = doc.DocPath.Split(new char[] { '\\' });
                var nombre = nombre_[nombre_.Length - 1];
                doc.DocPath = nombre;
                if (nombre.Contains(".pdf"))
                {
                    Documentos_pdf.Add(doc);
                    if (doc.TipoDoc == "Evidencia") { evidencia = doc; }
                }
                else
                {
                    Documentos_imagenes.Add(doc);
                }
            }
            ViewBag.Documentos_imagenes = Documentos_imagenes;
            ViewBag.Documentos_pdf = Documentos_pdf;
            ViewBag.Evidencia = evidencia;
            // Mensajes de error
            ViewData["ME1"] = Mensaje.getMError(15);
            ViewData["ME2"] = Mensaje.getMError(16);
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
                var controlcambio = (from cc in BD.ControlCambio where cc.Id_CC == id select cc).SingleOrDefault();
                // Revisar si es aprobación o corrección
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
                    // Enviar notificación de la correccion
                    Notificacion noti = new Notificacion(id, 4);
                    noti.clave_cc = General.generarClave(controlcambio);
                    noti.fecha_ejecucion_cc = controlcambio.FechaEjecucion.ToString().Substring(0, 10);
                    Notificaciones not = new Notificaciones();
                    not.fk_CC = id;
                    not.fk_U = controlcambio.Usuario.Id_U;
                    not.FechaEnvio = DateTime.Now;
                    not.Activa = true;
                    not.Tipo = "Correccion";
                    not.Contenido = noti.generate(9);
                    BD.Notificaciones.InsertOnSubmit(not);
                    BD.SubmitChanges();
                    // enviamos correo
                    noti.email = true;
                    General.Email(controlcambio.Usuario.Email, noti.getSubject(9), noti.generate(9));

                }
                if (collection["aprobar"] != null)
                {
                    controlcambio.Estado = "Aprobado";
                    BD.SubmitChanges();
                    // Enviar notificación de la correccion
                    Notificacion noti = new Notificacion(id, 3);
                    noti.clave_cc = General.generarClave(controlcambio);
                    noti.fecha_ejecucion_cc = controlcambio.FechaEjecucion.ToString().Substring(0, 10);
                    Notificaciones not = new Notificaciones();
                    not.fk_CC = id;
                    not.fk_U = controlcambio.Usuario.Id_U;
                    not.FechaEnvio = DateTime.Now;
                    not.Activa = true;
                    not.Tipo = "Aprobado";
                    not.Contenido = noti.generate(9);
                    BD.Notificaciones.InsertOnSubmit(not);
                    BD.SubmitChanges();
                    // enviamos correo
                    noti.email = true;
                    General.Email(controlcambio.Usuario.Email, noti.getSubject(9), noti.generate(9));
                }
                // se desactiva notificacion de revision
                Notificaciones notificaciones = (from n in BD.Notificaciones where n.fk_CC == id && n.Tipo == "Revision" && n.Activa == true select n).SingleOrDefault();
                notificaciones.Activa = false;
                BD.SubmitChanges();
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
        public ActionResult Corregir(int id)
        {
            // Notificaciones para navbar
            List<ControlCambio> ccs = (from n in BD.Notificaciones join cc in BD.ControlCambio on n.fk_CC equals cc.Id_CC where n.fk_U == Sesion && n.Activa == true select cc).ToList();
            ViewBag.Notificaciones_claves = General.generarListaClave(ccs);
            ViewBag.Notificaciones = (from n in BD.Notificaciones where n.fk_U == Sesion && n.Activa select n).ToList();
            // Codigo general
            ControlCambio model = (from cc in BD.ControlCambio where cc.Id_CC == id select cc).SingleOrDefault();
            List<Usuario> usuarios = (from a in BD.Usuario where a.Activo == true select a).ToList();
            List<ServiciosAplicaciones> servicios = (from a in BD.ServiciosAplicaciones where a.Activo == true select a).ToList();
            ViewBag.servicios = servicios;
            ViewBag.usuarios = usuarios;
            // Codigo para servicios/aplicaciones/actividades/riesgos/adjuntos asociados al control
            ViewBag.Actividades_Prev = (from ac in BD.ActividadesControl join ap in BD.Actividades on ac.fk_Ac equals ap.Id_Ac where ac.fk_CC == id && ap.Tipo == "Previa" select ac).ToList();
            ViewBag.Actividades_CC = (from ac in BD.ActividadesControl join ap in BD.Actividades on ac.fk_Ac equals ap.Id_Ac where ac.fk_CC == id && ap.Tipo == "ControlCambio" select ac).ToList();
            ViewBag.ControlServicios = (from sc in BD.ControlServicio where sc.fk_CC == id select sc).ToList();
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
            return View(model);
        }
        public ActionResult Notas(int id)
        {
            Revisiones revision = (from r in BD.Revisiones where r.fk_CC == id select r).SingleOrDefault();
            return View(revision);
        }
        // GET: ControlCambio/Cerrar/5
        public ActionResult Autorizar(int id, string tipo)
        {
            // Notificaciones para navbar
            List<ControlCambio> ccs = (from n in BD.Notificaciones join cc in BD.ControlCambio on n.fk_CC equals cc.Id_CC where n.fk_U == Sesion && n.Activa == true select cc).ToList();
            ViewBag.Notificaciones_claves = General.generarListaClave(ccs);
            ViewBag.Notificaciones = (from n in BD.Notificaciones where n.fk_U == Sesion && n.Activa select n).ToList();
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
            // Se revisa que no haya autrizado antes
            string error = "";
            List<Notificaciones> no = (from n in BD.Notificaciones where n.fk_U == Sesion && n.fk_CC == id && n.Activa == true && n.Tipo == "Autorizar" select n).ToList();
            if (no.Count == 0)
            {
                error = Mensaje.getMError(18);
            }
            // Variables para los PASOS a seguir
            ViewData["Paso"] = "1";
            ViewData["ME"] = Mensaje.getMError(0);
            ViewData["MA"] = Mensaje.getMAdvertencia(0);
            ViewData["ME2"] = error;
            return View();
        }

        // POST: ControlCambio/Cerrar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Autorizar(int id, string tipo, FormCollection collection)
        {
            try
            {
                // Notificaciones para navbar
                List<ControlCambio> ccs = (from n in BD.Notificaciones join cc in BD.ControlCambio on n.fk_CC equals cc.Id_CC where n.fk_U == Sesion && n.Activa == true select cc).ToList();
                ViewBag.Notificaciones_claves = General.generarListaClave(ccs);
                ViewBag.Notificaciones = (from n in BD.Notificaciones where n.fk_U == Sesion && n.Activa select n).ToList();
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
                // Se revisa que no haya autrizado antes
                List<Notificaciones> no = (from n in BD.Notificaciones where n.fk_U == Sesion && n.fk_CC == id && n.Activa == true && n.Tipo == "Autorizar" select n).ToList();
                if (no.Count == 0)
                {
                    ViewData["MA"] = Mensaje.getMAdvertencia(0);
                    ViewData["ME2"] = Mensaje.getMError(18);
                    return View();
                }
                // Codigo para los PASOS a seguir
                if (collection["Paso1"] != null || collection["PasoE"] != null)
                {
                    bool check = collection["autorizacion"] != null;
                    //Enviar Email
                    try
                    {
                        string to = (from u in BD.Usuario where u.Id_U == Sesion select u.Email).SingleOrDefault();
                        ControlCambio cc = (from control in BD.ControlCambio where control.Id_CC == id select control).SingleOrDefault();
                        Notificacion notificacion = new Notificacion("si", General.generarClave(cc));
                        notificacion.email = true;
                        if (General.Email(to,notificacion.getSubject(0),notificacion.generate(6)) != 0)
                        {
                            return RedirectToAction("./../Home/Index", new
                            {
                                mensaje = "E7"
                            });
                        }
                    } 
                    catch(Exception e)
                    {
                        return RedirectToAction("./../Home/Index", new
                        {
                            mensaje = "E1"
                        });
                    }
                    ViewData["check"] = check;
                    ViewData["motivo"] = collection["motivo"] != null ? collection["motivo"] : "";
                    ViewData["MA"] = Mensaje.getMAdvertencia(3);
                    ViewData["ME"] = Mensaje.getMError(0);
                    ViewData["ME2"] = "";
                    ViewData["Paso"] = "2";
                    return View();
                }
                if (collection["Paso2"] != null)
                {
                    Boolean check = collection["autorizacion"] != null;
                    if (collection["codigo"] == "si")
                    {
                        try
                        {
                            ControlCambio cc = (from control in BD.ControlCambio where control.Id_CC == id select control).SingleOrDefault();
                            Autorizaciones aut = new Autorizaciones();
                            if(cc.Estado == "PausadoE")
                            {
                                aut = (from a in BD.Autorizaciones where a.fk_CC == id && a.fk_U == Sesion && a.Tipo == "Ejecutar" select a).SingleOrDefault();
                            }
                            else
                            {
                                aut = (from a in BD.Autorizaciones where a.fk_CC == id && a.fk_U == Sesion && a.Tipo == "Termino" select a).SingleOrDefault();  
                            }
                            aut.Autorizado = collection["autorizacion"] != null;
                            aut.Motivo = collection["motivo"];
                            aut.Fecha = DateTime.Now;
                            BD.SubmitChanges();
                            // Si autoriza el S Admin
                            if(aut.Autorizado)
                            {
                                List<UsuarioRol> ur = aut.Usuario.UsuarioRol.ToList();
                                foreach (var rol in ur)
                                {
                                    if (rol.fk_Rol == 2)
                                    {
                                        // Cambio de estado
                                        if (cc.Estado == "PausadoT" || cc.Estado == "PausadoE")
                                        {
                                            cc.Estado = cc.Estado == "PausadoE" ? "Autorizado" : "Terminado";
                                        }
                                        // Desactivar notificacion
                                        Notificaciones not = (from n in BD.Notificaciones where n.fk_CC == id && n.fk_U == Sesion && n.Activa == true && (n.Tipo == "Autorizar" || n.Tipo == "Terminado") select n).SingleOrDefault();
                                        not.Activa = false;
                                        BD.SubmitChanges();
                                        return RedirectToAction("./../Home/Index", new
                                        {
                                            mensaje = "C11"
                                        });
                                    }
                                }
                            }
                            // Desactivar notificacion
                            Notificaciones noti = (from n in BD.Notificaciones where n.fk_CC == id && n.fk_U == Sesion && n.Activa == true && (n.Tipo == "Autorizar" || n.Tipo == "Terminado") select n).SingleOrDefault();
                            noti.Activa = false;
                            BD.SubmitChanges();
                            // Funcion para saber si ya se cumplieron las autorizaciones
                            revisarAut(aut.ControlCambio);
                        }
                         catch (Exception e)
                        {
                            return RedirectToAction("./../Home/Index", new
                            {
                                mensaje = "E1"
                            });
                        }
                        return RedirectToAction("./../Home/Index", new
                        {
                            mensaje = "C11"
                        });
                    }
                    else
                    {
                        ViewData["check"] = check;
                        ViewData["motivo"] = collection["motivo"] != null ? collection["motivo"] : "";
                        ViewData["MA"] = Mensaje.getMAdvertencia(0);
                        ViewData["ME"] = Mensaje.getMError(17);
                        ViewData["ME2"] = "";
                        ViewData["Paso"] = "E";
                        return View();
                    }
                }
                return RedirectToAction("./../Home/Index", new
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
        public ActionResult Notificar(int id)
        {
            Notificaciones model = new Notificaciones();
            string error = "";
            try
            {
                Notificaciones consulta = (from n in BD.Notificaciones where n.Id_No == id select n).SingleOrDefault();
                ControlCambio cc = (from control in BD.ControlCambio where control.Id_CC == consulta.fk_CC select control).SingleOrDefault();
                model = consulta;
                List<Autorizaciones> aut = new List<Autorizaciones>();
                aut = (from m in BD.Autorizaciones where m.fk_CC == cc.Id_CC && m.Autorizado == false select m).ToList();
                ViewBag.Contenido = consulta.Contenido.Split(new string[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
                ViewBag.Motivos = aut;
                ViewData["Clave"] = General.generarClave(cc);
            }
            catch(Exception e)
            {
                error = Mensaje.getMError(1);
            }
            ViewData["ME"] = error;
            return View(model);
        }
        public ActionResult Calendario()
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
                List<ControlCambio> ccs = (from cc in BD.ControlCambio where cc.FechaEjecucion == dia_ && cc.Creador == Sesion && cc.Estado == "Autorizado" select cc).ToList();
                if (ccs.Count > 0)
                {
                    claves = General.generarListaClave(ccs);
                    dia.setControlCambio(ccs, claves);
                }
                //Tomar los controles de cambio donde es responsable de actividades
                List<ControlCambio> actividades = (from ac in BD.ActividadesControl join a in BD.Actividades on ac.fk_Ac equals a.Id_Ac join cc in BD.ControlCambio on ac.fk_CC equals cc.Id_CC where a.FechaRealizacion == dia_ && a.Responsable == Sesion && cc.Estado == "Autorizado" select cc).ToList();
                if (actividades.Count > 0)
                {
                    claves = General.generarListaClave(actividades);
                    dia.setActividad(actividades, claves);
                }
                //Tomar los controles donde es afectado algún servicio del usuario
                List<ControlCambio> servapp = (from sc in BD.ControlServicio join cc in BD.ControlCambio on sc.fk_CC equals cc.Id_CC join sa in BD.ServiciosAplicaciones on sc.fk_SA equals sa.Id_SA where (sc.FechaInicio <= dia_ && sc.FechaFinal >= dia_) && sa.Dueno == Sesion && cc.Estado == "Autorizado" select cc).ToList();
                //Tomar los servicios para el nombre
                List<ServiciosAplicaciones> servapp_nombres = (from sc in BD.ControlServicio join cc in BD.ControlCambio on sc.fk_CC equals cc.Id_CC join sa in BD.ServiciosAplicaciones on sc.fk_SA equals sa.Id_SA where (sc.FechaInicio <= dia_ && sc.FechaFinal >= dia_) && sa.Dueno == Sesion && cc.Estado == "Autorizado" select sa).ToList();
                if (servapp.Count > 0)
                {
                    claves = General.generarListaClave(servapp);
                    dia.setServApp(servapp, claves, servapp_nombres);
                }
                dias.Add(dia);
            }
            ViewData["inicio"] = Convert.ToInt32(inicio.DayOfWeek); 
            ViewData["mes"] = meses[hoy.Month];
            ViewBag.Dias = dias;
            return View();
        }
        public ActionResult Autorizaciones(int id)
        {
            ControlCambio control = (from cc in BD.ControlCambio where cc.Id_CC == id select cc).SingleOrDefault();
            List<Autorizaciones> aut = new List<Autorizaciones>();
            if(control.Estado == "PausadoE")
            {
                aut = (from a in BD.Autorizaciones where a.fk_CC == id && a.Tipo == "Autorizado" select a).ToList();
            }
            else
            {
                aut = (from a in BD.Autorizaciones where a.fk_CC == id && a.Tipo == "Termino" select a).ToList();
            }
            ViewBag.Notificaciones = (from n in BD.Notificaciones where n.fk_CC == id && n.Activa == true select n).ToList();
            ViewBag.Autorizaciones = aut;
            return View();
        }
    }
}