using Incidentes.DatosFabrica;
using Incidentes.Logica;
using Incidentes.LogicaFabrica;
using Incidentes.LogicaInterfaz;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Incidentes.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            FabricaServicios fabrica = new FabricaServicios(services);
            fabrica.AgregarServicios();

            services.AddAutoMapper(typeof(Startup));
                       
            FabricaServiciosDatos fabricaDatos = new FabricaServiciosDatos(services);

            fabricaDatos.AgregarServicios();
            fabricaDatos.AgregarContextoDatos();
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
