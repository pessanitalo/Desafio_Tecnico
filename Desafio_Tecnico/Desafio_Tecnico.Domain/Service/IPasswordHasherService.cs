namespace Desafio_Tecnico.Domain.Service
{
    public interface IPasswordHasherService
    {
        string Hash(string senha);
        bool Verificar(string senha, string hash);
    }
}
