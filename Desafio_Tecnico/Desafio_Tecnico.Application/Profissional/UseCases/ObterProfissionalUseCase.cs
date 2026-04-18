using Desafio_Tecnico.Application.Profissional.DTOs;
using Desafio_Tecnico.Domain.Common;
using Desafio_Tecnico.Domain.Repositories;
using Desafio_Tecnico.Domain.Validation;
using Microsoft.Extensions.Logging;


namespace Desafio_Tecnico.Application.Profissional.UseCases
{
    public class ObterProfissionalUseCase : IObterProfissionalUseCase
    {
        private readonly IProfissionalRepository _profissionalRepository;
        protected readonly ILogger<ObterProfissionalUseCase> _logger;

        public ObterProfissionalUseCase(IProfissionalRepository profissionalRepository, ILogger<ObterProfissionalUseCase> logger)
        {
            _profissionalRepository = profissionalRepository;
            _logger = logger;
        }
        public async Task<Result<ObterProfissionalDTO>> ObterProfissional(string cpf)
        {
            try
            {
                var profissional = await _profissionalRepository.ObterProfissional(cpf);

                if (profissional == null)
                    return Result<ObterProfissionalDTO>.Fail("Medico não encontrado");

                var profissionalDto = new ObterProfissionalDTO
                {
                    ProfissionalId = profissional.ProfissionalId,
                    Nome = profissional.Nome,
                    CPF = profissional.CPF
                };

                return Result<ObterProfissionalDTO>.Ok(profissionalDto);
            }
            catch (DomainExceptionValidation ex)
            {
                _logger.LogError(ex, "Não foi possível encontrar o médico.");
                return Result<ObterProfissionalDTO>.Fail(ex.Message);
            }
        }
    }
}
