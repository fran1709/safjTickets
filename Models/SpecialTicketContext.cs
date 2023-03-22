using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace samTicket.Models
{
    public partial class SpecialTicketContext : DbContext
    {
        public SpecialTicketContext()
        {
        }

        public SpecialTicketContext(DbContextOptions<SpecialTicketContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Escenario> Escenarios { get; set; } = null!;
        public virtual DbSet<TipoEscenario> TipoEscenarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;database=samtickets", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.27-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Escenario>(entity =>
            {
                entity.ToTable("escenario");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_At")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("Created_By");

                entity.Property(e => e.Localizacion)
                    .HasMaxLength(100)
                    .HasColumnName("localizacion")
                    .UseCollation("utf8_spanish_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre")
                    .UseCollation("utf8_spanish_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_At");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("Updated_By");
            });

            modelBuilder.Entity<TipoEscenario>(entity =>
            {
                entity.ToTable("tipo_escenario");

                entity.HasIndex(e => e.IdEscenario, "id_escenario");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_At")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("Created_By");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .HasColumnName("descripcion")
                    .UseCollation("utf8_spanish_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.IdEscenario)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_escenario");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_At");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("Updated_By");

                entity.HasOne(d => d.IdEscenarioNavigation)
                    .WithMany(p => p.TipoEscenarios)
                    .HasForeignKey(d => d.IdEscenario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tipo_escenario_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
