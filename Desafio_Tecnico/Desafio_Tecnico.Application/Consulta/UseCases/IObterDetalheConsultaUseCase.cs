using Desafio_Tecnico.Application.Consulta.DTOs;
using Desafio_Tecnico.Domain.Common;


namespace Desafio_Tecnico.Application.Consulta.UseCases
{
    public interface IObterDetalheConsultaUseCase
    {
        Task<Result<ConsultaDetalheDTO>> ObterPorIdAsync(int id);
    }
}
