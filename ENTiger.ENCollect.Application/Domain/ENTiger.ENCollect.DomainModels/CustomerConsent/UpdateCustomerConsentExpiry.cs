using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public partial class CustomerConsent : DomainModelBridge
    {

        #region "Public Methods"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="consent"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual CustomerConsent UpdateCustomerConsentExpiry(CustomerConsent consent, string? userId)
        {
            Guard.AgainstNull("CustomerConsent command cannot be empty", consent);

            consent.IsActive = false;
            consent.ExpiryTime = DateTime.Now;
            consent.Status = CustomerConsentStatusEnum.Expired.Value;
            consent.LastModifiedBy = userId;

            consent.SetModified();

            return consent;
        }
        #endregion


        #region "Private Methods"
        #endregion

    }
}
