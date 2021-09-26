using NUnit.Framework;
using Incidentes.Dominio;

namespace Incidentes.Dominio.Test
{
    public class ProyectosTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void se_puede_dar_nombre_a_un_proyecto()
        {
            Proyecto unProyecto = new Proyecto()
            {
                Nombre = "Trabajo conjunto"
            };
            Assert.AreEqual("Trabajo conjunto", unProyecto.Nombre);
        }
    }
}
