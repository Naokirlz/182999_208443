using Incidentes.Dominio;
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
        public double Costo { get; set; }
        public int Duracion { get; set; }
        public int ProyectoId { get; set; }
        public TareaDTO() { }
        public TareaDTO(Tarea tarea)
        {
            Id = tarea.Id;
            Nombre = tarea.Nombre;
            Costo = tarea.Costo;
            Duracion = tarea.Duracion;
            ProyectoId = tarea.ProyectoId;
        }
        public Tarea convertirDTO_Dominio()
        {
            Tarea retorno = new Tarea()
            {
                Id = this.Id,
                Nombre = this.Nombre,
                Costo = this.Costo,
                Duracion = this.Duracion,
                ProyectoId = this.ProyectoId
            };
            return retorno;
        }
    }
}
