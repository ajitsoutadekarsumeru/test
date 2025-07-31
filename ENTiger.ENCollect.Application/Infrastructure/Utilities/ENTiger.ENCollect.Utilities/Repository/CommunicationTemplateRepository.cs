using ENTiger.ENCollect.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class CommunicationTemplateRepository : ICommunicationTemplateRepository
    {
        private readonly IRepoFactory _repoFactory;

        public CommunicationTemplateRepository(IRepoFactory repoFactory)
        {
            _repoFactory = repoFactory;
        }

        

        public async Task<CommunicationTemplate?> GetByIdAsync(
            FlexAppContextBridge context, string Id)
        {
            _repoFactory.Init(context);
            return await _repoFactory.GetRepo()
            .FindAll<CommunicationTemplate>()
                            .Where(w => w.Id == Id)                            
                            .FirstOrDefaultAsync();
        }
    }
}
