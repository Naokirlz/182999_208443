using NUnit.Framework;
using Incidentes.Datos;
using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using System.Linq;

namespace Incidentes.DatosTest
{
    public class RepositorioProyectoTest : RepositorioBaseTest
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
            Assert.IsInstanceOf(typeof(IRepositorioProyecto), _repoGestores.RepositorioProyecto);
        }

        [Test]
        public void se_puede_buscar_un_proyecto_por_nombre()
        {
            Proyecto pro = new Proyecto()
            {
                Nombre = "Proyecto del test"
            };
            DBContexto.Add(pro);
            DBContexto.SaveChanges();

            Proyecto buscado = _repoGestores.RepositorioProyecto.ObtenerPorCondicion(p => p.Nombre == pro.Nombre, false).FirstOrDefault();
            Assert.IsNotNull(buscado);
            Assert.AreEqual(pro.Nombre, buscado.Nombre);
        }

        [Test]
        public void no_se_puede_encontrar_un_proyecto_que_no_existe()
        {
            Proyecto buscado = _repoGestores.RepositorioProyecto.ObtenerPorCondicion(p => p.Nombre == "Nombre que no existe", false).FirstOrDefault();
            Assert.IsNull(buscado);
        }

        [Test]
        public void se_pueden_obtener_todos()
        {
            var buscados = _repoGestores.RepositorioProyecto.ObtenerTodos(false);
            Assert.AreEqual(2, buscados.Count());
        }

        [Test]
        public void existe_debe_devolver_true_si_existe()
        {
            Proyecto p = new Proyecto()
            {
                Nombre = "Proyecto del test"
            };
            DBContexto.Add(p);
            DBContexto.SaveChanges();

            bool existe = _repoGestores.RepositorioProyecto.Existe(pro => pro.Nombre == p.Nombre);
            Assert.IsTrue(existe);
        }

        [Test]
        public void existe_debe_devolver_false_si_no_existe()
        {
            bool existe = _repoGestores.RepositorioProyecto.Existe(p => p.Nombre == "nombre que no existe");
            Assert.IsFalse(existe);
        }
    }
}