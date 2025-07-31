using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Reflection.PortableExecutable;

namespace ENTiger.ENCollect
{
    public class OnAgencyAllocationChangeAccountFetchStrategy : IAccountFetchStrategy
    {
        protected readonly ILogger<OnAgencyAllocationChangeAccountFetchStrategy> _logger;
        private readonly ILoanAccountQueryRepository  _accountRepository;


        public OnAgencyAllocationChangeAccountFetchStrategy(
            ILogger<OnAgencyAllocationChangeAccountFetchStrategy> logger,
            ILoanAccountQueryRepository accountRepository
           )
        {
            _logger = logger;
            _accountRepository = accountRepository;
        }

        public string SupportedTriggerTypeId => "";

        public string SupportedTriggerType => TriggerConditionTypeEnum.OnAgencyAllocationChange.Value;

        public async Task<IReadOnlyList<string>> IdentifyActorsAsync(CommunicationTrigger trigger)
        {
            // Use the repository method to fetch accounts with PTP date equal to today
            var today = DateTime.UtcNow.Date;
            var accounts = await _accountRepository.GetAccountIdsByAgencyAllocationDateAsync(today);

            return accounts;
        }

        public Task<IReadOnlyList<string>> IdentifyActorsAsync(CommunicationTrigger trigger, FlexAppContextBridge context)
        {
            //throw new NotImplementedException();
            return Task.FromResult<IReadOnlyList<string>>(new List<string>());
        }
    }
}