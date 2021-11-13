using AutoMapper;
using Incidentes.Dominio;
using Incidentes.DTOs;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Incidentes.WebApiTest
{
    class AsociacionesControllerTest
    {
        private Mock<ILogicaProyecto> _logicaP;
        private Mock<ILogicaIncidente> _logicaI;
        private AsociacionesController _aController;
        private Usuario u;
        private Incidente i;
        private Proyecto p;

        [SetUp]
        public void Setup()
        {
            _logicaP = new Mock<ILogicaProyecto>();
            _logicaI = new Mock<ILogicaIncidente>();
            _aController = new AsociacionesController(_logicaP.Object, _logicaI.Object);
            i = new Incidente()
            {
                Id = 3,
                Version = "2.0",
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
            _logicaI = null;
            _aController = null;
            u = null;
            i = null;
            p = null;
        }

        [Test]
        public void se_pueden_asignar_usuarios_a_un_proyecto()
        {
            _logicaP.Setup(c => c.AgregarDesarrolladorAProyecto(It.IsAny<List<int>>(), It.IsAny<int>()));

            var result = _aController.Post(new AsignacionesDTO());

            Assert.IsNotNull(result);

            _logicaP.Verify(c => c.AgregarDesarrolladorAProyecto(It.IsAny<List<int>>(), It.IsAny<int>()));
        }
    }
}
