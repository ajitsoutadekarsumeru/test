using ENTiger.ENCollect.AuditTrailModule;
using ENTiger.ENCollect.Messages.Commands.ContactHistory;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class NsbRouteConfig
    {
        public static List<BusRouteConfig> GetRoute()
        {
            List<BusRouteConfig> routes = new();

            string defaultDestinationEndPoint = "ENTiger.CollectionProcess.Endpoint";
            string commonDestinationEndPoint = "ENTiger.Common.Endpoint";

            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, defaultDestinationEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(AddAuditTrailCommand).Namespace, commonDestinationEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(AddContactHistoryCommand).Namespace, commonDestinationEndPoint));
            return routes;
        }
    }
}