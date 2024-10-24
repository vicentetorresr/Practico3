using Microsoft.EntityFrameworkCore;
namespace Practico3.Data
{
    public class Contextt : DbContext
    {
        public Contextt(DbContextOptions<Contextt> options) : base(options)
        {
        }

        //public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Datos iniciales para Usuarios
            /*modelBuilder.Entity<Usuario>().HasData(new Usuario
            {
                Id = 1,
                Name = "Vicente",
                Fecha = DateTime.Now,
            });
            */
        }
    }
}
