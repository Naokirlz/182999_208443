using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Incidentes.Datos;
using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using System.Linq;

namespace Incidentes.DatosTest
{
    public class RepositorioTareaTest : RepositorioBaseTest
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
            Assert.IsInstanceOf(typeof(IRepositorioTarea), _repoGestores.RepositorioTarea);
        }

        [Test]
        public void se_puede_buscar_una_tarea_por_nombre()
        {
            Tarea tarea = new Tarea()
            {
                Nombre = "Tarea del test"
            };
            DBContexto.Add(tarea);
            DBContexto.SaveChanges();

            Tarea buscado = _repoGestores.RepositorioTarea.ObtenerPorCondicion(t => t.Nombre == tarea.Nombre, false).FirstOrDefault();
            Assert.IsNotNull(buscado);
            Assert.AreEqual(tarea.Nombre, buscado.Nombre);
        }

        [Test]
        public void no_se_puede_encontrar_una_tarea_que_no_existe()
        {
            Tarea buscado = _repoGestores.RepositorioTarea.ObtenerPorCondicion(t => t.Nombre == "Nombre que no existe", false).FirstOrDefault();
            Assert.IsNull(buscado);
        }

        [Test]
        public void se_pueden_obtener_todos()
        {
            Tarea tarea = new Tarea()
            {
                Nombre = "Tarea del test"
            };
            DBContexto.Add(tarea);
            DBContexto.SaveChanges();
            var buscados = _repoGestores.RepositorioTarea.ObtenerTodos(false);
            Assert.AreEqual(3, buscados.Count());
        }

        [Test]
        public void existe_debe_devolver_true_si_existe()
        {
            Tarea t = new Tarea()
            {
                Nombre = "Proyecto del test"
            };
            DBContexto.Add(t);
            DBContexto.SaveChanges();

            bool existe = _repoGestores.RepositorioTarea.Existe(tar => tar.Nombre == t.Nombre);
            Assert.AreEqual("Proyecto del test", t.Nombre);
            Assert.IsTrue(existe);
        }

        [Test]
        public void existe_debe_devolver_false_si_no_existe()
        {
            bool existe = _repoGestores.RepositorioTarea.Existe(i => i.Nombre == "nombre que no existe");
            Assert.IsFalse(existe);
        }
    }
}