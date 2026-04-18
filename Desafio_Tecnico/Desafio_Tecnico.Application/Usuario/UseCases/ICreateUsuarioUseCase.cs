using Desafio_Tecnico.Application.Usuario.DTOs;
using Desafio_Tecnico.Domain.Common;

namespace Desafio_Tecnico.Application.Usuario.UseCases
{
    public interface ICreateUsuarioUseCase
    {
        Task<Result<string>> AddAsync(CreateUsuarioDTO usuarioDTO);
    }
}
