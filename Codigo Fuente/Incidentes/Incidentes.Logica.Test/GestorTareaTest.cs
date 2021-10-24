using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using Incidentes.Logica.Excepciones;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Incidentes.Logica.Test
{
    public class GestorTareaTest
    {
        private Tarea tareaCompleta;
        Mock<IRepositorioGestores> repoGestores;
        GestorTarea gestor;

        [SetUp]
        public void Setup()
        {
            this.tareaCompleta = new Tarea()
            {
                Nombre = "Tarea 1",
                Costo = 3,
                Duracion = 5,
                Id = 9
            };

            repoGestores = new Mock<IRepositorioGestores>();
            gestor = new GestorTarea(repoGestores.Object);
        }

        [TearDown]
        public void TearDown()
        {
            this.tareaCompleta = null;
            repoGestores = null;
            gestor = null;
        }

        [Test]
        public void se_puede_guardar_una_tarea()
        {
            Tarea tar = new Tarea()
            {
                Nombre = "Tarea X",
                Costo = 2,
                Duracion = 5
            };

            repoGestores.Setup(c => c.RepositorioTarea.Alta(tar));

            Tarea tarea = gestor.Alta(tar);

            Assert.AreEqual(tar.Nombre, tarea.Nombre);
            repoGestores.Verify(c => c.RepositorioTarea.Alta(tar));
        }

        [Test]
        public void se_pueden_obtener_las_tareas()
        {
            List<Tarea> lista = new List<Tarea>();
            lista.Add(tareaCompleta);
            IQueryable<Tarea> queryableTarea = lista.AsQueryable();

            repoGestores.Setup(c => c.RepositorioTarea.ObtenerTodos(false)).Returns(queryableTarea);

            List<Tarea> listaT = gestor.ObtenerTodos().ToList();

            Assert.AreEqual(1, listaT.Count());
            repoGestores.Verify(c => c.RepositorioTarea.ObtenerTodos(false));
        }

        [Test]
        public void se_puede_obtener_una_tarea_por_id()
        {
            List<Tarea> lista = new List<Tarea>();
            lista.Add(tareaCompleta);
            IQueryable<Tarea> queryableTareas = lista.AsQueryable();

            repoGestores.Setup(c => c.RepositorioTarea.ObtenerPorCondicion(It.IsAny<Expression<Func<Tarea, bool>>>(), true)).Returns(queryableTareas);
            repoGestores.Setup(c => c.RepositorioTarea.Existe(It.IsAny<Expression<Func<Tarea, bool>>>())).Returns(true);

            Tarea tar = gestor.Obtener(3);

            Assert.AreEqual(tareaCompleta.Nombre, tar.Nombre);
            repoGestores.Verify(c => c.RepositorioTarea.ObtenerPorCondicion(It.IsAny<Expression<Func<Tarea, bool>>>(), true));
            repoGestores.Verify(c => c.RepositorioTarea.Existe(It.IsAny<Expression<Func<Tarea, bool>>>()));
        }

        [Test]
        public void no_se_puede_obtener_una_tarea_que_no_existe()
        {
            repoGestores.Setup(c => c.RepositorioTarea.Existe(It.IsAny<Expression<Func<Tarea, bool>>>())).Returns(false);

            Assert.Throws<ExcepcionElementoNoExiste>(() => gestor.Obtener(3));

            repoGestores.Verify(c => c.RepositorioTarea.Existe(It.IsAny<Expression<Func<Tarea, bool>>>()));
        }

        [Test]
        public void no_se_puede_dar_alta_una_tarea_nulo()
        {
            Assert.Throws<ExcepcionArgumentoNoValido>(() => gestor.Alta(null));
        }

        [Test]
        public void no_se_puede_guardar_una_tarea_con_nombre_corto()
        {
            repoGestores.Setup(c => c.RepositorioTarea.Existe(It.IsAny<Expression<Func<Tarea, bool>>>())).Returns(false);
            tareaCompleta.Nombre = "ss s";
            Assert.Throws<ExcepcionLargoTexto>(() => gestor.Alta(tareaCompleta));
            repoGestores.Verify(c => c.RepositorioTarea.Existe(It.IsAny<Expression<Func<Tarea, bool>>>()));
        }

        [Test]
        public void no_se_puede_guardar_una_tarea_con_nombre_largo()
        {
            repoGestores.Setup(c => c.RepositorioTarea.Existe(It.IsAny<Expression<Func<Tarea, bool>>>())).Returns(false);
            tareaCompleta.Nombre = "01234567890123456789012345";
            Assert.Throws<ExcepcionLargoTexto>(() => gestor.Alta(tareaCompleta));
            repoGestores.Verify(c => c.RepositorioTarea.Existe(It.IsAny<Expression<Func<Tarea, bool>>>()));
        }

        [Test]
        public void no_se_puede_guardar_una_tarea_con_costo_menor_a_cero()
        {
            repoGestores.Setup(c => c.RepositorioTarea.Existe(It.IsAny<Expression<Func<Tarea, bool>>>())).Returns(false);
            tareaCompleta.Costo = -52;
            Assert.Throws<ExcepcionArgumentoNoValido>(() => gestor.Alta(tareaCompleta));
            repoGestores.Verify(c => c.RepositorioTarea.Existe(It.IsAny<Expression<Func<Tarea, bool>>>()));
        }

        [Test]
        public void no_se_puede_guardar_una_tarea_con_duracion_menor_a_cero()
        {
            repoGestores.Setup(c => c.RepositorioTarea.Existe(It.IsAny<Expression<Func<Tarea, bool>>>())).Returns(false);
            tareaCompleta.Duracion = -52;
            Assert.Throws<ExcepcionArgumentoNoValido>(() => gestor.Alta(tareaCompleta));
            repoGestores.Verify(c => c.RepositorioTarea.Existe(It.IsAny<Expression<Func<Tarea, bool>>>()));
        }
    }
}