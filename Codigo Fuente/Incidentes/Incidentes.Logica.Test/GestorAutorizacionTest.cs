using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using Incidentes.Excepciones;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Incidentes.Logica.Test
{
    public class GestorAutorizacionTest
    {
        private Usuario usuarioCompleto;
        Mock<IRepositorioGestores> repoGestores;
        GestorAutorizacion gestor;
        GestorUsuario gestorU;

        [SetUp]
        public void Setup()
        {
            this.usuarioCompleto = new Usuario()
            {
                Nombre = "Martin",
                Apellido = "Cosa",
                Contrasenia = "Casa#Blanca",
                Email = "martin@gmail.com",
                Id = 1,
                NombreUsuario = "martincosa",
                Token = ""
            };

            repoGestores = new Mock<IRepositorioGestores>();
            gestor = new GestorAutorizacion(repoGestores.Object);
            gestorU = new GestorUsuario(repoGestores.Object);
        }

        [TearDown]
        public void TearDown()
        {
            this.usuarioCompleto = null;
            repoGestores = null;
            gestor = null;
        }

        [Test]
        public void se_espera_error_sin_estar_logueado()
        {
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);
            Assert.Throws<ExcepcionAccesoNoAutorizado>(() => gestor.UsuarioAutenticado(usuarioCompleto.Token));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }

        [Test]
        public void se_espera_error_si_el_token_es_nulo()
        {
            bool result = gestor.TokenValido(null);
            Assert.IsFalse(result);
        }

        [Test]
        public void se_espera_error_si_el_usuario_no_existe()
        {
            IQueryable<Usuario> usus = new List<Usuario>().AsQueryable();
            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), It.IsAny<bool>())).Returns(usus);
            bool result = gestor.TokenValido("token");
            Assert.IsFalse(result);
            repoGestores.VerifyAll();
        }

        [Test]
        public void se_espera_error_si_el_usuario_no_tiene_el_rol_esperado()
        {
            Usuario u = new Usuario() { 
                RolUsuario = Usuario.Rol.Desarrollador
            };
            List<Usuario> usuL = new List<Usuario>();
            usuL.Add(u);
            IQueryable<Usuario> usus = usuL.AsQueryable();
            string[] roles = new string[1] { "Administrador" };
            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), It.IsAny<bool>())).Returns(usus);

            bool result = gestor.TokenValido("token", roles);
            Assert.IsFalse(result);
            repoGestores.VerifyAll();
        }

        [Test]
        public void se_esperatrue_si_el_usuario_tiene_el_rol_esperado()
        {
            Usuario u = new Usuario()
            {
                RolUsuario = Usuario.Rol.Desarrollador
            };
            List<Usuario> usuL = new List<Usuario>();
            usuL.Add(u);
            IQueryable<Usuario> usus = usuL.AsQueryable();
            string[] roles = new string[1] { "Desarrollador" };
            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), It.IsAny<bool>())).Returns(usus);

            bool result = gestor.TokenValido("token", roles);
            Assert.IsTrue(result);
            repoGestores.VerifyAll();
        }
    }
}