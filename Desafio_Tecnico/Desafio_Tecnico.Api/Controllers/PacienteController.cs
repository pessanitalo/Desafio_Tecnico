using Desafio_Tecnico.Application.Paciente.DTOs;
using Desafio_Tecnico.Application.Paciente.UseCases;
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

        public PacienteController(ICreatePacienteUseCase emprestimoService, IGetAllPacienteUseCase getAllPacienteUseCase, IPesquisarPacientePorCpfUseCase pesquisarPacientePorCpfUseCase)
        {
            _pacienteService = emprestimoService;
            _getAllPacienteUseCase = getAllPacienteUseCase;
            _pesquisarPacientePorCpfUseCase = pesquisarPacientePorCpfUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create(PacienteDTO profissionalDTO)
        {

            var paciente = await _pacienteService.AddAsync(profissionalDTO);

            if (!paciente.Success)
                return BadRequest(new { errors = new[] { paciente.Error } });
            return Created(string.Empty, new { data = paciente.Data });
        }

        [HttpGet]
        public async Task<IActionResult> GelAll()
        {
            var pacientes = await _getAllPacienteUseCase.GetAllAsync();

            if (!pacientes.Success)
                return BadRequest(new { errors = new[] { pacientes.Error } });
            return Created(string.Empty, new { data = pacientes.Data });
        }

        [HttpGet("pesquisar/{cpf}")]
        public async Task<IActionResult> ObterPaciente(string cpf)
        {

            var paciente = await _pesquisarPacientePorCpfUseCase.ObterPacientePorCpf(cpf);

            if (!paciente.Success)
                return BadRequest(new { errors = new[] { paciente.Error } });
            return Created(string.Empty, new { data = paciente.Data });
        }
    }
}
