using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.Dominio
{
    public class Incidente
    {
        public string Nombre { get; set; }
        public string NombreProyecto { get; set; }
        public string Descripcion { get; set; }
        public int Version { get; set; }
        public Estado EstadoIncidente { get; set; }
        public int DesarrolladorId { get; set; }

        public Incidente() { }

        public enum Estado
        {
            Activo,
            Resuelto
        }
    }
}
