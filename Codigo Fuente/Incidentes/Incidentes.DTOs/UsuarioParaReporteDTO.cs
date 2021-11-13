using Incidentes.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.DTOs
{
    public class UsuarioParaReporteDTO
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public Usuario.Rol RolUsuario { get; set; }
        public int IncidentesResueltos { get; set; }
        public int ValorHora { get; set; }
    }
}
