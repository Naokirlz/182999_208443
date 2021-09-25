using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.Datos
{
    public abstract class RepositorioBaseEntity<T> : IRepositorioBaseEntity<T> where T : class
    {
        protected Contexto ContextoRepositorio;

        public RepositorioBaseEntity(Contexto contexto)
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
            //TODO: Este metodo es nuevo, es para demostrar el uso de Any(). No necesariamente debo leer el objeto.
            //Simplemente "pregunto" si existe alguno que cumpla condicion "expression"
            return ContextoRepositorio.Set<T>().Any(expression);
        }

        public void Crear(T entity) => ContextoRepositorio.Set<T>().Add(entity);

        public void Modificar(T entity) => ContextoRepositorio.Set<T>().Update(entity);

        public void Eliminar(T entity) => ContextoRepositorio.Set<T>().Remove(entity);

    }
}
