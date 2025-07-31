using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Reflection.PortableExecutable;

namespace ENTiger.ENCollect
{
    public class OnPTPDateAccountFetchStrategy : IAccountFetchStrategy
    {
        protected readonly ILogger<OnPTPDateAccountFetchStrategy> _logger;
        private readonly ILoanAccountQueryRepository  _accountRepository;


        public OnPTPDateAccountFetchStrategy(
            ILogger<OnPTPDateAccountFetchStrategy> logger,
            ILoanAccountQueryRepository accountRepository
           )
        {
            _logger = logger;
            _accountRepository = accountRepository;
        }
        public string SupportedTriggerTypeId => "";
        public string SupportedTriggerType => TriggerConditionTypeEnum.OnPtpDate.Value;

        public async Task<IReadOnlyList<string>> IdentifyActorsAsync(CommunicationTrigger trigger, FlexAppContextBridge context)
        {
            // Use the repository method to fetch accounts with PTP date equal to today
            var today = DateTime.UtcNow.Date;
            var accounts = await _accountRepository.GetAccountIdsByPTPDateAsync(today, context);

            return accounts;
        }
    }
}