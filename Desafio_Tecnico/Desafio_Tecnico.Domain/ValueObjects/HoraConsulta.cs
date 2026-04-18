using Desafio_Tecnico.Domain.Validation;

namespace Desafio_Tecnico.Domain.ValueObjects
{
    public class HoraConsulta
    {
        public TimeSpan Hora { get; private set; }

        public HoraConsulta(TimeSpan hora)
        {
            DomainExceptionValidation.When(
                hora < TimeSpan.FromHours(8) || hora > TimeSpan.FromHours(18),
                "Horário deve estar entre 08:00 e 18:00."
            );

            Hora = hora;
        }

        public HoraConsulta() { }

        // Método para reconstrução sem validação
        public static HoraConsulta CriarSemValidacao(TimeSpan hora)
        {
            return new HoraConsulta { Hora = hora };
        }
    }
}
