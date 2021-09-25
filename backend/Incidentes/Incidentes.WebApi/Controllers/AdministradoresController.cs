using AutoMapper;
using Incidentes.Dominio;
using Incidentes.Logica.Interfaz;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Incidentes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministradoresController : ControllerBase
    {
        private const string error_de_servidor = "Internal Server Error";
        private readonly IMapper _mapper;
        private readonly ILogicaAdministrador _logica;

        public AdministradoresController(ILogicaAdministrador logica, IMapper mapper)
        {
            _logica = logica;
            _mapper = mapper;
        }

        [HttpPost]
        //TODO: Implementación nueva, ahora usamos un DTO. 
        public IActionResult Post([FromBody] Administrador administrador)
        {

            try
            {
                /* Esto lo que hace es ejecutar las validaciones que pusimos dentro del objeto StudentDTO. 
                 * En otras palabras, valida los parametros.*/
                if (ModelState.IsValid)
                {
                    //A partir del DTO creamos un objeto Student.
                    Administrador a = new Administrador()
                    {
                        Id = administrador.Id,
                        Nombre = administrador.Nombre
                    };

                    //... y lo agregamos, como antes.
                    _logica.Alta(a);
                }
                else
                {
                    return UnprocessableEntity(ModelState);
                }
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

            return Ok();
        }
    }
}
