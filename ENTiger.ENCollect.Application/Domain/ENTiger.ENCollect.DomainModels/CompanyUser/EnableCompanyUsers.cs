using Sumeru.Flex;
using ENTiger.ENCollect.CompanyUsersModule;
using Elastic.Clients.Elasticsearch.Security;
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
        public virtual CompanyUser EnableCompanyUsers(EnableCompanyUsersCommand cmd)
        {
            Guard.AgainstNull("CompanyUser model cannot be empty", cmd);
            this.Convert(cmd.Dto);
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;
            this.CompanyUserWorkflowState = _flexHost.GetFlexStateInstance<CompanyUserApproved>().SetStateChangedBy(this.LastModifiedBy).SetTFlexId(this.Id);
            this.Remarks = cmd.Dto.Description;
            this.IsDeactivated = false;
            //Map any other field not handled by Automapper config

            this.SetModified();

            //Set your appropriate SetModified for the inner object here

            return this;
        }
        #endregion
    }
}
