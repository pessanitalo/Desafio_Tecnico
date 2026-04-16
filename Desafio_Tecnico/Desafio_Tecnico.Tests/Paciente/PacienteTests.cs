using Desafio_Tecnico.Domain.ValueObjects;
using Desafio_Tecnico.Domain.Validation;

namespace Desafio_Tecnico.Tests.Paciente
{
    public class PacienteTests
    {
        #region Nome Validation

        [Fact]
        public void Criar_Paciente_SemNome_DeveLancarExcecao()
        {
            Assert.Throws<DomainExceptionValidation>(() => new PessoaFisica(null!, "12345678901"));
        }

        [Fact]
        public void Criar_Paciente_ComNomeVazio_DeveLancarExcecao()
        {
            Assert.Throws<DomainExceptionValidation>(() => new PessoaFisica("", "12345678901"));
        }

        [Fact]
        public void Criar_Paciente_ComNomeMenorQue3Caracteres_DeveLancarExcecao()
        {
            Assert.Throws<DomainExceptionValidation>(() => new PessoaFisica("AB", "12345678901"));
        }

        [Fact]
        public void Criar_Paciente_ComNomeValido_DeveCriarComSucesso()
        {
            var nome = "João Silva";
            var cpf = "12345678901";

            var pessoa = new PessoaFisica(nome, cpf);

            Assert.Equal(nome, pessoa.Nome);
            Assert.Equal(cpf, pessoa.CPF);
        }

        #endregion

        #region CPF Validation

        [Fact]
        public void Criar_Paciente_SemCPF_DeveLancarExcecao()
        {
            Assert.Throws<DomainExceptionValidation>(() => new PessoaFisica("João Silva", null!));
        }

        [Fact]
        public void Criar_Paciente_ComCPFVazio_DeveLancarExcecao()
        {
            Assert.Throws<DomainExceptionValidation>(() => new PessoaFisica("João Silva", ""));
        }

        [Fact]
        public void Criar_Paciente_ComCPFMenorQue11Caracteres_DeveLancarExcecao()
        {
            Assert.Throws<DomainExceptionValidation>(() => new PessoaFisica("João Silva", "1234567890"));
        }

        [Fact]
        public void Criar_Paciente_ComCPFMaiorQue11Caracteres_DeveLancarExcecao()
        {
            Assert.Throws<DomainExceptionValidation>(() => new PessoaFisica("João Silva", "123456789012"));
        }

        [Fact]
        public void Criar_Paciente_ComCPFValido_DeveCriarComSucesso()
        {
            var nome = "João Silva";
            var cpf = "12345678901";

            var pessoa = new PessoaFisica(nome, cpf);

            Assert.Equal(cpf, pessoa.CPF);
        }

        #endregion

        #region Paciente Creation

        [Fact]
        public void Paciente_Criar_DeveRetornarPacienteComDadosCorretos()
        {
            var pessoa = new PessoaFisica("Maria Santos", "98765432109");

            var paciente = Domain.Entities.Paciente.Criar(pessoa);

            Assert.NotNull(paciente);
            Assert.Equal("Maria Santos", paciente.Nome);
            Assert.Equal("98765432109", paciente.CPF);
        }

        #endregion
    }
}
