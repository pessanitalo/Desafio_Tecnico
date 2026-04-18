namespace Desafio_Tecnico.Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Token { get; set; }
        public DateTime Expiracao { get; set; }
        public bool Revogado { get; set; }
    }
}
