using Backend.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/test-db", async (AppDbContext db) =>
{
    try
    {
        var canConnect = await db.Database.CanConnectAsync();
        if (canConnect)
        {
            return Results.Ok(new { message = "Успешное подключение к Clever Cloud MySQL!" });
        }
        return Results.BadRequest(new { message = "Не удалось подключиться к базе." });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Ошибка подключения: {ex.Message}");
    }
});

app.Run();
