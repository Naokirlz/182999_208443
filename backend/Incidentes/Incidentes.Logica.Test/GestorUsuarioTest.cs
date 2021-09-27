using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using Incidentes.Logica.Excepciones;
using Incidentes.Logica.Interfaz;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Assert = NUnit.Framework.Assert;

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

            gestor.AltaDesarrollador(usuarioCompleto.Token, unDesarrollador);

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
    }
}