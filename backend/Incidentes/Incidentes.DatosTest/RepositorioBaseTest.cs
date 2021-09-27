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

            Usuario d1 = new Desarrollador()
            {
                Nombre = "Martin",
                Apellido = "Cosa",
                Contrasenia = "Casa#Blanca",
                Email = "martind1@gmail.com",
                NombreUsuario = "martincosad1",
                Token = ""
            };

            Usuario d2 = new Desarrollador()
            {
                Nombre = "Martin",
                Apellido = "Cosa",
                Contrasenia = "Casa#Blanca",
                Email = "martind2@gmail.com",
                NombreUsuario = "martincosad2",
                Token = ""
            };

            Usuario t1 = new Tester()
            {
                Nombre = "Martin",
                Apellido = "Cosa",
                Contrasenia = "Casa#Blanca",
                Email = "martint1@gmail.com",
                NombreUsuario = "martincosat1",
                Token = ""
            };

            Usuario t2 = new Tester()
            {
                Nombre = "Martin",
                Apellido = "Cosa",
                Contrasenia = "Casa#Blanca",
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
                Descripcion = "Descripcion del incidente"
            };

            p1.Desarrolladores.Add((Desarrollador)d1);
            p1.Desarrolladores.Add((Desarrollador)d2);
            p1.Testers.Add((Tester)t1);
            p1.Testers.Add((Tester)t2);
            p1.Incidentes.Add(i1);

            DBContexto.Add(d1);
            DBContexto.Add(d2);
            DBContexto.Add(t1);
            DBContexto.Add(t2);
            DBContexto.Add(p1);
            DBContexto.Add(p2);
            DBContexto.Add(i1);

            DBContexto.SaveChanges();
        }

        protected void LimpiarBD()
        {
            DBContexto.Database.EnsureDeleted();
        }
    }
}