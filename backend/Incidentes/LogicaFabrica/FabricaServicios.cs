﻿using Incidentes.Logica;
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
        }
    }
}
