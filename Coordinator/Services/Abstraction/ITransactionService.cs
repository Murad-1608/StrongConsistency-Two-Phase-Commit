namespace Coordinator.Services.Abstraction
{
    public interface ITransactionService
    {
        Task<Guid> CreateTransaction();
        Task PrepareService(Guid transactionId);
        Task<bool> CheckReadyService(Guid transactionId);

    }
}
