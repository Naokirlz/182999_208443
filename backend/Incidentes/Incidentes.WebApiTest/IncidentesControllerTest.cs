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
    public class IncidentesControllerTest
    {
        private Mock<ILogicaIncidente> _logicaI;
        private Mock<IMapper> _mapper;
        private IncidentesController _iController;
        private IQueryable<Incidente> incidentesQ;
        private List<Incidente> incidentesL;

        [SetUp]
        public void Setup()
        {
            _logicaI = new Mock<ILogicaIncidente>();
            _mapper = new Mock<IMapper>();
            _iController = new IncidentesController(_logicaI.Object, _mapper.Object);
            incidentesL = new List<Incidente>();
        }

        [TearDown]
        public void TearDown()
        {
            _logicaI = null;
            _mapper = null;
            _iController = null;
            incidentesQ = null;
            incidentesL = null;
        }

        [Test]
        public void se_pueden_ver_los_incidentes()
        {
            incidentesL.Add(new Incidente());
            incidentesQ = incidentesL.AsQueryable();
            _logicaI.Setup(c => c.ObtenerTodos()).Returns(incidentesQ);

            var result = _iController.Get();
            var okResult = result as OkObjectResult;

            Assert.AreEqual(incidentesQ, okResult.Value);

            _logicaI.Verify(c => c.ObtenerTodos());
        }

        [Test]
        public void se_puede_ver_un_incidente()
        {
            Incidente i = new Incidente()
            {
                Nombre = "incidente"
            };

            _logicaI.Setup(c => c.Obtener(1)).Returns(i);

            var result = _iController.Get(1);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(i, okResult.Value);

            _logicaI.Verify(c => c.Obtener(1));
        }

        [Test]
        public void se_puede_guardar_un_incidente()
        {
            Incidente i = new Incidente()
            {
                Nombre = "Incidente"
            };

            _logicaI.Setup(c => c.Alta(i)).Returns(i);

            var result = _iController.Post(i);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(i, okResult.Value);

            _logicaI.Verify(c => c.Alta(It.IsAny<Incidente>()));
        }

        [Test]
        public void se_puede_actualizar_un_incidente()
        {
            Incidente i = new Incidente()
            {
                Id = 3,
                Nombre = "Incidente"
            };

            _logicaI.Setup(c => c.Modificar(3, i)).Returns(i);

            var result = _iController.Put(i);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(i, okResult.Value);

            _logicaI.Verify(c => c.Modificar(3, It.IsAny<Incidente>()));
        }

        [Test]
        public void se_puede_eliminar_un_incidente()
        {
            Incidente i = new Incidente()
            {
                Id = 3,
                Nombre = "Incidente"
            };

            _logicaI.Setup(c => c.Baja(3));

            var result = _iController.Delete(i);

            Assert.IsNotNull(result);

            _logicaI.Verify(c => c.Baja(3));
        }
    }
}