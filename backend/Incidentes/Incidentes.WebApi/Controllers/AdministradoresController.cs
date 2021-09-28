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
    public class AdministradoresController : ControllerBase
    {
        private const string error_de_servidor = "Internal Server Error";
        private readonly IMapper _mapper;
        private readonly ILogicaUsuario _logica;

        public AdministradoresController(ILogicaUsuario logica, IMapper mapper)
        {
            _logica = logica;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Administrador administrador)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    Administrador a = new Administrador()
                    {
                        Id = administrador.Id,
                        Nombre = administrador.Nombre,
                        NombreUsuario = administrador.Nombre
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
            }
            catch (Exception ex)
            {
                return StatusCode(500, error_de_servidor);
            }
            return Ok();
        }
    }
}
