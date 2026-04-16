using Desafio_Tecnico.Domain.Entities;

namespace Desafio_Tecnico.Domain.Repositories
{
    public interface IPacienteRepository :  IRepository<Paciente>
    {
        Task<IEnumerable<Paciente>> GetAllAsync();
        Task<Paciente> PesquisarPacientePorCpf(string cpf);
    }
}
