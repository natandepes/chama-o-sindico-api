namespace ChamaOSindico.Application.Interfaces
{
    public interface ITransactionService
    {
        Task ExecuteTransactionAsync(Func<Task> action);
    }
}
