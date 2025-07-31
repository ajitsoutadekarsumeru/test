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
        public virtual Agency RenewAgency(string userId, AgencyWorkflowState agencyWorkflowState)
        {
            this.AgencyWorkflowState = agencyWorkflowState?.SetStateChangedBy(userId).SetTFlexId(this.Id);
            this.LastModifiedBy = userId;

            //Map any other field not handled by Automapper config

            this.SetModified();

            //Set your appropriate SetModified for the inner object here

            return this;
        }

        #endregion "Public Methods"
    }
}