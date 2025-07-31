using ENTiger.ENCollect.PayInSlipsModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class PayInSlip : DomainModelBridge
    {
        #region "Public Methods"

        /// <summary>
        ///
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual PayInSlip UpdatePayInSlip(UpdatePayInSlipCommand cmd)
        {
            Guard.AgainstNull("PayInSlip model cannot be empty", cmd);
            this.Convert(cmd.Dto);
            //this.SetCMSPayInSlipNo(cmd.Dto.CMSPayInSlipNo);
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;

            //Map any other field not handled by Automapper config

            this.SetModified();

            //Set your appropriate SetModified for the inner object here

            return this;
        }

        #endregion "Public Methods"
    }
}