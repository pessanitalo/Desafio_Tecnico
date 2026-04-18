using Desafio_Tecnico.Application.Consulta.DTOs;
using Desafio_Tecnico.Domain.Common;
using Desafio_Tecnico.Domain.Repositories;
using Desafio_Tecnico.Domain.Validation;
using Microsoft.Extensions.Logging;
using ConsultaDomain = Desafio_Tecnico.Domain.Entities.Consulta;

namespace Desafio_Tecnico.Application.Consulta.UseCases
{
    public class CreateConsultaUseCase : ICreateConsultaUseCase
    {
        private readonly IConsultaRepository _consultaRepository;
        protected readonly ILogger<CreateConsultaUseCase> _logger;

        public CreateConsultaUseCase(IConsultaRepository consultaRepository, ILogger<CreateConsultaUseCase> logger)
        {
            _consultaRepository = consultaRepository;
            _logger = logger;
        }

        public async Task<Result<string>> AddAsync(ConsultaDTO consultaDTO)
        {
            try
            {
                var temConflito = await _consultaRepository.TemConflito(
                            consultaDTO.PacienteId,
                            consultaDTO.ProfissionalId,
                            consultaDTO.DataConsulta,
                            consultaDTO.HoraConsulta);

                if (temConflito)
                {
                    return Result<string>.Fail(
                        "Já existe uma consulta agendada neste horário");
                }
                ;

                var consulta = ConsultaDomain.Criar(consultaDTO.DataConsulta, consultaDTO.HoraConsulta, consultaDTO.PacienteId, consultaDTO.ProfissionalId);

                await _consultaRepository.AddAsync(consulta);
                return Result<string>.Ok("Consulta agendada com sucesso.");
            }
            catch (DomainExceptionValidation ex)
            {
                _logger.LogWarning(ex, "Validação falhou ao agendar consulta.");
                return Result<string>.Fail(ex.Message);
            }

        }
    }
}
