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
    }
}