using Incidentes.Datos;
using Incidentes.DatosInterfaz;
using Incidentes.Logica;
using Incidentes.Logica.Interfaz;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Incidentes.LogicaFabrica
{
    public class FabricaServicios
    {
        private readonly IServiceCollection services;
        public FabricaServicios(IServiceCollection services)
        {
            this.services = services;
        }
        public void AgregarServicios()
        {
            services.AddScoped<ILogicaUsuario, GestorUsuario>();
            //services.AddScoped<IRepositorioGestores, RepositorioGestores>();
        }
        public void AgregarContextoDatos()
        {
            /*services.AddDbContext<Contexto>(opts =>
            opts.UseSqlServer(Configuration.GetConnectionString("sqlConnection"), //Leo el conection string llamado "sqlConnection" desde appsettings.json 
            b => b.MigrationsAssembly("Incidentes.Datos"))); //Especifico el nombre del ensamblado donde quiero guardar las migraciones.
            services.AddDbContext<DbContext, Contexto>();*/
        }
    }
}
