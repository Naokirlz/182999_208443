using Incidentes.DTOs;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Incidentes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsociacionesController : ControllerBase
    {
        private readonly ILogicaProyecto _logicaP;
        private readonly ILogicaIncidente _logicaI;

        public AsociacionesController(ILogicaProyecto logica, ILogicaIncidente logicaI)
        {
            _logicaP = logica;
            _logicaI = logicaI;
        }

        [HttpPost]
        [FilterAutorizacion("Administrador")]
        [TrapExcepciones]
        public IActionResult Post([FromBody] AsignacionesDTO asignaciones)
        {
            _logicaP.AgregarDesarrolladorAProyecto(asignaciones.UsuarioId, asignaciones.ProyectoId);
            return Ok();
        }
    }
}
