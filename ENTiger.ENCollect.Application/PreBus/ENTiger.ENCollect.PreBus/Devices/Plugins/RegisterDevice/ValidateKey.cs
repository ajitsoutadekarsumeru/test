using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.DevicesModule.RegisterDeviceDevicesPlugins
{
    /// <summary>
    /// Business rule to validate a device registration key.
    /// </summary>
    public partial class ValidateKey : FlexiBusinessRuleBase, IFlexiBusinessRule<RegisterDeviceDataPacket>
    {
        public override string Id { get; set; } = "3a1347a8bc32cac68e3cff1affb96911";
        public override string FriendlyName { get; set; } = "ValidateKey";

        private readonly ILogger<ValidateKey> _logger;
        private readonly IRepoFactory _repoFactory;

        public ValidateKey(ILogger<ValidateKey> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// Validates the device registration packet.
        /// </summary>
        public async Task Validate(RegisterDeviceDataPacket packet)
        {
            _repoFactory.Init(packet.Dto);

            if (packet.HasError) return;

            string generatedKey = await ValidateReferenceKeyAsync(packet, packet.Dto.ReferenceId);
            packet.Key = generatedKey;
        }

        /// <summary>
        /// Validates the reference key using the repository.
        /// </summary>
        private async Task<string> ValidateReferenceKeyAsync(FlexiFlowDataPacket packet, string referenceId)
        {
            _logger.LogInformation("ValidateKey: Starting key validation for ReferenceId: {ReferenceId}", referenceId);

            var repo = _repoFactory.GetRepo();
            var keyEntity = await repo.Find<UserLoginKeys>(referenceId).FirstOrDefaultAsync();

            if (keyEntity == null)
            {
                _logger.LogWarning("ValidateKey: Invalid ReferenceId: {ReferenceId}", referenceId);
                packet.AddError("Error", "Invalid Request. Please contact administrator");
                return string.Empty;
            }

            if (!keyEntity.IsActive)
            {
                _logger.LogInformation("ValidateKey: Key is inactive for ReferenceId: {ReferenceId}", referenceId);
                packet.AddError("Error", "Please refresh and try again.");
                return string.Empty;
            }

            _logger.LogInformation("ValidateKey: Valid and active key found for ReferenceId: {ReferenceId}", referenceId);

            keyEntity.IsActive = false;
            keyEntity.SetLastModifiedDate(DateTimeOffset.Now);
            keyEntity.SetAddedOrModified();

            repo.InsertOrUpdate(keyEntity);
            await repo.SaveAsync();

            _logger.LogInformation("ValidateKey: Key successfully deactivated for ReferenceId: {ReferenceId}", referenceId);

            return keyEntity.Key;
        }
    }
}
