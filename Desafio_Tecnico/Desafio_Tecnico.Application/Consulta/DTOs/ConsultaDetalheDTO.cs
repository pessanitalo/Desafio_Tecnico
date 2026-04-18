namespace Desafio_Tecnico.Application.Consulta.DTOs
{
    public class ConsultaDetalheDTO
    {
        public int Id { get; set; }
        public int ProfissionalId { get; set; }
        public string PacienteNome { get; set; }
        public string ProfissionalNome { get; set; }
        public DateTime DataConsulta { get; set; }
        public TimeSpan HoraConsulta { get; set; }
    }
}
