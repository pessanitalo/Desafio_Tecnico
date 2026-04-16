using Desafio_Tecnico.Application.Paciente.DTOs;
using Desafio_Tecnico.Domain.Common;


namespace Desafio_Tecnico.Application.Paciente.UseCases
{
    public interface IGetAllPacienteUseCase
    {
        Task<Result<IEnumerable<PacienteDTO>>> GetAllAsync();
    }
}
