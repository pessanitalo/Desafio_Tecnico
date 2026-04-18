using Desafio_Tecnico.Api.DependencyInjection;
using Desafio_Tecnico.Application.Consulta.UseCases;
using Desafio_Tecnico.Application.Login.UseCases;
using Desafio_Tecnico.Application.Paciente.UseCases;
using Desafio_Tecnico.Application.Profissional.UseCases;
using Desafio_Tecnico.Application.Usuario.UseCases;
using Desafio_Tecnico.Domain.Repositories;
using Desafio_Tecnico.Domain.Service;
using Desafio_Tecnico.Infrastructure.Persistence.Repository;
using Desafio_Tecnico.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState.Values
            .SelectMany(value => value.Errors)
            .Select(error => string.IsNullOrWhiteSpace(error.ErrorMessage)
                ? "Dados da requisicao invalidos."
                : error.ErrorMessage)
            .Distinct()
            .ToArray();

        return new BadRequestObjectResult(new { errors });
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDbConnection>(_ =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddApplicationservices();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.MapControllers();

app.Run();
