using Incidentes.DTOs;
using Incidentes.Logica.Excepciones;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private const string elemento_no_corresponde = "La entidad no corresponde con la enviada por parámetro";

        public IncidentesController(ILogicaIncidente logicaI, ILogicaUsuario logicaU, ILogicaProyecto logicaP)
        {
            _logicaI = logicaI;
            _logicaU = logicaU;
            _logicaP = logicaP;
        }

        [HttpGet]
        [FilterAutorizacion("Administrador", "Tester", "Desarrollador")]
        public IActionResult Get()
        {
            string token = Request.Headers["autorizacion"];
            UsuarioDTO usu = _logicaU.ObtenerPorToken(token);

            IEnumerable<IncidenteDTO> result = new List<IncidenteDTO>();

            if (usu.RolUsuario == 0)
            {
                result = _logicaI.ObtenerTodos();
            }
            else
            {
                result = _logicaI.ListaDeIncidentesDeLosProyectosALosQuePertenece(usu.Id, "", new IncidenteDTO());
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        [FilterAutorizacion("Administrador", "Tester", "Desarrollador")]
        public IActionResult Get(int id)
        {
            string token = Request.Headers["autorizacion"];
            idUsuario(token, id);

            var incidente = _logicaI.Obtener(id);
            return Ok(incidente);
        }

        [HttpGet("filtrado")]
        [FilterAutorizacion("Desarrollador", "Tester")]
        [TrapExcepciones]
        public IActionResult GetIncidentes([FromQuery] string idIncidente = null, string nombreProyecto = null, string nombreIncidente = null, string estadoIncidente = null)
        {
            string token = Request.Headers["autorizacion"];
            UsuarioDTO usu = _logicaU.ObtenerPorToken(token);

            IncidenteDTO incidente = new IncidenteDTO()
            {
                Nombre = nombreIncidente
            };
            if(idIncidente != null)
            {
                incidente.Id = Int32.Parse(idIncidente);
            }
            if (estadoIncidente != null && "Activo".Contains(estadoIncidente)) incidente.EstadoIncidente = IncidenteDTO.Estado.Activo;
            if (estadoIncidente != null && "Resuelto".Contains(estadoIncidente)) incidente.EstadoIncidente = IncidenteDTO.Estado.Resuelto;

            List<IncidenteDTO> result = _logicaI.ListaDeIncidentesDeLosProyectosALosQuePertenece(usu.Id, nombreProyecto, incidente);
            return Ok(result);
        }

        [HttpPost]
        [FilterAutorizacion("Administrador", "Tester")]
        public IActionResult Post([FromBody] IncidenteDTO incidente)
        {
            string token = Request.Headers["autorizacion"];
            int idU = idUsuario(token, -1, incidente.ProyectoId);
            if (incidente.EstadoIncidente == IncidenteDTO.Estado.Resuelto) incidente.DesarrolladorId = idU;
            if (incidente.EstadoIncidente == IncidenteDTO.Estado.Indiferente) incidente.EstadoIncidente = IncidenteDTO.Estado.Activo;

            _logicaI.Alta(incidente);
            return Ok(incidente);
        }

        [HttpDelete("{id}")]
        [FilterAutorizacion("Administrador", "Tester")]
        public IActionResult Delete(int id)
        {
            string token = Request.Headers["autorizacion"];
            idUsuario(token, id);

            _logicaI.Baja(id);
            return StatusCode(204);
        }

        [HttpPut("{id}")]
        [FilterAutorizacion("Administrador", "Tester")]
        public IActionResult Put(int id, [FromBody] IncidenteDTO incidente)
        {
            if (id != incidente.Id) throw new ExcepcionArgumentoNoValido(elemento_no_corresponde);
            string token = Request.Headers["autorizacion"];
            int idU = idUsuario(token, incidente.Id);
            if (incidente.EstadoIncidente == IncidenteDTO.Estado.Resuelto) incidente.DesarrolladorId = idU;
            if (incidente.EstadoIncidente == IncidenteDTO.Estado.Indiferente) incidente.EstadoIncidente = IncidenteDTO.Estado.Activo;
            if (incidente.EstadoIncidente == IncidenteDTO.Estado.Activo) incidente.DesarrolladorId = 0;
            if (incidente.EstadoIncidente == IncidenteDTO.Estado.Activo) incidente.DesarrolladorNombre = "";

            IncidenteDTO inc = _logicaI.Modificar(incidente.Id, incidente);
            return Ok(inc);
        }

        //private void usuarioPerteneceAlProyecto(string token, int idIncidente, int proyId = 0)
        //{
        //    UsuarioDTO usu = _logicaU.ObtenerPorToken(token);
        //    if (usu.RolUsuario != UsuarioDTO.Rol.Administrador)
        //    {
        //        if (idIncidente != -1)
        //        {
        //            IncidenteDTO inc = _logicaI.Obtener(idIncidente);
        //            bool autorizado = _logicaP.VerificarUsuarioPerteneceAlProyecto(usu.Id, inc.ProyectoId);
        //            if (!autorizado) throw new ExcepcionAccesoNoAutorizado(usuario_no_pertenece);
        //        }
        //        else
        //        {
        //            bool autorizado = _logicaP.VerificarUsuarioPerteneceAlProyecto(usu.Id, proyId);
        //            if (!autorizado) throw new ExcepcionAccesoNoAutorizado(usuario_no_pertenece);
        //        }
        //    }
        //}

        private int idUsuario(string token, int idIncidente, int proyId = 0)
        {
            UsuarioDTO usu = _logicaU.ObtenerPorToken(token);
            if (usu.RolUsuario != UsuarioDTO.Rol.Administrador)
            {
                if (idIncidente != -1)
                {
                    IncidenteDTO inc = _logicaI.Obtener(idIncidente);
                    usuarioPerteneceProyecto(usu.Id, inc.ProyectoId);
                }
                else
                {
                    usuarioPerteneceProyecto(usu.Id, proyId);
                }
            }
            return usu.Id;
        }

        private void usuarioPerteneceProyecto(int idUsu, int idPro)
        {
            bool autorizado = _logicaP.VerificarUsuarioPerteneceAlProyecto(idUsu, idPro);
            if (!autorizado) throw new ExcepcionAccesoNoAutorizado(usuario_no_pertenece);
        }
    }
}