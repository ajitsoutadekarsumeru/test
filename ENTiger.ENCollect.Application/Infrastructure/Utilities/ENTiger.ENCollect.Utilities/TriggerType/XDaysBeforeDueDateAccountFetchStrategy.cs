using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Reflection.PortableExecutable;

namespace ENTiger.ENCollect
{
    public class XDaysBeforeDueDateAccountFetchStrategy : IAccountFetchStrategy
    {
        protected readonly ILogger<XDaysBeforeDueDateAccountFetchStrategy> _logger;
        private readonly ILoanAccountQueryRepository  _accountRepository;


        public XDaysBeforeDueDateAccountFetchStrategy(
            ILogger<XDaysBeforeDueDateAccountFetchStrategy> logger,
            ILoanAccountQueryRepository accountRepository
           )
        {
            _logger = logger;
            _accountRepository = accountRepository;
        }
        public string SupportedTriggerTypeId => "";
        public string SupportedTriggerType => TriggerConditionTypeEnum.XDaysBeforeDueDate.Value;

        public async Task<IReadOnlyList<string>> IdentifyActorsAsync(CommunicationTrigger trigger, FlexAppContextBridge context)
        {
            // Use the repository method to fetch accounts with PTP date equal to today
            var daysOffset = trigger.DaysOffset;
            var accounts = await _accountRepository.GetAccountIdsByXDaysBeforeDueDate(daysOffset,context);

            return accounts;
        }
    }
}