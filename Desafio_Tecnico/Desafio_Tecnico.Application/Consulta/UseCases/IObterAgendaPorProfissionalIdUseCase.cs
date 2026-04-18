using Desafio_Tecnico.Application.Profissional.DTOs;
using Desafio_Tecnico.Domain.Common;


namespace Desafio_Tecnico.Application.Consulta.UseCases
{
    public interface IObterAgendaPorProfissionalIdUseCase
    {
        Task<Result<IEnumerable<AgendaMedicoDTO>>> ObterAgendaPorProfissionalIdAsync(int profissionalId);
    }
}
