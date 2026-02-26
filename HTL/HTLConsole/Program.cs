using HTLConsole.Models;
using Microsoft.EntityFrameworkCore;

// 1. Initialize the context
using var db = new HtlContext();

Console.WriteLine("Fetching HTL Database Records...");
Console.WriteLine("---------------------------------");

// 2. Fetch buildings AND their related rooms using .Include
var gebaeudeListe = await db.Gebäudes
    .Include(g => g.Raums) // This links the tables in the query
    .ToListAsync();

// 3. Nested loop to display the data
foreach (var g in gebaeudeListe)
{
    Console.WriteLine($"Building: {g.Bez} (ID: {g.Id})");
    
    if (g.Raums.Any())
    {
        foreach (var r in g.Raums)
        {
            Console.WriteLine($"\t Room: {r.Bez} (ID: {r.Id})");
        }
    }
    else
    {
        Console.WriteLine("\t No rooms assigned)");
    }
    Console.WriteLine(); 
}