using Microsoft.EntityFrameworkCore;

namespace Coordinator.Models.Contexts
{
    public class TwoPhaseCommitContext : DbContext
    {
        public TwoPhaseCommitContext(DbContextOptions<TwoPhaseCommitContext> options) : base(options)
        {
        }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<NodeState> NodeStates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Node>().HasData(
                new Node { Id = Guid.NewGuid(), Name = "Order.Api" },
                new Node { Id = Guid.NewGuid(), Name = "Stock.Api" },
                new Node { Id = Guid.NewGuid(), Name = "Payment.Api" }
            );
        }
    }
}
