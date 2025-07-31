using ENTiger.ENCollect.AgencyUsersModule;
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
        public virtual AgencyUser ApproveAgent(ApproveAgentCommand cmd)
        {
            this.Convert(cmd.Dto);
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;
            this.AgencyUserWorkflowState = _flexHost.GetFlexStateInstance<AgencyUserApproved>().SetStateChangedBy(this.LastModifiedBy).SetTFlexId(this.Id);

            //Map any other field not handled by Automapper config

            this.SetModified();

            //Set your appropriate SetModified for the inner object here

            return this;
        }

        #endregion "Public Methods"
    }
}