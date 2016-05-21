using Visualisation.Core.Domain;

namespace Visualisation.Core.Interfaces
{
	public interface IMapDisplay
	{
		void DisplayLocation(string title, LatLongPoint latLong, double radiusModifier);
	}
}