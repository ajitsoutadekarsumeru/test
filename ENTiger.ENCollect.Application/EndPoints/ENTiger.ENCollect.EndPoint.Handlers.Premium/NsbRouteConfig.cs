using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class NsbRouteConfig
    {
        public static List<BusRouteConfig> GetRoute()
        {
            List<BusRouteConfig> routes = new();

            string premiumEndPoint = "ENTiger.Premium.Endpoint";
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(ExecuteFragmentedTreatmentCommand).Namespace, premiumEndPoint));

            return routes;
        }
    }
}