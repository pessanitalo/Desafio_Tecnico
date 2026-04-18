using Desafio_Tecnico.Domain.Entities;

namespace Desafio_Tecnico.Domain.Repositories
{
    public interface IUsuarioRepository 
    {
        Task<int> AddAsync(Usuario entity);
        Task<Usuario> GetByEmail(string cpf);
    }
}
