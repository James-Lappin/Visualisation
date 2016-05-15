using System.Web.Mvc;
using Visualisation.Core.Domain;
using Visualisation.Core.Services;

namespace Visualisation.Web.Controllers
{
	public class TransactionRequestController : Controller
	{
		private readonly GeoLocationService _geoLocationService;

		public TransactionRequestController(GeoLocationService geoLocationService)
		{
			_geoLocationService = geoLocationService;
		}

		[HttpPost]
		public void Location(TransactionRequestModel model)
		{
			if (model == null) { return; }

			LatLongPoint latLong;
			if (model.Latitude.HasValue && model.Longitude.HasValue)
			{
				latLong = new LatLongPoint { Latitude = model.Latitude.Value, Longitude = model.Longitude.Value };
			}
			else if (!string.IsNullOrWhiteSpace(model.Postcode))
			{
				latLong = _geoLocationService.GetLatAndLongFromPostCode(model.Postcode);
			}
			else if (!string.IsNullOrWhiteSpace(model.IpAddress))
			{
				latLong = _geoLocationService.GetLatLongFromIpAddress(model.IpAddress);
			}
			else
			{
				return;
			}

			var transaction = new TransactionRequest(model.Title, latLong.Longitude, latLong.Latitude);
			transaction.Map();
		}
	}

	public class TransactionRequestModel
	{
		public string Title { get; set; }
		public double? Longitude { get; set; }
		public double? Latitude { get; set; }
		public string Postcode { get; set; }
		public string IpAddress { get; set; }
	}
}
