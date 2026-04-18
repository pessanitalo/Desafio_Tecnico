using Desafio_Tecnico.Application.Paciente.DTOs;
using Desafio_Tecnico.Domain.Common;
using Desafio_Tecnico.Domain.Repositories;
using Desafio_Tecnico.Domain.Validation;
using Desafio_Tecnico.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using PacienteDomain = Desafio_Tecnico.Domain.Entities.Paciente;

namespace Desafio_Tecnico.Application.Paciente.UseCases
{
    public class CreatePacienteUseCase : ICreatePacienteUseCase
    {

        private readonly IPacienteRepository _pacienteRepository;
        protected readonly ILogger<CreatePacienteUseCase> _logger;

        public CreatePacienteUseCase(IPacienteRepository pacienteRepository, ILogger<CreatePacienteUseCase> logger)
        {
            _pacienteRepository = pacienteRepository;
            _logger = logger;
        }

        public async Task<Result<string>> AddAsync(PacienteDTO pacienteDTO)
        {
            try
            {
                var existe = await _pacienteRepository.PesquisarPacientePorCpf(pacienteDTO.Paciente.CPF);
                if (existe != null)
                    return Result<string>.Fail("Já existe um paciente com esse cpf cadastrado.");

                var pessoaFisica = new PessoaFisica(
                  pacienteDTO.Paciente.Nome,
                  pacienteDTO.Paciente.CPF
                );

                var paciente = PacienteDomain.Criar(pessoaFisica);

                await _pacienteRepository.AddAsync(paciente);
                return Result<string>.Ok("Paciente salvo com sucesso.");
            }

            catch (DomainExceptionValidation ex)
            {
                _logger.LogError(ex, "Não foi possivel salvar o paciente.");
                return Result<string>.Fail(ex.Message);
            }

        }
    }
}
