using Incidentes.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.DTOs
{
    public class ProyectosDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Duracion { get; set; }
        public int Costo { get; set; }
        public List<Incidente> Incidentes { get; set; }
        public List<Tarea> Tareas { get; set; }
        public List<UsuarioDTO> Asignados { get; set; }

        public ProyectosDTO()
        {
            Incidentes = new List<Incidente>();
            Tareas = new List<Tarea>();
            Asignados = new List<UsuarioDTO>();
        }
    }
}
