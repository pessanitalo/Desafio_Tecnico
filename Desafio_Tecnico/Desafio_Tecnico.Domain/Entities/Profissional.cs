using Desafio_Tecnico.Domain.ValueObjects;


namespace Desafio_Tecnico.Domain.Entities
{
    public class Profissional
    {
        public int ProfissionalId { get; set; }
        public string Nome { get; private set; }
        public string CPF { get; private set; }

        public PessoaFisica Pessoa => new PessoaFisica(Nome, CPF);

        public Profissional() { }

 
        public static Profissional Criar(PessoaFisica pessoa)
        {
            return new Profissional
            {
                Nome = pessoa.Nome,
                CPF = pessoa.CPF
            };
        }

    }
}
