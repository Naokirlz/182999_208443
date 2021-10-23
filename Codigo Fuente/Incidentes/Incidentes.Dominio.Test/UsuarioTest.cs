using NUnit.Framework;
using Incidentes.Dominio;

namespace Incidentes.Dominio.Test
{
    public class UsuarioTest

    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void se_puede_dar_nombre_a_un_administrador()
        {
            Usuario unUsuario = new Usuario()
            {
                Nombre = "Luisito"
            };
            Assert.AreEqual("Luisito", unUsuario.Nombre);
        }

        [Test]
        public void se_puede_dar_nombreUsuario_a_un_administrador()
        {
            Usuario unUsuario = new Usuario()
            {
                NombreUsuario = "Luis123"
            };
            Assert.AreEqual("Luis123", unUsuario.NombreUsuario);
        }

        [Test]
        public void se_puede_dar_apellido_a_un_administrador()
        {
            Usuario unUsuario = new Usuario()
            {
                Apellido = "Perez"
            };
            Assert.AreEqual("Perez", unUsuario.Apellido);
        }

        [Test]
        public void se_puede_dar_contrasenia_a_un_administrador()
        {
            Usuario unUsuario = new Usuario()
            {
                Contrasenia = "ABC123"
            };
            Assert.AreEqual("ABC123", unUsuario.Contrasenia);
        }

        [Test]
        public void se_puede_dar_valor_hora_a_los_usuarios()
        {
            Usuario unUsuario = new Usuario()
            {
                ValorHora = 15
            };
            Assert.AreEqual(15, unUsuario.ValorHora);
        }

        [Test]
        public void se_puede_dar_email_a_un_administrador()
        {
            Usuario unUsuario = new Usuario()
            {
                Email = "luis@gmail.com"
            };
            Assert.AreEqual("luis@gmail.com", unUsuario.Email);
        }

        [Test]
        public void se_puede_dar_token_a_un_administrador()
        {
            Usuario unUsuario = new Usuario()
            {
                Token = "lusadqwiiiashzxytafwfqwe"
            };
            Assert.AreEqual("lusadqwiiiashzxytafwfqwe", unUsuario.Token);
        }
    }
}