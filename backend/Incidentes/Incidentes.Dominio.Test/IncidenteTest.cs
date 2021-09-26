using NUnit.Framework;
using Incidentes.Dominio;

namespace Incidentes.Dominio.Test
{
    public class IncidenteTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void se_puede_dar_nombre_a_un_incidente()
        {
            Incidente unIncidente = new Incidente()
            {
                Nombre = "Incidente complicado"
            };
            Assert.AreEqual("Incidente complicado", unIncidente.Nombre);
        }
    }
}
