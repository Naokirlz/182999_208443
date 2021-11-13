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

        [Test]
        public void funciona_correctamente_importacionesDto()
        {
            ImportacionesDTO imp = new ImportacionesDTO()
            {
                Ruta = "asasas"
            };
            Assert.AreEqual(imp.Ruta, "asasas");
        }

        [Test]
        public void funciona_correctamente_proyectoParaReporteDto()
        {
            ProyectoParaReporteDTO pro = new ProyectoParaReporteDTO()
            {
                Id = 3,
                Nombre = "nombre pro",
                CantidadDeIncidentes = 4
            };
            Assert.AreEqual(3, pro.Id);
            Assert.AreEqual("nombre pro", pro.Nombre);
            Assert.AreEqual(4, pro.CantidadDeIncidentes);
        }
    }
}