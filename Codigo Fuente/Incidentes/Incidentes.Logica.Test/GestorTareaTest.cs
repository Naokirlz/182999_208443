using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using Incidentes.DTOs;
using Incidentes.Excepciones;
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
        private TareaDTO tareaDTOCompleta;
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
                ProyectoId = 9,
                Id = 9
            };
            tareaDTOCompleta = new TareaDTO(tareaCompleta);
            repoGestores = new Mock<IRepositorioGestores>();
            gestor = new GestorTarea(repoGestores.Object);
        }

        [TearDown]
        public void TearDown()
        {
            this.tareaCompleta = null;
            this.tareaDTOCompleta = null;
            repoGestores = null;
            gestor = null;
        }

        [Test]
        public void se_puede_guardar_una_tarea()
        {
            TareaDTO tar = new TareaDTO()
            {
                Nombre = "Tarea X",
                Costo = 2,
                Duracion = 5
            };

            repoGestores.Setup(c => c.RepositorioTarea.Alta(It.IsAny<Tarea>()));

            TareaDTO tarea = gestor.Alta(tar);

            Assert.AreEqual(tar.Nombre, tarea.Nombre);
            repoGestores.Verify(c => c.RepositorioTarea.Alta(It.IsAny<Tarea>()));
        }

        [Test]
        public void se_pueden_obtener_las_tareas()
        {
            List<Tarea> lista = new List<Tarea>();
            lista.Add(tareaCompleta);
            IQueryable<Tarea> queryableTarea = lista.AsQueryable();

            repoGestores.Setup(c => c.RepositorioTarea.ObtenerTodos(false)).Returns(queryableTarea);

            List<TareaDTO> listaT = gestor.ObtenerTodos().ToList();

            Assert.AreEqual(1, listaT.Count());
            repoGestores.Verify(c => c.RepositorioTarea.ObtenerTodos(false));
        }

        [Test]
        public void se_puede_obtener_una_tarea_por_id()
        {
            List<Tarea> lista = new List<Tarea>();
            lista.Add(tareaCompleta);
            IQueryable<Tarea> queryableTareas = lista.AsQueryable();

            repoGestores.Setup(c => c.RepositorioTarea.ObtenerPorCondicion(It.IsAny<Expression<Func<Tarea, bool>>>(), false)).Returns(queryableTareas);
            repoGestores.Setup(c => c.RepositorioTarea.Existe(It.IsAny<Expression<Func<Tarea, bool>>>())).Returns(true);

            TareaDTO tar = gestor.Obtener(3);

            Assert.AreEqual(tareaCompleta.Nombre, tar.Nombre);
            repoGestores.Verify(c => c.RepositorioTarea.ObtenerPorCondicion(It.IsAny<Expression<Func<Tarea, bool>>>(), false));
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
            tareaDTOCompleta.Nombre = "ss s";
            Assert.Throws<ExcepcionLargoTexto>(() => gestor.Alta(tareaDTOCompleta));
        }

        [Test]
        public void no_se_puede_guardar_una_tarea_con_nombre_largo()
        {
            tareaDTOCompleta.Nombre = "01234567890123456789012345";
            Assert.Throws<ExcepcionLargoTexto>(() => gestor.Alta(tareaDTOCompleta));
        }

        [Test]
        public void no_se_puede_guardar_una_tarea_con_costo_menor_a_cero()
        {
            tareaDTOCompleta.Costo = -52;
            Assert.Throws<ExcepcionArgumentoNoValido>(() => gestor.Alta(tareaDTOCompleta));
        }

        [Test]
        public void no_se_puede_guardar_una_tarea_con_duracion_menor_a_cero()
        {
            tareaDTOCompleta.Duracion = -52;
            Assert.Throws<ExcepcionArgumentoNoValido>(() => gestor.Alta(tareaDTOCompleta));
        }

        [Test]
        public void se_puede_eliminar_una_tarea()
        {
            List<Tarea> lista = new List<Tarea>();
            lista.Add(tareaCompleta);
            IQueryable<Tarea> queryableT = lista.AsQueryable();

            repoGestores.Setup(c => c.RepositorioTarea.ObtenerPorCondicion(It.IsAny<Expression<Func<Tarea, bool>>>(), false)).Returns(queryableT);
            repoGestores.Setup(c => c.RepositorioTarea.Existe(It.IsAny<Expression<Func<Tarea, bool>>>())).Returns(true);
            repoGestores.Setup(c => c.RepositorioTarea.Eliminar(It.IsAny <Tarea>()));

            gestor.Baja(5);
            Assert.Pass();

            repoGestores.Verify(c => c.RepositorioTarea.Existe(It.IsAny<Expression<Func<Tarea, bool>>>()));
            repoGestores.Verify(c => c.RepositorioTarea.ObtenerPorCondicion(It.IsAny<Expression<Func<Tarea, bool>>>(), false));
            repoGestores.Verify(c => c.RepositorioTarea.Eliminar(It.IsAny<Tarea>()));
        }

        [Test]
        public void no_se_puede_modificar_una_tarea_que_no_existe()
        {
            repoGestores.Setup(c => c.RepositorioTarea.Existe(It.IsAny<Expression<Func<Tarea, bool>>>())).Returns(false);

            Assert.Throws<ExcepcionElementoNoExiste>(() => gestor.Modificar(20, new TareaDTO()));

            repoGestores.Verify(c => c.RepositorioTarea.Existe(It.IsAny<Expression<Func<Tarea, bool>>>()));
        }

        [Test]
        public void no_se_puede_eliminar_una_tarea_que_no_existe()
        {
            repoGestores.Setup(c => c.RepositorioTarea.Existe(It.IsAny<Expression<Func<Tarea, bool>>>())).Returns(false);

            Assert.Throws<ExcepcionElementoNoExiste>(() => gestor.Baja(20));

            repoGestores.Verify(c => c.RepositorioTarea.Existe(It.IsAny<Expression<Func<Tarea, bool>>>()));
        }


        [Test]
        public void se_puede_modificar_una_tarea()
        {
            List<Tarea> lista = new List<Tarea>();
            lista.Add(tareaCompleta);
            IQueryable<Tarea> queryableT = lista.AsQueryable();

            repoGestores.Setup(c => c.RepositorioTarea.ObtenerPorCondicion(It.IsAny<Expression<Func<Tarea, bool>>>(), false)).Returns(queryableT);
            repoGestores.Setup(c => c.RepositorioTarea.Existe(It.IsAny<Expression<Func<Tarea, bool>>>())).Returns(true);
            repoGestores.Setup(c => c.RepositorioTarea.Modificar(It.IsAny<Tarea>()));
            Tarea modificada = new Tarea()
            {
                Nombre = "Tarea modificada",
                Costo = 5,
                Duracion = 2,
                ProyectoId = 5,
                Id = 9
            };
            TareaDTO modificado = gestor.Modificar(9, tareaDTOCompleta);

            Assert.AreEqual(tareaCompleta.Nombre, modificado.Nombre);

            repoGestores.Verify(c => c.RepositorioTarea.Existe(It.IsAny<Expression<Func<Tarea, bool>>>()));
            repoGestores.Verify(c => c.RepositorioTarea.ObtenerPorCondicion(It.IsAny<Expression<Func<Tarea, bool>>>(), false));
            repoGestores.Verify(c => c.RepositorioTarea.Modificar(It.IsAny<Tarea>()));
        }

        [Test]
        public void no_se_puede_modificar_una_tarea_nula()
        {
            Assert.Throws<ExcepcionArgumentoNoValido>(() => gestor.Modificar(20, null));
        }

        [Test]
        public void se_puede_ver_la_lista_de_tareas_de_los_proyectos_que_pertenece()
        {
            List<Tarea> lista = new List<Tarea>();
            lista.Add(new Tarea());

            List<Usuario> listaU = new List<Usuario>();
            listaU.Add(new Usuario());
            IQueryable<Usuario> queryableU = listaU.AsQueryable();

            repoGestores.Setup(
                c => c.RepositorioUsuario
                .ListaDeTareasDeProyectosALosQuePertenece(It.IsAny<int>()))
                .Returns(lista);

            IEnumerable<TareaDTO> tareas = gestor.ListaDeTareasDeProyectosALosQuePertenece(5);

            Assert.AreEqual(1, lista.Count());
            repoGestores.Verify(
                c => c.RepositorioUsuario
                .ListaDeTareasDeProyectosALosQuePertenece(It.IsAny<int>()));
        }
    }
}