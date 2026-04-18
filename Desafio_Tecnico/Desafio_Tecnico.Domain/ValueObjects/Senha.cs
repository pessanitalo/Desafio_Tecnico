using Desafio_Tecnico.Domain.Validation;

namespace Desafio_Tecnico.Domain.ValueObjects
{
    public class Senha
    {
        public string Valor { get; set; }

        public Senha(string valor)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(valor),
                "A senha é obrigatório.");
            DomainExceptionValidation.When(valor.Length < 6,
                "A senha precisa ter no minimo seis caracteres.");
            Valor = valor;
        }
    }
}
