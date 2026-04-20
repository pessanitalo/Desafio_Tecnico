using Dapper;
using Desafio_Tecnico.Domain.Repositories;
using System.Data;


namespace Desafio_Tecnico.Infrastructure.Persistence.Repository
{
    public class FeriadoRepository : IFeriadoRepository
    {
        private readonly IDbConnection _connection;

        public FeriadoRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> EhFeriadoAsync(DateTime data)
        {
            const string sql = "SELECT 1 FROM Feriado WHERE Data = @Data";
            var resultado = await _connection.QueryFirstOrDefaultAsync(sql, new { Data = data.Date });
             return resultado != null;
        }
    }
}
