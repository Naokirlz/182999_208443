using Incidentes.Dominio;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Incidentes.WebApiTest
{
    public class ProyectosControllerTest
    {
        private Mock<ILogicaProyecto> _logicaP;
        private ProyectosController _pController;
        private List<Proyecto> proyectosL;
        private Usuario u;
        private Incidente i;
        private Proyecto p;

        [SetUp]
        public void Setup()
        {
            _logicaP = new Mock<ILogicaProyecto>();
            _pController = new ProyectosController(_logicaP.Object);
            proyectosL = new List<Proyecto>();
            i = new Incidente()
            {
                Id = 3,
                Version = "2.0",
                UsuarioId = 1,
                DesarrolladorId = 5,
                Descripcion = "descripcion",
                EstadoIncidente = Incidente.Estado.Resuelto,
                Nombre = "Nombre",
                ProyectoId = 7
            };
            u = new Usuario()
            {
                Nombre = "sssss",
                Id = 9,
                Apellido = "aaaaaaa",
                Email = "ssasssa@asdasda.com",
                RolUsuario = Usuario.Rol.Desarrollador
            };
            p = new Proyecto()
            {
                Nombre = "Proyecto",
                Id = 7,
                Incidentes = new List<Incidente>() { i },
                Asignados = new List<Usuario>() { u }
            };
        }

        [TearDown]
        public void TearDown()
        {
            _logicaP = null;
            _pController = null;
            proyectosL = null;
            u = null;
            i = null;
            p = null;
        }

        [Test]
        public void se_pueden_ver_los_proyectos()
        {
            Proyecto p = new Proyecto();
            Usuario u = new Usuario() {
                Nombre = "Martin",
                Apellido = "Cosa",
                Contrasenia = "Casa#Blanca",
                RolUsuario = Usuario.Rol.Tester,
                Email = "martint1@gmail.com",
                NombreUsuario = "martincosat1",
                Token = ""
            };
            p.Asignados.Add(u);

            proyectosL.Add(p);
            
            _logicaP.Setup(c => c.ObtenerTodos()).Returns(proyectosL);

            var result = _pController.Get();
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(result);

            _logicaP.Verify(c => c.ObtenerTodos());
        }

        [Test]
        public void se_puede_ver_un_proyecto()
        {
            Proyecto p = new Proyecto()
            {
                Nombre = "proyecto"
            };

            _logicaP.Setup(c => c.Obtener(1)).Returns(p);

            var result = _pController.Get(1);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(p, okResult.Value);

            _logicaP.Verify(c => c.Obtener(1));
        }

        [Test]
        public void se_puede_guardar_un_proyecto()
        {
            Proyecto p = new Proyecto()
            {
                Nombre = "proyecto"
            };

            _logicaP.Setup(c => c.Alta(p)).Returns(p);

            var result = _pController.Post(p);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(p, okResult.Value);

            _logicaP.Verify(c => c.Alta(It.IsAny<Proyecto>()));
        }

        [Test]
        public void se_puede_actualizar_un_proyecto()
        {
            _logicaP.Setup(c => c.Modificar(It.IsAny<int>(), It.IsAny<Proyecto>())).Returns(p);

            var result = _pController.Put(p);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(result);

            _logicaP.Verify(c => c.Modificar(It.IsAny<int>(), It.IsAny<Proyecto>()));
        }

        [Test]
        public void se_puede_eliminar_un_proyecto()
        {
            Proyecto p = new Proyecto()
            {
                Id = 3,
                Nombre = "proyecto"
            };

            _logicaP.Setup(c => c.Baja(3));

            var result = _pController.Delete(p);

            Assert.IsNotNull(result);

            _logicaP.Verify(c => c.Baja(3));
        }
    }
}