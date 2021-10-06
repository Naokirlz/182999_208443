using Incidentes.Dominio;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Incidentes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosController : ControllerBase
    {
        private const string error_de_servidor = "Internal Server Error";
        private readonly ILogicaIncidente _logicaI;

        public EstadosController(ILogicaIncidente logica)
        {
            _logicaI = logica;
        }

        [HttpPut]
        [FilterAutorizacion("Desarrollador")]
        public IActionResult Put([FromBody] Incidente incidente)
        {
            Incidente aResolver = new Incidente()
            {
                Id = incidente.Id,
                EstadoIncidente = Incidente.Estado.Resuelto,
                DesarrolladorId = incidente.DesarrolladorId
            };
            _logicaI.Modificar(incidente.Id, aResolver);
            return Ok(incidente);
        }
    }
}
