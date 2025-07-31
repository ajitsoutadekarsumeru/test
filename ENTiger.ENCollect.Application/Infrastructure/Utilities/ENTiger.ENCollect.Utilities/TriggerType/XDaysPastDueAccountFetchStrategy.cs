using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Reflection.PortableExecutable;

namespace ENTiger.ENCollect
{
    public class XDaysPastDueAccountFetchStrategy : IAccountFetchStrategy
    {
        protected readonly ILogger<XDaysPastDueAccountFetchStrategy> _logger;
        private readonly ILoanAccountQueryRepository  _accountRepository;


        public XDaysPastDueAccountFetchStrategy(
            ILogger<XDaysPastDueAccountFetchStrategy> logger,
            ILoanAccountQueryRepository accountRepository
           )
        {
            _logger = logger;
            _accountRepository = accountRepository;
        }
        public string SupportedTriggerTypeId => "";
        public string SupportedTriggerType => TriggerConditionTypeEnum.XDaysPastDue.Value;

        public async Task<IReadOnlyList<string>> IdentifyActorsAsync(CommunicationTrigger trigger, FlexAppContextBridge context)
        {
            // Use the repository method to fetch accounts with PTP date equal to today
            var today = DateTime.UtcNow.Date;
            var daysOffset = trigger.DaysOffset;
            var accounts = await _accountRepository.GetAccountIdsByXDaysPastDueDate(daysOffset, context);

            return accounts;
        }
    }
}