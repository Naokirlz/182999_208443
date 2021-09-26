using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using Incidentes.Logica.Interfaz;
using Moq;
using NUnit.Framework;

namespace Incidentes.Logica.Test
{
    public class GestorUsuarioTest
    {
        private Usuario usuarioCompleto;

        [SetUp]
        public void Setup()
        {
            this.usuarioCompleto = new Administrador() { 
                Nombre = "Martin",
                Apellido = "Cosa",
                Contrasenia = "Casa#Blanca",
                Email = "martin@gmail.com",
                Id = 1,
                NombreUsuario = "martincosa",
                Token = ""
            };
        }

        [Test]
        public void se_puede_guardar_administrador()
        {
            Administrador administrador = new Administrador()
            {
                Nombre = "Luisito"
            };    

            Mock<IRepositorioGestores> repoGestores = new Mock<IRepositorioGestores>();

            repoGestores.Setup(c => c.RepositorioUsuario.Alta(administrador));

            GestorUsuario gestor = new GestorUsuario(repoGestores.Object);

            Usuario admin = gestor.Alta(administrador);

            Assert.AreEqual(administrador.Nombre, admin.Nombre);
            repoGestores.Verify(c => c.RepositorioUsuario.Alta(administrador));
        }

        [Test]
        public void se_puede_guardar_tester()
        {
            Tester tester1 = new Tester()
            {
                Nombre = "Luisito"
            };

            Mock<IRepositorioGestores> repoGestores = new Mock<IRepositorioGestores>();

            repoGestores.Setup(c => c.RepositorioUsuario.Alta(tester1));

            GestorUsuario gestor = new GestorUsuario(repoGestores.Object);

            Usuario tester = gestor.Alta(tester1);

            Assert.AreEqual(tester1.Nombre, tester.Nombre);
            repoGestores.Verify(c => c.RepositorioUsuario.Alta(tester1));
        }

        [Test]
        public void se_puede_guardar_desarrollador()
        {
            Desarrollador desarrollador1 = new Desarrollador()
            {
                Nombre = "Luisito"
            };

            Mock<IRepositorioGestores> repoGestores = new Mock<IRepositorioGestores>();

            repoGestores.Setup(c => c.RepositorioUsuario.Alta(desarrollador1));

            GestorUsuario gestor = new GestorUsuario(repoGestores.Object);

            Usuario desarrollador = gestor.Alta(desarrollador1);

            Assert.AreEqual(desarrollador1.Nombre, desarrollador.Nombre);
            repoGestores.Verify(c => c.RepositorioUsuario.Alta(desarrollador1));
        }


        /*************************************************************************************
         *  FUNCIONES
         * ************************************************************************************/
        [Test]
        public void un_usuario_se_puede_loguear()
        {
            /*Mock<IRepositorioGestores> repoGestores = new Mock<IRepositorioGestores>();

            repoGestores.Setup(c => c.RepositorioUsuario.Crear(this.usuarioCompleto));

            GestorUsuario gestor = new GestorUsuario(repoGestores.Object);

            Usuario desarrollador = gestor.Alta(desarrollador1);

            Assert.AreEqual(desarrollador1.Nombre, desarrollador.Nombre);
            repoGestores.Verify(c => c.RepositorioUsuario.Crear(desarrollador1));*/
        }
    }
}