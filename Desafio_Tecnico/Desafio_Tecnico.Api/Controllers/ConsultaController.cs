using Desafio_Tecnico.Application.Consulta.DTOs;
using Desafio_Tecnico.Application.Consulta.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_Tecnico.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private readonly ICreateConsultaUseCase _consultaService;
        private readonly IObterDetalheConsultaUseCase _obterDetalheConsultaUseCase;
        private readonly IObterTodasConsultasUseCase _obterTodasConsultasUseCase;
        private readonly IObterAgendaPorProfissionalIdUseCase _obterAgendaPorProfissionalIdUseCase;


        public ConsultaController(IObterAgendaPorProfissionalIdUseCase obterAgendaPorProfissionalIdUseCase, ICreateConsultaUseCase emprestimoService, IObterDetalheConsultaUseCase obterDetalheConsultaUseCase, IObterTodasConsultasUseCase obterTodasConsultasUseCase)
        {
            _consultaService = emprestimoService;
            _obterDetalheConsultaUseCase = obterDetalheConsultaUseCase;
            _obterTodasConsultasUseCase = obterTodasConsultasUseCase;
            _obterAgendaPorProfissionalIdUseCase = obterAgendaPorProfissionalIdUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ConsultaDTO consultaDTO)
        {
            var consulta = await _consultaService.AddAsync(consultaDTO);

            if (!consulta.Success)
                return BadRequest(new { errors = new[] { consulta.Error } });
            return Created(string.Empty, new { data = consulta.Data });

        }

        [HttpGet("agenda-profissional/{profissionalid:int}")]
        public async Task<IActionResult> ObterAgendaProfissional(int profissionalid)
        {
            var agenda = await _obterAgendaPorProfissionalIdUseCase.ObterAgendaPorProfissionalIdAsync(profissionalid);

            if (!agenda.Success)
                return BadRequest(new { errors = new[] { agenda.Error } });
            return Created(string.Empty, new { data = agenda.Data });
        }


        [HttpGet("detalhes/{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {

            var consulta = await _obterDetalheConsultaUseCase.ObterPorIdAsync(id);

            if (!consulta.Success)
                return BadRequest(new { errors = new[] { consulta.Error } });
            return Created(string.Empty, new { data = consulta.Data });
        }

        [HttpGet()]
        public async Task<IActionResult> GelAll()
        {
            var consultas = await _obterTodasConsultasUseCase.ObterTodasConsultasIdAsync();

            if (!consultas.Success)
                return BadRequest(new { errors = new[] { consultas.Error } });
            return Created(string.Empty, new { data = consultas.Data });
        }
    }
}
