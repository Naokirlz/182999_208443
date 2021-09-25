﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Incidentes.Datos
{
    public interface IRepositorioBaseEntity<T>
    {
        IQueryable<T> ObtenerTodos(bool trackChanges);
        IQueryable<T> ObtenerPorCondicion(Expression<Func<T, bool>> expression, bool trackChanges);
        bool Existe(Expression<Func<T, bool>> expression);
        void Crear(T entity);
        void Modificar(T entity);
        void Eliminar(T entity);
    }
}
