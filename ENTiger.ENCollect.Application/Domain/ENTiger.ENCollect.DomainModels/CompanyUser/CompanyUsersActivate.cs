using Sumeru.Flex;
using ENTiger.ENCollect.CompanyUsersModule;
namespace ENTiger.ENCollect
{
    public partial class CompanyUser : ApplicationUser
    {
        #region "Public Methods"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual CompanyUser CompanyUsersActivate(CompanyUsersActivateCommand cmd)
        {
            Guard.AgainstNull("CompanyUser model cannot be empty", cmd);
            this.Convert(cmd.Dto);
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;
            this.CompanyUserWorkflowState = _flexHost.GetFlexStateInstance<CompanyUserPendingApproval>().SetStateChangedBy(LastModifiedBy).SetTFlexId(this.Id);
            this.TransactionSource = cmd.Dto.GetAppContext()?.RequestSource;

            this.SetModified();
            return this;
        }
        #endregion

        #region "Private Methods"
        #endregion

    }
}
