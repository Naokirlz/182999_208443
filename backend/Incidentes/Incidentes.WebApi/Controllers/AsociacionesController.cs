﻿using AutoMapper;
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
    [ApiController]
    public class AsociacionesController : ControllerBase
    {
        private const string error_de_servidor = "Internal Server Error";
        private readonly IMapper _mapper;
        private readonly ILogicaProyecto _logicaP;
        private readonly ILogicaIncidente _logicaI;

        public AsociacionesController(ILogicaProyecto logica, ILogicaIncidente logicaI, IMapper mapper)
        {
            _logicaP = logica;
            _logicaI = logicaI;
            _mapper = mapper;
        }

        [HttpGet]
        //[Autorizacion("Administrador")]
        [Route("{id}/proyectos")]
        public IActionResult GetProyectos([FromRoute] string idUsuario)
        {
            try
            {
                IQueryable<Proyecto> result = _logicaP.ListaDeProyectosALosQuePertenece(Int32.Parse(idUsuario));
                return Ok(result);
                // var returnResult = _mapper.Map<IEnumerable<ProyectoDTOWithCouresesForGet>>(result);
            }
            catch (Exception ex)
            {
                //Log de la excepcion
                return StatusCode(500, "");
            }
        }

        [HttpGet]
        //[Autorizacion("Administrador")]
        [Route("{id}/proyecto")]
        public IActionResult GetProyecto([FromRoute] string idUsuario, [FromQuery] int idProyecto)
        {
            try
            {
                Proyecto result = _logicaP.ObtenerParaUsuario(Int32.Parse(idUsuario), idProyecto);
                return Ok(result);
                // var returnResult = _mapper.Map<IEnumerable<ProyectoDTOWithCouresesForGet>>(result);
            }
            catch (Exception ex)
            {
                //Log de la excepcion
                return StatusCode(500, "");
            }
        }

        [HttpGet]
        //[Autorizacion("Administrador")]
        [Route("{id}/incidente")]
        public IActionResult GetIncidente([FromRoute] string idUsuario, [FromQuery] int idIncidente)
        {
            try
            {
                Incidente result = _logicaI.ObtenerParaUsuario(Int32.Parse(idUsuario), idIncidente);
                return Ok(result);
                // var returnResult = _mapper.Map<IEnumerable<ProyectoDTOWithCouresesForGet>>(result);
            }
            catch (Exception ex)
            {
                //Log de la excepcion
                return StatusCode(500, "");
            }
        }

        [HttpGet]
        //[Autorizacion("Administrador")]
        [Route("{id}/incidentes")]
        public IActionResult GetIncidentes([FromRoute] string idUsuario, [FromQuery] string nombreProyecto = null, Incidente incidente = null)
        {
            try
            {
                List<Incidente> result = _logicaI.ListaDeIncidentesDeLosProyectosALosQuePertenece(Int32.Parse(idUsuario), nombreProyecto, incidente);
                return Ok(result);
                // var returnResult = _mapper.Map<IEnumerable<ProyectoDTOWithCouresesForGet>>(result);
            }
            catch (Exception ex)
            {
                //Log de la excepcion
                return StatusCode(500, "");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] AsignacionesDTO asignaciones)
        {
            try
            {
                _logicaP.AgregarDesarrolladorAProyecto(asignaciones.UsuarioId, asignaciones.ProyectoId);
            }
            catch (ArgumentNullException nullex)
            {
                return UnprocessableEntity(nullex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, error_de_servidor);
            }
            return Ok();
        }
    }
}
