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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDbConnection>(_ =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICreatePacienteUseCase, CreatePacienteUseCase>();
builder.Services.AddScoped<ICreateProfissionalUseCase, CreateProfissionalUseCase>();
builder.Services.AddScoped<ICreateConsultaUseCase, CreateConsultaUseCase>();

builder.Services.AddScoped<IConsultaRepository, ConsultaRepository>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IProfissionalRepository, ProfissionalRepository>();

builder.Services.AddScoped<ICreateUsuarioUseCase, CreateUsuarioUseCase>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IGetAllPacienteUseCase, GetAllPacienteUseCase>();

builder.Services.AddScoped<IGetAllProfissionalUseCase, GetAllProfissionalUseCase>();
builder.Services.AddScoped<IObterDetalheConsultaUseCase, ObterDetalheConsultaUseCase>();
builder.Services.AddScoped<IObterTodasConsultasUseCase, ObterTodasConsultasUseCase>();
builder.Services.AddScoped<IPesquisarPacientePorCpfUseCase, PesquisarPacientePorCpfUseCase>();

builder.Services.AddScoped<IObterProfissionalUseCase, ObterProfissionalUseCase>();
builder.Services.AddScoped<IObterProfissionalUseCase, ObterProfissionalUseCase>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();
builder.Services.AddScoped<IObterAgendaPorProfissionalIdUseCase, ObterAgendaPorProfissionalIdUseCase>();

builder.Services.AddScoped<ILoginUseCase, LoginUseCase>();

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
