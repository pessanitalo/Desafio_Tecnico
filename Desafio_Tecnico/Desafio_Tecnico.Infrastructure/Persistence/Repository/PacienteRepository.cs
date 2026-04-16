using Dapper;
using Desafio_Tecnico.Domain.Entities;
using Desafio_Tecnico.Domain.Repositories;
using System.Data;

namespace Desafio_Tecnico.Infrastructure.Persistence.Repository
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly IDbConnection _connection;

        public PacienteRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task AddAsync(Paciente paciente)
        {
            var sql = @"
            INSERT INTO Paciente (Nome, CPF)
            VALUES (@Nome, @CPF);
            
            SELECT SCOPE_IDENTITY();";

            var parametros = new
            {
                Nome = paciente.Pessoa.Nome,
                CPF = paciente.Pessoa.CPF
            };

            var id = await _connection.ExecuteAsync(sql, parametros);
        }

        public async Task<IEnumerable<Paciente>> GetAllAsync()
        {
            const string sql = "SELECT * FROM Paciente";
            return await _connection.QueryAsync<Paciente>(sql);
        }

        public async Task<Paciente> PesquisarPacientePorCpf(string cpf)
        {
            const string sql = "SELECT * FROM Paciente WHERE CPF = @CPF";
            return await _connection.QueryFirstOrDefaultAsync<Paciente>(sql, new { CPF = cpf });
        }

    }
}
