using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    public class LocationSettings
    {
        /// <summary>
        /// Defines the allowed radius (in kilometers) for location-based checks,
        /// such as proximity to a specific point or service area.
        /// </summary>
        public double RadiusInKms { get; set; } = 0;

    }

}
