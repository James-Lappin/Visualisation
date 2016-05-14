using Microsoft.AspNet.SignalR;
using Visualisation.Web.Hubs;

namespace Visualisation.Web.Models
{
	public class TransactionRequest
	{
		public string Title { get; set; }
		public double Long { get; set; }
		public double Lat { get; set; }


		public void Map()
		{
			var mapHub = GlobalHost.ConnectionManager.GetHubContext<MapHub>();
			mapHub.Clients.All.displayLocation(Title, Lat, Long);


		}
	}
}