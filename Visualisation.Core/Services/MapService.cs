using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visualisation.Core.Domain;
using Visualisation.Core.Interfaces;

namespace Visualisation.Core.Services
{
	public class MapService
	{
		private readonly IRepository<TransactionRequest> _repository;
		private readonly IMapDisplay _mapDisplay;
		private readonly GeoLocationService _geoLocationService;

		public MapService(IRepository<TransactionRequest> repository, IMapDisplay mapDisplay, GeoLocationService geoLocationService)
		{
			_geoLocationService = geoLocationService;
			_mapDisplay = mapDisplay;
			_repository = repository;
		}

		public void MapTransaction(TransactionRequest transaction)
		{
			var latLong = GetLatLong(transaction);
			if (latLong == null) { return; }

			_mapDisplay.DisplayLocation(transaction.Title, latLong);
			_repository.CreateOrUpdate(transaction);
		}

		public LatLongPoint GetLatLong(TransactionRequest transaction)
		{
			if (transaction.Latitude.HasValue && transaction.Longitude.HasValue)
			{
				//This needs to become null as we are doing a lookup on postcodes
				//This could potentially poison the data.
				transaction.Postcode = null;
				return new LatLongPoint { Latitude = transaction.Latitude.Value, Longitude = transaction.Longitude.Value };
			}

			var latLongFromPreviousRequest = GetLatLongFromPreviousRequest(transaction);
			if (latLongFromPreviousRequest != null) { return latLongFromPreviousRequest; }

			if (!string.IsNullOrWhiteSpace(transaction.Postcode))
			{
				return _geoLocationService.GetLatAndLongFromPostCode(transaction.Postcode);
			}

			return null;
		}

		private LatLongPoint GetLatLongFromPreviousRequest(TransactionRequest transaction)
		{
			var previousRequest = _repository.GetSingleByFilter(x => x.Postcode == transaction.Postcode
																		&& x.Latitude.HasValue
																		&& x.Longitude.HasValue);
			return previousRequest == null
				? null
				: new LatLongPoint { Latitude = previousRequest.Latitude.Value, Longitude = previousRequest.Longitude.Value };
		}
	}

}
