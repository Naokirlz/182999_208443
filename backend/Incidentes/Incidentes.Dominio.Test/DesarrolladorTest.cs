using NUnit.Framework;
using Incidentes.Dominio;


namespace Incidentes.Dominio.Test
{
    public class DesarrolladorTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void se_puede_dar_nombre_a_un_desarrollador()
        {
            Desarrollador unUsuario = new Desarrollador()
            {
                Nombre = "Luisito"
            };
            Assert.AreEqual("Luisito", unUsuario.Nombre);
        }

        [Test]
        public void se_puede_dar_nombreUsuario_a_un_desarrollador()
        {
            Desarrollador unUsuario = new Desarrollador()
            {
                NombreUsuario = "Luis123"
            };
            Assert.AreEqual("Luis123", unUsuario.NombreUsuario);
        }

        [Test]
        public void se_puede_dar_apellido_a_un_desarrollador()
        {
            Desarrollador unUsuario = new Desarrollador()
            {
                Apellido = "Perez"
            };
            Assert.AreEqual("Perez", unUsuario.Apellido);
        }

        [Test]
        public void se_puede_dar_contrasenia_a_un_desarrollador()
        {
            Desarrollador unUsuario = new Desarrollador()
            {
                Contrasenia = "ABC123"
            };
            Assert.AreEqual("ABC123", unUsuario.Contrasenia);
        }

        [Test]
        public void se_puede_dar_email_a_un_desarrollador()
        {
            Desarrollador unUsuario = new Desarrollador()
            {
                Email = "luis@gmail.com"
            };
            Assert.AreEqual("luis@gmail.com", unUsuario.Email);
        }


    }
}
