using Desafio_Tecnico.Domain.Validation;

namespace Desafio_Tecnico.Domain.ValueObjects
{
    public class CPF
    {
        public string Numero { get; set; }

        public CPF(string numero)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(numero),
                "CPF é obrigatório.");
            DomainExceptionValidation.When(numero.Length != 11,
                "O cpf precisa ter 11 caracteres.");
            Numero = numero;
        }
    }
}
