﻿namespace Incidentes.Logica.DTOs
{
    public class Bug
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Version { get; set; }
        public string Estado { get; set; }

        public Bug() { }
    }
}