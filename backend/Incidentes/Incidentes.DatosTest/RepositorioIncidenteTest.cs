using NUnit.Framework;
using Incidentes.Datos;
using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using System.Linq;

namespace Incidentes.DatosTest
{
    public class RepositorioIncidenteTest : RepositorioBaseTest
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
            Assert.IsInstanceOf(typeof(IRepositorioIncidente), _repoGestores.RepositorioIncidente);
        }

        [Test]
        public void se_puede_buscar_un_incidente_por_nombre()
        {
            Incidente incidente = new Incidente()
            {
                Nombre = "Incidente del test"
            };
            DBContexto.Add(incidente);
            DBContexto.SaveChanges();

            Incidente buscado = _repoGestores.RepositorioIncidente.ObtenerPorCondicion(i => i.Nombre == incidente.Nombre, false).FirstOrDefault();
            Assert.IsNotNull(buscado);
            Assert.AreEqual(incidente.Nombre, buscado.Nombre);
        }

        [Test]
        public void no_se_puede_encontrar_un_incidente_que_no_existe()
        {
            Incidente buscado = _repoGestores.RepositorioIncidente.ObtenerPorCondicion(i => i.Nombre == "Nombre que no existe", false).FirstOrDefault();
            Assert.IsNull(buscado);
        }

        [Test]
        public void se_pueden_obtener_todos()
        {
            Incidente incidente = new Incidente()
            {
                Nombre = "Incidente del test"
            };
            DBContexto.Add(incidente);
            DBContexto.SaveChanges();
            var buscados = _repoGestores.RepositorioIncidente.ObtenerTodos(false);
            Assert.AreEqual(2, buscados.Count());
        }

        [Test]
        public void existe_debe_devolver_true_si_existe()
        {
            Incidente i = new Incidente()
            {
                Nombre = "Proyecto del test"
            };
            DBContexto.Add(i);
            DBContexto.SaveChanges();

            bool existe = _repoGestores.RepositorioIncidente.Existe(inc => inc.Nombre == i.Nombre);
            Assert.IsTrue(existe);
        }

        [Test]
        public void existe_debe_devolver_false_si_no_existe()
        {
            bool existe = _repoGestores.RepositorioIncidente.Existe(i => i.Nombre == "nombre que no existe");
            Assert.IsFalse(existe);
        }
    }
}