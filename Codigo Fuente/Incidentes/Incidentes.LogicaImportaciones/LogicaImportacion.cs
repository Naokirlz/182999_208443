using System.Collections.Generic;
using Incidentes.Dominio;
using System.Linq;
using Incidentes.DatosInterfaz;
using Incidentes.LogicaInterfaz;
using System.Reflection;
using System;
using System.IO;
using System.Diagnostics;
using Incidentes.Logica.Excepciones;

namespace Incidentes.LogicaImportaciones
{
    public class LogicaImportacion : ILogicaImportaciones
    {
        const string directorio_plugins = "C:\\Users\\crist\\Desktop\\reflection";
        IRepositorioGestores _repositorioGestor;
        private const string elemento_no_existe = "El elemento no existe";

        public LogicaImportacion(IRepositorioGestores repositorioGestores)
        {
            _repositorioGestor = repositorioGestores;
        }

        public void ImportarBugs(string rutaFuente, string rutaBinario, int usuarioId)
        {
            if (!File.Exists(rutaFuente))
            {
                throw new ExcepcionElementoNoExiste(elemento_no_existe);
            }
            string tipo = Path.GetExtension(rutaFuente).TrimStart('.'); ;
            IFuente fuente = ObtenerImplementacion(rutaBinario, tipo);

            List<Proyecto> proys = fuente.ImportarBugs(rutaFuente);
            foreach (Proyecto p in proys)
            {
                Proyecto proyecto = _repositorioGestor.RepositorioProyecto.ObtenerPorCondicion(u => u.Nombre == p.Nombre, true).FirstOrDefault();
                foreach (Incidente i in p.Incidentes)
                {
                    i.UsuarioId = usuarioId;
                    _repositorioGestor.RepositorioIncidente.Alta(i);
                    proyecto.Incidentes.Add(i);
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
