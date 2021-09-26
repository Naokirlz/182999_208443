using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.Dominio
{
    public class Proyecto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Incidente> Incidentes{ get; set; }
        public List<Desarrollador> Desarrolladores{ get; set; }
        public List<Tester> Testers { get; set; }

        public Proyecto() {

            Incidentes = new List<Incidente>();
            Desarrolladores = new List<Desarrollador>();
            Testers = new List<Tester>();

        }

    }
}
