using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule.AddCompanyUserCompanyUsersPlugins
{
    public partial class CheckWalletLimit : FlexiBusinessRuleBase, IFlexiBusinessRule<AddCompanyUserDataPacket>
    {
        public override string Id { get; set; } = "3a12cbc5ecddc2ff61165a98d27b2613";
        public override string FriendlyName { get; set; } = "CheckWalletLimit";

        protected readonly ILogger<CheckWalletLimit> _logger;
        protected readonly IRepoFactory _repoFactory;


        public CheckWalletLimit(ILogger<CheckWalletLimit> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(AddCompanyUserDataPacket packet)
        {
            _repoFactory.Init(packet.Dto);

            /// <summary>
            /// Retrieves the receipt posting configuration to check if wallet updates are enabled.
            /// </summary>
            var walletLimitEnabled = await _repoFactory.GetRepo().FindAll<FeatureMaster>()
                                            .FirstOrDefaultAsync(p => p.Parameter == "WalletLimitEnable");

            bool isWalletLimitEnabled = bool.TryParse(walletLimitEnabled?.Value, out var enabled) && enabled;

            // Wallet Limit Validation
            if (isWalletLimitEnabled)
            {
                var walletLimitEntry = await _repoFactory.GetRepo().FindAll<FeatureMaster>()
                                                    .FirstOrDefaultAsync(p => p.Parameter == "WalletLimit");

                if (walletLimitEntry != null && decimal.TryParse(walletLimitEntry.Value, out decimal walletLimit))
                {
                    decimal inputWalletLimit = packet.Dto.WalletLimit;

                    if (inputWalletLimit > walletLimit)
                    {
                        packet.AddError("Error", $"The cash wallet amount cannot exceed the defined wallet limit:{walletLimit}.");
                    }
                }
            }
        }
    }
}
