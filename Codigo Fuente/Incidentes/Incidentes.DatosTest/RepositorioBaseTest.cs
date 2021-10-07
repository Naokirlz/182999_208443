using NUnit.Framework;
using Incidentes.Datos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Incidentes.Dominio;

namespace Incidentes.DatosTest
{
    public class RepositorioBaseTest
    {
        protected Contexto DBContexto { get; private set; }

        protected void SetearBaseDeDatos()
        {
            var opciones = new DbContextOptionsBuilder<Contexto>().UseInMemoryDatabase(databaseName: "Incidentes").Options;
            DBContexto = new Contexto(opciones);

            Usuario d1 = new Usuario()
            {
                Nombre = "Martin",
                Apellido = "Cosa",
                Contrasenia = "Casa#Blanca",
                RolUsuario = Usuario.Rol.Desarrollador,
                Email = "martind1@gmail.com",
                NombreUsuario = "martincosad1",
                Token = ""
            };

            Usuario d2 = new Usuario()
            {
                Nombre = "Martin",
                Apellido = "Cosa",
                Contrasenia = "Casa#Blanca",
                Email = "martind2@gmail.com",
                RolUsuario = Usuario.Rol.Desarrollador,
                NombreUsuario = "martincosad2",
                Token = ""
            };

            Usuario t1 = new Usuario()
            {
                Nombre = "Martin",
                Apellido = "Cosa",
                Contrasenia = "Casa#Blanca",
                RolUsuario = Usuario.Rol.Tester,
                Email = "martint1@gmail.com",
                NombreUsuario = "martincosat1",
                Token = ""
            };

            Usuario t2 = new Usuario()
            {
                Nombre = "Martin",
                Apellido = "Cosa",
                Contrasenia = "Casa#Blanca",
                RolUsuario = Usuario.Rol.Tester,
                Email = "martint2@gmail.com",
                NombreUsuario = "martincosat2",
                Token = ""
            };

            Proyecto p1 = new Proyecto()
            {
                Nombre = "Proyecto 1"
            };

            Proyecto p2 = new Proyecto()
            {
                Nombre = "Proyecto 2"
            };

            Incidente i1 = new Incidente() {
                Nombre = "Incidente 1",
                Descripcion = "Descripcion del incidente",
                EstadoIncidente = Incidente.Estado.Resuelto,
                ProyectoId = 1,
                DesarrolladorId = 1
            };

            Incidente i2 = new Incidente()
            {
                Nombre = "Incidente 2",
                ProyectoId = 2,
                Descripcion = "Descripcion del incidente"
            };

            p1.Asignados.Add((Usuario)d1);
            p1.Asignados.Add((Usuario)d2);
            p1.Asignados.Add((Usuario)t1);
            p2.Asignados.Add((Usuario)t1);
            p1.Asignados.Add((Usuario)t2);
            p1.Incidentes.Add(i1);
            p2.Incidentes.Add(i2);

            DBContexto.Add(d1);
            DBContexto.Add(d2);
            DBContexto.Add(t1);
            DBContexto.Add(t2);
            DBContexto.Add(p1);
            DBContexto.Add(p2);
            DBContexto.Add(i1);
            DBContexto.Add(i2);

            DBContexto.SaveChanges();
        }

        protected void LimpiarBD()
        {
            DBContexto.Database.EnsureDeleted();
        }
    }
}