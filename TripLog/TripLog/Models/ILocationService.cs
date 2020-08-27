using System.Threading.Tasks;

namespace TripLog.Models
{
    public interface ILocationService
    {
        Task<GeoCoords> GetGeoCoordinatesAsync();
    }
}