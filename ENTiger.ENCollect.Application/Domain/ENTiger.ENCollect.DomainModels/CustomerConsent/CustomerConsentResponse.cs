using Sumeru.Flex;
using ENTiger.ENCollect.AccountsModule;
namespace ENTiger.ENCollect
{

    public partial class CustomerConsent : DomainModelBridge
    {

        #region "Public Methods"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual CustomerConsent CustomerConsentResponse(CustomerConsentResponseCommand cmd)
        {
            Guard.AgainstNull("CustomerConsent model cannot be empty", cmd);
            //this.Convert(cmd.Dto);
            this.Status = cmd.Dto.Action;
           
            if (this.Status == CustomerConsentStatusEnum.Expired.Value || this.Status == CustomerConsentStatusEnum.Rejected.Value) { this.IsActive = false; }
            this.ConsentResponseTime = DateTime.Now; //(Rajesh) 2025-03-14 : use time in the command --> AVD) this is on the DomainModel and not on the DTO from the client, that is why we set this here
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;

            this.SetModified();

            return this;
        }
        #endregion

        #region "Private Methods"
        #endregion

    }
}
