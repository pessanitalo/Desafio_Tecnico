namespace Desafio_Tecnico.Domain.Service
{
    public interface ITokenService
    {
        string GerarToken(int usuarioId, string email);
        string GerarRefreshToken();
    }
}
