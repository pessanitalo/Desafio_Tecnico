using Dapper;
using Desafio_Tecnico.Domain.Entities;
using Desafio_Tecnico.Domain.Repositories;
using System.Data;

namespace Desafio_Tecnico.Infrastructure.Persistence.Repository
{
    public class ProfissionalRepository : IProfissionalRepository
    {
        private readonly IDbConnection _connection;

        public ProfissionalRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task AddAsync(Profissional profissional)
        {
            var sql = @"
            INSERT INTO Profissional (Nome, CPF)
            VALUES (@Nome, @CPF);
            
            SELECT SCOPE_IDENTITY();";

            var parametros = new
            {
                Nome = profissional.Pessoa.Nome,
                CPF = profissional.Pessoa.CPF
            };

            var id = await _connection.ExecuteAsync(sql, parametros);
        }

        public async Task<IEnumerable<Profissional>> GetAllAsync()
        {
            const string sql = "SELECT * FROM Profissional";
            return await _connection.QueryAsync<Profissional>(sql);
        }

        public async Task<Profissional> ObterProfissional(string cpf)
        {
            const string sql = "SELECT * FROM Profissional WHERE CPF = @CPF";
            return await _connection.QueryFirstOrDefaultAsync<Profissional>(sql, new { CPF = cpf });
        }
    }
}
