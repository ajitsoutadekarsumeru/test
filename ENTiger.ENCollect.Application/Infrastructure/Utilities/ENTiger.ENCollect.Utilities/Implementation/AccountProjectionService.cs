using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    public class LoanAccountProjectionService : ILoanAccountProjectionService
    {
        private readonly IRepoFactory _repoFactory;
        private readonly DynamicLoanAccountFieldFetcherService _fetcher;
        public LoanAccountProjectionService(IRepoFactory repoFactory, DynamicLoanAccountFieldFetcherService fetcher)
        {
            _repoFactory = repoFactory;
            _fetcher = fetcher;
        }
        public async Task<List<Dictionary<string, object?>>> GetAccountProjectionsAsync(
            string triggerId, 
            string triggerRunId, 
            List<string> fields,
            FlexAppContextBridge context)
        {
            _repoFactory.Init(context);
            var projections = new List<Dictionary<string, object?>>();
            var accountIds = _repoFactory.GetRepo().FindAll<TriggerAccountQueueProjection>()
                            .Where(a => a.TriggerId == triggerId && a.RunId == triggerRunId)
                            .Select(b=>b.AccountId)
                            .ToList();

            foreach (var accountId in accountIds)
            {
                var projection = await _fetcher.FetchFieldsAsync(a => a.Id == accountId, fields);
                projections.Add(projection);
            }

            return projections;
        }
    }


}
