using Desafio_Tecnico.Application.Profissional.DTOs;
using Desafio_Tecnico.Domain.Common;
using Desafio_Tecnico.Domain.Repositories;
using Desafio_Tecnico.Domain.Validation;
using Microsoft.Extensions.Logging;


namespace Desafio_Tecnico.Application.Consulta.UseCases
{
    public class ObterAgendaPorProfissionalIdUseCase : IObterAgendaPorProfissionalIdUseCase
    {
        private readonly IConsultaRepository _consultaRepository;
        protected readonly ILogger<CreateConsultaUseCase> _logger;

        public ObterAgendaPorProfissionalIdUseCase(IConsultaRepository consultaRepository, ILogger<CreateConsultaUseCase> logger)
        {
            _consultaRepository = consultaRepository;
            _logger = logger;
        }
        public async Task<Result<IEnumerable<AgendaMedicoDTO>>> ObterAgendaPorProfissionalIdAsync(int profissionalId)
        {
            try
            {
                var resultados = await _consultaRepository.ObterAgendaPorProfissionalIdAsync(profissionalId);

                var dto = resultados.Select(c => new AgendaMedicoDTO
                {
                    ConsultaId = c.ConsultaId,
                    ProfissionalId = c.ProfissionalId,
                    NomeProfissional = c.ProfissionalNome,
                    Data = c.DataConsulta.Data,
                    Hora = c.HoraConsulta.Hora,
                    PacienteId = c.PacienteId,
                    NomePaciente = c.PacienteNome
                }).ToList();

                return Result<IEnumerable<AgendaMedicoDTO>>.Ok(dto);
            }
            catch (DomainExceptionValidation ex)
            {
                _logger.LogWarning(ex, "Não foi possível consultar a agenda do profissional.");
                return Result<IEnumerable<AgendaMedicoDTO>>.Fail(ex.Message);
            }
        }
    }
}
