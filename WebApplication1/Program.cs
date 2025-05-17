using Business;
using Business.Implements;
using Business.Interfaces;
using Data;
using Entity.Contexts;
using Entity.DTOs;
using Entity.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Configuración de API y documentación
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔹 Configuración de CORS
var origenesPermitidos = builder.Configuration.GetValue<string>("OrigenesPermitidos")!.Split(",");
builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(politica =>
    {
        politica.WithOrigins(origenesPermitidos).AllowAnyHeader().AllowAnyMethod();
    });
});

// 🔹 Configuración de la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Registro de servicios de acceso a datos
builder.Services.AddScoped<IGenericData<Rol>, RolData>();

builder.Services.AddScoped<IGenericData<User>, UserData>();

builder.Services.AddScoped<IGenericData<RolUser>, RolUserData>();


// 🔹 Registro de servicios específicos de acceso a datos (para métodos adicionales)
builder.Services.AddScoped<RolData>();

builder.Services.AddScoped<UserData>();

builder.Services.AddScoped<RolUserData>();


// 🔹 Registro de servicios de negocio
builder.Services.AddScoped<IGenericBusiness<RolDto, Rol>, RolBusiness>();

builder.Services.AddScoped<IGenericBusiness<UserDto, User>, UserBusiness>();

builder.Services.AddScoped<IGenericBusiness<RolUserDto, RolUser>, RolUserBusiness>();


// 🔹 Registro de servicios específicos de negocio (para acceder a métodos adicionales)
builder.Services.AddScoped<RolBusiness>();

builder.Services.AddScoped<UserBusiness>();

builder.Services.AddScoped<RolUserBusiness>();

// 🔹 Servicios adicionales
builder.Services.AddHttpContextAccessor();
builder.Services.AddLogging();
builder.Services.AddControllers().AddNewtonsoftJson();

// 🔹 Configuración de Kestrel
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5163);
});

// 🔹 Construcción de la aplicación
var app = builder.Build();

// 🔹 Configuración del pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();