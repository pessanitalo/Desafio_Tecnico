using Desafio_Tecnico.Application.Usuario.DTOs;
using Desafio_Tecnico.Domain.Common;
using Desafio_Tecnico.Domain.Repositories;
using Desafio_Tecnico.Domain.Service;
using Desafio_Tecnico.Domain.Validation;
using Microsoft.Extensions.Logging;
using UsuarioDomain = Desafio_Tecnico.Domain.Entities.Usuario;

namespace Desafio_Tecnico.Application.Usuario.UseCases
{
    public class CreateUsuarioUseCase : ICreateUsuarioUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPasswordHasherService _passwordHasher;
        protected readonly ILogger<CreateUsuarioUseCase> _logger;

        public CreateUsuarioUseCase(IUsuarioRepository usuarioRepository, ILogger<CreateUsuarioUseCase> logger, IPasswordHasherService passwordHasher)
        {
            _usuarioRepository = usuarioRepository;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<string>> AddAsync(CreateUsuarioDTO usuarioDTO)
        {
            try
            {
                var senhaHash = _passwordHasher.Hash(usuarioDTO.Senha);
                var profissional = UsuarioDomain.Criar(usuarioDTO.Email, senhaHash);
                await _usuarioRepository.AddAsync(profissional);
                return Result<string>.Ok("Usuario salvo com sucesso.");
            }
            catch (DomainExceptionValidation ex)
            {
                _logger.LogError(ex, "Não foi possível salvar o usuário.");
                return Result<string>.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Não foi possível salvar o usuário.");
                return Result<string>.Fail($"Não foi possível salvar o usuário.");
            }
        }
    }

}
