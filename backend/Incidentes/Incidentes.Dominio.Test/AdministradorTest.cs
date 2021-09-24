using NUnit.Framework;
using Incidentes.Dominio;

namespace Incidentes.Dominio.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void se_puede_dar_nombre_a_un_administrador()
        {
            Administrador unAdministrador = new Administrador()
            {
                Nombre = "Luisito"
            };
            Assert.AreEqual("Luisito", unAdministrador.Nombre);
        }
    }
}