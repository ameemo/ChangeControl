using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaCC.Controllers
{
    public class Evento
    {
        public int id;
        public string clave, acronimo;
        public Evento()
        {
            id = 0;
            clave = "";
            acronimo = "";
        }
    }
}