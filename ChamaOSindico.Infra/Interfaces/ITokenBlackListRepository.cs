namespace ChamaOSindico.Infra.Interfaces
{
    public interface ITokenBlackListRepository
    {
        Task AddTokenToBlackListAsync(string token, DateTime expiry);
        Task<bool> IsTokenBlackListeAsync(string token);
    }
}
