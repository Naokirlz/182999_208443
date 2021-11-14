using Incidentes.DTOs;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
            IEnumerable<ProyectoDTO> proyectos = _logicaP.ObtenerTodos();
            return Ok(proyectos);
        }

        [HttpGet("{id}/incidentes")]
        [FilterAutorizacion("Administrador")]
        [TrapExcepciones]
        public IActionResult GetDesarrollador(string id)
        {
            UsuarioDTO usuario = _logicaU.Obtener(Int32.Parse(id));
            int cantidad = _logicaU.CantidadDeIncidentesResueltosPorUnDesarrollador(usuario.Id);
            usuario.IncidentesResueltos = cantidad;
            return Ok(usuario);
        }
    }
}
