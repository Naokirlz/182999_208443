﻿using Incidentes.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.LogicaInterfaz
{
    public interface IFuente
    {
        public List<Proyecto> ImportarBugs();
    }
}
