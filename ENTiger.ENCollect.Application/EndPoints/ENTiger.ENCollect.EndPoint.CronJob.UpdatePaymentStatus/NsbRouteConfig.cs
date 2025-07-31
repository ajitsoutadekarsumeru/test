using ENTiger.ENCollect.AgencyUsersModule;
using ENTiger.ENCollect.CompanyUsersModule;
using ENTiger.ENCollect.GeoTagModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class NsbRouteConfig
    {
        public static List<BusRouteConfig> GetRoute()
        {
            List<BusRouteConfig> routes = new();

            string defaultDestinationEndPoint = "ENTiger.Common.Endpoint";
            //string commonEndPoint = "ENTiger.Common.Endpoint";
            // routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, defaultDestinationEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(GenerateGeoReportCommand).Namespace, defaultDestinationEndPoint));
            return routes;
        }
    }
}