using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Reflection.PortableExecutable;

namespace ENTiger.ENCollect
{
    public class XDaysAfterStatementDateFetchStrategy : IAccountFetchStrategy
    {
        protected readonly ILogger<XDaysAfterStatementDateFetchStrategy> _logger;
        private readonly ILoanAccountQueryRepository  _accountRepository;


        public XDaysAfterStatementDateFetchStrategy(
            ILogger<XDaysAfterStatementDateFetchStrategy> logger,
            ILoanAccountQueryRepository accountRepository
           )
        {
            _logger = logger;
            _accountRepository = accountRepository;
        }
        public string SupportedTriggerTypeId => "";
        public string SupportedTriggerType => TriggerConditionTypeEnum.XDaysAfterStatementDate.Value;

        public async Task<IReadOnlyList<string>> IdentifyActorsAsync(CommunicationTrigger trigger, FlexAppContextBridge context)
        {
            // Use the repository method to fetch accounts with PTP date equal to today
            var today = DateTime.UtcNow.Date;
            var daysOffset = trigger.DaysOffset;
            var accounts = await _accountRepository.GetAccountIdsByXDaysAfterStatementDate(daysOffset, context);

            return accounts;
        }
    }
}