using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace HTLConsole.Models;

public partial class HtlContext : DbContext
{
    public HtlContext()
    {
    }

    public HtlContext(DbContextOptions<HtlContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Gebäude> Gebäudes { get; set; }

    public virtual DbSet<Raum> Raums { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=HTL;user=root;password=insy", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.44-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Gebäude>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("gebäude");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Bez)
                .HasMaxLength(255)
                .HasColumnName("bez");
        });

        modelBuilder.Entity<Raum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("raum");

            entity.HasIndex(e => e.Gid, "fk_gebäude");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Bez)
                .HasMaxLength(255)
                .HasColumnName("bez");
            entity.Property(e => e.Gid).HasColumnName("gid");

            entity.HasOne(d => d.GidNavigation).WithMany(p => p.Raums)
                .HasForeignKey(d => d.Gid)
                .HasConstraintName("fk_gebäude");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
