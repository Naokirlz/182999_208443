﻿using AutoMapper;
using Incidentes.Dominio;
using Incidentes.Logica.Interfaz;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Incidentes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectosController : ControllerBase
    {
        private const string error_de_servidor = "Internal Server Error";
        private readonly IMapper _mapper;
        private readonly ILogicaProyecto _logicaP;

        public ProyectosController(ILogicaProyecto logica, IMapper mapper)
        {
            _logicaP = logica;
            _mapper = mapper;
        }

        [HttpGet]
        [Autorizacion("Administrador")]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Proyecto> result = _logicaP.ObtenerTodos();
                return Ok(result);
                // var returnResult = _mapper.Map<IEnumerable<ProyectoDTOWithCouresesForGet>>(result);
            }
            catch (Exception ex)
            {
                //Log de la excepcion
                return StatusCode(500, "");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var proyecto = _logicaP.Obtener(id);
                if (proyecto == null)
                {
                    return NotFound(id);
                }

                return Ok(proyecto);
                /*return Ok(new
                {
                    Nombre = student.Name,
                    Apellido = student.LastName
                });*/
            }
            catch (Exception ex)
            {
                //Log de la excepcion
                return StatusCode(500, "");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Proyecto proyecto)
        {
            try
            {
                _logicaP.Alta(proyecto);
            }
            catch (ArgumentNullException nullex)
            {
                return UnprocessableEntity(nullex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, error_de_servidor);
            }
            return Ok(proyecto);
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] Proyecto proyecto)
        {
            try
            {
                    _logicaP.Baja(proyecto.Id);
            }
            catch (ArgumentNullException nullex)
            {
                return UnprocessableEntity(nullex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, error_de_servidor);
            }
            return StatusCode(204, "Eliminado Satisfactoriamente.");
        }
        
        [HttpPut]
        public IActionResult Put([FromBody] Proyecto proyecto)
        {
            try
            {
                 _logicaP.Modificar(proyecto.Id, proyecto);
            }
            catch (ArgumentNullException nullex)
            {
                return UnprocessableEntity(nullex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, error_de_servidor);
            }
            return Ok(proyecto);
        }
    }
}