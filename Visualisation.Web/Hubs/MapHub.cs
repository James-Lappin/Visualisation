using Microsoft.AspNet.SignalR;
using Visualisation.Core.Domain;
using Visualisation.Core.Interfaces;

namespace Visualisation.Web.Hubs
{
	public class MapHub : Hub, IMapDisplay
	{
		public void DisplayLocation(string title, LatLongPoint latLong)
		{
			var mapHub = GlobalHost.ConnectionManager.GetHubContext<MapHub>();
			mapHub.Clients.All.displayLocation(title, latLong.Latitude, latLong.Longitude);
		}
	}
}