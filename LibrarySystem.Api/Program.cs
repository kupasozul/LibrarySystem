using LibrarySystem.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// 1. Controller támogatás hozzáadása és JSON ciklusok kezelése
builder.Services.AddControllers()
    .AddJsonOptions(options => 
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddOpenApi();

// 2. Adatbázis kontextus regisztrálása
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlite("Data Source=library.db"));

// 3. CORS engedélyezése (nélkülözhetetlen a Blazor WASM-hez)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// 4. CORS használata
app.UseCors("AllowBlazor");

app.UseAuthorization();

// 5. Controller végpontok feltérképezése
app.MapControllers();

app.Run();