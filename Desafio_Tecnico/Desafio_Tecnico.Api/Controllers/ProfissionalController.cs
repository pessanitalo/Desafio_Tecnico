using Desafio_Tecnico.Application.Profissional.DTOs;
using Desafio_Tecnico.Application.Profissional.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_Tecnico.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfissionalController : ControllerBase
    {
        private readonly ICreateProfissionalUseCase _profissionalService;
        private readonly IGetAllProfissionalUseCase _getAllProfissionalUseCase;
        private readonly IObterProfissionalUseCase _obterProfissionalUseCase;

        public ProfissionalController(ICreateProfissionalUseCase emprestimoService, IGetAllProfissionalUseCase getAllProfissionalUseCase, IObterProfissionalUseCase obterProfissionalUseCase)
        {
            _profissionalService = emprestimoService;
            _getAllProfissionalUseCase = getAllProfissionalUseCase;
            _obterProfissionalUseCase = obterProfissionalUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProfissionalDTO profissionalDTO)
        {

            var profissional = await _profissionalService.AddAsync(profissionalDTO);

            if (!profissional.Success)
                return BadRequest(new { errors = new[] { profissional.Error } });
            return Created(string.Empty, new { data = profissional.Data });
        }

        [HttpGet]
        public async Task<IActionResult> GelAll()
        {
            var profissionais = await _getAllProfissionalUseCase.GetAllAsync();

            if (!profissionais.Success)
                return BadRequest(new { errors = new[] { profissionais.Error } });
            return Created(string.Empty, new { data = profissionais.Data });
        }

        [HttpGet("pesquisar/{cpf}")]
        public async Task<IActionResult> ObterPaciente(string cpf)
        {
            var profissional = await _obterProfissionalUseCase.ObterProfissional(cpf);

            if (!profissional.Success)
                return BadRequest(new { errors = new[] { profissional.Error } });
            return Created(string.Empty, new { data = profissional.Data });

        }
    }
}
