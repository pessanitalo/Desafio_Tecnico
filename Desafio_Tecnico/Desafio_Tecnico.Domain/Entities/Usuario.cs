using Desafio_Tecnico.Domain.ValueObjects;

namespace Desafio_Tecnico.Domain.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public Email Email { get; set; }
        public Senha Senha { get; set; }

        private Usuario() { }


        public Usuario(int usuarioId, Email email, Senha senha)
        {
            UsuarioId = usuarioId;
            Email = email;
            Senha = senha;
        }

        public static Usuario Criar(string email, string senha)
        {
            return new Usuario(
                0,
                new Email(email),
                new Senha(senha)
            );
        }
    }
}
