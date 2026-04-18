using Desafio_Tecnico.Domain.Entities;

namespace Desafio_Tecnico.Domain.Repositories
{
    public interface IProfissionalRepository : IRepository<Profissional>
    {
        Task<IEnumerable<Profissional>> GetAllAsync();
        Task<Profissional> ObterProfissional(string cpf);
    }
}
