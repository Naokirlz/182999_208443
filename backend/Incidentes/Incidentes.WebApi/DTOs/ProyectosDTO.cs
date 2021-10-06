using Incidentes.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Incidentes.WebApi.DTOs
{
    public class ProyectosDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Incidente> Incidentes { get; set; }
        public List<UsuarioParaReporteDTO> Asignados { get; set; }

        public ProyectosDTO()
        {
            Incidentes = new List<Incidente>();
            Asignados = new List<UsuarioParaReporteDTO>();
        }
    }
}
