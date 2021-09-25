using Incidentes.Dominio;
using Incidentes.Logica;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Incidentes.WebApi.Controllers
{
    [ApiController]
    [Route("Administradores")]
    public class AdministradorController : ControllerBase
    {
        private const string server_error = "Internal Server Error";

        private readonly GestorAdministrador _logica;

        public AdministradorController()
        {
            _logica = new GestorAdministrador();
            
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
                        Nombre = administrador.Nombre,
                        
                    };
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
                return StatusCode(500, server_error);
            }

            return Ok();
        }

    }
}
