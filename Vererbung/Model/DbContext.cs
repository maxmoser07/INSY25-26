using Microsoft.EntityFrameworkCore;

namespace Vererbung;

public class MyDbContext : DbContext
{
    // These tell EF which classes should become tables
    public DbSet<Animals> Animals { get; set; }
    public DbSet<Dog> Dogs { get; set; }
    public DbSet<Bird> Birds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Use your MySQL connection string here
        optionsBuilder.UseMySQL("Server=127.0.0.1;Database=demo;Uid=root;Pwd=insy;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // This tells EF to create separate tables for the subclasses
        // They will be linked by the Primary Key (Id)
        modelBuilder.Entity<Animals>().ToTable("Animals");
        modelBuilder.Entity<Dog>().ToTable("Dogs");
        modelBuilder.Entity<Bird>().ToTable("Birds");
    }
}