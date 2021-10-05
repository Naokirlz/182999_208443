using AutoMapper;
using Incidentes.Dominio;
using Incidentes.Logica.Interfaz;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.DTOs;
using Incidentes.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Incidentes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [TrapExcepciones]
    [ApiController]
    public class AsociacionesController : ControllerBase
    {
        private readonly ILogicaProyecto _logicaP;
        private readonly ILogicaIncidente _logicaI;

        public AsociacionesController(ILogicaProyecto logica, ILogicaIncidente logicaI)
        {
            _logicaP = logica;
            _logicaI = logicaI;
        }

        [HttpGet]
        [FilterAutorizacion("Desarrollador", "Tester")]
        [Route("{id}/proyectos")]
        public IActionResult GetProyectos([FromRoute] string idUsuario)
        {
            IQueryable<Proyecto> result = _logicaP.ListaDeProyectosALosQuePertenece(Int32.Parse(idUsuario));
            return Ok(result);
        }

        [HttpGet]
        [FilterAutorizacion("Desarrollador", "Tester")]
        [Route("{id}/proyecto")]
        public IActionResult GetProyecto([FromRoute] string idUsuario, [FromQuery] int idProyecto)
        {
            Proyecto result = _logicaP.ObtenerParaUsuario(Int32.Parse(idUsuario), idProyecto);
            return Ok(result);
        }

        [HttpGet]
        [FilterAutorizacion("Desarrollador", "Tester")]
        [Route("{id}/incidente")]
        public IActionResult GetIncidente([FromRoute] string idUsuario, [FromQuery] int idIncidente)
        {
            Incidente result = _logicaI.ObtenerParaUsuario(Int32.Parse(idUsuario), idIncidente);
            return Ok(result);
        }

        [HttpGet]
        [FilterAutorizacion("Desarrollador", "Tester")]
        [Route("{id}/incidentes")]
        public IActionResult GetIncidentes([FromRoute] string idUsuario, [FromQuery] string nombreProyecto = null, Incidente incidente = null)
        {
            List<Incidente> result = _logicaI.ListaDeIncidentesDeLosProyectosALosQuePertenece(Int32.Parse(idUsuario), nombreProyecto, incidente);
            return Ok(result);
        }

        [HttpPost]
        [FilterAutorizacion("Administrador")]
        public IActionResult Post([FromBody] AsignacionesDTO asignaciones)
        {
            _logicaP.AgregarDesarrolladorAProyecto(asignaciones.UsuarioId, asignaciones.ProyectoId);
            return Ok();
        }
    }
}
