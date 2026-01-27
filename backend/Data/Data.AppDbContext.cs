using Microsoft.EntityFrameworkCore;
using MiWebAPIFotos.Entities;

namespace MiWebAPIFotos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Visita> Visitas => Set<Visita>();
        public DbSet<Foto> Fotos => Set<Foto>();
        public DbSet<Seleccion> Selecciones => Set<Seleccion>();
        public DbSet<SeleccionFoto> SeleccionFotos => Set<SeleccionFoto>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SeleccionFoto>()
                .HasKey(sf => new { sf.SeleccionId, sf.FotoId });

            modelBuilder.Entity<SeleccionFoto>()
                .HasOne(sf => sf.Seleccion)
                .WithMany(s => s.SeleccionFotos)
                .HasForeignKey(sf => sf.SeleccionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SeleccionFoto>()
                .HasOne(sf => sf.Foto)
                .WithMany(f => f.SeleccionFotos)
                .HasForeignKey(sf => sf.FotoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
