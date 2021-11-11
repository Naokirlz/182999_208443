using Incidentes.Dominio;
using Incidentes.Logica.Excepciones;
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
        private Mock<ILogicaUsuario> _logicaU;
        private Mock<ILogicaProyecto> _logicaP;
        private IncidentesController _iController;
        private IQueryable<Incidente> incidentesQ;
        private List<Incidente> incidentesL;

        [SetUp]
        public void Setup()
        {
            _logicaI = new Mock<ILogicaIncidente>();
            _logicaU = new Mock<ILogicaUsuario>();
            _logicaP = new Mock<ILogicaProyecto>();
            _iController = new IncidentesController(_logicaI.Object, _logicaU.Object, _logicaP.Object);
            incidentesL = new List<Incidente>();
        }

        [TearDown]
        public void TearDown()
        {
            _logicaI = null;
            _logicaU = null;
            _logicaP = null;
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

            Usuario usu = new Usuario() { };
            Proyecto pro = new Proyecto() { };

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(usu);
            _logicaI.Setup(c => c.Obtener(It.IsAny<int>())).Returns(i);
            _logicaP.Setup(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _logicaI.Setup(c => c.Baja(3));

            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new IncidentesController(_logicaI.Object, _logicaU.Object, _logicaP.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            var result = tested.Delete(i.Id);

            Assert.IsNotNull(result);

            _logicaI.Verify(c => c.Baja(3));
            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
            _logicaI.Verify(c => c.Obtener(It.IsAny<int>()));
            _logicaP.Verify(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
        }

        [Test]
        public void no_se_puede_eliminar_un_incidente_si_usuario_no_pertenece_proyecto()
        {
            Incidente i = new Incidente()
            {
                Id = 3,
                Nombre = "Incidente"
            };
            Usuario usu = new Usuario() { };
            Proyecto pro = new Proyecto() { };

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(usu);
            _logicaI.Setup(c => c.Obtener(It.IsAny<int>())).Returns(i);
            _logicaP.Setup(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(false);

            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new IncidentesController(_logicaI.Object, _logicaU.Object, _logicaP.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            Assert.Throws<ExcepcionAccesoNoAutorizado>(() => tested.Delete(i.Id));

            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
            _logicaI.Verify(c => c.Obtener(It.IsAny<int>()));
            _logicaP.Verify(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
        }
    }
}