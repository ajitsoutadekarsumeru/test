using ENCollect.Dyna.Workflows;
using ENTiger.ENCollect.AccountsModule;
using ENTiger.ENCollect.AgencyModule;
using ENTiger.ENCollect.AgencyUsersModule;
using ENTiger.ENCollect.AllocationModule;
using ENTiger.ENCollect.ApplicationUsersModule;
using ENTiger.ENCollect.AuditTrailModule;
using ENTiger.ENCollect.CollectionBatchesModule;
using ENTiger.ENCollect.CollectionsModule;
using ENTiger.ENCollect.CommonModule;
using ENTiger.ENCollect.CommunicationModule;
using ENTiger.ENCollect.CompanyUsersModule;
using ENTiger.ENCollect.DesignationsModule;
using ENTiger.ENCollect.DevicesModule;
using ENTiger.ENCollect.FeedbackModule;
using ENTiger.ENCollect.GeoTagModule;
using ENTiger.ENCollect.HierarchyModule;
using ENTiger.ENCollect.PayInSlipsModule;
using ENTiger.ENCollect.PermissionSchemesModule;
using ENTiger.ENCollect.PublicModule;
using ENTiger.ENCollect.SegmentationModule;
using ENTiger.ENCollect.SettlementModule;
using ENTiger.ENCollect.TreatmentModule;
using ENTiger.ENCollect.UserSearchCriteriaModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class NsbRouteConfig
    {
        public static List<BusRouteConfig> GetRoute()
        {
            List<BusRouteConfig> routes = new();

            //string defaultDestinationEndPoint = "ENTiger-ENCollect-EndPoint-Handlers-Default";

            string accountEndPoint = "ENTiger.Account.Endpoint";
            string umEndPoint = "ENTiger.UM.Endpoint";
            string collectionEndPoint = "ENTiger.Collection.Endpoint";
            string collectionProcessEndPoint = "ENTiger.CollectionProcess.Endpoint";
            string commonEndPoint = "ENTiger.Common.Endpoint";
            string premiumEndPoint = "ENTiger.Premium.Endpoint";

            //routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, defaultDestinationEndPoint));

            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(AddAgencyCommand).Namespace, umEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(AddAgentCommand).Namespace, umEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(AddUserAttendanceCommand).Namespace, umEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(AddCompanyUserCommand).Namespace, umEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(RegisterDeviceCommand).Namespace, umEndPoint));

            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(UpdateAccountContactDetailsCommand).Namespace, accountEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(PrimaryAllocationByBatchCommand).Namespace, accountEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(RequestCustomerConsentCommand).Namespace, accountEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(CustomerConsentResponseCommand).Namespace, accountEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(RequestSettlementCommand).Namespace, accountEndPoint));
            routes.Add(new BusRouteConfig(typeof(DynaWorkflowTransitionCommand).Assembly, typeof(DynaWorkflowTransitionCommand).Namespace, accountEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(ImportAccountsCommand).Namespace, accountEndPoint));

            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(AddCollectionCommand).Namespace, collectionEndPoint));

            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(AddCollectionBatchCommand).Namespace, collectionProcessEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(CreatePayInSlipCommand).Namespace, collectionProcessEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(AddFeedbackCommand).Namespace, collectionProcessEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(AddGeoTagCommand).Namespace, collectionProcessEndPoint));

            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(UpdateACMCommand).Namespace, commonEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(AddCommand).Namespace, commonEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(AddTemplateCommand).Namespace, commonEndPoint));

            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(AddSegmentCommand).Namespace, premiumEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(AddTreatmentCommand).Namespace, premiumEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(UpdateCodeOfConductCommand).Namespace, umEndPoint));

            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(AddAuditTrailCommand).Namespace, commonEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(CreatePermissionSchemeCommand).Namespace, commonEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(CreatePermissionSchemeCommand).Namespace, commonEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(UpdatePermissionSchemeCommand).Namespace, commonEndPoint));
            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(AssignSchemeCommand).Namespace, commonEndPoint));

            routes.Add(new BusRouteConfig(typeof(DummyMessage).Assembly, typeof(AddGeoMasterCommand).Namespace, commonEndPoint));
            return routes;
        }
    }
}