var builder = WebApplication.CreateBuilder(args);

// 1. Add the Controller services (This is what's missing!)
builder.Services.AddControllers();

// 2. Register your DbContext (Ensure your connection string is here)
builder.Services.AddDbContext<Vererbung.MyDbContext>();

// 3. Add CORS (Crucial for your Blazor WASM to talk to this API)
builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorPolicy", policy =>
    {
        policy.AllowAnyOrigin() // For school projects, AllowAnyOrigin is easiest
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// 4. Use CORS before mapping controllers
app.UseCors("BlazorPolicy");

// 5. Map the controllers
app.MapControllers();

app.Run();