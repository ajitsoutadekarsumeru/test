using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserVerificationCodes : DomainModelBridge
    {
        #region "Public Methods"

        /// <summary>
        ///
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual UserVerificationCodes ForgotPasswordVerificationCode(string shortCode, string authCode, string userId)
        {
            Guard.AgainstNull("ForgotPasswordVerificationCode shortCode cannot be empty", shortCode);

            //Map any other field not handled by Automapper config
            this.ShortVerificationCode = shortCode;
            this.VerificationCode = authCode;
            this.UserVerificationCodeTypeId = UserVerificationCodeTypeEnum.ForgotPassword.Value;
            this.UserId = userId;
            this.SetAdded();

            //Set your appropriate SetAdded for the inner object here
            return this;
        }

        #endregion "Public Methods"
    }
}