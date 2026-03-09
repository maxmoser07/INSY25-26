var builder = WebApplication.CreateBuilder(args);

// 1. Define the policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:7001") // Your Blazor URL
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// 2. Use the policy
app.UseCors("BlazorPolicy"); 

app.MapControllers();
app.Run();