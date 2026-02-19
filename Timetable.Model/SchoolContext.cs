using Microsoft.EntityFrameworkCore;
using Timetable.Model;

public class SchoolContext : DbContext
{
    public DbSet<SchoolClass> Classes { get; set; }
    public DbSet<Subject> Subjects { get; set; }

    // Konstruktoren für DI und Design-Time (wie im vorigen Schritt erklärt)
    public SchoolContext() { }
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Ersetze den String mit deinen Daten
            var connectionString = "server=localhost;database=SchoolDb;user=root;password=insy";
            optionsBuilder.UseMySQL(connectionString); 
            // Hinweis: Bei Oracle heißt die Methode oft .UseMySQL() (großes SQL) 
            // statt .UseMySql() (kleines ysl)
        }
    }
}