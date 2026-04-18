namespace Desafio_Tecnico.Application.Profissional.DTOs
{
    public class AgendaMedicoDTO
    {
        public int ConsultaId { get; set; }
        public int ProfissionalId { get; set; }
        public string NomeProfissional { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Hora { get; set; }

        public int PacienteId { get; set; }
        public string NomePaciente { get; set; }
        public string CpfPaciente { get; set; }
    }
}
    