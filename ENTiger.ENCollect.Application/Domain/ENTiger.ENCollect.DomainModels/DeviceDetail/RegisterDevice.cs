using ENTiger.ENCollect.DevicesModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class DeviceDetail : DomainModelBridge
    {
        #region "Public Methods"

        /// <summary>
        ///
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual DeviceDetail RegisterDevice(RegisterDeviceCommand cmd, string userId, string OTP)
        {
            Guard.AgainstNull("DeviceDetail command cannot be empty", cmd);

            this.Convert(cmd.Dto);
            this.CreatedBy = cmd.Dto.GetAppContext()?.UserId;
            this.LastModifiedBy = cmd.Dto.GetAppContext()?.UserId;
            this.UserId = userId;
            this.OTPCount = 1;
            this.OTP = OTP;
            this.OTPTimeStamp = DateTime.Now;
            //Map any other field not handled by Automapper config

            this.SetAdded(cmd.Dto.GetGeneratedId());

            //Set your appropriate SetAdded for the inner object here

            return this;
        }

        #endregion "Public Methods"
    }
}