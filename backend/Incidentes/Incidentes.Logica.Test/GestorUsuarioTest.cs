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
    public class GestorUsuarioTest
    {
        private Usuario usuarioCompleto;
        Mock<IRepositorioGestores> repoGestores;
        GestorUsuario gestor;

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

            repoGestores = new Mock<IRepositorioGestores>();
            gestor = new GestorUsuario(repoGestores.Object);
        }

        [TearDown]
        public void TearDown()
        {
            this.usuarioCompleto = null;
            repoGestores = null;
            gestor = null;
        }

        [Test]
        public void se_puede_guardar_administrador()
        {
            Administrador administrador = new Administrador()
            {
                Nombre = "Luisito"
            };    

            repoGestores.Setup(c => c.RepositorioUsuario.Alta(administrador));

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

            repoGestores.Setup(c => c.RepositorioUsuario.Alta(tester1));

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

            repoGestores.Setup(c => c.RepositorioUsuario.Alta(desarrollador1));

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
            List<Usuario> lista = new List<Usuario>();
            lista.Add(usuarioCompleto);
            IQueryable<Usuario> queryableUsuarios = lista.AsQueryable();
            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false))
                .Returns(queryableUsuarios);

            bool loginCorrecto = gestor.Login(this.usuarioCompleto.NombreUsuario, queryableUsuarios.FirstOrDefault().Contrasenia);

            Assert.IsTrue(loginCorrecto);
            repoGestores.Verify(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false));
        }

        [Test]
        public void un_usuario_no_se_puede_loguear_con_contrasenia_incorrecta()
        {
            List<Usuario> lista = new List<Usuario>();
            lista.Add(usuarioCompleto);
            IQueryable<Usuario> queryableUsuarios = lista.AsQueryable();
            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false))
                .Returns(queryableUsuarios);

            bool loginCorrecto = gestor.Login(this.usuarioCompleto.NombreUsuario, "password incorrecto");

            Assert.IsFalse(loginCorrecto);
            repoGestores.Verify(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false));
        }

        [Test]
        public void un_usuario_al_loguearse_recibe_un_token()
        {
            List<Usuario> lista = new List<Usuario>();
            lista.Add(usuarioCompleto);
            IQueryable<Usuario> queryableUsuarios = lista.AsQueryable();
            List<Usuario> listaVacia = new List<Usuario>();
            IQueryable<Usuario> queryableUsuariosVacia = listaVacia.AsQueryable();

            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false))
                .Returns(queryableUsuarios);
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);
            repoGestores.Setup(c => c.RepositorioUsuario.Modificar(It.IsAny<Usuario>()));

            bool loginCorrecto = gestor.Login(this.usuarioCompleto.NombreUsuario, queryableUsuarios.FirstOrDefault().Contrasenia);

            Assert.IsNotEmpty(usuarioCompleto.Token);
            repoGestores.Verify(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
            repoGestores.Verify(c => c.RepositorioUsuario.Modificar(It.IsAny<Usuario>()));
        }

        [Test]
        public void un_usuario_logueado_se_puede_desloguear()
        {
            usuarioCompleto.Token = "asdasdasdasdasdasdasd";

            List<Usuario> lista = new List<Usuario>();
            lista.Add(usuarioCompleto);
            IQueryable<Usuario> queryableUsuarios = lista.AsQueryable();
            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false))
                .Returns(queryableUsuarios);

            gestor.Logout(usuarioCompleto.Token);

            Assert.IsEmpty(usuarioCompleto.Token);
            repoGestores.Verify(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false));
        }

        [Test]
        public void un_administrador_logueado_puede_dar_de_alta_un_desarrollador()
        {
            usuarioCompleto.Token = "asdasdasdasdasdasdasd";

            List<Usuario> lista = new List<Usuario>();
            lista.Add(usuarioCompleto);
            IQueryable<Usuario> queryableUsuarios = lista.AsQueryable();
            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false))
                .Returns(queryableUsuarios);
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(true);

            Usuario unDesarrollador = new Desarrollador()
            {
                Nombre = "Martin",
                Apellido = "Cosa",
                Contrasenia = "Casa#Blanca",
                Email = "martinDes@gmail.com",
                Id = 2,
                NombreUsuario = "martincosadesarrollador",
                Token = ""
            };

            repoGestores.Setup(c => c.RepositorioUsuario.Existe(u => u.NombreUsuario == unDesarrollador.NombreUsuario)).Returns(false);

            gestor.AltaDesarrollador(unDesarrollador);

            List<Usuario> lista2 = new List<Usuario>();
            lista2.Add(unDesarrollador);
            IQueryable<Usuario> queryableUsuarios2 = lista2.AsQueryable();

            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerPorCondicion(a => a.Id == 2, false)).Returns(queryableUsuarios2);

            Usuario desarrollador = gestor.Obtener(unDesarrollador.Id);

            Assert.IsNotNull(desarrollador);
            repoGestores.Verify(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(u => u.NombreUsuario == unDesarrollador.NombreUsuario));
            repoGestores.Verify(c => c.RepositorioUsuario.ObtenerPorCondicion(a => a.Id == 2, false));
        }

        [Test]
        public void un_administrador_puede_ver_cantidad_de_bugs_resueltos_por_un_desarrollador()
        {
            List<Usuario> lista = new List<Usuario>();
            lista.Add(usuarioCompleto);
            IQueryable<Usuario> queryableUsuarios = lista.AsQueryable();

            repoGestores.Setup(c => c.RepositorioUsuario.CantidadDeIncidentesResueltosPorUnDesarrollador(It.IsAny<int>())).Returns(5);

            int incidentes = gestor.CantidadDeIncidentesResueltosPorUnDesarrollador(3);

            Assert.AreEqual(5, incidentes);
            repoGestores.Verify(c => c.RepositorioUsuario.CantidadDeIncidentesResueltosPorUnDesarrollador(It.IsAny<int>()));
        }

        [Test]
        public void se_puede_ver_la_lista_de_bugs_de_los_proyectos_que_pertenece()
        {
            List<Incidente> lista = new List<Incidente>();
            lista.Add(new Incidente());

            List<Usuario> listaU = new List<Usuario>();
            listaU.Add(new Tester());
            IQueryable<Usuario> queryableU = listaU.AsQueryable();

            repoGestores.Setup(
                c => c.RepositorioUsuario
                .ListaDeIncidentesDeLosProyectosALosQuePertenece(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<Incidente>()))
                .Returns(lista);

            List<Incidente> incidentes = gestor.ListaDeIncidentesDeLosProyectosALosQuePertenece(usuarioCompleto.Id, "", new Incidente());

            Assert.AreEqual(1, lista.Count());
            repoGestores.Verify(
                c => c.RepositorioUsuario
                .ListaDeIncidentesDeLosProyectosALosQuePertenece(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<Incidente>()));
        }

        [Test]
        public void se_puede_ver_los_proyectos_a_los_cuales_pertenece()
        {
            List<Proyecto> lista = new List<Proyecto>();
            lista.Add(new Proyecto());
            IQueryable<Proyecto> queryableP = lista.AsQueryable();

            List<Usuario> listaU = new List<Usuario>();
            listaU.Add(new Tester());
            IQueryable<Usuario> queryableU = listaU.AsQueryable();

            repoGestores.Setup(
                c => c.RepositorioUsuario
                .ListaDeProyectosALosQuePertenece(It.IsAny<int>()))
                .Returns(queryableP);

            IQueryable<Proyecto> proyectos = gestor.ListaDeProyectosALosQuePertenece(usuarioCompleto.Id, 3);

            Assert.AreEqual(1, proyectos.Count());
            repoGestores.Verify(
                c => c.RepositorioUsuario
                .ListaDeProyectosALosQuePertenece(It.IsAny<int>()));
        }
    }
}