using Desafio_Tecnico.Domain.ValueObjects;

namespace Desafio_Tecnico.Domain.Entities
{
    public class Consulta
    {
        public int ConsultaId { get; private set; }
        public DataConsulta DataConsulta { get; private set; }
        public HoraConsulta HoraConsulta { get; private set; }

        public int PacienteId { get; set; }
        public int ProfissionalId { get; set; }
        public string PacienteNome { get; set; }
        public string ProfissionalNome { get; set; }

        private Consulta() { }


        public Consulta(DataConsulta data, HoraConsulta hora, int pacienteId, int profissionalId)
        {
            DataConsulta = data;
            HoraConsulta = hora;
            PacienteId = pacienteId;
            ProfissionalId = profissionalId;
        }

        // aqui ele cria a consulta e valida os valeu object
        public static Consulta Criar(DateTime data, TimeSpan hora, int pacienteId, int profissionalId)
        {
            return new Consulta(
                new DataConsulta(data),
                new HoraConsulta(hora),
                pacienteId,
                profissionalId
            );
        }

        // Método para reconstruir do banco SEM validação
        public static Consulta ReconstituirDoRepository(
            int consultaId,
            DateTime data,
            TimeSpan hora,
            int pacienteId,
            int profissionalId,
            string pacienteNome,
            string profissionalNome)
        {
            var consulta = new Consulta(
                DataConsulta.CriarSemValidacao(data),
                HoraConsulta.CriarSemValidacao(hora),
                pacienteId,
                profissionalId
            );

            consulta.ConsultaId = consultaId;
            consulta.PacienteNome = pacienteNome;
            consulta.ProfissionalNome = profissionalNome;

            return consulta;
        }
    }
}
