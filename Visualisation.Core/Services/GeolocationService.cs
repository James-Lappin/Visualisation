using GoogleMaps.LocationServices;
using Visualisation.Core.Domain;

namespace Visualisation.Core.Services
{
    public class GeoLocationService
    {
        public LatLongPoint GetLatAndLongFromPostCode(string postcode)
        {
            var googleLocationService = new GoogleLocationService(true);
            var addressData = new AddressData { Zip = postcode };
            var mapPoint = googleLocationService.GetLatLongFromAddress(addressData);
            return new LatLongPoint { Latitude = mapPoint.Latitude, Longitude = mapPoint.Longitude };
        }

        public LatLongPoint GetLatLongFromIpAddress(string ipAddress)
        {
            return new LatLongPoint();
        }
    }
}
