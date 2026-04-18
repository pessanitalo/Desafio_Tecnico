using Desafio_Tecnico.Application.Consulta.DTOs;
using Desafio_Tecnico.Domain.Common;
using Desafio_Tecnico.Domain.Repositories;
using Desafio_Tecnico.Domain.Validation;
using Microsoft.Extensions.Logging;


namespace Desafio_Tecnico.Application.Consulta.UseCases
{
    public class ObterTodasConsultasUseCase : IObterTodasConsultasUseCase
    {
        private readonly IConsultaRepository _consultaRepository;
        protected readonly ILogger<ObterTodasConsultasUseCase> _logger;

        public ObterTodasConsultasUseCase(IConsultaRepository pacienteRepository, ILogger<ObterTodasConsultasUseCase> logger)
        {
            _consultaRepository = pacienteRepository;
            _logger = logger;
        }
        public async Task<Result<IEnumerable<ConsultaDetalheDTO>>> ObterTodasConsultasIdAsync()
        {
            try
            {
                var consultas = await _consultaRepository.ObterTodasConsultasIdAsync();
                if (consultas == null)
                    return Result<IEnumerable<ConsultaDetalheDTO>>.Fail("Consulta não encontrada");

                var dtos = consultas.Select(c => new ConsultaDetalheDTO
                {
                    Id = c.ConsultaId,
                    PacienteNome = c.PacienteNome,
                    ProfissionalNome = c.ProfissionalNome,
                    ProfissionalId = c.ProfissionalId,
                    DataConsulta = c.DataConsulta.Data,
                    HoraConsulta = c.HoraConsulta.Hora
                }).ToList();

                return Result<IEnumerable<ConsultaDetalheDTO>>.Ok(dtos);
            }
            catch (DomainExceptionValidation ex)
            {
                _logger.LogWarning(ex, "Não foi possível obter as consultas.");
                return Result<IEnumerable<ConsultaDetalheDTO>>.Fail(ex.Message);
            }
        }

    }
}
