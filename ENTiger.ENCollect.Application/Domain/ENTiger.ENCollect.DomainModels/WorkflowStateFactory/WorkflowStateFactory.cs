using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public static class WorkflowStateFactory
    {
        public static AgencyWorkflowState GetCollectionAgencyWFState(string status)
        {
            IFlexHost host = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            AgencyWorkflowState state = null;
            switch (status.ToLower())
            {
                case "approved":
                    state = host.GetFlexStateInstance<AgencyApproved>();
                    break;

                case "contractexpired":
                    state = host.GetFlexStateInstance<AgencyContractExpired>();
                    break;

                case "disabled":
                    state = host.GetFlexStateInstance<AgencyDisabled>();
                    break;

                case "pendingapproval":
                    state = host.GetFlexStateInstance<AgencyPendingApproval>();
                    break;

                case "pendingapprovalwithdeferrals":
                    state = host.GetFlexStateInstance<AgencyPendingApprovalWithDeferrals>();
                    break;

                case "approvedwithdeferrals":
                    state = host.GetFlexStateInstance<AgencyApprovedWithDeferrals>();
                    break;

                case "rejected":
                    state = host.GetFlexStateInstance<AgencyRejected>();
                    break;

                case "savedasdraft":
                    state = host.GetFlexStateInstance<AgencySavedAsDraft>();
                    break;
            }
            return state;
        }

        public static AgencyUserWorkflowState GetCollectionAgencyUserWFState(string status)
        {
            IFlexHost host = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            AgencyUserWorkflowState state = null;
            switch (status.ToLower())
            {
                case "approved":
                    state = host.GetFlexStateInstance<AgencyUserApproved>();
                    break;

                case "authorizationcardrenewalprocessed":
                    state = host.GetFlexStateInstance<AgencyUserRenewalProcessed>();
                    break;

                case "disabled":
                    state = host.GetFlexStateInstance<AgencyUserDisabled>();
                    break;

                case "idcardsprinted":
                    state = host.GetFlexStateInstance<AgencyUserIDCardsPrinted>();
                    break;

                case "pendingapproval":
                    state = host.GetFlexStateInstance<AgencyUserPendingApproval>();
                    break;

                case "rejected":
                    state = host.GetFlexStateInstance<AgencyUserRejected>();
                    break;

                case "savedasdraft":
                    state = host.GetFlexStateInstance<AgencyUserSavedAsDraft>();
                    break;

                case "agentapprovedpendingprinting":
                    state = host.GetFlexStateInstance<AgencyUserPendingPrintingApproved>();
                    break;

                case "dormant":
                    state = host.GetFlexStateInstance<AgencyUserDormant>();
                    break;
            }
            return state;
        }

        public static CompanyUserWorkflowState GetCompanyUserWFState(string status)
        {
            IFlexHost host = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            CompanyUserWorkflowState state = null;

            switch (status.ToLower())
            {
                case "approved":
                    state = host.GetFlexStateInstance<CompanyUserApproved>();
                    break;

                case "rejected":
                    state = host.GetFlexStateInstance<CompanyUserRejected>();
                    break;

                case "pendingapproval":
                    state = host.GetFlexStateInstance<CompanyUserPendingApproval>();
                    break;

                case "disabled":
                    state = host.GetFlexStateInstance<CompanyUserDisabled>();
                    break;

                case "savedasdraft":
                    state = host.GetFlexStateInstance<CompanyUserSavedAsDraft>();
                    break;

                case "dormant":
                    state = host.GetFlexStateInstance<CompanyUserDormant>();
                    break;
            }

            return state;
        }

        public static string GetCollectionAgencyStatus(AgencyWorkflowState agencyworkflowstate)
        {
            string status = null;
            switch (agencyworkflowstate.Name)
            {
                case "AgencyApproved":
                    status = "Approved";
                    break;

                case "AgencyContractExpired":
                    status = "ContractExpired";
                    break;

                case "AgencyDisabled":
                    status = "Disabled";
                    break;

                case "AgencyPendingApproval":

                    status = "PendingApproval";
                    break;

                case "AgencyPendingApprovalWithDeferrals":
                    status = "PendingApprovalWithDeferrals";
                    break;

                case "AgencyApprovedWithDeferrals":
                    status = "ApprovedWithDeferrals";
                    break;

                case "AgencyRejected":
                    status = "Rejected";
                    break;

                case "AgencySavedAsDraft":
                    status = "SavedAsDraft";
                    break;
            }
            return status;
        }

        public static string GetCompanyUserStatus(CompanyUserWorkflowState workflowstate)
        {
            string status = null;

            switch (workflowstate.Name)
            {
                case "CompanyUserApproved":
                    status = "Approved";
                    break;

                case "CompanyUserRejected":
                    status = "Rejected";
                    break;

                case "CompanyUserPendingApproval":
                    status = "PendingApproval";
                    break;

                case "CompanyUserDisabled":
                    status = "Disabled";
                    break;

                case "CompanyUserSavedAsDraft":
                    status = "SavedAsDraft";
                    break;

                case "CompanyUserDormant":
                    status = "Dormant";
                    break;
            }
            return status;
        }

        public static string GetCollectionAgencyUserstatus(string status)
        {
            switch (status)
            {
                case "AgencyUserApproved":
                    status = "Approved";
                    break;

                case "AgencyUserRenewalProcessed":
                    status = "AuthorizationcardRenewalProcessed";
                    break;

                case "AgencyUserDisabled":
                    status = "Disabled";
                    break;

                case "AgencyUserIDCardsPrinted":
                    status = "IdCardsPrinted";
                    break;

                case "AgencyUserPendingApproval":
                    status = "PendingApproval";
                    break;

                case "AgencyUserRejected":
                    status = "Rejected";
                    break;

                case "AgencyUserSavedAsDraft":
                    status = "SavedAsDraft";
                    break;

                case "AgencyUserPendingPrintingApproved":
                    status = "AgentApprovedPendingPrinting";
                    break;

                case "AgencyUserDormant":
                    status = "Dormant";
                    break;
            }
            return status;
        }

        public static CollectionBatchWorkflowState GetCollectionBatchWorkflowState(string status)
        {
            IFlexHost host = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            CollectionBatchWorkflowState state = null;
            switch (status)
            {
                case "BatchCreated":
                    state = host.GetFlexStateInstance<CollectionBatchCreated>();
                    break;

                case "BatchAcknowledged":
                    state = host.GetFlexStateInstance<CollectionBatchAcknowledged>();
                    break;

                case "BatchDissolved":
                    state = host.GetFlexStateInstance<Dissolved>();
                    break;
            }
            return state;
        }
    }
}