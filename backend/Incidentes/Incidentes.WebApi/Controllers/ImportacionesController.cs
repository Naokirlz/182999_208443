using Incidentes.LogicaInterfaz;
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
        public IActionResult Post([FromBody] string rutaFuente)
        {
            _logica.ImportarBugs(rutaFuente);
            return Ok();
        }
    }
}
