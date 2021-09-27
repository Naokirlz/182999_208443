using NUnit.Framework;
using Incidentes.Datos;
using Incidentes.DatosFabrica;
using Incidentes.DatosInterfaz;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Incidentes.Dominio;
using System.Linq;

namespace Incidentes.DatosTest
{
    public class RepositorioUsuarioTest : RepositorioBaseTest
    {
        IRepositorioGestores _repoGestores;
        [SetUp]
        public void Setup()
        {
            SetearBaseDeDatos();
            _repoGestores = new RepositorioGestores(DBContexto);
        }

        [TearDown]
        public void TearDown()
        {
            LimpiarBD();
            _repoGestores = null;
        }

        [Test]
        public void correcto_tipo_de_repositorio()
        {
            Assert.IsInstanceOf(typeof(IRepositorioUsuario), _repoGestores.RepositorioUsuario);
        }

        [Test]
        public void se_puede_buscar_un_usuario_por_nombreusuario()
        {
            Usuario a2 = new Desarrollador()
            {
                Nombre = "Martin",
                Apellido = "Cosa",
                Contrasenia = "Casa#Blanca",
                Email = "martinf@gmail.com",
                NombreUsuario = "martincosaf",
                Token = ""
            };
            DBContexto.Add(a2);
            DBContexto.SaveChanges();
          
            Usuario buscado = _repoGestores.RepositorioUsuario.ObtenerPorCondicion(u => u.NombreUsuario == a2.NombreUsuario, false).FirstOrDefault();
            Assert.IsNotNull(buscado);
        }

        [Test]
        public void no_se_puede_encontrar_un_usuario_que_no_existe()
        {
            Usuario buscado = _repoGestores.RepositorioUsuario.ObtenerPorCondicion(u => u.NombreUsuario == "Nombre que no existe", false).FirstOrDefault();
            Assert.IsNull(buscado);
        }

        [Test]
        public void se_pueden_obtener_todos()
        {
            var buscados = _repoGestores.RepositorioUsuario.ObtenerTodos(false);
            Assert.AreEqual(4, buscados.Count());
        }

        [Test]
        public void existe_debe_devolver_true_si_existe()
        {
            Usuario d3 = new Desarrollador()
            {
                Nombre = "Martin",
                Apellido = "Cosa",
                Contrasenia = "Casa#Blanca",
                Email = "martinf@gmail.com",
                NombreUsuario = "martincosaf",
                Token = ""
            };
            DBContexto.Add(d3);
            DBContexto.SaveChanges();

            bool existe = _repoGestores.RepositorioUsuario.Existe(u => u.NombreUsuario == d3.NombreUsuario);
            Assert.IsTrue(existe);
        }

        [Test]
        public void existe_debe_devolver_false_si_no_existe()
        {
            bool existe = _repoGestores.RepositorioUsuario.Existe(u => u.NombreUsuario == "nombre que no existe");
            Assert.IsFalse(existe);
        }
    }
}