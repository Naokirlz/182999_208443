using Incidentes.Logica.Excepciones;
using System.Text.RegularExpressions;


namespace Incidentes.Logica
{
    internal class Validaciones
    {
        internal static void ValidarLargoTexto(string texto, int largoMax, int largoMin, string campo)
        {
            texto = texto.Trim();
            if (texto.Length > largoMax || texto.Length < largoMin)
                throw new ExcepcionLargoTexto("El largo del campo " + campo + " debe ser de entre " +
                                              largoMin.ToString() + " y " + largoMax.ToString() + " caracteres.");
        }

        internal static void ValidarPassword(string texto)
        {
            bool noValido = texto.Contains(" ");
            if (noValido)
                throw new ExcepcionArgumentoNoValido("No puede contener espacios en blanco en la contraseña");

            if (texto.Length > 25 || texto.Length < 8)
                throw new ExcepcionArgumentoNoValido("El largo de la contraseña debe ser de entre 8 y 25 caracteres.");
        }

        internal static void ValidarEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if(!match.Success)
                throw new ExcepcionArgumentoNoValido("Debe ingresar un email válido.");
        }

        public static void ValidarMayorACero(double unValor, string unCampo)
        {
            if (unValor < 0)
                throw new ExcepcionArgumentoNoValido("El valor de " + unCampo + " debe ser mayor a cero.");
        }
    }
}
