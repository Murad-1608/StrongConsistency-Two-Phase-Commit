using Coordinator.Enums;
using Coordinator.Models;
using Coordinator.Models.Contexts;
using Coordinator.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Coordinator.Services
{
    public class TransactionService(IHttpClientFactory _httpClientFactory, TwoPhaseCommitContext _context) : ITransactionService
    {
        HttpClient _orderHttpClient = _httpClientFactory.CreateClient("OrderApi");
        HttpClient _stockHttpClient = _httpClientFactory.CreateClient("StockApi");
        HttpClient _paymentHttpClient = _httpClientFactory.CreateClient("PaymentApi");
        public async Task<Guid> CreateTransactionAsync()
        {
            Guid transactionId = Guid.NewGuid();

            var nodes = await _context.Nodes.ToListAsync();
            nodes.ForEach(node =>
            {
                node.NodeStates = new List<NodeState>
                {
                    new NodeState
                    {
                        TransactionId = transactionId,
                        IsReady = ReadyType.Pending,
                        TransactionState = TransactionState.Pending,
                    }
                };
            });
            await _context.SaveChangesAsync();

            return transactionId;
        }
        public async Task PrepareServiceAsync(Guid transactionId)
        {
            var transactionNodes = await _context.NodeStates.Include(x => x.Node).Where(x => x.TransactionId == transactionId).ToListAsync();

            foreach (var transactionNode in transactionNodes)
            {
                try
                {
                    var response = await (transactionNode.Node.Name switch
                    {
                        "Order.API" => _orderHttpClient.GetAsync("ready"),
                        "Stock.API" => _stockHttpClient.GetAsync("ready"),
                        "Payment.API" => _paymentHttpClient.GetAsync("ready")
                    });

                    var result = bool.Parse(await response.Content.ReadAsStringAsync());
                    transactionNode.IsReady = result ? ReadyType.Ready : ReadyType.Unready;
                }
                catch
                {
                    transactionNode.IsReady = ReadyType.Unready;
                }
            }
            await _context.SaveChangesAsync();
        }
        public async Task<bool> CheckReadyServiceAsync(Guid transactionId) => (await _context.NodeStates
                               .Where(x => x.TransactionId == transactionId)
                               .ToListAsync())
                               .TrueForAll(x => x.IsReady == ReadyType.Ready);

        public async Task CommitAsync(Guid transactionId)
        {
            var transactionNodes = await _context.NodeStates.Include(x => x.Node).Where(x => x.TransactionId == transactionId).ToListAsync();

            foreach (var transactionNode in transactionNodes)
            {
                try
                {
                    var response = await (transactionNode.Node.Name switch
                    {
                        "Order.API" => _orderHttpClient.GetAsync("commit"),
                        "Stock.API" => _stockHttpClient.GetAsync("commit"),
                        "Payment.API" => _paymentHttpClient.GetAsync("commit")
                    });
                    var result = bool.Parse(await response.Content.ReadAsStringAsync());
                    transactionNode.TransactionState = result ? TransactionState.Done : TransactionState.Abort;
                }
                catch
                {
                    transactionNode.TransactionState = TransactionState.Abort;
                }
            }

            _context.SaveChanges();
        }
        public async Task<bool> CheckTransactionStateServicesAsync(Guid transactionId) => (await _context.NodeStates
                               .Where(x => x.TransactionId == transactionId)
                               .ToListAsync()).TrueForAll(x => x.TransactionState == TransactionState.Done);
        public async Task RollbackAsync(Guid transactionId)
        {
            var transactionNodes = await _context.NodeStates.Where(x => x.TransactionId == transactionId && x.TransactionState == TransactionState.Done).ToListAsync();

            foreach (var transactionNode in transactionNodes)
            {
                try
                {
                    _ = await (transactionNode.Node.Name switch
                    {
                        "Order.API" => _orderHttpClient.GetAsync("rollback"),
                        "Stock.API" => _stockHttpClient.GetAsync("rollback"),
                        "Payment.API" => _paymentHttpClient.GetAsync("rollback")
                    });

                    transactionNode.TransactionState = TransactionState.Abort;
                }
                catch
                {
                    transactionNode.TransactionState = TransactionState.Abort;
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
