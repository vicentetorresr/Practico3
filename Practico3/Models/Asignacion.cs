namespace Practico3.Models
{
    public class Asignacion
    {
        public int Id { get; set; }
        public int HerramientaId { get; set; }
        public Herramientas? Herramienta { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public DateTime FechaAsignacion { get; set; } = DateTime.Now;
        public DateTime? FechaDevolucion { get; set; }
        public string Estado { get; set; } = "En uso";
    }
}
