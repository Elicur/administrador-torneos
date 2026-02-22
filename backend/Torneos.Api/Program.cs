using Microsoft.EntityFrameworkCore;
using Torneos.Api.Data;
using Torneos.Api.Services.Equipos;
using Torneos.Api.Services.Torneos;
using Torneos.Api.Services.Fixtures;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Services
builder.Services.AddScoped<IEquiposService, EquiposService>();
builder.Services.AddScoped<ITorneosService, TorneosService>();
builder.Services.AddScoped<IFixtureService, FixtureService>();

var app = builder.Build();

// Swagger en Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.ConfigObject.AdditionalItems["theme"] = "dark";
    });
}

// Pipeline
app.UseRouting();

// En Docker dev por HTTP, no redirijas a HTTPS
// En prod lo habilitás detrás de un reverse proxy con TLS
// app.UseHttpsRedirection();

app.UseAuthorization();

// Endpoints
app.MapControllers();

// (Opcional) endpoints de prueba
app.MapGet("/db-test", async (AppDbContext db) =>
{
    var canConnect = await db.Database.CanConnectAsync();
    return Results.Ok(new { connected = canConnect });
});

app.Run();
