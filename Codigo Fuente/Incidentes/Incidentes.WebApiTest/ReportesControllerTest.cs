using Incidentes.Dominio;
using Incidentes.DTOs;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Controllers;
using Incidentes.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Incidentes.WebApiTest
{
    public class ReportesControllerTest
    {
        private Mock<ILogicaProyecto> _logicaP;
        private Mock<ILogicaUsuario> _logicaU;
        private ReportesController _rController;
        private IQueryable<Proyecto> proyectosQ;
        private List<Proyecto> proyectosL;

        [SetUp]
        public void Setup()
        {
            _logicaP = new Mock<ILogicaProyecto>();
            _logicaU = new Mock<ILogicaUsuario>();
            _rController = new ReportesController(_logicaP.Object, _logicaU.Object);
            proyectosL = new List<Proyecto>();
        }

        [TearDown]
        public void TearDown()
        {
            _logicaP = null;
            _rController = null;
            proyectosQ = null;
            proyectosL = null;
        }

        [Test]
        public void se_pueden_ver_el_reporte_de_los_proyectos()
        {
            proyectosL.Add(new Proyecto());
            proyectosQ = proyectosL.AsQueryable();
            _logicaP.Setup(c => c.ObtenerTodos()).Returns(proyectosQ);

            var result = _rController.GetProyectos();
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<ProyectoParaReporteDTO>>(okResult.Value);

            _logicaP.Verify(c => c.ObtenerTodos());
        }

        [Test]
        public void se_pueden_ver_el_reporte_de_los_usuarios()
        {
            Usuario usu = new Usuario();
            _logicaU.Setup(c => c.Obtener(It.IsAny<int>())).Returns(usu);
            _logicaU.Setup(c => c.CantidadDeIncidentesResueltosPorUnDesarrollador(It.IsAny<int>())).Returns(3);

            var result = _rController.GetDesarrollador("1");
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<UsuarioParaReporteDTO>(okResult.Value);

            _logicaU.Verify(c => c.Obtener(It.IsAny<int>()));
            _logicaU.Verify(c => c.CantidadDeIncidentesResueltosPorUnDesarrollador(It.IsAny<int>()));
        }
    }
}
