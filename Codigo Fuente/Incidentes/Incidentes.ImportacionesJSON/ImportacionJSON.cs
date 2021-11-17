using Incidentes.DTOs;
using Incidentes.LogicaInterfaz;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Incidentes.ImportacionesJSON
{
    public class ImportacionJSON : IFuente
    {
        public List<ProyectoDTO> ImportarBugs(string rutaFuente)
        {
            List<ProyectoDTO> retorno = new List<ProyectoDTO>();

            StreamReader jsonFile = new StreamReader(rutaFuente);
            string jsonString = jsonFile.ReadToEnd();
            ProyectoDTO pro = JsonSerializer.Deserialize<ProyectoDTO>(jsonString);
            jsonFile.Close();
            retorno.Add(pro);
            return retorno;
        }
    }
}
