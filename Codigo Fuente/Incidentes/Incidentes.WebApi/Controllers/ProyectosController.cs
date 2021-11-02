using Incidentes.Dominio;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.DTOs;
using Incidentes.WebApi.Filters;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Incidentes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [TrapExcepciones]
    [ApiController]
    public class ProyectosController : ControllerBase
    {
        private readonly ILogicaProyecto _logicaP;

        public ProyectosController(ILogicaProyecto logica)
        {
            _logicaP = logica;
        }

        [HttpGet]
        [FilterAutorizacion("Administrador")]
        public IActionResult Get()
        {
            List<Proyecto> result = _logicaP.ObtenerTodos().ToList();
            List<ProyectosDTO> proyectos = new List<ProyectosDTO>();
            foreach(Proyecto p in result)
            {
                ProyectosDTO pro = new ProyectosDTO()
                {
                    Id = p.Id,
                    Incidentes = p.Incidentes,
                    Tareas = p.Tareas,
                    Nombre = p.Nombre
                };
                foreach(Usuario u in p.Asignados)
                {
                    UsuarioParaReporteDTO usu = new UsuarioParaReporteDTO()
                    {
                        Apellido = u.Apellido,
                        Email = u.Email,
                        Id = u.Id,
                        Nombre = u.Nombre,
                        ValorHora = u.ValorHora,
                        NombreUsuario = u.NombreUsuario,
                        RolUsuario = u.RolUsuario
                    };
                    pro.Asignados.Add(usu);
                }
                proyectos.Add(pro);
            }
            return Ok(proyectos);
        }

        [HttpGet("{id}")]
        [FilterAutorizacion("Administrador")]
        public IActionResult Get(int id)
        {
            var p = _logicaP.Obtener(id);
            ProyectosDTO pro = new ProyectosDTO()
            {
                Id = p.Id,
                Incidentes = p.Incidentes,
                Tareas = p.Tareas,
                Nombre = p.Nombre
            };
            foreach (Usuario u in p.Asignados)
            {
                UsuarioParaReporteDTO usu = new UsuarioParaReporteDTO()
                {
                    Apellido = u.Apellido,
                    Email = u.Email,
                    Id = u.Id,
                    Nombre = u.Nombre,
                    ValorHora = u.ValorHora,
                    NombreUsuario = u.NombreUsuario,
                    RolUsuario = u.RolUsuario
                };
                pro.Asignados.Add(usu);
            }
            int duracion = 0;
            int costo = 0;
            foreach(Tarea t in p.Tareas)
            {
                duracion += t.Duracion;
                costo += t.Duracion * t.Costo;
            }
            foreach (Incidente i in p.Incidentes)
            {
                duracion += i.Duracion;
                if(i.EstadoIncidente == Incidente.Estado.Resuelto)
                {
                    Usuario u = p.Asignados.Find(d => d.Id == i.DesarrolladorId);
                    costo += i.Duracion * u.ValorHora;
                }
            }
            pro.Duracion = duracion;
            pro.Costo = costo;
            return Ok(pro);
        }

        [HttpPost]
        [FilterAutorizacion("Administrador")]
        public IActionResult Post([FromBody] Proyecto proyecto)
        {
            _logicaP.Alta(proyecto);
            return Ok(proyecto);
        }

        [HttpDelete]
        [FilterAutorizacion("Administrador")]
        public IActionResult Delete([FromBody] Proyecto proyecto)
        {
            _logicaP.Baja(proyecto.Id);
            return StatusCode(204, "Eliminado Satisfactoriamente.");
        }
        
        [HttpPut]
        [FilterAutorizacion("Administrador")]
        public IActionResult Put([FromBody] Proyecto proyecto)
        {
            _logicaP.Modificar(proyecto.Id, proyecto);
            ProyectosDTO pro = new ProyectosDTO()
            {
                Id = proyecto.Id,
                Incidentes = proyecto.Incidentes,
                Tareas = proyecto.Tareas,
                Nombre = proyecto.Nombre
            };
            foreach (Usuario u in proyecto.Asignados)
            {
                UsuarioParaReporteDTO usu = new UsuarioParaReporteDTO()
                {
                    Apellido = u.Apellido,
                    Email = u.Email,
                    Id = u.Id,
                    Nombre = u.Nombre,
                    ValorHora = u.ValorHora,
                    NombreUsuario = u.NombreUsuario,
                    RolUsuario = u.RolUsuario
                };
                pro.Asignados.Add(usu);
            }

            return Ok(pro);
        }
    }
}