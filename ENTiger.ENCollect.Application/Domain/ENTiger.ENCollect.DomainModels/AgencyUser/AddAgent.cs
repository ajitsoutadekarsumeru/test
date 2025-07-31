using ENTiger.ENCollect.AgencyUsersModule;
using ENTiger.ENCollect.DomainModels;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyUser : ApplicationUser
    {
        #region "Public Methods"

        /// <summary>
        ///
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual AgencyUser AddAgent(AddAgentCommand cmd)
        {
            Guard.AgainstNull("AgencyUser command cannot be empty", cmd);

            this.Convert(cmd.Dto);
            this.CreatedBy = cmd.Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;
            this.SetAdded(cmd.Dto.GetGeneratedId());

            //Map any other field not handled by Automapper config
            if (cmd.Dto.isSaveAsDraft)
            {
                AgencyUserWorkflowState = _flexHost.GetFlexStateInstance<AgencyUserSavedAsDraft>().SetTFlexId(this.Id).SetStateChangedBy(this.CreatedBy ?? "");
            }
            else
            {
                AgencyUserWorkflowState = _flexHost.GetFlexStateInstance<AgencyUserPendingApproval>().SetTFlexId(this.Id).SetStateChangedBy(this.CreatedBy ?? "");
            }

            //Set your appropriate SetAdded for the inner object here
            this.ProductScopes?.SetAddedOrModified();
            this.ProductScopes?.ToList().ForEach(x => x.SetCreatedBy(this.CreatedBy));
            this.ProductScopes?.ToList().ForEach(x => x.ApplicationUserId = this.Id);
            this.GeoScopes?.SetAddedOrModified();
            this.GeoScopes?.ToList().ForEach(x => x.SetCreatedBy(this.CreatedBy));
            this.GeoScopes?.ToList().ForEach(x => x.ApplicationUserId = this.Id);
            this.BucketScopes?.SetAddedOrModified();
            this.BucketScopes?.ToList().ForEach(x => x.SetCreatedBy(this.CreatedBy));
            this.BucketScopes?.ToList().ForEach(x => x.ApplicationUserId = this.Id);
            this.Designation?.SetAddedOrModified();
            this.Designation?.ToList().ForEach(x => x.SetCreatedBy(this.CreatedBy));
            this.PlaceOfWork?.SetAddedOrModified();
            this.PlaceOfWork?.ToList().ForEach(x => x.SetCreatedBy(this.CreatedBy));

            this.Languages?.SetAddedOrModified();
            this.Languages?.ToList().ForEach(x => x.SetCreatedBy(this.CreatedBy));
            this.userCustomerPersona?.SetAddedOrModified();
            this.userCustomerPersona?.ToList().ForEach(x => x.SetCreatedBy(this.CreatedBy));
            this.userPerformanceBand?.SetAddedOrModified();
            this.userPerformanceBand?.ToList().ForEach(x => x.SetCreatedBy(this.CreatedBy));
            this.CreditAccountDetails?.SetAddedOrModified();
            this.CreditAccountDetails?.SetCreatedBy(this.CreatedBy);
            this.Address?.SetAddedOrModified();
            this.AgencyUserIdentifications?.SetAddedOrModified();
            this.AgencyUserIdentifications?.ToList().ForEach(x => x.SetCreatedBy(this.CreatedBy));

            if (cmd.Dto.WalletLimit > 0)
            {
                var wallet = new Wallet(this.Id, cmd.Dto.WalletLimit);
                wallet?.SetAddedOrModified();
                wallet?.SetCreatedBy(this.CreatedBy);
                this.Wallet = wallet;
            }
            return this;
        }

        #endregion "Public Methods"
    }
}