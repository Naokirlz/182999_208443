using Incidentes.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Incidentes.Datos
{
    public class Contexto : DbContext
    {
        public Contexto() { }
        public Contexto(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Usuario>()
                .HasIndex(u => u.Id) 
                .IsUnique(); 

            builder.Entity<Proyecto>()
               .HasIndex(p => p.Id) 
               .IsUnique();

            builder.Entity<Incidente>()
               .HasIndex(i => i.Id)
               .IsUnique();

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Incidente> Incidentes { get; set; }

    }
}