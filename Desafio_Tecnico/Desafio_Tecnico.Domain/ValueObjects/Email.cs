using Desafio_Tecnico.Domain.Validation;

namespace Desafio_Tecnico.Domain.ValueObjects
{
    public class Email
    {
        public string Valor { get; set; }

        public Email(string valor)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(valor),
                "O e-mail é obrigatório.");
            DomainExceptionValidation.When(valor.Length < 3,
                "O e-mail precisa ter no minimo tres caracteres.");
            Valor = valor;
        }
    }
}
