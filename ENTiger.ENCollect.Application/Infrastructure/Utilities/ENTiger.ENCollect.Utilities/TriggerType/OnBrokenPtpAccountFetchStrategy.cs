using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Reflection.PortableExecutable;

namespace ENTiger.ENCollect
{
    public class OnBrokenPtpAccountFetchStrategy : IAccountFetchStrategy
    {
        protected readonly ILogger<OnBrokenPtpAccountFetchStrategy> _logger;
        private readonly ILoanAccountQueryRepository  _accountRepository;


        public OnBrokenPtpAccountFetchStrategy(
            ILogger<OnBrokenPtpAccountFetchStrategy> logger,
            ILoanAccountQueryRepository accountRepository
           )
        {
            _logger = logger;
            _accountRepository = accountRepository;
        }
        public string SupportedTriggerTypeId => "";
        public string SupportedTriggerType => TriggerConditionTypeEnum.OnBrokenPtp.Value;

        public async Task<IReadOnlyList<string>> IdentifyActorsAsync(CommunicationTrigger trigger, FlexAppContextBridge context)
        {
            // Use the repository method to fetch accounts with PTP date equal to today
            var today = DateTime.UtcNow.Date;
            var accounts = await _accountRepository.GetAccountIdsByBrokenPTPDateAsync(today,context);

            return accounts;
        }
    }
}