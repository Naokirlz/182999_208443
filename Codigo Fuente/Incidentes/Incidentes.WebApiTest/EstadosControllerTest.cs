using Incidentes.Dominio;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Controllers;
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
            Incidente i = new Incidente()
            {
                Id = 3,
                DesarrolladorId = 2
            };

            _logicaI.Setup(c => c.Modificar(3, i)).Returns(i);

            var result = _eController.Put(i);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(i, okResult.Value);

            _logicaI.Verify(c => c.Modificar(3, It.IsAny<Incidente>()));
        }
    }
}
