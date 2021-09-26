using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.Dominio
{
    public class Proyecto
    {
        public string Nombre { get; set; }
        public List<Incidente> Incidentes{ get; set; }

        public Proyecto() { }

    }
}
