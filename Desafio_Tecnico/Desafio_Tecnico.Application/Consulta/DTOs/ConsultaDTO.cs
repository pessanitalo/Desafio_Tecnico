
namespace Desafio_Tecnico.Application.Consulta.DTOs
{
    public class ConsultaDTO
    {
        public DateTime DataConsulta { get; set; }

        public TimeSpan HoraConsulta { get; set; }

        public int PacienteId { get; set; }
        public int ProfissionalId { get; set; }
    }
}
