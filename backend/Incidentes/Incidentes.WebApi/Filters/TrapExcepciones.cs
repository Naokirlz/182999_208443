using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Incidentes.Logica.Excepciones;

namespace Incidentes.WebApi.Filters
{
    public class TrapExcepciones : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            int statusCode = 500;
            string content = context.Exception.Message;

            var type = context.Exception.GetType();
            if (type == typeof(ExcepcionAccesoNoAutorizado))
            {
                statusCode = 401; 
            }
            else if (type == typeof(ExcepcionArgumentoNoValido))
            {
                statusCode = 400; 
            }
            else if (type == typeof(ExcepcionElementoNoExiste))
            {
                statusCode = 400; 
            }
            else if (type == typeof(ExcepcionLargoTexto))
            {
                statusCode = 400; 
            }
            else
            {
                content = "Internal Server Error | " + context.Exception.Message;
            }

            context.Result = new ContentResult()
            {
                StatusCode = statusCode,
                Content = content
            };
        }
    }
}
