using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;


namespace Schedule.Context;

public partial class ScheduleContext : DbContext
{
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<T_has_S> TeacherSubjects { get; set; }
    public DbSet<C_has_S> ClassSubjects { get; set; }
    public DbSet<T_has_C> TeacherClasses { get; set; }
    public DbSet<Unterricht> Lessons { get; set; }
    public ScheduleContext()
    {
    }

    public ScheduleContext(DbContextOptions<ScheduleContext> options)
        : base(options)
    {
    }
    public DbSet<Unterricht> Unterrichtseinheiten { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=insy;database=demo", Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.4.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        // 1. Composite Keys für Join-Tables definieren
        modelBuilder.Entity<T_has_S>().HasKey(ts => new { ts.Tid, ts.Sid });
        modelBuilder.Entity<C_has_S>().HasKey(cs => new { cs.Cid, cs.Sid });
        modelBuilder.Entity<T_has_C>().HasKey(tc => new { tc.Tid, tc.Cid });

        // 2. Unterricht-Mapping: Sicherheit durch Composite FKs
        // Wir sagen EF, dass (Tid, Sid) zusammen existieren müssen (in T_has_S)
        modelBuilder.Entity<Unterricht>()
            .HasOne<T_has_S>() 
            .WithMany()
            .HasForeignKey(u => new { u.Tid, u.Sid });

        // Wir sagen EF, dass (Cid, Sid) zusammen existieren müssen (in C_has_S)
        modelBuilder.Entity<Unterricht>()
            .HasOne<C_has_S>()
            .WithMany()
            .HasForeignKey(u => new { u.Cid, u.Sid });

        // 3. Optionale Enum-Konvertierung
        modelBuilder.Entity<Unterricht>()
            .Property(u => u.Day)
            .HasConversion<int>();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
