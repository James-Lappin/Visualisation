using Microsoft.Practices.ServiceLocation;
using MongoDB.Driver;
using System;
using Visualisation.Core.Interfaces;
using Visualisation.Core.Services;

namespace Visualisation.Core.Domain
{
	public class TransactionRequest
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public double? Longitude { get; set; }
		public double? Latitude { get; set; }
		public string Postcode { get; set; }
		public DateTimeOffset CreatedDate { get; set; }

		public void Map()
		{
			var latLong = GetLatLong();
			if (latLong == null) { return; }

			var mapDisplay = ServiceLocator.Current.GetInstance<IMapDisplay>();
			mapDisplay.DisplayLocation(Title, latLong);

			Save();
		}

		private void Save()
		{
			const string connectionString = "mongodb://localhost";

			// Create a MongoClient object by using the connection string

			var client = new MongoClient(connectionString);

			var dave = client.ListDatabases();

			//Use the MongoClient to access the server
			var server = client.GetDatabase("test");

			// Use the server to access the 'test' database
			// MongoDatabase database = server.GetDatabase("test");
		}

		private LatLongPoint GetLatLong()
		{
			if (Latitude.HasValue && Longitude.HasValue)
			{
				return new LatLongPoint { Latitude = Latitude.Value, Longitude = Longitude.Value };
			}
			if (!string.IsNullOrWhiteSpace(Postcode))
			{
				var geoLocationService = ServiceLocator.Current.GetInstance<GeoLocationService>();
				return geoLocationService.GetLatAndLongFromPostCode(Postcode);
			}
			return null;
		}


	}
}