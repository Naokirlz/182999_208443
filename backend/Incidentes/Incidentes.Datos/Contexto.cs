using Incidentes.Dominio;
using Microsoft.EntityFrameworkCore;
using System;

namespace Incidentes.Datos
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Administrador>()
                .HasIndex(s => s.Id) //...Tenga un indice en BD por la columna StduentId
                .IsUnique(); //...Y que sea unico. No admitira duplicados.

         }

        public DbSet<Administrador> Administradores { get; set; }
    }
}