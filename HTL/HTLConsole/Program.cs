using HTLConsole.Models;
using Microsoft.EntityFrameworkCore;

using var db = new HtlContext();

Console.WriteLine("--- Subjects and their Classes ---");

// Load Subjects and "Include" the linked Classes
var faecher = await db.Faches
    .Include(f => f.Kids) // 'Kids' is the default name EF gives to the linked Klassen collection
    .ToListAsync();

foreach (var f in faecher)
{
    Console.WriteLine($"{f.Id} {f.Bez}, {f.Kids.Count}");
    
    foreach (var k in f.Kids)
    {
        Console.WriteLine($"   -{k.Id} {k.Bez}");
    }
    Console.WriteLine();
}