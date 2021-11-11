using Incidentes.Dominio;
using Incidentes.Logica.Excepciones;
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
    public class IncidentesController : ControllerBase
    {
        private readonly ILogicaIncidente _logicaI;
        private readonly ILogicaUsuario _logicaU;
        private readonly ILogicaProyecto _logicaP;
        private const string usuario_no_pertenece = "El usuario no pertenece al proyecto";

        public IncidentesController(ILogicaIncidente logicaI, ILogicaUsuario logicaU, ILogicaProyecto logicaP)
        {
            _logicaI = logicaI;
            _logicaU = logicaU;
            _logicaP = logicaP;
        }

        [HttpGet]
        [FilterAutorizacion("Administrador")]
        public IActionResult Get()
        {
            IEnumerable<Incidente> result = _logicaI.ObtenerTodos();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [FilterAutorizacion("Administrador", "Tester", "Desarrollador")]
        public IActionResult Get(int id)
        {
            string token = Request.Headers["autorizacion"];
            usuarioPerteneceAlProyecto(token, id);

            var incidente = _logicaI.Obtener(id);
            return Ok(incidente);
        }

        [HttpPost]
        [FilterAutorizacion("Administrador", "Tester")]
        public IActionResult Post([FromBody] Incidente incidente)
        {
            string token = Request.Headers["autorizacion"];
            usuarioPerteneceAlProyecto(token, -1, incidente.ProyectoId);

            _logicaI.Alta(incidente);
            return Ok(incidente);
        }

        [HttpDelete("{id}")]
        [FilterAutorizacion("Administrador", "Tester")]
        public IActionResult Delete(int id)
        {
            string token = Request.Headers["autorizacion"];
            usuarioPerteneceAlProyecto(token, id);

            _logicaI.Baja(id);
            return StatusCode(204);
        }

        [HttpPut]
        [FilterAutorizacion("Administrador", "Tester")]
        public IActionResult Put([FromBody] Incidente incidente)
        {
            string token = Request.Headers["autorizacion"];
            usuarioPerteneceAlProyecto(token, incidente.Id);

            _logicaI.Modificar(incidente.Id, incidente);
            return Ok(incidente);
        }

        private void usuarioPerteneceAlProyecto(string token, int idIncidente, int proyId = 0)
        {
            Usuario usu = _logicaU.ObtenerPorToken(token);
            if (idIncidente != -1)
            {
                Incidente inc = _logicaI.Obtener(idIncidente);
                bool autorizado = _logicaP.VerificarUsuarioPerteneceAlProyecto(usu.Id, inc.ProyectoId);
                if (!autorizado) throw new ExcepcionAccesoNoAutorizado(usuario_no_pertenece);
            }
            else
            {
                bool autorizado = _logicaP.VerificarUsuarioPerteneceAlProyecto(usu.Id, proyId);
                if (!autorizado) throw new ExcepcionAccesoNoAutorizado(usuario_no_pertenece);
            }
        }
    }
}