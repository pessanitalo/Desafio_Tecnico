using Dapper;
using Desafio_Tecnico.Application.UsuarioDTO.DTOs;
using Desafio_Tecnico.Domain.Entities;
using Desafio_Tecnico.Domain.Repositories;
using Desafio_Tecnico.Domain.ValueObjects;
using System.Data;

namespace Desafio_Tecnico.Infrastructure.Persistence.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbConnection _connection;

        public UsuarioRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<int> AddAsync(Usuario usuario)
        {
            var sql = @"
            INSERT INTO Usuario (Email, Senha)
            VALUES (@Email, @Senha);
            
            SELECT SCOPE_IDENTITY();";

            var parametros = new
            {
                Email = usuario.Email.Valor,
                Senha = usuario.Senha.Valor
            };

            var id = await _connection.QuerySingleAsync<int>(sql, parametros);

            return id;

        }

        public async Task<Usuario> GetByEmail(string email)
        {
            const string sql = "SELECT * FROM Usuario WHERE Email = @Email";

            var result = await _connection.QueryFirstOrDefaultAsync<UsuarioDTO>(
                sql,
                new { Email = email });

            if (result == null)
                return null;

            return new Usuario(
                result.UsuarioId,
                new Email(result.Email),
                Senha.ReconstituirDoRepository(result.Senha)
            );
        }
    }
}
