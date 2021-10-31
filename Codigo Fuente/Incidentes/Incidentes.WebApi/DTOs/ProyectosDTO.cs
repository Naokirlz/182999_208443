using Incidentes.Dominio;
using System.Collections.Generic;

namespace Incidentes.WebApi.DTOs
{
    public class ProyectosDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Duracion { get; set; }
        public int Costo { get; set; }
        public List<Incidente> Incidentes { get; set; }
        public List<Tarea> Tareas { get; set; }
        public List<UsuarioParaReporteDTO> Asignados { get; set; }

        public ProyectosDTO()
        {
            Incidentes = new List<Incidente>();
            Tareas = new List<Tarea>();
            Asignados = new List<UsuarioParaReporteDTO>();
        }
    }
}
