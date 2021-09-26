using Incidentes.Logica;
using Incidentes.LogicaInterfaz;
using Microsoft.Extensions.DependencyInjection;


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
            services.AddScoped<ILogicaProyecto, GestorProyecto>();
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
