using AutoMapper;
using Incidentes.Dominio;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.WebApiTest
{
    public class TestersControllerTest
    {
        private Mock<ILogicaUsuario> _logicaU;
        private Mock<IMapper> _mapper;
        private TestersController _tController;
        private IQueryable<Usuario> testersQ;
        private List<Usuario> testersL;

        [SetUp]
        public void Setup()
        {
            _logicaU = new Mock<ILogicaUsuario>();
            _mapper = new Mock<IMapper>();
            _tController = new TestersController(_logicaU.Object, _mapper.Object);
            testersL = new List<Usuario>();
        }

        [TearDown]
        public void TearDown()
        {
            _logicaU = null;
            _mapper = null;
            _tController = null;
            testersQ = null;
            testersL = null;
        }

        [Test]
        public void se_pueden_ver_los_testers()
        {
            testersL.Add(new Usuario() { 
                RolUsuario = Usuario.Rol.Tester
            });
            _logicaU.Setup(c => c.ObtenerTesters()).Returns(testersL);

            var result = _tController.Get();

            Assert.IsNotNull(result);

            _logicaU.Verify(c => c.ObtenerTesters());
        }

        [Test]
        public void se_puede_ver_un_tester()
        {
            Usuario buscado = new Usuario()
            {
                RolUsuario = Usuario.Rol.Tester
            };
            _logicaU.Setup(c => c.ObtenerTester(3)).Returns(buscado);

            var result = _tController.Get(3);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(buscado, okResult.Value);

            _logicaU.Verify(c => c.ObtenerTester(3));
        }

        [Test]
        public void se_puede_guardar_un_tester()
        {
            Usuario t = new Usuario()
            {
                Nombre = "Tester",
                RolUsuario = Usuario.Rol.Tester
            };

            _logicaU.Setup(c => c.Alta(t)).Returns(t);

            var result = _tController.Post(t);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(t, okResult.Value);

            _logicaU.Verify(c => c.Alta(It.IsAny<Usuario>()));
        }
    }
}
