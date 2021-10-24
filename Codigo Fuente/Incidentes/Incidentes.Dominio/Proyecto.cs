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
        public List<Tarea> Tareas{ get; set; }
        public virtual List<Usuario> Asignados{ get; set; }

        public Proyecto() {
            Incidentes = new List<Incidente>();
            Tareas = new List<Tarea>();
            Asignados = new List<Usuario>();
        }

    }
}
