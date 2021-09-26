using NUnit.Framework;
using Incidentes.Datos;
using Incidentes.DatosFabrica;
using Incidentes.DatosInterfaz;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Incidentes.DatosTest
{
    public class RepositorioAdministradorTest
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void correcto_tipo_de_repositorio()
        {
            Contexto contexto = new Contexto();

            IRepositorioGestores repoGestores = new RepositorioGestores(contexto);
            Assert.IsInstanceOf(typeof(IRepositorioAdministrador), repoGestores.RepositorioAdministrador);
        }
    }
}