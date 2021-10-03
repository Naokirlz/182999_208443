using AutoMapper;
using Incidentes.Dominio;
using Incidentes.Logica.Interfaz;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.DTOs;
using Incidentes.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Incidentes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private const string error_de_servidor = "Internal Server Error";
        private readonly IMapper _mapper;
        private readonly ILogicaProyecto _logicaP;
        private readonly ILogicaUsuario _logicaU;

        public ReportesController(ILogicaProyecto logica, ILogicaUsuario logicaU, IMapper mapper)
        {
            _logicaP = logica;
            _logicaU = logicaU;
            _mapper = mapper;
        }

        [HttpGet]
        //[Autorizacion("Administrador")]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Proyecto> proyectos = _logicaP.ObtenerTodos();
                List<ProyectoParaReporteDTO> result = new List<ProyectoParaReporteDTO>();
                foreach(Proyecto p in proyectos)
                {
                    ProyectoParaReporteDTO nuevo = new ProyectoParaReporteDTO() { 
                        Id = p.Id,
                        Nombre = p.Nombre,
                        CantidadDeIncidentes = p.Incidentes.Count()
                    };
                    result.Add(nuevo);
                }


                return Ok(result);
                // var returnResult = _mapper.Map<IEnumerable<ProyectoDTOWithCouresesForGet>>(result);
            }
            catch (Exception ex)
            {
                //Log de la excepcion
                return StatusCode(500, "");
            }
        }

        [HttpGet]
        //[Autorizacion("Administrador")]
        [Route("{id}/incidentes")]
        public IActionResult GetDesarrollador([FromRoute] string idUsuario)
        {
            try
            {
                Usuario usuario = _logicaU.Obtener(Int32.Parse(idUsuario));
                int cantidad = _logicaU.CantidadDeIncidentesResueltosPorUnDesarrollador(usuario.Id);
                UsuarioParaReporteDTO user = new UsuarioParaReporteDTO() { 
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Email = usuario.Email,
                    Id = usuario.Id,
                    IncidentesResueltos = cantidad,
                    NombreUsuario = usuario.NombreUsuario,
                    RolUsuario = usuario.RolUsuario
                };
                return Ok(user);
                // var returnResult = _mapper.Map<IEnumerable<ProyectoDTOWithCouresesForGet>>(result);
            }
            catch (Exception ex)
            {
                //Log de la excepcion
                return StatusCode(500, "");
            }
        }
    }
}
