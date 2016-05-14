using System.Web.Mvc;
using Visualisation.Core.Domain;
using Visualisation.Core.Interfaces;
using Visualisation.Web.Hubs;

namespace Visualisation.Web.Controllers
{
	public class TransactionRequestController : Controller
	{
		private readonly IMapDisplay _mapDisplay;

		//public TransactionRequestController(IMapDisplay mapDisplay)
		//{
		//	_mapDisplay = mapDisplay;
		//}

		public TransactionRequestController(MapHub mapDisplay)
		{
			_mapDisplay = mapDisplay;
		}

		[HttpPost]
		[AllowAnonymous]
		public void Location(TransactionRequestModel model)
		{
			var transaction = new TransactionRequest(model.Title, model.Longitude, model.Latitude, _mapDisplay);
			transaction?.Map();
		}
	}

	public class TransactionRequestModel
	{
		public string Title { get; set; }
		public double Longitude { get; set; }
		public double Latitude { get; set; }
	}
}
