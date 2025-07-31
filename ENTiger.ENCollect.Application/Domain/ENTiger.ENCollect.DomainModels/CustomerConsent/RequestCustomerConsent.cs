using Sumeru.Flex;
using ENTiger.ENCollect.AccountsModule;

namespace ENTiger.ENCollect
{
    public partial class CustomerConsent : DomainModelBridge
    {
        protected readonly ICustomUtility _customUtility;
        public CustomerConsent(ICustomUtility customUtility)
        {
            _customUtility = customUtility;
        }
        #region "Public Methods"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual CustomerConsent RequestCustomerConsent(RequestCustomerConsentCommand cmd)
        {
            Guard.AgainstNull("CustomerConsent command cannot be empty", cmd);

            this.Convert(cmd.Dto);
            this.ExpiryTime = this.RequestedVisitTime;
            this.Status = CustomerConsentStatusEnum.Pending.Value;
            this.SecureToken = _customUtility.GenerateShortSecureToken(16, true);
            this.UserId = cmd.Dto.GetAppContext()?.UserId;
            this.CreatedBy = cmd.Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;

            this.SetAdded(cmd.Dto.GetGeneratedId());
            return this;
        }
        #endregion


        #region "Private Methods"
        #endregion

    }
}
