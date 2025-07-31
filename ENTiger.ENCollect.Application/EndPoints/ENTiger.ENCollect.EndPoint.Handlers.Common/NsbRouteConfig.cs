using ENTiger.ENCollect.AgencyUsersModule;
using ENTiger.ENCollect.AuditTrailModule;
using ENTiger.ENCollect.CompanyUsersModule;
using ENTiger.ENCollect.FeedbackModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class NsbRouteConfig
    {
        public static List<BusRouteConfig> GetRoute()
        {
            List<BusRouteConfig> routes = new();

            string defaultDestinationEndPoint = "ENTiger.Common.Endpoint";
            string umEndPoint = "ENTiger.UM.Endpoint";
            string WebAPIEndPoint = "ENTiger.WebAPI";

            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, WebAPIEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, defaultDestinationEndPoint));

            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(AddAgentCommand).Namespace, umEndPoint));
            routes.Add(new BusRouteConfig(typeof(AgencyUsersActivateCommand).Assembly, umEndPoint));
            routes.Add(new BusRouteConfig(typeof(CompanyUsersActivateCommand).Assembly, umEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(AddAuditTrailCommand).Namespace, defaultDestinationEndPoint));
            return routes;
        }
    }
}