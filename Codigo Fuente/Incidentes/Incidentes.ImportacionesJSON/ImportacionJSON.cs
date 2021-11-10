using Incidentes.Dominio;
using Incidentes.LogicaInterfaz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Incidentes.ImportacionesJSON
{
    public class ImportacionJSON : IFuente
    {
        public List<Proyecto> ImportarBugs(string rutaFuente)
        {
            List<Proyecto> retorno = new List<Proyecto>();

            StreamReader jsonFile = new StreamReader(rutaFuente);
            string jsonString = jsonFile.ReadToEnd();
            Proyecto pro = JsonSerializer.Deserialize<Proyecto>(jsonString);

            retorno.Add(pro);
            return retorno;
        }
    }
}
