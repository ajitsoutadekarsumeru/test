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
        public virtual CompanyUser ApproveCompanyUser(string userId, string remarks)
        {
            this.CompanyUserWorkflowState = _flexHost.GetFlexStateInstance<CompanyUserApproved>().SetStateChangedBy(userId).SetTFlexId(this.Id);
            this.LastModifiedBy = userId;
            this.Remarks = remarks;
            this.IsDeactivated = false;
            //Map any other field not handled by Automapper config

            this.SetModified();

            //Set your appropriate SetModified for the inner object here

            return this;
        }

        #endregion "Public Methods"
    }
}