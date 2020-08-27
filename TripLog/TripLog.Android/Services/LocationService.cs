using System.Threading.Tasks;
using TripLog.Models;

namespace TripLog.Droid.Services
{
    public class LocationService: ILocationService
    {
        public async Task<GeoCoords> GetGeoCoordinatesAsync()
        {
            var location = await Xamarin.Essentials.Geolocation.GetLocationAsync();
            return new GeoCoords()
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };
        }
    }
}