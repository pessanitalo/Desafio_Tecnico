using Desafio_Tecnico.Application.Consulta.UseCases;
using Desafio_Tecnico.Application.Login.UseCases;
using Desafio_Tecnico.Application.Paciente.UseCases;
using Desafio_Tecnico.Application.Profissional.UseCases;
using Desafio_Tecnico.Application.Usuario.UseCases;
using Desafio_Tecnico.Domain.Repositories;
using Desafio_Tecnico.Domain.Service;
using Desafio_Tecnico.Infrastructure.Persistence.Repository;
using Desafio_Tecnico.Infrastructure.Services;

namespace Desafio_Tecnico.Api.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationservices(this IServiceCollection services)
        {
            services.AddScoped<ICreatePacienteUseCase, CreatePacienteUseCase>();
            services.AddScoped<ICreateProfissionalUseCase, CreateProfissionalUseCase>();
            services.AddScoped<ICreateConsultaUseCase, CreateConsultaUseCase>();

            services.AddScoped<IConsultaRepository, ConsultaRepository>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<IProfissionalRepository, ProfissionalRepository>();

            services.AddScoped<ICreateUsuarioUseCase, CreateUsuarioUseCase>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IGetAllPacienteUseCase, GetAllPacienteUseCase>();

            services.AddScoped<IGetAllProfissionalUseCase, GetAllProfissionalUseCase>();
            services.AddScoped<IObterDetalheConsultaUseCase, ObterDetalheConsultaUseCase>();
            services.AddScoped<IObterTodasConsultasUseCase, ObterTodasConsultasUseCase>();
            services.AddScoped<IPesquisarPacientePorCpfUseCase, PesquisarPacientePorCpfUseCase>();

            services.AddScoped<IObterProfissionalUseCase, ObterProfissionalUseCase>();
            services.AddScoped<IObterProfissionalUseCase, ObterProfissionalUseCase>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPasswordHasherService, PasswordHasherService>();
            services.AddScoped<IObterAgendaPorProfissionalIdUseCase, ObterAgendaPorProfissionalIdUseCase>();

            services.AddScoped<ILoginUseCase, LoginUseCase>();

            return services;
        }
    }
}
