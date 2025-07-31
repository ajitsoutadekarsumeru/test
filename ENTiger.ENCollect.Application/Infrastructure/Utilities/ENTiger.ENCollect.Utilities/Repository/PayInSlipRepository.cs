using Microsoft.EntityFrameworkCore;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class PayInSlipRepository: IPayInSlipRepository
    {
        private readonly IRepoFactory _repoFactory;

        public PayInSlipRepository(IRepoFactory repoFactory)
        {
            _repoFactory = repoFactory;
        }
        public async Task<PayInSlip?> GetPayInSlipByIdAsync(string id, FlexAppContextBridge context)
        {
            _repoFactory.Init(context);
            return await _repoFactory.GetRepo()
                .FindAll<PayInSlip>()
                .FlexInclude(x => x.CollectionBatches)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
