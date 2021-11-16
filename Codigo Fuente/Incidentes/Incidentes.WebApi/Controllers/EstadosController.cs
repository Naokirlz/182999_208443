using Incidentes.DTOs;
using Incidentes.Logica.Excepciones;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Filters;
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
        public IActionResult Put([FromBody] IncidenteDTO incidente)
        {
            string token = Request.Headers["autorizacion"];
            int idUsu = idUsuario(token, incidente.Id);

            IncidenteDTO aResolver = new IncidenteDTO()
            {
                Id = incidente.Id,
                EstadoIncidente = incidente.EstadoIncidente,
                ProyectoId = incidente.ProyectoId,
                Duracion = incidente.Duracion,
                DesarrolladorId = idUsu
            };
            if (incidente.EstadoIncidente == IncidenteDTO.Estado.Activo) aResolver.DesarrolladorId = 0;

            _logicaI.Modificar(incidente.Id, aResolver);
            return Ok(incidente);
        }

        private int idUsuario(string token, int idIncidente)
        {
            UsuarioDTO usu = _logicaU.ObtenerPorToken(token);
            IncidenteDTO inc = _logicaI.Obtener(idIncidente);
            usuarioPerteneceProyecto(usu.Id, inc.ProyectoId);
            return usu.Id;
        }

        private void usuarioPerteneceProyecto(int idUsu, int idPro)
        {
            bool autorizado = _logicaP.VerificarUsuarioPerteneceAlProyecto(idUsu, idPro);
            if (!autorizado) throw new ExcepcionAccesoNoAutorizado(usuario_no_pertenece);
        }
    }
}
