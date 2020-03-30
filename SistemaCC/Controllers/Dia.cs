using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaCC.Models;
namespace SistemaCC.Controllers
{
    public class Dia
    {
        public List<Evento> ControlCambio = new List<Evento>();
        public List<Evento> Actividad = new List<Evento>();
        public List<Evento> ServApp = new List<Evento>();
        public void setControlCambio(List<ControlCambio> ccs, List<string> claves)
        {
            foreach(var cc in ccs)
            {
                Evento e = new Evento();
                e.id = cc.Id_CC;
                e.clave = claves[ccs.IndexOf(cc)];
                ControlCambio.Add(e);
            }
        }
        public void setActividad(List<ControlCambio> ccs, List<string> claves)
        {
            foreach (var cc in ccs)
            {
                Evento e = new Evento();
                e.id = cc.Id_CC;
                e.clave = claves[ccs.IndexOf(cc)];
                ControlCambio.Add(e);
            }
        }
        public void setServApp(List<ControlCambio> ccs, List<string> claves, List<ServiciosAplicaciones> nombres)
        {
            foreach (var cc in ccs)
            {
                Evento e = new Evento();
                e.id = cc.Id_CC;
                e.clave = claves[ccs.IndexOf(cc)];
                e.acronimo = nombres[ccs.IndexOf(cc)].Acronimo;
                ControlCambio.Add(e);
            }
        }
    }
}