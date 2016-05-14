using System.Data.Entity;
using Visualisation.Core.Domain;

namespace Visualisation.Core.Context
{
	public class VisualisationContext : DbContext
	{
		public DbSet<TransactionRequest> TransactionRequests { get; set; }
		public DbSet<VisualisationUser> Users { get; set; }
	}
}