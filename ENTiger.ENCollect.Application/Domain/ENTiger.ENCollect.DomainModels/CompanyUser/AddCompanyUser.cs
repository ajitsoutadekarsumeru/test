using ENTiger.ENCollect.CompanyUsersModule;
using ENTiger.ENCollect.DomainModels;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class CompanyUser : ApplicationUser
    {
        #region "Public Methods"
        /// <summary>
        ///
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual CompanyUser AddCompanyUser(AddCompanyUserCommand cmd)
        {
            Guard.AgainstNull("CompanyUser command cannot be empty", cmd);

            this.Convert(cmd.Dto);
            this.CreatedBy = cmd.Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;
            //Set your appropriate SetAdded for the inner object here
            this.SetAdded(cmd.Dto.GetGeneratedId());
            //Map any other field not handled by Automapper config
            if (cmd.Dto.IsSaveAsDraft)
            {
                this.CompanyUserWorkflowState = _flexHost.GetFlexStateInstance<CompanyUserSavedAsDraft>().SetTFlexId(this.Id).SetStateChangedBy(this.CreatedBy ?? "");
            }
            else
            {
                this.CompanyUserWorkflowState = _flexHost.GetFlexStateInstance<CompanyUserPendingApproval>().SetTFlexId(this.Id).SetStateChangedBy(this.CreatedBy ?? "");
            }
            this.ProductScopes?.SetAddedOrModified();
            this.ProductScopes?.ToList().ForEach(x => x.SetCreatedBy(this.CreatedBy));
            this.ProductScopes?.ToList().ForEach(x => x.ApplicationUserId = this.Id);
            this.GeoScopes?.SetAddedOrModified();
            this.GeoScopes?.ToList().ForEach(x => x.SetCreatedBy(this.CreatedBy));
            this.GeoScopes?.ToList().ForEach(x => x.ApplicationUserId = this.Id);
            this.BucketScopes?.SetAddedOrModified();
            this.BucketScopes?.ToList().ForEach(x => x.SetCreatedBy(this.CreatedBy));
            this.BucketScopes?.ToList().ForEach(x => x.ApplicationUserId = this.Id);
            this.PlaceOfWork?.SetAddedOrModified();
            this.PlaceOfWork?.ToList().ForEach(x => x.SetCreatedBy(this.CreatedBy));
            this.Designation?.SetAddedOrModified();
            this.Designation?.ToList().ForEach(x => x.SetCreatedBy(this.CreatedBy));
            this.Languages?.SetAddedOrModified();
            this.Languages?.ToList().ForEach(x => x.SetCreatedBy(this.CreatedBy));
            this.userCustomerPersona?.SetAddedOrModified();
            this.userCustomerPersona?.ToList().ForEach(x => x.SetCreatedBy(this.CreatedBy));
            this.userPerformanceBand?.SetAddedOrModified();
            this.userPerformanceBand?.ToList().ForEach(x => x.SetCreatedBy(this.CreatedBy));
            this.CreditAccountDetails?.SetAddedOrModified();
            this.CreditAccountDetails?.SetCreatedBy(this.CreatedBy);

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

        #region "Private Methods"
        #endregion "Private Methods"
    }
}