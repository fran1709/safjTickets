﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace devTicket.Models
{
    public partial class DevTicketContext : IdentityDbContext
    {
        public DevTicketContext()
        {
        }

        public DevTicketContext(DbContextOptions<DevTicketContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Asiento> Asientos { get; set; } = null!;
        public virtual DbSet<Compra> Compras { get; set; } = null!;
        public virtual DbSet<Entrada> Entradas { get; set; } = null!;
        public virtual DbSet<Escenario> Escenarios { get; set; } = null!;
        public virtual DbSet<Evento> Eventos { get; set; } = null!;
        public virtual DbSet<TipoEscenario> TipoEscenarios { get; set; } = null!;
        public virtual DbSet<TipoEvento> TipoEventos { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;database=dev_ticket", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.27-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Asiento>(entity =>
            {
                entity.ToTable("asiento");

                entity.HasComment("tipos de asiento del escenario");

                entity.HasIndex(e => e.IdEscenario, "id_escenario");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Cantidad)
                    .HasColumnType("int(11)")
                    .HasColumnName("cantidad");

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
                    .WithMany(p => p.Asientos)
                    .HasForeignKey(d => d.IdEscenario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("asiento_ibfk_1");
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.ToTable("compra");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_spanish_ci");

                entity.HasIndex(e => e.IdCliente, "id_cliente");

                entity.HasIndex(e => e.IdEntrada, "id_entrada");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Active).HasColumnType("int(11)");

                entity.Property(e => e.Cantidad)
                    .HasColumnType("int(11)")
                    .HasColumnName("cantidad");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_At");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("Created_By");

                entity.Property(e => e.FechaPago)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_pago");

                entity.Property(e => e.FechaReserva)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_reserva");

                entity.Property(e => e.IdCliente)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_cliente");

                entity.Property(e => e.IdEntrada)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_entrada");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_At");

                entity.Property(e => e.UpdatedBy).HasColumnType("int(11)");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("compra_ibfk_1");

                entity.HasOne(d => d.IdEntradaNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdEntrada)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("compra_ibfk_2");
            });

            modelBuilder.Entity<Entrada>(entity =>
            {
                entity.ToTable("entradas");

                entity.HasIndex(e => e.IdEvento, "id_evento");

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

                entity.Property(e => e.IdEvento)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_evento");

                entity.Property(e => e.Precio)
                    .HasPrecision(10)
                    .HasColumnName("precio");

                entity.Property(e => e.TipoAsiento)
                    .HasMaxLength(100)
                    .HasColumnName("tipo_asiento")
                    .UseCollation("utf8_spanish_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_At");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("Updated_By");

                entity.HasOne(d => d.IdEventoNavigation)
                    .WithMany(p => p.Entrada)
                    .HasForeignKey(d => d.IdEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("entradas_ibfk_1");
            });

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

            modelBuilder.Entity<Evento>(entity =>
            {
                entity.ToTable("evento");

                entity.HasIndex(e => e.IdEscenario, "id_escenario");

                entity.HasIndex(e => e.IdTipoEvento, "id_tipo_evento");

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

                entity.Property(e => e.Disponible)
                    .HasColumnType("int(11)")
                    .HasColumnName("disponible");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdEscenario)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_escenario");

                entity.Property(e => e.IdTipoEvento)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_tipo_evento");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_At");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("Updated_By");

                entity.HasOne(d => d.IdEscenarioNavigation)
                    .WithMany(p => p.Eventos)
                    .HasForeignKey(d => d.IdEscenario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("evento_ibfk_1");

                entity.HasOne(d => d.IdTipoEventoNavigation)
                    .WithMany(p => p.Eventos)
                    .HasForeignKey(d => d.IdTipoEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("evento_ibfk_2");
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

            modelBuilder.Entity<TipoEvento>(entity =>
            {
                entity.ToTable("tipo_evento");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Active).HasColumnType("int(11)");

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

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_At");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("Updated_By");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_spanish_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Correo)
                    .HasMaxLength(100)
                    .HasColumnName("correo");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_At")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("Created_By");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Rol)
                    .HasColumnType("int(11)")
                    .HasColumnName("rol");

                entity.Property(e => e.Telefono)
                    .HasColumnType("int(11)")
                    .HasColumnName("telefono");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Updated_At");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("Updated_By");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
