using Incidentes.Dominio;
using Microsoft.EntityFrameworkCore;

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

            builder.Entity<Usuario>()
                .HasIndex(u => u.NombreUsuario)
                .IsUnique();

            builder.Entity<Proyecto>()
               .HasIndex(p => p.Id) 
               .IsUnique();


            builder.Entity<Incidente>()
               .HasIndex(i => i.Id)
               .IsUnique();

            builder.Entity<Proyecto>()
               .HasMany<Usuario>(s => s.Asignados)
               .WithMany(c => c.proyectos);
               

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Incidente> Incidentes { get; set; }

    }
}