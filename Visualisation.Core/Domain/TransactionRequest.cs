using Microsoft.Practices.ServiceLocation;
using System;
using Visualisation.Core.Interfaces;

namespace Visualisation.Core.Domain
{
	public class TransactionRequest
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public double Longitude { get; set; }
		public double Latitude { get; set; }
		public DateTimeOffset CreatedDate { get; set; }

		public TransactionRequest(string title, double longitude, double latitude)
		{
			Title = title;
			Longitude = longitude;
			Latitude = latitude;
		}

		public void Map()
		{
			var mapDisplay = ServiceLocator.Current.GetInstance<IMapDisplay>();
			mapDisplay.DisplayLocation(this);
		}
	}
}