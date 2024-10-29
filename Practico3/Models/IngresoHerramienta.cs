namespace Practico3.Models
{
    public class IngresoHerramienta
    {
        public int Id { get; set; }
        public int HerramientaId { get; set; }
        public Herramientas Herramienta { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public bool EstaEnUso { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}