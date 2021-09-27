using System;
using System.Collections.Generic;

namespace Incidentes.Logica.Interfaz
{
    public interface ILogica<T>
    {
        T Alta(string token, T entity);

        void Baja(int id);

        T Modificar(int id, T entity);

        T Obtener(int id);

        IEnumerable<T> ObtenerTodos(string token);
    }
}
