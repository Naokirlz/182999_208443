using Incidentes.DTOs;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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
        public IActionResult Get()
        {
            List<UsuarioDTO> result = _logica.ObtenerTodos().ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [FilterAutorizacion("Administrador")]
        public IActionResult Get(int id)
        {
            UsuarioDTO usuario = _logica.Obtener(id);
            return Ok(usuario);
        }

        [HttpPost]
        [FilterAutorizacion("Administrador")]
        public IActionResult Post([FromBody] UsuarioDTO user)
        {
            _logica.Alta(user);
            return Ok(user);
        }
    }
}
