using Desafio_Tecnico.Application.DTOs;
using Desafio_Tecnico.Application.Login.DTOs;
using Desafio_Tecnico.Domain.Common;

namespace Desafio_Tecnico.Application.Login.UseCases
{
    public interface ILoginUseCase
    {
        Task<Result<LoginResponse>> Execute(LoginDTO dto);
    }
}
