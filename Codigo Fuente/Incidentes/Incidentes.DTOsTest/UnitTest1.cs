using Incidentes.Dominio;
using Incidentes.DTOs;
using NUnit.Framework;
using System.Collections.Generic;

namespace Incidentes.DTOsTest
{
    public class Tests
    {
        [Test]
        public void funciona_correctamente_asignacionesDto()
        {
            List<int> lista = new List<int>();
            AsignacionesDTO asig = new AsignacionesDTO()
            {
                ProyectoId = 3,
                UsuarioId = new List<int>()
            };
            Assert.AreEqual(3, asig.ProyectoId);
            Assert.AreEqual(lista, asig.UsuarioId);
        }

        [Test]
        public void funciona_correctamente_importacionesDto()
        {
            ImportacionesDTO imp = new ImportacionesDTO()
            {
                Ruta = "asasas"
            };
            Assert.AreEqual(imp.Ruta, "asasas");
        }

        [Test]
        public void funciona_correctamente_proyectoDto()
        {
            UsuarioDTO usu = new UsuarioDTO()
            {
                Nombre = "Incidente complicado",
                Apellido = "Apellido",
                Email = "email@email",
                RolUsuario = UsuarioDTO.Rol.Tester,
                NombreUsuario = "martincosat1",
                Id = 5,
                ValorHora = 3,
                IncidentesResueltos = 3,
            };
            List<UsuarioDTO> usus = new List<UsuarioDTO>();
            usus.Add(usu);
            List<IncidenteDTO> incL = new List<IncidenteDTO>();
            List<TareaDTO> tarL = new List<TareaDTO>();
            ProyectoDTO pro = new ProyectoDTO()
            {
                Id = 3,
                Nombre = "nombre pro",
                Asignados = usus,
                Tareas = tarL,
                Incidentes = incL,
                CantidadDeIncidentes = 4
            };
            Assert.AreEqual("nombre pro", pro.Nombre);
            Assert.AreEqual(3, pro.Id);
            Assert.AreEqual(4, pro.CantidadDeIncidentes);
            Assert.AreEqual(usus, pro.Asignados);
            Assert.AreEqual(incL, pro.Incidentes);
            Assert.AreEqual(tarL, pro.Tareas);
        }

        [Test]
        public void funciona_correctamente_usuarioParaDto()
        {
            UsuarioDTO usu = new UsuarioDTO()
            {
                Nombre = "Usuario",
                Apellido = "Apellido",
                Email = "email@email",
                RolUsuario = UsuarioDTO.Rol.Tester,
                NombreUsuario = "martincosat1",
                Id = 5,
                ValorHora = 3,
                IncidentesResueltos = 3,
                Contrasenia = "aaaaa",
                Token = "wwwww"
            };
            Assert.AreEqual("Usuario", usu.Nombre);
            Assert.AreEqual("aaaaa", usu.Contrasenia);
            Assert.AreEqual("wwwww", usu.Token);
            Assert.AreEqual("Apellido", usu.Apellido);
            Assert.AreEqual("email@email", usu.Email);
            Assert.AreEqual("martincosat1", usu.NombreUsuario);
            Assert.AreEqual(UsuarioDTO.Rol.Tester, usu.RolUsuario);
            Assert.AreEqual(5, usu.Id);
            Assert.AreEqual(3, usu.ValorHora);
            Assert.AreEqual(3, usu.IncidentesResueltos);
        }

        [Test]
        public void puede_haber_usuario_admin()
        {
            UsuarioDTO usu = new UsuarioDTO()
            {
                RolUsuario = UsuarioDTO.Rol.Administrador
            };
            Assert.AreEqual(UsuarioDTO.Rol.Administrador, usu.RolUsuario);
        }

        [Test]
        public void puede_haber_usuario_des()
        {
            UsuarioDTO usu = new UsuarioDTO()
            {
                RolUsuario = UsuarioDTO.Rol.Desarrollador
            };
            Assert.AreEqual(UsuarioDTO.Rol.Desarrollador, usu.RolUsuario);
        }

        [Test]
        public void puede_haber_usuario_tester()
        {
            UsuarioDTO usu = new UsuarioDTO()
            {
                RolUsuario = UsuarioDTO.Rol.Tester
            };
            Assert.AreEqual(UsuarioDTO.Rol.Tester, usu.RolUsuario);
        }

        [Test]
        public void funciona_correctamente_fuentesDto()
        {
            FuenteDTO fuente = new FuenteDTO()
            {
                rutaBinario = "aaaaa",
                rutaFuente = "asdasdasd",
                usuarioId = 2
            };
            Assert.AreEqual(2, fuente.usuarioId);
            Assert.AreEqual("aaaaa", fuente.rutaBinario);
            Assert.AreEqual("asdasdasd", fuente.rutaFuente);
        }

        [Test]
        public void funciona_correctamente_tareaDto()
        {
            TareaDTO tarea = new TareaDTO()
            {
                Id = 3,
                Costo = 15,
                Duracion = 12,
                Nombre = "Tarea",
                ProyectoId = 4
            };
            Assert.AreEqual(3, tarea.Id);
            Assert.AreEqual(4, tarea.ProyectoId);
            Assert.AreEqual(15, tarea.Costo);
            Assert.AreEqual(12, tarea.Duracion);
            Assert.AreEqual("Tarea", tarea.Nombre);
        }

        [Test]
        public void funciona_correctamente_incidenteDto()
        {
            IncidenteDTO usu = new IncidenteDTO()
            {
                Nombre = "incidente",
                Duracion = 5,
                DesarrolladorId = 3,
                Descripcion = "aaaaa",
                EstadoIncidente = IncidenteDTO.Estado.Activo,
                Id = 6,
                ProyectoId = 9,
                Version = "1.2"
            };
            Assert.AreEqual("incidente", usu.Nombre);
            Assert.AreEqual("aaaaa", usu.Descripcion);
            Assert.AreEqual("1.2", usu.Version);
            Assert.AreEqual(IncidenteDTO.Estado.Activo, usu.EstadoIncidente);
            Assert.AreEqual(5, usu.Duracion);
            Assert.AreEqual(6, usu.Id);
            Assert.AreEqual(9, usu.ProyectoId);
            Assert.AreEqual(3, usu.DesarrolladorId);
        }
    }
}