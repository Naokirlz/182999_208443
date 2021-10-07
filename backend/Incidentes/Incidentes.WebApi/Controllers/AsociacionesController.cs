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
    [TrapExcepciones]
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

        [HttpGet("{id}/proyectos")]
        [FilterAutorizacion("Desarrollador", "Tester")]
        public IActionResult GetProyectos(string id)
        {
            List<Proyecto> result = _logicaP.ListaDeProyectosALosQuePertenece(Int32.Parse(id)).ToList();
            List<ProyectosDTO> proyectos = new List<ProyectosDTO>();
            foreach (Proyecto p in result)
            {
                ProyectosDTO pro = new ProyectosDTO()
                {
                    Id = p.Id,
                    Incidentes = p.Incidentes,
                    Nombre = p.Nombre
                };
                foreach (Usuario u in p.Asignados)
                {
                    UsuarioParaReporteDTO usu = new UsuarioParaReporteDTO()
                    {
                        Apellido = u.Apellido,
                        Email = u.Email,
                        Id = u.Id,
                        Nombre = u.Nombre,
                        NombreUsuario = u.NombreUsuario,
                        RolUsuario = u.RolUsuario
                    };
                    pro.Asignados.Add(usu);
                }
                proyectos.Add(pro);
            }
            return Ok(proyectos);
        }

        [HttpGet("{id}/proyecto")]
        [FilterAutorizacion("Desarrollador", "Tester")]
        //[Route("{id}/proyecto")]
        public IActionResult GetProyecto(string id, [FromQuery] int idProyecto)
        {
            Proyecto result = _logicaP.ObtenerParaUsuario(Int32.Parse(id), idProyecto);
            ProyectosDTO pro = new ProyectosDTO()
            {
                Id = result.Id,
                Incidentes = result.Incidentes,
                Nombre = result.Nombre
            };
            foreach (Usuario u in result.Asignados)
            {
                UsuarioParaReporteDTO usu = new UsuarioParaReporteDTO()
                {
                    Apellido = u.Apellido,
                    Email = u.Email,
                    Id = u.Id,
                    Nombre = u.Nombre,
                    NombreUsuario = u.NombreUsuario,
                    RolUsuario = u.RolUsuario
                };
                pro.Asignados.Add(usu);
            }
            return Ok(pro);
        }

        [HttpGet]
        [FilterAutorizacion("Desarrollador", "Tester")]
        [Route("{id}/incidente")]
        public IActionResult GetIncidente([FromRoute] string idUsuario, [FromQuery] int idIncidente)
        {
            Incidente result = _logicaI.ObtenerParaUsuario(Int32.Parse(idUsuario), idIncidente);
            return Ok(result);
        }

        [HttpGet]
        [FilterAutorizacion("Desarrollador", "Tester")]
        [Route("{id}/incidentes")]
        public IActionResult GetIncidentes([FromRoute] string idUsuario, [FromQuery] string nombreProyecto = null, Incidente incidente = null)
        {
            List<Incidente> result = _logicaI.ListaDeIncidentesDeLosProyectosALosQuePertenece(Int32.Parse(idUsuario), nombreProyecto, incidente);
            return Ok(result);
        }

        [HttpPost]
        [FilterAutorizacion("Administrador")]
        public IActionResult Post([FromBody] AsignacionesDTO asignaciones)
        {
            _logicaP.AgregarDesarrolladorAProyecto(asignaciones.UsuarioId, asignaciones.ProyectoId);
            return Ok();
        }
    }
}
