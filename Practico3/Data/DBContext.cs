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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Datos iniciales para Usuarios
            modelBuilder.Entity<Usuario>().HasData(new Usuario
            {
                Id = 1, 
                Name = "Vicente",
              
            });

        }
    }
}
