using Coordinator.Services.Abstraction;

namespace Coordinator.Services
{
    public class TransactionService : ITransactionService
    {
        public Task<Guid> CreateTransactionAsync()
        {
            throw new NotImplementedException();
        }
        public Task PrepareServiceAsync(Guid transactionId)
        {
            throw new NotImplementedException();
        }
        public Task<bool> CheckReadyServiceAsync(Guid transactionId)
        {
            throw new NotImplementedException();
        }
        public Task CommitAsync(Guid transactionId)
        {
            throw new NotImplementedException();
        }
        public Task<bool> CheckTransactionStateServicesAsync(Guid transactionId)
        {
            throw new NotImplementedException();
        }
        public Task RollbackAsync(Guid transactionId)
        {
            throw new NotImplementedException();
        }
    }
}
