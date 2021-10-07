using Incidentes.Logica.DTOs;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Incidentes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportacionesController : ControllerBase
    {
        private readonly ILogicaProyecto _logica;

        public ImportacionesController(ILogicaProyecto logica)
        {
            _logica = logica;
        }

        [HttpPost]
        [TrapExcepciones]
        public IActionResult Post([FromBody] FuenteDTO fuente)
        {
            _logica.ImportarBugs(fuente.rutaFuente.Replace("_", "/"), fuente.usuarioId);
            return Ok();
        }
    }
}
