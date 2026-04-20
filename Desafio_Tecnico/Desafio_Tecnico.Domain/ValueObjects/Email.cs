using System.Net.Mail;
using Desafio_Tecnico.Domain.Validation;

namespace Desafio_Tecnico.Domain.ValueObjects
{
    public class Email
    {
        public string Valor { get; set; }

        public Email(string valor)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(valor),
                "O e-mail é obrigatório.");

            var emailNormalizado = valor.Trim();

            DomainExceptionValidation.When(emailNormalizado.Length < 3,
                "O e-mail precisa ter no minimo tres caracteres.");
            DomainExceptionValidation.When(!EhEmailValido(emailNormalizado),
                "O e-mail informado é inválido.");

            Valor = emailNormalizado;
        }

        private static bool EhEmailValido(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
