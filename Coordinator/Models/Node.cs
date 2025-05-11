namespace Coordinator.Models
{
    public class Node
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        ICollection<NodeState> NodeStates { get; set; } 
    }
}
