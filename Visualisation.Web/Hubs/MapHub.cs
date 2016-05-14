using Microsoft.AspNet.SignalR;
using Visualisation.Core.Domain;
using Visualisation.Core.Interfaces;

namespace Visualisation.Web.Hubs
{
	public class MapHub : Hub, IMapDisplay
	{
		public void DisplayLocation(TransactionRequest transactionRequest)
		{
			var mapHub = GlobalHost.ConnectionManager.GetHubContext<MapHub>();
			mapHub.Clients.All.displayLocation(transactionRequest.Title, transactionRequest.Latitude, transactionRequest.Longitude);
		}
	}
}