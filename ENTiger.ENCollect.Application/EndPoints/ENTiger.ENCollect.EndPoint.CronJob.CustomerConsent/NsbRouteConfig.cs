using ENTiger.ENCollect.AccountsModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class NsbRouteConfig
    {
        public static List<BusRouteConfig> GetRoute()
        {
            List<BusRouteConfig> routes = new();

            string defaultDestinationEndPoint = "ENTiger.Account.Endpoint";
            //string commonEndPoint = "ENTiger.Common.Endpoint";
           // routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, defaultDestinationEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(UpdateCustomerConsentExpiryCommand).Namespace, defaultDestinationEndPoint));
            
            return routes;
        }
    }
}
