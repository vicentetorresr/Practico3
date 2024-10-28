namespace Practico3.Models
{
    public class Herramienta
    {
        public int Id { get; set; }
        public string Modelo { get; set; }
        public int MarcaId { get; set; } // Llave foránea para Marca
        public Marca Marca { get; set; }
        public int CantidadTotal { get; set; }
        public int CantidadDisponible { get; set; }
        public int CantidadUsada { get; set; }
        public int CantidadEnMantenimiento { get; set; }
        public string Estado { get; set; } // 'Disponible', 'En Uso', 'En Mantenimiento'
        public List<Asignacion> Asignaciones { get; set; } // Relación con asignaciones
        public List<Mantenimiento> Mantenimientos { get; set; } // Relación con mantenimientos
    }
}
