using Desafio_Tecnico.Domain.Service;
using Microsoft.AspNetCore.Identity;

namespace Desafio_Tecnico.Infrastructure.Services
{
    public class PasswordHasherService : IPasswordHasherService
    {
        private readonly PasswordHasher<object> _hasher = new();

        public string Hash(string senha)
        {
            return _hasher.HashPassword(null, senha);
        }

        public bool Verificar(string senha, string hash)
        {
            var result = _hasher.VerifyHashedPassword(null, hash, senha);
            return result == PasswordVerificationResult.Success;
        }
    }
}
