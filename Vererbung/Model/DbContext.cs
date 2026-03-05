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
        // 1. Tell EF to use the TPC strategy for the base class
        modelBuilder.Entity<Animals>().UseTpcMappingStrategy();

        // 2. Map the concrete classes to their own tables
        // Note: 'Animal' does not get a ToTable() because it won't have one!
        modelBuilder.Entity<Dog>().ToTable("Dogs");
        modelBuilder.Entity<Bird>().ToTable("Birds");
    }
}