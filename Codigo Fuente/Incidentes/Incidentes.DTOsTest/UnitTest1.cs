using Incidentes.DTOs;
using NUnit.Framework;
using System.Collections.Generic;

namespace Incidentes.DTOsTest
{
    public class Tests
    {
        [Test]
        public void funciona_correctamente_asignacionesDto()
        {
            List<int> lista = new List<int>();
            AsignacionesDTO asig = new AsignacionesDTO()
            {
                ProyectoId = 3,
                UsuarioId = new List<int>()
            };
            Assert.AreEqual(3, asig.ProyectoId);
            Assert.AreEqual(lista, asig.UsuarioId);
        }
    }
}