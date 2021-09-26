using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using Incidentes.Logica.Interfaz;
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
            Mock<IRepositorioGestores> repoGestores = new Mock<IRepositorioGestores>();

            List<Usuario> lista = new List<Usuario>();
            lista.Add(usuarioCompleto);
            IQueryable<Usuario> queryableUsuarios = lista.AsQueryable();
            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false))
                .Returns(queryableUsuarios);

            GestorUsuario gestor = new GestorUsuario(repoGestores.Object);

            bool loginCorrecto = gestor.Login(this.usuarioCompleto.NombreUsuario, queryableUsuarios.FirstOrDefault().Contrasenia);

            Assert.IsTrue(loginCorrecto);
            repoGestores.Verify(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false));
        }

        [Test]
        public void un_usuario_no_se_puede_loguear_con_contrasenia_incorrecta()
        {
            Mock<IRepositorioGestores> repoGestores = new Mock<IRepositorioGestores>();

            List<Usuario> lista = new List<Usuario>();
            lista.Add(usuarioCompleto);
            IQueryable<Usuario> queryableUsuarios = lista.AsQueryable();
            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false))
                .Returns(queryableUsuarios);

            GestorUsuario gestor = new GestorUsuario(repoGestores.Object);

            bool loginCorrecto = gestor.Login(this.usuarioCompleto.NombreUsuario, "password incorrecto");

            Assert.IsFalse(loginCorrecto);
            repoGestores.Verify(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false));
        }

        [Test]
        public void un_usuario_al_loguearse_recibe_un_token()
        {
            Mock<IRepositorioGestores> repoGestores = new Mock<IRepositorioGestores>();

            List<Usuario> lista = new List<Usuario>();
            lista.Add(usuarioCompleto);
            IQueryable<Usuario> queryableUsuarios = lista.AsQueryable();
            List<Usuario> listaVacia = new List<Usuario>();
            IQueryable<Usuario> queryableUsuariosVacia = listaVacia.AsQueryable();

            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false))
                .Returns(queryableUsuarios);
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);
            repoGestores.Setup(c => c.RepositorioUsuario.Modificar(It.IsAny<Usuario>()));

            GestorUsuario gestor = new GestorUsuario(repoGestores.Object);

            bool loginCorrecto = gestor.Login(this.usuarioCompleto.NombreUsuario, queryableUsuarios.FirstOrDefault().Contrasenia);

            Assert.IsNotEmpty(usuarioCompleto.Token);
            repoGestores.Verify(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
            repoGestores.Verify(c => c.RepositorioUsuario.Modificar(It.IsAny<Usuario>()));
        }

        [Test]
        public void un_usuario_logueado_se_puede_desloguear()
        {
            Mock<IRepositorioGestores> repoGestores = new Mock<IRepositorioGestores>();

            usuarioCompleto.Token = "asdasdasdasdasdasdasd";

            List<Usuario> lista = new List<Usuario>();
            lista.Add(usuarioCompleto);
            IQueryable<Usuario> queryableUsuarios = lista.AsQueryable();
            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false))
                .Returns(queryableUsuarios);

            GestorUsuario gestor = new GestorUsuario(repoGestores.Object);

            gestor.Logout(usuarioCompleto.Token);

            Assert.IsEmpty(usuarioCompleto.Token);
            repoGestores.Verify(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false));
        }
    }
}