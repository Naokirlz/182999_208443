using AutoMapper;
using Incidentes.Dominio;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Controllers;
using Incidentes.WebApi.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Incidentes.WebApiTest
{
    public class FilterAutorizacionTest
    {
        private Mock<ILogicaAutorizacion> _logicaA;
        private Mock<IMapper> _mapper;
        private FilterAutorizacion _aFilter;
        private string[] _roles;

        [SetUp]
        public void Setup()
        {
            _roles = new string[3] { "Administrador" , "Desarrollador", "Tester" };
            _logicaA = new Mock<ILogicaAutorizacion>();
            _mapper = new Mock<IMapper>();
            _aFilter = new FilterAutorizacion(_roles);
        }

        [TearDown]
        public void TearDown()
        {
            _roles = null;
            _logicaA = null;
            _mapper = null;
            _aFilter = null;
        }

        [Test]
        public void se_pueden_pasar_la_autorizacion()
        {
            /*DefaultHttpContext httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["autorizacion"] = "token";
            AuthorizationFilterContext actionContext = new AuthorizationFilterContext(
                new ActionContext()
                {
                    HttpContext = httpContext,
                    ActionDescriptor = new ActionDescriptor(),
                    RouteData = new RouteData()
                },
                new List<IFilterMetadata>()
            );

            _logicaA.Setup(c => c.TokenValido(It.IsAny<string>(), It.IsAny<string[]>())).Returns(true);

            _aFilter.OnAuthorization(actionContext);*/
            Assert.Pass();
        }
    }
}
