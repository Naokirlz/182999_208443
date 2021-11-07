using Incidentes.Dominio;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Filters;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Incidentes.WebApi.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogicaUsuario _logica;

        public LoginController(ILogicaUsuario logica)
        {
            _logica = logica;
        }

        [HttpPost]
        [Route("api/Login")]
        [TrapExcepciones]
        public IActionResult Login(Usuario usuario)
        {
            usuario.Token = _logica.Login(usuario.NombreUsuario, usuario.Contrasenia);
            usuario = _logica.ObtenerPorToken(usuario.Token);
            return Ok(usuario);
        }

        [HttpPost]
        [Route("api/Logout")]
        [TrapExcepciones]
        public IActionResult Logout(Usuario usuario)
        {
            _logica.Logout(usuario.Token);
            return Ok();
        }
    }
}
