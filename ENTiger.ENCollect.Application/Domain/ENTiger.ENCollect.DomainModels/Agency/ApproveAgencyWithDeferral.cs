using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Agency : ApplicationOrg
    {
        #region "Public Methods"

        /// <summary>
        ///
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual Agency ApproveAgencyWithDeferral(string UserId)
        {
            this.AgencyWorkflowState = _flexHost.GetFlexStateInstance<AgencyApprovedWithDeferrals>().SetStateChangedBy(UserId).SetTFlexId(this.Id);
            this.LastModifiedBy = UserId;

            //Map any other field not handled by Automapper config

            this.SetModified();

            //Set your appropriate SetModified for the inner object here

            return this;
        }

        #endregion "Public Methods"
    }
}