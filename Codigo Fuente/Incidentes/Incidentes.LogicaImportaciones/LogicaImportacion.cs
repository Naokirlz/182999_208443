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
    struct PluginInfo
    {
        public int Option;
        public string DisplayName;
        public string FullPath;
        public Type LoggerType;

        public override string ToString()
        {
            return $"{Option} -- {DisplayName}";
        }
    }

    public class LogicaImportacion : ILogicaImportaciones
    {
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

        private IFuente ObtenerImplementacion(string ruta, string tipo)
        {
            Type importacionesInterface = typeof(IFuente);

            foreach (string dllName in System.IO.Directory.GetFiles(ruta, "*.dll"))
            {
                if (dllName.ToLower().Contains(tipo.ToLower()))
                {
                    //1) Creo una instancia de Assembly, dado su nombre
                    Assembly dynamicAssembly = Assembly.LoadFrom(dllName);

                    //2) Buco una clase definida en ese assembly, que implemente la interfaz
                    var type = dynamicAssembly.GetTypes(). //De todos los tipos que esten definidos en el assembly

                        Where(t => importacionesInterface.IsAssignableFrom(t) //Me quedo con los que que se podrian asignar a la intefaz ICustomLogger

                              && t != importacionesInterface) //y debo excluir al que me define la interfaz en si misma.

                        .FirstOrDefault(); //Como asumo de antemano que tengo una sola clase por dll que implementa esa interfaz, lo selecciono
                    if (type != null)
                    {
                        return Activator.CreateInstance(type) as IFuente;
                    }
                }
                
            }

            //Assembly xmlAssembly = Assembly.LoadFrom(ruta);
            //foreach (var item in xmlAssembly.GetTypes().Where(t => typeof(IFuente).IsAssignableFrom(t)))
            //{
            //    if (item.FullName.ToLower().Contains(tipo.ToLower()))
            //    {
            //        return Activator.CreateInstance(item) as IFuente;
            //    }
            //}
            throw new DllNotFoundException("La implementación no fue encontrada.");
        }
    }
}
