using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class UpdateUserWalletOnPayInSlipAcknowledged : IUpdateUserWalletOnPayInSlipAcknowledged
    {
        protected readonly ILogger<UpdateUserWalletOnPayInSlipAcknowledged> _logger;
        protected string EventCondition = "";  // Event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public UpdateUserWalletOnPayInSlipAcknowledged(
            ILogger<UpdateUserWalletOnPayInSlipAcknowledged> logger,
            IRepoFactory repoFactory
            )
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(PayInSlipAcknowledgedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; // Do not remove this line
            _repoFactory.Init(@event);

            /// <summary>
            /// Retrieves the receipt posting configuration to check if wallet updates are enabled.
            /// </summary>
            var walletLimitEnabled = await _repoFactory.GetRepo().FindAll<FeatureMaster>()
                                           .FirstOrDefaultAsync(p => p.Parameter == "WalletLimitEnable");

            bool isWalletLimitEnabled = bool.TryParse(walletLimitEnabled?.Value, out var enabled) && enabled;

            if (!isWalletLimitEnabled)
            {
                _logger.LogWarning("Wallet limit update is disabled in the configuration.");
                return;
            }

            // Convert PayInSlipIds to HashSet for optimized lookup
            var payInSlipIds = @event.PayInSlipIds.ToHashSet();

            // Fetch only relevant collections
            var payInSlips = await _repoFactory.GetRepo()
                                    .FindAll<PayInSlip>()
                                    .Where(m => payInSlipIds.Contains(m.Id) &&
                                                m.ModeOfPayment.Equals(CollectionModeEnum.Cash.Value) &&
                                                m.Lattitude != null && m.Longitude != null)
                                    .ToListAsync();

            if (!payInSlips.Any())
            {
                _logger.LogInformation("No PayInSlips found for Cash Collection Mode.");
                return;
            }

            // Extract unique user IDs from collections
            var userIds = payInSlips.Select(c => c.CreatedBy).Distinct().ToList();

            // Fetch users in a single query and store them in a dictionary
            var users = await _repoFactory.GetRepo().FindAll<ApplicationUser>()
                            .Where(u => userIds.Contains(u.Id)).ToListAsync();                            

            foreach (var user in users)
            {               
                decimal totalAmount = payInSlips.Where(p => p.CreatedBy == user.Id).Sum(p => p.Amount);

                _repoFactory.GetRepo().InsertOrUpdate(user);

                _logger.LogInformation("Updated user {UserId} with total amount {Amount}.", user.Id, totalAmount);
            }
            // Save all updates in a single transaction
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("User wallet updates committed successfully in the database.");
            }
            else
            {
                _logger.LogWarning("No records were updated in the database.");
            }
            // Fire event after successful execution
            await this.Fire<UpdateUserWalletOnPayInSlipAcknowledged>(EventCondition, serviceBusContext);
        }
    }
}
