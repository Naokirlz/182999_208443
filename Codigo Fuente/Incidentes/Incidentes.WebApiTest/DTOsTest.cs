using Incidentes.Dominio;
using Incidentes.WebApi.DTOs;
using NUnit.Framework;
using System.Collections.Generic;

namespace Incidentes.WebApiTest
{
    public class DTOsTest
    {
        [Test]
        public void funciona_correctamente_usuarioParaDto()
        {
            UsuarioParaReporteDTO usu = new UsuarioParaReporteDTO()
            {
                Nombre = "Usuario",
                Apellido = "Apellido",
                Email = "email@email",
                RolUsuario = Usuario.Rol.Tester,
                NombreUsuario = "martincosat1",
                Id = 5,
                ValorHora = 3,
                IncidentesResueltos = 3
            };
            Assert.AreEqual("Usuario", usu.Nombre);
            Assert.AreEqual("Apellido", usu.Apellido);
            Assert.AreEqual("email@email", usu.Email);
            Assert.AreEqual("martincosat1", usu.NombreUsuario);
            Assert.AreEqual(Usuario.Rol.Tester, usu.RolUsuario);
            Assert.AreEqual(5, usu.Id);
            Assert.AreEqual(3, usu.ValorHora);
            Assert.AreEqual(3, usu.IncidentesResueltos);
        }

        [Test]
        public void funciona_correctamente_proyectoDto()
        {
            UsuarioParaReporteDTO usu = new UsuarioParaReporteDTO()
            {
                Nombre = "Incidente complicado",
                Apellido = "Apellido",
                Email = "email@email",
                RolUsuario = Usuario.Rol.Tester,
                NombreUsuario = "martincosat1",
                Id = 5,
                ValorHora = 3,
                IncidentesResueltos = 3
            };
            List<UsuarioParaReporteDTO> usus = new List<UsuarioParaReporteDTO>();
            usus.Add(usu);
            List<Incidente> incL = new List<Incidente>();
            List<Tarea> tarL = new List<Tarea>();
            ProyectosDTO pro = new ProyectosDTO()
            {
                Id = 3,
                Nombre = "nombre pro",
                Asignados = usus,
                Tareas = tarL,
                Incidentes = incL
            };
            Assert.AreEqual("nombre pro", pro.Nombre);
            Assert.AreEqual(3, pro.Id);
            Assert.AreEqual(usus, pro.Asignados);
            Assert.AreEqual(incL, pro.Incidentes);
            Assert.AreEqual(tarL, pro.Tareas);
        }

        [Test]
        public void funciona_correctamente_proyectoParaReporteDto()
        {
            ProyectoParaReporteDTO pro = new ProyectoParaReporteDTO()
            {
                Id = 3,
                Nombre = "nombre pro",
                CantidadDeIncidentes = 4
            };
            Assert.AreEqual(3, pro.Id);
            Assert.AreEqual("nombre pro", pro.Nombre);
            Assert.AreEqual(4, pro.CantidadDeIncidentes);
        }

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
    }
}
