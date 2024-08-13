using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WsApiexamen.Models;

public partial class BdiExamenContext : DbContext
{
    public BdiExamenContext()
    {
    }

    public BdiExamenContext(DbContextOptions<BdiExamenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Examen> TblExamen { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Examen>(entity =>
        {
            entity.HasKey(e => e.IdExamen).HasName("PK__tblExame__E569399B1D9002F5");

            entity.ToTable("tblExamen");

            entity.Property(e => e.IdExamen).HasColumnName("idExamen");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
