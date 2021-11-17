using Incidentes.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.DTOs
{
    public class ProyectoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Duracion { get; set; }
        public double Costo { get; set; }
        public int CantidadDeIncidentes { get; set; }
        public List<IncidenteDTO> Incidentes { get; set; }
        public List<TareaDTO> Tareas { get; set; }
        public List<UsuarioDTO> Asignados { get; set; }

        public ProyectoDTO()
        {
            Incidentes = new List<IncidenteDTO>();
            Tareas = new List<TareaDTO>();
            Asignados = new List<UsuarioDTO>();
            CantidadDeIncidentes = Incidentes.Count();
        }
        public ProyectoDTO(Proyecto p)
        {
            Incidentes = new List<IncidenteDTO>();
            Tareas = new List<TareaDTO>();
            Asignados = new List<UsuarioDTO>();
            Id = p.Id;
            Nombre = p.Nombre;
            int duracion = 0;
            double costo = 0;
            foreach (Incidente i in p.Incidentes)
            {
                IncidenteDTO inc = new IncidenteDTO(i);
                inc.NombreProyecto = p.Nombre;
                Incidentes.Add(inc);
                duracion += i.Duracion;
                if (i.EstadoIncidente == Incidente.Estado.Resuelto)
                {
                    Usuario u = p.Asignados.Find(d => d.Id == i.DesarrolladorId);
                    if (u != null)
                    {
                        costo += i.Duracion * u.ValorHora;
                        inc.DesarrolladorNombre = u.Nombre + " " + u.Apellido;
                    }
                }
            }
            foreach (Tarea t in p.Tareas)
            {
                Tareas.Add(new TareaDTO(t));
                duracion += t.Duracion;
                costo += t.Duracion * t.Costo;
            }
            foreach (Usuario u in p.Asignados)
            {
                Asignados.Add(new UsuarioDTO(u));
            }
            CantidadDeIncidentes = p.Incidentes.Count();
            Duracion = duracion;
            Costo = costo;
        }
        public Proyecto convertirDTO_Dominio()
        {
            Proyecto p = new Proyecto(){ 
                Nombre = this.Nombre,
                Id = this.Id,
            };
            foreach (IncidenteDTO i in Incidentes)
            {
                p.Incidentes.Add(i.convertirDTO_Dominio());
            }
            foreach (TareaDTO t in Tareas)
            {
                p.Tareas.Add(t.convertirDTO_Dominio());
            }
            foreach (UsuarioDTO u in Asignados)
            {
                p.Asignados.Add(u.convertirDTO_Dominio());
            }
            return p;
        }
    }
}
