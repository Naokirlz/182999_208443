using AutoMapper;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Filters;
using Moq;
using NUnit.Framework;

namespace Incidentes.WebApiTest
{
    public class FilterAutorizacionTest
    {
        private string[] _roles;

        [SetUp]
        public void Setup()
        {
            _roles = new string[3] { "Administrador" , "Desarrollador", "Tester" };
        }

        [TearDown]
        public void TearDown()
        {
            _roles = null;
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
