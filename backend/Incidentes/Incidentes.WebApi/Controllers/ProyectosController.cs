using AutoMapper;
using Incidentes.Dominio;
using Incidentes.Logica.Interfaz;
using Incidentes.LogicaInterfaz;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Incidentes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectosController : ControllerBase
    {
        private const string error_de_servidor = "Internal Server Error";
        private readonly IMapper _mapper;
        private readonly ILogicaProyecto _logica;

        public ProyectosController(ILogicaProyecto logica, IMapper mapper)
        {
            _logica = logica;
            _mapper = mapper;
        }

        [HttpPost]
        //TODO: Implementación nueva, ahora usamos un DTO. 
        public IActionResult Post([FromBody] Proyecto proyecto)
        {

            try
            {
                /* Esto lo que hace es ejecutar las validaciones que pusimos dentro del objeto StudentDTO. 
                 * En otras palabras, valida los parametros.*/
                if (ModelState.IsValid)
                {
                    //A partir del DTO creamos un objeto Student.
                    Proyecto a = new Proyecto()
                    {
                        Id = proyecto.Id,
                        Nombre = proyecto.Nombre,
                        

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