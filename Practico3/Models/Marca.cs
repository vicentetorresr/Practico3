﻿namespace Practico3.Models
{
    public class Marca
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Herramientas> Herramientas { get; set; } // Relación con herramientas de esta marca
    }
}
