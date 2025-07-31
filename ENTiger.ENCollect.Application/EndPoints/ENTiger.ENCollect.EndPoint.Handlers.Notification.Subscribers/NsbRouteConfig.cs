using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class NsbRouteConfig
    {
        public static List<BusRouteConfig> GetRoute()
        {
            List<BusRouteConfig> routes = new();

            string defaultDestinationEndPoint = "ENTiger.Notification.Subscribers";
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

            return routes;
        }
    }
}