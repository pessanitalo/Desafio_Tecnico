using Desafio_Tecnico.Application.Login.DTOs;
using Desafio_Tecnico.Application.Login.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_Tecnico.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ILoginUseCase _pacienteService;

        public LoginController(ILoginUseCase emprestimoService)
        {
            _pacienteService = emprestimoService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(LoginDTO loginDTO)
        {

            var login = await _pacienteService.Execute(loginDTO);

            if (!login.Success)
                return BadRequest(new { errors = new[] { login.Error } });
            return Created(string.Empty, new { data = login.Data });
        }
    }
}
