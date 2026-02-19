using Timetable.Model;
using Microsoft.EntityFrameworkCore;

using (var db = new SchoolContext())
{
    // 1. Datenbank sicherstellen (erstellt Tabellen automatisch)
    //db.Database.EnsureCreated();

    // 2. Testdaten erstellen, falls leer
    if (!db.Classes.Any())
    {
        var math = new Subject { Title = "Mathematik" };
        var physics = new Subject { Title = "Physik" };
        
        var class10A = new SchoolClass { Name = "10A" };
        
        // M:N Zuweisung
        class10A.Subjects.Add(math);
        class10A.Subjects.Add(physics);

        db.Classes.Add(class10A);
        db.SaveChanges();
        Console.WriteLine("Testdaten gespeichert!");
    }

    // 3. Daten abfragen
    var classes = db.Classes.Include(c => c.Subjects).ToList();

    foreach (var c in classes)
    {
        Console.WriteLine($"Klasse: {c.Name}");
        foreach (var s in c.Subjects)
        {
            Console.WriteLine($"  -> Fach: {s.Title}");
        }
    }
}