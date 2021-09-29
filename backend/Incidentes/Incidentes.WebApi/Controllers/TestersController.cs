using AutoMapper;
using Incidentes.Dominio;
using Incidentes.Logica.Interfaz;
using Incidentes.LogicaInterfaz;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Incidentes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestersController : ControllerBase
    {
        private const string error_de_servidor = "Internal Server Error";
        private readonly IMapper _mapper;
        private readonly ILogicaUsuario _logica;

        public TestersController(ILogicaUsuario logica, IMapper mapper)
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
                List<Tester> result = _logica.ObtenerTesters();
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
                Tester tester = _logica.ObtenerTester(id);
                if (tester == null)
                {
                    return NotFound(id);
                }

                return Ok(tester);
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
        public IActionResult Post([FromBody] Tester tester)
        {

            try
            {
                //... y lo agregamos, como antes.
                _logica.Alta(tester);
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

            return Ok(tester);
        }
    }
}
