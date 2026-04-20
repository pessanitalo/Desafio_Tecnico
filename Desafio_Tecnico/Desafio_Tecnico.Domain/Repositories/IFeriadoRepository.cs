namespace Desafio_Tecnico.Domain.Repositories
{
    public interface IFeriadoRepository
    {
        Task<bool> EhFeriadoAsync(DateTime data);
    }
}
