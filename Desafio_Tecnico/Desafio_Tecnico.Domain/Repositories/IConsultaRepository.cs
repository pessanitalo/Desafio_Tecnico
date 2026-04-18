using Desafio_Tecnico.Domain.Entities;

namespace Desafio_Tecnico.Domain.Repositories
{
    public interface IConsultaRepository : IRepository<Consulta>
    {
        Task<Consulta> ObterPorIdAsync(int id);
        Task<IEnumerable<Consulta>> ObterTodasConsultasIdAsync();
        Task<Consulta> GetByConsultaAgendada(int pacienteId, int profissionalId, DateTime dataConsulta, TimeSpan horaConsulta);
        Task<IEnumerable<Consulta>> ObterAgendaPorProfissionalIdAsync(int id);
        Task<bool> TemConflito(int pacienteId, int profissionalId, DateTime dataConsulta, TimeSpan horaConsulta);
    }
}
