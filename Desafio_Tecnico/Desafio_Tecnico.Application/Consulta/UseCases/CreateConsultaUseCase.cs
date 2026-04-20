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
        private readonly IFeriadoRepository _feriadoRepository;
        protected readonly ILogger<CreateConsultaUseCase> _logger;

        public CreateConsultaUseCase(IConsultaRepository consultaRepository, ILogger<CreateConsultaUseCase> logger, IFeriadoRepository feriadoRepository)
        {
            _consultaRepository = consultaRepository;
            _feriadoRepository = feriadoRepository;
            _logger = logger;
        }

        public async Task<Result<string>> AddAsync(ConsultaDTO consultaDTO)
        {
            try
            {
                // o usuario não pode ter duas consultas no mesmo horário, mesmo profissional ou paciente.
                var consultaExistente = await _consultaRepository.GetByConsultaAgendada(
                            consultaDTO.PacienteId,
                            consultaDTO.ProfissionalId,
                            consultaDTO.DataConsulta);

                if (consultaExistente != null) return Result<string>.Fail("O usuario já possui uma consulta agendada neste dia.");

                var ehFeriado = await _feriadoRepository.EhFeriadoAsync(consultaDTO.DataConsulta);

                if (ehFeriado) return Result<string>.Fail("Data indisponível.");


                var temDisponibilidadeNoHorario = await _consultaRepository.TemDisponibilidadeNoHorario(
                            consultaDTO.PacienteId,
                            consultaDTO.ProfissionalId,
                            consultaDTO.DataConsulta,
                            consultaDTO.HoraConsulta);

                if (temDisponibilidadeNoHorario) return Result<string>.Fail("Já existe uma consulta agendada neste horário");

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
