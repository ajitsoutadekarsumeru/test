using ENCollect.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sumeru.Flex;
using System.Text;

namespace ENTiger.ENCollect.DevicesModule.VerifyRegisteredDeviceDevicesPlugins
{
    /// <summary>
    /// Business rule for verifying a user during device registration.
    /// </summary>
    public partial class VerifyUser : FlexiBusinessRuleBase, IFlexiBusinessRule<VerifyRegisteredDeviceDataPacket>
    {
        public override string Id { get; set; } = "3a131e9e6442d8e1a6c04436915e8f3e";
        public override string FriendlyName { get; set; } = nameof(VerifyUser);

        private readonly ILogger<VerifyUser> _logger;
        private readonly IRepoFactory _repoFactory;
        private readonly MobileSettings _mobileSettings;

        public VerifyUser(ILogger<VerifyUser> logger, IRepoFactory repoFactory, IOptions<MobileSettings> mobileSettings)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _mobileSettings = mobileSettings.Value;
        }

        /// <summary>
        /// Validates the user based on email and optionally mobile number.
        /// </summary>
        public async Task Validate(VerifyRegisteredDeviceDataPacket packet)
        {
            _repoFactory.Init(packet.Dto);

            _logger.LogInformation("VerifyUser: Validation started for Email: {Email}", packet.Dto.Email);
            _logger.LogDebug("Original Packet JSON: {Packet}", JsonConvert.SerializeObject(packet.Dto));

            DecryptSensitiveFields(packet);

            _logger.LogDebug("Decrypted Packet JSON: {Packet}", JsonConvert.SerializeObject(packet.Dto));

            var user = await FindUserByEmailAsync(packet.Dto.Email);
            if (user == null)
            {
                LogAndAddError(packet, "Invalid User", "Invalid user - Email not found or deleted");
                return;
            }

            if (_mobileSettings.DeviceValidation.EnableRegisterDeviceBasedOnMobileNo &&
                (!string.IsNullOrEmpty(packet.Dto.MobileNumberSim1) || !string.IsNullOrEmpty(packet.Dto.MobileNumberSim2)))
            {
                if (!await IsMobileNumberValidAsync(packet.Dto.Email, packet.Dto.MobileNumberSim1, packet.Dto.MobileNumberSim2))
                {
                    LogAndAddError(packet, "Registered mobile number is not available in the device.", "Mobile number mismatch");
                    return;
                }
            }

            _logger.LogInformation("VerifyUser: Validation successful for Email: {Email}", packet.Dto.Email);
        }

        private async Task<ApplicationUser?> FindUserByEmailAsync(string email)
        {
            return await _repoFactory.GetRepo()
                .FindAll<ApplicationUser>()
                .FirstOrDefaultAsync(u => u.PrimaryEMail == email && !u.IsDeleted);
        }

        private async Task<bool> IsMobileNumberValidAsync(string email, string? sim1, string? sim2)
        {
            return await _repoFactory.GetRepo()
                .FindAll<ApplicationUser>()
                .AnyAsync(u =>
                    u.PrimaryEMail == email &&
                    (u.PrimaryMobileNumber == sim1 || u.PrimaryMobileNumber == sim2));
        }

        private void LogAndAddError(VerifyRegisteredDeviceDataPacket packet, string userMessage, string logMessage)
        {
            _logger.LogWarning("VerifyUser: {LogMessage} - Email: {Email}", logMessage, packet.Dto.Email);
            packet.AddError("Error", userMessage);
        }

        /// <summary>
        /// Decrypts sensitive fields like Email and Mobile Numbers.
        /// </summary>
        private void DecryptSensitiveFields(VerifyRegisteredDeviceDataPacket packet)
        {
            var dto = packet.Dto;
            var aes = new AesGcmCrypto();
            var aesKey = Encoding.UTF8.GetBytes(packet.Key);

            dto.Email = aes.Decrypt(dto.Email, aesKey);
            dto.MobileNumberSim1 = DecryptIfPresent(dto.MobileNumberSim1, aes, aesKey);
            dto.MobileNumberSim2 = DecryptIfPresent(dto.MobileNumberSim2, aes, aesKey);
        }

        /// <summary>
        /// Decrypts a string if it's not null or empty.
        /// </summary>
        private string DecryptIfPresent(string? encryptedValue, AesGcmCrypto aes, byte[] key)
            => string.IsNullOrEmpty(encryptedValue) ? string.Empty : aes.Decrypt(encryptedValue, key);
    }
}
