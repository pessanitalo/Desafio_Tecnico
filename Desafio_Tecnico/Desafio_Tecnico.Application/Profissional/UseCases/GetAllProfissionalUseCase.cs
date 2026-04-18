using Desafio_Tecnico.Application.DTOs;
using Desafio_Tecnico.Application.Profissional.DTOs;
using Desafio_Tecnico.Domain.Common;
using Desafio_Tecnico.Domain.Repositories;
using Desafio_Tecnico.Domain.Validation;
using Microsoft.Extensions.Logging;

namespace Desafio_Tecnico.Application.Profissional.UseCases
{
    public class GetAllProfissionalUseCase : IGetAllProfissionalUseCase
    {
        private readonly IProfissionalRepository _profissionalRepository;
        protected readonly ILogger<GetAllProfissionalUseCase> _logger;

        public GetAllProfissionalUseCase(IProfissionalRepository profissionalRepository, ILogger<GetAllProfissionalUseCase> logger)
        {
            _profissionalRepository = profissionalRepository;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<ProfissionalDTO>>> GetAllAsync()
        {
            try
            {
                var profissionais = await _profissionalRepository.GetAllAsync();

                var profissionaisDto = profissionais.Select(p => new ProfissionalDTO
                {
                    Profissional = new PessoaFisicaDTO
                    {
                        Nome = p.Pessoa.Nome,
                        CPF = p.Pessoa.CPF
                    }
                });
                return Result<IEnumerable<ProfissionalDTO>>.Ok(profissionaisDto);
            }
            catch (DomainExceptionValidation ex)
            {
                _logger.LogError(ex, "Não foi possível obter todos os profissionais.");
                return Result<IEnumerable<ProfissionalDTO>>.Fail(ex.Message);
            }
        }
    }
}
