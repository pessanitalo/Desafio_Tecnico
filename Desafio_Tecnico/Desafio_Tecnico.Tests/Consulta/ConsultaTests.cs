using Desafio_Tecnico.Domain.ValueObjects;
using Desafio_Tecnico.Domain.Validation;

namespace Desafio_Tecnico.Tests.Consulta
{
    public class ConsultaTests
    {
        #region Data Validation

        [Fact]
        public void Criar_Consulta_DataNoPassado_DeveLancarExcecao()
        {
            var dataPassada = DateTime.Today.AddDays(-1);
            var horaValida = TimeSpan.FromHours(10);

            Assert.Throws<DomainExceptionValidation>(() =>
                new DataConsulta(dataPassada));
        }

        [Fact]
        public void Criar_Consulta_DataHoje_DeveCriarComSucesso()
        {
            var dataHoje = DateTime.Today;
            var horaValida = TimeSpan.FromHours(10);

            var dataConsulta = new DataConsulta(dataHoje);

            Assert.Equal(dataHoje, dataConsulta.Data);
        }

        [Fact]
        public void Criar_Consulta_DataFutura_DeveCriarComSucesso()
        {
            var dataFutura = DateTime.Today.AddDays(5);
            var horaValida = TimeSpan.FromHours(10);

            var dataConsulta = new DataConsulta(dataFutura);

            Assert.Equal(dataFutura.Date, dataConsulta.Data);
        }

        #endregion

        #region Hora Validation

        [Fact]
        public void Criar_Consulta_HoraAntesDas8_DeveLancarExcecao()
        {
            var horaAntesDas8 = TimeSpan.FromHours(7);

            Assert.Throws<DomainExceptionValidation>(() =>
                new HoraConsulta(horaAntesDas8));
        }

        [Fact]
        public void Criar_Consulta_HoraDepoisDas18_DeveLancarExcecao()
        {
            var horaDepoisDas18 = TimeSpan.FromHours(19);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new HoraConsulta(horaDepoisDas18));
        }

        [Fact]
        public void Criar_Consulta_HoraValida_DeveCriarComSucesso()
        {
            var horaValida = TimeSpan.FromHours(10);

            var horaConsulta = new HoraConsulta(horaValida);

            Assert.Equal(horaValida, horaConsulta.Hora);
        }

        [Fact]
        public void Criar_Consulta_HoraLimite8_DeveCriarComSucesso()
        {
            var horaLimite = TimeSpan.FromHours(8);

            var horaConsulta = new HoraConsulta(horaLimite);

            Assert.Equal(horaLimite, horaConsulta.Hora);
        }

        [Fact]
        public void Criar_Consulta_HoraLimite18_DeveCriarComSucesso()
        {
            var horaLimite = TimeSpan.FromHours(18);

            var horaConsulta = new HoraConsulta(horaLimite);

            Assert.Equal(horaLimite, horaConsulta.Hora);
        }

        #endregion

        #region Consulta Creation

        [Fact]
        public void Consulta_Criar_DeveRetornarConsultaComDadosCorretos()
        {
            var dataFutura = DateTime.Today.AddDays(1);
            var horaValida = TimeSpan.FromHours(14);
            int pacienteId = 1;
            int profissionalId = 2;

            var consulta = Domain.Entities.Consulta.Criar(dataFutura, horaValida, pacienteId, profissionalId);

            Assert.NotNull(consulta);
            Assert.Equal(pacienteId, consulta.PacienteId);
            Assert.Equal(profissionalId, consulta.ProfissionalId);
        }

        [Fact]
        public void Consulta_Criar_ComDataPassada_DeveLancarExcecao()
        {

            var dataPassada = DateTime.Today.AddDays(-1);
            var horaValida = TimeSpan.FromHours(10);

            Assert.Throws<DomainExceptionValidation>(() =>
                Domain.Entities.Consulta.Criar(dataPassada, horaValida, 1, 2));
        }

        [Fact]
        public void Consulta_Criar_ComHoraInvalida_DeveLancarExcecao()
        {
            var dataFutura = DateTime.Today.AddDays(1);
            var horaInvalida = TimeSpan.FromHours(20);

            Assert.Throws<DomainExceptionValidation>(() =>
                Domain.Entities.Consulta.Criar(dataFutura, horaInvalida, 1, 2));
        }

        [Fact]
        public void Consulta_ReconstituirDoRepository_DeveCriarSemValidacao()
        {
            var dataPassada = DateTime.Today.AddDays(-5);
            var horaForaRange = TimeSpan.FromHours(7);
            int pacienteId = 1;
            int profissionalId = 2;
            string pacienteNome = "João Silva";
            string profissionalNome = "Dr. Maria";

            var consulta = Domain.Entities.Consulta.ReconstituirDoRepository(
                1, dataPassada, horaForaRange,
                pacienteId, profissionalId,
                pacienteNome, profissionalNome);

            Assert.NotNull(consulta);
            Assert.Equal(1, consulta.ConsultaId);
            Assert.Equal(pacienteId, consulta.PacienteId);
            Assert.Equal(profissionalId, consulta.ProfissionalId);
            Assert.Equal(pacienteNome, consulta.PacienteNome);
            Assert.Equal(profissionalNome, consulta.ProfissionalNome);
        }

        #endregion
    }
}
