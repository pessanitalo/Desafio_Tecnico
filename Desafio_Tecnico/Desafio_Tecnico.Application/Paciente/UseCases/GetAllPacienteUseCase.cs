using Desafio_Tecnico.Application.DTOs;
using Desafio_Tecnico.Application.Paciente.DTOs;
using Desafio_Tecnico.Domain.Common;
using Desafio_Tecnico.Domain.Repositories;
using Microsoft.Extensions.Logging;


namespace Desafio_Tecnico.Application.Paciente.UseCases
{
    public class GetAllPacienteUseCase : IGetAllPacienteUseCase
    {
        private readonly IPacienteRepository _pacienteRepository;
        protected readonly ILogger<GetAllPacienteUseCase> _logger;

        public GetAllPacienteUseCase(IPacienteRepository pacienteRepository, ILogger<GetAllPacienteUseCase> logger)
        {
            _pacienteRepository = pacienteRepository;
            _logger = logger;
        }
        public async Task<Result<IEnumerable<PacienteDTO>>> GetAllAsync()
        {
            try
            {
                var pacientes = await _pacienteRepository.GetAllAsync();

                var pacientesDto = pacientes.Select(p => new PacienteDTO
                {
                    Paciente = new PessoaFisicaDTO
                    {
                        Nome = p.Pessoa.Nome,
                        CPF = p.Pessoa.CPF
                    }
                });
                return Result<IEnumerable<PacienteDTO>>.Ok(pacientesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Não foi possível obter todos os pacientes.");
                return Result<IEnumerable<PacienteDTO>>.Fail($"Não foi possível obter todos os pacientes.");
            }
        }
    }
}
