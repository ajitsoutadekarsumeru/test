using Sumeru.Flex;

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
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual CompanyUser DeactivateCompanyUser(string userId, string reason)
        {
            this.CompanyUserWorkflowState = _flexHost.GetFlexStateInstance<CompanyUserDisabled>().SetStateChangedBy(userId).SetTFlexId(this.Id);
            this.LastModifiedBy = userId;
            this.DeactivationReason = reason;
            this.CompanyUserWorkflowState.Remarks = reason;
            this.IsDeactivated = true;
            //Map any other field not handled by Automapper config

            this.SetModified();

            //Set your appropriate SetModified for the inner object here

            return this;
        }

        #endregion "Public Methods"
    }
}