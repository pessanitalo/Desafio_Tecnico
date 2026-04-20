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
                var usuarioExistente = await _usuarioRepository.GetByEmail(usuarioDTO.Email);
                if (usuarioExistente != null) return Result<string>.Fail("Já existe um usuario com esse e-mail.");

                var usuario = UsuarioDomain.Criar(usuarioDTO.Email, usuarioDTO.Senha);
                usuario.Senha = Domain.ValueObjects.Senha.ReconstituirDoRepository(
                    _passwordHasher.Hash(usuario.Senha.Valor));

                await _usuarioRepository.AddAsync(usuario);
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
