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
        // Map each class to its own table
        // This creates the link (Foreign Key) automatically
        modelBuilder.Entity<Vererbung.Animals>().ToTable("Animals");
        modelBuilder.Entity<Vererbung.Dog>().ToTable("Dogs");
        modelBuilder.Entity<Vererbung.Bird>().ToTable("Birds");
    }
}