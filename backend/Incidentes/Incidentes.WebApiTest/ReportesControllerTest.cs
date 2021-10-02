using AutoMapper;
using Incidentes.Dominio;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Controllers;
using Incidentes.WebApi.DTOs;
using Microsoft.AspNetCore.Http;
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
        private Mock<IMapper> _mapper;
        private ReportesController _rController;
        private IQueryable<Proyecto> proyectosQ;
        private List<Proyecto> proyectosL;

        [SetUp]
        public void Setup()
        {
            _logicaP = new Mock<ILogicaProyecto>();
            _mapper = new Mock<IMapper>();
            _rController = new ReportesController(_logicaP.Object, _mapper.Object);
            proyectosL = new List<Proyecto>();
        }

        [TearDown]
        public void TearDown()
        {
            _logicaP = null;
            _mapper = null;
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

            var result = _rController.Get();
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<ProyectoParaReporteDTO>>(okResult.Value);

            _logicaP.Verify(c => c.ObtenerTodos());
        }
    }
}
