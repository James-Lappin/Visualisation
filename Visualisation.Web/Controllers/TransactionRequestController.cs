using Microsoft.AspNet.SignalR;
using System.Web.Mvc;
using Visualisation.Web.Hubs;
using Visualisation.Web.Models;

namespace Visualisation.Web.Controllers
{
	public class TransactionRequestController : Controller
	{
		[HttpPost]
		public void Location(TransactionRequest transaction)
		{
			if (transaction == null)
			{
				return;
			}

			var mapHub = GlobalHost.ConnectionManager.GetHubContext<MapHub>();
			mapHub.Clients.All.displayLocation(transaction.Lat, transaction.Long);
		}
	}
}
