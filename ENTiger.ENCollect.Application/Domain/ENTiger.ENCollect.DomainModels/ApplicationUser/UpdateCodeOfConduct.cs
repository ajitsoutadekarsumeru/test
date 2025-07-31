using Sumeru.Flex;
using ENTiger.ENCollect.ApplicationUsersModule;
namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ApplicationUser : PersonBridge
    {

        #region "Public Methods"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual ApplicationUser UpdateCodeOfConduct(UpdateCodeOfConductCommand cmd)
        {
            Guard.AgainstNull("ApplicationUser model cannot be empty", cmd);
            this.Convert(cmd.Dto);
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;
            this.IsPolicyAccepted = true;
            this.PolicyAcceptedDate = DateTime.UtcNow;

            //Map any other field not handled by Automapper config

            this.SetModified();

            //Set your appropriate SetModified for the inner object here

            return this;
        }
#endregion

        #region "Private Methods"
        #endregion

    }
}
