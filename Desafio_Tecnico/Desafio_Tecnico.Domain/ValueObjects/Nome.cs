
using Desafio_Tecnico.Domain.Validation;

namespace Desafio_Tecnico.Domain.ValueObjects
{
    public class Nome
    {
        public string Valor { get; set; }

        public Nome(string valor)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(valor),
                "O nome do paciente é obrigatório.");
            DomainExceptionValidation.When(valor.Length < 3,
                "O nome do paciente precisa ter no minimo tres caracteres.");
            Valor = valor;
        }
    }
}
