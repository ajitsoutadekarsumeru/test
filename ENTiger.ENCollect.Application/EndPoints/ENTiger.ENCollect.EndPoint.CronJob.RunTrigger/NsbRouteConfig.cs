using ENTiger.ENCollect.CommunicationModule;
using ENTiger.ENCollect.FeedbackModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class NsbRouteConfig
    {
        public static List<BusRouteConfig> GetRoute()
        {
            List<BusRouteConfig> routes = new();

            string commonDestinationEndPoint = "ENTiger.Common.Endpoint";
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(RunTriggersCommand).Namespace, commonDestinationEndPoint));

            return routes;
        }
    }
}
