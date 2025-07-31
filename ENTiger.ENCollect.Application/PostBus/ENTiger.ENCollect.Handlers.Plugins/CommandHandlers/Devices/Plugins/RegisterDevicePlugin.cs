using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.DevicesModule
{
    public partial class RegisterDevicePlugin : FlexiPluginBase, IFlexiPlugin<RegisterDevicePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a131e9eb33eea18e04ebe0ca800b6fe";
        public override string FriendlyName { get; set; } = "RegisterDevicePlugin";

        protected string EventCondition = "";

        protected readonly ILogger<RegisterDevicePlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected DeviceDetail? _model;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly OtpSettings _otpSettings;

        public RegisterDevicePlugin(ILogger<RegisterDevicePlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, IOptions<OtpSettings> otpSettings)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _otpSettings = otpSettings.Value;
        }

        public virtual async Task Execute(RegisterDevicePostBusDataPacket packet)
        {
            bool staticOTP = _otpSettings.StaticOtp.Enabled;
            string otp = _otpSettings.StaticOtp.Otp;

            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            otp = staticOTP == true ? otp : GenerateOTP();
            _model = await _repoFactory.GetRepo().FindAll<DeviceDetail>().Where(x => x.Email == packet.Cmd.Dto.Email).FirstOrDefaultAsync();

            var user = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => x.PrimaryEMail == packet.Cmd.Dto.Email).FirstOrDefaultAsync();

            if (_model == null)
            {
                _model = _flexHost.GetDomainModel<DeviceDetail>().RegisterDevice(packet.Cmd, user.Id, otp);
            }
            else
            {
                DateTime lastDeviceOTPDate = _model.OTPTimeStamp.DateTime;
                if (_model.OTPTimeStamp == null || DateTime.Now.Date > lastDeviceOTPDate.Date)
                {
                    _model.OTPCount = 1;
                }
                else
                {
                    _model.OTPCount = _model.OTPCount + 1;
                }
                _model.OTPTimeStamp = DateTime.Now;
                _model.OldIMEI = _model.IMEI;
                _model.IMEI = packet.Cmd.Dto.IMEI;
                _model.IsVerified = false;
                _model.VerifiedDate = null;
                _model.OTP = otp;
                _model.SetAddedOrModified();
            }

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(DeviceDetail).Name, _model.Id);
                EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(DeviceDetail).Name, _model.Id);
            }
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private string GenerateOTP()
        {
            string numbers = "1234567890";
            int otpLength = 6;
            string otp = string.Empty;
            for (int i = 0; i < otpLength; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, numbers.Length);
                    character = numbers.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;
        }
    }
}