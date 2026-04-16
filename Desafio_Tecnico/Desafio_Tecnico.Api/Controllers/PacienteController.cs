using Desafio_Tecnico.Application.Paciente.DTOs;
using Desafio_Tecnico.Application.Paciente.UseCases;
using Desafio_Tecnico.Domain.Validation;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_Tecnico.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly ICreatePacienteUseCase _pacienteService;
        private readonly IGetAllPacienteUseCase _getAllPacienteUseCase;
        private readonly IPesquisarPacientePorCpfUseCase _pesquisarPacientePorCpfUseCase;
        protected readonly ILogger<PacienteController> _logger;

        public PacienteController(ICreatePacienteUseCase emprestimoService, ILogger<PacienteController> logger, IGetAllPacienteUseCase getAllPacienteUseCase, IPesquisarPacientePorCpfUseCase pesquisarPacientePorCpfUseCase)
        {
            _pacienteService = emprestimoService;
            _logger = logger;
            _getAllPacienteUseCase = getAllPacienteUseCase;
            _pesquisarPacientePorCpfUseCase = pesquisarPacientePorCpfUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create(PacienteDTO profissionalDTO)
        {
            try
            {
                var resultado = await _pacienteService.AddAsync(profissionalDTO);

                if (!resultado.Success)
                    return BadRequest(new { errors = new[] { resultado.Error } });
                return Created(string.Empty, new { data = resultado.Data });
            }
            catch (DomainExceptionValidation ex)
            {
                _logger.LogWarning("Erro de validação ao criar cliente: {erro}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao criar cliente");
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GelAll()
        {
            try
            {
                var resultado = await _getAllPacienteUseCase.GetAllAsync();

                if (!resultado.Success)
                    return BadRequest(new { errors = new[] { resultado.Error } });
                return Created(string.Empty, new { data = resultado.Data });
            }
            catch (DomainExceptionValidation ex)
            {
                _logger.LogWarning("Erro de validação ao criar cliente: {erro}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao criar cliente");
                throw;
            }
        }

        [HttpGet("pesquisar/{cpf}")]
        public async Task<IActionResult> ObterPaciente(string cpf)
        {
            try
            {
                var resultado = await _pesquisarPacientePorCpfUseCase.ObterPacientePorCpf(cpf);

                if (!resultado.Success)
                    return BadRequest(new { errors = new[] { resultado.Error } });
                return Created(string.Empty, new { data = resultado.Data });
            }
            catch (DomainExceptionValidation ex)
            {
                _logger.LogWarning("Erro de validação ao criar cliente: {erro}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao criar cliente");
                throw;
            }
        }
    }
}
