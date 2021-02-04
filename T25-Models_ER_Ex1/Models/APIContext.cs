using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace T25_Models_ER_Ex1.Models
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options) { }

        public DbSet<Articulos> Articulos { get; set; }
        public DbSet<Fabricantes> Fabricantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fabricantes>(fabricantes =>
            {
                // TABLA
                fabricantes.ToTable("FABRICANTES");

                //  DEFINICION COLUMNA ID
                fabricantes.Property(p => p.Codigo).HasColumnName("Codigo");
                fabricantes.HasKey(p => p.Codigo);

                // COLUMNAS
                fabricantes.Property(p => p.Codigo)
                    .IsRequired()
                    .IsUnicode(false);
                fabricantes.Property(p => p.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode();

            });

            modelBuilder.Entity<Articulos>(articulos =>
            {
                // TABLA
                articulos.ToTable("ARTICULOS");

                // DEFINICION COLUMNA ID
                articulos.Property(p => p.Codigo).HasColumnName("Codigo");
                articulos.HasKey(p => p.Codigo);

                // COLUMNAS
                articulos.Property(p => p.Codigo)
                    .IsRequired()
                    .IsUnicode();
                articulos.Property(p => p.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
                articulos.Property(p => p.Precio)
                    .IsRequired()
                    .IsUnicode();
                articulos.Property(p => p.Fabricante)
                    .IsRequired()
                    .IsUnicode();

                // RELACIONES
                articulos.HasOne(e => e.Fabricantes)
                    .WithMany(p => p.Articulos)
                    .HasForeignKey(d => d.Fabricante)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
