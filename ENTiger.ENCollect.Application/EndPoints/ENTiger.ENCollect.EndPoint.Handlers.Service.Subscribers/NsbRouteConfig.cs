using ENTiger.ENCollect.AgencyModule;
using ENTiger.ENCollect.AgencyUsersModule;
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

            string defaultDestinationEndPoint = "ENTiger.Service.Subscribers";
            string accountEndPoint = "ENTiger.Account.Endpoint";
            string umEndPoint = "ENTiger.UM.Endpoint";
            string collectionEndPoint = "ENTiger.Collection.Endpoint";
            string collectionProcessEndPoint = "ENTiger.CollectionProcess.Endpoint";
            string commonEndPoint = "ENTiger.Common.Endpoint";

            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, defaultDestinationEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, accountEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, umEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, collectionEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, collectionProcessEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, commonEndPoint));

            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(AddAgentCommand).Namespace, umEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(AddAgencyCommand).Namespace, umEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(AddCompanyUserCommand).Namespace, umEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(AddFeedbackCommand).Namespace, collectionProcessEndPoint));

            return routes;
        }
    }
}