using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect;

public class DistanceMatrixSettings
{
    /// <summary>
    /// Gets or sets the Google Maps Distance Matrix API key.
    /// </summary>
    public string ApiKey { get; set; } = "6LdZNZwdAAAAAB36NspD05PVNfStbtgR6n8HY1TC";

    /// <summary>
    /// Gets or sets the base URL for the Distance Matrix API.
    /// Default is "https://maps.googleapis.com/maps/api/distancematrix/json"
    /// </summary>
    public string Url { get; set; } = "https://maps.googleapis.com/maps/api/distancematrix/json";
}
