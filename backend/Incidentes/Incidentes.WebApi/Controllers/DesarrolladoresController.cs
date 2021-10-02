using AutoMapper;
using Incidentes.Dominio;
using Incidentes.Logica.Interfaz;
using Incidentes.LogicaInterfaz;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Incidentes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesarrolladoresController : ControllerBase
    {
        private const string error_de_servidor = "Internal Server Error";
        private readonly IMapper _mapper;
        private readonly ILogicaUsuario _logica;

        public DesarrolladoresController(ILogicaUsuario logica, IMapper mapper)
        {
            _logica = logica;
            _mapper = mapper;
        }

        [HttpGet]
        //[Auth("professor")]
        public IActionResult Get()
        {
            try
            {
                List<Usuario> result = _logica.ObtenerDesarrolladores();
                // var returnResult = _mapper.Map<IEnumerable<ProyectoDTOWithCouresesForGet>>(result);
                return Ok(result);
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
                Usuario desarrollador = _logica.ObtenerDesarrollador(id);
                if (desarrollador == null)
                {
                    return NotFound(id);
                }

                return Ok(desarrollador);
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
        //TODO: Implementación nueva, ahora usamos un DTO. 
        public IActionResult Post([FromBody] Usuario desarrollador)
        {

            try
            {
                    //... y lo agregamos, como antes.
                    _logica.Alta(desarrollador);
            }
            catch (ArgumentNullException nullex)
            {
                return UnprocessableEntity(nullex.Message);

                // return BadRequest(nullex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, error_de_servidor);
            }

            return Ok(desarrollador);
        }
    }
}
