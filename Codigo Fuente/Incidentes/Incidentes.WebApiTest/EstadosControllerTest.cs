using Incidentes.Dominio;
using Incidentes.DTOs;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Incidentes.WebApiTest
{
    public class EstadosControllerTest
    {
        private Mock<ILogicaIncidente> _logicaI;
        private Mock<ILogicaUsuario> _logicaU;
        private Mock<ILogicaProyecto> _logicaP;
        private EstadosController _eController;
        private List<Incidente> incidentesL;

        [SetUp]
        public void Setup()
        {
            _logicaI = new Mock<ILogicaIncidente>();
            _logicaU = new Mock<ILogicaUsuario>();
            _logicaP = new Mock<ILogicaProyecto>();
            _eController = new EstadosController(_logicaI.Object, _logicaU.Object, _logicaP.Object);
            incidentesL = new List<Incidente>();
        }

        [TearDown]
        public void TearDown()
        {
            _logicaI = null;
            _logicaU = null;
            _logicaP = null;
            _eController = null;
            incidentesL = null;
        }

        [Test]
        public void se_puede_resolver_un_incidente()
        {
            UsuarioDTO usu = new UsuarioDTO()
            {
                Id = 3,
                RolUsuario = UsuarioDTO.Rol.Desarrollador
            };

            Incidente i = new Incidente()
            {
                Id = 3,
                DesarrolladorId = 2
            };

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(usu);
            _logicaI.Setup(c => c.Obtener(It.IsAny<int>())).Returns(i);
            _logicaP.Setup(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new EstadosController(_logicaI.Object, _logicaU.Object, _logicaP.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            _logicaI.Setup(c => c.Modificar(3, i)).Returns(i);

            var result = tested.Put(i);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(i, okResult.Value);

            _logicaI.Verify(c => c.Modificar(3, It.IsAny<Incidente>()));
            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
            _logicaI.Verify(c => c.Obtener(It.IsAny<int>()));
            _logicaP.Verify(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
        }
    }
}
