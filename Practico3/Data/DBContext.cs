using Microsoft.EntityFrameworkCore;
using Practico3.Models;

namespace Practico3.Data
{
    public class Contextt : DbContext
    {
        public Contextt(DbContextOptions<Contextt> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<IngresoHerramienta> IngresoHerramientas { get; set; }
        public DbSet<Herramienta> Herramientas { get; set; }
        public DbSet<Asignacion> Asignaciones { get; set; }
        public DbSet<Mantenimiento> Mantenimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aquí puedes agregar configuraciones básicas si es necesario
            // o dejarlo vacío si no necesitas configuraciones adicionales
        }
    }
}