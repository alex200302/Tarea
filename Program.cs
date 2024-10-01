using Microsoft.EntityFrameworkCore;
using System;
using TIenda.Models;

var builder = WebApplication.CreateBuilder(args);

// Agrega la configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin() // Permite cualquier origen
                  .AllowAnyMethod()  // Permite cualquier método (GET, POST, etc.)
                  .AllowAnyHeader(); // Permite cualquier cabecera
        });
});

// Configura el contexto de base de datos para usar MySQL
builder.Services.AddDbContext<TiendaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agrega los servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Aplica la política CORS antes de las solicitudes a la API
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
