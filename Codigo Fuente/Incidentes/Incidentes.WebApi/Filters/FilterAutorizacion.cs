using Incidentes.LogicaInterfaz;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Incidentes.WebApi.Filters
{
    public class FilterAutorizacion : Attribute, IAuthorizationFilter
    {
        private ILogicaAutorizacion _autorizacion;
        private readonly string[] _roles;

        public FilterAutorizacion(params string[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _autorizacion = (ILogicaAutorizacion)context.HttpContext.RequestServices.GetService(typeof(ILogicaAutorizacion));

            string token = context.HttpContext.Request.Headers["autorizacion"];
            if (token == null)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "No presento las credenciales de autenticacion"
                };
                return;
            }
            if (!_autorizacion.TokenValido(token, _roles))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 403,
                    Content = "Sesion no valida, o no tiene permisos para ejecutar la accion solicitada"
                };
                return;
            }
        }
    }
}
