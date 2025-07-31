using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class NsbRouteConfig
    {
        public static List<BusRouteConfig> GetRoute()
        {
            List<BusRouteConfig> routes = new();

            string defaultDestinationEndPoint = "ENTiger.Collection.Endpoint";
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, defaultDestinationEndPoint));
            return routes;
        }
    }
}