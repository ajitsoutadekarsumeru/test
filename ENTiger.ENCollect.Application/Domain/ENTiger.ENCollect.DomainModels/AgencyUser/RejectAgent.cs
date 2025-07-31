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
        public virtual AgencyUser RejectAgent(string userId, string reason)
        {
            this.AgencyUserWorkflowState = _flexHost.GetFlexStateInstance<AgencyUserRejected>().SetStateChangedBy(userId).SetTFlexId(this.Id);
            this.RejectionReason = reason;
            this.LastModifiedBy = userId;
            this.AgencyUserWorkflowState.Remarks = reason;
            //Map any other field not handled by Automapper config

            this.SetModified();

            //Set your appropriate SetModified for the inner object here

            return this;
        }

        #endregion "Public Methods"
    }
}