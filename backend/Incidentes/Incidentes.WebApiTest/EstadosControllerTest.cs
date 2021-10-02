using AutoMapper;
using Incidentes.Dominio;
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
        private Mock<IMapper> _mapper;
        private EstadosController _eController;
        private IQueryable<Incidente> incidentesQ;
        private List<Incidente> incidentesL;

        [SetUp]
        public void Setup()
        {
            _logicaI = new Mock<ILogicaIncidente>();
            _mapper = new Mock<IMapper>();
            _eController = new EstadosController(_logicaI.Object, _mapper.Object);
            incidentesL = new List<Incidente>();
        }

        [TearDown]
        public void TearDown()
        {
            _logicaI = null;
            _mapper = null;
            _eController = null;
            incidentesQ = null;
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
