using System.Collections.Generic;
using Incidentes.Dominio;
using System.Linq;
using Incidentes.DatosInterfaz;
using Incidentes.LogicaInterfaz;
using System.Reflection;
using System;
using System.IO;
using Incidentes.Logica.Excepciones;
using System.Xml.Serialization;
using System.Xml;
using Incidentes.DTOs;

namespace Incidentes.LogicaImportaciones
{
    public class LogicaImportacion : ILogicaImportaciones
    {
        private string directorio_plugins;
        IRepositorioGestores _repositorioGestor;
        private const string elemento_no_existe = "El elemento no existe";

        public LogicaImportacion(IRepositorioGestores repositorioGestores)
        {
            _repositorioGestor = repositorioGestores;
            directorio_plugins = Directory.GetCurrentDirectory();
            int back = 3;
            if (directorio_plugins.Contains("Debug")) back = 6;
            for (int i=0; i< back; i++)
            {
                directorio_plugins = System.IO.Directory.GetParent(directorio_plugins).FullName;
            }
            directorio_plugins += "\\Documentacion\\Accesorios-Postman-Fuentes\\DLLs\\";
        }

        public void ImportarBugs(string rutaFuente, string rutaBinario, int usuarioId)
        {
            if (!File.Exists(rutaFuente))
            {
                throw new ExcepcionElementoNoExiste(elemento_no_existe);
            }
            string tipo = Path.GetExtension(rutaFuente).TrimStart('.');

            IFuente fuente = ObtenerImplementacion(rutaBinario, tipo);

            List<ProyectoDTO> proys = fuente.ImportarBugs(rutaFuente);
            foreach (ProyectoDTO p in proys)
            {
                Proyecto proyecto = _repositorioGestor.RepositorioProyecto.ObtenerPorCondicion(u => u.Nombre == p.Nombre, true).FirstOrDefault();
                foreach (IncidenteDTO i in p.Incidentes)
                {
                    Incidente idominio = i.convertirDTO_Dominio();
                    _repositorioGestor.RepositorioIncidente.Alta(idominio);
                    proyecto.Incidentes.Add(idominio);
                    _repositorioGestor.Save();
                }
            }
        }

        public List<string> ListarPlugins()
        {
            List<string> retorno = new List<string>();

            Type importacionesInterface = typeof(IFuente);

            foreach (string dllName in System.IO.Directory.GetFiles(directorio_plugins, "*.dll"))
            {
                
                Assembly dynamicAssembly = Assembly.LoadFrom(dllName);

               
                var type = dynamicAssembly.GetTypes().

                    Where(t => importacionesInterface.IsAssignableFrom(t) 

                          && t != importacionesInterface)

                    .FirstOrDefault(); 

                
                if (type != null)
                {
                    retorno.Add(dllName);
                }
            }

            return retorno;
        }

        private IFuente ObtenerImplementacion(string ruta, string tipo)
        {
            Type importacionesInterface = typeof(IFuente);
            string directory = System.IO.Directory.GetParent(ruta).FullName;

            foreach (string dllName in System.IO.Directory.GetFiles(directory, "*.dll"))
            {
                if (dllName.ToLower().Contains(ruta.ToLower()))
                {
                    Assembly dynamicAssembly = Assembly.LoadFrom(dllName);
                    
                    var type = dynamicAssembly.GetTypes().

                        Where(t => importacionesInterface.IsAssignableFrom(t) 

                              && t != importacionesInterface)

                        .FirstOrDefault();
                    if (type != null)
                    {
                        return Activator.CreateInstance(type) as IFuente;
                    }
                }
                
            }

            throw new DllNotFoundException("La implementación no fue encontrada.");
        }
    }
}
