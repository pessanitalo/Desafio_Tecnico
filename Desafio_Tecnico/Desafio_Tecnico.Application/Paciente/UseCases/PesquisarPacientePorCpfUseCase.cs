using Desafio_Tecnico.Application.Paciente.DTOs;
using Desafio_Tecnico.Domain.Common;
using Desafio_Tecnico.Domain.Repositories;
using Microsoft.Extensions.Logging;


namespace Desafio_Tecnico.Application.Paciente.UseCases
{
    public class PesquisarPacientePorCpfUseCase : IPesquisarPacientePorCpfUseCase
    {
        private readonly IPacienteRepository _pacienteRepository;
        protected readonly ILogger<PesquisarPacientePorCpfUseCase> _logger;

        public PesquisarPacientePorCpfUseCase(IPacienteRepository pacienteRepository, ILogger<PesquisarPacientePorCpfUseCase> logger)
        {
            _pacienteRepository = pacienteRepository;
            _logger = logger;
        }
        public async Task<Result<DetalhesPacienteDTO>> ObterPacientePorCpf(string cpf)
        {
            try
            {
                var paciente = await _pacienteRepository.PesquisarPacientePorCpf(cpf);

                if (paciente == null)
                    return Result<DetalhesPacienteDTO>.Fail("Paciente não encontrado");

                var pacienteDto = new DetalhesPacienteDTO
                {
                    PacienteId = paciente.PacienteId,
                    Nome = paciente.Nome,
                    CPF = paciente.CPF
                };

                return Result<DetalhesPacienteDTO>.Ok(pacienteDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Não foi possível obter paciente.");
                return Result<DetalhesPacienteDTO>.Fail($"Não foi possível obter paciente.");
            }
        }
    }
}
