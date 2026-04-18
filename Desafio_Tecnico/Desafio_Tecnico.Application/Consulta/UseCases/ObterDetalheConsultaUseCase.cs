using Desafio_Tecnico.Application.Consulta.DTOs;
using Desafio_Tecnico.Application.Profissional.DTOs;
using Desafio_Tecnico.Domain.Common;
using Desafio_Tecnico.Domain.Repositories;
using Desafio_Tecnico.Domain.Validation;
using Microsoft.Extensions.Logging;


namespace Desafio_Tecnico.Application.Consulta.UseCases
{
    public class ObterDetalheConsultaUseCase : IObterDetalheConsultaUseCase
    {
        private readonly IConsultaRepository _consultaRepository;
        protected readonly ILogger<ObterDetalheConsultaUseCase> _logger;

        public ObterDetalheConsultaUseCase(IConsultaRepository pacienteRepository, ILogger<ObterDetalheConsultaUseCase> logger)
        {
            _consultaRepository = pacienteRepository;
            _logger = logger;
        }

        public async Task<Result<ConsultaDetalheDTO>> ObterPorIdAsync(int id)
        {
            try
            {
                var consulta = await _consultaRepository.ObterPorIdAsync(id);

                if (consulta == null)
                    return Result<ConsultaDetalheDTO>.Fail("Consulta não encontrada");

                var dto = new ConsultaDetalheDTO
                {
                    Id = consulta.ConsultaId,
                    PacienteNome = consulta.PacienteNome,
                    ProfissionalNome = consulta.ProfissionalNome,
                    DataConsulta = consulta.DataConsulta.Data,
                    HoraConsulta = consulta.HoraConsulta.Hora
                };

                return Result<ConsultaDetalheDTO>.Ok(dto);
            }
            catch (DomainExceptionValidation ex)
            {
                _logger.LogWarning(ex, "Não foi possível obter a consultar.");
                return Result<ConsultaDetalheDTO>.Fail(ex.Message);
            }

        }
    }
}
