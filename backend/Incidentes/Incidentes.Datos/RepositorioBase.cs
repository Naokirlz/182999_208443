using Incidentes.DatosInterfaz;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Incidentes.Datos
{
    public abstract class RepositorioBase<T> : IRepositorioBase<T> where T : class
    {
        protected Contexto ContextoRepositorio;

        public RepositorioBase(Contexto contexto)
        {
            ContextoRepositorio = contexto;
        }

        public IQueryable<T> ObtenerTodos(bool trackChanges) =>
            !trackChanges ?
              ContextoRepositorio.Set<T>()
                .AsNoTracking() :
              ContextoRepositorio.Set<T>();

        public IQueryable<T> ObtenerPorCondicion(Expression<Func<T, bool>> expression,
        bool trackChanges) =>
            !trackChanges ?
              ContextoRepositorio.Set<T>()
                .Where(expression)
                .AsNoTracking() :
              ContextoRepositorio.Set<T>()
                .Where(expression);

        public bool Existe(Expression<Func<T, bool>> expression)
        {
            return ContextoRepositorio.Set<T>().Any(expression);
        }

        public void Alta(T entity) => ContextoRepositorio.Set<T>().Add(entity);

        public virtual void Modificar(T entity) => ContextoRepositorio.Set<T>().Update(entity);

        public void Eliminar(T entity) => ContextoRepositorio.Set<T>().Remove(entity);

    }
}
