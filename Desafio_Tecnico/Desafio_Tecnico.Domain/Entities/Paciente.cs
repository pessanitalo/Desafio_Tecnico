using Desafio_Tecnico.Domain.ValueObjects;

namespace Desafio_Tecnico.Domain.Entities
{
    public class Paciente
    {
        public int PacienteId { get; set; }
        public string Nome { get; private set; }
        public string CPF { get; private set; }

        public PessoaFisica Pessoa => new PessoaFisica(Nome, CPF);

        private Paciente() { }

        public static Paciente Criar(PessoaFisica pessoa)
        {
            return new Paciente
            {
                Nome = pessoa.Nome,
                CPF = pessoa.CPF
            };
        }
    }
}
