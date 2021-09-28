using AutoMapper;
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
    public class ProyectosControllerTest
    {
        private Mock<ILogicaProyecto> _logicaP;
        private Mock<IMapper> _mapper;
        private ProyectosController _pController;
        private IQueryable<Proyecto> proyectosQ;
        private List<Proyecto> proyectosL;

        [SetUp]
        public void Setup()
        {
            _logicaP = new Mock<ILogicaProyecto>();
            _mapper = new Mock<IMapper>();
            _pController = new ProyectosController(_logicaP.Object, _mapper.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _logicaP = null;
            _mapper = null;
            _pController = null;
            proyectosQ = null;
            proyectosL = null;
        }

        [Test]
        public void un_administrador_puede_ver_los_proyectos()
        {
            proyectosL.Add(new Proyecto());
            proyectosQ = proyectosL.AsQueryable();
            _logicaP.Setup(c => c.ObtenerTodos()).Returns(proyectosQ);

            // IEnumerable<Proyecto> resultado = _logicaP;

            // Assert.AreEqual(1, P);
        }
    }
}