using Dapper;
using Desafio_Tecnico.Domain.Entities;
using Desafio_Tecnico.Domain.Repositories;
using System.Data;

namespace Desafio_Tecnico.Infrastructure.Persistence.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IDbConnection _connection;

        public RefreshTokenRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task AddAsync(RefreshToken refreshToken)
        {
            var sql = @"
            INSERT INTO RefreshToken (UsuarioId, Token, Expiracao, Revogado)
            VALUES (@UsuarioId, @Token, @Expiracao, @Revogado);
            
            SELECT SCOPE_IDENTITY();";

            var parametros = new
            {
                UsuarioId = refreshToken.UsuarioId,
                Token = refreshToken.Token,
                Expiracao = refreshToken.Expiracao,
                Revogado = refreshToken.Revogado,
            };

            var id = await _connection.ExecuteAsync(sql, parametros);
        }
    }
}
