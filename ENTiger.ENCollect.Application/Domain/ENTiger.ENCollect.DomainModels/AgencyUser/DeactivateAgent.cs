using Sumeru.Flex;

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
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual AgencyUser DeactivateAgent(string userId, string reason)
        {
            this.AgencyUserWorkflowState = _flexHost.GetFlexStateInstance<AgencyUserDisabled>().SetStateChangedBy(userId).SetTFlexId(this.Id);
            this.LastModifiedBy = userId;
            this.DeactivationReason = reason;
            this.IsDeactivated = true;
            this.AgencyUserWorkflowState.Remarks = reason;
            //Map any other field not handled by Automapper config

            this.SetModified();

            //Set your appropriate SetModified for the inner object here

            return this;
        }

        #endregion "Public Methods"
    }
}