using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add Services
var connectionString = "server=localhost;user=root;password=insy;database=demo";
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => 
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

app.UseCors();

// GET: All entries
// If "db" still shows an error, try: async ([FromServices] AppDbContext db)
app.MapGet("/api/timetable", async (AppDbContext db) => 
{
    return await db.TimetableEntries.ToListAsync();
});

// POST: Add entry
// Ensure it says MapPost
app.MapPost("/api/timetable", async (TimetableEntry entry, AppDbContext db) => 
{
    db.TimetableEntries.Add(entry);
    await db.SaveChangesAsync();
    return Results.Ok(entry);
});

app.Run();