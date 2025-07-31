using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Azure.Core.GeoJson;
namespace ENTiger.ENCollect;

public class DistanceCalculatorService : IDistanceCalculatorService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _baseUrl;

    private readonly ILogger<DistanceCalculatorService> _logger;
    public DistanceCalculatorService(HttpClient httpClient,
         IOptions<GoogleSettings> googleOptions,
        ILogger<DistanceCalculatorService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;

        var settings = googleOptions.Value.DistanceMatrix;
        _apiKey = !string.IsNullOrWhiteSpace(settings.ApiKey)
            ? settings.ApiKey
            : throw new ArgumentException("DistanceMatrix API key is missing.");

        _baseUrl = !string.IsNullOrWhiteSpace(settings.Url)
            ? settings.Url
            : throw new ArgumentException("DistanceMatrix API URL is missing.");
    }

    public async Task<double> CalculateTotalDistanceInKmAsync(List<(double lat, double lng)> coordinates)
    {
        if (coordinates == null || coordinates.Count < 2)
            //throw new ArgumentException("At least two coordinates are required to calculate distance.");
            return 0;

        double totalDistanceMeters = 0;
        int MaxDiagonalPairsPerBatch = 10; // Google API limit: max 100 origin * destination pairs per call

        // Loop through coordinates in chunks of up to 100 consecutive hops
        for (int i = 0; i < coordinates.Count - 1; i += MaxDiagonalPairsPerBatch)
        {
            var origins = new List<string>();
            var destinations = new List<string>();

            // Determine how many hops in this batch
            int currentBatchSize = Math.Min(MaxDiagonalPairsPerBatch, coordinates.Count - 1 - i);

            // Build batch of origin-destination pairs: (i ➜ i+1), (i+1 ➜ i+2), ...
            for (int j = 0; j < currentBatchSize; j++)
            {
                origins.Add($"{coordinates[i + j].lat},{coordinates[i + j].lng}");
                destinations.Add($"{coordinates[i + j + 1].lat},{coordinates[i + j + 1].lng}");
            }

            // Construct Google Distance Matrix API URL for this batch
            var url = BuildRequestUrl(origins, destinations);

            // Call API and add returned distance (in meters) to total
            var distance = await GetBatchDistanceAsync(url, currentBatchSize);
            totalDistanceMeters += distance;
        }

        // Convert meters to kilometers and round to 3 decimal places
        return Math.Round(totalDistanceMeters / 1000.0, 3);
    }
    // Builds the API URL from given origin and destination lists
    private string BuildRequestUrl(List<string> origins, List<string> destinations)
    {
        var originsParam = string.Join("|", origins);
        var destinationsParam = string.Join("|", destinations);

        return $"{_baseUrl}?" +
          $"origins={Uri.EscapeDataString(originsParam)}&" +
          $"destinations={Uri.EscapeDataString(destinationsParam)}&" +
          $"key={_apiKey}";
    }
    // Makes a call to Google API and returns total distance of the diagonal pairs
    private async Task<double> GetBatchDistanceAsync(string url, int pairCount)
    {
        double total = 0;
        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            var json = await JsonDocument.ParseAsync(stream);

            var rows = json.RootElement.GetProperty("rows");

            // Loop through each diagonal pair: row[i].elements[i] = origin[i] ➜ destination[i]
            for (int i = 0; i < pairCount; i++)
            {
                var element = rows[i].GetProperty("elements")[i];
                var status = element.GetProperty("status").GetString();

                if (status == "OK")
                {
                    total += element.GetProperty("distance").GetProperty("value").GetDouble();
                }
                else
                {
                    _logger.LogWarning("Google Distance Matrix API returned non-OK status '{Status}' for pair index {Index}", status, i);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling Google Distance Matrix API for URL: {Url}", url);
            throw;
        }
        return total;
    }
    /// <summary>
    /// Calculates the great-circle distance (in kilometers) between two geographic points using the Haversine formula.
    /// </summary>
    /// <param name="point1">The first geographic point with latitude and longitude.</param>
    /// <param name="point2">The second geographic point with latitude and longitude.</param>
    /// <returns>Distance in kilometers between the two points.</returns>
    public double CalculateHaversineDistance(GeoPoint point1, GeoPoint point2)
    {
        const double EarthRadiusKm = 6371.0;

        double dLat = DegreesToRadians(point2.Coordinates.Latitude - point1.Coordinates.Latitude);
        double dLon = DegreesToRadians(point2.Coordinates.Longitude - point1.Coordinates.Longitude);

        double lat1 = DegreesToRadians(point1.Coordinates.Latitude);
        double lat2 = DegreesToRadians(point2.Coordinates.Latitude);

        double a = Math.Pow(Math.Sin(dLat / 2), 2) +
                   Math.Cos(lat1) * Math.Cos(lat2) *
                   Math.Pow(Math.Sin(dLon / 2), 2);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return EarthRadiusKm * c;
    }

    /// <summary>
    /// Converts degrees to radians.
    /// </summary>
    /// <param name="degrees">Angle in degrees.</param>
    /// <returns>Angle in radians.</returns>
    private static double DegreesToRadians(double degrees) => degrees * Math.PI / 180.0;

}
