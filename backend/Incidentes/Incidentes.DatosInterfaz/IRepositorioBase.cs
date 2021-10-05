using System;
using System.Linq;
using System.Linq.Expressions;

namespace Incidentes.DatosInterfaz
{
    public interface IRepositorioBase<T>
    {
        IQueryable<T> ObtenerTodos(bool trackChanges);
        IQueryable<T> ObtenerPorCondicion(Expression<Func<T, bool>> expression, bool trackChanges);
        bool Existe(Expression<Func<T, bool>> expression);
        void Alta(T entity);
        void Modificar(T entity);
        void Eliminar(T entity);
    }
}
