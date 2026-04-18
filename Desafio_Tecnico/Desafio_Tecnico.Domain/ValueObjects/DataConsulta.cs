using Desafio_Tecnico.Domain.Validation;

namespace Desafio_Tecnico.Domain.ValueObjects
{
    public class DataConsulta
    {
        public DateTime Data { get; private set; }

        // Construtor público com validação
        public DataConsulta(DateTime data)
        {
            var somenteData = data.Date;

            DomainExceptionValidation.When(
                somenteData < DateTime.Today,
                "Data da consulta não pode ser no passado."
            );

            // DomainExceptionValidation.When(
            //      EhFimDeSemana(somenteData),
            //      "Consulta não pode ser marcada para fim de semana."
            //  );

            Data = somenteData;
        }

        public DataConsulta() { }


        // Construtor privado para reconstrução
        private DataConsulta(DateTime data, bool _)
        {
            Data = data.Date;
        }

        private static bool EhFimDeSemana(DateTime data)
        {
            return data.DayOfWeek == DayOfWeek.Saturday ||
                   data.DayOfWeek == DayOfWeek.Sunday;
        }

        // Método para reconstrução sem validação
        public static DataConsulta CriarSemValidacao(DateTime data)
        {
            return new DataConsulta { Data = data.Date };
        }
    }
}
