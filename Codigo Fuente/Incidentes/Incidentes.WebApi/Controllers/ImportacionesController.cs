using Incidentes.Logica.DTOs;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.DTOs;
using Incidentes.WebApi.Filters;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Incidentes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportacionesController : ControllerBase
    {
        private readonly ILogicaImportaciones _logica;

        public ImportacionesController(ILogicaImportaciones logica)
        {
            _logica = logica;
        }

        [HttpPost]
        [TrapExcepciones]
        public IActionResult Post([FromBody] FuenteDTO fuente)
        {
            _logica.ImportarBugs(fuente.rutaFuente, fuente.rutaBinario, fuente.usuarioId);
            return Ok();
        }

        [HttpGet]
        [TrapExcepciones]
        public IActionResult Get()
        {
            List<string> result = _logica.ListarPlugins();
            List<FuenteDTO> lista = new List<FuenteDTO>();
            foreach(string s in result)
            {
                FuenteDTO imp = new FuenteDTO()
                {
                    rutaBinario = s
                };
                lista.Add(imp);
            }
            return Ok(lista);
        }
    }
}
