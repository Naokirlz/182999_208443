using Incidentes.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Incidentes.WebApi.DTOs
{
    public class ProyectoParaReporteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int CantidadDeIncidentes { get; set; }

        public ProyectoParaReporteDTO()
        {
        }
    }
}
