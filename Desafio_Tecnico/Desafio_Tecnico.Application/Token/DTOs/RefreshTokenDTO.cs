namespace Desafio_Tecnico.Application.TokenDTO.DTOs
{
    public class RefreshTokenDTO
    {
        public int UsuarioId { get; set; }
        public string Token { get; set; }
        public DateTime Expiracao { get; set; }
        public bool Revogado { get; set; }
    }
}
