using System.Linq;
using Desafio_Tecnico.Domain.Validation;

namespace Desafio_Tecnico.Domain.ValueObjects
{
    public class Senha
    {
        public string Valor { get; set; }

        private Senha() { }

        public Senha(string valor)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(valor),
                "A senha é obrigatória.");

            var senhaNormalizada = valor.Trim();

            DomainExceptionValidation.When(senhaNormalizada.Length < 6,
                "A senha precisa ter no minimo seis caracteres.");
            DomainExceptionValidation.When(!senhaNormalizada.Any(char.IsUpper),
                "A senha precisa ter pelo menos uma letra maiúscula.");
            DomainExceptionValidation.When(!senhaNormalizada.Any(char.IsLower),
                "A senha precisa ter pelo menos uma letra minúscula.");
            DomainExceptionValidation.When(!senhaNormalizada.Any(char.IsDigit),
                "A senha precisa ter pelo menos um número.");
            DomainExceptionValidation.When(!senhaNormalizada.Any(ch => !char.IsLetterOrDigit(ch)),
                "A senha precisa ter pelo menos um caractere especial.");

            Valor = senhaNormalizada;
        }

        public static Senha ReconstituirDoRepository(string valor)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(valor),
                "A senha é obrigatória.");

            return new Senha
            {
                Valor = valor
            };
        }
    }
}
