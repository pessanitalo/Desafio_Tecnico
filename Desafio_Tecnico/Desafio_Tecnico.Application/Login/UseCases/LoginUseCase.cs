using Desafio_Tecnico.Application.DTOs;
using Desafio_Tecnico.Application.Login.DTOs;
using Desafio_Tecnico.Domain.Common;
using Desafio_Tecnico.Domain.Entities;
using Desafio_Tecnico.Domain.Repositories;
using Desafio_Tecnico.Domain.Service;
using Microsoft.Extensions.Logging;


namespace Desafio_Tecnico.Application.Login.UseCases
{
    public class LoginUseCase : ILoginUseCase
    {

        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenService;
        protected readonly ILogger<LoginUseCase> _logger;

        public LoginUseCase(IRefreshTokenRepository refreshTokenRepository, IUsuarioRepository usuarioRepository, ILogger<LoginUseCase> logger, ITokenService tokenService)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _usuarioRepository = usuarioRepository;
            _logger = logger;
            _tokenService = tokenService;
        }

        public async Task<Result<LoginResponse>> Execute(LoginDTO dto)
        {
            try
            {
                // 1 buscar o usuario
                var usuario = await _usuarioRepository.GetByEmail(dto.Email);

                // 2 gerar o token eo refresh token

                var token = _tokenService.GerarToken(usuario.UsuarioId, dto.Email);
                var refresfToken = _tokenService.GerarRefreshToken();

                // 3 salvar o refresh token
                await _refreshTokenRepository.AddAsync(new RefreshToken
                {
                    UsuarioId = usuario.UsuarioId,
                    Token = refresfToken,
                    Expiracao = DateTime.UtcNow.AddDays(7)
                });
                var response = new LoginResponse(token, refresfToken);
                return Result<LoginResponse>.Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao realizar login.");
                return Result<LoginResponse>.Fail("Erro ao realizar login.");
            }
        }
    }
}
