using System;
using Visualisation.Core.Interfaces;

namespace Visualisation.Core.Domain
{
	public class TransactionRequest
	{
		private readonly IMapDisplay _display;

		public int TransactionRequestId { get; set; }
		public string Title { get; set; }
		public double Longitude { get; set; }
		public double Latitude { get; set; }
		public DateTimeOffset CreatedDate { get; set; }

		public TransactionRequest(string title, double longitude, double latitude, IMapDisplay display)
		{
			Title = title;
			Longitude = longitude;
			Latitude = latitude;
			_display = display;
		}

		public void Map()
		{
			_display.DisplayLocation(this);
		}
	}
}