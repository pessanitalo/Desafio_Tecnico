using Desafio_Tecnico.Domain.ValueObjects;
using Desafio_Tecnico.Domain.Validation;

namespace Desafio_Tecnico.Tests.Profissional
{
    public class ProfissionalTests
    {
        #region Nome Validation

        [Fact]
        public void Criar_Profissional_SemNome_DeveLancarExcecao()
        {
            Assert.Throws<DomainExceptionValidation>(() => new PessoaFisica(null!, "12345678901"));
        }

        [Fact]
        public void Criar_Profissional_ComNomeVazio_DeveLancarExcecao()
        {
            Assert.Throws<DomainExceptionValidation>(() => new PessoaFisica("", "12345678901"));
        }

        [Fact]
        public void Criar_Profissional_ComNomeMenorQue3Caracteres_DeveLancarExcecao()
        {
            Assert.Throws<DomainExceptionValidation>(() => new PessoaFisica("AB", "12345678901"));
        }

        [Fact]
        public void Criar_Profissional_ComNomeValido_DeveCriarComSucesso()
        {
            var nome = "Dr. João Silva";
            var cpf = "12345678901";

            var pessoa = new PessoaFisica(nome, cpf);

            Assert.Equal(nome, pessoa.Nome);
            Assert.Equal(cpf, pessoa.CPF);
        }

        #endregion

        #region CPF Validation

        [Fact]
        public void Criar_Profissional_SemCPF_DeveLancarExcecao()
        {
            Assert.Throws<DomainExceptionValidation>(() => new PessoaFisica("Dr. João Silva", null!));
        }

        [Fact]
        public void Criar_Profissional_ComCPFVazio_DeveLancarExcecao()
        {
            Assert.Throws<DomainExceptionValidation>(() => new PessoaFisica("Dr. João Silva", ""));
        }

        [Fact]
        public void Criar_Profissional_ComCPFMenorQue11Caracteres_DeveLancarExcecao()
        {
            Assert.Throws<DomainExceptionValidation>(() => new PessoaFisica("Dr. João Silva", "1234567890"));
        }

        [Fact]
        public void Criar_Profissional_ComCPFMaiorQue11Caracteres_DeveLancarExcecao()
        {
            Assert.Throws<DomainExceptionValidation>(() => new PessoaFisica("Dr. João Silva", "123456789012"));
        }

        [Fact]
        public void Criar_Profissional_ComCPFValido_DeveCriarComSucesso()
        {
            var nome = "Dr. João Silva";
            var cpf = "12345678901";

            var pessoa = new PessoaFisica(nome, cpf);

            Assert.Equal(cpf, pessoa.CPF);
        }

        #endregion

        #region Profissional Creation

        [Fact]
        public void Profissional_Criar_DeveRetornarProfissionalComDadosCorretos()
        {
            var pessoa = new PessoaFisica("Dra. Maria Santos", "98765432109");

            var profissional = Domain.Entities.Profissional.Criar(pessoa);

            Assert.NotNull(profissional);
            Assert.Equal("Dra. Maria Santos", profissional.Nome);
            Assert.Equal("98765432109", profissional.CPF);
        }

        #endregion
    }
}
