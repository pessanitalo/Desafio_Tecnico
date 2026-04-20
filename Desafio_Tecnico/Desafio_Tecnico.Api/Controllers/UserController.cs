using Desafio_Tecnico.Application.Usuario.DTOs;
using Desafio_Tecnico.Application.Usuario.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_Tecnico.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICreateUsuarioUseCase _usuarioService;
 
        public UserController(ICreateUsuarioUseCase emprestimoService)
        {
            _usuarioService = emprestimoService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUsuarioDTO usuarioDTO)
        {

            var resultado = await _usuarioService.AddAsync(usuarioDTO);

            if (!resultado.Success)
                return BadRequest(new { errors = new[] { resultado.Error } });
            return Created(string.Empty, new { data = resultado.Data });

        }
    }
}
