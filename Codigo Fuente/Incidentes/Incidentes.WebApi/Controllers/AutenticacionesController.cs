using Incidentes.Dominio;
using Incidentes.Logica.Excepciones;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Filters;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Incidentes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionesController : ControllerBase
    {
        private readonly ILogicaUsuario _logica;
        private const string usuario_distinto = "No puede desautenticar otro usuario.";

        public AutenticacionesController(ILogicaUsuario logica)
        {
            _logica = logica;
        }

        [HttpPost]
        [TrapExcepciones]
        public IActionResult Login(Usuario usuario)
        {
            usuario.Token = _logica.Login(usuario.NombreUsuario, usuario.Contrasenia);
            usuario = _logica.ObtenerPorToken(usuario.Token);
            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        [TrapExcepciones]
        public IActionResult Logout(int id)
        {
            string token = Request.Headers["autorizacion"];
            Usuario usu = _logica.ObtenerPorToken(token);
            bool autorizado = usu.Id == id;
            if (!autorizado) throw new ExcepcionAccesoNoAutorizado(usuario_distinto);

            _logica.Logout(usu.Token);
            return Ok();
        }
    }
}
