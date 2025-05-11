using Coordinator.Enums;

namespace Coordinator.Models
{
    public class NodeState
    {
        public Guid Id { get; set; }
        public Guid TransactionId { get; set; }
        /// <summary>
        /// 1. Level state
        /// </summary>
        public ReadyType IsReady { get; set; }
        /// <summary>
        /// 2 . Level State
        /// </summary>
        public TransactionState TransactionState { get; set; }
        public Node Node { get; set; }
    }
}
