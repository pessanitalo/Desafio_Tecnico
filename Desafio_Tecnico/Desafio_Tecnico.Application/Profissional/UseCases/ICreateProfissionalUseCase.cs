using Desafio_Tecnico.Application.Profissional.DTOs;
using Desafio_Tecnico.Domain.Common;

namespace Desafio_Tecnico.Application.Profissional.UseCases
{
    public interface ICreateProfissionalUseCase
    {
        Task<Result<string>> AddAsync(ProfissionalDTO profissionalDTO);
    }
}
