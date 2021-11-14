using Incidentes.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.DTOs
{
    public class IncidenteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int ProyectoId { get; set; }
        public string Descripcion { get; set; }
        public string Version { get; set; }
        public Estado EstadoIncidente { get; set; }
        public int DesarrolladorId { get; set; }
        public int Duracion { get; set; }

        public IncidenteDTO() { }
        public IncidenteDTO(Incidente i)
        {
            Id = i.Id;
            Nombre = i.Nombre;
            ProyectoId = i.ProyectoId;
            Descripcion = i.Descripcion;
            Version = i.Version;
            DesarrolladorId = i.DesarrolladorId;
            Duracion = i.Duracion;
            if(i.EstadoIncidente == Incidente.Estado.Activo)
            {
                EstadoIncidente = Estado.Activo;
            }else if (i.EstadoIncidente == Incidente.Estado.Resuelto)
            {
                EstadoIncidente = Estado.Resuelto;
            }
            else
            {
                EstadoIncidente = Estado.Indiferente;
            }
        }

        public Incidente convertirDTO_Dominio()
        {
            Incidente i = new Incidente()
            {
                DesarrolladorId = this.DesarrolladorId,
                Duracion = this.Duracion,
                Descripcion = this.Descripcion,
                Id = this.Id,
                Nombre = this.Nombre,
                ProyectoId = this.ProyectoId,
                Version = this.Version
            };
            if (EstadoIncidente == Estado.Activo)
            {
                i.EstadoIncidente = Incidente.Estado.Activo;
            }
            else if (EstadoIncidente == Estado.Resuelto)
            {
                i.EstadoIncidente = Incidente.Estado.Resuelto;
            }
            else
            {
                i.EstadoIncidente = Incidente.Estado.Indiferente;
            }
            return i;
        }

        public enum Estado
        {
            Indiferente,
            Activo,
            Resuelto
        }
    }
}
