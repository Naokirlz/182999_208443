using Incidentes.Dominio;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Filters;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Incidentes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [TrapExcepciones]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ILogicaUsuario _logica;

        public UsuariosController(ILogicaUsuario logica)
        {
            _logica = logica;
        }

        [HttpGet]
        [FilterAutorizacion("Administrador")]
        public IActionResult Get(Usuario.Rol? rol = null)
        {
            List<Usuario> result = _logica.Obtener(rol);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [FilterAutorizacion("Administrador")]
        public IActionResult Get(int id)
        {
            Usuario desarrollador = _logica.Obtener(id);
            return Ok(desarrollador);
        }

        [HttpPost]
        [FilterAutorizacion("Administrador")]
        public IActionResult Post([FromBody] Usuario user)
        {
            _logica.Alta(user);
            return Ok(user);
        }
    }
}
