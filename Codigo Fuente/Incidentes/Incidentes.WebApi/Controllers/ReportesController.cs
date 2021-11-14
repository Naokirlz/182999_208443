using Incidentes.Dominio;
using Incidentes.DTOs;
using Incidentes.LogicaInterfaz;
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

        [HttpGet("incidentes/proyectos")]
        [FilterAutorizacion("Administrador")]
        [TrapExcepciones]
        public IActionResult GetProyectos()
        {
            IEnumerable<Proyecto> proyectos = _logicaP.ObtenerTodos();
            List<ProyectoDTO> result = new List<ProyectoDTO>();
            foreach(Proyecto p in proyectos)
            {
                ProyectoDTO nuevo = new ProyectoDTO() { 
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
            UsuarioDTO user = new UsuarioDTO(usuario);
            return Ok(user);
        }
    }
}
