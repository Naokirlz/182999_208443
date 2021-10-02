using AutoMapper;
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
    public class IncidentesController : ControllerBase
    {
        private const string error_de_servidor = "Internal Server Error";
        private readonly IMapper _mapper;
        private readonly ILogicaIncidente _logicaI;

        public IncidentesController(ILogicaIncidente logica, IMapper mapper)
        {
            _logicaI = logica;
            _mapper = mapper;
        }

        [HttpGet]
        //[Autorizacion("Administrador")]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Incidente> result = _logicaI.ObtenerTodos();
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
                var incidente = _logicaI.Obtener(id);
                if (incidente == null)
                {
                    return NotFound(id);
                }

                return Ok(incidente);
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
        public IActionResult Post([FromBody] Incidente incidente)
        {
            try
            {
                _logicaI.Alta(incidente);
            }
            catch (ArgumentNullException nullex)
            {
                return UnprocessableEntity(nullex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, error_de_servidor);
            }
            return Ok(incidente);
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] Incidente incidente)
        {
            try
            {
                _logicaI.Baja(incidente.Id);
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
        public IActionResult Put([FromBody] Incidente incidente)
        {
            try
            {
                _logicaI.Modificar(incidente.Id, incidente);
            }
            catch (ArgumentNullException nullex)
            {
                return UnprocessableEntity(nullex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, error_de_servidor);
            }
            return Ok(incidente);
        }
    }
}