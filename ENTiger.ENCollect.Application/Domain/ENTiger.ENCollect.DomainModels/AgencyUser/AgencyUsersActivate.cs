using Sumeru.Flex;
using ENTiger.ENCollect.AgencyUsersModule;
namespace ENTiger.ENCollect
{
    public partial class AgencyUser : ApplicationUser
    {

        #region "Public Methods"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual AgencyUser AgencyUsersActivate(AgencyUsersActivateCommand cmd)
        {
            Guard.AgainstNull("AgencyUser model cannot be empty", cmd);
            this.Convert(cmd.Dto);
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;
            this.AgencyUserWorkflowState = _flexHost.GetFlexStateInstance<AgencyUserPendingApproval>().SetStateChangedBy(LastModifiedBy).SetTFlexId(this.Id);
            this.TransactionSource = cmd.Dto.GetAppContext()?.RequestSource;

            this.SetModified();

            return this;
        }
#endregion

        #region "Private Methods"
        #endregion

    }
}
