using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents the settings required for configuring a REST client.
    /// </summary>
    public class RestClientSettings
    {
        /// <summary>
        /// Gets or sets the API key for the .NET Core Cloud service.
        /// </summary>
        public string NetCoreCloudApiKey { get; set; } = "";

        /// <summary>
        /// Gets or sets the API URL for the .NET Core Cloud service.
        /// </summary>
        public string NetCoreCloudApiUrl { get; set; } = "";
    }
}
