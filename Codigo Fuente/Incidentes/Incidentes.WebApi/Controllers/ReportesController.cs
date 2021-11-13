using Incidentes.Dominio;
using Incidentes.DTOs;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.DTOs;
using Incidentes.WebApi.Filters;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Incidentes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly ILogicaProyecto _logicaP;
        private readonly ILogicaUsuario _logicaU;

        public ReportesController(ILogicaProyecto logica, ILogicaUsuario logicaU)
        {
            _logicaP = logica;
            _logicaU = logicaU;
        }

        [HttpGet("/proyectos")]
        [FilterAutorizacion("Administrador")]
        [TrapExcepciones]
        public IActionResult GetProyectos()
        {
            IEnumerable<Proyecto> proyectos = _logicaP.ObtenerTodos();
            List<ProyectoParaReporteDTO> result = new List<ProyectoParaReporteDTO>();
            foreach(Proyecto p in proyectos)
            {
                ProyectoParaReporteDTO nuevo = new ProyectoParaReporteDTO() { 
                    Id = p.Id,
                    Nombre = p.Nombre,
                    CantidadDeIncidentes = p.Incidentes.Count()
                };
                result.Add(nuevo);
            }

            return Ok(result);
        }

        [HttpGet("{id}/incidentes")]
        [FilterAutorizacion("Administrador")]
        [TrapExcepciones]
        public IActionResult GetDesarrollador(string id)
        {
            Usuario usuario = _logicaU.Obtener(Int32.Parse(id));
            int cantidad = _logicaU.CantidadDeIncidentesResueltosPorUnDesarrollador(usuario.Id);
            UsuarioParaReporteDTO user = new UsuarioParaReporteDTO() { 
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Id = usuario.Id,
                IncidentesResueltos = cantidad,
                NombreUsuario = usuario.NombreUsuario,
                RolUsuario = usuario.RolUsuario
            };
            return Ok(user);
        }
    }
}
