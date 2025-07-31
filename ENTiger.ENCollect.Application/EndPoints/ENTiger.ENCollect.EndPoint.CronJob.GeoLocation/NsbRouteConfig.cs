using ENTiger.ENCollect.AgencyModule;
using ENTiger.ENCollect.GeoLocationModule;
using Sumeru.Flex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    public class NsbRouteConfig
    {
        public static List<BusRouteConfig> GetRoute()
        {
            List<BusRouteConfig> routes = new();

            string defaultDestinationEndPoint = "ENTiger.ENCollect.EndPoint.CronJob.GeoLocation";
            //routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, defaultDestinationEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(UpdateGeoLocationsCommand).Namespace, defaultDestinationEndPoint));
            
            return routes;
        }
    }
}
