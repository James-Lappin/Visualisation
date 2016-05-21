using Microsoft.Practices.ServiceLocation;
using System;
using System.ComponentModel;
using Visualisation.Core.Interfaces;
using Visualisation.Core.Responsitories;
using Visualisation.Core.Services;

namespace Visualisation.Core.Domain
{
	public class TransactionRequest : EntityBase
	{
		public string Title { get; set; }
		public PaymentType PaymentType { get; set; }
		public AmountEnum AmountEnum { get; set; }
		public double? Longitude { get; set; }
		public double? Latitude { get; set; }
		public string Postcode { get; set; }
		public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;

		public IMapDisplay MapDisplay => ServiceLocator.Current.GetInstance<IMapDisplay>();
		public IRepository<TransactionRequest> Repository => ServiceLocator.Current.GetInstance<IRepository<TransactionRequest>>();

		public void Map()
		{
			var latLong = GetLatLong();
			if (latLong == null) { return; }

			MapDisplay.DisplayLocation(Title, latLong);

			Save();
		}

		private void Save()
		{
			Repository.CreateOrUpdate(this);
		}

		private LatLongPoint GetLatLong()
		{
			if (Latitude.HasValue && Longitude.HasValue)
			{
				//This needs to become null as we are doing a lookup on postcodes
				//This could potentially poison the data.
				Postcode = null;
				return new LatLongPoint { Latitude = Latitude.Value, Longitude = Longitude.Value };
			}

			var latLongFromPreviousRequest = GetLatLongFromPreviousRequest();
			if (latLongFromPreviousRequest != null) { return latLongFromPreviousRequest; }

			if (!string.IsNullOrWhiteSpace(Postcode))
			{
				var geoLocationService = ServiceLocator.Current.GetInstance<GeoLocationService>();
				return geoLocationService.GetLatAndLongFromPostCode(Postcode);
			}

			return null;
		}

		private LatLongPoint GetLatLongFromPreviousRequest()
		{
			var previousRequest = Repository.GetSingleByFilter(x => x.Postcode == Postcode
																		&& x.Latitude.HasValue
																		&& x.Longitude.HasValue);
			return previousRequest == null
				? null
				: new LatLongPoint { Latitude = previousRequest.Latitude.Value, Longitude = previousRequest.Longitude.Value };
		}
	}

	public enum AmountEnum
	{
		[Description("Between 0-50")]
		Small = 0,
		[Description("between 51-250")]
		Medium = 5,
		[Description("between 250-1000")]
		Large = 10,
		[Description("Over 1000")]
		Major = 20
	}

	public enum PaymentType
	{
		PreAuth = 1,
		Payment = 2,
		Collection = 3,
		Void = 4,
		Refund = 5,
	}
}