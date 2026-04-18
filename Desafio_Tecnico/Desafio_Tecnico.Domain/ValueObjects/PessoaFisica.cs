using Desafio_Tecnico.Domain.Validation;

namespace Desafio_Tecnico.Domain.ValueObjects
{
    public class PessoaFisica : ValueObject
    {
        public string Nome { get; private set; }
        public string CPF { get; private set; }

        public PessoaFisica(string nome, string cpf)
        {
            DomainExceptionValidation.When(
                string.IsNullOrEmpty(nome),
                "Nome é obrigatório"
            );

            DomainExceptionValidation.When(nome.Length < 3, "Nome deve ter no mínimo 3 caracteres");

            DomainExceptionValidation.When(string.IsNullOrEmpty(cpf), "CPF é obrigatório.");

            DomainExceptionValidation.When(cpf.Length != 11, "O CPF precisa ter onze caracteres.");

            Nome = nome;
            CPF = cpf;
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Nome;
            yield return CPF;
        }
    }
}
