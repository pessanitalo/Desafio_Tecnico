using Desafio_Tecnico.Application.Profissional.DTOs;
using Desafio_Tecnico.Domain.Common;
using Desafio_Tecnico.Domain.Repositories;
using Desafio_Tecnico.Domain.Validation;
using Desafio_Tecnico.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using ProfissionalDomain = Desafio_Tecnico.Domain.Entities.Profissional;

namespace Desafio_Tecnico.Application.Profissional.UseCases
{
    public class CreateProfissionalUseCase : ICreateProfissionalUseCase
    {

        private readonly IProfissionalRepository _profissionalRepository;
        protected readonly ILogger<CreateProfissionalUseCase> _logger;

        public CreateProfissionalUseCase(IProfissionalRepository profissionalRepository, ILogger<CreateProfissionalUseCase> logger)
        {
            _profissionalRepository = profissionalRepository;
            _logger = logger;
        }

        public async Task<Result<string>> AddAsync(ProfissionalDTO profissionalDTO)
        {
            try
            {
                var existe = await _profissionalRepository.ObterProfissional(profissionalDTO.Profissional.CPF);
                if (existe != null)
                    return Result<string>.Fail("Já existe um profissional com esse cpf cadastrado.");

                var pessoaFisica = new PessoaFisica(
                    profissionalDTO.Profissional.Nome,
                    profissionalDTO.Profissional.CPF
                );

                var profissional = ProfissionalDomain.Criar(pessoaFisica);

                await _profissionalRepository.AddAsync(profissional);
                return Result<string>.Ok("Profissional salvo com sucesso.");
            }
            catch (DomainExceptionValidation ex)
            {
                _logger.LogError(ex, "Não foi possível salvar o profissional.");
                return Result<string>.Fail(ex.Message);
            }
        }

    }
}
