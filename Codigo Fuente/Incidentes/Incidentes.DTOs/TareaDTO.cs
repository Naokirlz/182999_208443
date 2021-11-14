using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.DTOs
{
    public class TareaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Costo { get; set; }
        public int Duracion { get; set; }
        public int ProyectoId { get; set; }
        public TareaDTO() { }
    }
}
