

using Azure.Core.GeoJson;

namespace ENTiger.ENCollect;

public interface IDistanceCalculatorService
{
    double CalculateHaversineDistance(GeoPoint geoPoint1, GeoPoint geoPoint2);
    Task<double> CalculateTotalDistanceInKmAsync(List<(double lat, double lng)> coordinates);
}
