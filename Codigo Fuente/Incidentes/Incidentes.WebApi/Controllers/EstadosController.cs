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
    [TrapExcepciones]
    public class EstadosController : ControllerBase
    {
        private readonly ILogicaIncidente _logicaI;
        private readonly ILogicaUsuario _logicaU;
        private readonly ILogicaProyecto _logicaP;
        private const string usuario_no_pertenece = "El usuario no pertenece al proyecto";

        public EstadosController(ILogicaIncidente logica, ILogicaUsuario logicaU, ILogicaProyecto logicaP)
        {
            _logicaI = logica;
            _logicaU = logicaU;
            _logicaP = logicaP;
        }

        [HttpPut]
        [FilterAutorizacion("Desarrollador", "Tester")]
        public IActionResult Put([FromBody] Incidente incidente)
        {
            string token = Request.Headers["autorizacion"];
            usuarioPerteneceAlProyecto(token, incidente.Id);

            Incidente aResolver = new Incidente()
            {
                Id = incidente.Id,
                EstadoIncidente = Incidente.Estado.Resuelto,
                ProyectoId = incidente.ProyectoId,
                DesarrolladorId = incidente.DesarrolladorId
            };
            _logicaI.Modificar(incidente.Id, aResolver);
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
